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
        List<Tuple<String, String>> validLoc = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validDummy = new List<Tuple<String, String>>();
       
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
            //connected = lm.ConnectDummyLocations(dlList);

            res = new Residential(1, 3, 5);
            com = new Commercial(2, 4, 7);
            civ = new Civic(3, 6, 3);

            validLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:True,Sublocations,CurrentSublocation", "Standard location is valid"));
            validLoc.Add(new Tuple<String, String>("Type:Location,ID:2,Connections:1:3:4,Visited:True,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1", "Standard location is valid"));
            validLoc.Add(new Tuple<String, String>("Type:Location,ID:3,Connections:1:2:5,Visited:True,Sublocations:" + res.ParseToString() + ":" + civ.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1", "Standard location is valid"));

            validDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:4,Connections:2:6", "Standard dummy location is valid"));
            validDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:5,Connections:3:6", "Standard dummy location is valid"));
            validDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:6,Connections:4:5:7", "Standard dummy location is valid"));
            validDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:7,Connections:6:8", "Standard dummy location is valid"));
            validDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:8,Connections:7", "Standard dummy location is valid"));
        }


        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_CreateListOfDummyLocations()
        {
            var unvisited = lm.CreateDummyLocations(100);

            for(int i = 0; i < unvisited.Count; i++)
            {
                DummyLocation temp = unvisited[i];
                Assert.AreEqual(i+1, temp.GetLocationID(), "List ID should match locationID");
            }

            lm = new LocationModel();
            unvisited = lm.CreateDummyLocations(101);
            Assert.AreEqual(104, unvisited.Count);
        }
   
        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_InitializeLM()
        {
            lm = new LocationModel();
            lm.InitializeLocationModel(1024);
            
            Assert.IsTrue(lm.GetCurentLocation().GetLocationID() == 0, "Start location should be 0");
            Assert.IsTrue(lm.GetVisited().Count == 1, "Only one visited location");
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_ParseToString()
        {
            lm = new LocationModel(1024);

            String visited = lm.ParseVisitedToString();
            String unvisited = lm.ParseUnvisitedToString();
            String currentLoc = lm.ParseCurrLocationToString();
            String currentSub = lm.ParseCurrSubLocToString();

            LocationModel temp = new LocationModel(visited, unvisited, currentLoc, currentSub);
            Assert.AreEqual(visited, temp.ParseVisitedToString(), "Visited strings should be the same");
            Assert.AreEqual(unvisited, temp.ParseUnvisitedToString(), "Unvisited strings should be the same");
            Assert.AreEqual(currentLoc, temp.ParseCurrLocationToString(), "Current Location strings should be the same");
            Assert.AreEqual(currentSub, temp.ParseCurrSubLocToString(), "Current Sublocation strings should be the same");

        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_ValidVisitedString()
        {
            String validString = LocationModel.VISITED_TAG;
            String invalid = "UnVis";
            String invalid2 = LocationModel.VISITED_TAG + "#invlaidLocation";
            foreach(var test in validLoc)
            {
                validString += "#" + test.Item1;
                invalid += "#" + test.Item1;
                invalid2 += "#" + test.Item1;
            }
            Assert.IsTrue(LocationModel.IsValidVisitedLocations(validString),"Valid list of locations should be valid");
            Assert.IsFalse(LocationModel.IsValidVisitedLocations(invalid), "Invalid header makes string invalid");
            Assert.IsFalse(LocationModel.IsValidVisitedLocations(invalid2), "Invalid location makes string invalid");
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_ValidUnvisitedString()
        {
            String validString = LocationModel.UNVISITED_TAG;
            String invalid = "InVis";
            String invalid2 = LocationModel.UNVISITED_TAG + "#invlaidLocation";
            foreach (var test in validDummy)
            {
                validString += "#" + test.Item1;
                invalid += "#" + test.Item1;
                invalid2 += "#" + test.Item1;
            }
            Assert.IsTrue(LocationModel.IsValidUnvisitedLocations(validString), "Valid list of dummy locations should be valid");
            Assert.IsFalse(LocationModel.IsValidUnvisitedLocations(invalid), "Invalid header makes stirng invalid");
            Assert.IsFalse(LocationModel.IsValidUnvisitedLocations(invalid2), "Invalid dummy location makes string invalid");
        }

        [TestCategory("Location"), TestCategory("LocationModel"), TestMethod()]
        public void LocationModel_MoveToLocation()
        {
            LocationModel temp = new LocationModel(1024);
            Location start = temp.GetCurentLocation();
            Location curr = temp.GetCurentLocation();
            Location expected;
            SortedList<int, DummyLocation> unvisited = temp.GetUnvisited();
            SortedList<int, Location> visited = temp.GetVisited();

            Assert.AreSame(start, curr, "Start and current should be the same");

            Assert.IsTrue(temp.LocationVisited(0), "Start should be visisted");
            Assert.IsFalse(temp.LocationVisited(1), "Location 1 should be unvisisted");


            //Assert.IsFalse(temp.MoveToVisitedLocation(1), "Move should be unsuccessful as location is unvisited");
            //Assert.IsTrue(temp.MoveToUnvisitedLocation(1), "Move should be successful");
            Assert.IsTrue(temp.MoveToLocation(1), "Move should be successful");
            curr = temp.GetCurentLocation();
            visited.TryGetValue(1, out expected);

            Assert.IsTrue(temp.LocationVisited(0), "Start should be visisted");
            Assert.IsTrue(temp.LocationVisited(1), "Location 1 should be visisted");

            Assert.AreEqual(1, curr.GetLocationID(), "Current location should now be 1");
            Assert.IsTrue(curr.GetVisited(), "Location should be visited");
            Assert.IsNull(temp.GetSubLocation(), "Sublocation should be null");
            Assert.AreNotSame(start, curr, "Start and current should not be the same");
            Assert.AreSame(expected, curr, "Should be at the expected location");
            Assert.IsFalse(temp.MoveToLocation(1), "Move should be unsuccessful");
            Assert.IsFalse(temp.MoveToLocation(-1), "Move should be unsuccessful");

            Assert.IsFalse(temp.MoveToLocation(20000), "Move should be unsuccessful");

            Assert.IsTrue(temp.MoveToLocation(0), "Move should be successful");
            //Assert.IsFalse(temp.MoveToUnvisitedLocation(0), "Move should be unsuccessful as location has been visited");
            //curr = temp.GetCurentLocation();
            //Assert.AreNotSame(start, curr, "Start and current should not be the same");
            //Assert.AreSame(expected, curr, "Should be at the expected location");

            //Assert.IsTrue(temp.MoveToVisitedLocation(0), "Move should be successful");
            curr = temp.GetCurentLocation();
            Assert.AreSame(start, curr, "Start and current should be the same");
        }
    }
}
