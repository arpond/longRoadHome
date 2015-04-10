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
        List<Tuple<String, String>> valid = new List<Tuple<String, String>>();
        List<Tuple<String, String>> invalid = new List<Tuple<String, String>>();

        [TestInitialize]
        public void Setup()
        {
            valid.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:2:3", "Standard dummy location is valid"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,ID:1","Should be at least 3 components"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections,Connections","At most 3 components"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,blah:1,Connections", "Components should be valid"));
            invalid.Add(new Tuple<String, String>("Type:sadas,ID:,Connections,", "Type should be DummyLocaiton"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,ID:sadas,Connections", "ID should be an int"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,ID:-1,Connections", "ID should be positive"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:", "If there are connections there should be at least one"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:1", "Can't connect to self"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:2:2", "Can't connect to locaiton more than once"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:2:3:4:-1", "Connections should all be positive"));
            invalid.Add(new Tuple<String, String>("Type:DummyLocation,ID:1,Connections:4:2::3", "Each should have a value"));
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
            dl = new DummyLocation();
            foreach(Tuple<String,String> test in valid)
            {
                Assert.IsTrue(dl.IsValidDummyLocation(test.Item1),test.Item2);
            }

            foreach (Tuple<String,String> test in invalid)
            {
                Assert.IsFalse(dl.IsValidDummyLocation(test.Item1), test.Item2);
            }
            
        }
    }
}
