using System;

namespace DotNetPivotalTrackerApi.Exceptions
{
    public class PivotalNotFoundException : Exception
    {
        public PivotalNotFoundException() : base() { }
        public PivotalNotFoundException(string message) : base(message) { }
        public PivotalNotFoundException(string message, Exception innerException) :base(message, innerException) { }
    }
}
