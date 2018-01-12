using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.SECURITY.Core
{
    public interface IPermission
    {
        int ID { get; set; }
        IProfile Profile { get; set; }
        string FullMethodName { get; set; }
        string Macroarea { get; set; }
        string Alias { get; set; }

    }
}
