using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi
{
    public class PivotalTracker
    {
        public API.Shared.Services.PivotalTracker Tracker { get; set; }

        public PivotalTracker(string apiToken, int? projectId = null)
        {
            Tracker = new API.Shared.Services.PivotalTracker(apiToken, projectId);
        }
    }
}
