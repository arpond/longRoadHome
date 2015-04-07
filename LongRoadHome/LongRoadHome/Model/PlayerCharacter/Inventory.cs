
using System;
using System.Collections.Generic;
using System.Collections;

namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class Inventory
    {
        public const String TAG = "Inventory";

        private int size;
        private ArrayList inventory;

        public Inventory()
        {
            size = 16;
            inventory = new ArrayList();
        }

        /// <summary>
        /// Constructor which takes a valid inventory string and parses it into an inventory
        /// </summary>
        /// <param name="toParse">The string to parse into an inventory</param>
        public Inventory(String toParse)
        {
            size = 16;
            inventory = new ArrayList();

            String[] inventoryElements = toParse.Split('#');
            if (inventoryElements.Length > 1)
            {
                for (int i = 1; i < inventoryElements.Length; i++)
                {
                    Item temp = new Item(inventoryElements[i]);
                    inventory.Add(temp);
                }
            }
        }

        /// <summary>
        /// Checks if inventory is full
        /// </summary>
        /// <returns>bool of whether it is full or not</returns>
        public bool IsInventoryFull()
        {
            if (inventory.Count == size)
            {
                return true;
            }
            return false;
        }

        public int NumberOfUniqueItems()
        {
            return inventory.Count;
        }

        /// <summary>
        /// Trys to add an item to the inventory
        /// </summary>
        /// <param name="toAdd">The item to add to the inventory</param>
        /// <returns>If the item was succesfully added</returns>
        public bool AddItem(Item toAdd)
        {
           if (inventory.Contains(toAdd))
           {
               int i = inventory.IndexOf(toAdd);
               Item stored = inventory[i] as Item;
               stored.SetAmount(stored.GetAmount() + toAdd.GetAmount());
               inventory[i] = stored;
               return true;
           }
           else
           {
               if (inventory.Count >= size)
               {
                   return false;
               }
               inventory.Add(toAdd.Clone() as Item);
               return true;
           }
        }

        /// <summary>
        /// Removes an item from the inventory
        /// This is not complete remove if amount is more than 1
        /// </summary>
        /// <param name="invSlot">The inventory slot to remove an item from</param>
        /// <returns>A clone of the item removed with an amount of 1 (regardless of how many are stored)</returns>
        public Item RemoveItem(int invSlot)
        {
            if (invSlot >= inventory.Count)
            {
                return null;
            }

            Item stored = inventory[invSlot] as Item;
            Item item = stored.Clone() as Item;
            if (stored != null)
            {
                int amount = stored.GetAmount();
                if (amount == 1)
                {
                    inventory.RemoveAt(invSlot);
                    return item;
                }
                else
                {
                    stored.SetAmount(--amount);
                    inventory[invSlot] = stored;
                    item.SetAmount(1);
                    return item;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the Item in the inventory slot
        /// </summary>
        /// <param name="invSlot">Inventory slot of the item to get</param>
        /// <returns>Item to be gotten</returns>
        public Item GetItemSlot(int invSlot)
        {
            if (invSlot >= inventory.Count)
            {
                return null;
            }
            return inventory[invSlot] as Item;
        }

        /// <summary>
        /// Gets the amount of an item in the inventory
        /// </summary>
        /// <param name="toGetAmount">The item to get the amount of</param>
        /// <returns>The number of the item in the inventory</returns>
        public int GetAmount(Item toGetAmount)
        {
            if (inventory.Contains(toGetAmount))
            {
                int i = inventory.IndexOf(toGetAmount);
                Item stored = inventory[i] as Item;
                return stored.GetAmount();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Accessory method for the inventory
        /// </summary>
        /// <returns>The inventory stored</returns>
        public ArrayList GetInventory()
        {
            return this.inventory;
        }

        /// <summary>
        /// Checks if the inventory contains the item
        /// </summary>
        /// <param name="toCheck">The item to check for</param>
        /// <returns>Ifit contains it or not</returns>
        public bool Contains(Item toCheck)
        {
            return inventory.Contains(toCheck);
        }

        /// <summary>
        /// Gets the item IDs for all items in the inventory
        /// </summary>
        /// <returns>HashSet of IDs</returns>
        public HashSet<int> GetItemIDs()
        {
            var ids = new HashSet<int>();
            foreach(Item item in inventory)
            {
                ids.Add(item.GetID());
            }
            return ids;
        }

        /// <summary>
        /// Gets the inventory slot of an item
        /// Returns -1 if the item is not in the inventory
        /// </summary>
        /// <param name="toGet">The item to get the inventory slot of</param>
        /// <returns></returns>
        public int GetInventorySlot(Item toGet)
        {
            return inventory.IndexOf(toGet);
        }

        /// <summary>
        /// Checks if an inventory string is a valid inventory
        /// </summary>
        /// <param name="toTest">The string to test</param>
        /// <returns>bool representing if the string is a valid inventory or not</returns>
        public static bool IsValidInventory(String toTest)
        {
            List<int> idList = new List<int>();
            String[] inventoryElements = toTest.Split('#');
            if (inventoryElements[0] != TAG || inventoryElements.Length > 17)
            {
                return false;
            }
            if (inventoryElements.Length > 1)
            {
                for (int i = 1; i < inventoryElements.Length; i++)
                {
                    if (!Item.IsValidItem(inventoryElements[i]))
                    {
                        return false;
                    }
                    var itemElements = inventoryElements[i].Split(',');
                    var idElements = itemElements[0].Split(':');
                    int currID = Convert.ToInt32(idElements[1]);
                    if (idList.Contains(currID))
                    {
                        return false;
                    }
                    idList.Add(currID);
                }
            }
            return true;
        }

        /// <summary>
        /// Parses the current inventory to a string
        /// </summary>
        /// <returns>The inventory as a string suitable for saving</returns>
        public String ParseToString()
        {
            String parsed = TAG;
            foreach (Item item in inventory)
            {
                parsed += "#" + item.ParseToString();
            }
            return parsed;
        }

        /// <summary>
        /// Gets a list of all passive effects in the inventory
        /// Merges passive effects of the same type multiplicatively
        /// </summary>
        /// <returns>A list of the passive effects</returns>
        public List<PassiveEffect> GetAllPassives()
        {
            var inventoryPassives = new List<PassiveEffect>();
            foreach (Item item in inventory)
            {
                if (item.HasPassiveEffect())
                {
                    var itemPassives = item.GetPassiveEffects();
                    foreach (PassiveEffect itemPassive in itemPassives)
                    {
                        bool done = false;
                        foreach (PassiveEffect mergedPassive in inventoryPassives)
                        {
                            if (itemPassive.SamePassiveType(mergedPassive))
                            {
                                inventoryPassives.Remove(mergedPassive);
                                inventoryPassives.Add(mergedPassive.MergeEffect(itemPassive));
                                done = true;
                                break;
                            }
                        }
                        if (!done)
                        {
                            inventoryPassives.Add(itemPassive);
                        }
                    }
                }
            }
            return inventoryPassives;
        }
    }
}
