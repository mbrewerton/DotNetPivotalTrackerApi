using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Exceptions
{
    public class PivotalHttpException : PivotalException
    {
        public PivotalHttpException() : base() { }
        public PivotalHttpException(string message) : base(message) { }
        public PivotalHttpException(string message, Exception innerException) : base(message, innerException) { }
    }
}
