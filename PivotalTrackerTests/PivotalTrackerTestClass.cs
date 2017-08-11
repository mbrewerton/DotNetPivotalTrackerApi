using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetPivotalTrackerApi.Services;
using System.Configuration;
using DotNetPivotalTrackerApi.Models.Stories;
using System.Collections.Generic;
using DotNetPivotalTrackerApi.Models.Comments;
using System.IO;
using DotNetPivotalTrackerApi.Enums;
using DotNetPivotalTrackerApi.Models.Tasks;
using System.Linq;

namespace PivotalTrackerTests
{
    [TestClass]
    public class PivotalTrackerTestClass
    {
        private readonly string _apiToken = ConfigurationManager.AppSettings["ApiToken"];
        private readonly PivotalTracker pt;
        private readonly int _testProjectId = int.Parse(ConfigurationManager.AppSettings["ProjectId"]);

        public PivotalTrackerTestClass()
        {
            pt = new DotNetPivotalTrackerApi.Services.PivotalTracker(_apiToken);
        }

        [TestMethod]
        public void TestGetUser()
        {
            var user = pt.GetUser();

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestGetProjects()
        {
            var projects = pt.GetProjects();

            Assert.IsNotNull(projects);
        }

        [TestMethod]
        public void Test_Get_Current_Project_With_Persisted_Id()
        {
            pt.SetProjectId(_testProjectId);
            var project = pt.GetCurrentProject();

            Assert.IsNotNull(project);
        }

        [TestMethod]
        public void Test_Get_Current_Project_Without_Persisted_Id()
        {
            var project = pt.GetCurrentProject(_testProjectId);

            Assert.IsNotNull(project);
        }

        [TestMethod]
        public void TestGetProjectStories()
        {
            var projectStories = pt.GetProjectStories(_testProjectId);

            Assert.IsNotNull(projectStories);
        }

        [TestMethod]
        public void TestCreateProjectStoryWithFullCtor()
        {
            var labels = new List<string>
            {
                "label1",
                "label2",
            };
            var createdStory = pt.CreateNewStory(_testProjectId, "My Name", DotNetPivotalTrackerApi.Enums.StoryType.feature, labels, "My Description");


            Assert.IsNotNull(createdStory);
        }

        [TestMethod]
        public void TestCreateProjectStory()
        {
            var story = new PivotalNewStory
            {
                Name = "My new story",
                Description = "My new description",
                StoryType = StoryType.bug.ToString(),
                Labels = new List<string>
                {
                    "My label"
                }
            };
            var createdStory = pt.CreateNewStory(_testProjectId, story);

            Assert.IsNotNull(createdStory);
        }

        [TestMethod]
        public void TestGetComments()
        {
            var comments = pt.GetComments(_testProjectId, 129853273);

            Assert.IsNotNull(comments);
        }

        [TestMethod]
        public void TestCreateComment()
        {
            var pivotalComment =  new PivotalNewComment
            {
                StoryId = 129853273,
                ProjectId = _testProjectId,
                Text = "Yaaay"
            };
            var comments = pt.CreateNewComment(pivotalComment);

            Assert.IsNotNull(comments);
        }

        [TestMethod]
        public void TestCreateCommentWithFullCtor()
        {
            var comments = pt.CreateNewComment(_testProjectId, 143518577, "My comment test");

            Assert.IsNotNull(comments);
        }

        [TestMethod]
        public void TestCreateCommentWithFullCtorAndFile()
        {
            var fileStream = File.OpenRead(@"C:\Users\MattBrewerton\Pictures\doge.png");
            var comments = pt.CreateNewComment(_testProjectId, 143518577, "My comment upload test", fileStream);

            Assert.IsNotNull(comments);
        }

        [TestMethod]
        public void Test_Update_Comment()
        {
            var comment = pt.GetComments(_testProjectId, 143518577).First();
            var oldText = comment.Text;
            var newText = oldText += " - This has been appended.";
            comment.Text = newText;
            var updatedComment = pt.UpdateComment(_testProjectId, 143518577, comment);

            Assert.IsTrue(updatedComment.Text == newText);
        }

        [TestMethod]
        public void TestGetTasksFromStory()
        {
            var storyTasks = pt.GetTasksFromStory(_testProjectId, 143518577);

            Assert.IsNotNull(storyTasks);
        }

        [TestMethod]
        public void TestCreateStoryTask()
        {
            var newStoryTask = new PivotalNewTask
            {
                Complete = false,
                Description = "My Task 2"
            };

            var createdTask = pt.CreateNewStoryTask(_testProjectId, 143518577, newStoryTask);

            Assert.IsNotNull(createdTask);
        }
        
        [TestMethod]
        public void TestCreateStoryTaskWithFullCtor()
        {
            var createdTask = pt.CreateNewStoryTask(_testProjectId, 143518577, "This is another task, should be second", position: 2);

            Assert.IsNotNull(createdTask);
        }

        [TestMethod]
        public void TestUpdateStoryTask()
        {
            var myTask = pt.GetTasksFromStory(_testProjectId, 143518577)[2];
            myTask.Position = 1;
            myTask.Description = myTask.Description + " This has been appended in an update";
            var createdTask = pt.UpdateStoryTask(_testProjectId, 143518577, myTask);

            Assert.IsNotNull(createdTask);
        }

        [TestMethod]
        public void TestDeleteStoryTask()
        {
            var myTask = pt.GetTasksFromStory(_testProjectId, 143518577)[2];

            Assert.IsTrue(pt.DeleteStoryTask(_testProjectId, 143518577, myTask.Id));
        }

        //[TestMethod]
        //public void TestGetEpics()
        //{
        //    var epics = pt.GetEpics(_testProjectId);

        //    Assert.IsNotNull(epics);
        //}
    }
}
