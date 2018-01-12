using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.SECURITY.Core
{
    public interface IRole
    {
        int ID { get; set; }
        string Description { get; set; }
        IList<IProfile> Profiles { get; set; }
        IList<IUser> Users { get; set; }
    }
}
