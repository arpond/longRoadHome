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
        DummyLocation dl1, dl2, dl3, dl4, a, b, c, d;
        List<DummyLocation> dlList;
        List<DummyLocation> connected;
        Random rnd = new Random();
       
        [TestInitialize]
        public void Setup()
        {
            Residential res;
            Commercial com;
            Civic civ;

            lm = new LocationModel();
            dl1 = new DummyLocation(1);
            dl2 = new DummyLocation(2);
            dl3 = new DummyLocation(3);
            dl4 = new DummyLocation(4);

            dlList = new List<DummyLocation>();
            dlList.Add(dl1);
            dlList.Add(dl2);
            dlList.Add(dl3);
            dlList.Add(dl4);
            connected = lm.ConnectDummyLocations(dlList);
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_ConnectListOfDummyLocations()
        {
            Assert.IsTrue(connected.Count == dlList.Count, "List should contain " + dlList.Count + " elements");
            var list = connected;
            for (int i = 0; i < connected.Count; i++)
            {
                DummyLocation temp = list[i];
                for (int j = 0; j < list.Count; j++)
                {
                    TestNotConnected(temp, temp);
                    DummyLocation temp2 = list[j];
                    if (i != j)
                    {
                        TestConnected(temp, temp2);
                    }
                }
            }
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_CreateListOfDummyLocations()
        {
            lm.CreateDummyLocations(100);
            var unvisted = lm.GetUnvisited();

            for(int i = 1; i < 101; i++)
            {
                DummyLocation temp;
                if(unvisted.TryGetValue(i, out temp))
                {
                    Assert.AreEqual(i, temp.GetLocationID(), "List ID should match locationID");
                }
                else
                {
                    Assert.Fail("Dummy location should be in the list");
                }
            }

            lm = new LocationModel();
            lm.CreateDummyLocations(101);
            unvisted = lm.GetUnvisited();

            Assert.AreEqual(104, unvisted.Count);
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_ConnectDummyLocationsInGroupsOfFour()
        {
            lm = new LocationModel();
            lm.CreateDummyLocations(100);
            lm.ConnectUnvisitedIntoGroupsOfFour();
            var unvisted = lm.GetUnvisited();

            for(int i = 1; i< 101; i = i+4)
            {
                List<DummyLocation> dlList = new List<DummyLocation>();
                if (unvisted.TryGetValue(i, out a) && unvisted.TryGetValue(i+1, out b) && unvisted.TryGetValue(i+2, out c) && unvisted.TryGetValue(i+3, out d))
                {
                    dlList.Add(a);
                    dlList.Add(b);
                    dlList.Add(c);
                    dlList.Add(d);
                    TestConnectionValidity(dlList, i);
                }
                else
                {
                    Assert.Fail("There should be 4 nodes to get");
                }
            }
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_GroupByConnectionNumber()
        {
            List<DummyLocation> temp = new List<DummyLocation>();
            DummyLocation a, b, c, d, e, f, g, h;
            a = new DummyLocation(1, new HashSet<int> { 2, 3, 4 });
            b = new DummyLocation(2, new HashSet<int> { 1 });
            c = new DummyLocation(3, new HashSet<int> { 1 , 5 });
            d = new DummyLocation(4, new HashSet<int> { 1 , 6 , 7});
            e = new DummyLocation(5, new HashSet<int> { 3 , 6 });
            f = new DummyLocation(6, new HashSet<int> { 5 , 4 });
            g = new DummyLocation(7, new HashSet<int> { 4 , 8});
            h = new DummyLocation(8, new HashSet<int> { 7 });

            temp.Add(a);
            temp.Add(b);
            temp.Add(c);
            temp.Add(d);
            temp.Add(e);
            temp.Add(f);
            temp.Add(g);
            temp.Add(h);

            var oneConnection = lm.GroupByConnectionNumber(1, temp);
            var twoConnections = lm.GroupByConnectionNumber(2, temp);
            var threeConnections = lm.GroupByConnectionNumber(3, temp);
            var fourConnections = lm.GroupByConnectionNumber(4, temp);

            Assert.AreEqual(2, oneConnection.Count, "Should be two items with one connection");
            Assert.IsTrue(oneConnection.Contains(b), "Should contain b");
            Assert.IsTrue(oneConnection.Contains(h), "Should contain h");
            Assert.AreEqual(4, twoConnections.Count, "Should be four items with two connection");
            Assert.IsTrue(twoConnections.Contains(c), "Should contain c");
            Assert.IsTrue(twoConnections.Contains(e), "Should contain e");
            Assert.IsTrue(twoConnections.Contains(f), "Should contain f");
            Assert.IsTrue(twoConnections.Contains(g), "Should contain g");
            Assert.AreEqual(2, threeConnections.Count, "Should be two items with three connection");
            Assert.IsTrue(threeConnections.Contains(a), "Should contain a");
            Assert.IsTrue(threeConnections.Contains(d), "Should contain d");
            Assert.AreEqual(0, fourConnections.Count, "Should be no items with four connections");
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_ConnectGroupsOfFour()
        {
            lm = new LocationModel();
            lm.CreateDummyLocations(100);
            lm.ConnectUnvisitedIntoGroupsOfFour();
            var unvisited = lm.GetUnvisited();
            
            List<DummyLocation> fullList = new List<DummyLocation>();
            for (int i = 1; i < 17; i++)
            {
                DummyLocation tempDL;
                if (unvisited.TryGetValue(i, out tempDL))
                {
                    fullList.Add(tempDL);
                }
            }

            Assert.IsTrue(lm.ConnectByGroupsOfFour(fullList), "Connection should be succesfull");
            TestConnectionValidity(fullList, 1);
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_ConnectAllGroupsStepwise()
        {
            lm = new LocationModel();
            lm.CreateDummyLocations(256);
            lm.ConnectUnvisitedIntoGroupsOfFour();
            var unvisited = lm.GetUnvisited();
            List<DummyLocation> dls = new List<DummyLocation>(unvisited.Values);

            String graph = LocationsToGraphviz(unvisited.Values);
            System.IO.StreamWriter file = new System.IO.StreamWriter("E:\\Graphs\\initial.txt");
            file.Write(graph);
            file.Close();

            int groupSize = 16;
            int powerOfFour = Convert.ToInt32(Math.Ceiling(Math.Log(unvisited.Count) / Math.Log(4)));

            for (int i = 2; i <= powerOfFour; i++ )
            {
                groupSize = Convert.ToInt32(Math.Pow(4, i));
                int numOfReps = unvisited.Count / groupSize;

                for (int j = 0; j < numOfReps; j++)
                {
                    int start = j * groupSize;
                    int count = groupSize;
                    if (start + groupSize > unvisited.Count)
                    {
                        count = unvisited.Count - start;
                    }
                    List<DummyLocation> group = dls.GetRange(start, count);
                    Assert.IsTrue(lm.ConnectByGroupsOfFour(group), "Connection should be succesfull");
                    TestConnectionValidity(group, start + 1);
                }

                graph = LocationsToGraphviz(unvisited.Values);
                file = new System.IO.StreamWriter("E:\\Graphs\\Pass" + i + ".txt");
                file.Write(graph);
                file.Close();
            }

            graph = LocationsToGraphviz(unvisited.Values);
            file = new System.IO.StreamWriter("E:\\Graphs\\Final.txt");
            file.Write(graph);
            file.Close();
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_ConnectAllGroups()
        {
            lm = new LocationModel();
            lm.CreateDummyLocations(1024);
            lm.ConnectUnvisitedIntoGroupsOfFour();
            lm.GenerateConnections();
            var unvisited = lm.GetUnvisited();
            List<DummyLocation> dls = new List<DummyLocation>(unvisited.Values);
            TestConnectionValidity(dls, 1);

            String graph = LocationsToGraphviz(unvisited.Values);
            System.IO.StreamWriter file = new System.IO.StreamWriter("E:\\Graphs\\Final.txt");
            file.Write(graph);
            file.Close();
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_InitializeLM()
        {
            lm = new LocationModel();
            lm.InitializeLocationModel(1024);
            
            Assert.IsTrue(lm.GetCurentLocation().GetLocationID() == -1, "Start location should be -1");
            Assert.IsTrue(lm.GetVisited().Count == 1, "Only one visited location");
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_RemoveConnection()
        {
            DummyLocation temp1 = new DummyLocation(1, new HashSet<int> { 2 });
            DummyLocation temp2 = new DummyLocation(2, new HashSet<int> { 1 });
            TestConnected(temp1, temp2);

            lm.RemoveConnection(temp1, temp2);
            TestNotConnected(temp1, temp2);
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_RemoveOneExterior()
        {
            resetNodes();
            lm.RemoveOneExterior(a,b,c,d, 76);
            TestConnectionValidity();
            TestNotConnected(a, b);

            resetNodes();
            lm.RemoveOneExterior(a, b, c, d, 51);
            TestConnectionValidity();
            TestNotConnected(a, c);

            resetNodes();
            lm.RemoveOneExterior(a, b, c, d, 26);
            TestConnectionValidity();
            TestNotConnected(b, d);

            resetNodes();
            lm.RemoveOneExterior(a, b, c, d, 20);
            TestConnectionValidity();
            TestNotConnected(c, d);
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_RemoveTwoExterior()
        {
            resetNodes();
            lm.RemoveTwoExterior(a, b, c, d, 51, 51);
            TestConnectionValidity();
            TestNotConnected(a, b);
            TestNotConnected(c, d);

            resetNodes();
            lm.RemoveTwoExterior(a, b, c, d, 25, 51);
            TestConnectionValidity();
            TestNotConnected(a, c);
            TestNotConnected(c, d);

            resetNodes();
            lm.RemoveTwoExterior(a, b, c, d, 51, 25);
            TestConnectionValidity();
            TestNotConnected(a, b);
            TestNotConnected(b, d);

            resetNodes();
            lm.RemoveTwoExterior(a, b, c, d, 25, 25);
            TestConnectionValidity();
            TestNotConnected(a, c);
            TestNotConnected(b, d);
        }


        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_RemoveOneDiagonals()
        {
            resetNodes();
            lm.RemoveOneDiagonal(a, b, c, d, 81, 51, 51);
            TestConnectionValidity();
            TestNotConnected(a, d);
            TestNotConnected(a, b);
            TestNotConnected(c, d);

            resetNodes();
            lm.RemoveOneDiagonal(a, b, c, d, 34, 51, 51);
            TestConnectionValidity();
            TestNotConnected(a, d);
            TestNotConnected(a, c);

        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_RemoveTwoDiagonals()
        {
            resetNodes();
            lm.RemoveTwoDiagonals(a, b, c, d, 90, 76);
            TestConnectionValidity();
            TestNotConnected(a, d);
            TestNotConnected(b, c);
            TestNotConnected(a, b);

            resetNodes();
            lm.RemoveTwoDiagonals(a, b, c, d, 90, 51);
            TestConnectionValidity();
            TestNotConnected(a, d);
            TestNotConnected(b, c);
            TestNotConnected(a, c);

            resetNodes();
            lm.RemoveTwoDiagonals(a, b, c, d, 90, 26);
            TestConnectionValidity();
            TestNotConnected(a, d);
            TestNotConnected(b, c);
            TestNotConnected(b, d);

            resetNodes();
            lm.RemoveTwoDiagonals(a, b, c, d, 90, 1);
            TestConnectionValidity();
            TestNotConnected(a, d);
            TestNotConnected(b, c);
            TestNotConnected(c, d);

            resetNodes();
            lm.RemoveTwoDiagonals(a, b, c, d, 25, 100);
            TestConnectionValidity();
            TestNotConnected(a, d);
            TestNotConnected(b, c);

        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_RemoveConnections()
        {
            for (int i = 0; i < 100000; i++)
            {
                resetNodes();
                lm.RemoveConnections(a, b, c, d, rnd.Next(3), rnd.Next(1, 101), rnd.Next(1, 101), rnd.Next(1, 101));
                TestConnectionValidity();
            }
                
        }

        private void resetNodes()
        {
            connected = lm.ConnectDummyLocations(dlList);
            var list = connected;
            a = list[0];
            b = list[1];
            c = list[list.Count - 2];
            d = list[list.Count - 1];
        }


        private void TestConnected(DummyLocation temp1, DummyLocation temp2)
        {
            Assert.IsTrue(temp1.IsConnected(temp2), temp1.GetLocationID() + " should be connected to " + temp2.GetLocationID());
            Assert.IsTrue(temp2.IsConnected(temp1), temp2.GetLocationID() + " should be connected to " + temp1.GetLocationID());
        }

        private void TestNotConnected(DummyLocation temp1, DummyLocation temp2)
        {
            Assert.IsFalse(temp1.IsConnected(temp2), temp1.GetLocationID() + " should not be connected to " + temp2.GetLocationID());
            Assert.IsFalse(temp2.IsConnected(temp1), temp2.GetLocationID() + " should not be connected to " + temp1.GetLocationID());
        }

        private void TestConnectionValidity()
        {
            var list = connected;
            foreach (DummyLocation dl in list)
            {
                HashSet<int> connections = dl.GetConnections();
                Assert.IsTrue(connections.Count > 0, "There should be at least one connection on dummy location " + dl.GetLocationID());
                Assert.IsTrue(connections.IsSubsetOf(new HashSet<int> { 1, 2, 3, 4 }), "The connections should be only to nodes available");
            }
        }

        private void TestConnectionValidity(List<DummyLocation> dlList, int index)
        {
            var superSet = new HashSet<int>();
            for (int i = 0; i < dlList.Count; i++)
            {
                superSet.Add(index + i);
            }

            foreach (DummyLocation dl in dlList)
            {
                HashSet<int> connections = dl.GetConnections();
                Assert.IsTrue(connections.Count > 0, "There should be at least one connection on dummy location " + dl.GetLocationID());
                Assert.IsTrue(connections.IsSubsetOf(superSet), "The connections should be only to nodes available. SuperSet: " + superSet.ToString() + " subset: " + connections.ToString());
            }
        }

        //[TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        //public void LocationModel_InitializeLM()
        //{
        //    lm.InitializeLocationModel(200, 40, 5);
        //    SortedList<int, Location> visited = lm.GetVisited();
        //    SortedList<int, DummyLocation> unvisited = lm.GetUnvisited();

        //    Assert.AreEqual(visited.Count, 1, "There should only be one location visited initially");
        //    Assert.IsTrue(visited.ContainsKey(-1), "The location should be the start location");

        //    Location start;
        //    DummyLocation dl1;

        //    if (visited.TryGetValue(-1, out start) && unvisited.TryGetValue(1, out dl1))
        //    {
        //        Assert.IsTrue(start.IsConnected(dl1), "Start should be connected to dl1");
        //        Assert.IsTrue(dl1.IsConnected(start), "dl1 should be connected to start");
        //    }

        //}

        //[TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        //public void LocationModel_GenerateDummyLocations()
        //{
        //    lm.GenerateDummyLocations(200, 40, 5);

        //    SortedList<int, DummyLocation> generated = lm.GetUnvisited();
        //    IList<DummyLocation> dls = generated.Values;
        //    for (int i = 0; i < dls.Count; i++ )
        //    {
        //        DummyLocation dl = dls[i];
        //        HashSet<int> connections = dl.GetConnections();
        //        Assert.IsTrue(connections.Count >= 1,"Every location should have at least 1 connections");
        //        Assert.IsTrue(connections.Count <= 4,"Every location should have at most 4 connections");
        //        foreach(int connected in connections)
        //        {
        //            Assert.IsTrue(dl.IsConnected(dls[connected - 1]), "All connections should be undirected");
        //        }
        //    }

        //    String graph = LocationsToGraphviz(generated.Values);
        //    DateTime today = DateTime.Now;
        //    System.IO.StreamWriter file = new System.IO.StreamWriter("E:\\Graphs\\generated.txt");
        //    file.Write(graph);
        //    file.Close();
        //}

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
