using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Models.Stories
{
    public class PivotalSearchStory
    {
        public IEnumerable<PivotalStory> Stories { get; set; }
        public int TotalHits { get; set; }
        public int TotalHitsWithDone { get; set; }
        public int TotalPoints { get; set; }
        public int TotalpointsCompleted{ get; set; }
    }
}
