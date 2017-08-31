using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetPivotalTrackerApi.Exceptions;
using DotNetPivotalTrackerApi.Models.Project;
using DotNetPivotalTrackerApi.Models.Stories;
using DotNetPivotalTrackerApi.Models.User;
using DotNetPivotalTrackerApi.Services;
using Moq;
using Xunit;

namespace PivotalTrackerTests.Services
{
    //public class TestPivotalTracker : PivotalTracker
    //{
    //    public TestPivotalTracker(IHttpService service, int? projectId = null) : base("Test String", projectId)
    //    {
    //        HttpService = service;
    //    }
    //}

    public class PivotalTrackerTestClass : BasePivotalTrackerTestClass
    {
        public PivotalTrackerTestClass()
        {
            FakeHttpService = new Mock<FakeHttpService>();
        }

        [Fact]
        public void Test_PivotalTracker_Authentication_With_Credentials_Returns_User()
        {
            var tracker = GetTracker();
            var returnUser = new PivotalUser
            {
                Username = "TestUser",
                ApiToken = "MyToken"
            };
            var response = CreateResponse(returnUser);
            FakeHttpService.Setup(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(response));
            tracker.AuthorizeAsync("testUser", "testPassword");
            FakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var user = tracker.GetUser().Result;

            Assert.Equal(user.ApiToken, returnUser.ApiToken);
        }

        [Fact]
        public void Test_PivotalTracker_Authentication_Sets_ApiToken()
        {
            var tracker = GetTracker();
            var returnUser = new PivotalUser
            {
                Username = "TestUser",
                ApiToken = "MyToken"
            };
            var response = CreateResponse(returnUser);
            FakeHttpService.Setup(x => x.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(response));
            var auth = tracker.AuthorizeAsync("test", "test").Result;

            Assert.Equal(returnUser.ApiToken, tracker.ApiToken);
        }

        [Fact]
        public void Test_PivotalTracker_Authentication_Without_Credentials_Throws()
        {
            var tracker = GetTracker();
            Assert.ThrowsAsync<PivotalMethodNotValidException>(() => tracker.AuthorizeAsync("", ""));
        }

        [Fact]
        public void Test_Credential_Authorisation_Throws_On_Forbidden()
        {
            //var tracker = GetTracker("testuser", "testPassword");
            //var returnResponse = new HttpResponseMessage(HttpStatusCode.Forbidden);
            //FakeHttpService.Setup(x => x.)
        }

        [Fact]
        public void Test_Get_User_Returns_User()
        {
            var tracker = GetTracker();
            var returnUser = new PivotalUser
            {
                Email = "test@test.com",
                Id = 27,
                Initials = "TU",
                Name = "Test User",
                Username = "testusername"
            };
            var response = CreateResponse(returnUser);
            FakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var user = tracker.GetUser();

            Assert.Equal(user.Id, returnUser.Id);
        }

        [Fact]
        public void Test_Get_Projects_Returns_Projects()
        {
            var tracker = GetTracker();
            var returnProjects = new List<PivotalProject>
            {
                new PivotalProject { Id = 1, Name = "Project 1", Public = true },
                new PivotalProject { Id = 2, Name = "Project 2", Public = false },
                new PivotalProject { Id = 3, Name = "Project 3", Public = false }
            };
            var response = CreateResponse(returnProjects);
            FakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var projects = tracker.GetProjects().Result;

            Assert.Equal(3, projects.Count);
        }

        [Fact]
        private async void Test_Get_Current_Project_With_No_ProjectId_Or_Persisted_Throws()
        {
            var tracker = GetTracker();
            await Assert.ThrowsAsync<PivotalException>(() => tracker.GetCurrentProjectAsync());
        }

        [Fact]
        private void Test_Get_Current_Project_With_Persisted_Id()
        {
            var tracker = GetTracker(projectId: 1);
            var returnProject = new PivotalProject { Id = 1, Name = "Project 1", Public = true };
            var response = CreateResponse(returnProject);
            FakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var project = tracker.GetCurrentProjectAsync();

            Assert.NotNull(project);
        }

        [Fact]
        private void Test_Get_Current_Project_Without_Persisted_Id()
        {
            var tracker = GetTracker();
            var returnProject = new PivotalProject { Id = 1, Name = "Project 1", Public = true };
            var response = CreateResponse(returnProject);
            FakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var project = tracker.GetCurrentProjectAsync(1);

            Assert.NotNull(project);
        }

        [Fact]
        private void Test_Get_Project_Stories()
        {
            var tracker = GetTracker();
            var returnStories = new List<PivotalStory>
            {
                new PivotalStory { Id = 1, Name = "Story 1" },
                new PivotalStory { Id = 2, Name = "Story 2" },
                new PivotalStory { Id = 3, Name = "Story 3" },
            };
            var response = CreateResponse(returnStories);
            FakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var stories = tracker.GetProjectStoriesAsync(1);

            Assert.NotNull(stories);
        }

        [Fact]
        private void Test_CheckProjectIds_Throws_If_Both_Null()
        {
            var tracker = GetTracker();
            Assert.Throws<NullReferenceException>(() => tracker.CheckProjectIds(null));
        }

        //[Fact]
        //public void TestCreateProjectStoryWithFullCtor()
        //{
        //    var labels = new List<string>
        //    {
        //        "label1",
        //        "label2",
        //    };
        //    var createdStory = pt.CreateNewStory(_testProjectId, "My Name", DotNetPivotalTrackerApi.Enums.StoryType.feature, labels, "My Description");


        //    Assert.IsNotNull(createdStory);
        //}

        //[Fact]
        //public void TestCreateProjectStory()
        //{
        //    var story = new PivotalNewStory
        //    {
        //        Name = "My new story",
        //        Description = "My new description",
        //        StoryType = StoryType.bug.ToString(),
        //        Labels = new List<string>
        //        {
        //            "My label"
        //        }
        //    };
        //    var createdStory = pt.CreateNewStory(_testProjectId, story);

        //    Assert.IsNotNull(createdStory);
        //}

        //[Fact]
        //public void TestGetComments()
        //{
        //    var comments = pt.GetComments(_testProjectId, 129853273);

        //    Assert.IsNotNull(comments);
        //}

        //[Fact]
        //public void TestCreateComment()
        //{
        //    var pivotalComment =  new PivotalNewComment
        //    {
        //        StoryId = 129853273,
        //        ProjectId = _testProjectId,
        //        Text = "Yaaay"
        //    };
        //    var comments = pt.CreateNewComment(pivotalComment);

        //    Assert.IsNotNull(comments);
        //}

        //[Fact]
        //public void TestCreateCommentWithFullCtor()
        //{
        //    var comments = pt.CreateNewComment(_testProjectId, 143518577, "My comment test");

        //    Assert.IsNotNull(comments);
        //}

        //[Fact]
        //public void TestCreateCommentWithFullCtorAndFile()
        //{
        //    var fileStream = File.OpenRead(@"C:\Users\MattBrewerton\Pictures\doge.png");
        //    var comments = pt.CreateNewComment(_testProjectId, 143518577, "My comment upload test", fileStream);

        //    Assert.IsNotNull(comments);
        //}

        //[Fact]
        //public void Test_Update_Comment()
        //{
        //    var comment = pt.GetComments(_testProjectId, 143518577).First();
        //    var oldText = comment.Text;
        //    var newText = oldText += " - This has been appended.";
        //    comment.Text = newText;
        //    var updatedComment = pt.UpdateComment(_testProjectId, 143518577, comment);

        //    Assert.IsTrue(updatedComment.Text == newText);
        //}

        //[Fact]
        //public void TestGetTasksFromStory()
        //{
        //    var storyTasks = pt.GetTasksFromStory(_testProjectId, 143518577);

        //    Assert.IsNotNull(storyTasks);
        //}

        //[Fact]
        //public void TestCreateStoryTask()
        //{
        //    var newStoryTask = new PivotalNewTask
        //    {
        //        Complete = false,
        //        Description = "My Task 2"
        //    };

        //    var createdTask = pt.CreateNewStoryTask(_testProjectId, 143518577, newStoryTask);

        //    Assert.IsNotNull(createdTask);
        //}

        //[Fact]
        //public void TestCreateStoryTaskWithFullCtor()
        //{
        //    var createdTask = pt.CreateNewStoryTask(_testProjectId, 143518577, "This is another task, should be second", position: 2);

        //    Assert.IsNotNull(createdTask);
        //}

        //[Fact]
        //public void TestUpdateStoryTask()
        //{
        //    var myTask = pt.GetTasksFromStory(_testProjectId, 143518577)[2];
        //    myTask.Position = 1;
        //    myTask.Description = myTask.Description + " This has been appended in an update";
        //    var createdTask = pt.UpdateStoryTask(_testProjectId, 143518577, myTask);

        //    Assert.IsNotNull(createdTask);
        //}

        //[Fact]
        //public void TestDeleteStoryTask()
        //{
        //    var myTask = pt.GetTasksFromStory(_testProjectId, 143518577)[2];

        //    Assert.IsTrue(pt.DeleteStoryTask(_testProjectId, 143518577, myTask.Id));
        //}

        //[Fact]
        //public void TestGetEpics()
        //{
        //    var epics = pt.GetEpics(_testProjectId);

        //    Assert.IsNotNull(epics);
        //}
    }
}
