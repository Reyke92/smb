using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    public class LogoutRequiredException : Exception
    {
        public LogoutRequiredException() : base() { }
        public LogoutRequiredException(string message) : base(message) { }
    }
}
