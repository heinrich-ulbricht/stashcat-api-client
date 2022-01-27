namespace StashCat.Model;

using Newtonsoft.Json;
public partial class UserStatus
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string? Id { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
    public string? Created { get; set; }

    [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
    public string? Active { get; set; }

    [JsonProperty("notifications", NullValueHandling = NullValueHandling.Ignore)]
    public string? Notifications { get; set; }

    [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? UserId { get; set; }
}