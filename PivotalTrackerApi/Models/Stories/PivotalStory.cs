using System;
using System.Collections.Generic;
using DotNetPivotalTrackerApi.Enums;

namespace DotNetPivotalTrackerApi.Models.Stories
{
    public class PivotalStory
    {
        public int ProjectId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StoryType StoryType { get; set; }
        public StoryState StoryState { get; set; }
        public float Estimate { get; set; }
        public DateTime Deadline { get; set; }
        public int RequestedById { get; set; }
        public List<int> OwnerIds { get; set; }
        public List<PivotalLabel> Labels { get; set; }
    }
}
