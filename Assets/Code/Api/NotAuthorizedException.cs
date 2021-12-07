using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base() { }
        public NotAuthorizedException(string message) : base(message) { }
    }
}
