#nullable enable
namespace StashCat.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class StashCatTopLevelResponse
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public Status? Status { get; set; }

        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public Payload? Payload { get; set; }

        [JsonProperty("signature", NullValueHandling = NullValueHandling.Ignore)]
        public string? Signature { get; set; }
    }

    public partial class Payload
    {
        [JsonProperty("client_key", NullValueHandling = NullValueHandling.Ignore)]
        public string? ClientKey { get; set; }

        [JsonProperty("userinfo", NullValueHandling = NullValueHandling.Ignore)]
        public Userinfo? Userinfo { get; set; }
        [JsonProperty("keys", NullValueHandling = NullValueHandling.Ignore)]
        public Keys? Keys { get; set; }
        [JsonProperty("conversations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Conversation>? Conversations { get; set; }
        [JsonProperty("companies")]
        public List<Company>? Companies { get; set; }        
        [JsonProperty("channels", NullValueHandling = NullValueHandling.Ignore)]
        public List<Channel>? Channels { get; set; }

    }

    public partial class Status
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string? Value { get; set; }

        [JsonProperty("short_message", NullValueHandling = NullValueHandling.Ignore)]
        public string? ShortMessage { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }
    }
}
