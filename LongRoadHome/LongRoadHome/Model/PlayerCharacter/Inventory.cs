
using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class Inventory
    {
        private int size;
        private ArrayList<Item> inventory;

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
        public ArrayList<Item> GetInventory()
        {
            return this.inventory;
        }
        public String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }
        public ArrayList<PassiveMod> GetAllPassives()
        {
            throw new System.Exception("Not implemented");
        }

        private PCModel pCModel;
        private Item item;

    }
}
