using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.SECURITY.Core
{
    public interface IProfile
    {
        int ID { get; set; }
        string Description { get; set; }
        IList<IRole> Roles { get; set; }
        IList<IPermission> Permissions { get; set; }
    }
}
