namespace DotNetPivotalTrackerApi.Models.Attachments
{
    public class PivotalAttachment : PivotalModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int UploaderId { get; set; }
    }
}
