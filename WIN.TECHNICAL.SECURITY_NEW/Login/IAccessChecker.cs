using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.Login
{
    public interface IAccessChecker
    {
         LoginResult CheckAccess(LoginAction action);
    }
}
