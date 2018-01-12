using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.MENU_CUSTOMIZER
{
    public class InvalidNodeException : Exception 
    {

        private string _message;

        public InvalidNodeException(string message)  
        {
            _message = message;
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
