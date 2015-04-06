
using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class Inventory
    {
        private int size;
        private List<Item> inventory;

        public Inventory(ref String inv)
        {
            throw new System.Exception("Not implemented");
        }
        public bool InventoryFull()
        {
            throw new System.Exception("Not implemented");
        }
        public void AddItem(ref Item toAdd)
        {
            throw new System.Exception("Not implemented");
        }
        public Item RemoveItem(ref int invSlot)
        {
            throw new System.Exception("Not implemented");
        }
        public List<Item> GetInventory()
        {
            return this.inventory;
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
