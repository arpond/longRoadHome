
using System;
using System.Collections.Generic;
using System.Collections;

namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class Inventory
    {
        private int size;
        private ArrayList inventory;

        public Inventory()
        {
            size = 16;
            inventory = new ArrayList();
        }

        public Inventory(String toParse)
        {
            size = 16;
            inventory = new ArrayList();
        }

        public bool IsInventoryFull()
        {
            if (inventory.Count == size)
            {
                return true;
            }
            return false;
        }

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
               inventory.Add(toAdd);
               return true;
           }
        }

        public Item RemoveItem(int invSlot)
        {
            throw new System.Exception("Not implemented");
        }
        public ArrayList GetInventory()
        {
            return this.inventory;
        }

        public int GetInventorySlot(Item toGet)
        {
            return inventory.IndexOf(toGet);
        }

        public String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }
        public List<PassiveEffect> GetAllPassives()
        {
            throw new System.Exception("Not implemented");
        }

        private PCModel pCModel;
        private Item item;

    }
}
