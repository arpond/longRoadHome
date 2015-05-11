using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using System.Collections.Generic;

namespace UnitTests_LongRoadHome.LocationTests
{
    [TestClass]
    public class TLocationModel
    {
        LocationModel lm;
       
        [TestInitialize]
        public void Setup()
        {
                
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_GenerateDummyLocations()
        {
            lm = new LocationModel();
            lm.GenerateDummyLocations(200, 40, 5);

            SortedList<int, DummyLocation> generated = lm.GetUnvisited();
            IList<DummyLocation> dls = generated.Values;
            for (int i = 0; i < dls.Count; i++ )
            {
                DummyLocation dl = dls[i];
                HashSet<int> connections = dl.GetConnections();
                Assert.IsTrue(connections.Count >= 2,"Every location should have at least 2 connections");
                Assert.IsTrue(connections.Count <= 4,"Every location should have at most 5 connections");
                foreach(int connected in connections)
                {
                    Assert.IsTrue(dl.IsConnected(dls[connected - 1]), "All connections should be undirected");
                }
            }

            String graph = LocationsToGraphviz(generated.Values);
            DateTime today = DateTime.Now;
            System.IO.StreamWriter file = new System.IO.StreamWriter("E:\\Graphs\\generated.txt");
            file.Write(graph);
            file.Close();
        }

        private String LocationsToGraphML(List<DummyLocation> list )
        {
            String graph = "<graph id=\"G\" edgedefault=\"undirected\">";
            foreach(DummyLocation dl in list)
            {
                graph += "<node id=\"n" + dl.GetLocationID() + "\"/>";
                HashSet<int> connections = dl.GetConnections();
                foreach(int con in connections)
                {
                    graph += "<edge source=\"n" + dl.GetLocationID() + "\" target= \"n" + con + "\"/>";
                }
            }
            graph += "</graph>";
            return graph;
        }

        private String LocationsToGraphviz(IList<DummyLocation> list)
        {
            String graph = "graph{\n\trankdir=LR;";
            foreach (DummyLocation dl in list)
            {
                graph += "\n\tDL" + dl.GetLocationID() + " -- {";
                HashSet<int> connections = dl.GetConnections();
                foreach(int con in connections)
                {
                    graph += " DL" + con;
                }
                graph += "};";
            }
            graph += "}";
            return graph;
        }
    }
}
