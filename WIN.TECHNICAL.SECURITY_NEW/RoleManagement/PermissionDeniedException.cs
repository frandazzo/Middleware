using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.RoleManagement
{
    public class PermissionDeniedException: Exception 
    {
        public override string Message
        {
            get
            {
                return "Permesso di accedere alla funzione richieste negato";
            }
        }
    }
}
