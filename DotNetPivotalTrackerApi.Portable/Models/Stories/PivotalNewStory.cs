using System.Collections.Generic;

namespace DotNetPivotalTrackerApi.Portable.Models.Stories
{
    public class PivotalNewStory
    {
        public PivotalNewStory()
        {
            //OwnerIds = new List<int>();
            Labels = new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string StoryType { get; set; }

        //public float? Estimate { get; set; }

        //public DateTime? Deadline { get; set; }

        //public List<int> OwnerIds { get; set; }

        public List<string> Labels { get; set; }
    }
}
