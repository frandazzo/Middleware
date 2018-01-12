using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.TECHNICAL.MENU_CUSTOMIZER
{
    [Serializable]
    public class Node
    {

        public Node() { }


        private string _text = "";

        [XmlAttribute("TestoMenu")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        private string _parent = "";

        [XmlAttribute("Padre")]
        public string Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private string _child = "";

        [XmlAttribute("Figlio")]
        public string Child
        {
            get { return _child; }
            set { _child = value; }
        }



        private string _url = "";

        [XmlAttribute("Url")]
        public string Url
        {
            get { return _url ; }
            set { _url  = value; }
        }


        private string _urlImage = "";

        [XmlAttribute("UrlImage")]
        public string UrlImage
        {
            get { return _urlImage; }
            set { _urlImage = value; }
        }


        private int _index = -1;
        private int _level = -1;

        [XmlAttribute("Livello")]
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        [XmlAttribute("Indice")]
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public override string ToString()
        {
            return _text;
        }

        public string Position
        {
            get { return _level.ToString() + "-" + _index.ToString(); }
        }


        public static  int CompareByLevel(Node x, Node y)
        {
            if (x.Level  == -1)
            {
                if (y.Level == -1)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y.Level == -1)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    return x.Level.CompareTo(y.Level);
                    
                }
            }



        }
  }
}
