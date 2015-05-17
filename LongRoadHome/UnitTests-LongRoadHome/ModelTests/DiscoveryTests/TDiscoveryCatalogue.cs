using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using System.Collections.Generic;

namespace UnitTests_LongRoadHome.DiscoveryTests
{
    [TestClass]
    public class TDiscoveryCatalogue
    {
        List<Tuple<String, String>> invalidDiscoveries = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validDiscoveries = new List<Tuple<String, String>>();
        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();
        [TestInitialize]
        public void Setup()
        {
            validDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text:1", "Basic String is valid"));
            validDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":2:Text:2", "Basic String is valid"));
            validDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":3:Text:3", "Basic String is valid"));
            validDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":4:Text:4", "Basic String is valid"));
            validDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":5:Text:5", "Basic String is valid"));
            validDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":6:Text:6", "Basic String is valid"));
            validDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":7:Text:7", "Basic String is valid"));
            validDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":8:Text:8", "Basic String is valid"));
            validDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":9:Text:9", "Basic String is valid"));

            invalidDiscoveries.Add(new Tuple<string, string>("", "Empty String is invalid"));
            invalidDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text", "Should have at least 4 elements"));
            invalidDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text:10:1", "Should have at most 4 elements"));
            invalidDiscoveries.Add(new Tuple<string, string>("blah:1:Text:10", "Should start with " + Discovery.TAG));
            invalidDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":meh:Text:10", "ID should be an int"));
            invalidDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":-1:Text:10", "ID should be positive"));
            invalidDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text:blah", "Min number should be an int"));
            invalidDiscoveries.Add(new Tuple<string, string>(Discovery.TAG + ":1:Text:-1", "Min number should be positive"));

            validStrings.Add(new Tuple<string, string>(DiscoveryCatalogue.TAG, "Empty Catalogue is valid"));
            validStrings.Add(new Tuple<string, string>(DiscoveryCatalogue.TAG + "#" + validDiscoveries[0].Item1, "Catalogue with a single item is valid"));
            validStrings.Add(new Tuple<string, string>(DiscoveryCatalogue.TAG + "#" + validDiscoveries[0].Item1 + "#" + validDiscoveries[1].Item1, "Catalogue with multiple items is valid"));

            String temp = DiscoveryCatalogue.TAG;
            foreach(var disc in validDiscoveries)
            {
                temp += "#" + disc.Item1;
            }

            validStrings.Add(new Tuple<string, string>(temp, "Large catalogue is valid"));

            invalidStrings.Add(new Tuple<string, string>("", "Empty String is invalid"));
            invalidStrings.Add(new Tuple<string, string>("blah", "Should start with " + DiscoveryCatalogue.TAG));
            invalidStrings.Add(new Tuple<string, string>(DiscoveryCatalogue.TAG + "#" + invalidDiscoveries[1].Item1, "Invalid Discovery means invalid catalogue"));
            invalidStrings.Add(new Tuple<string, string>(DiscoveryCatalogue.TAG + "#" + validDiscoveries[0].Item1 + "#" + validDiscoveries[0].Item1, "Duplicate  discovery ID means invalid catalogue"));

            invalidStrings.Add(new Tuple<string, string>("", ""));
        }

        [TestCategory("DiscoveryCatalgoue"), TestCategory("DiscoveryModel"), TestMethod()]
        public void DiscoveryCatalogue_IsValidDiscoveryCatalogue()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(DiscoveryCatalogue.IsValidDiscoveryCatalogue(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(DiscoveryCatalogue.IsValidDiscoveryCatalogue(test.Item1), test.Item2);
            }
        }

        [TestCategory("DiscoveryCatalgoue"), TestCategory("DiscoveryModel"), TestMethod()]
        public void DiscoveryCatalogue_ParseFromString()
        {
            int i = 1;
            List<Discovery> validDisc = new List<Discovery>();
            
            foreach(var test in validDiscoveries)
            {
                validDisc.Add(new Discovery(test.Item1));
            }

            foreach (Tuple<String, String> test in validStrings)
            {
                DiscoveryCatalogue dc = new DiscoveryCatalogue(test.Item1);
                var discoveries = dc.GetDiscoveries().Values;

                switch (i)
                {
                    case 1:
                        Assert.AreEqual(0, discoveries.Count, "Catalogue should be empty");
                        break;
                    case 2:
                        Assert.AreEqual(1, discoveries.Count, "Catalogue should have one item");
                        break;
                    case 3:
                        Assert.AreEqual(2, discoveries.Count, "Catalogue should have 2 items");
                        break;
                    case 4:
                        Assert.AreEqual(9, discoveries.Count, "Catalogue should have 9 empty");
                        break;
                }

                int j = 0;
                foreach (Discovery disc in discoveries)
                {
                    Assert.AreEqual(disc.ParseToString(), validDisc[j].ParseToString(), "Discoveries should be equal");
                    j++;
                }
                i++;
            }
        }

        [TestCategory("DiscoveryCatalgoue"), TestCategory("DiscoveryModel"), TestMethod()]
        public void DiscoveryCatalogue_ParseToString()
        {
            int i = 1;
            foreach (Tuple<String, String> test in validStrings)
            {
                DiscoveryCatalogue dc = new DiscoveryCatalogue(test.Item1);
                String expected = test.Item1;

                Assert.AreEqual(expected, dc.ParseToString(), "Strings should match for catalogue " + i);
                i++;
            }
        }
    }
}
