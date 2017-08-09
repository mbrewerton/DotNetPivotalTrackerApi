using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Exceptions
{
    public class PivotalNotFoundException : Exception
    {
        public PivotalNotFoundException() : base() { }
        public PivotalNotFoundException(string message) : base(message) { }
        public PivotalNotFoundException(string message, Exception innerException) :base(message, innerException) { }
    }
}
