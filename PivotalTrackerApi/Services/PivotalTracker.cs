using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DotNetPivotalTrackerApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DotNetPivotalTrackerApi.Enums;
using DotNetPivotalTrackerApi.Models.Stories;
using System.IO;
using DotNetPivotalTrackerApi.Models.Attachments;
using DotNetPivotalTrackerApi.Models.Comments;
using DotNetPivotalTrackerApi.Models.Project;
using DotNetPivotalTrackerApi.Models.User;
using DotNetPivotalTrackerApi.Utils;
using DotNetPivotalTrackerApi.Models.Tasks;

namespace DotNetPivotalTrackerApi.Services
{
    public class PivotalTracker
    {

        private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        private readonly string _apiToken;
        public string ApiToken => _apiToken;

        /// <summary>
        /// Instantiates a new PivotalTracker instance using the specified apiToken (visit <see cref="https://www.pivotaltracker.com/profile"/> to generate a token).
        /// </summary>
        /// <param name="apiToken">Your Api Token.</param>
        public PivotalTracker(string apiToken)
        {
            _apiToken = apiToken;
            // Sets up up our HttpService to make sure it is ready to use
            HttpService.SetupHttpClient(_apiToken);
        }

        #region User
        /// <summary>
        /// Gets the user details for the current Api Token.
        /// </summary>
        /// <returns>Returns a PivotalUser.</returns>
        public PivotalUser GetUser()
        {
            // Gets current user data for the user of the current Api token
            var response = HttpService.GetAsync("me").Result;

            return HandleResponse<PivotalUser>(response);
        }
        #endregion

        #region Projects
        /// <summary>
        /// Gets all projects as a List&lt;PivotalProject&gt; that the current user is assigned to.
        /// </summary>
        /// <returns>Returns List&lt;PivotalProject&gt; containing all projects user is assigned to.</returns>
        public List<PivotalProject> GetProjects()
        {
            var response = HttpService.GetAsync("projects").Result;

            return HandleResponse<List<PivotalProject>>(response);
        }

        /// <summary>
        /// Gets a project by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the project.</param>
        /// <returns>Returns a PivotalProject by <paramref name="id"/></returns>
        public PivotalProject GetProjectById(int id)
        {
            var response = HttpService.GetAsync($"projects/{id}").Result;

            return HandleResponse<PivotalProject>(response);
        }
        #endregion

        #region Project Stories
        /// <summary>
        /// Gets all stories for a given project by <paramref name="projectId"/>.
        /// </summary>
        /// <param name="projectId">Id of the project to get stories for.</param>
        /// <returns>Returns projects as a List&lt;PivotalStory&gt;.</returns>
        public List<PivotalStory> GetProjectStories(int projectId)
        {
            var response = HttpService.GetAsync(StringUtil.PivotalStoriesUrl(projectId)).Result;

            return HandleResponse<List<PivotalStory>>(response);
        }

        /// <summary>
        /// Gets a story within a project by Id.
        /// </summary>
        /// <param name="projectId">Id of the project to get the story from.</param>
        /// <param name="storyId">Id of the story you want to return.</param>
        /// <returns></returns>
        public PivotalStory GetStoryById(int projectId, int storyId)
        {
            var response = HttpService.GetAsync(StringUtil.PivotalStoriesUrl(projectId, storyId)).Result;

            return HandleResponse<PivotalStory>(response);
        }

        /// <summary>
        /// Deletes a story from a project. This is irreversible. Use with caution.
        /// </summary>
        /// <param name="projectId">Id of the project to delete the story from.</param>
        /// <param name="storyId">Id of the story to delete.</param>
        /// <returns>Boolean</returns>
        public bool DeleteStory(int projectId, int storyId)
        {
            var response = HttpService.DeleteAsync(StringUtil.PivotalStoriesUrl(projectId, storyId)).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw ThrowException(response);
        }

        /// <summary>
        /// Creates a new story in a project.
        /// </summary>
        /// <param name="projectId">Id of the project to create a story in.</param>
        /// <param name="name">The name you wish to give to the story.</param>
        /// <param name="storyType">Type of the story, eg feature, bug.</param>
        /// <param name="labels">Any labels you wish to give to the story.</param>
        /// <param name="description">(optional) Description of the story. Use this for additional info.</param>
        /// <returns>Returns a completed PivotalStory</returns>
        public PivotalStory CreateNewStory(int projectId, string name, StoryType storyType, List<string> labels = null, string description = null)
        {
            var story = new PivotalNewStory
            {
                Name = name,
                Description = description,
                Labels = labels ?? new List<string>(),
                StoryType = storyType.ToString()
            };
            var response = HttpService.PostAsync(StringUtil.PivotalStoriesUrl(projectId), story).Result;

            return HandleResponse<PivotalStory>(response);
        }

        /// <summary>
        /// Creates a new story in a project.
        /// </summary>
        /// <param name="projectId">Id of the project to create a story in.</param>
        /// <param name="pivotalStory">Pre-created story to create in the project.</param>
        /// <returns>Returns a completed PivotalStory.</returns>
        public PivotalStory CreateNewStory(int projectId, PivotalNewStory pivotalStory)
        {
            // Try and create a new PivotalStory
            var response = HttpService.PostAsync(StringUtil.PivotalStoriesUrl(projectId), pivotalStory).Result;

            return HandleResponse<PivotalStory>(response);
        }
        #endregion

        #region Story Tasks
        /// <summary>
        /// Gets all tasks for a project story.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <returns>Returns a List&lt;PivotalTask&gt;.</returns>
        public List<PivotalTask> GetTasksFromStory(int projectId, int storyId)
        {
            var response = HttpService.GetAsync(StringUtil.PivotalStoryTasksUrl(projectId, storyId)).Result;

            return HandleResponse<List<PivotalTask>>(response);
        }

        /// <summary>
        /// Creates a new tasks on a project story.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="pivotalTask">The predefined PivotalNewTask to create.</param>
        /// <returns>Returns a PivotalTask.</returns>
        public PivotalTask CreateNewStoryTask(int projectId, int storyId, PivotalNewTask pivotalTask)
        {
            var response = HttpService.PostAsync(StringUtil.PivotalStoryTasksUrl(projectId, storyId), pivotalTask).Result;

            return HandleResponse<PivotalTask>(response);
        }

        /// <summary>
        /// Creates a new task on a story.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="description">Description of the task.</param>
        /// <param name="complete">(optional) Determines whether or not the task is marked as "complete". (default: false)</param>
        /// <param name="position">(optional) Sets the position of the task on the story. If null, the task will be placed at the end of the list. (default: null)</param>
        /// <returns>Returns a PivotalTask.</returns>
        public PivotalTask CreateNewStoryTask(int projectId, int storyId, string description, bool complete = false, int? position = null)
        {
            var pivotalTask = new PivotalNewTask
            {
                Description = description,
                Complete = complete,
                Position = position
            };
            var response = HttpService.PostAsync(StringUtil.PivotalStoryTasksUrl(projectId, storyId), pivotalTask).Result;

            return HandleResponse<PivotalTask>(response);
        }

        /// <summary>
        /// Update a task on a story. Uses the <paramref name="pivotalTask"/> with updated data to update the task. It will use the Id property on the model to determine which task should be updated.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="pivotalTask">The task model to update,</param>
        /// <returns>Returns a PivotalTask.</returns>
        public PivotalTask UpdateStoryTask(int projectId, int storyId, PivotalTask pivotalTask)
        {
            var response = HttpService.PutAsync(StringUtil.PivotalStoryTasksUrl(projectId, storyId, pivotalTask.Id), pivotalTask).Result;

            return HandleResponse<PivotalTask>(response);
        }

        /// <summary>
        /// Deletes a task from a story. Will return true if deletion was successful.
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="taskId">Id of the task to delete.</param>
        /// <returns>Returns a Boolean (true) if successful.</returns>
        public bool DeleteStoryTask(int projectId, int storyId, int taskId)
        {
            var response = HttpService.DeleteAsync(StringUtil.PivotalStoryTasksUrl(projectId, storyId, taskId)).Result;
            return HandleResponseBoolean(response);
        }
        #endregion

        #region Comments
        /// <summary>
        /// Gets all comments for a project story. You can also request any attachments (default: false).
        /// </summary>
        /// <param name="projectId">Id of the project.</param>
        /// <param name="storyId">Id of the story.</param>
        /// <param name="includeAttachments">(optional) Defines whether we return attachments with the comments. Default: False</param>
        /// <returns>Returns a List&lt;PivotalComment&gt;. If you set <paramref name="includeAttachments"/> to true, also returns attachments</returns>
        public List<PivotalComment> GetComments(int projectId, int storyId, bool includeAttachments = false)
        {
            // TODO: Implement include attachments.
            // Try and get all comments for a specific story
            var response = HttpService.GetAsync(StringUtil.PivotalCommentsUrl(projectId, storyId)).Result;

            return HandleResponse<List<PivotalComment>>(response);
        }

        /// <summary>
        /// Creates a new comment on a story with from a pre-defined <see cref="PivotalStory"/>
        /// </summary>
        /// <param name="pivotalStory">The pre-defined <see cref="PivotalStory"/> to add the comment to.</param>
        /// <param name="bodyText">The main descrtiption text of the comment.</param>
        /// <param name="fileData">(optional) File data you want to add to the comment as an attachment as Stream.</param>
        /// <returns></returns>
        public PivotalComment CreateNewComment(PivotalStory pivotalStory, string bodyText)
        {
            // Attempts to get the story for our pivotalStory object
            var storyFromPivotal = GetStoryById(pivotalStory.ProjectId, pivotalStory.Id);

            // Initialise a new PivotalComment
            PivotalComment comment;

            // Make sure our story actually exists, throws exception if false.
            if (storyFromPivotal != null)
            {
                // Setup a new comment to send to Pivotal
                var newComment = new PivotalNewComment
                {
                    ProjectId = storyFromPivotal.ProjectId,
                    StoryId = storyFromPivotal.Id,
                    Text = bodyText,
                    FileAttachments = new List<PivotalAttachment>()
                };

                // Try and add the comment to our story
                var response =
                    HttpService.PostAsync(
                        StringUtil.PivotalCommentsUrl(storyFromPivotal.ProjectId, storyFromPivotal.Id), newComment)
                        .Result;

                return HandleResponse<PivotalComment>(response);
            }

            // Failure, our story does not exist on this project.
            throw new PivotalNotFoundException($"Story with Id {pivotalStory.Id} does not exist on project with Id {pivotalStory.ProjectId}");
        }

        /// <summary>
        /// Creates a new comment on a story with a pre-defined <see cref="PivotalNewComment"/>.
        /// </summary>
        /// <remarks>
        /// Note: <paramref name="pivotalComment"/> must include ProjectId and StoryId.
        /// </remarks>
        /// <param name="pivotalComment">The pre-defined PivotalNewComment to create.</param>
        /// <returns></returns>
        public PivotalComment CreateNewComment(PivotalNewComment pivotalComment)
        {
            // Try and send our whole comment object to Pivotal
            var response = HttpService.PostAsync(StringUtil.PivotalCommentsUrl(pivotalComment.ProjectId, pivotalComment.StoryId), pivotalComment).Result;

            return HandleResponse<PivotalComment>(response);
        }

        /// <summary>
        /// Creates a new <see cref="PivotalComment"/> and adds to to a story.
        /// </summary>
        /// <param name="projectId">Id of the project to work with.</param>
        /// <param name="storyId">Id of the story to add the comment to.</param>
        /// <param name="bodyText">The main description text of the comment.</param>
        /// <returns></returns>
        public PivotalComment CreateNewComment(int projectId, int storyId, string bodyText)
        {
            // TODO: Implement include attachments.
            var pivotalComment = new PivotalNewComment
            {
                ProjectId = projectId,
                StoryId = storyId,
                Text = bodyText,
                FileAttachments = new List<PivotalAttachment>()
            };

            // Try and send our new comment to Pivotal
            var response = HttpService.PostAsync(StringUtil.PivotalCommentsUrl(projectId, storyId), pivotalComment).Result;

            return HandleResponse<PivotalComment>(response);
        }

        /// <summary>
        /// Creates a new <see cref="PivotalComment"/> and adds to to a story.
        /// </summary>
        /// <param name="projectId">Id of the project to work with.</param>
        /// <param name="storyId">Id of the story to add the comment to.</param>
        /// <param name="bodyText">The main description text of the comment.</param>
        /// <param name="fileData">(optional) File data you want to add to the comment as an attachment as Stream.</param>
        /// <returns></returns>
        public PivotalComment CreateNewComment(int projectId, int storyId, string bodyText, Stream fileData)
        {
            // Create a new comment on our story before we go any further
            var pivotalComment = CreateNewComment(projectId, storyId, bodyText);

            using (var ms = new MemoryStream())
            {
                // Setup a new HTTP form to post to the server with our attachment(s)
                var form = new MultipartFormDataContent();
                // Copy our fileData to our MemoryStream to create a Byte array
                fileData.CopyTo(ms);
                // Try and cast our fileData as a FileStream so that we can access the real file name.
                FileStream fileStream = (FileStream)fileData;
                // Create a new byte array from our MemoryStream with our fileData in
                byte[] data = ms.ToArray();

                // Create a ByteArrayContent from our byte array
                var content = new ByteArrayContent(data);
                // Setup Headers of our content
                content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = fileStream.Name
                };

                // Make sure we dispose of our FileStream as we no longer need it
                fileStream.Dispose();

                // Add our finalised ByteArrayContent to our HTTP form ready to post
                form.Add(content);

                // Try and send our form to Pivotal
                var uploadResponse = HttpService.PostContentAsync(StringUtil.PivotalUploadsUrl(projectId), form).Result;

                if (uploadResponse.IsSuccessStatusCode)
                {
                    // Success, our file was uploaded to Pivotal, we need to serialise it so that we can construct a new comment with the attachment
                    var uploadedFile = Serialize<PivotalAttachment>(uploadResponse);
                    // Create a new List<PivotalAttachment> to add to our comment to handle multiple attachments (TODO)
                    var attachments = new List<PivotalAttachment> { uploadedFile };
                    // Create our fully constructed comment to send to Pivotal
                    var newComment = new PivotalNewComment()
                    {
                        ProjectId = projectId,
                        StoryId = storyId,
                        Text = pivotalComment.Text,
                        FileAttachments = attachments
                    };

                    // Try and send our comment to Pivotal
                    var commentResponse = HttpService.PostAsync($"projects/{projectId}/stories/{storyId}/comments", newComment).Result;
                    if (commentResponse.IsSuccessStatusCode)
                    {
                        // Serialise response to object
                        var serializedComment = Serialize<PivotalComment>(commentResponse);
                        // Add our attachments as the response from Pivotal doesn't return them
                        serializedComment.FileAttachments = attachments;
                        // Success, return serialised response
                        return serializedComment;
                    }

                    // Failure, throw generic exception with response
                    throw ThrowException(commentResponse);
                }

                // Failure, throw generic exception with response
                throw ThrowException(uploadResponse);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks the IsSuccessStatusCode property of the <paramref name="responseMessage"/> and serialises the response. Throws <see cref="PivotalHttpException"/> if false.
        /// </summary>
        /// <typeparam name="T">Type to serialise the JSON response to.</typeparam>
        /// <param name="responseMessage">The response from the Pivotal Tracker API.</param>
        /// <returns>JSON serialised as object using <typeparamref name="T"/>.</returns>
        private T HandleResponse<T>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                // Success, return serialised response
                return Serialize<T>(responseMessage);
            }

            // Failure, throw generic exception with response
            throw ThrowException(responseMessage);
        }
        /// <summary>
        /// Checks the IsSuccessStatusCode property of the <paramref name="responseMessage"/> returns true if successful. Throws <see cref="PivotalHttpException"/> if false.
        /// </summary>
        /// <param name="responseMessage">The response from the Pivotal Tracker API.</param>
        /// <returns>boolean (true) if successful.</returns>
        private bool HandleResponseBoolean(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                // Success, return a true;
                return true;
            }

            // Failure, throw generic exception with response
            throw ThrowException(responseMessage);
        }

        /// <summary>
        /// Helper method to serialize a HttpResponseMessage <paramref name="response"/> to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Object type to seralize to.</typeparam>
        /// <param name="response">The HttpResponseMessage to seralize.</param>
        /// <returns>Returns <paramref name="response"/> as seralized object of type <typeparamref name="T"/></returns>
        private T Serialize<T>(HttpResponseMessage response)
        {
            return JsonService.SerializeJsonToObject<T>(response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Reads the Result of <paramref name="response"/> as a string and throws a PivotalHttpException with the response.
        /// </summary>
        /// <param name="response">The response of the Http call.</param>
        /// <returns>Throws PivotalHttpException.</returns>
        private PivotalHttpException ThrowException(HttpResponseMessage response)
        {
            // Gets the result of the response as a string so that we can construct an exception.
            var text = response.Content.ReadAsStringAsync().Result;

            // Throws new exception with the body of our response result.
            throw new PivotalHttpException($"Result was unsuccessful. {text}");
        }
        #endregion
    }
}
