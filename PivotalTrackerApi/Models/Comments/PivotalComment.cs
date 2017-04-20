using Newtonsoft.Json;
using DotNetPivotalTrackerApi.Enums;
using DotNetPivotalTrackerApi.Models.Stories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetPivotalTrackerApi.Models.Attachments;

namespace DotNetPivotalTrackerApi.Models.Comments
{
    public class PivotalComment : PivotalNewComment
    {
        [JsonProperty("file_attachment_ids")]
        public List<int> FileAttachmentIds { get; set; }
        [JsonProperty("person_id")]
        public int? PersonId { get; set; }
    }
    public class PivotalNewComment
    {
        [JsonProperty("story_id")]
        public int StoryId { get; set; }
        [JsonProperty("project_id")]
        public int ProjectId { get; set; }
        [JsonProperty("file_attachments")]
        public List<PivotalAttachment> FileAttachments { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }        
    }

}
