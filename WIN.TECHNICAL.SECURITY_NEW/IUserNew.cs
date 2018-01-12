using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.RoleManagement;

namespace WIN.TECHNICAL.SECURITY_NEW
{
    public interface IUserNew
    {
        string Mail { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string CompleteName { get;  }
        bool Locked { get; set; }
        DateTime PasswordData { get; set; }
        DateTime PasswordDecay { get; }
        IList<Role> Roles { get; set; }
        bool IsEnabled(string profileName);
        void AddRole(Role role);
        void RemoveRole(Role role);
        IList<string> EnabledFunctionNames();
    }
}
