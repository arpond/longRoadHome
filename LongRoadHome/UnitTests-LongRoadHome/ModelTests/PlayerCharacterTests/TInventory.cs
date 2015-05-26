using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
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
        }

        [TestCategory("PlayerCharacter"), TestCategory("Inventory"), TestMethod()]
        public void Inventory_AddItemInventoryFull()
        {
            Inventory inv = new Inventory();
            Assert.IsFalse(inv.IsInventoryFull(), "Inventory should not be full");
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
            Item item2 = new Item(itemStr2);
            Item item3 = new Item(itemStr3);
            Item item4 = new Item(itemStr4);
            Item item5 = new Item(itemStr5);
            Item item6 = new Item(itemStr6);
            Item item7 = new Item(itemStr7);
            Item item8 = new Item(itemStr8);
            Item item9 = new Item(itemStr9);
            Item item10 = new Item(itemStr10);
            Item item11 = new Item(itemStr11);
            Item item12 = new Item(itemStr12);
            Item item13 = new Item(itemStr13);
            Item item14 = new Item(itemStr14);
            Item item15 = new Item(itemStr15);
            Item item16 = new Item(itemStr16);
            Item itemFull = new Item(itemStr17);

            inv.AddItem(item1);
            Assert.IsFalse(inv.IsInventoryFull(), "Inventory should not be full");
            inv.AddItem(item2);
            inv.AddItem(item3);
            inv.AddItem(item4);
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
            Assert.IsFalse(inv.IsInventoryFull(), "Inventory should not be full");
            inv.AddItem(item16);

            Assert.IsTrue(inv.IsInventoryFull(), "Inventory should be full");
            Assert.IsFalse(inv.AddItem(itemFull), "You cannont add a new Item to a full inventory");
            Assert.IsTrue(inv.AddItem(item5), "You can add an exisiting item to a full inventory");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Inventory"), TestMethod()]
        public void Inventory_RemoveItem()
        {
            Inventory inv = new Inventory();
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr2 = "ID:2,Name:TestItem,Amount:2,Description:test item 2,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ",Requirements:2";
            String itemStr3 = "ID:3,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";

            Item item1a = new Item(itemStr1);
            Item item1b = new Item(itemStr1);
            Item item1c = new Item(itemStr1);
            Item item2 = new Item(itemStr2);
            Item item3 = new Item(itemStr3);

            inv.AddItem(item1a);
            inv.AddItem(item1b);
            inv.AddItem(item1c);
            inv.AddItem(item2);
            inv.AddItem(item3);

            ArrayList inventory = inv.GetInventory();
            int i = inventory.IndexOf(item1a);

            Assert.IsTrue(inventory.Contains(item1a), "Inventory should contrain the Item");
            Assert.AreEqual(3, inventory.Count, "Inventory should contain the same item only once");
            Assert.AreEqual(3, inv.GetAmount(item1a), "Should be 3 of the item");

            Item removed = inv.RemoveItem(i);

            Assert.AreEqual(item1a, removed, "Item returned when remove should match item added");
            Assert.AreEqual(2, inv.GetAmount(item1a), "Should now be 2 of the item");
            Assert.AreEqual(1, removed.GetAmount(), "1 Item was removed");

            removed = inv.RemoveItem(i);

            Assert.AreEqual(item1a, removed, "Item returned when remove should match item added");
            Assert.AreEqual(1, inv.GetAmount(item1a), "Should now be 1 of the item");
            Assert.AreEqual(1, removed.GetAmount(), "1 Item was removed");

            removed = inv.RemoveItem(i);

            Assert.AreEqual(item1a, removed, "Item returned when remove should match item added");
            Assert.AreEqual(0, inv.GetAmount(item1a), "Should now be 0 of the item");
            Assert.AreEqual(1, removed.GetAmount(), "1 Item was removed");

            removed = inv.RemoveItem(2);

            Assert.AreEqual(null, removed, "Item returned when remove should be null");
            Assert.AreEqual(0, inv.GetAmount(item1a), "Should be 0 of the item");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Inventory"), TestMethod()]
        public void Inventory_ParseToString()
        {
            Inventory inv = new Inventory();
            Inventory emptyInv = new Inventory();
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            String itemStr2 = "ID:2,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            String itemStr3 = "ID:3,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";

            String itemStr4 = "ID:1,Name:TestItem,Amount:3,Description:test item 3,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            String expected = Inventory.TAG + "#" + itemStr4 + "#" + itemStr2 + "#" + itemStr3;

            Item item1a = new Item(itemStr1);
            Item item1b = new Item(itemStr1);
            Item item1c = new Item(itemStr1);
            Item item2 = new Item(itemStr2);
            Item item3 = new Item(itemStr3);

            inv.AddItem(item1a);
            inv.AddItem(item1b);
            inv.AddItem(item1c);
            inv.AddItem(item2);
            inv.AddItem(item3);

            Assert.AreEqual(expected, inv.ParseToString(), "The expected inventory should match");
            Assert.AreEqual(Inventory.TAG, emptyInv.ParseToString(), "The expected inventory should match");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Inventory"), TestMethod()]
        public void Inventory_CheckStringValid()
        {
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr2 = "ID:2,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";
            String itemStr3 = "ID:3,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";

            String itemStr4 = "ID:1,Name:TestItem,Amount:3,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";

            String invalidItem = "ID:-1,Name:TestItem,Amount:3,Description:test item 3,ActiveEffect,PassiveEffect,Requirements";

            String expected = Inventory.TAG + "#" + itemStr4 + "#" + itemStr2 + "#" + itemStr3;
            String nonUniqueID = ItemCatalogue.TAG + ";" + itemStr1 + ";" + itemStr2 + ";" + itemStr3 + ";" + itemStr1;


            Assert.IsTrue(Inventory.IsValidInventory(Inventory.TAG), "empty inventory should be valid");
            Assert.IsTrue(Inventory.IsValidInventory(expected), "Standard inventory should be valid");
            Assert.IsFalse(Inventory.IsValidInventory(Inventory.TAG + "#"), "if an inventory has items it should have at least one item");
            Assert.IsFalse(Inventory.IsValidInventory("NotanInventory#" + itemStr1), "Not an inventory should be invalid");
            Assert.IsFalse(Inventory.IsValidInventory("#" + itemStr1), "No Tag should be invalid");
            Assert.IsFalse(Inventory.IsValidInventory(Inventory.TAG + itemStr1), "No # should be invalid");
            Assert.IsFalse(Inventory.IsValidInventory(Inventory.TAG + "#" + invalidItem), "Invalid item means invalid inventory");
            Assert.AreEqual(Inventory.IsValidInventory(Inventory.TAG + "#" + invalidItem), Item.IsValidItem(invalidItem), "Invalid item should match invalid inventory");
            Assert.IsFalse(Inventory.IsValidInventory(nonUniqueID), "Each ID should be unique");
        }

        [TestCategory("PlayerCharacter"), TestCategory("Inventory"), TestMethod()]
        public void Inventory_ParseFromString()
        {
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            String itemStr2 = "ID:2,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            String itemStr3 = "ID:3,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";

            String itemStr4 = "ID:1,Name:TestItem,Amount:3,Description:test item 3,ActiveEffect,PassiveEffect,Requirements,Icon:test.png";
            String expected = Inventory.TAG + "#" + itemStr4 + "#" + itemStr2 + "#" + itemStr3;
            
            Inventory parsedInv = new Inventory(expected);
            Inventory createdInv = new Inventory();
            
            Item item1a = new Item(itemStr1);
            Item item1b = new Item(itemStr1);
            Item item1c = new Item(itemStr1);
            Item item2 = new Item(itemStr2);
            Item item3 = new Item(itemStr3);

            createdInv.AddItem(item1a);
            createdInv.AddItem(item1b);
            createdInv.AddItem(item1c);
            createdInv.AddItem(item2);
            createdInv.AddItem(item3);

            Assert.IsTrue(Inventory.IsValidInventory(parsedInv.ParseToString()), "Parsed inventory should create valid string");
            Assert.AreEqual(expected, parsedInv.ParseToString(), "Parsed Inventory should match");

            ArrayList created = createdInv.GetInventory();
            ArrayList parsed = parsedInv.GetInventory();

            foreach (Item itemC in created)
            {
                int i = parsed.IndexOf(itemC);
                if (i >= 0)
                {
                    Item itemP = parsed[i] as Item;
                    Assert.AreEqual(itemP, itemC, "Items at each index should match");
                }
                else
                {
                    Assert.Fail("Both must contain the same items");
                }
            }
        }

        [TestCategory("PlayerCharacter"), TestCategory("Inventory"), TestMethod()]
        public void Inventory_TotalPassives()
        {
            String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements";
            String itemStr2 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ",Requirements:2";
            String itemStr3 = "ID:3,Name:TestItem,Amount:2,Description:test item 3,"
                            + "ActiveEffect:" + ActiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "10"
                            + ":" + ActiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "10"
                            + ",PassiveEffect:" + PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.SANITY + ":" + "0.9"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.HUNGER + ":" + "0.8"
                            + ":" + PassiveEffect.TAG + ":" + PlayerCharacter.THIRST + ":" + "0.8"
                            + ",Requirements:2";

            var emptyInv = new Inventory();
            var noPassives = new Inventory(Inventory.TAG + "#" + itemStr1);
            var singlePassive = new Inventory(Inventory.TAG + "#" + itemStr1 + "#" + itemStr2);
            var multiplePassives = new Inventory(Inventory.TAG + "#" + itemStr1 + "#" + itemStr2 + "#" + itemStr3);

            var calculatedPassives = new List<PassiveEffect>();

            calculatedPassives = emptyInv.GetAllPassives();
            Assert.AreEqual(0, calculatedPassives.Count, "Should be no pasives in  an empty inventory");

            calculatedPassives = noPassives.GetAllPassives();
            Assert.AreEqual(0, calculatedPassives.Count, "Should be no pasives in an inventory with no passives");

            calculatedPassives = singlePassive.GetAllPassives();
            var single = calculatedPassives[0];
            var singleTest = new PassiveEffect(PassiveEffect.TAG + ":" + PlayerCharacter.HEALTH + ":" + "0.8");

            Assert.AreEqual(1, calculatedPassives.Count, "Should only be a single passive");
            Assert.AreEqual(0.8, single.GetModifier(), 0.001, "modifier should be 0.8");
            Assert.AreEqual(singleTest.ParseToString(), single.ParseToString(), "PassiveEffects should match");

            calculatedPassives = multiplePassives.GetAllPassives();

            Assert.AreEqual(4, calculatedPassives.Count, "Should be exactly 4 passives");
            
            foreach (PassiveEffect calc in calculatedPassives)
            {
                switch (calc.GetResourceName())
                {
                    case PlayerCharacter.HEALTH:
                        Assert.AreEqual(0.8 * 0.8, calc.GetModifier(), 0.001, "Health modifier should be merged");
                        break;
                    case PlayerCharacter.HUNGER:
                        Assert.AreEqual(0.8, calc.GetModifier(), 0.001, "Hunger modifier should be 0.8");
                        break;
                    case PlayerCharacter.SANITY:
                        Assert.AreEqual(0.9, calc.GetModifier(), 0.001, "Sanity modifier should be 0.9");
                        break;
                    case PlayerCharacter.THIRST:
                        Assert.AreEqual(0.8, calc.GetModifier(), 0.001, "Thirst modifier should be 0.8");
                        break;
                }
            }
        }
    }
}
