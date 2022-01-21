#nullable enable
namespace StashCat.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public partial class Company
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty("manager", NullValueHandling = NullValueHandling.Ignore)]
        public Manager? Manager { get; set; }

        [JsonProperty("quota", NullValueHandling = NullValueHandling.Ignore)]
        public string? Quota { get; set; }

        [JsonProperty("max_users")]
        public object? MaxUsers { get; set; }

        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public string? Created { get; set; }

        [JsonProperty("deleted")]
        public object? Deleted { get; set; }

        [JsonProperty("online_payment", NullValueHandling = NullValueHandling.Ignore)]
        public string? OnlinePayment { get; set; }

        [JsonProperty("freemium", NullValueHandling = NullValueHandling.Ignore)]
        public string? Freemium { get; set; }

        [JsonProperty("logo", NullValueHandling = NullValueHandling.Ignore)]
        public string? Logo { get; set; }

        [JsonProperty("logo_url", NullValueHandling = NullValueHandling.Ignore)]
        public string? LogoUrl { get; set; }

        [JsonProperty("users", NullValueHandling = NullValueHandling.Ignore)]
        public Users? Users { get; set; }

        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Features { get; set; }

        [JsonProperty("provider", NullValueHandling = NullValueHandling.Ignore)]
        public string? Provider { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address? Address { get; set; }

        [JsonProperty("protected", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Protected { get; set; }

        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public List<Role>? Roles { get; set; }

        [JsonProperty("permission", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Permission { get; set; }

        [JsonProperty("settings", NullValueHandling = NullValueHandling.Ignore)]
        public Settings_Company? Settings { get; set; }

        [JsonProperty("domains", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Domains { get; set; }

        [JsonProperty("domain", NullValueHandling = NullValueHandling.Ignore)]
        public string? Domain { get; set; }

        [JsonProperty("user_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? UserCount { get; set; }

        [JsonProperty("time_joined", NullValueHandling = NullValueHandling.Ignore)]
        public string? TimeJoined { get; set; }

        [JsonProperty("membership_expiry")]
        public object? MembershipExpiry { get; set; }

        [JsonProperty("deactivated")]
        public object? Deactivated { get; set; }

        [JsonProperty("maps", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Maps { get; set; }

        [JsonProperty("unread_messages", NullValueHandling = NullValueHandling.Ignore)]
        public long? UnreadMessages { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("street")]
        public object? Street { get; set; }

        [JsonProperty("zipcode")]
        public object? Zipcode { get; set; }

        [JsonProperty("city")]
        public object? City { get; set; }

        [JsonProperty("country_code")]
        public object? CountryCode { get; set; }
    }

    public partial class Manager
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

        [JsonProperty("socket_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? SocketId { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; }

        [JsonProperty("user_status", NullValueHandling = NullValueHandling.Ignore)]
        public UserStatus? UserStatus { get; set; }

        [JsonProperty("quota", NullValueHandling = NullValueHandling.Ignore)]
        public string? Quota { get; set; }

        [JsonProperty("email")]
        public object? Email { get; set; }

        [JsonProperty("email_validated", NullValueHandling = NullValueHandling.Ignore)]
        public string? EmailValidated { get; set; }

        [JsonProperty("notifications", NullValueHandling = NullValueHandling.Ignore)]
        public string? Notifications { get; set; }

        [JsonProperty("last_login", NullValueHandling = NullValueHandling.Ignore)]
        public string? LastLogin { get; set; }

        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string? Language { get; set; }
    }

    public partial class Settings_Company
    {
        [JsonProperty("device_pin", NullValueHandling = NullValueHandling.Ignore)]
        public string? DevicePin { get; set; }

        [JsonProperty("device_pin_delay", NullValueHandling = NullValueHandling.Ignore)]
        public string? DevicePinDelay { get; set; }

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

        [JsonProperty("client_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClientCount { get; set; }

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

        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string? Language { get; set; }

        [JsonProperty("mdm", NullValueHandling = NullValueHandling.Ignore)]
        public Mdm? Mdm { get; set; }

        [JsonProperty("membership_expired_notify_1", NullValueHandling = NullValueHandling.Ignore)]
        public string? MembershipExpiredNotify1 { get; set; }

        [JsonProperty("membership_expired_notify_2")]
        public object? MembershipExpiredNotify2 { get; set; }

        [JsonProperty("membership_expired_notify_3")]
        public object? MembershipExpiredNotify3 { get; set; }

        [JsonProperty("waiting_period_days", NullValueHandling = NullValueHandling.Ignore)]
        public long? WaitingPeriodDays { get; set; }

        [JsonProperty("ldapsync_enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LdapsyncEnabled { get; set; }

        [JsonProperty("ldapsync_usersync_only", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LdapsyncUsersyncOnly { get; set; }

        [JsonProperty("link_preview", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LinkPreview { get; set; }

        [JsonProperty("password_restrictions", NullValueHandling = NullValueHandling.Ignore)]
        public PasswordRestrictions? PasswordRestrictions { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Status { get; set; }

        [JsonProperty("company_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? CompanyId { get; set; }
    }



    public partial class Users
    {
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public long? Created { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public long? Active { get; set; }
    }
}
