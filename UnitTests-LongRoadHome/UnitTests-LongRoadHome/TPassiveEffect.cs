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
            Assert.IsFalse(PassiveEffect.IsValidPassiveEffect(test1), "Empty String is invalid");

            String test2 =PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0";
            Assert.IsTrue(PassiveEffect.IsValidPassiveEffect(test2), "Basic Active Effect is valid");

            String test3 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH;
            Assert.IsFalse(PassiveEffect.IsValidPassiveEffect(test3), "Should be at least 3 items in a resource");

            String test4 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":10:11";
            Assert.IsFalse(PassiveEffect.IsValidPassiveEffect(test4), "Should be at most 3 items in a resource");

            String test5 = PassiveEffect.TAG + ":blah:10";
            Assert.IsFalse(PassiveEffect.IsValidPassiveEffect(test5), "Should affect one of the 4 primary character resources");

            String test6 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + PlayerCharacter.HEALTH;
            Assert.IsFalse(PassiveEffect.IsValidPassiveEffect(test6), "Third item should be an int");

            String test7 = "AE:" + PlayerCharacter.HEALTH + ":100";
            Assert.IsFalse(PassiveEffect.IsValidPassiveEffect(test7), "First item should be PE");
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

        [TestCategory("PlayerCharacter"), TestCategory("PassiveEffect"), TestMethod()]
        public void PassiveEffect_SamePassive()
        {
            String passive1 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0.7";
            String passive2 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0.8";
            String passive3 = PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":0.7";
            PassiveEffect pe1 = new PassiveEffect(passive1);
            PassiveEffect pe2 = new PassiveEffect(passive2);
            PassiveEffect pe3 = new PassiveEffect(passive3);

            Assert.IsTrue(pe1.SamePassiveType(pe1), "Passive Effect type should match itself");
            Assert.IsTrue(pe1.SamePassiveType(pe2), "Passive Effect type should match another of the same type");
            Assert.IsFalse(pe1.SamePassiveType(pe3), "Passive Effect type should not match another type");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PassiveEffect"), TestMethod()]
        public void PassiveEffect_MergePassive()
        {
            String passive1 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0.7";
            String passive2 = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":0.8";
            String passive3 = PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":0.7";
            PassiveEffect pe1 = new PassiveEffect(passive1);
            PassiveEffect pe2 = new PassiveEffect(passive2);
            PassiveEffect pe3 = new PassiveEffect(passive3);

            var merged1 = pe1.MergeEffect(pe2);
            var merged2 = pe1.MergeEffect(pe3);

            Assert.AreEqual(0.7 * 0.8, merged1.GetModifier(), 0.001,"After the merge modifier should be " + 0.7 * 0.8);
            Assert.AreEqual(pe1, merged2, "Different type merged should leave orginal the same");
        }
    }
}
