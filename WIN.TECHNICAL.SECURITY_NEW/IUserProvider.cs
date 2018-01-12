using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.RoleManagement;

namespace WIN.TECHNICAL.SECURITY_NEW
{
    public interface IUserProvider
    {
         IUserNew GetUserByUserName(string userName);
         IUserNew GetUserByToken(string token);
         void SetUserTokenSession(string token, string username);
         void CancelUserTokenSession(string token);
       
         //void UpdateUser(IUserNew user);

         void UpdateUserPassword(IUserNew user);


        
    }
}
