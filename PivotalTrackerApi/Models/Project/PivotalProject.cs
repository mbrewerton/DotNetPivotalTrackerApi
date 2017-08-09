using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Models.Project
{
    public class PivotalProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Public { get; set; }
    }
}
