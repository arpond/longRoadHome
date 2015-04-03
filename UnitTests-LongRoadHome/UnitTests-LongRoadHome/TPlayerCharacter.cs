using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests_LongRoadHome
{
    [TestClass]
    public class TPlayerCharacter
    {
        [TestMethod]
        public void PC_CreatedWithCorrectValues()
        {
            PlayerCharacter pc = new PlayerCharacter();
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.HEALTH), "Health Value Incorrect");
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.HUNGER), "Hunger Value Incorrect");
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.THIRST), "Sanity Value Incorrect");
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.SANITY), "Thirst Value Incorrect");
        }

        [TestMethod]
        public void PC_HealthAdjustment()
        {
            PlayerCharacter pc = new PlayerCharacter();

            int adjustment1 = 10;
            int adjustment2 = -20;
            int adjustment3 = 10;
            int adjustment4 = -200;

            int expected1 = 100;
            int expected2 = 80;
            int expected3 = 90;
            int expected4 = 0;

            pc.AdjustResource(PlayerCharacter.HEALTH, adjustment1);
            int result1 = pc.GetResource(PlayerCharacter.HEALTH);
            Assert.AreEqual(expected1, result1, "Health should be capped at 100");

            pc.AdjustResource(PlayerCharacter.HEALTH, adjustment2);
            int result2 = pc.GetResource(PlayerCharacter.HEALTH);
            Assert.AreEqual(expected2, result2, "Health should be 80");

            pc.AdjustResource(PlayerCharacter.HEALTH, adjustment3);
            int result3 = pc.GetResource(PlayerCharacter.HEALTH);
            Assert.AreEqual(expected3, result3, "Health should be 90");

            pc.AdjustResource(PlayerCharacter.HEALTH, adjustment4);
            int result4 = pc.GetResource(PlayerCharacter.HEALTH);
            Assert.AreEqual(expected4, result4, "Health at minimum 0");
        }

        [TestMethod]
        public void PC_ParseToString()
        {
            PlayerCharacter pc = new PlayerCharacter();

            String expected = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                            + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1";

            String result = pc.ParseToString();

            Assert.AreEqual(expected, result, "Parsed String does not match");
        }

        [TestMethod]
        public void PC_CheckStringIsValid()
        {
            String test1 = "";
            Assert.IsFalse(PlayerCharacter.isValidPC(test1), "Empty String is invalid");

            String test2 = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1";
            Assert.IsTrue(PlayerCharacter.isValidPC(test2), "Basic Character is valid");

            String test3 = PlayerCharacter.HEALTH + ":101:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1";
            Assert.IsFalse(PlayerCharacter.isValidPC(test3), "Resources are capped at 100");

            String test4 = PlayerCharacter.HEALTH + ":-1:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1";
            Assert.IsFalse(PlayerCharacter.isValidPC(test4), "Resources cannot be negative");

            String test5 = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1" + PlayerCharacter.SANITY + ":100:1";
            Assert.IsFalse(PlayerCharacter.isValidPC(test5), "There should only be 4 resources");

            String test6 = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.isValidPC(test6), "There should be at least 4 resources");

            String test7 = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.isValidPC(test7), "There should be no duplicate resources");

            String test8 = "Blah" + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.isValidPC(test8), "All resoureces should be one of the 4 satic values");

            String test9 = PlayerCharacter.HUNGER + ":100:1," + PlayerCharacter.HEALTH + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.isValidPC(test9), "Resources should be in the correct order");

            String test10 = PlayerCharacter.HEALTH + ":100," + PlayerCharacter.HUNGER + ":100:1,"
                          + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.isValidPC(test10), "Resources should include a modifier");

            String test11 = PlayerCharacter.HEALTH + "," + PlayerCharacter.HUNGER + ":100,"
                          + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.isValidPC(test11), "Resources should include a value");

            String test12 = PlayerCharacter.HEALTH + ":100:1:2," + PlayerCharacter.HUNGER + ":100,"
                          + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.isValidPC(test12), "Resources should not have additional fields");

            String test13 = "100:" + PlayerCharacter.HEALTH + ":1," + PlayerCharacter.HUNGER + ":100,"
                          + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.isValidPC(test13), "Resources should be pf the form String:Int:float");

            String test14 = PlayerCharacter.HEALTH + ":1.2:100," + PlayerCharacter.HUNGER + ":100,"
              + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.isValidPC(test14), "Resources should be pf the form String:Int:float");

        }

        [TestMethod]
        public void PC_ParseFromString()
        {
            String creationString = PlayerCharacter.HEALTH + ":80:1.1," + PlayerCharacter.HUNGER + ":60:1,"
                                  + PlayerCharacter.THIRST + ":70:1.2," + PlayerCharacter.SANITY + ":50:1";
            String result = creationString;
            PlayerCharacter pc = new PlayerCharacter(creationString);

            Assert.AreEqual(creationString, result, "Result Strings and creation are not equal");
            Assert.AreEqual(80, pc.GetResource(PlayerCharacter.HEALTH), "Health Value Incorrect");
            Assert.AreEqual(60, pc.GetResource(PlayerCharacter.HUNGER), "Hunger Value Incorrect");
            Assert.AreEqual(70, pc.GetResource(PlayerCharacter.THIRST), "Sanity Value Incorrect");
            Assert.AreEqual(50, pc.GetResource(PlayerCharacter.SANITY), "Thirst Value Incorrect");

        }
    }
}
