using System;
using System.Collections.Generic;
using System.Text;

using WIN.SECURITY.Core;

namespace WIN.SECURITY.Core
{
    [Serializable]
    public class Permission : IPermission
    {
        private int _id;
        private IProfile _profile;
        private string _fullMethodName;
        private string _macroarea;
        private string _alias;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public IProfile Profile
        {
            get { return _profile; }
            set { _profile = value; }
        }

        public string FullMethodName
        {
            get { return _fullMethodName; }
            set { _fullMethodName = value; }
        }

        public string Macroarea
        {
            get { return _macroarea; }
            set { _macroarea = value; }
        }

        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
    }
}
