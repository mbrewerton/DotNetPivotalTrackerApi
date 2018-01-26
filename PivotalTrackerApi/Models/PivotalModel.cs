using System;

namespace DotNetPivotalTrackerApi.Models
{
    public class PivotalModel
    {
        //public int Id { get; set; }
        public string Kind { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
