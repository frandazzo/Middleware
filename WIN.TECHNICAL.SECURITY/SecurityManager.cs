using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

using WIN.SECURITY.Core;
using WIN.SECURITY.Attributes;
using System.IO;


namespace WIN.SECURITY
{
    public class SecurityManager
    {
        //singleton
        private static SecurityManager _instance;
        private SecurityLogDelegate _logDelegate;

        private SecurityManager()
        {
            _logDelegate = new SecurityLogDelegate(DefaultLogDelegate);
        }

        public static SecurityManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SecurityManager();

                return _instance;
            }
        }

        public SecurityLogDelegate LogDelegate
        {
            get { return _logDelegate; }
            set { _logDelegate = value; }
        }

        private IUser _currentUser;
        private string _lastError;
        private ISecureDataAccess _secureDataAccess = null;
        private List<Assembly> _assemblies = new List<Assembly>();
        private Dictionary<string, Secure> _secureMethods = new Dictionary<string, Secure>();

        private void DefaultLogDelegate(string message)
        {
            Debug.WriteLine(message);
        }

        public IUser CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public string LastError
        {
            get { return _lastError; }
            set { _lastError = value; }
        }

        public ISecureDataAccess SecureDataAccess
        {
            get { return _secureDataAccess; }
            set { _secureDataAccess = value; }
        }

        public Dictionary<string, Secure> SecureMethods
        {
            get { return _secureMethods; }
            set { _secureMethods = value; }
        }
        /// <summary>
        /// Adds an assembly to the security manager in order to cofigure the secure methods inside the
        /// assembly types
        /// </summary>
        /// <param name="assembly"></param>
        public void AddAssembly(Assembly assembly)
        {

            try
            {
                if (_assemblies.Contains(assembly))
                    return;

                try
                {
                    Type[] t = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex1)
                {

                    string inner = "";
                    if (ex1.InnerException != null)
                        inner = ex1.InnerException.Message;

                    Exception[] lki = ex1.LoaderExceptions;
                    StringBuilder b = new StringBuilder();
                    foreach (Exception item in lki)
                    {
                        b.AppendLine(item.Message);
                    }

                    throw new Exception("Errore nella getTypes della add Assembly: " + ex1.Message + " ___ " + inner + "____" + b.ToString() + " ___ " + ex1.StackTrace);
                }
                


                _assemblies.Add(assembly);
                //Ciclo su tutti i type dell'assembly
                foreach (Type type in assembly.GetTypes())
                {
                    //Recupera l' attributo di tipo secure context dal tipo (ci può essere al massimmo un attributo) 
                    object[] secureContextAttributes = type.GetCustomAttributes(typeof(SecureContext), true);

                    foreach (SecureContext secureContext in secureContextAttributes)
                    {
                        //Verifica che la sicurezza sia abilitata per questo type
                        if (secureContext.Enabled)
                        {
                            //Prende tutti i metodi pubblici del tipo
                            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                            foreach (MethodInfo method in methods)
                            {
                                //Cerco gli attributi di tipo "Secure" su ogni metodo
                                Object[] secureAttributes = method.GetCustomAttributes(typeof(Secure), true);
                                //Ciclo su ogni attributo (ci può essere al massimmo un attributo)
                                foreach (Secure secure in secureAttributes)
                                {
                                    //Se è abilitata la sicurezza su quel metodo
                                    if (secure.Enabled)
                                    {
                                        //Scrivo il nome del metodo con firma e la classe di appartenenza
                                        string methodName = String.Format("{1} [{0}]", method.ReflectedType.ToString(), method.ToString());
                                        //Valorizzo tutti i campi dell'attributo
                                        if (secure.Alias == null)
                                            secure.Alias = methodName;

                                        secure.FullName = methodName;

                                        CheckSecureMethodAttributes(secure);

                                        //Inserisco l'attributo con chiave uguale al suo methodName
                                        //In modo che l'applicazione possa riconoscere in ogni momento i metodi sicuri
                                        _secureMethods.Add(methodName, secure);
                                        //_logDelegate(String.Format("Secure method found: {0}", methodName));
                                    }
                                }
                            }
                        }//if (secureContext.Enabled)
                    }//foreach (Attributes.SecureContext secureContext in secureContextAttributes)
                }
            }
            catch (Exception ex)
            {
                string inner = "";
                if (ex.InnerException != null)
                 inner = ex.InnerException.Message;
                throw new Exception ("Errore nella add Assembly: " + ex.Message + " ___ " + inner);
            }
           
        }

        private void CheckSecureMethodAttributes(Secure secure)
        {
            if (secure.MacroArea.Length == 0)
                throw new InvalidProgramException(String.Format("Macroarea not set for {0}", secure.FullName));

            if (secure.Area.Length == 0)
                throw new InvalidProgramException(String.Format("Area not set for {0}", secure.FullName));

            if (secure.Alias.Length == 0)
                throw new InvalidProgramException(String.Format("Macroarea not set for {0}", secure.FullName));
        }

        /// <summary>
        /// Verify the logon
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Logon(string username, string password)
        {
            _lastError = String.Empty;
            
            if(_secureDataAccess == null)
                throw new NullReferenceException("SecureDataAccess is not initialized");

            IUser user = _secureDataAccess.GetUser(username, password);
            if (user != null)
            {
                _currentUser = user;
                return true;
            }

            _lastError = "Nome utente o password errati";
            _currentUser = null;
            return false;
        }

        /// <summary>
        /// This method is called when is necessary to check the authorization. 
        /// Checks if the user is authorized to the calling method
        /// </summary>
        public void Check()
        {
            if (_currentUser.Username == "Admin")
                return;
            
            string methodName = GetCallingMethodName();
            //File.AppendAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "secure.txt"), methodName + ": accessing;" + Environment.NewLine, Encoding.Unicode);

            if (_currentUser == null)
                throw new Exceptions.UserNotLoggedException(String.Format("Utente non loggato ({0})", methodName));

            if (_secureMethods.ContainsKey(methodName))
                if (!HasCurrentUserPermission(methodName))
                {
                    throw (new Exceptions.AccessDeniedException(String.Format("L'utente {0} non può accedere {1}", _currentUser.Username, methodName)));
                }
                //else
                //{
                //    File.AppendAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "secure.txt"), methodName + ": accessed;" + Environment.NewLine, Encoding.Unicode);
                //}
                    

        }

        /// <summary>
        /// Gets the name of the calling method
        /// </summary>
        /// <returns></returns>
        private string GetCallingMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(2);
            MethodInfo callingMethod = (MethodInfo)stackFrame.GetMethod();

            string methodName = String.Format("{1} [{0}]", callingMethod.ReflectedType.ToString(), callingMethod.ToString());
            //_logDelegate("Checked method found: " + methodName);
            return methodName;
        }

        /// <summary>
        /// Returns true if the user is authorized for the current method
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private bool HasCurrentUserPermission(string functionName)
        {
            foreach (IProfile profile in _currentUser.Role.Profiles)
            {
                foreach (IPermission permission in profile.Permissions)
                {
                    if (permission.FullMethodName.Equals(functionName))
                        return true;
                }
            }

            return false;
        }

        
    }
}
