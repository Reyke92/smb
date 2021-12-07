using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    public class LoginRequiredException : Exception
    {
        public LoginRequiredException() : base() { }
        public LoginRequiredException(string message) : base(message) { }
    }
}
