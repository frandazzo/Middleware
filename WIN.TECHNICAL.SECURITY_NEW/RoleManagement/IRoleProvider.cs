using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.RoleManagement
{
    public interface IRoleProvider
    {
        IList<Role> GetRoles();

        IList<Profile> GetProfiles();

        IList<string> GetProfileNameList();
    }
}
