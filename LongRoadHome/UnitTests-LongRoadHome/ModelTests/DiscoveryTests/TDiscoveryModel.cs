using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;

namespace UnitTests_LongRoadHome.DiscoveryTests
{
    [TestClass]
    public class TDiscoveryModel
    {
        DiscoveryModel dm;
        DiscoveryCatalogue dc;

        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validDiscoveries = new List<Tuple<String, String>>();
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

            String temp = DiscoveryCatalogue.TAG;
            foreach (var disc in validDiscoveries)
            {
                temp += "#" + disc.Item1;
            }

            dc = new DiscoveryCatalogue(temp);

            validStrings.Add(new Tuple<string, string>(DiscoveryModel.DISCOVERED_TAG, "Empty discovered is valid"));
            validStrings.Add(new Tuple<string, string>(DiscoveryModel.DISCOVERED_TAG + ":1", "One discovery is valid"));
            validStrings.Add(new Tuple<string, string>(DiscoveryModel.DISCOVERED_TAG + ":1:2:3:4:5:6:7:8", "Mulitple discoveries are valid"));

            invalidStrings.Add(new Tuple<string, string>(DiscoveryModel.DISCOVERED_TAG + ":", "If there are discoveries there should be at least one"));
            invalidStrings.Add(new Tuple<string, string>(DiscoveryModel.DISCOVERED_TAG + ":1:1", "Discoveries should be unique"));
            invalidStrings.Add(new Tuple<string, string>(DiscoveryModel.DISCOVERED_TAG + ":a", "Discoveries should be ints"));
            invalidStrings.Add(new Tuple<string, string>(DiscoveryModel.DISCOVERED_TAG + ":-1", "Discoveries should be positive"));
        }

        [TestCategory("DiscoveryModelClass"), TestCategory("DiscoveryModel"), TestMethod()]
        public void DiscoveryModel_CheckStringIsValidDiscovered()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(DiscoveryModel.IsValidDiscovered(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(DiscoveryModel.IsValidDiscovered(test.Item1), test.Item2);
            }
        }

        [TestCategory("DiscoveryModelClass"), TestCategory("DiscoveryModel"), TestMethod()]
        public void DiscoveryModel_ParseFromString()
        {
            dm = new DiscoveryModel(validStrings[2].Item1, dc.ParseToString());

            HashSet<int> disc = dm.GetDiscovered();

            Assert.AreEqual(8, disc.Count, "Should contain 8 elements");
            for (int i = 1; i < 9; i++)
            {
                Assert.IsTrue(disc.Contains(i), i + "should be discovered");
            }
            Assert.AreEqual(dm.GetDiscoveryCatalogue().ParseToString(),dc.ParseToString(), "Catalgoues should be equal");
        }

        [TestCategory("DiscoveryModelClass"), TestCategory("DiscoveryModel"), TestMethod()]
        public void DiscoveryModel_ParseToString()
        {
            dm = new DiscoveryModel(validStrings[2].Item1, dc.ParseToString());

            Assert.AreEqual(validStrings[2].Item1, dm.ParseDiscoveredToString(), "String should match");
        }
    }
}
