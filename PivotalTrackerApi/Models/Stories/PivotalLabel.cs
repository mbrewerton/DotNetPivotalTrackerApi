namespace DotNetPivotalTrackerApi.Models.Stories
{
    public class PivotalLabel : PivotalModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ProjectId { get; set; }
    }
}
