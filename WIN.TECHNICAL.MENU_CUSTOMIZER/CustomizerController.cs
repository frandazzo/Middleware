using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.MENU_CUSTOMIZER
{
    public class CustomizerController
    {

        private ITemplateMenuProvider _applicationMenuProvider;

        private MenuRepresentation _representation;

        private static CustomizerController _instance;

        private CustomizerController() { }


        public static CustomizerController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CustomizerController();
                return _instance;
            }
        }
        public static CustomizerController NewInstance
        {
            get
            {
                   return new CustomizerController(); 
            }
        }

        public void Initialize(ITemplateMenuProvider applicationMenuProvider)
        {
            if (applicationMenuProvider == null)
                throw new ArgumentException("Il provider dei menù non può essere nullo");

            _applicationMenuProvider = applicationMenuProvider;
        }


        public void  CreateAndConstructSubMenuRepresentation(IList <string> enabledFunctions, IMenuWidgetConstructor constructor)
        {

            if (_applicationMenuProvider == null)
                throw new ArgumentException("Il provider dei menù non può essere nullo; Inizializzare la classe facade!");


            //Creo la sotto rappresentazione del menu
            if (_representation == null)
                _representation  = _applicationMenuProvider.CreateApplicationMenuRepresentation ();


            
            //Costruisco fisicamente il menu
            if (constructor != null)
                constructor.ConstructMenu(_representation.CreateSubMenu(enabledFunctions));



        }

        public void CreateAndConstructSubMenuRepresentation(IMenuWidgetConstructor constructor)
        {

            if (_applicationMenuProvider == null)
                throw new ArgumentException("Il provider dei menù non può essere nullo; Inizializzare la classe facade!");


            //Creo la sotto rappresentazione del menu
            if (_representation == null)
                _representation = _applicationMenuProvider.CreateApplicationMenuRepresentation();



            //Costruisco fisicamente il menu
            if (constructor != null)
                constructor.ConstructMenu(_representation );



        }


    }
}
