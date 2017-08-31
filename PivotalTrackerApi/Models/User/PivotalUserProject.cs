namespace DotNetPivotalTrackerApi.Models.User
{
    public class PivotalUserProject : PivotalModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Role { get; set; }
        public string ProjectColor { get; set; }
    }
}
