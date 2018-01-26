using System;
using System.Collections.Generic;
using DotNetPivotalTrackerApi.Enums;

namespace DotNetPivotalTrackerApi.Models.Stories
{
    public class PivotalStory : PivotalModel
    {
        public int ProjectId { get; set; }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StoryType { get; set; }
        public string StoryState { get; set; }
        public float? Estimate { get; set; }
        //public DateTime? Deadline { get; set; } = DateTime.Now;
        public int? RequestedById { get; set; }
        public List<int> OwnerIds { get; set; }
        public List<PivotalLabel> Labels { get; set; }
    }
}
