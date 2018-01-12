using System;
using System.Collections.Generic;
using System.Text;



using WIN.SECURITY.Core;
using WIN.SECURITY;

namespace WIN.SECURITY.Core
{
    [Serializable]
    public class User : IUser
    {
        public User() { }
        public User(IRole role)
        {
            _role = role;
            _role.Users.Add(this);
        }

        public static User Current
        {
            get
            {
                return SecurityManager.Instance.CurrentUser as User;
            }
        }

        private int _id;
        private IRole _role;
        private string _username="";
        private string _password="";
        private string _mail = "";
        private string _name = "";
        private string _surname = "";


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

       

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public IRole Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        
       



    }
}
