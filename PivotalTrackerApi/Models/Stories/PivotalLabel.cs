using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Models.Stories
{
    public class PivotalLabel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("project_id")]
        public int? ProjectId { get; set; }
    }
}
