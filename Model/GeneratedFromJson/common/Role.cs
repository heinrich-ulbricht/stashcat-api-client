#nullable enable
namespace StashCat.Model
{
    using Newtonsoft.Json;
    public partial class Role
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string? Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty("global", NullValueHandling = NullValueHandling.Ignore)]
        public string? Global { get; set; }

        [JsonProperty("company_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? CompanyId { get; set; }

        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string? Time { get; set; }

        [JsonProperty("editable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Editable { get; set; }
    }
  
}