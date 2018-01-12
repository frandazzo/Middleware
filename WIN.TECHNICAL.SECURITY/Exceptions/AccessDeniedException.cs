using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.SECURITY.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message)
            : base(message)
        { }
    }
}
