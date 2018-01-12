using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.SECURITY.Attributes
{
    [AttributeUsage( AttributeTargets.Method )]
    public class Secure : System.Attribute
    {
        private string _macroArea = "";
        private string _fullName = "";
        private string _area = "";
        private string _alias = "";
        private bool _enabled = true;
        private bool _fullToString = false;

        public bool FullToString
        {
            get { return _fullToString; }
            set { _fullToString = value; }
        }

        public string MacroArea
        {
            get { return _macroArea; }
            set { _macroArea = value; }
        }

        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }

        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public override string ToString()
        {
            string value = _fullName;

            if (_alias != _fullName)
            {
                if (_fullToString)
                {
                    return string.Format("{0} ({1})", _alias, _fullName);
                }
                else
                {
                    return _alias;
                }
            }
            
            

            return value;
        }
    }
}
