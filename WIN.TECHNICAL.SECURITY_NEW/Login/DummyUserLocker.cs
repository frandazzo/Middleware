using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.Login
{
    public class DummyUserLocker : IUserLocker 
    {
        #region IUserLocker Membri di

        public void LockUser(IUserNew user)
        {
            return;
        }

        #endregion
    }
}
