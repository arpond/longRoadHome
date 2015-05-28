using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests_LongRoadHome
{
    [TestClass]
    public class TItem
    {
        [TestCategory("PlayerCharacter"), TestCategory("Item"), TestMethod()]
        public void Item_StandardConstructor()
        {
            Item item = new Item();

            Assert.AreEqual(1, item.GetID(), "Default ID should be 1");
            Assert.AreEqual("TestItem", item.GetName(), "Deafault name should be TestItem");
            Assert.AreEqual(1, item.GetAmount(), "Default amount should be 1");
            Assert.AreEqual("test item 1", item.GetDescription(), "Default description should be test item 1");
            Assert.IsFalse(item.HasActiveEffect(), "Should not have any active effect");
            Assert.IsFalse(item.HasPassiveEffect(), "Should not have any passive effect");
            Assert.IsFalse(item.HasRequirements(), "Should not have any requirements");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Item"), TestMethod()]
        public void Item_BasicParseToString()
        {
            Item item = new Item();
            String expected = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";

            Assert.AreEqual(expected, item.ParseToString(), "String should be parsed to the expected String");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Item"), TestMethod()]
        public void Item_CheckBasicStringIsValid()
        {
            String test1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsTrue(Item.IsValidItem(test1), "Basic item should be valid");

            String test2 = "ID:1:2,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test2), "Should only have 1 ID");

            String test3 = "ID,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test3), "ID should have at least 1 item");

            String test4 = "ID:,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test4), "ID should not be null");

            String test5 = "ID:asdas,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test5), "ID should not be a string");

            String test6 = "ID:1.1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test6), "ID should not be a float");

            String test7 = "ID:-1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test7), "ID should be positive");

            String test8 = "ID:1,Name:TestItem:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test8), "Item should only have 1 Name");

            String test9 = "ID:1,Name,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test9), "Item should at least 1 Name");

            String test10 = "ID:1,Name:,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test10), "Name should not be null");

            String test11 = "ID:1,Name:TestItem,Amount:1:2,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test11), "Should only have 1 Amount");

            String test12 = "ID:1,Name:TestItem,Amount,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test12), "Amount should have at least 1 item");

            String test13 = "ID:1,Name:TestItem,Amount:,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test13), "Amount should not be null");

            String test14 = "ID:1,Name:TestItem,Amount:asdasd,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test14), "Amount should not be a string");

            String test15 = "ID:1,Name:TestItem,Amount:1.1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test15), "Amount should not be a float");

            String test16 = "ID:1,Name:TestItem,Amount:1,Description:test item 1:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test16), "Item should only have 1 Description");

            String test17 = "ID:1,Name:TestItem,Amount:1,Description,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test17), "Item should at least 1 Description");

            String test18 = "ID:1,Name:TestItem,Amount:1,Description:,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test18), "Description should not be null");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Item"), TestMethod()]
        public void Item_CheckFullStringIsValid()
        {
            String expected = "ID:1,Name:TestItem,Amount:1,Description:test item 1,"
                           + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                           + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                           + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                           + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                           + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                           + ",Requirements:2,Icon:test.png";
            Assert.IsTrue(Item.IsValidItem(expected), "Full String should be valid");

            String test1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect:,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test1), "If active effect has items it should have at least 1");

            String test2 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect:a,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test2), "If active effect has items they should be in groups of 3");

            String test3 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect:a:b,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test3), "If active effect has items they should be in groups of 3");

            String test4 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect:a:b:c:d,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test4), "If active effect has items they should be in groups of 3");

            String test5a = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10,PassiveEffect,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test5a), "Active Effect should be valid");

            String test5b = PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10";
            Assert.AreEqual(Item.IsValidItem(test5a), ActiveEffect.IsValidActiveEffect(test5b), "Invalid Active Effect should mean invalid Item");

            String test6 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect:,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test6), "If passive effect has items it should have at least 1");

            String test7 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect:a,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test7), "If passive effect has items they should be in groups of 3");

            String test8 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect:a:b,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test8), "If passive effect has items they should be in groups of 3");

            String test9 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect:a:b:c:d,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test9), "If passive effect has items they should be in groups of 3");

            String test10a = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10,Requirements,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test10a), "Passive Effect should be valid");

            String test10b = ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10";
            Assert.AreEqual(Item.IsValidItem(test10a), PassiveEffect.IsValidPassiveEffect(test10b), "Invalid Passive Effect should mean invalid Item");

            String test11 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements:,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test11), "If Requirements has items it should have at least 1");

            String test12 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements:asdasd,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test12), "If Requirements has items they should be ints");

            String test13 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements:2:3:2,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test13), "If Requirements has items there shouldn't be duplicates");

            String test14 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements:2:3::4,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test14), "If Requirements has items none should be null");

            String test15 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements:2:-3:3:4,Icon:test.png";
            Assert.IsFalse(Item.IsValidItem(test15), "If Requirements has items none should be negative");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Item"), TestMethod()]
        public void Item_BasicParseFromString()
        {
            String expected = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            Item item = new Item(expected);

            Assert.AreEqual(expected, item.ParseToString(), "String should be the same as the string constructed with");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Item"), TestMethod()]
        public void Item_FullParseFromString()
        {
            String expected = "ID:1,Name:TestItem,Amount:1,Description:test item 1,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ",Requirements:2,Icon:test.png";
            Item item = new Item(expected);

            Assert.AreEqual(expected, item.ParseToString(), "String should be the same as the string constructed with");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Item"), TestMethod()]
        public void Item_ItemValue()
        {
            String expected = "ID:1,Name:TestItem,Amount:1,Description:test item 1,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect,Requirements:2,Icon:test.png";
            Item item =new Item(expected);
            Assert.AreEqual(0.05d, item.CalculateItemValue(),0.00001, "Item value should be 0.05");

            expected = "ID:1,Name:TestItem,Amount:1,Description:test item 1,"
                            + "ActiveEffect"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ",Requirements:2,Icon:test.png";
            item = new Item(expected);
            Assert.AreEqual(0.06d, item.CalculateItemValue(), 0.00001, "Item value should be 0.06d");
            
            expected = "ID:1,Name:TestItem,Amount:1,Description:test item 1,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ",Requirements:2,Icon:test.png";
            item = new Item(expected);
            Assert.AreEqual(0.11d, item.CalculateItemValue(), 0.00001, "Item value should be 0.11");
        }
    }
}
