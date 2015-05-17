using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests_LongRoadHome
{
    [TestClass]
    public class TPlayerCharacter
    {
        [TestCategory("PlayerCharacter"), TestCategory("PC"), TestMethod()]
        public void PC_CreatedWithCorrectValues()
        {
            PlayerCharacter pc = new PlayerCharacter();
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.HEALTH), "Health Value Incorrect");
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.HUNGER), "Hunger Value Incorrect");
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.THIRST), "Sanity Value Incorrect");
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.SANITY), "Thirst Value Incorrect");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PC"), TestMethod()]
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

        [TestCategory("PlayerCharacter"), TestCategory("PC"), TestMethod()]
        public void PC_ParseToString()
        {
            PlayerCharacter pc = new PlayerCharacter();

            String expected = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                            + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1";

            String result = pc.ParseToString();

            Assert.AreEqual(expected, result, "Parsed String does not match");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PC"), TestMethod()]
        public void PC_CheckStringIsValid()
        {
            String test1 = "";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test1), "Empty String is invalid");

            String test2 = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1";
            Assert.IsTrue(PlayerCharacter.IsValidPC(test2), "Basic Character is valid");

            String test3 = PlayerCharacter.HEALTH + ":101:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test3), "Resources are capped at 100");

            String test4 = PlayerCharacter.HEALTH + ":-1:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test4), "Resources cannot be negative");

            String test5 = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1" + PlayerCharacter.SANITY + ":100:1";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test5), "There should only be 4 resources");

            String test6 = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test6), "There should be at least 4 resources");

            String test7 = PlayerCharacter.HEALTH + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test7), "There should be no duplicate resources");

            String test8 = "Blah" + ":100:1," + PlayerCharacter.HUNGER + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test8), "All resoureces should be one of the 4 satic values");

            String test9 = PlayerCharacter.HUNGER + ":100:1," + PlayerCharacter.HEALTH + ":100:1,"
                         + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test9), "Resources should be in the correct order");

            String test10 = PlayerCharacter.HEALTH + ":100," + PlayerCharacter.HUNGER + ":100:1,"
                          + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test10), "Resources should include a modifier");

            String test11 = PlayerCharacter.HEALTH + "," + PlayerCharacter.HUNGER + ":100,"
                          + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test11), "Resources should include a value");

            String test12 = PlayerCharacter.HEALTH + ":100:1:2," + PlayerCharacter.HUNGER + ":100,"
                          + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test12), "Resources should not have additional fields");

            String test13 = "100:" + PlayerCharacter.HEALTH + ":1," + PlayerCharacter.HUNGER + ":100,"
                          + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test13), "Resources should be pf the form String:Int:float");

            String test14 = PlayerCharacter.HEALTH + ":1.2:100," + PlayerCharacter.HUNGER + ":100,"
              + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.THIRST + ":100:1,";
            Assert.IsFalse(PlayerCharacter.IsValidPC(test14), "Resources should be pf the form String:Int:float");

        }

        [TestCategory("PlayerCharacter"), TestCategory("PC"), TestMethod()]
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

        [TestCategory("PlayerCharacter"), TestCategory("PC"), TestMethod()]
        public void PC_UpdateModifiers()
        {
            PlayerCharacter pc = new PlayerCharacter();
            PassiveEffect healthMod = new PassiveEffect(PlayerCharacter.HEALTH, 1.2f);

            List<PassiveEffect> list = new List<PassiveEffect>();
            list.Add(healthMod);

            pc.UpdateModifers(list);

            String expected = PlayerCharacter.HEALTH + ":100:1.2," + PlayerCharacter.HUNGER + ":100:1,"
                            + PlayerCharacter.THIRST + ":100:1," + PlayerCharacter.SANITY + ":100:1";
            String result = pc.ParseToString();

            Assert.AreEqual(expected, result, "Health modifier should be 1.2");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PC"), TestMethod()]
        public void PC_ModifierApplied()
        {
            PlayerCharacter pc = new PlayerCharacter();
            PassiveEffect healthMod = new PassiveEffect(PlayerCharacter.HEALTH, 0.8f);

            List<PassiveEffect> list = new List<PassiveEffect>();
            list.Add(healthMod);

            pc.UpdateModifers(list);
            pc.AdjustResource(PlayerCharacter.HEALTH, -20);
            int expected = 100 - Convert.ToInt32(20*0.8);

            int result = pc.GetResource(PlayerCharacter.HEALTH);
            Assert.AreEqual(expected, result, "Health should be 84");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PC"), TestMethod()]
        public void PC_AdjustmentMinimumIsOne()
        {
            PlayerCharacter pc = new PlayerCharacter();
            PassiveEffect healthMod = new PassiveEffect(PlayerCharacter.HEALTH, 0.1f);

            List<PassiveEffect> list = new List<PassiveEffect>();
            list.Add(healthMod);

            pc.UpdateModifers(list);
            pc.AdjustResource(PlayerCharacter.HEALTH, -1);
            int expected = 100 - 1;

            int result = pc.GetResource(PlayerCharacter.HEALTH);
            Assert.AreEqual(expected, result, "Health should be 99");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PC"), TestMethod()]
        public void PC_ModifierNotAppliedToPositive()
        {
            PlayerCharacter pc = new PlayerCharacter();
            PassiveEffect healthMod = new PassiveEffect(PlayerCharacter.HEALTH, 0.8f);

            List<PassiveEffect> list = new List<PassiveEffect>();
            list.Add(healthMod);

            pc.UpdateModifers(list);
            pc.AdjustResource(PlayerCharacter.HEALTH, -20);
            pc.AdjustResource(PlayerCharacter.HEALTH, 10);
            int expected = 100 - Convert.ToInt32(20 * 0.8) + 10;

            int result = pc.GetResource(PlayerCharacter.HEALTH);
            Assert.AreEqual(expected, result, "Health should be 94");
        }
    }
}
