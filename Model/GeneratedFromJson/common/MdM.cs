#nullable enable
using Newtonsoft.Json;

namespace StashCat.Model
{
    public partial class Mdm
    {
        [JsonProperty("mdm_access_calendar", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAccessCalendar { get; set; }

        [JsonProperty("mdm_access_camera", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAccessCamera { get; set; }

        [JsonProperty("mdm_access_gps", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAccessGps { get; set; }

        [JsonProperty("mdm_access_microphone", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAccessMicrophone { get; set; }

        [JsonProperty("mdm_access_storage_pictures", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAccessStoragePictures { get; set; }

        [JsonProperty("mdm_access_storage_videos", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAccessStorageVideos { get; set; }

        [JsonProperty("mdm_ability_copypaste", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAbilityCopypaste { get; set; }

        [JsonProperty("mdm_ability_sharing", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAbilitySharing { get; set; }

        [JsonProperty("mdm_ability_chat_history", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAbilityChatHistory { get; set; }

        [JsonProperty("mdm_access_attachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MdmAccessAttachments { get; set; }
    }
}