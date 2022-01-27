namespace StashCat.Api;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            throw new Exception($"ClientKey needs to be initialized before calling {GetMyCompaniesAsync}");
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
            throw new Exception($"ClientKey needs to be initialized before calling {GetMyCompaniesAsync}");
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
}