using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests_LongRoadHome
{
    [TestClass]
    public class TActiveEffect
    {
        [TestCategory("PlayerCharacter"), TestCategory("ActiveEffect"), TestMethod()]
        public void ActiveEffect_StandardConstructor()
        {
            ActiveEffect ae = new ActiveEffect();
            String expectedPR = PlayerCharacter.HEALTH + ":0";

            Assert.AreEqual(0, ae.GetValue(), "The value should be 0");
            Assert.AreEqual(expectedPR, ae.GetResource().ParseToString(), "The string should match the expected value");
        }

        [TestCategory("PlayerCharacter"), TestCategory("ActiveEffect"), TestMethod()]
        public void ActiveEffect_CheckStringIsValid()
        {
            String test1 = "";
            Assert.IsFalse(ActiveEffect.isValidActiveEffect(test1), "Empty String is invalid");

            String test2 = ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0";
            Assert.IsTrue(ActiveEffect.isValidActiveEffect(test2), "Basic Active Effect is valid");

            String test3 = ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH;
            Assert.IsFalse(ActiveEffect.isValidActiveEffect(test3), "Should be at least 3 items in a resource");

            String test4 = ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":10:11";
            Assert.IsFalse(ActiveEffect.isValidActiveEffect(test4), "Should be at most 3 items in a resource");

            String test5 = ActiveEffect.TAG + ":" + ":blah:10";
            Assert.IsFalse(ActiveEffect.isValidActiveEffect(test5), "Should affect one of the 4 primary character resources");

            String test6 = ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + PlayerCharacter.HEALTH;
            Assert.IsFalse(ActiveEffect.isValidActiveEffect(test6), "Third item should be an int");

            String test7 = "PE:" + PlayerCharacter.HEALTH + ":100";
            Assert.IsFalse(ActiveEffect.isValidActiveEffect(test7), "First item should be AE");
        }

        [TestCategory("PlayerCharacter"), TestCategory("ActiveEffect"), TestMethod()]
        public void ActiveEffect_ParseFromString()
        {
            ActiveEffect ae = new ActiveEffect(ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0");
            String expectedPR = PlayerCharacter.HEALTH + ":0";

            Assert.AreEqual(0, ae.GetValue(), "The value should be 0");
            Assert.AreEqual(expectedPR, ae.GetResource().ParseToString(), "The string should match the expected value");
        }

        [TestCategory("PlayerCharacter"), TestCategory("ActiveEffect"), TestMethod()]
        public void ActiveEffect_ParseToString()
        {
            String expected = ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":10";
            ActiveEffect ae = new ActiveEffect(expected);

            Assert.AreEqual(expected, ae.ParseToString(), "String should be " + expected);
        }
    }
}
