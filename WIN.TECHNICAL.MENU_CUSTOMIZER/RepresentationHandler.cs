using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.MENU_CUSTOMIZER
{
    public class RepresentationHandler
    {

        private MenuRepresentation _representation;

        public RepresentationHandler(MenuRepresentation representation)
        {
            if (representation == null)
                throw new ArgumentException ("Rappresentazione Nulla!");

            _representation = representation;
        }


        //public IList<Function> GetFunctionsByLevel(int level)
        //{
        //    List<Function> result = new List<Function>();

        //    foreach (Function item in _representation.Functions)
        //    {
        //        if (item.FunctionLevel.Equals(level))
        //            result.Add(item);
        //    }
        //    return result;
        //}

        /// <summary>
        /// Prende i nodi dello stesso livello facendo attenzione a non prende nodi che abbiano un 
        /// child in comune
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public IList<Node> GetNodes(int level, string parent)
        {
            List<Node> result = new List<Node>();

            foreach (Node item in TotalNodeListOrderedByLevel)
            {
                if (item.Level.Equals(level) && item.Parent.ToLower().Equals(parent.ToLower()))
                    if (!ExsistNodeWithChild(item.Child, result))
                        result.Add(item);
            }





            return result;
        }

        internal bool ExsistNodeWithChild(string childName, IList<Node> list)
        {
            if (childName == "")
                return false;
            foreach (Node item in list)
            {
                if (item.Child.ToLower().Equals(childName.ToLower()))
                    return true;
            }
            return false;
        }


        //public int GetMaxLevel
        //{
        //    get
        //    {
        //        IList<Node> t = TotalNodeListOrderedByLevel;
        //        if (t.Count == 0)
        //            return 0;
        //        //prendo l'ultimo poichè sono ordinati per livello
        //        return t[t.Count - 1].Level ;
        //    }
        //}


        public IList<Node> TotalNodeListOrderedByLevel
        {
            get
            {
                //Prendo i nodi per livello
                List<Node> result = new List<Node>();
                foreach (Function item in _representation.Functions)
                {
                    item.FillNodeList(result);
                }

                //Li ordino per livello
                result.Sort(Node.CompareByLevel);

                return result;
            }
        }



        



    }
}
