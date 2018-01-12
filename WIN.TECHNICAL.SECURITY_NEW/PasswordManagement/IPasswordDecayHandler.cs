using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.PasswordManagement
{
    public interface IPasswordDecayHandler
    {
        DateTime CalculateDecayDate(DateTime date);
    }
}
