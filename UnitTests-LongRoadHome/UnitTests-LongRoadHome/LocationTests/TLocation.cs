using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using System.Collections.Generic;

namespace UnitTests_LongRoadHome.LocationTests
{
    [TestClass]
    public class TLocation
    {
        DummyLocation dl;
        Location l;
        List<Tuple<String, String>> validDummy = new List<Tuple<String, String>>();
        List<Tuple<String, String>> invalidDummy = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validLoc = new List<Tuple<String, String>>();
        List<Tuple<String, String>> invalidLoc = new List<Tuple<String, String>>();
        Random rnd = new Random();

        [TestInitialize]
        public void Setup()
        {
            //System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(Civic).TypeHandle);
            //System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(Residential).TypeHandle);
            //System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(Commercial).TypeHandle);
            validDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:2:3", "Standard dummy location is valid"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:1","Should be at least 3 components"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections,Connections","At most 3 components"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,blah:1,Connections", "Components should be valid"));
            invalidDummy.Add(new Tuple<String, String>("Type:sadas,ID:,Connections,", "Type should be DummyLocaiton"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:sadas,Connections", "ID should be an int"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:-1,Connections", "ID should be positive"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:", "If there are connections there should be at least one"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:1", "Can't connect to self"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:2:2", "Can't connect to locaiton more than once"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:2:3:4:-1", "Connections should all be positive"));
            invalidDummy.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:4:2::3", "Each Connection should have a value"));

            Residential res = new Residential(1, 3, 5);
            Commercial com = new Commercial(2, 4, 7);
            Civic civ = new Civic(3, 6, 3);

            validLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations,CurrentSublocation", "Standard location is valid"));
            validLoc.Add(new Tuple<String, String>("Type:Location,ID:2,Connections:1:3,Visited:False,Sublocations:" + res.ParseToString() + ":" + com.ParseToString() + ",CurrentSublocation:1" , "Standard location is valid"));
            //invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations,CurrentSublocation", "Standard location is valid"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations", "Should be at least 6 components"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations,CurrentSublocation,CurrentSublocation", "Should be at most 6 components"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Invalid,CurrentSublocation", "Components should be valid"));
            invalidLoc.Add(new Tuple<String, String>("Type:BlaLocation,ID:1,Connections:2:3,Visited:False,Sublocations,CurrentSublocation", "Type should be Location"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:megh,Connections:2:3,Visited:False,Sublocations,CurrentSublocation", "ID should be an int"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:-1,Connections:2:3,Visited:False,Sublocations,CurrentSublocation", "ID should be positive"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:,Visited:False,Sublocations,CurrentSublocation", "If there are connections there should be at least one"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:1,Visited:False,Sublocations,CurrentSublocation", "Can't connect to self"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3:2,Visited:False,Sublocations,CurrentSublocation", "Can't connect to location more than once"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3:-1,Visited:False,Sublocations,CurrentSublocation", "Connections should all be positive"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3::6,Visited:False,Sublocations,CurrentSublocation", "Each Connection should have a value"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited,Sublocations,CurrentSublocation", "Visited should have a value"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:True:False,Sublocations,CurrentSublocation", "Visited should a single value"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:4,Sublocations,CurrentSublocation", "Visited should be a bool"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:blah,Sublocations,CurrentSublocation", "Visited should be a bool"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations:,CurrentSublocation", "If there are sublocations there should be at least one"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations:Residential:1:False:2:blah:temp,CurrentSublocation", "Sublocation should be valid"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations:Residential:1:False,CurrentSublocation", "Sublocation should be complete"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations:Residential:1:False:2:3:temp:Civic:1:False:2:3:temp,CurrentSublocation", "Sublocations can not share IDs"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations,CurrentSublocation:", "if there is a Current Sublocation it should have a value"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations:Residential:1:False:2:3:temp,CurrentSublocation:one", "Current Sublocation should be an int"));
            invalidLoc.Add(new Tuple<String, String>("Type:Location,ID:1,Connections:2:3,Visited:False,Sublocations:Residential:1:False:2:3:temp,CurrentSublocation:2", "Current Sublocation should be defined in sublocations"));
        }

        [TestCategory("Location"), TestCategory("DummyLocation"), TestMethod()]
        public void DummyLocation_ParseToString()
        {
            String expected = "Type:DummyLocation,ID:1,Connections:2:3";
            dl = new DummyLocation(expected);
            Assert.AreEqual(expected, dl.ParseToString(), "String should be parsed correctly");
        }

        [TestCategory("Location"), TestCategory("DummyLocation"), TestMethod()]
        public void DummyLocation_CheckStringIsValid()
        {
             foreach(Tuple<String,String> test in validDummy)
            {
                Assert.IsTrue(DummyLocation.IsValidDummyLocation(test.Item1),test.Item2);
            }

            foreach (Tuple<String,String> test in invalidDummy)
            {
                Assert.IsFalse(DummyLocation.IsValidDummyLocation(test.Item1), test.Item2);
            }   
        }

        [TestCategory("Location"), TestCategory("LocationClass"), TestMethod()]
        public void Location_StandardConstructor()
        {
            l = new Location();

            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            Assert.AreNotEqual(null, l.GetSublocationByID(1), "There should be Sublocation 1");
            Assert.AreNotEqual(null, l.GetSublocationByID(2), "There should be Sublocation 2");
            Assert.AreNotEqual(null, l.GetSublocationByID(3), "There should be Sublocation 3");

            Assert.AreEqual(null, l.GetSublocationByID(4), "There should be no Sublocation 4");
            Assert.AreEqual(null, l.GetSublocationByID(5), "There should be no Sublocation 5");

            Assert.IsInstanceOfType(l.GetSublocationByID(1), typeof(Sublocation), "1 should be a sublocation");
            Assert.IsInstanceOfType(l.GetSublocationByID(2), typeof(Sublocation), "2 should be a sublocation");
            Assert.IsInstanceOfType(l.GetSublocationByID(3), typeof(Sublocation), "3 should be a sublocation");
        }

        [TestCategory("Location"), TestCategory("LocationClass"), TestMethod()]
        public void Location_SublocationSetGet()
        {
            Sublocation sl1, sl2, sl3;
            l = new Location();

            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            Assert.IsTrue(l.SetCurrentSubLocation(1), "Setting to an id which exists (1) should be succesful");
            Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
            sl1 = l.GetCurrentSubLocation();
            Assert.AreEqual(1, sl1.GetSublocationID(), "IDs should match");
            
            Assert.IsTrue(l.SetCurrentSubLocation(2), "Setting to an id which exists (2) should be succesful");
            Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
            sl2 = l.GetCurrentSubLocation();
            Assert.AreEqual(2, sl2.GetSublocationID(), "IDs should match");
            
            Assert.IsTrue(l.SetCurrentSubLocation(3), "Setting to an id which exists (3) should be succesful");
            Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
            sl3 = l.GetCurrentSubLocation();
            Assert.AreEqual(3, sl3.GetSublocationID(), "IDs should match");

            Assert.IsFalse(l.SetCurrentSubLocation(4), "Setting to an id which does not exists (4) should be unsuccesful");
            Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
            Assert.AreEqual(sl3, l.GetCurrentSubLocation(), "Current Sublocation should have remained the same");

            Assert.IsTrue(l.SetCurrentSubLocation(1), "Setting to an id which exists (1) should be succesful");
            Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
            Assert.AreEqual(sl1, l.GetCurrentSubLocation(), "Current Sublocation should be the same as when set to 1 before");

            Assert.IsTrue(l.SetCurrentSubLocation(3), "Setting to an id which exists (3) should be succesful");
            Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
            Assert.AreEqual(sl3, l.GetCurrentSubLocation(), "Current Sublocation should be the same as when set to 3 before");

            Assert.IsTrue(l.SetCurrentSubLocation(2), "Setting to an id which exists (2) should be succesful");
            Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
            Assert.AreEqual(sl2, l.GetCurrentSubLocation(), "Current Sublocation should be the same as when set to 1 before");
        }

        [TestCategory("Location"), TestCategory("LocationClass"), TestMethod()]
        public void Location_SizeConstructor()
        {
            int testSize = 6;
            l = new Location(testSize);
            Sublocation last = new Residential();

            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            Assert.AreEqual(testSize, l.GetSize(), "Size should be "+ testSize);
            for (int i = 1; i<testSize + 1; i++)
            {
                Assert.IsTrue(l.SetCurrentSubLocation(i), "Setting to an id which exists (" + i + ") should be succesful");
                Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");

                last = l.GetCurrentSubLocation();
                Assert.AreEqual(i, last.GetSublocationID(), "IDs should match");
            }

            Assert.IsFalse(l.SetCurrentSubLocation(testSize+1), "Setting to an id which does not exists ("+ (testSize+1) +") should be unsuccesful");
            Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
            Assert.AreEqual(last, l.GetCurrentSubLocation(), "Current Sublocation should have remained the same");
        }

        [TestCategory("Location"), TestCategory("LocationClass"), TestMethod()]
        public void Location_InvalidConstructor()
        {
            l = new Location(0);
            Assert.IsInstanceOfType(l, typeof(Location), "It should be a location");
            Assert.AreNotEqual(null, l, "Location should not be null");
            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            l = new Location(0, 1, 1);
            Assert.IsInstanceOfType(l, typeof(Location), "It should be a location");
            Assert.AreNotEqual(null, l, "Location should not be null");
            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            l = new Location(1, -1, 1);
            Assert.IsInstanceOfType(l, typeof(Location), "It should be a location");
            Assert.AreNotEqual(null, l, "Location should not be null");
            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            l = new Location(1, 1, -1);
            Assert.IsInstanceOfType(l, typeof(Location), "It should be a location");
            Assert.AreNotEqual(null, l, "Location should not be null");
            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            l = new Location(4, 1, 1, 1);
            Assert.IsInstanceOfType(l, typeof(Location), "It should be a location");
            Assert.AreNotEqual(null, l, "Location should not be null");
            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            l = new Location(-1, 1, 1, 1);
            Assert.IsInstanceOfType(l, typeof(Location), "It should be a location");
            Assert.AreNotEqual(null, l, "Location should not be null");
            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            l = new Location(1, -1, 1, 1);
            Assert.IsInstanceOfType(l, typeof(Location), "It should be a location");
            Assert.AreNotEqual(null, l, "Location should not be null");
            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

        }

        [TestCategory("Location"), TestCategory("LocationClass"), TestMethod()]
        public void Location_FullConstructor()
        {
            int testSize = 100;
            int maxItems = 10;
            int maxAmount = 5;
            l = new Location(testSize, maxItems, maxAmount);
            Sublocation last = new Residential();

            Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
            Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
            Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

            Assert.AreEqual(testSize, l.GetSize(), "Size should be " + testSize);
            for (int i = 1; i < testSize + 1; i++)
            {
                Assert.IsTrue(l.SetCurrentSubLocation(i), "Setting to an id which exists (" + i + ") should be succesful");
                Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");

                last = l.GetCurrentSubLocation();
                Assert.AreEqual(i, last.GetSublocationID(), "IDs should match");
                Assert.AreEqual(maxItems, last.GetMaxItems(), "Max items should match");
                Assert.AreEqual(maxAmount, last.GetMaxAmount(), "Max amount should match");
            }

            Assert.IsFalse(l.SetCurrentSubLocation(testSize + 1), "Setting to an id which does not exists (" + (testSize + 1) + ") should be unsuccesful");
            Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
            Assert.AreEqual(last, l.GetCurrentSubLocation(), "Current Sublocation should have remained the same");
        }

        [TestCategory("Location"), TestCategory("LocationClass"), TestMethod()]
        public void Location_RandomFullConstructor()
        {
            for (int j = 0; j < 100; j++)
            {
                int testSize = rnd.Next(10000);
                int maxItems = rnd.Next(10000);
                int maxAmount = rnd.Next(10000);
                l = new Location(testSize, maxItems, maxAmount);
                Sublocation last = new Residential();

                Assert.IsFalse(l.GetVisited(), "New Location should not be visited");
                Assert.IsTrue(l.IsCurrentSublocationNull(), "Current sublocation should be null");
                Assert.AreEqual(null, l.GetCurrentSubLocation(), "Current Sublocation should be null");

                Assert.AreEqual(testSize, l.GetSize(), "Size should be " + testSize);
                for (int i = 1; i < testSize + 1; i++)
                {
                    Assert.IsTrue(l.SetCurrentSubLocation(i), "Setting to an id which exists (" + i + ") should be succesful");
                    Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");

                    last = l.GetCurrentSubLocation();
                    Assert.AreEqual(i, last.GetSublocationID(), "IDs should match");
                    Assert.AreEqual(maxItems, last.GetMaxItems(), "Max items should match");
                    Assert.AreEqual(maxAmount, last.GetMaxAmount(), "Max amount should match");
                }

                Assert.IsFalse(l.SetCurrentSubLocation(testSize + 1), "Setting to an id which does not exists (" + (testSize + 1) + ") should be unsuccesful");
                Assert.IsFalse(l.IsCurrentSublocationNull(), "Current sublocation should not be null");
                Assert.AreEqual(last, l.GetCurrentSubLocation(), "Current Sublocation should have remained the same");

            }
        }

        [TestCategory("Location"), TestCategory("LocationClass"), TestMethod()]
        public void Location_ParseFromString()
        {
            foreach (Tuple<String, String> test in validLoc)
            {
                Assert.IsTrue(Location.IsValidLocation(test.Item1), test.Item2);
            }
        }

        [TestCategory("Location"), TestCategory("LocationClass"), TestMethod()]
        public void Location_ParseToString()
        {
            foreach (Tuple<String, String> test in validLoc)
            {
                l = new Location(test.Item1);
                Assert.AreEqual(test.Item1, l.ParseToString(), "Strings should be the same");
            }
        }

        [TestCategory("Location"), TestCategory("LocationClass"), TestMethod()]
        public void Location_CheckStringValid()
        {
            foreach (Tuple<String, String> test in validLoc)
            {
                Assert.IsTrue(Location.IsValidLocation(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidLoc)
            {
                Assert.IsFalse(Location.IsValidLocation(test.Item1), test.Item2);
            }   
        }
    }
}
