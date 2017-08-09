using Newtonsoft.Json;

namespace DotNetPivotalTrackerApi.Portable.Models.Stories
{
    public class PivotalLabel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("project_id")]
        public int? ProjectId { get; set; }
    }
}
