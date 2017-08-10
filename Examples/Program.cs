using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DotNetPivotalTrackerApi.Enums;
using DotNetPivotalTrackerApi.Models.Project;
using DotNetPivotalTrackerApi.Models.Stories;
using DotNetPivotalTrackerApi.Models.User;
using DotNetPivotalTrackerApi.Services;

namespace Examples
{
    class Program
    {
        private static string _apiKey = ConfigurationManager.AppSettings["ApiKey"];
        private static int _projectId = 2008069;
        private static PivotalTracker _mytracker;
        static void Main(string[] args)
        {
            _mytracker = new PivotalTracker(_apiKey);

            GetUserInfo();
            GetProjects();
            CreateNewStory();
            PersistProjectIdToTrackerInstance();
            Console.ReadKey();
        }

        private static void GetUserInfo()
        {
            // This method uses the current API Key for the tracker (_mytracker) to get your user data
            PivotalUser user = _mytracker.GetUser();
            Console.WriteLine($"User Info: {user.Name} ({user.Initials}) has username {user.Username} and Email {user.Email}");
        }

        private static void GetProjects()
        {
            PivotalUser user = _mytracker.GetUser();
            // This method uses the current API Key to get the projects the user is assigned to
            List<PivotalProject> projects = _mytracker.GetProjects();

            Console.WriteLine($"{user.Name} is assigned to the following projects:");
            
            foreach(var project in projects)
            {
                Console.WriteLine($@"   - {project.Name} {(project.Public == false ? "(PRIVATE)" : "")}");
            }
        }

        private static void PersistProjectIdToTrackerInstance()
        {
            // Just grab us the first Project our API Key has access to
            int projectIdToPersist = 
                _mytracker.GetProjects()
                .First()
                .Id;
            // Create a new PivotalTracker instance so that it doesn't interfere with `_mytracker`.
            // Passing it an Id will cause it to persist that Id
            PivotalTracker newTracker = new PivotalTracker(_apiKey, projectIdToPersist);

            // We can call the GetProjectStories without passing a projectId as we have one persisted
            List<PivotalStory> projectStories = newTracker.GetProjectStories();
            Console.WriteLine($"Found {projectStories.Count} stories using the persisted Project Id '{projectIdToPersist}'");
        }

        private static void CreateNewStory()
        {
            // This will create a new feature, "Please raise me a feature" with no labels
            _mytracker.CreateNewStory(_projectId, "Please raise me a feature", StoryType.feature);
            Console.WriteLine("Story has been raised, please check the project you used.");
        }
    }
}
