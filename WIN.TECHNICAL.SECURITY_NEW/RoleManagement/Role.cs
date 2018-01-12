using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.TECHNICAL.SECURITY_NEW.RoleManagement
{
    [Serializable]
    public class Role
    {
        private string _name = "";

        private string _roleId = "";

        [XmlAttribute("RoleId")]
        public string RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }


        [XmlAttribute("Nome")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Role()
        { }

        private Profile [] _profiles;



        [XmlArray("Profilo"), XmlArrayItem("Funzione", typeof(Profile))]
        public Profile[] Profiles
        {
            get { return _profiles; }
            set { _profiles = value; }
        }


        public bool IsEnabled(string profileName)
        {
            foreach (Profile item in _profiles )
            {
                if (item.Name.ToUpper().Equals(profileName.ToUpper()))
                    return true;
            }
            return false;
        }




    }
}
