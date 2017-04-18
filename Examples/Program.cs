using DotNetPivotalTrackerApi.Enums;
using DotNetPivotalTrackerApi.Models.User;
using DotNetPivotalTrackerApi.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //GetUserInfo();
            //GetProjects();
            CreateNewStory();
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
            _mytracker.GetProjects();
        }

        private static void CreateNewStory()
        {
            // This will create a new feature, "Please raise me a feature" with no labels
            _mytracker.CreateNewStory(_projectId, "Please raise me a feature", StoryType.feature);
            Console.WriteLine("Story has been raised, please check the project you used.");
        }
    }
}
