﻿using System.Collections.Generic;
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
    public class TestPivotalTracker : PivotalTracker
    {
        public TestPivotalTracker(IHttpService service, int? projectId = null) : base("Test String", projectId)
        {
            HttpService = service;
        }
    }

    public class PivotalTrackerTestClass
    {
        private PivotalTracker _tracker;
        private readonly Mock<FakeHttpService> _fakeHttpService;

        public PivotalTrackerTestClass()
        {
            _fakeHttpService = new Mock<FakeHttpService>();
            _tracker = new TestPivotalTracker(_fakeHttpService.Object);
        }

        [Fact]
        public void Test_Get_User_Returns_User()
        {
            var returnUser = new PivotalUser
            {
                Email = "test@test.com",
                Id = 27,
                Initials = "TU",
                Name = "Test User",
                Username = "testusername"
            };
            var response = CreateResponse(returnUser);
            _fakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var user = _tracker.GetUser();

            Assert.Equal(user.Id, returnUser.Id);
        }

        [Fact]
        public void Test_Get_Projects_Returns_Projects()
        {
            var returnProjects = new List<PivotalProject>
            {
                new PivotalProject { Id = 1, Name = "Project 1", Public = true },
                new PivotalProject { Id = 2, Name = "Project 2", Public = false },
                new PivotalProject { Id = 3, Name = "Project 3", Public = false }
            };
            var response = CreateResponse(returnProjects);
            _fakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var projects = _tracker.GetProjects();

            Assert.Equal(3, projects.Count);
        }

        private HttpResponseMessage CreateResponse<T>(T model)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonService.SerializeObjectToJson(model))
            };
        }

        [Fact]
        private void Test_Get_Current_Project_With_No_ProjectId_Or_Persisted_Throws()
        {
            _tracker = new TestPivotalTracker(_fakeHttpService.Object);

            Assert.Throws<PivotalException>(() => _tracker.GetCurrentProject());
        }

        [Fact]
        private void Test_Get_Current_Project_With_Persisted_Id()
        {
            _tracker = new TestPivotalTracker(_fakeHttpService.Object, 1);
            var returnProject = new PivotalProject { Id = 1, Name = "Project 1", Public = true };
            var response = CreateResponse(returnProject);
            _fakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var project = _tracker.GetCurrentProject();

            Assert.NotNull(project);
        }

        [Fact]
        private void Test_Get_Current_Project_Without_Persisted_Id()
        {
            _tracker = new TestPivotalTracker(_fakeHttpService.Object);
            var returnProject = new PivotalProject { Id = 1, Name = "Project 1", Public = true };
            var response = CreateResponse(returnProject);
            _fakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var project = _tracker.GetCurrentProject(1);

            Assert.NotNull(project);
        }

        [Fact]
        private void Test_Get_Project_Stories()
        {
            _tracker = new TestPivotalTracker(_fakeHttpService.Object);
            var returnStories = new List<PivotalStory>
            {
                new PivotalStory { Id = 1, Name = "Story 1" },
                new PivotalStory { Id = 2, Name = "Story 2" },
                new PivotalStory { Id = 3, Name = "Story 3" },
            };
            var response = CreateResponse(returnStories);
            _fakeHttpService.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var stories = _tracker.GetProjectStories(1);

            Assert.NotNull(stories);
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