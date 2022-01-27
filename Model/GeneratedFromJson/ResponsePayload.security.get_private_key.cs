namespace StashCat.Model;

using Newtonsoft.Json;
public partial class Keys
{
    [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? UserId { get; set; }

    [JsonProperty("private_key", NullValueHandling = NullValueHandling.Ignore)]
    public string? PrivateKey { get; set; }

    [JsonProperty("public_key", NullValueHandling = NullValueHandling.Ignore)]
    public string? PublicKey { get; set; }

    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public string? Time { get; set; }

    [JsonProperty("deleted")]
    public object? Deleted { get; set; }

    [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
    public long? Version { get; set; }
}

public partial class PrivateKey
{
    [JsonProperty("private", NullValueHandling = NullValueHandling.Ignore)]
    public string? Private { get; set; }
}