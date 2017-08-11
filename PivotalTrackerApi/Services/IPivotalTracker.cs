using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetPivotalTrackerApi.Enums;
using DotNetPivotalTrackerApi.Exceptions;
using DotNetPivotalTrackerApi.Models.Comments;
using DotNetPivotalTrackerApi.Models.Project;
using DotNetPivotalTrackerApi.Models.Stories;
using DotNetPivotalTrackerApi.Models.Tasks;
using DotNetPivotalTrackerApi.Models.User;

namespace DotNetPivotalTrackerApi.Services
{
    public interface IPivotalTracker
    {
        /// <summary>
        /// Sets the persisted Project Id for the Pivotal Tracker instance. Setting this to null will make methods throw a <see cref="PivotalMethodNotValidException"/> 
        /// if you do not pass a projectId to the method.
        /// </summary>
        /// <param name="projectId">Project id to persist for the PivotalTracker instance.</param>
        void SetProjectId(int? projectId);

        /// <summary>
        /// Gets the user details for the current Api Token.
        /// </summary>
        /// <returns>Returns a PivotalUser.</returns>
        PivotalUser GetUser();

        /// <summary>
        /// Gets all projects as a List&lt;PivotalProject&gt; that the current user is assigned to.
        /// </summary>
        /// <returns>Returns List&lt;PivotalProject&gt; containing all projects user is assigned to.</returns>
        List<PivotalProject> GetProjects();

        /// <summary>
        /// Gets the current project for the PivotalTracker instance. If a <paramref name="projectId"/> is passed to the method, it will use that to get the project, otherwise it looks to the persisted <see cref="PivotalTracker._projectId"/>.
        /// Throws <see cref="NullReferenceException"/> if both are null.
        /// </summary>
        /// <param name="projectId">(optional) Id of the project (default: null)</param>
        /// <returns>Returns a PivotalProject by Id.</returns>
        PivotalProject GetCurrentProject(int? projectId = null);

        /// <summary>
        /// Gets all stories for a given project by <paramref name="projectId"/>.
        /// </summary>
        /// <param name="projectId">Id of the project to get stories for.</param>
        /// <returns>Returns projects as a List&lt;PivotalStory&gt;.</returns>
        List<PivotalStory> GetProjectStories(int? projectId = null);

        /// <summary>
        /// Gets a story within a project by Id.
        /// </summary>
        /// <param name="projectId">Id of the project to get the story from.</param>
        /// <param name="storyId">Id of the story you want to return.</param>
        /// <returns></returns>
        PivotalStory GetStoryById(int? projectId, int storyId);

        /// <summary>
        /// Deletes a story from a project. This is irreversible. Use with caution.
        /// </summary>
        /// <param name="projectId">Id of the project to delete the story from.</param>
        /// <param name="storyId">Id of the story to delete.</param>
        /// <returns>Boolean</returns>
        bool DeleteStory(int? projectId, int storyId);

        /// <summary>
        /// Creates a new story in a project.
        /// </summary>
        /// <param name="projectId">Id of the project to create a story in.</param>
        /// <param name="name">The name you wish to give to the story.</param>
        /// <param name="storyType">Type of the story, eg feature, bug.</param>
        /// <param name="labels">Any labels you wish to give to the story.</param>
        /// <param name="description">(optional) Description of the story. Use this for additional info.</param>
        /// <returns>Returns a completed PivotalStory</returns>
        PivotalStory CreateNewStory(int? projectId, string name, StoryType storyType, List<string> labels = null, string description = null);

        /// <summary>
        /// Creates a new story in a project.
        /// </summary>
        /// <param name="projectId">Id of the project to create a story in.</param>
        /// <param name="pivotalStory">Pre-created story to create in the project.</param>
        /// <returns>Returns a completed PivotalStory.</returns>
        PivotalStory CreateNewStory(int? projectId, PivotalNewStory pivotalStory);

        /// <summary>
        /// Gets all tasks for a project story.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <returns>Returns a List&lt;PivotalTask&gt;.</returns>
        List<PivotalTask> GetTasksFromStory(int? projectId, int storyId);

        /// <summary>
        /// Creates a new tasks on a project story.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="pivotalTask">The predefined PivotalNewTask to create.</param>
        /// <returns>Returns a PivotalTask.</returns>
        PivotalTask CreateNewStoryTask(int? projectId, int storyId, PivotalNewTask pivotalTask);

        /// <summary>
        /// Creates a new task on a story.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="description">Description of the task.</param>
        /// <param name="complete">(optional) Determines whether or not the task is marked as "complete". (default: false)</param>
        /// <param name="position">(optional) Sets the position of the task on the story. If null, the task will be placed at the end of the list. (default: null)</param>
        /// <returns>Returns a PivotalTask.</returns>
        PivotalTask CreateNewStoryTask(int? projectId, int storyId, string description, bool complete = false, int? position = null);

        /// <summary>
        /// Update a task on a story. Uses the <paramref name="pivotalTask"/> with updated data to update the task. It will use the Id property on the model to determine which task should be updated.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="pivotalTask">The task model to update,</param>
        /// <returns>Returns a PivotalTask.</returns>
        PivotalTask UpdateStoryTask(int? projectId, int storyId, PivotalTask pivotalTask);

        /// <summary>
        /// Deletes a task from a story. Will return true if deletion was successful.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="taskId">Id of the task to delete.</param>
        /// <returns>Returns a Boolean (true) if successful.</returns>
        bool DeleteStoryTask(int? projectId, int storyId, int taskId);

        /// <summary>
        /// Gets all comments for a project story. You can also request any attachments (default: false).
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="includeAttachments">(optional) Defines whether we return attachments with the comments. Default: False</param>
        /// <returns>Returns a List&lt;PivotalComment&gt;. If you set <paramref name="includeAttachments"/> to true, also returns attachments</returns>
        List<PivotalComment> GetComments(int? projectId, int storyId, bool includeAttachments = false);

        /// <summary>
        /// Creates a new comment on a story with from a pre-defined <see cref="PivotalStory"/>
        /// </summary>
        /// <param name="pivotalStory">The pre-defined <see cref="PivotalStory"/> to add the comment to.</param>
        /// <param name="bodyText">The main descrtiption text of the comment.</param>
        /// <param name="fileData">(optional) File data you want to add to the comment as an attachment as Stream.</param>
        /// <returns></returns>
        PivotalComment CreateNewComment(PivotalStory pivotalStory, string bodyText);

        /// <summary>
        /// Creates a new comment on a story with a pre-defined <see cref="PivotalNewComment"/>.
        /// </summary>
        /// <remarks>
        /// Note: <paramref name="pivotalComment"/> must include ProjectId and StoryId.
        /// </remarks>
        /// <param name="pivotalComment">The pre-defined PivotalNewComment to create.</param>
        /// <returns></returns>
        PivotalComment CreateNewComment(PivotalNewComment pivotalComment);

        /// <summary>
        /// Creates a new <see cref="PivotalComment"/> and adds to to a story.
        /// </summary>
        /// <param name="projectId">Id of the project to work with.</param>
        /// <param name="storyId">Id of the story to add the comment to.</param>
        /// <param name="bodyText">The main description text of the comment.</param>
        /// <returns></returns>
        PivotalComment CreateNewComment(int? projectId, int storyId, string bodyText);

        /// <summary>
        /// Creates a new <see cref="PivotalComment"/> and adds to to a story.
        /// </summary>
        /// <param name="projectId">Id of the project to work with.</param>
        /// <param name="storyId">Id of the story to add the comment to.</param>
        /// <param name="bodyText">The main description text of the comment.</param>
        /// <param name="fileData">(optional) File data you want to add to the comment as an attachment as Stream.</param>
        /// <returns></returns>
        PivotalComment CreateNewComment(int? projectId, int storyId, string bodyText, Stream fileData);

        PivotalComment UpdateComment(int? projectId, int storyId, PivotalComment comment);

        /// <summary>
        /// Checks if both the <param name="projectId"></param> parameter and <see cref="PivotalTracker._projectId"/> are null.
        /// Returns boolean, or throws <exception cref="NullReferenceException"></exception> if <param name="throwException"></param> is true.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="throwException">(optional) Should the method throw a <exception cref="NullReferenceException"></exception> on null check failure. (default: true)</param>
        /// <returns></returns>
        bool CheckProjectIds(int? projectId, bool throwException = true);

        /// <summary>
        /// Returns the proper Project Id to use. Prioritises the Id passed into the method over the persisted Id allowing you to override for one call.
        /// </summary>
        /// <param name="projectId">Id of the project via method call. Leave as null to use persisted Project Id.</param>
        /// <returns>Returns the proper Project Id as an int value.</returns>
        int GetProjectIdToUse(int? projectId);
    }
}
