namespace StashCat.Api;
#nullable enable
public interface IConfiguration
{
    public string StashCatBaseUrl { get; set; }
    public string StashCatUsername { get; set; }
    public string StashCatPassword { get; set; }
    public string? StashCatEncryptionKey { get; set; }
}