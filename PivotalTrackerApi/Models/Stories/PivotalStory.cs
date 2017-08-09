using Newtonsoft.Json;
using DotNetPivotalTrackerApi.Models.Stories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetPivotalTrackerApi.Enums;

namespace DotNetPivotalTrackerApi.Models.Stories
{
    public class PivotalStory
    {
        [JsonProperty("project_id")]
        public int ProjectId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonProperty("story_type")]
        public StoryType StoryType { get; set; }
        [JsonProperty("story_state")]
        public StoryState StoryState { get; set; }

        public float Estimate { get; set; }

        public DateTime Deadline { get; set; }

        [JsonProperty("requested_by_id")]
        public int RequestedById { get; set; }
        [JsonProperty("owner_ids")]
        public List<int> OwnerIds { get; set; }

        public List<PivotalLabel> Labels { get; set; }
    }
}
