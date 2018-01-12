using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.SECURITY.Core
{
    public interface IUser
    {
        int ID { get; set; }
        string Username { get; set; }
        string Password { get; set; }
       
        string Mail { get; set; }
        IRole Role { get; set; }
    }
}
