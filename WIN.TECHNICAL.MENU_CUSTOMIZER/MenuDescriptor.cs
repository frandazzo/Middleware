using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.TECHNICAL.MENU_CUSTOMIZER
{

    [XmlRootAttribute("DescrittoreMenu", Namespace = "www.fenealgestweb.it", IsNullable = false)]
    public class MenuDescriptor
    {


        public MenuDescriptor() { }


        public MenuDescriptor(MenuRepresentation[] representatios)
        {
            _menuRepresentations = representatios;
        }


        private MenuRepresentation[] _menuRepresentations;

        [XmlArray("ListaMenu"), XmlArrayItem("Menu", typeof(MenuRepresentation))] 
        public MenuRepresentation[] MenuRepresentations
        {
            get { return _menuRepresentations; }
            set { _menuRepresentations = value; }
        }

    }
}
