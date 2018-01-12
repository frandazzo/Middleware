using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.PasswordManagement
{
    public class PasswordMismatchException : Exception 
    {
        public override string Message
        {
            get
            {
                return "Password errata!";
            }
        }
    }
}
