using System;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests_LongRoadHome.DiscoveryTests
{
    [TestClass]
    public class TDiscovery
    {
        Discovery disc;
        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();

        [TestInitialize]
        public void Setup()
        {
            validStrings.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text:1", "Basic String is valid"));
            validStrings.Add(new Tuple<string, string>(Discovery.TAG + ":2:Text:2", "Basic String is valid"));
            validStrings.Add(new Tuple<string, string>(Discovery.TAG + ":3:Text:3", "Basic String is valid"));
            validStrings.Add(new Tuple<string, string>(Discovery.TAG + ":4:Text:4", "Basic String is valid"));
            validStrings.Add(new Tuple<string, string>(Discovery.TAG + ":5:Text:5", "Basic String is valid"));
            validStrings.Add(new Tuple<string, string>(Discovery.TAG + ":6:Text:6", "Basic String is valid"));
            validStrings.Add(new Tuple<string, string>(Discovery.TAG + ":7:Text:7", "Basic String is valid"));
            validStrings.Add(new Tuple<string, string>(Discovery.TAG + ":8:Text:8", "Basic String is valid"));
            validStrings.Add(new Tuple<string, string>(Discovery.TAG + ":9:Text:9", "Basic String is valid"));

            invalidStrings.Add(new Tuple<string, string>("", "Empty String is invalid"));
            invalidStrings.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text", "Should have at least 4 elements"));
            invalidStrings.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text:10:1", "Should have at most 4 elements"));
            invalidStrings.Add(new Tuple<string, string>("blah:1:Text:10", "Should start with " + Discovery.TAG));
            invalidStrings.Add(new Tuple<string, string>(Discovery.TAG + ":meh:Text:10", "ID should be an int"));
            invalidStrings.Add(new Tuple<string, string>(Discovery.TAG + ":-1:Text:10", "ID should be positive"));
            invalidStrings.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text:blah", "Min number should be an int"));
            invalidStrings.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text:-1", "Min number should be positive"));
            invalidStrings.Add(new Tuple<string, string>("", ""));
        }

        [TestCategory("Discovery"), TestCategory("DiscoveryModel"), TestMethod()]
        public void Discovery_StandardConstructor()
        {
            disc = new Discovery();

            Assert.AreEqual(1, disc.GetDiscoveryID(), "ID should be 1");
            Assert.AreEqual("Test Text", disc.GetDiscoveryText(), "Text should be Test Text");
            Assert.AreEqual(1, disc.GetMinLocationNumber(), "Min locaiton should be 1");
        }

        [TestCategory("Discovery"), TestCategory("DiscoveryModel"), TestMethod()]
        public void Discovery_CheckStringIsValid()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(Discovery.IsValidDiscovery(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(Discovery.IsValidDiscovery(test.Item1), test.Item2);
            }
        }

        [TestCategory("Discovery"), TestCategory("DiscoveryModel"), TestMethod()]
        public void Discovery_ParseFromString()
        {
            int i = 1;
            foreach (Tuple<String, String> test in validStrings)
            {
                Discovery dc = new Discovery(test.Item1);
                Assert.AreEqual(i, dc.GetDiscoveryID(), "ID should match for discovery " + i);
                Assert.AreEqual(i, dc.GetMinLocationNumber(), "Min location number should match for discovery " + i);
                Assert.AreEqual("Text", dc.GetDiscoveryText(), "Text should match for discovery " + i);
                i++;
            }
        }

        [TestCategory("Discovery"), TestCategory("DiscoveryModel"), TestMethod()]
        public void Discovery_ParseToString()
        {
            int i = 1;
            foreach (Tuple<String, String> test in validStrings)
            {
                Discovery dc = new Discovery(test.Item1);
                String expected = test.Item1;

                Assert.AreEqual(expected, dc.ParseToString(), "Strings should match for discovery " + i);
                i++;
            }
        }

        [TestCategory("Discovery"), TestCategory("DiscoveryModel"), TestMethod()]
        public void Discovery_IsDiscoverable()
        {
            int i = 0;
            int j = 1;
            int k = 2;
            foreach (Tuple<String, String> test in validStrings)
            {
                Discovery dc = new Discovery(test.Item1);
                String expected = test.Item1;

                Assert.IsFalse(dc.IsDiscoverable(i), "Discovery " + j + " should not be discoverable at " + i);
                Assert.IsTrue(dc.IsDiscoverable(j), "Discovery " + j + " should be discoverable at " + j);
                Assert.IsTrue(dc.IsDiscoverable(k), "Discovery " + j + " should be discoverable at " + k);
                i++; j++; k++;
            }
        }

        
    }
}
