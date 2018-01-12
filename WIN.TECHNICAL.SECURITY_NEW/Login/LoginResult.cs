using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.Login
{

    public enum LoginActionResult
    {
        AccessOk,
        DecayedPassword,
        WrongUserOrPassword,
        UserLocked,
        Undefined
    }


    public class LoginResult
    {

        private LoginResult() { }

        private LoginActionResult _actionResult = LoginActionResult.Undefined;


        public LoginActionResult ActionResult
        {
            get { return _actionResult; }
        }


        private string _loginMessage = "";
        private bool _canAccess;
        private string _token = null;
        private int _possibleTryNumber = 0;


        public string Token
        {
            get
            {
                return _token;

            }
        }

        internal void SetToken(string token)
        {
            _token = token;
        }

        public int PossibleTryNumber
        {
            get { return _possibleTryNumber; }
        }

        public string LoginMessage
        {
            get { return _loginMessage; }
        }

        public bool CanAccess
        {
            get { return _canAccess; }
        }

        public LoginResult(bool canAccess, string loginMessage, int possibleTryNumber,LoginActionResult actionResult )
        {
            _loginMessage = loginMessage;
            _canAccess = canAccess;
            _possibleTryNumber = possibleTryNumber;
            _actionResult = actionResult;
        }

        //public LoginResult(bool canAccess, string loginMessage, int possibleTryNumber, LoginActionResult actionResult, string token)
        //{
        //    _loginMessage = loginMessage;
        //    _canAccess = canAccess;
        //    _possibleTryNumber = possibleTryNumber;
        //    _actionResult = actionResult;
        //    _token = token;
        //}


        public string TryNumberMessage()
        {
            if (_possibleTryNumber == -1)
                return "";


            return "Tentativi rimanenti: " + _possibleTryNumber.ToString();
        }


    }
}
