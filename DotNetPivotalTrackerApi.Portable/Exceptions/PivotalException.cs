using System;

namespace DotNetPivotalTrackerApi.Portable.Exceptions
{
    public class PivotalException : Exception
    {
        public PivotalException() : base() { }
        public PivotalException(string message) : base(message) { }
        public PivotalException(string message, Exception innerException) :base(message, innerException) { }
    }
}
