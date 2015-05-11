using System;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;

namespace UnitTests_LongRoadHome.LocationTests
{
    [TestClass]
    public class TSublocation
    {
        Residential res = new Residential();
        Commercial com = new Commercial();
        Civic civ = new Civic();
        List<Item> items = new List<Item>();
        Random rnd = new Random();
        String stdSubLoc;
        List<Tuple<String, String>> invalid = new List<Tuple<String, String>>();

        [TestInitialize]
        public void Setup()
        {
            stdSubLoc = ":1:False:2:3:temp";
            invalid.Add(new Tuple<String, String>(":1:False:2:3","String should have exactly 6 items"));
            invalid.Add(new Tuple<String, String>(":1:False:2:3:temp:7", "String should have exactly 6 items"));
            invalid.Add(new Tuple<String, String>(":dasdas:False:2:3", "ID should be an int"));
            invalid.Add(new Tuple<String, String>(":1:blah:2:3:temp", "Scavenged should be a bool"));
            invalid.Add(new Tuple<String, String>(":1:False:sada:3:temp", "Max Items should be an int"));
            invalid.Add(new Tuple<String, String>(":1:False:2:blah:temp", "Max Amount should be an int"));
            invalid.Add(new Tuple<String, String>("::False:2:3:temp", "ID should have a value"));
            invalid.Add(new Tuple<String, String>(":1::2:3:temp", "Scav should have a value"));
            invalid.Add(new Tuple<String, String>(":1:False::3:temp", "Max Items should have a value"));
            invalid.Add(new Tuple<String, String>(":1:False:2::temp", "Max Amount should have a value"));
            res = new Residential(1, 3, 5);
            com = new Commercial(2, 4, 7);
            civ = new Civic(3, 6, 3);
            for (int i = 1; i < 21; i++)
            {
                Item tmp = new Item(StringMaker.makeItemStr(i));
                items.Add(tmp);
            }
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Residential"), TestMethod()]
        public void Residential_StandardConstructor()
        {
            Assert.IsFalse(res.GetScavenged(), "Should not be scavenged");
            Assert.AreEqual(1, res.GetSublocationID(), "ID should be 1");
            Assert.AreEqual(3, res.GetMaxItems(), "Max items should be 3");
            Assert.AreEqual(5, res.GetMaxAmount(), "Max amount should be 5");
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Residential"), TestMethod()]
        public void Residential_CheckStringIsValid()    
        {
            Assert.IsTrue(res.IsValidSublocation(Residential.TYPE + stdSubLoc), "Basic location should be valid");
            Assert.IsFalse(res.IsValidSublocation(Civic.TYPE + stdSubLoc), "Wrong type of location should be invalid");
            Assert.IsFalse(res.IsValidSublocation(Commercial.TYPE + stdSubLoc), "Wrong type of location should be invalid");
            Assert.IsFalse(res.IsValidSublocation(stdSubLoc), "No type of location should be invalid");

            foreach(Tuple<String,String> test in invalid)
            {
                Assert.IsFalse(res.IsValidSublocation(Residential.TYPE + test.Item1), test.Item2);
            }
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Commercial"), TestMethod()]
        public void Commercial_CheckStringIsValid()
        {
            Assert.IsTrue(com.IsValidSublocation(Commercial.TYPE + stdSubLoc), "Basic location should be valid");
            Assert.IsFalse(com.IsValidSublocation(Civic.TYPE + stdSubLoc), "Wrong type of location should be invalid");
            Assert.IsFalse(com.IsValidSublocation(Residential.TYPE + stdSubLoc), "Wrong type of location should be invalid");
            Assert.IsFalse(com.IsValidSublocation(stdSubLoc), "No type of location should be invalid");

            foreach (Tuple<String, String> test in invalid)
            {
                Assert.IsFalse(com.IsValidSublocation(Commercial.TYPE + test.Item1), test.Item2);
            }
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Civic"), TestMethod()]
        public void Civic_CheckStringIsValid()
        {
            Assert.IsTrue(civ.IsValidSublocation(Civic.TYPE + stdSubLoc), "Basic location should be valid");
            Assert.IsFalse(civ.IsValidSublocation(Commercial.TYPE + stdSubLoc), "Wrong type of location should be invalid");
            Assert.IsFalse(civ.IsValidSublocation(Residential.TYPE + stdSubLoc), "Wrong type of location should be invalid");
            Assert.IsFalse(civ.IsValidSublocation(stdSubLoc), "No type of location should be invalid");

            foreach (Tuple<String, String> test in invalid)
            {
                Assert.IsFalse(civ.IsValidSublocation(Civic.TYPE + test.Item1), test.Item2);
            }
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Commercial"), TestMethod()]
        public void Commercial_StandardConstructor()
        {
            Assert.IsFalse(com.GetScavenged(), "Should not be scavenged");
            Assert.AreEqual(2, com.GetSublocationID(), "ID should be 1");
            Assert.AreEqual(4, com.GetMaxItems(), "Max items should be 3");
            Assert.AreEqual(7, com.GetMaxAmount(), "Max amount should be 5");
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Civic"), TestMethod()]
        public void Civic_StandardConstructor()
        {
            Assert.IsFalse(civ.GetScavenged(), "Should not be scavenged");
            Assert.AreEqual(3, civ.GetSublocationID(), "ID should be 1");
            Assert.AreEqual(6, civ.GetMaxItems(), "Max items should be 3");
            Assert.AreEqual(3, civ.GetMaxAmount(), "Max amount should be 5");
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Residential"), TestMethod()]
        public void Residential_ParseToString()
        {
            String exp = Residential.TYPE + ":" + "1:False:3:5:temp";
            Assert.AreEqual(exp, res.ParseToString(), "Strings should match");
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Commecial"), TestMethod()]
        public void Commercial_ParseToString()
        {
            String exp = Commercial.TYPE + ":" + "2:False:4:7:temp";
            Assert.AreEqual(exp, com.ParseToString(), "Strings should match");
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Civic"), TestMethod()]
        public void Civic_ParseToString()
        {
            String exp = Civic.TYPE + ":" + "3:False:6:3:temp";
            Assert.AreEqual(exp, civ.ParseToString(), "Strings should match");
        }

        [TestCategory("Location"), TestCategory("Sublocation"), TestCategory("Residential"), TestMethod()]
        public void Residential_Scavenge()
        {
            for (int i = 0; i<1000; i++)
            {
                items = new List<Item>();
                for (int j = 1; j < 21; j++)
                {
                    Item tmp = new Item(StringMaker.makeItemStr(j));
                    items.Add(tmp);
                }
                res = new Residential(1, rnd.Next(1,10), rnd.Next(1,10));
                var ids = new List<int>();
                var found = res.Scavenge(items);
                Assert.IsTrue(res.GetScavenged(), "Should be scavenged");
                Assert.IsTrue(res.GetMaxItems() >= found.Count, "Items found should not exceed max");
                Assert.IsTrue(found.Count > 0,"Should be at least one item");
                foreach (Item item in found)
                {
                    Assert.IsFalse(ids.Contains(item.GetID()), "ID should not be in the list already");

                    ids.Add(item.GetID());
                    Assert.IsTrue(res.GetMaxAmount() >= item.GetAmount(), "Items found should not have more instances max");
                }

                found = res.Scavenge(items);
                Assert.IsTrue(found.Count == 0, "Should be no items in list");
            }
        }
    }
}
