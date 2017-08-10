using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Utils
{
    public static class StringUtil
    {
        /// <summary>
        /// Returns the relative url for accessing the user data associated to the current Api Token.
        /// </summary>
        /// <returns></returns>
        public static string PivotalCurrentUser()
        {
            return $"me";
        }
        /// <summary>
        /// Returns the relative url for accessing all Pivotal stories within a project as a string.
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <returns></returns>
        public static string PivotalStoriesUrl(int projectId)
        {
            return $"projects/{projectId}/stories";
        }

        /// <summary>
        /// Returns the relative url for accessing a single Pivotal story within a project as a string.
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <param name="storyId">Id of the story to retrieve</param>
        /// <returns></returns>
        public static string PivotalStoriesUrl(int projectId, int storyId)
        {
            return $"projects/{projectId}/stories/{storyId}";
        }

        public static string PivotalStoryTasksUrl(int projectId, int storyId)
        {
            return $"projects/{projectId}/stories/{storyId}/tasks";
        }

        public static string PivotalStoryTasksUrl(int projectId, int storyId, int taskId)
        {
            return $"projects/{projectId}/stories/{storyId}/tasks/{taskId}";
        }

        /// <summary>
        /// Returns the relative url for accessing all Pivotal comments on a story as a string.
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <param name="storyId">Id of the story to retrieve comments for.</param>
        /// <returns></returns>
        public static string PivotalCommentsUrl(int projectId, int storyId)
        {
            return $"projects/{projectId}/stories/{storyId}/comments";
        }

        /// <summary>
        /// Returns the relative url for accessing a single Pivotal comment within a story as a string.
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <param name="storyId">Id of the story to retrieve a comment for.</param>
        /// <param name="commentId">Id of the comment to retrieve.</param>
        /// <returns></returns>
        public static string PivotalCommentsUrl(int projectId, int storyId, int commentId)
        {
            return $"projects/{projectId}/stories/{storyId}/comments/{commentId}";
        }

        public static string PivotalProjectsUrl(int? projectId = null)
        {
            return $"projects/{(projectId != null ? "/" + projectId : "")}";
        }

        /// <summary>
        /// Returns the relative url for accessing Pivotal uploads as a string.
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <returns></returns>
        public static string PivotalUploadsUrl(int projectId)
        {
            return $"projects/{projectId}/uploads";
        }

        /// <summary>
        /// Returns the relative url for accsesing Pivotal story tasks
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <param name="storyId">Id of the story to access tasks from.</param>
        /// <returns></returns>
        public static string StoryTasksUrl(int projectId, int storyId)
        {
            return $"/projects/{projectId}/stories/{storyId}/tasks";
        }
    }
}
