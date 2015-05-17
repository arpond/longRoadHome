using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using uk.ac.dundee.arpond.longRoadHome.Controller;

namespace UnitTests_LongRoadHome.ControllerTests
{
    [TestClass]
    public class TDifficultyController
    {

        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();

        [TestInitialize]
        public void Setup()
        {
            validStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":1.1:Tracker", "Basic string should be valid"));
            validStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":1.1:Tracker|1.1", "With one value tracked"));
            validStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":1.1:Tracker|1.1|1|0.9|0.7|1.1", "Multiple tracked values"));

            invalidStrings.Add(new Tuple<string, string>("", "Empty String is invalid"));
            invalidStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":1.1", "Should have at least 3 elements"));
            invalidStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":1.1:Tracker:blah", "At most 3 elements"));
            invalidStrings.Add(new Tuple<string, string>("blah:1.1:Tracker", "Should start with "+ DifficultyController.TAG));
            invalidStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":not a double:Tracker", "Second element should be a double"));
            invalidStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":-1.1:Tracker", "Second element should be positive"));
            invalidStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":1.1:notTracker", "Third element should start with Tracker"));
            invalidStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":1.1:Tracker|", "If it has an element it should have at least one"));
            invalidStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":1.1:Tracker|1.1|a", "Each element should be a double"));
            invalidStrings.Add(new Tuple<string, string>(DifficultyController.TAG + ":1.1:Tracker|1.2|1.1|1.1|-1", "Each element should be positive"));
        }

        [TestCategory("DifficultyController"), TestCategory("Controller"), TestMethod()]
        public void DifficultyController_StandardConstructor()
        {
            DifficultyController dc = new DifficultyController();
            Assert.AreEqual(1, dc.GetPlayerStatus(), "Player status should be 1");
            Assert.AreEqual(0, dc.GetEndLocationChance(), "Should be no chance of end location");
            Assert.AreEqual(1d, dc.GetEventModifier(), "Event modifier should be 1");
            Assert.AreEqual(0, dc.GetPlayerStatusTracker().Count, "Status tracker should be empty");
            //Assert.IsTrue(40 / 100 <= dc.GetEventChance() && dc.GetEventChance() <= 80 / 100, "Event chance should be between 40% and 80%");
        }

        [TestCategory("DifficultyController"), TestCategory("Controller"), TestMethod()]
        public void DifficultyController_CheckStringIsValid()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(DifficultyController.IsValidDifficultyController(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(DifficultyController.IsValidDifficultyController(test.Item1), test.Item2);
            }
        }

        [TestCategory("DifficultyController"), TestCategory("Controller"), TestMethod()]
        public void DifficultyController_ParseFromString()
        {
            DifficultyController dc = new DifficultyController(validStrings[0].Item1);
            Assert.AreEqual(1.1, dc.GetPlayerStatus(), "Player status should be 1.1");
            Assert.AreEqual(0, dc.GetEndLocationChance(), "Should be no chance of end location");
            Assert.AreEqual(0.9d, dc.GetEventModifier(), 0.0001, "Event modifier should be 0.9");
            Assert.AreEqual(0, dc.GetPlayerStatusTracker().Count, "Status tracker should be empty");
            Assert.IsTrue(0.4d <= dc.GetEventChance() && dc.GetEventChance() <= 0.8d, "Event chance should be between 40% and 80%");

            dc = new DifficultyController(validStrings[2].Item1);
            Assert.AreEqual(1.1, dc.GetPlayerStatus(), "Player status should be 1.1");
            Assert.AreEqual(0, dc.GetEndLocationChance(), "Should be no chance of end location");
            Assert.AreEqual(0.9d, dc.GetEventModifier(), 0.0001, "Event modifier should be 0.9");
            Assert.AreEqual(5, dc.GetPlayerStatusTracker().Count, "Status tracker should be empty");
            //Assert.IsTrue(40 / 100 <= dc.GetEventChance() && dc.GetEventChance() <= 80 / 100, "Event chance should be between 40% and 80%");
        }

        [TestCategory("DifficultyController"), TestCategory("Controller"), TestMethod()]
        public void DifficultyController_ParseToString()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                DifficultyController dc = new DifficultyController(test.Item1);
                Assert.AreEqual(test.Item1, dc.ParseToString(), "String should be equal");
            }
        }

        [TestCategory("DifficultyController"), TestCategory("Controller"), TestMethod()]
        public void DifficultyController_UpdatePlayerStatus()
        {
            DifficultyController dc = new DifficultyController(validStrings[0].Item1);
            Assert.AreEqual(1.1, dc.GetPlayerStatus(), "Player status should be 1.1");
            Assert.AreEqual(0, dc.GetEndLocationChance(), "Should be no chance of end location");
            Assert.AreEqual(0.9d, dc.GetEventModifier(), 0.0001, "Event modifier should be 0.9");
            Assert.AreEqual(0, dc.GetPlayerStatusTracker().Count, "Status tracker should be empty");
            //Assert.IsTrue(40 / 100 <= dc.GetEventChance() && dc.GetEventChance() <= 80 / 100, "Event chance should be between 40% and 80%");

            int statsSum = 300;
            double invValue = 0.125;
            double newStatus = (double) statsSum / 400 + invValue;
            double current = dc.GetPlayerStatus();
            double expected = 0.75d * current + (1 - 0.75d) * newStatus;

            dc.UpdatePlayerStatus(statsSum, invValue);
            Assert.AreEqual(expected, dc.GetPlayerStatus(), "Player status should be the same as expected");

            statsSum = 320;
            invValue = 0.1;
            newStatus = (double)statsSum / 400 + invValue;
            current = dc.GetPlayerStatus();
            expected = 0.75d * current + (1 - 0.75d) * newStatus;

            dc.UpdatePlayerStatus(statsSum, invValue);
            Assert.AreEqual(expected, dc.GetPlayerStatus(), "Player status should be the same as expected");
        }

        public void DifficultyController_UpdatePlayerTracker()
        {
            DifficultyController dc = new DifficultyController(validStrings[0].Item1);
            Assert.AreEqual(1.1, dc.GetPlayerStatus(), "Player status should be 1.1");
            Assert.AreEqual(0, dc.GetEndLocationChance(), "Should be no chance of end location");
            Assert.AreEqual(0.9d, dc.GetEventModifier(), 0.0001, "Event modifier should be 0.9");
            Assert.AreEqual(0, dc.GetPlayerStatusTracker().Count, "Status tracker should be empty");
            Assert.IsTrue(40 / 100 <= dc.GetEventChance() && dc.GetEventChance() <= 80 / 100, "Event chance should be between 40% and 80%");
            dc.UpdateStatusTracker();
            Assert.AreEqual(1, dc.GetPlayerStatusTracker().Count, "Status tracker should contain 1 value");
            int statsSum = 300;
            double invValue = 0.125;
            double newStatus = (double)statsSum / 400 + invValue;
            double current = dc.GetPlayerStatus();
            double expected = 0.75d * current + (1 - 0.75d) * newStatus;

            dc.UpdatePlayerStatus(statsSum, invValue);
            Assert.AreEqual(expected, dc.GetPlayerStatus(), "Player status should be the same as expected");

            statsSum = 320;
            invValue = 0.1;
            newStatus = (double)statsSum / 400 + invValue;
            current = dc.GetPlayerStatus();
            expected = 0.75d * current + (1 - 0.75d) * newStatus;

            dc.UpdatePlayerStatus(statsSum, invValue);
            Assert.AreEqual(expected, dc.GetPlayerStatus(), "Player status should be the same as expected");
            dc.UpdateStatusTracker();
            Assert.AreEqual(2, dc.GetPlayerStatusTracker().Count, "Status tracker should contain 2 values");
        }
    }
}
