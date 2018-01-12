using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.PasswordManagement
{
    public class SimpleDecaydateCalculator : IPasswordDecayHandler 
    {
        #region IPasswordDecayHandler Membri di

        public DateTime CalculateDecayDate(DateTime date)
        {
            return date.AddMonths(3).Date;
        }

        #endregion
    }
}
