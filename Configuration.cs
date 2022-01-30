namespace StashCat.Api;

public class Configuration : IConfiguration
{
    public string StashCatBaseUrl { get; set; }
    public string StashCatUsername { get; set; }
    public string StashCatPassword { get; set; }
    public string UniqueDeviceId { get; set; }
    public string? StashCatEncryptionKey { get; set; }

    public Configuration(string baseUrl, string username, string password, string uniqueDeviceId)
    {
        StashCatBaseUrl = baseUrl;
        StashCatUsername = username;
        StashCatPassword = password;
        UniqueDeviceId = uniqueDeviceId;
    }
}