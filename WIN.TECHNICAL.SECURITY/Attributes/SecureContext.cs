using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.SECURITY.Attributes
{
    [AttributeUsage( AttributeTargets.Class )]
    public class SecureContext : System.Attribute
    {
        private bool enabled = true;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

    }
}
