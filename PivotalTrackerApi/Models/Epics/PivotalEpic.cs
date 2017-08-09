using DotNetPivotalTrackerApi.Models.Comments;
using DotNetPivotalTrackerApi.Models.Stories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Models.Epics
{
    public class PivotalEpic
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public PivotalLabel Label { get; set; }
        public string Description { get; set; }
        public List<PivotalComment> Comments { get; set; }
        public int AfterId { get; set; }
        public int BeforeId { get; set; }
    }
}
