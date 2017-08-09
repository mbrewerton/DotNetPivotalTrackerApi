using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Models.User
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
