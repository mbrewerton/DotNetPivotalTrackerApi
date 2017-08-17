using System;

namespace DotNetPivotalTrackerApi.Exceptions
{
    public class PivotalHttpException : PivotalException
    {
        public PivotalHttpException() : base() { }
        public PivotalHttpException(string message) : base(message) { }
        public PivotalHttpException(string message, Exception innerException) : base(message, innerException) { }
    }
}
