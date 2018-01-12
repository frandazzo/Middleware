using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.SECURITY.Exceptions
{
    public class UserNotLoggedException : Exception
    {
        public UserNotLoggedException(string message)
            : base(message)
        { }
    }
}
