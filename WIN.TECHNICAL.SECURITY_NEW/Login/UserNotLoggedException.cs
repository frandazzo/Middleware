using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.Login
{
    public class UserNotLoggedException : Exception 
    {
        public override string Message
        {
            get
            {
                return "Utente non loggato";
            }
        }
    }
}
