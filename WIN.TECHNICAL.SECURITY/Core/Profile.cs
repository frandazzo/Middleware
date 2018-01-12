using System;
using System.Collections.Generic;
using System.Text;


using WIN.SECURITY.Core;

namespace WIN.SECURITY.Core
{
    [Serializable]
    public class Profile : IProfile
    {
        public Profile() { }

        private int _id;
        private string _description;
        private IList<IRole> _roles;
        private IList<IPermission> _permissions = new List<IPermission>();

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public IList<IRole> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        public IList<IPermission> Permissions
        {
            get { return _permissions; }
            set { _permissions = value; }
        }

        public override bool Equals(object obj)
        {
            Profile profile = obj as Profile;
            if (profile == null)
                return false;

            return profile.ID == ID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
