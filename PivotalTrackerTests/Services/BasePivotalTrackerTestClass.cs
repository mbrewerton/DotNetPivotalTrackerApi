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

    public class BasePivotalTrackerTestClass
    {
        private PivotalTracker _tracker;
        public Mock<FakeHttpService> FakeHttpService = new Mock<FakeHttpService>();

        public PivotalTracker GetTracker(string apiToken = "test", int? projectId = null)
        {
            // Don't use object initialiser here! We want to initialise the PivotalTracker instance and then overwrite the HttpService with our Mock
            _tracker = new PivotalTracker(apiToken, projectId)
            {
                HttpService = FakeHttpService.Object
            };
            return _tracker;

        }

        public PivotalTracker GetTracker()
        {
            // Don't use object initialiser here! We want to initialise the PivotalTracker instance and then overwrite the HttpService with our Mock
            _tracker = new PivotalTracker { HttpService = FakeHttpService.Object };
            return _tracker;
        }

        public HttpResponseMessage CreateResponse<T>(T model)
        {
            //return new Task<HttpResponseMessage>(() => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonService.SerializeObjectToJson(model))});
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonService.SerializeObjectToJson(model))
            };
        }
    }
}
