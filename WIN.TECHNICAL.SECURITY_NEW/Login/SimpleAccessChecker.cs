using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.Login
{
    internal class SimpleAccessChecker : IAccessChecker 
    {

        #region IAccessChecker Membri di

        public LoginResult CheckAccess(LoginAction action)
        {
            if (action.User.Password.Equals(action.LoginPassword))
                return new LoginResult(true, "Benvenuto " + action.User.CompleteName , 0, LoginActionResult.AccessOk );
            return new LoginResult(false, "Username o password errati!", -1, LoginActionResult.WrongUserOrPassword);
        }

        #endregion
    }
}
