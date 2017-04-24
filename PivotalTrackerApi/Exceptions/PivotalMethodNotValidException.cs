using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPivotalTrackerApi.Exceptions
{
    public class PivotalMethodNotValidException : PivotalException
    {
        public PivotalMethodNotValidException() : base() { }
        public PivotalMethodNotValidException(string message) : base(message) { }
        public PivotalMethodNotValidException(string message, Exception innerException) : base(message, innerException) { }
    }
}
