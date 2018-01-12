using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.TECHNICAL.MENU_CUSTOMIZER
{
    [Serializable]
    public class MenuRepresentation
    {

        private Function[] _functions = new Function[]{};

        [XmlArray("Funzioni"), XmlArrayItem("Funzione", typeof(Function))] 
        public Function[] Functions
        {
            get { return _functions; }
            set {_functions = value; }
        }

        public MenuRepresentation() {}

        public void AddFunction(Function function)
        {
            

            if (!ExsistFunction(function))
            {
                Array.Resize(ref _functions, _functions.Length + 1);
                _functions[_functions.Length - 1] = function;
            }

        }

        private bool ExsistFunction(Function function)
        {
            bool found;
            found = false;

            foreach (Function item in _functions)
            {
                if (item.Name.ToLower().Equals(function.Name.ToLower()))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        private Function FindFunction(string functionName)
        {
            
            foreach (Function item in _functions)
            {
                if (item.Name.ToLower().Equals(functionName.ToLower ()))
                {
                    return item;
                }
            }
            return null;
        }



        public MenuRepresentation CreateSubMenu(IList<string> enabledFunctions)
        {
            MenuRepresentation r = new MenuRepresentation();

            foreach (string item in enabledFunctions)
            {
                Function f = FindFunction(item);
                if (f != null)
                {
                    r.AddFunction(f);
                }
            }
            return r;
        }



        
    }
}
