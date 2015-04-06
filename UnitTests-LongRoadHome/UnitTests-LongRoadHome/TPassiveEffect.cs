using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;

namespace UnitTests_LongRoadHome
{
    [TestClass]
    public class TPassiveEffect
    {
        [TestCategory("PlayerCharacter"), TestCategory("PassiveEffect"), TestMethod()]
        public void PassiveEffect_StandardConstructor()
        {
            float mod = 0.8f;
            String res = PlayerCharacter.HEALTH;
            PassiveEffect pe = new PassiveEffect(res, mod);

            Assert.AreEqual(res, pe.GetResourceName(), "Resource should be " + res);
            Assert.AreEqual(mod, pe.GetModifier(), "Modifier should match" + mod);
        }

        [TestCategory("PlayerCharacter"), TestCategory("PassiveEffect"), TestMethod()]
        public void PassiveEffect_CheckStringIsValid()
        {
            String test1 = "";
            Assert.IsFalse(PassiveEffect.isValidPassiveEffect(test1), "Empty String is invalid");

            String test2 =PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0";
            Assert.IsTrue(PassiveEffect.isValidPassiveEffect(test2), "Basic Active Effect is valid");

            String test3 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH;
            Assert.IsFalse(PassiveEffect.isValidPassiveEffect(test3), "Should be at least 3 items in a resource");

            String test4 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":10:11";
            Assert.IsFalse(PassiveEffect.isValidPassiveEffect(test4), "Should be at most 3 items in a resource");

            String test5 = PassiveEffect.TAG + ":blah:10";
            Assert.IsFalse(PassiveEffect.isValidPassiveEffect(test5), "Should affect one of the 4 primary character resources");

            String test6 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + PlayerCharacter.HEALTH;
            Assert.IsFalse(PassiveEffect.isValidPassiveEffect(test6), "Third item should be an int");

            String test7 = "AE:" + PlayerCharacter.HEALTH + ":100";
            Assert.IsFalse(PassiveEffect.isValidPassiveEffect(test7), "First item should be PE");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PassiveEffect"), TestMethod()]
        public void PassiveEffect_ParseFromString()
        {
            PassiveEffect pe = new PassiveEffect(PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0.8");

            Assert.AreEqual(0.8f, pe.GetModifier(), "The value should be 0.8");
            Assert.AreEqual(PlayerCharacter.HEALTH, pe.GetResourceName(), "The string should match " + PlayerCharacter.HEALTH);
        }

        [TestCategory("PlayerCharacter"), TestCategory("PassiveEffect"), TestMethod()]
        public void PassiveEffect_ParseToString()
        {
            String expected = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0.7";
            PassiveEffect pe = new PassiveEffect(expected);

            Assert.AreEqual(expected, pe.ParseToString(), "String should be " + expected);
        }
    }
}
