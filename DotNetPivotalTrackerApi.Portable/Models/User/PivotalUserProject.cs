using Newtonsoft.Json;

namespace DotNetPivotalTrackerApi.Portable.Models.User
{
    public class PivotalUserProject
    {
        [JsonProperty("project_id")]
        public int ProjectId { get; set; }
        [JsonProperty("project_name")]
        public string ProjectName { get; set; }
        public string Role { get; set; }
        [JsonProperty("project_color")]
        public string ProjectColor { get; set; }
    }
}
