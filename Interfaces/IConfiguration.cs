namespace StashCat.Api;

using System.ComponentModel.DataAnnotations;
public interface IConfiguration
{
    public string StashCatBaseUrl { get; set; }
    public string StashCatUsername { get; set; }
    public string StashCatPassword { get; set; }
    [StringLength(32, MinimumLength = 32)]
    public string UniqueDeviceId { get; set; }
    public string? StashCatEncryptionKey { get; set; }
}