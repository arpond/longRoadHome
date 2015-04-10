using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.ac.dundee.arpond.longRoadHome.Model.Graph
{
    public class Arc
    {
        private Node node1;
        private Node node2;

        public Arc(Node node1, Node node2)
        {
            this.node1 = node1;
            this.node2 = node2;
        }

        public Node GetNode1()
        {
            return node1;
        }

        public Node GetNode2()
        {
            return node2;
        }
    }
}
