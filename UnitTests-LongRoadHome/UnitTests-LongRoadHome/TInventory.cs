using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests_LongRoadHome
{
    [TestClass]
    public class TInventory
    {
        [TestCategory("PlayerCharacter"), TestCategory("Inventory"), TestMethod()]
        public void Inventory_AddItem()
        {
            Inventory inv = new Inventory();
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ",Requirements:2";
            Item item1 = new Item(itemStr1);

            inv.AddItem(item1);

            Assert.IsTrue(inv.GetInventory().Contains(item1), "Inventory should contrain the Item");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Inventory"), TestMethod()]
        public void Inventory_AddItemDuplicate()
        {
            Inventory inv = new Inventory();
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ",Requirements:2";
            String itemStr2 = "ID:2,Name:TestItem,Amount:2,Description:test item 2,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ",Requirements:2";
            String itemStr3 = "ID:3,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr4 = "ID:4,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr5 = "ID:5,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr6 = "ID:6,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr7 = "ID:7,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr8 = "ID:8,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr9 = "ID:9,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr10 = "ID:10,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr11 = "ID:11,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr12 = "ID:12,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr13 = "ID:13,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr14 = "ID:14,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr15 = "ID:15,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr16 = "ID:16,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr17 = "ID:17,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";

            Item item1 = new Item(itemStr1);
            Item item2 = new Item(itemStr1);
            Item item3 = new Item(itemStr1);
            Item item4 = new Item(itemStr2);
            Item item5 = new Item(itemStr3);
            Item item6 = new Item(itemStr4);
            Item item7 = new Item(itemStr5);
            Item item8 = new Item(itemStr6);
            Item item9 = new Item(itemStr7);
            Item item10 = new Item(itemStr8);
            Item item11 = new Item(itemStr9);
            Item item12 = new Item(itemStr10);
            Item item13 = new Item(itemStr11);
            Item item14 = new Item(itemStr12);
            Item item15 = new Item(itemStr13);
            Item item16 = new Item(itemStr14);
            Item item17 = new Item(itemStr15);
            Item item18 = new Item(itemStr16);
            Item itemFull = new Item(itemStr17);

            inv.AddItem(item1);
            inv.AddItem(item2);
            inv.AddItem(item3);

            ArrayList inventory = inv.GetInventory();
            int i = inventory.IndexOf(item1);

            Assert.IsTrue(inventory.Contains(item1), "Inventory should contrain the Item");
            Assert.AreEqual(1, inventory.Count, "Inventory should contain the same item only once");
            Assert.AreEqual(3, (inventory[i] as Item).GetAmount(), "Should be 3 of the item");

            inv.AddItem(item4);
            i = inventory.IndexOf(item4);
            Assert.IsTrue(inventory.Contains(item4), "Inventory should contain Item2");
            Assert.AreEqual(2, inventory.Count, "Inventory should contain the two items only once");
            Assert.AreEqual(2, (inventory[i] as Item).GetAmount(), "Should be 2 of item2");

            inv.AddItem(item5);
            inv.AddItem(item6);
            inv.AddItem(item7);
            inv.AddItem(item8);
            inv.AddItem(item9);
            inv.AddItem(item10);
            inv.AddItem(item11);
            inv.AddItem(item12);
            inv.AddItem(item13);
            inv.AddItem(item14);
            inv.AddItem(item15);
            inv.AddItem(item16);
            inv.AddItem(item17);
            inv.AddItem(item18);

            Assert.IsFalse(inv.AddItem(itemFull), "You cannont add a new Item to a full inventory");
            Assert.IsTrue(inv.AddItem(item5), "You can add an exisiting item to a full inventory");
        }
    }
}
