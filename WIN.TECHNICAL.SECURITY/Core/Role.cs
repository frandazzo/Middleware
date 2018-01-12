using System;
using System.Collections.Generic;
using System.Text;

using WIN.SECURITY.Core;

namespace WIN.SECURITY.Core
{
    [Serializable]
    public class Role : IRole
    {
        private int _id;
        private string _description;
        private IList<IProfile> _profiles = new List<IProfile>();
        private IList<IUser> _users = new List<IUser>();

        public int ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        public IList<IProfile> Profiles
        {
            get { return this._profiles; }
            set { this._profiles = value; }
        }

        public IList<IUser> Users
        {
            get { return _users; }
            set { this._users = value; }
        }

        public override string ToString()
        {
            return this._description;
        }

        //public override string ToHistoryString()
        //{
        //    string result = "";
        //    if (this.Description != null)
        //        result = "Descrizione Unità Misura = " + this.Description + "\n";
        //    int i = 0;
        //    if (this._profiles != null)
        //    {
        //        foreach (IProfile profile in _profiles)
        //        {
        //            if (profile.Description != null)
        //            {
        //                i++;
        //                result = result + "   Descrizione Profilo " + i.ToString() + " = " + profile.Description + "\n";
        //            }
        //        }
        //    }

        //    i = 0;
        //    if (this._users != null)
        //    {
        //        foreach (IUser user in _users)
        //        {
        //            if (user.Description != null)
        //            {
        //                i++;
        //                result = result + "   Descrizione Utente " + i.ToString() + " = " + user.Username + "\n";
        //            }
        //        }
        //    }

        //    return result;
        //}
    }
}
