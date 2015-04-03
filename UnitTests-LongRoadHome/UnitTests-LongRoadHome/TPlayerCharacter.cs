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
    }
}
