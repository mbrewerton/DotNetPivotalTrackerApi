using System;

namespace DotNetPivotalTrackerApi.Models.Tasks
{
    public class PivotalTask : PivotalModel
    {
        public int? TaskId { get; set; }
        public int Id { get; set; }
        public int StoryId { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public int Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
