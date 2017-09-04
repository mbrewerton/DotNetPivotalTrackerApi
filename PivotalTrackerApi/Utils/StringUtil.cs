using DotNetPivotalTrackerApi.Enums;

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

        /// <summary>
        /// Returns the relative url for accessing stories within "My Work".
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <param name="queryValue">The query value of the "My Work" search. Example: "MB"</param>
        /// <returns></returns>
        public static string PivotalMyWorkQuery(int projectId, string queryValue)
        {
            var url = PivotalStorySearchUrl(projectId, $"mywork:{queryValue}");
            return url;
        }

        /// <summary>
        /// Returns the relative url for accessing stories within the project backlog.
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <returns></returns>
        public static string PivotalBacklogQuery(int projectId)
        {
            var url = PivotalStorySearchUrl(projectId, $"state:{StoryState.Unstarted.ToString().ToLower()}");
            return url;
        }

        /// <summary>
        /// Returns the relative url for accessing stories within the project icebox.
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <returns></returns>
        public static string PivotalIceboxQuery(int projectId)
        {
            var url = PivotalStorySearchUrl(projectId, $"state:{StoryState.Unscheduled.ToString().ToLower()}");
            return url;
        }

        /// <summary>
        /// Searches all Pivotal Tracker stories using the search query provided. For help with search quieries see <a href="https://www.pivotaltracker.com/help/articles/advanced_search/">https://www.pivotaltracker.com/help/articles/advanced_search/</a>
        /// </summary>
        /// <param name="projectId">Id of your project.</param>
        /// <param name="query">Search query string. For help see </param>
        /// <returns></returns>
        public static string PivotalStorySearchUrl(int projectId, string query)
        {
            var url = $"{PivotalProjectsUrl(projectId)}/search?query={query}";
            return url;
        }

        /// <summary>
        /// Returns the relative url for accessing all tasks on a story by story id.
        /// </summary>
        /// <param name="projectId">Id of your project</param>
        /// <param name="storyId">Id of the story to retrieve tasks for.</param>
        /// <returns></returns>
        public static string PivotalStoryTasksUrl(int projectId, int storyId)
        {
            return $"projects/{projectId}/stories/{storyId}/tasks";
        }
        
        /// <summary>
        /// Returns the relative url for a specific tasks on a story by id.
        /// </summary>
        /// <param name="projectId">Id of your project</param>
        /// <param name="storyId">Id of the story to retrieve tasks for.</param>
        /// <param name="taskId">Id of the task to retrieve.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the relative url for accessing a single project.
        /// </summary>
        /// <param name="projectId">If of your project.</param>
        /// <returns></returns>
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
