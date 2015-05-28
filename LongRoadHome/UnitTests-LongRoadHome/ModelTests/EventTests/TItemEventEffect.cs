using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using System.Collections.Generic;

namespace UnitTests_LongRoadHome.EventTests
{
    [TestClass]
    public class TItemEventEffect
    {
        List<Tuple<String, String>> invalidStrings = new List<Tuple<String, String>>();
        List<Tuple<String, String>> validStrings = new List<Tuple<String, String>>();

        [TestInitialize]
        public void Setup()
        {
            String complexItem = "ID:1,Name:TestItem,Amount:1,Description:test item 1,"
                           + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                           + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                           + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                           + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                           + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                           + ",Requirements:2,Icon:test.png";
            String basicItem1 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            String basicItem2 = "ID:3,Name:TestItem,Amount:2,Description:test item 3,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            String invalidItem1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect:,PassiveEffect,Requirements,Icon:test.png";

            invalidStrings.Add(new Tuple<String, String>("", "Empty String is invalid"));
            invalidStrings.Add(new Tuple<String, String>(ItemEventEffect.ITEM_EFFECT_TAG + "#" + basicItem1, "Should be at least 3 fields in string"));
            invalidStrings.Add(new Tuple<String, String>(ItemEventEffect.ITEM_EFFECT_TAG + "#" + basicItem1 + "#Test Result#12", "Should be at most 3 fields in string"));
            invalidStrings.Add(new Tuple<String, String>(ItemEventEffect.ITEM_EFFECT_TAG + "#" + invalidItem1 + "#Test Result", "Invalid item means invalid effect"));
            invalidStrings.Add(new Tuple<String, String>("InvalidTag#" + basicItem1 + "#Test Result", "Should start with" + ItemEventEffect.ITEM_EFFECT_TAG));

            validStrings.Add(new Tuple<String, String>(ItemEventEffect.ITEM_EFFECT_TAG + "#" + basicItem1 + "#Test Result", "Basic effect should be valid"));
            validStrings.Add(new Tuple<String, String>(ItemEventEffect.ITEM_EFFECT_TAG + "#" + basicItem2 + "#Test Result", "Basic effect should be valid"));
            validStrings.Add(new Tuple<String, String>(ItemEventEffect.ITEM_EFFECT_TAG + "#" + complexItem + "#Test Result", "Complex effect should be valid"));
        }

        [TestCategory("ItemEventEffect"), TestCategory("EventModel"), TestMethod()]
        public void ItemEventEffect_StandardConstructor()
        {
            ItemEventEffect iee = new ItemEventEffect();
            Item item = new Item("ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements");

            Assert.AreEqual(item, iee.GetItem(), "Items should be equal");
        }

        [TestCategory("ItemEventEffect"), TestCategory("EventModel"), TestMethod()]
        public void ItemEventEffect_CheckStringValid()
        {
            foreach (Tuple<String, String> test in validStrings)
            {
                Assert.IsTrue(ItemEventEffect.IsValidItemEventEffect(test.Item1), test.Item2);
            }

            foreach (Tuple<String, String> test in invalidStrings)
            {
                Assert.IsFalse(ItemEventEffect.IsValidItemEventEffect(test.Item1), test.Item2);
            }
        }

        [TestCategory("ItemEventEffect"), TestCategory("EventModel"), TestMethod()]
        public void ItemEventEffect_ParseFromString()
        {
            ItemEventEffect iee = new ItemEventEffect(validStrings[0].Item1);
            String expectedItem = "ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.AreEqual(expectedItem, iee.GetItem().ParseToString(), "The string should match the expected value");
        }

        [TestCategory("ItemEventEffect"), TestCategory("EventModel"), TestMethod()]
        public void ItemEventEffect_ParseToString()
        {
            String expected = ItemEventEffect.ITEM_EFFECT_TAG + "#ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements,Icon:test.png#Test Result";
            ItemEventEffect iee = new ItemEventEffect(expected);
            String expectedItem = "ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";

            Assert.AreEqual(expectedItem, iee.GetItem().ParseToString(), "The string should match the expected value");
            Assert.AreEqual(expected, iee.ParseToString(), "String should be " + expected);
        }
    }
}
