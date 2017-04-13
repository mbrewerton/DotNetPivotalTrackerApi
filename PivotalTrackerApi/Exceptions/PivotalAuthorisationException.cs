using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Exceptions
{
    public class PivotalAuthorisationException : Exception
    {
        public PivotalAuthorisationException() : base() { }
        public PivotalAuthorisationException(string message) : base(message) { }
        public PivotalAuthorisationException(string message, Exception innerException) :base(message, innerException) { }
    }
}
