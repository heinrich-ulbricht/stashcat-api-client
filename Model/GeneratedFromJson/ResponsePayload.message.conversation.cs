namespace StashCat.Model;

using System.Collections.Generic;
using Newtonsoft.Json;

public partial class Conversation
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string? Id { get; set; }

    [JsonProperty("name")]
    public object? Name { get; set; }

    [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
    public string? Created { get; set; }

    [JsonProperty("last_action", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastAction { get; set; }

    [JsonProperty("last_activity", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastActivity { get; set; }

    [JsonProperty("encrypted", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Encrypted { get; set; }

    [JsonProperty("members", NullValueHandling = NullValueHandling.Ignore)]
    public List<Callable>? Members { get; set; }

    [JsonProperty("user_count", NullValueHandling = NullValueHandling.Ignore)]
    public long? UserCount { get; set; }

    [JsonProperty("unread_messages", NullValueHandling = NullValueHandling.Ignore)]
    public long? UnreadMessages { get; set; }

    [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
    public string? Key { get; set; }

    [JsonProperty("key_requested")]
    public object? KeyRequested { get; set; }

    [JsonProperty("archive")]
    public object? Archive { get; set; }

    [JsonProperty("favorite", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Favorite { get; set; }

    [JsonProperty("deleted")]
    public object? Deleted { get; set; }

    [JsonProperty("muted")]
    public object? Muted { get; set; }

    [JsonProperty("members_without_keys", NullValueHandling = NullValueHandling.Ignore)]
    public List<object>? MembersWithoutKeys { get; set; }

    [JsonProperty("num_members_without_keys", NullValueHandling = NullValueHandling.Ignore)]
    public long? NumMembersWithoutKeys { get; set; }

    [JsonProperty("callable", NullValueHandling = NullValueHandling.Ignore)]
    public List<Callable>? Callable { get; set; }
}

public partial class Callable
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string? Id { get; set; }

    [JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? FirstName { get; set; }

    [JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastName { get; set; }

    [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
    public string? Image { get; set; }

    [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
    public string? Active { get; set; }

    [JsonProperty("deleted")]
    public object? Deleted { get; set; }

    [JsonProperty("allows_voip_calls", NullValueHandling = NullValueHandling.Ignore)]
    public bool? AllowsVoipCalls { get; set; }

    [JsonProperty("online", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Online { get; set; }

    [JsonProperty("public_key", NullValueHandling = NullValueHandling.Ignore)]
    public string? PublicKey { get; set; }

    // "de"
    [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
    public string? Language { get; set; }

    [JsonProperty("user_status", NullValueHandling = NullValueHandling.Ignore)]
    public UserStatus? UserStatus { get; set; }

    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }
}