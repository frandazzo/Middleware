using System;
using System.Collections.Generic;
using System.Text;

using WIN.SECURITY.Core;

namespace WIN.SECURITY
{
    public interface ISecureDataAccess
    {
        IUser GetUser(string username, string password);
        IList<IUser> GetUsers();
        IList<IRole> GetRoles();
        void Save(IUser user);
        void SaveRole(IRole role);
        void Save(IProfile profile);
        void DeleteRole(IRole role);
        void DeleteProfile(IProfile profile);
        IList<IProfile> GetProfiles();
    }
}
