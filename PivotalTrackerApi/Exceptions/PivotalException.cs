using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Exceptions
{
    public class PivotalException : Exception
    {
        public PivotalException() : base() { }
        public PivotalException(string message) : base(message) { }
        public PivotalException(string message, Exception innerException) :base(message, innerException) { }
    }
}
