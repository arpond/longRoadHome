using System;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace UnitTests_LongRoadHome
{
    [TestClass]
    public class TPCModel
    {
        PCModel pcm;
        PCModel std_pcm;

        String itemStr1 = "ID:1,Name:TestItem,Amount:1,Description:test item 1,ActiveEffect,PassiveEffect,Requirements";
        String itemStr2 = "ID:2,Name:TestItem,Amount:1,Description:test item 2,ActiveEffect,PassiveEffect,Requirements:1";
        String itemStr3 = "ID:3,Name:TestItem,Amount:1,Description:test item 3,ActiveEffect,PassiveEffect,Requirements:1:2";
        String itemStr4 = "ID:4,Name:TestItem,Amount:1,Description:test item 4,ActiveEffect,PassiveEffect,Requirements:10";

        String pcStr, invStr, catalogue;
        Item item1, item2, item3, item4;
        List<Item> items = new List<Item>();

        Random rnd = new Random();

        [TestInitialize()]
        public void Setup()
        {
            pcStr = PlayerCharacter.HEALTH + ":80:1," + PlayerCharacter.HUNGER + ":50:1,"
             + PlayerCharacter.THIRST + ":60:1," + PlayerCharacter.SANITY + ":70:1";
            invStr = Inventory.TAG + "#" + itemStr1 + "#" + itemStr2 + "#" + itemStr3;
            catalogue = ItemCatalogue.TAG + ";" + itemStr1 + ";" + itemStr2 + ";" + itemStr3 + ";" + itemStr4;
            std_pcm = new PCModel(catalogue);
            pcm = new PCModel(pcStr, invStr, catalogue);

            item1 = new Item(itemStr1);
            item2 = new Item(itemStr2);
            item3 = new Item(itemStr3);
            item4 = new Item(itemStr4);
            for (int i = 1;  i<21; i++)
            {
                Item tmp = new Item(makeItemStr(i));
                items.Add(tmp);
            }
        }

        private String makeItemStr(int id)
        {
            String item = "ID:" + id + ",Name:TestItem,Amount:1,Description:test item " + id + ",ActiveEffect,PassiveEffect,Requirements";
            int requirements = rnd.Next(4);
            for (int i = 0; i < requirements; i++ )
            {
                item += ":" + rnd.Next(1, 21);
            }
            return item;
        }

        [TestCategory("PlayerCharacter"), TestCategory("PCModel"), TestMethod()]
        public void PCModel_StandardConstructor()
        {
            var pc = std_pcm.GetPC();
            var inv = std_pcm.GetInventory();
            var ic = std_pcm.GetItemCatalogue();
            
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.HEALTH), "Health Value Incorrect");
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.HUNGER), "Hunger Value Incorrect");
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.THIRST), "Sanity Value Incorrect");
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.SANITY), "Thirst Value Incorrect");

            Assert.AreEqual(itemStr1, ic.GetItem(1).ParseToString(), "Item 1 should be in the catalogue");
            Assert.AreEqual(itemStr2, ic.GetItem(2).ParseToString(), "Item 2 should be in the catalogue");
            Assert.AreEqual(itemStr3, ic.GetItem(3).ParseToString(), "Item 3 should be in the catalogue");
            Assert.AreEqual(itemStr4, ic.GetItem(4).ParseToString(), "Item 4 should be in the catalogue");
            Assert.AreEqual(null, ic.GetItem(5), "Item 5 should not be in the catalogue");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PCModel"), TestMethod()]
        public void PCModel_FullConstructor()
        {
            var pc = pcm.GetPC();
            var inv = pcm.GetInventory();
            var ic = pcm.GetItemCatalogue();

            Assert.AreEqual(80, pc.GetResource(PlayerCharacter.HEALTH), "Health Value Incorrect");
            Assert.AreEqual(50, pc.GetResource(PlayerCharacter.HUNGER), "Hunger Value Incorrect");
            Assert.AreEqual(60, pc.GetResource(PlayerCharacter.THIRST), "Sanity Value Incorrect");
            Assert.AreEqual(70, pc.GetResource(PlayerCharacter.SANITY), "Thirst Value Incorrect");

            Assert.IsTrue(inv.Contains(items[0]), "Inventory should have item1");
            Assert.IsTrue(inv.Contains(items[1]), "Inventory should have item2");
            Assert.IsTrue(inv.Contains(items[2]), "Inventory should have item3");
            Assert.IsFalse(inv.Contains(items[3]), "Inventory should not have item4");
            Assert.AreEqual(invStr, inv.ParseToString(), "Parsed Inventory should match");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PCModel"), TestMethod()]
        public void PCModel_ModififyPrimaryResource()
        {
            var pc = pcm.GetPC();
            var pr1 = new PrimaryResource(10, PlayerCharacter.HEALTH);
            var pr2 = new PrimaryResource(20, PlayerCharacter.HEALTH);
            var pr3 = new PrimaryResource(-30, PlayerCharacter.HEALTH);
            var pr4 = new PrimaryResource(-100, PlayerCharacter.HEALTH);

            pcm.ModifyPrimaryResource(pr1, pr1.GetAmount());
            Assert.AreEqual(90, pc.GetResource(PlayerCharacter.HEALTH), "Health should be incremented by 10");

            pcm.ModifyPrimaryResource(pr2, pr2.GetAmount());
            Assert.AreEqual(100, pc.GetResource(PlayerCharacter.HEALTH), "Health should be capped at 100");

            pcm.ModifyPrimaryResource(pr3, pr3.GetAmount());
            Assert.AreEqual(70, pc.GetResource(PlayerCharacter.HEALTH), "Health should be reduced by 30");

            pcm.ModifyPrimaryResource(pr4, pr4.GetAmount());
            Assert.AreEqual(0, pc.GetResource(PlayerCharacter.HEALTH), "Health should be capped at 0");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PCModel"), TestMethod()]
        public void PCModel_ModififyInventory()
        {
            var inventory = pcm.GetInventory();

            Assert.IsTrue(inventory.Contains(items[0]), "Inventory should have item1");
            Assert.IsTrue(inventory.Contains(items[1]), "Inventory should have item2");
            Assert.IsTrue(inventory.Contains(items[2]), "Inventory should have item3");
            Assert.IsFalse(inventory.Contains(items[3]), "Inventory should not have item4");

            Assert.AreEqual(1, inventory.GetAmount(items[0]), "Should have one of item1");
            Assert.AreEqual(1, inventory.GetAmount(items[1]), "Should have one of item2");
            Assert.AreEqual(1, inventory.GetAmount(items[2]), "Should have one of item3");
            Assert.AreEqual(0, inventory.GetAmount(items[3]), "Should have zero of item4");

            pcm.ModifyInventory(items[0], 1);
            Assert.AreEqual(2, inventory.GetAmount(items[0]), "Should have two of item1");
            pcm.ModifyInventory(items[0], -1);
            Assert.AreEqual(1, inventory.GetAmount(items[0]), "Should have one of item1");
            pcm.ModifyInventory(items[0], -1);
            Assert.AreEqual(0, inventory.GetAmount(items[0]), "Should have zero of item1");
            pcm.ModifyInventory(items[0], -1);
            Assert.AreEqual(0, inventory.GetAmount(items[0]), "Should have zero of item1");
            pcm.ModifyInventory(items[0], -1);
            Assert.AreEqual(0, inventory.GetAmount(items[0]), "Should have zero of item1");
            pcm.ModifyInventory(items[0], 4);
            Assert.AreEqual(4, inventory.GetAmount(items[0]), "Should have four of item1");
            pcm.ModifyInventory(items[0], -6);
            Assert.AreEqual(0, inventory.GetAmount(items[0]), "Should have zero of item1");
            pcm.ModifyInventory(items[0], 4);
            Assert.AreEqual(4, inventory.GetAmount(items[0]), "Should have four of item1");
            
            for (int i = 3; i<items.Count; i++)
            {
                pcm.ModifyInventory(items[i], i);
                if (i < 16)
                {
                    Assert.IsTrue(inventory.Contains(items[i]), "Inventory should have item" + (i + 1));
                    Assert.AreEqual(i, inventory.GetAmount(items[i]), "Should have " + i + " of item" + (i+1));
                }
                else
                {
                    Assert.IsFalse(inventory.Contains(items[i]), "Inventory should not have item" + (i + 1));
                    Assert.IsTrue(inventory.IsInventoryFull(), "Inventory should be full");
                }
            }
        }

        [TestCategory("PlayerCharacter"), TestCategory("PCModel"), TestMethod()]
        public void PCModel_ItemUsable()
        {
            var inventory = pcm.GetInventory();
            Assert.IsTrue(pcm.ItemUsable(0), "Item has no requirements");
            Assert.IsTrue(pcm.ItemUsable(1), "Item has a single requirement");
            Assert.IsTrue(pcm.ItemUsable(2), "Item has multiple requirements");

            pcm.ModifyInventory(item4, 1);
            Assert.IsFalse(pcm.ItemUsable(3), "Item requriements should not be met");
            pcm.ModifyInventory(items[9], 1);
            Assert.IsTrue(pcm.ItemUsable(3), "Item requriement should now be met");
            pcm.ModifyInventory(items[9], -1);
            Assert.IsFalse(pcm.ItemUsable(3), "Item requriements should not be met");
        }

        [TestCategory("PlayerCharacter"), TestCategory("PCModel"), TestMethod()]
        public void PCModel_ModifyInventoryRandomly()
        {
            var inventory = pcm.GetInventory();
            HashSet<int> ids;
            ArrayList inv;
            for (int i = 0; i < 10000; i++)
            {
                int slotNum, currAmount, modifier, expected;
                Item item, before;
                bool addNew = false;

                // If rnd less than 30 or inventory is empty
                if (rnd.Next(1, 101) < 30 || inventory.NumberOfUniqueItems() == 0)
                {
                    // Get an item from the list
                    item = items[rnd.Next(0, 20)];
                    // If it is in the inventory
                    if (inventory.Contains(item))
                    {
                        // slot num is the items slot, item is the inventory version of the item, curr amount is the number in the inv
                        slotNum = inventory.GetInventorySlot(item);
                        item = (Item)inventory.GetItemSlot(slotNum).Clone();
                        currAmount = item.GetAmount();
                    }
                    // Else it is not
                    else
                    {
                        // slot num is the next free slot, curr amount is 0
                        slotNum = inventory.NumberOfUniqueItems();
                        currAmount = 0;
                        addNew = true;
                    }
                }
                // Else inventory is not empty and rnd is not less than 30
                else
                {
                    // Slot num is a random item  in the inventory, item is the item at that slot, curr amount is the number in the inv
                    slotNum = rnd.Next(0, inventory.NumberOfUniqueItems());
                    item = (Item)inventory.GetItemSlot(slotNum).Clone();
                    currAmount = item.GetAmount();
                }

                // Random value between 1 and 10
                modifier = rnd.Next(1, 11);

                // 50% chance that it is negative
                if (rnd.Next(1, 101) < 50)
                {
                    modifier = -modifier;
                }

                before = (Item)item.Clone();

                if (addNew && inventory.IsInventoryFull())
                {
                    // Modify the inventory
                    pcm.ModifyInventory(item, modifier);
                    // Expected value is 0
                    expected = 0;
                }
                else
                {
                    // Modify the inventory
                    pcm.ModifyInventory(item, modifier);
                    // Expected value is the curr amount + mod
                    expected = currAmount + modifier;
                }

                // If there are no items expected
                if (expected <= 0 && !addNew)
                {
                    Assert.IsFalse(inventory.Contains(item), "Inventory should not have the item"
                        + " Expected: " + expected + " AddNew: " + addNew
                        + " currAmount:" + currAmount + " Modifier:" + modifier + " "
                        + " Before: " + before.ParseToString() + " After: " + item.ParseToString());
                    Assert.AreEqual(0, inventory.GetAmount(item), "Should have zero of the item"
                        + " Expected: " + expected + " AddNew: " + addNew
                        + " currAmount:" + currAmount + " Modifier:" + modifier + " "
                        + " Before: " + before.ParseToString() + " After: " + item.ParseToString());
                    Assert.IsFalse(inventory.IsInventoryFull(), "Inventory should not be full after item removal"
                        + " Expected: " + expected + " AddNew: " + addNew
                        + " currAmount:" + currAmount + " Modifier:" + modifier + " "
                        + " Before: " + before.ParseToString() + " After: " + item.ParseToString());
                }
                else if (expected <= 0 && addNew)
                {
                    Assert.IsFalse(inventory.Contains(item), "Inventory should not have the item"
                       + " Expected: " + expected + " AddNew: " + addNew
                       + " currAmount:" + currAmount + " Modifier:" + modifier + " "
                       + " Before: " + before.ParseToString() + " After: " + item.ParseToString());
                    Assert.AreEqual(0, inventory.GetAmount(item), "Should have zero of the item"
                        + " Expected: " + expected + " AddNew: " + addNew
                        + " currAmount:" + currAmount + " Modifier:" + modifier + " "
                        + " Before: " + before.ParseToString() + " After: " + item.ParseToString());
                }
                else
                {
                    Assert.IsTrue(inventory.Contains(item), "Inventory should have the item"
                        + " Expected: " + expected + " AddNew: " + addNew
                        + " currAmount:" + currAmount + " Modifier:" + modifier + " "
                        + " Before: " + before.ParseToString() + " After: " + item.ParseToString());
                    Assert.AreEqual(expected, inventory.GetAmount(item), "Should be " + expected + " of the item"
                        + " Expected: " + expected + " AddNew: " + addNew
                        + " currAmount:" + currAmount + " Modifier:" + modifier + " "
                        + " Before: " + before.ParseToString() + " After: " + item.ParseToString());
                }

                ids = inventory.GetItemIDs();
                inv = inventory.GetInventory();

                foreach(Item tmp in inv)
                {
                    int slot = inventory.GetInventorySlot(tmp);
                    Assert.AreEqual(tmp.CheckReqs(ids), pcm.ItemUsable(slot), "Usability should match item reqs");
                }
            }
        }
    }
}
