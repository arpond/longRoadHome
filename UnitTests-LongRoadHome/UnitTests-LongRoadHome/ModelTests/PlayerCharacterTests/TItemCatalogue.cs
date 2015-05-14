using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests_LongRoadHome
{
    [TestClass]
    public class TItemCatalogue
    {
        [TestCategory("PlayerCharacter"), TestCategory("ItemCatalogue"), TestMethod()]
        public void ItemCatalogue_CheckStringIsValid()
        {
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements";
            String itemStr2 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements";
            String itemStr3 = "ID:3,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String invalidItem1 = "ID:-1,Name:TestItem,Amount:1,Description:test item -1,ActiveEffect,PassiveEffect,Requirements";
            String invalidItemAmount1 = "ID:4,Name:TestItem,Amount:2,Description:test item 4,ActiveEffect,PassiveEffect,Requirements";

            String expected = ItemCatalogue.TAG + ";" + itemStr1 + ";" + itemStr2 + ";" + itemStr3;
            String nonUniqueID = ItemCatalogue.TAG + ";" + itemStr1 + ";" + itemStr2 + ";" + itemStr3 + ";" + itemStr1;

            Assert.IsTrue(ItemCatalogue.IsValidItemCatalogue(ItemCatalogue.TAG), "empty catalogue should be valid");
            Assert.IsTrue(ItemCatalogue.IsValidItemCatalogue(expected), "Standard catalogue should be valid");
            Assert.IsFalse(ItemCatalogue.IsValidItemCatalogue(ItemCatalogue.TAG + ";"), "if an catalogue has items it should have at least one item");
            Assert.IsFalse(ItemCatalogue.IsValidItemCatalogue("NotaCatalogue;" + itemStr1), "Not an catalogue should be invalid");
            Assert.IsFalse(ItemCatalogue.IsValidItemCatalogue(";" + itemStr1), "No Tag should be invalid");
            Assert.IsFalse(ItemCatalogue.IsValidItemCatalogue(ItemCatalogue.TAG + itemStr1), "No # should be invalid");
            Assert.IsFalse(ItemCatalogue.IsValidItemCatalogue(ItemCatalogue.TAG + ";" + invalidItem1), "Invalid item means invalid catalogue");
            Assert.AreEqual(ItemCatalogue.IsValidItemCatalogue(ItemCatalogue.TAG + ";" + invalidItem1), Item.IsValidItem(invalidItem1), "Invalid item should match invalid catalogue");
            Assert.IsFalse(ItemCatalogue.IsValidItemCatalogue(ItemCatalogue.TAG + ";" + invalidItemAmount1), "Catalogue Items should only have amount of 1");
            Assert.IsFalse(ItemCatalogue.IsValidItemCatalogue(nonUniqueID), "Each ID should be unique");
        }

        [TestCategory("PlayerCharacter"), TestCategory("ItemCatalogue"), TestMethod()]
        public void ItemCatalogue_ParseFromString()
        {
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements";
            String itemStr2 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ",Requirements:2";
            String itemStr3 = "ID:3,Name:TestItem,Amount:1,Description:test item 3,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "0.8"
                            + ",Requirements:2";

            String catalogueStr = ItemCatalogue.TAG + ";" + itemStr1 + ";" + itemStr2 + ";" + itemStr3;

            var catalogue = new ItemCatalogue(catalogueStr);

            Assert.AreEqual(itemStr1, catalogue.GetItem(1).ParseToString(), "Item 1 should be in the catalogue");
            Assert.AreEqual(itemStr2, catalogue.GetItem(2).ParseToString(), "Item 2 should be in the catalogue");
            Assert.AreEqual(itemStr3, catalogue.GetItem(3).ParseToString(), "Item 3 should be in the catalogue");
            Assert.AreEqual(null, catalogue.GetItem(4), "Item 4 should not be in the catalogue");
        }

        [TestCategory("PlayerCharacter"), TestCategory("ItemCatalogue"), TestMethod()]
        public void ItemCatalogue_GetRandomItem()
        {
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements";
            String itemStr2 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ",Requirements:2";
            String itemStr3 = "ID:3,Name:TestItem,Amount:1,Description:test item 3,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "0.8"
                            + ",Requirements:2";
            
            String catalogueStr = ItemCatalogue.TAG + ";" + itemStr1 + ";" + itemStr2 + ";" + itemStr3;

            for(int i = 4; i <= 1000; i++)
            {
                String loopItem = "ID:"+ i +",Name:TestItem,Amount:1,Description:test item"+ i +",ActiveEffect,PassiveEffect,Requirements";
                catalogueStr += ";" + loopItem;
            }

            var catalogue = new ItemCatalogue(catalogueStr);

            for (int i = 0; i< 10; i++)
            {
                var item1 = catalogue.GetRandomItem();
                var item2 = catalogue.GetRandomItem();

                Assert.AreNotSame(item1, item2, "Two randomly selected items should not be the same");
            }
           
            for (int i = 0; i < 1000; i++)
            {
                var item3 = catalogue.GetRandomItem(-1, 10001);
                Assert.IsInstanceOfType(item3, typeof(Item));
            }
            
            for (int i = 0;  i < 100; i++)
            {
                var item5 = catalogue.GetRandomItem(100, 200);
                Assert.IsTrue(item5.GetID() >= 100 && item5.GetID() <= 200, "Random items should be in range, ID was " + item5.GetID());
            }
            
        }

    }
}
