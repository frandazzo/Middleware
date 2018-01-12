using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace WIN.TECHNICAL.MENU_CUSTOMIZER
{
    [Serializable]
    public class Function
    {


        public Function() { }

        private string _name = "";


        [XmlAttribute("Nome")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private Node[] _nodes = new Node[]{};

        [XmlArray("Nodi"), XmlArrayItem("Nodo", typeof(Node))] 
        public Node[] Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
        }

        public void AddNode(Node node)
        {
            bool canAdd = true;
            string errorMessage = "";



            if (ExsistNodeByPosition(node))
            {
                canAdd = false;
                errorMessage = "Posizione nodo esistente!";
            }

            if (!AreNodesContiguos())
            {
                canAdd = false;
                errorMessage = "Nodi con livelli non contigui!";
            }




            if (canAdd)
                AddNodeToArray(node);
            else
                throw new InvalidNodeException(errorMessage);

        }

        internal bool AreNodesContiguos()
        {
            //Verifico la contiguità dei nodi

            //prendo il numero di nodi esistenti
            int totalNodes = _nodes.Length;

            if (totalNodes == 0)
                return true;

            //faccio partire una funzine ricorsiva che verifica 
            //uno per uno la presenza di tutti i livelli
            //a partire da zero fino al numero di nodi - 1
            return CheckContiguity(0);

        }


        internal bool CheckContiguity(int nodeToCheck)
        {
            if (nodeToCheck  == _nodes.Length)
                return true;

            foreach (Node item in _nodes)
            {
                if (item.Level == nodeToCheck)
                    return CheckContiguity(nodeToCheck + 1);
            }

            return false;


        }




        private void AddNodeToArray(Node node)
        {
            Array.Resize(ref _nodes, _nodes.Length + 1);
            _nodes[_nodes.Length - 1] = node;
        }

        private bool ExsistNodeByPosition(Node node)
        {
            bool found;
            found = false;

            foreach (Node item in _nodes)
            {
                if (item.Position.Equals(node.Position))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }


        internal void FillNodeList(IList<Node> nodes)
        {

            foreach (Node item in _nodes)
            {
                nodes.Add(item);
            }
        }


        internal int FunctionLevel
        {
            get
            {
                int lev = -1;
                foreach (Node item in _nodes)
                {
                    if (item.Level > lev)
                        lev = item.Level;
                }
                return lev;
            }
        }


    }
}
