using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.Login
{
    public class LoginAction
    {

        


       
        private IUserNew _user;
       
        private string _loginPassword = "";

        public string  LoginPassword
        {
            get { return _loginPassword; }
        }

       

        private int _tryNumber = 0;

        public int TryNumber
        {
            get { return _tryNumber; }
        }


        public IUserNew User
        {
            get { return _user; }
        }



        public LoginAction(IUserNew user, string loginPassword)
        {
            if (user == null)
                throw new Exception("Utente non specificato");

           
            _user = user;
            _loginPassword = loginPassword;
        }


        public void IncrementTryNumber()
        {
            _tryNumber++;
        }

        internal void SetNewLoginPassword(string loginPassword)
        {
            _loginPassword = loginPassword;
        }

        internal void SetNewUser(IUserNew user)
        {
            if (!user.UserName.ToLower().Equals(_user.UserName.ToLower()))
                _tryNumber = 0;


            _user  = user ;
        }

        internal void ReseTryNumber()
        {
            _tryNumber = 0;
        }

    }
}
