namespace StashCat.Model;

using System.Collections.Generic;
using Newtonsoft.Json;
public partial class Userinfo
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string? Id { get; set; }

    [JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? FirstName { get; set; }

    [JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastName { get; set; }

    [JsonProperty("socket_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? SocketId { get; set; }

    [JsonProperty("online", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Online { get; set; }

    [JsonProperty("status")]
    public object? Status { get; set; }

    [JsonProperty("user_status", NullValueHandling = NullValueHandling.Ignore)]
    public List<object>? UserStatus { get; set; }

    [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
    public string? Active { get; set; }

    [JsonProperty("deleted")]
    public object? Deleted { get; set; }

    [JsonProperty("allows_voip_calls", NullValueHandling = NullValueHandling.Ignore)]
    public bool? AllowsVoipCalls { get; set; }

    [JsonProperty("enter_is_newline", NullValueHandling = NullValueHandling.Ignore)]
    public bool? EnterIsNewline { get; set; }

    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty("email_validated", NullValueHandling = NullValueHandling.Ignore)]
    public string? EmailValidated { get; set; }

    [JsonProperty("notifications", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Notifications { get; set; }

    [JsonProperty("device_notifications", NullValueHandling = NullValueHandling.Ignore)]
    public bool? DeviceNotifications { get; set; }

    [JsonProperty("last_login", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastLogin { get; set; }

    [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
    public string? Language { get; set; }

    [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
    public string? Image { get; set; }

    [JsonProperty("quota", NullValueHandling = NullValueHandling.Ignore)]
    public string? Quota { get; set; }

    [JsonProperty("ldap_login")]
    public object? LdapLogin { get; set; }

    [JsonProperty("public_key", NullValueHandling = NullValueHandling.Ignore)]
    public string? PublicKey { get; set; }

    [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
    public List<Role>? Roles { get; set; }

    [JsonProperty("permissions", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Permissions { get; set; }

    [JsonProperty("company_features", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? CompanyFeatures { get; set; }

    [JsonProperty("settings", NullValueHandling = NullValueHandling.Ignore)]
    public Settings_UserInfo? Settings { get; set; }
}

public partial class Settings_UserInfo
{
    [JsonProperty("device_pin", NullValueHandling = NullValueHandling.Ignore)]
    public string? DevicePin { get; set; }

    [JsonProperty("device_pin_delay", NullValueHandling = NullValueHandling.Ignore)]
    public long? DevicePinDelay { get; set; }

    [JsonProperty("device_gps", NullValueHandling = NullValueHandling.Ignore)]
    public string? DeviceGps { get; set; }

    [JsonProperty("device_encryption", NullValueHandling = NullValueHandling.Ignore)]
    public string? DeviceEncryption { get; set; }

    [JsonProperty("file_export", NullValueHandling = NullValueHandling.Ignore)]
    public bool? FileExport { get; set; }

    [JsonProperty("file_import", NullValueHandling = NullValueHandling.Ignore)]
    public bool? FileImport { get; set; }

    [JsonProperty("share_links", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ShareLinks { get; set; }

    [JsonProperty("encryption", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Encryption { get; set; }

    [JsonProperty("open_channels", NullValueHandling = NullValueHandling.Ignore)]
    public bool? OpenChannels { get; set; }

    [JsonProperty("autostart", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Autostart { get; set; }

    [JsonProperty("lockscreen_content", NullValueHandling = NullValueHandling.Ignore)]
    public string? LockscreenContent { get; set; }

    [JsonProperty("client_count")]
    public object? ClientCount { get; set; }

    [JsonProperty("must_validate_email", NullValueHandling = NullValueHandling.Ignore)]
    public bool? MustValidateEmail { get; set; }

    [JsonProperty("email_validation", NullValueHandling = NullValueHandling.Ignore)]
    public string? EmailValidation { get; set; }

    [JsonProperty("may_change_email", NullValueHandling = NullValueHandling.Ignore)]
    public bool? MayChangeEmail { get; set; }

    [JsonProperty("may_change_password", NullValueHandling = NullValueHandling.Ignore)]
    public bool? MayChangePassword { get; set; }

    [JsonProperty("manual_account_creation", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ManualAccountCreation { get; set; }

    [JsonProperty("ttl_content")]
    public object? TtlContent { get; set; }

    [JsonProperty("ttl_marked_content")]
    public object? TtlMarkedContent { get; set; }

    [JsonProperty("ttl_server_content")]
    public object? TtlServerContent { get; set; }

    [JsonProperty("can_delete_messages", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CanDeleteMessages { get; set; }

    [JsonProperty("share_unencrypted_files_into_encrypted_chats", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ShareUnencryptedFilesIntoEncryptedChats { get; set; }

    [JsonProperty("force_device_notifications", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ForceDeviceNotifications { get; set; }

    [JsonProperty("device_login_management", NullValueHandling = NullValueHandling.Ignore)]
    public bool? DeviceLoginManagement { get; set; }

    [JsonProperty("key_password_policy", NullValueHandling = NullValueHandling.Ignore)]
    public string? KeyPasswordPolicy { get; set; }

    [JsonProperty("giphy", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Giphy { get; set; }

    [JsonProperty("voip", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Voip { get; set; }

    [JsonProperty("self_deletion", NullValueHandling = NullValueHandling.Ignore)]
    public bool? SelfDeletion { get; set; }

    [JsonProperty("link_preview", NullValueHandling = NullValueHandling.Ignore)]
    public bool? LinkPreview { get; set; }

    [JsonProperty("password_restrictions", NullValueHandling = NullValueHandling.Ignore)]
    public PasswordRestrictions? PasswordRestrictions { get; set; }

    [JsonProperty("mdm", NullValueHandling = NullValueHandling.Ignore)]
    public Mdm? Mdm { get; set; }
}