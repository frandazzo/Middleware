using System;
using System.Collections.Generic;

using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.Login;
using WIN.TECHNICAL.SECURITY_NEW.PasswordManagement;
using System.Net.Mail;
using WIN.TECHNICAL.SECURITY_NEW.RoleManagement;

namespace WIN.TECHNICAL.SECURITY_NEW
{
    public class SecurityController
    {
        private IPasswordDecayHandler _decayHandler = new SimpleDecaydateCalculator();
      
        private IUserProvider _provider;
        private IUserNew _currentUser;
        private IAccessChecker _checker = new SimpleAccessChecker();
        private IRoleProvider _roleProvider;
        private LoginAction _action;
        private LoginResult _result;
        private string _token = null;

        public string CurrentToken
        {
            get
            {
                return _token;
            }
        }

        private static SecurityController _instance;

        public static SecurityController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SecurityController();
                return _instance;
            }
        }

        private SecurityController()
        {

        }

        /// <summary>
        /// Crea una nuova istanza non in modalità Singleton
        /// Di utilizzo necessario per il web!
        /// </summary>
        public static SecurityController NewInstance
        {
            get
            {
                
                    return   new SecurityController();
               
            }
        }


        public IRoleProvider RoleProvider
        {
            get { return _roleProvider; }
        }


        public IList<Profile> GetProfiles()
        {
            if (_roleProvider == null)
                return new List<Profile>();

            return _roleProvider.GetProfiles();
        }

        public IList<string> GetProfileNameList()
        {
            if (_roleProvider == null)
                return new List<string>();

            return _roleProvider.GetProfileNameList();
        }


        public Role GetRoleByName(string name)
        {
            if (_roleProvider == null)
                return null;


            foreach (Role item in _roleProvider.GetRoles())
            {
                if (item.Name.Equals(name))
                    return item;
            }
            return null; 

        }


        public void InializeSecurityController(IUserProvider provider, IRoleProvider roleProvider)
        {
            if (provider == null)
                throw new Exception("Provider per la ricerca utente nullo");

            _provider = provider;


            if (roleProvider  == null)
                throw new Exception("Provider per la ricerca ruoli nullo");

            _roleProvider = roleProvider;

            _token = null;
          
        }


        public void InializeSecurityController(IUserProvider provider, IRoleProvider roleProvider,  IUserLocker locker)
        {
            if (provider == null)
                throw new Exception("Provider per la ricerca utente nullo");

            _provider = provider;


            if (roleProvider == null)
                throw new Exception("Provider per la ricerca ruoli nullo");

            _roleProvider = roleProvider;


            _token = null;

          
            if (locker != null)
                _checker = new ComplexAccessChecker(locker, provider);
              

            //_locker = locker;

        }


        //public void InializeSecurityController(IUserProvider provider, IRoleProvider roleProvider, IAccessChecker checker)
        //{
        //    if (provider == null)
        //        throw new Exception("Provider per la ricerca utente nullo");

        //    if (roleProvider == null)
        //        throw new Exception("Provider per la ricerca ruoli nullo");

        //    _roleProvider = roleProvider;


        //    _token = null;
         

        //    _provider = provider;

        //    if (checker != null)
        //        _checker = checker;

        //    //if (locker != null)
        //    //    _locker = locker;

        //}


        public void InializeSecurityController(IUserProvider provider, IRoleProvider roleProvider,  IAccessChecker checker)
        {
            if (provider == null)
                throw new Exception("Provider per la ricerca utente nullo");

            if (roleProvider == null)
                throw new Exception("Provider per la ricerca ruoli nullo");

            _roleProvider = roleProvider;


            _token = null;
            
            _provider = provider;

            if (checker != null)
                _checker = checker;

        }


        public LoginResult Login(string token)
        {

            if (_provider == null)
                throw new Exception("Controller sicurezza non inizializzato");

          

            //ottengo l'utente con il token selezionato
            _currentUser = _provider.GetUserByToken(token);


            if (_currentUser == null)
            {
                _result = new LoginResult(false, "Username o password errati!", -1, LoginActionResult.WrongUserOrPassword);
                _result.SetToken(null);
                return _result;
            }

            _result = new LoginResult(true, "", -1, LoginActionResult.AccessOk);
            _token = token;

            return _result;
        }

        public LoginResult Login(string userName, string password)
        {

            if (_provider == null)
                throw new Exception ("Controller sicurezza non inizializzato");


            //la materializzazione dell'utente che avviene tramite i metodi
            //materialize e rematerialize non fa altro che verificate che l'utente con lo username inserito esista
            //e una volta verificata l'esistenza crea un oggetto action che tiene conto dell'utente con quello username
            //della password immessa, e del numero di tentativi effettuati
            if (_currentUser == null)
                MaterializeCurrentUser(userName, password);
            else
                ReMaterializeCurrentUser(userName, password);

            if (_currentUser == null)
            {
                _result = new LoginResult(false, "Username o password errati!", -1, LoginActionResult.WrongUserOrPassword);
                _result.SetToken(null);
                return _result;
            }

            //dato che un utente è stato rovato con quello username
            //verifico l'accesso tenendo conto del numero di tentativi
            _result = _checker.CheckAccess(_action);


            if (_result.CanAccess)
            {
                _action.ReseTryNumber();
                _token = _result.Token;
            }
            

            return _result;


        }


        //public LoginResult StatelessLogin(string userName, string password)
        //{

        //    if (_provider == null)
        //        throw new Exception("Controller sicurezza non inizializzato");

        //    IUserNew user = _provider.GetUserByUserName(userName);

        //    if (user == null)
        //    {
        //        return new LoginResult(false, "Accesso negato", -1, LoginActionResult.WrongUserOrPassword);
        //    }


        //    LoginAction action = new LoginAction(user, password);

        //    LoginResult result;

        //    result = _checker.CheckAccess(action);

        //    return result;
        //}






        private void ReMaterializeCurrentUser(string userName, string password)
        {
            _currentUser = _provider.GetUserByUserName(userName);
            if (_currentUser != null)
            {
                if (_action != null)
                {
                    _action.SetNewLoginPassword(password);
                    _action.SetNewUser(_currentUser);
                }
                else
                {
                    _action = new LoginAction(_currentUser, password);
                }
            }
            else
            {
                _action = null;
            } 
        }

        public IUserNew CurrentUser
        {
            get { return _currentUser; }
        }

        private void MaterializeCurrentUser(string userName, string password)
        {
            _currentUser = _provider.GetUserByUserName(userName);
            if (_currentUser != null)
                _action = new LoginAction(_currentUser, password );
            else
                _action = null;
        }


        public void ResetLogin()
        {
            
            _action = null;
            _result = null;


            if (_provider != null)
                if (_currentUser != null)
                    _provider.CancelUserTokenSession(_token);
            _currentUser = null;
            _token = null;
        }


        public bool IsUserLogged()
        {
            if (_result == null)
                return false;
            return _result.CanAccess;
        }

        public bool IsUserLogged(string token)
        {
            if (_result == null)
                return false;
            return _result.CanAccess && token == _token && _token != null;
        }

        public bool IsUserauthorized(string profileName)
        {
            if (IsUserLogged() == false)
                return false;
            if (!_currentUser.IsEnabled(profileName))
                return false;
            return true;
        }

        public void RenewPassword(string oldPassword, string newPassword)
        {
            

            if (!this.IsUserLogged())
                throw new UserNotLoggedException ();

            if (!_currentUser.Password.Equals(oldPassword))
                throw new PasswordMismatchException();

            _currentUser.PasswordData = DateTime.Now.Date;
            _currentUser.Password = newPassword;


            _provider.UpdateUserPassword(_currentUser);
            
        }

        public bool  RenewPassword(string username, string oldPassword, string newPassword)
        {

           // bool result;

            if (string.IsNullOrEmpty(newPassword))
                throw new Exception("Null password is invalid");

            IUserNew u = _provider.GetUserByUserName(username);

            if (u == null)
                return false;

            if (u.Locked)
                return false;

            if (!u.Password.Equals(oldPassword))
                return false;


            if (u.UserName.ToLower() == "fenealuil")
                return false;

            u.PasswordData = DateTime.Now.Date;
            u.Password = newPassword;


            _provider.UpdateUserPassword(u);

            return true;

        }



        public void CheckAuthorizzation(string profileName)
        {
            if (IsUserLogged() == false)
                throw new UserNotLoggedException();

            if (!_currentUser.IsEnabled(profileName))
                throw new PermissionDeniedException();
        }

        public void CheckUserLogged()
        {
            if (IsUserLogged() == false)
                throw new UserNotLoggedException();
        }



        public void SetPasswordDecayHandler(IPasswordDecayHandler handler)
        {
            if (handler != null)
                _decayHandler = handler;
        }



        public void SendNewPasswordbyEmail(string userName, IMailer mailer)
        {
            IUserNew user = _provider.GetUserByUserName(userName);
            if (user == null)
                return;


            if (user.UserName.ToLower() == "fenealuil")
                return;

            string pwd = Guid.NewGuid().ToString();

            user.Password = pwd.Substring(0, 8); 

            user.PasswordData = DateTime.Now.Date;

            _provider.UpdateUserPassword(user);

            try
            {
                if (mailer != null)
                    mailer.SendMail(user.Mail, "Invio nuova password", "La password è: " + user.Password);
            }
            catch (Exception)
            {
                //non fa niente
            }

          
            
        }





        public void RenewPasswordWithoutCheck(string username, string newPwd)
        {

            IUserNew u = _provider.GetUserByUserName(username);
            if (u == null)
                return ;

            u.PasswordData = DateTime.Now.Date;
            u.Password = newPwd;


            _provider.UpdateUserPassword(u);

        }
    }
}
