namespace DotNetPivotalTrackerApi.Models.Project
{
    public class PivotalProject : PivotalModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Public { get; set; }
    }
}
