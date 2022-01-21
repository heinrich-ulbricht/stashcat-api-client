#nullable enable
using Newtonsoft.Json;

namespace StashCat.Model
{
    public partial class PasswordRestrictions
    {
        [JsonProperty("pw_restrictions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PwRestrictions { get; set; }

        [JsonProperty("pw_min_length", NullValueHandling = NullValueHandling.Ignore)]
        public long? PwMinLength { get; set; }

        [JsonProperty("pw_uppercase_lowercase", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PwUppercaseLowercase { get; set; }

        [JsonProperty("pw_specialchars", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PwSpecialchars { get; set; }

        [JsonProperty("pw_numbers", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PwNumbers { get; set; }
    }
}