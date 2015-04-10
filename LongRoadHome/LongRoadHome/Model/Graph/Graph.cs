using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.ac.dundee.arpond.longRoadHome.Model.Graph
{
    public class Graph
    {
        int maxConnections = 4, minConnections = 2;
        SortedList<int, Node> nodes;
        Random rnd = new Random();

        public Graph()
        {
            nodes = new SortedList<int, Node>();
        }

        public void addNode(Node node)
        {
            if (nodes.Count == 0)
            {
                nodes.Add(node.GetID(), node);
            }
            else if (nodes.Count == 1) 
            {
                nodes.Add(node.GetID(), node);
                addArc(nodes[0], node);
            }
            else if (nodes.Count == 2)
            {
                nodes.Add(node.GetID(), node);
                addArc(nodes[0], node);
                addArc(nodes[1], node);
            }
            else
            {
              
                Node toConnect = null;

                for (int i = 0; i < 4 && node.NumberOfArcs() < maxConnections; i++)
                {
                    toConnect = RandomlySelectNode(node);
                    if (toConnect == null)
                    {
                        break;
                    }
                    ChanceToAddArc(node.NumberOfArcs(), toConnect, node);
                }

                nodes.Add(node.GetID(), node);
            }
        }

        private Node RandomlySelectNode(Node node)
        {
            Node toConnect = null;
            int toConnectIndex, count = 0, currentID = node.GetID();
            do
            {
                count++;
                if (currentID - 10 < 0)
                {
                    toConnectIndex = rnd.Next(nodes.Count);
                }
                else
                {
                    toConnectIndex = rnd.Next(currentID - 10, currentID);
                }
                if (count == 40)
                {
                    for (int j = currentID - 1; j > 0 && j > currentID - 10; j--)
                    {
                        if (nodes.TryGetValue(j, out toConnect) && toConnect.NumberOfArcs() != maxConnections)
                        {
                            toConnectIndex = j;
                            count = 0;
                            break;
                        }
                        toConnectIndex = -1;
                    }
                }
            } while (toConnectIndex != -1 && (toConnectIndex < 0 || !nodes.TryGetValue(toConnectIndex, out toConnect) || toConnect.NumberOfArcs() == maxConnections || node.IsConnected(toConnectIndex)));
            return toConnect;
        }


        private void ChanceToAddArc(int numConnections, Node toConnect, Node newNode)
        {
            int p = (int)((1.0f - numConnections / (float)maxConnections) * 100.0f);

            if (numConnections < minConnections || numConnections >= minConnections && rnd.Next(1, 100) < p)
            {
                addArc(toConnect, newNode);
            }
        }

        public void addArc(Node node1, Node node2)
        {
            Arc arc1 = new Arc(node1, node2);
            Arc arc2 = new Arc(node2, node1);
            node1.AddConnection(arc1);
            node2.AddConnection(arc2); 
        }
    }
}
