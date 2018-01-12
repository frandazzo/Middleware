using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SECURITY_NEW.Login
{
    internal class ComplexAccessChecker: IAccessChecker
    {
        private IUserLocker _locker;
        private IUserProvider _userProvider;

        public ComplexAccessChecker(IUserLocker locker, IUserProvider userProvider)
        {
            if (locker == null)
                throw new ArgumentException("Locker utente mancante!");
            _locker = locker;
            if (userProvider  == null)
                throw new ArgumentException("UserProvider utente mancante!");
            _userProvider = userProvider;
        }




        public LoginResult CheckAccess(LoginAction action)
        {
            LoginResult result = null;
            //Verifico se l'utente è bloccato
            if (action.User.Locked)
            {
                result = new LoginResult(false, "Utente bloccato. Contattare l'amministratore per lo sblocco dell'account", -1, LoginActionResult.UserLocked );
                result.SetToken(null);
                //_userProvider.SetUserTokenSession("", action.User.UserName);
                return result;
            }

            ////Verifico se il numero di tentativi è terminato
            if ((action.TryNumber > 4) && (!action.User.Password.Equals(action.LoginPassword)))
            {
                _locker.LockUser(action.User);
                result = new LoginResult(false, "Nessun tentativo disponibile. Utente bloccato. Contattare l'amministratore per lo sblocco dell'account", -1, LoginActionResult.UserLocked);
                result.SetToken(null);
                //_userProvider.SetUserTokenSession("", action.User.UserName);
                return result;
            }

          

            //Verifico la password
            //Se nn è uguale devo incrementare il numero di tentativi eseguiti
            if (!action.User.Password.Equals(action.LoginPassword))
            {
                ////action.IncrementTryNumber();
                //string message = "Username o password non corretti!";
                ////int remainingtry = 5 - action.TryNumber; 
                ////if (remainingtry == 0)
                ////    message = "Nessun tentativo disponibile. Un nuovo errore bloccherà l'utente";
                //result = new LoginResult(false, message, 1,  LoginActionResult.WrongUserOrPassword );
                //result.SetToken(null);
                //return result;
                action.IncrementTryNumber();
                string message = "Password non corretta! Reinserisci la password.";
                int remainingtry = 5 - action.TryNumber;
                if (remainingtry == 0)
                    message = "Nessun tentativo disponibile. Un nuovo errore bloccherà l'utente";
                result = new LoginResult(false, message, (5 - action.TryNumber), LoginActionResult.WrongUserOrPassword);
                result.SetToken(null);
                //_userProvider.SetUserTokenSession("", action.User.UserName);
                return result;
            }
            //Verifico la password
            //Se è uguale devo verificare la scadenza della password
            else
            {
                //se la password è scaduta: data odierna minore di datas scadenzza pwd
                if (DateTime.Now.Date.CompareTo(action.User.PasswordDecay) > -1)
                {
                    result = new LoginResult(false, "Password scaduta. Rinnova la password!", -1,  LoginActionResult.DecayedPassword );
                    result.SetToken(null);
                    //_userProvider.SetUserTokenSession("", action.User.UserName);
                    return result;
                } 
                else
                {
                        result = new LoginResult(true, String.Format("Benvenuto {0}!", action.User.CompleteName), -1, LoginActionResult.AccessOk);
                        string token = Guid.NewGuid().ToString();
                        result.SetToken(token);
                        _userProvider.SetUserTokenSession(token, action.User.UserName);
                        return result;
                }
            }
           
        }

    }
}

