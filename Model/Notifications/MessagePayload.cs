namespace StashCat.Notifications.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class MessagePayload
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("conversation_id")]
        public long ConversationId { get; set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; set; }

        [JsonProperty("thread_id")]
        public long ThreadId { get; set; }

        [JsonProperty("hash")]
        public string? Hash { get; set; }

        [JsonProperty("verification")]
        public string? Verification { get; set; }

        [JsonProperty("broadcast")]
        public object? Broadcast { get; set; }

        [JsonProperty("alarm")]
        public bool Alarm { get; set; }

        [JsonProperty("confirmation_required")]
        public bool ConfirmationRequired { get; set; }

        [JsonProperty("confirmations")]
        public List<object>? Confirmations { get; set; }

        [JsonProperty("time")]
        public string? Time { get; set; }

        [JsonProperty("micro_time")]
        public string? MicroTime { get; set; }

        [JsonProperty("sender")]
        public Sender? Sender { get; set; }

        [JsonProperty("device")]
        public string? Device { get; set; }

        [JsonProperty("device_id")]
        public string? DeviceId { get; set; }

        [JsonProperty("deleted")]
        public object? Deleted { get; set; }

        [JsonProperty("kind")]
        public string? Kind { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("location")]
        public Location? Location { get; set; }

        [JsonProperty("is_forwarded")]
        public bool IsForwarded { get; set; }

        [JsonProperty("metainfo")]
        public object? Metainfo { get; set; }

        [JsonProperty("reciever")]
        public List<object>? Reciever { get; set; }

        [JsonProperty("files")]
        public List<File>? Files { get; set; }

        [JsonProperty("likes")]
        public long Likes { get; set; }

        [JsonProperty("liked")]
        public bool Liked { get; set; }

        [JsonProperty("flagged")]
        public bool Flagged { get; set; }

        [JsonProperty("tags")]
        public List<object>? Tags { get; set; }

        [JsonProperty("links")]
        public List<object>? Links { get; set; }

        [JsonProperty("seen")]
        public List<Seen>? Seen { get; set; }

        [JsonProperty("unread")]
        public bool Unread { get; set; }

        [JsonProperty("encrypted")]
        public bool Encrypted { get; set; }

        [JsonProperty("iv")]
        public string? Iv { get; set; }

        [JsonProperty("reply_to")]
        public object? ReplyTo { get; set; }
    }

    public partial class File
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("virtual_folder")]
        public object? VirtualFolder { get; set; }

        [JsonProperty("size")]
        public string? Size { get; set; }

        [JsonProperty("size_byte")]
        public string? SizeByte { get; set; }

        [JsonProperty("size_string")]
        public string? SizeString { get; set; }

        [JsonProperty("folder_type")]
        public string? FolderType { get; set; }

        [JsonProperty("type_id")]
        public string? TypeId { get; set; }

        [JsonProperty("dimensions")]
        public Dimensions? Dimensions { get; set; }

        [JsonProperty("ext")]
        public string? Ext { get; set; }

        [JsonProperty("mime")]
        public string? Mime { get; set; }

        [JsonProperty("base_64")]
        public object? Base64 { get; set; }

        [JsonProperty("uploaded")]
        public string? Uploaded { get; set; }

        [JsonProperty("modified")]
        public string? Modified { get; set; }

        [JsonProperty("permission")]
        public string? Permission { get; set; }

        [JsonProperty("owner_id")]
        public string? OwnerId { get; set; }

        [JsonProperty("owner")]
        public List<object>? Owner { get; set; }

        [JsonProperty("last_download")]
        public string? LastDownload { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("times_downloaded")]
        public string? TimesDownloaded { get; set; }

        [JsonProperty("deleted")]
        public object? Deleted { get; set; }

        [JsonProperty("encrypted")]
        public bool Encrypted { get; set; }

        [JsonProperty("e2e_iv")]
        public object? E2EIv { get; set; }

        [JsonProperty("md5")]
        public string? Md5 { get; set; }
    }

    public partial class Dimensions
    {
        [JsonProperty("width")]
        public object? Width { get; set; }

        [JsonProperty("height")]
        public object? Height { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("longitude")]
        public object? Longitude { get; set; }

        [JsonProperty("latitude")]
        public object? Latitude { get; set; }

        [JsonProperty("encrypted")]
        public bool Encrypted { get; set; }

        [JsonProperty("iv")]
        public string? Iv { get; set; }
    }

    public partial class Seen
    {
        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        [JsonProperty("first_name")]
        public string? FirstName { get; set; }

        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }

    public partial class Sender
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("first_name")]
        public string? FirstName { get; set; }

        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonProperty("image")]
        public string? Image { get; set; }

        [JsonProperty("active")]
        public string? Active { get; set; }

        [JsonProperty("deleted")]
        public object? Deleted { get; set; }

        [JsonProperty("allows_voip_calls")]
        public bool AllowsVoipCalls { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("public_key")]
        public string? PublicKey { get; set; }

        [JsonProperty("language")]
        public string? Language { get; set; }
    }
}
