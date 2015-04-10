using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;

namespace uk.ac.dundee.arpond.longRoadHome.Model.Graph
{
    public class Node
    {
        private DummyLocation dl;
        private List<Arc> connectedTo;

        public Node(DummyLocation dl)
        {
            connectedTo = new List<Arc>();
            this.dl = dl;
        }

        public DummyLocation GetDL()
        {
            return dl;
        }

        public int GetID()
        {
            return dl.GetLocationID();
        }

        public void AddConnection(Arc connection)
        {
            connectedTo.Add(connection);
        }

        public int NumberOfArcs()
        {
            return connectedTo.Count;
        }

        public bool IsConnected(int id)
        {
            foreach(Arc arc in connectedTo)
            {
                Node temp1 = arc.GetNode1();
                Node temp2 = arc.GetNode2();
                if(temp1.GetID() == id || temp2.GetID() == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
