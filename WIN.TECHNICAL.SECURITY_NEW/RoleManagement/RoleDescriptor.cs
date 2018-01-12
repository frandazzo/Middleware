using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace WIN.TECHNICAL.SECURITY_NEW.RoleManagement
{
    [XmlRootAttribute("DescrittoreRuoli", Namespace = "www.fenealgestweb.it", IsNullable = false)]
    public class RoleDescriptor
    {
        private Role[] _roles;
        


        public RoleDescriptor() { }


        public RoleDescriptor(Role[] roles) { _roles = roles; }


        [XmlArray("Ruoli"), XmlArrayItem("Ruolo", typeof(Role))] 
        public Role[] Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }


        public IList<string> EnabledFunctionNames()
        {
            IList<string> result = new List<string>();
            Hashtable h = new Hashtable ();

            foreach (Role item in _roles)
            {
                foreach (Profile elem in item.Profiles )
	            {
                    if (!h.ContainsKey(elem.Name))
                    {
                        result.Add(elem.Name);
                        h.Add(elem.Name, "");
                    }
                    		 
	            }
            }

            return result;
        }

    }
}
