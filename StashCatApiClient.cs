namespace StashCat.Api;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StashCat.Model;
using StashCat.Notifications;
using StashCat.Tools;

public class StashCatApiClient
{
    public string StashCatVersion { get; set; } = "4.13.0";
    private string? ClientKey { get; set; }
    private string? UserId { get; set; }
    private string? PrivateEncryptedKey { get; set; }
    private string? PublicKey { get; set; }
    public string? SocketId { get; private set; }
    private RSACryptoServiceProvider? Cipher { get; set; }
    public Company? Company { get; private set; }
    public List<Conversation> Conversations { get; private set; } = new List<Conversation>();
    public List<Channel> Channels { get; private set; } = new List<Channel>();
    public IDistributedCache Cache { get; set; } = new NoOpCache();

    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private const long _loginCacheLifetimeDays = 30;
    private readonly HttpClient _httpClient = InitHttpClient();
    public StashCatNotificationClient? NotificationClient { get; set; }

    public string AppName
    {
        get
        {
            return $"schul.cloud-browser-Chrome:94.0.4606.81-{StashCatVersion}";
        }
    }

    public StashCatApiClient(ILogger logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    private static HttpClient InitHttpClient()
    {
        var handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
        };
        var httpClient = new HttpClient(handler);
        var headers = httpClient.DefaultRequestHeaders;
        headers.Accept.Clear();

        headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/x-www-form-urlencoded; charset=UTF-8"));
        headers.AcceptEncoding.Clear();
        headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
        headers.AcceptLanguage.Clear();
        headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US", 0.5));
        headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en", 0.5));
        headers.CacheControl = CacheControlHeaderValue.Parse("no-cache");
        headers.Connection.Clear();
        headers.Connection.Add("keep-alive");
        headers.UserAgent.Clear();
        headers.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
        return httpClient;
    }

    public async Task ClearCacheAsync()
    {
        await Cache.RemoveAsync($"{_configuration.StashCatUsername}-auth-login-result");
        await Cache.RemoveAsync($"{_configuration.StashCatUsername}-security-get_private_key-result");
    }

    // note: every login will generate an event in other clients; thus you are strongly advised to assign a Cache instance to save the API result for reuse on future logins
    // note: when running on Azure DistributedCache.AzureTableStorage works well as cache implementation with Azure Table Storage backend
    public async Task LoginAsync()
    {
        _logger.LogDebug("Entering {Function}, logging in as {Username}", nameof(LoginAsync), _configuration.StashCatUsername);

        string? apiResultString;
        var apiResultStringCacheKey = $"{_configuration.StashCatUsername}-auth-login-result";
        try
        {
            apiResultString = await Cache.GetStringAsync(apiResultStringCacheKey);
        }
        catch
        {
            apiResultString = "";
        }

        StashCatTopLevelResponse? apiResponse;
        if (string.IsNullOrEmpty(apiResultString))
        {
            _logger.LogDebug("Found no cached login data, getting from API");
            var payloadFormData = new Dictionary<string, string>();
            payloadFormData.Add("email", _configuration.StashCatUsername);
            payloadFormData.Add("password", _configuration.StashCatPassword);
            payloadFormData.Add("device_id", _configuration.UniqueDeviceId);
            payloadFormData.Add("app_name", AppName);
            payloadFormData.Add("encrypted", "true");
            payloadFormData.Add("callable", "true");
            var payload = new FormUrlEncodedContent(payloadFormData);

            (_, apiResultString, apiResponse) = await _httpClient.PostWithGuaranteedSuccess($"{_configuration.StashCatBaseUrl}/auth/login", payload);

            var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_loginCacheLifetimeDays) };
            await Cache.SetStringAsync(apiResultStringCacheKey, apiResultString, cacheOptions);
        }
        else
        {
            _logger.LogDebug("Using cached login data");
            apiResponse = JsonConvert.DeserializeObject<StashCatTopLevelResponse>(apiResultString);
        }

        ClientKey = apiResponse?.Payload?.ClientKey;
        UserId = apiResponse?.Payload?.Userinfo?.Id;
        SocketId = apiResponse?.Payload?.Userinfo?.SocketId;

        try
        {
            InitNotificationClient();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception while initializing the notification client. Ignoring, push notifications not available.");
            NotificationClient = null;
        }

        _logger.LogDebug("Leaving {Function}", nameof(LoginAsync));
    }

    public async Task GetPrivateKeyAsync()
    {
        _logger.LogDebug("Entering {Function}", nameof(GetPrivateKeyAsync));
        if (string.IsNullOrEmpty(ClientKey))
        {
            throw new Exception("ClientKey needs to be initialized before getting private key");
        }

        string? apiResultString;
        var apiResultStringCacheKey = $"{_configuration.StashCatUsername}-security-get_private_key-result";
        try
        {
            apiResultString = await Cache.GetStringAsync(apiResultStringCacheKey);
        }
        catch
        {
            apiResultString = "";
        }

        StashCatTopLevelResponse? apiResponse;
        if (string.IsNullOrEmpty(apiResultString))
        {
            _logger.LogDebug("Found no cached private key, getting from API");
            var payloadFormData = new Dictionary<string, string>();
            payloadFormData.Add("client_key", ClientKey);
            payloadFormData.Add("device_id", _configuration.UniqueDeviceId);
            HttpContent payload = new FormUrlEncodedContent(payloadFormData);

            (_, apiResultString, apiResponse) = await _httpClient.PostWithGuaranteedSuccess($"{_configuration.StashCatBaseUrl}/security/get_private_key", payload);

            var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_loginCacheLifetimeDays) };
            await Cache.SetStringAsync(apiResultStringCacheKey, apiResultString, cacheOptions);
        }
        else
        {
            _logger.LogDebug("Using cached private key");
            apiResponse = JsonConvert.DeserializeObject<StashCatTopLevelResponse>(apiResultString);
        }

        var privateKey = JsonConvert.DeserializeObject<PrivateKey>(apiResponse?.Payload?.Keys?.PrivateKey ?? "{}");
        PrivateEncryptedKey = privateKey?.Private;
        PublicKey = apiResponse?.Payload?.Keys?.PublicKey;
        _logger.LogDebug("Leaving {Function}", nameof(GetPrivateKeyAsync));
    }

    public void UnlockPrivateKeyAsync(string encryptionKey)
    {
        RSACryptoServiceProvider cipher = new RSACryptoServiceProvider();

        var passwordBytes = Encoding.ASCII.GetBytes(encryptionKey);
        cipher.ImportFromEncryptedPem(this.PrivateEncryptedKey, passwordBytes);
        this.Cipher = cipher;
    }

    // tested with channel message notification; conversation tbd
    public async Task<string?> DecodeMessageAsync(string? message_hexEncoded, string? channelOrConversationId, string? iv_hexEncoded)
    {
        _logger.LogDebug("Trying to get channel or conversation with ID {Id}", channelOrConversationId);

        if (string.IsNullOrWhiteSpace(message_hexEncoded) || string.IsNullOrEmpty(iv_hexEncoded) || string.IsNullOrEmpty(channelOrConversationId))
        {
            _logger.LogWarning("Cannot decode message if message, channel ID or IV are empty.");
            return null;
        }

        if (null == Cipher)
        {
            throw new Exception($"Cipher is not initialized, call {nameof(UnlockPrivateKeyAsync)} first.");
        }

        _logger.LogDebug("Trying channel first...", channelOrConversationId);
        var conversationKey_base64Encoded = Channels.SingleOrDefault(c => c.Id == channelOrConversationId)?.Key;
        if (conversationKey_base64Encoded == null)
        {
            _logger.LogDebug("No luck, trying conversations...", channelOrConversationId);
            conversationKey_base64Encoded = Conversations.SingleOrDefault(c => c.Id == channelOrConversationId)?.Key;
            if (conversationKey_base64Encoded == null)
            {
                _logger.LogDebug("No luck, trying with updated channels again...", channelOrConversationId);
                await GetChannelsAsync();
                conversationKey_base64Encoded = Channels.SingleOrDefault(c => c.Id == channelOrConversationId)?.Key;
                if (conversationKey_base64Encoded == null)
                {
                    _logger.LogDebug("No luck, trying with updated conversations again...", channelOrConversationId);
                    await GetConversationsAsync();
                    conversationKey_base64Encoded = Conversations.SingleOrDefault(c => c.Id == channelOrConversationId)?.Key;
                }
            }
        }
        if (null == conversationKey_base64Encoded)
        {
            throw new Exception($"Cannot find channel or conversation with ID {channelOrConversationId}");
        }
        _logger.LogDebug("Found conversation key, good");

        var decodedConversationKey = Convert.FromBase64String(conversationKey_base64Encoded);
        var decryptedConversationKey = Cipher.Decrypt(decodedConversationKey, true); // found no docs about the second parameter, but it worked

        var cipheredData = Convert.FromHexString(message_hexEncoded);
        using MemoryStream ms = new MemoryStream(cipheredData);
        using Aes aes = Aes.Create();
        aes.Padding = PaddingMode.PKCS7; // found no docs about this, but it worked
        aes.Mode = CipherMode.CBC;
        aes.Key = decryptedConversationKey;
        aes.IV = Convert.FromHexString(iv_hexEncoded);

        ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);
        using CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read);
        using StreamReader sr = new StreamReader(cs, Encoding.UTF8);

        var decrypted = sr.ReadToEnd();
        return decrypted;
    }

    public async Task GetConversationsAsync()
    {
        _logger.LogDebug("Entering {Function}", nameof(GetConversationsAsync));
        if (string.IsNullOrEmpty(ClientKey))
        {
            throw new Exception("ClientKey needs to be initialized before getting private key");
        }
        var payloadFormData = new Dictionary<string, string>();
        payloadFormData.Add("client_key", ClientKey);
        payloadFormData.Add("device_id", _configuration.UniqueDeviceId);
        payloadFormData.Add("limit", "30");
        payloadFormData.Add("offset", "0");
        payloadFormData.Add("archive", "0");
        var payload = new FormUrlEncodedContent(payloadFormData);

        var (_, _, apiResponse) = await _httpClient.PostWithGuaranteedSuccess($"{_configuration.StashCatBaseUrl}/message/conversations", payload);

        _logger.LogDebug("Got {Count} conversations", apiResponse?.Payload?.Conversations?.Count);
        Conversations = apiResponse?.Payload?.Conversations ?? new List<Conversation>();

        _logger.LogDebug("Leaving {Function}", nameof(GetConversationsAsync));
    }

    public async Task GetMyCompaniesAsync()
    {
        _logger.LogDebug("Entering {Function}", nameof(GetMyCompaniesAsync));
        if (string.IsNullOrEmpty(ClientKey))
        {
            throw new Exception($"ClientKey needs to be initialized before calling {nameof(GetMyCompaniesAsync)}");
        }
        var payloadFormData = new Dictionary<string, string>();
        payloadFormData.Add("client_key", ClientKey);
        payloadFormData.Add("device_id", _configuration.UniqueDeviceId);
        payloadFormData.Add("no_cache", "true");
        var payload = new FormUrlEncodedContent(payloadFormData);

        var (_, _, apiResponse) = await _httpClient.PostWithGuaranteedSuccess($"{_configuration.StashCatBaseUrl}/company/member", payload);

        // currently expect single company
        Company = apiResponse?.Payload?.Companies?.Single();
        _logger.LogDebug("Leaving {Function}", nameof(GetMyCompaniesAsync));
    }

    public async Task GetChannelsAsync()
    {
        _logger.LogDebug("Entering {Function}", nameof(GetChannelsAsync));
        if (null == Company)
        {
            await GetMyCompaniesAsync();
        }
        if (string.IsNullOrEmpty(Company?.Id))
        {
            throw new ArgumentNullException("Missing company ID, cannot retrieve channels.");
        }
        if (string.IsNullOrEmpty(ClientKey))
        {
            throw new Exception($"ClientKey needs to be initialized before calling {nameof(GetMyCompaniesAsync)}");
        }
        var payloadFormData = new Dictionary<string, string>();
        payloadFormData.Add("client_key", ClientKey);
        payloadFormData.Add("device_id", _configuration.UniqueDeviceId);
        payloadFormData.Add("company", Company.Id);
        var payload = new FormUrlEncodedContent(payloadFormData);

        var (_, _, apiResponse) = await _httpClient.PostWithGuaranteedSuccess($"{_configuration.StashCatBaseUrl}/channels/subscripted", payload);
        _logger.LogDebug("Got {Count} channels", apiResponse?.Payload?.Channels?.Count);
        Channels = apiResponse?.Payload?.Channels ?? new List<Channel>();

        _logger.LogDebug("Leaving {Function}", nameof(GetChannelsAsync));
    }

    private void InitNotificationClient()
    {
        if (string.IsNullOrEmpty(ClientKey))
        {
            throw new Exception($"ClientKey needs to be initialized before calling {nameof(InitNotificationClient)}. Call {nameof(LoginAsync)} first.");
        }
        if (string.IsNullOrEmpty(SocketId))
        {
            throw new Exception($"SocketId needs to be initialized before calling {nameof(InitNotificationClient)}. Call {nameof(LoginAsync)} first.");
        }

        var notificationConfiguration = new StashCat.Notifications.Configuration(_configuration.UniqueDeviceId, ClientKey, SocketId)
        {
            // maybe in the future there is the need to configure the push endpoint; for now the default works (for my use case at least...)
        };
        NotificationClient = new StashCatNotificationClient(_logger, notificationConfiguration);
    }
}