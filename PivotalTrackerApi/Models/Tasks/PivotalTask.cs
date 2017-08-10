using System;

namespace DotNetPivotalTrackerApi.Models.Tasks
{
    public class PivotalTask
    {
        public int? TaskId { get; set; }
        public int Id { get; set; }
        public int StoryId { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public int Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        /*
         id int 
 —  Database id of the task. This field is read only. This field is always returned.
 
story_id int 
 —  The id of the story to which the task is connected. This field is read only.
 
description string[1000] 
Required On Create  —  Content of the task. This field is required on create.
 
complete boolean 
 —  Flag showing the completion of the task.
 
position int 
 —  Offset from the top of the task list. Positions start counting from 1 for the first task on a story.
 
created_at datetime 
 —  Creation time. This field is read only.
 
updated_at datetime 
 —  Time of last update. This field is read only.
 
kind string 
 —  The type of this object: task. This field is read only.
         */
    }
}
