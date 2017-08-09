using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Exceptions
{
    public class PivotalUserException : PivotalException
    {
        public PivotalUserException() : base() { }
        public PivotalUserException(string message) : base(message) { }
    }
}
