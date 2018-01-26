using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Models.Stories
{
    public class PivotalSearchModel
    {
        public PivotalSearchStory Stories { get; set; }
        public string Query { get; set; }
    }
}
