using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using DotNetPivotalTrackerApi.Enums;
using DotNetPivotalTrackerApi.Models.Project;
using DotNetPivotalTrackerApi.Models.Stories;
using DotNetPivotalTrackerApi.Models.User;
using DotNetPivotalTrackerApi.Services;
using DotNetPivotalTrackerApi.Models.Comments;
using System.IO;

namespace Examples
{
    class Program
    {
        private static string _apiKey = ConfigurationManager.AppSettings["ApiKey"];
        private static int _projectId = 0;
        private static PivotalTracker _mytracker;
        static void Main(string[] args)
        {
            _mytracker = new PivotalTracker(_apiKey);

            //GetIcebox();

            //Authorisation();

            //var story = new PivotalStory();
            //story.Name = "Hello";
            //story.Description = "World";
            //story.StoryType = StoryType.Chore.ToString();
            //story.Labels = new List<PivotalLabel>()
            //{
            //    new PivotalLabel { Name = "My Label" }
            //};

            //var saved = Task.Run(() => _mytracker.CreateNewStoryAsync(_projectId, story)).Result;

            //Task.Run(() => _mytracker.CreateNewStoryTaskAsync(_projectId, saved.Id.Value, "I am first first raised.", position: 1));
            //Task.Run(() => _mytracker.CreateNewStoryTaskAsync(_projectId, saved.Id.Value, "I am second first raised.", position: 2));
            //Task.Run(() => _mytracker.CreateNewStoryTaskAsync(_projectId, saved.Id.Value, "I am second third raised."));
            //Console.WriteLine("Done!");
            //GetUserInfo();
            //GetProjects();
            //CreateNewStory();
            //PersistProjectIdToTrackerInstance();
            //TestSearch();


            Console.ReadKey();
        }

        private static void TestSearch()
        {
            Console.Write("Enter a search query:");
            var query = Console.ReadLine();
            var result = _mytracker.SearchByQueryAsync(_projectId, query).Result;
            foreach (var story in result.Stories.Stories)
            {
                Console.WriteLine($"{story.Name} :: Owned by: {story.OwnerIds}");
            }
        }

        private static void GetIcebox()
        {
            var icebox = _mytracker.GetIceboxAsync(_projectId).Result;
            foreach (var story in icebox.Stories.Stories)
            {
                Console.WriteLine(story.Name);
            }
        }

        private static void Authorisation()
        {
            // We create a new instance of the PivotalTracker class using the default constructor
            var tracker = new PivotalTracker();
            // Authorise the tracker instance with username/password. Returns a PivotalUser if successful. Throws an exception if not.
            var authUser = tracker.AuthorizeAsync("your_username", "your_password").Result;
            Console.WriteLine($"API Token on user: {authUser.ApiToken}");
            Console.WriteLine($"API Token on tracker: {tracker.ApiToken}");
        }

        private static void GetUserInfo()
        {
            // This method uses the current API Key for the tracker (_mytracker) to get your user data
            PivotalUser user = _mytracker.GetUserAsync().Result;
            Console.WriteLine($"User Info: {user.Name} ({user.Initials}) has username {user.Username} and Email {user.Email}");
        }

        private static void GetProjects()
        {
            PivotalUser user = _mytracker.GetUserAsync().Result;
            // This method uses the current API Key to get the projects the user is assigned to
            List<PivotalProject> projects = _mytracker.GetProjectsAsync().Result;

            Console.WriteLine($"{user.Name} is assigned to the following projects:");

            foreach (var project in projects)
            {
                Console.WriteLine($@"   - {project.Id} - {project.Name} {(project.Public == false ? "(PRIVATE)" : "")}");
            }
        }

        private static void PersistProjectIdToTrackerInstance()
        {
            // Just grab us the first Project our API Key has access to
            int projectIdToPersist = 
                _mytracker.GetProjectsAsync().Result
                .First()
                .Id;
            // Create a new PivotalTracker instance so that it doesn't interfere with `_mytracker`.
            // Passing it an Id will cause it to persist that Id
            PivotalTracker newTracker = new PivotalTracker(_apiKey, projectIdToPersist);

            // We can call the GetProjectStories without passing a projectId as we have one persisted
            List<PivotalStory> projectStories = newTracker.GetProjectStoriesAsync().Result;
            Console.WriteLine($"Found {projectStories.Count} stories using the persisted Project Id '{projectIdToPersist}'");
        }

        private static void CreateNewStory()
        {
            // This will create a new feature, "Please raise me a feature" with no labels
            var story = _mytracker.CreateNewStoryAsync(_projectId, "Please raise me a feature lol", StoryType.Feature).Result;
            var task1 = _mytracker.CreateNewStoryTaskAsync(_projectId, story.Id.Value, "I am task 1").Result;
            var task2 = _mytracker.CreateNewStoryTaskAsync(_projectId, story.Id.Value, "I am task 2").Result;
            var comment = _mytracker.CreateNewCommentAsync(_projectId, story.Id.Value, "I am comment").Result;
            Console.WriteLine($"Story comment: {comment.Text}");
            var story2 = _mytracker.GetStoryByIdAsync(_projectId, story.Id.Value).Result;
            var comments = _mytracker.GetCommentsAsync(_projectId, story.Id.Value).Result;
            Console.WriteLine($"From story: {comments.First().Text}");
            Console.WriteLine("Story has been raised, please check the project you used.");
        }
    }
}
