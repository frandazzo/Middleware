using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.TECHNICAL.SECURITY_NEW.RoleManagement
{
    [Serializable]
    public class Profile
    {
        private string _name = "";

        [XmlAttribute("Nome")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _area = "";

        [XmlAttribute("Area")]
        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }


        private string _command = "";

        [XmlAttribute("Comando")]
        public string Command
        {
            get { return _command; }
            set { _command = value; }
        }

        private string _description = "";


        [XmlAttribute("Descrizione")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        private string _group = "";


        [XmlAttribute("Gruppo")]
        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }

        public static int CompareResourcesByName(Profile x, Profile y)
        {
            if (x.Name == null)
            {
                if (y.Name == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y.Name == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = x.Name.CompareTo(y.Name);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return x.Name.CompareTo(y.Name);
                    }
                }
            }

        }



        public Profile()
        { }
    }
}
