namespace StashCat.Model;

using Newtonsoft.Json;
public partial class Channel
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string? Id { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string? Description { get; set; }

    [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
    public string? Image { get; set; }

    [JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
    public string? Company { get; set; }

    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string? Type { get; set; }

    [JsonProperty("visible", NullValueHandling = NullValueHandling.Ignore)]
    public string? Visible { get; set; }

    [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
    public string? Password { get; set; }

    [JsonProperty("encrypted", NullValueHandling = NullValueHandling.Ignore)]
    public string? Encrypted { get; set; }

    [JsonProperty("encryption", NullValueHandling = NullValueHandling.Ignore)]
    public string? Encryption { get; set; }

    [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
    public string? Key { get; set; }

    [JsonProperty("key_requested", NullValueHandling = NullValueHandling.Ignore)]
    public string? KeyRequested { get; set; }

    [JsonProperty("last_action", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastAction { get; set; }

    [JsonProperty("last_activity", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastActivity { get; set; }

    [JsonProperty("favorite", NullValueHandling = NullValueHandling.Ignore)]
    public string? Favorite { get; set; }

    [JsonProperty("manager", NullValueHandling = NullValueHandling.Ignore)]
    public string? Manager { get; set; }

    [JsonProperty("writable", NullValueHandling = NullValueHandling.Ignore)]
    public string? Writable { get; set; }

    [JsonProperty("inviteable", NullValueHandling = NullValueHandling.Ignore)]
    public string? Inviteable { get; set; }

    [JsonProperty("can_leave", NullValueHandling = NullValueHandling.Ignore)]
    public string? CanLeave { get; set; }

    [JsonProperty("membership", NullValueHandling = NullValueHandling.Ignore)]
    public Membership? Membership { get; set; }

    [JsonProperty("user_count", NullValueHandling = NullValueHandling.Ignore)]
    public string? UserCount { get; set; }

    [JsonProperty("unread", NullValueHandling = NullValueHandling.Ignore)]
    public string? Unread { get; set; }

    [JsonProperty("group_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? GroupId { get; set; }

    [JsonProperty("ldap_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? LdapName { get; set; }

    [JsonProperty("show_membership_activities", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ShowMembershipActivities { get; set; }
}

public partial class Membership
{
    [JsonProperty("is_member", NullValueHandling = NullValueHandling.Ignore)]
    public string? IsMember { get; set; }

    [JsonProperty("joined", NullValueHandling = NullValueHandling.Ignore)]
    public string? Joined { get; set; }

    [JsonProperty("confirmation", NullValueHandling = NullValueHandling.Ignore)]
    public string? Confirmation { get; set; }

    [JsonProperty("may_manage", NullValueHandling = NullValueHandling.Ignore)]
    public string? MayManage { get; set; }

    [JsonProperty("write", NullValueHandling = NullValueHandling.Ignore)]
    public string? Write { get; set; }

    [JsonProperty("muted")]
    public object? Muted { get; set; }
}