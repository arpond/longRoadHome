using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PCModel
    {
        private PlayerCharacter currentPC;
        private ItemCatalogue itemCatalogue;
        private Inventory currentInventory;

        public PCModel(String pc, String inventory, String catalogue)
        {
            currentPC = new PlayerCharacter(pc);
            currentInventory = new Inventory(inventory);
            itemCatalogue = new ItemCatalogue(catalogue);
        }
        public String ParsePCToString()
        {
            return currentPC.ParseToString();
        }
        public String ParseInventoryToString()
        {
            return currentInventory.ParseToString();
        }
        public void ModifyPrimaryResource(PrimaryResource resource, int amount)
        {
            currentPC.AdjustResource(resource.GetName(), amount);
        }
        public void ModifyInventory(Item item, int amount)
        {
            if(amount > 0)
            {
                item.SetAmount(amount);
                currentInventory.AddItem(item);
            }
            else
            {
                for (int i = 0; i < amount; i++ )
                {
                    int invSlot = currentInventory.GetInventorySlot(item);
                    currentInventory.RemoveItem(invSlot);
                }    
            }
        }
        public PlayerCharacter GetPC()
        {
            return currentPC;
        }
        public bool IsGameOver()
        {
            return currentPC.IsDead();
        }
        public void UseItem(int invSlot)
        {
            Item toUse = currentInventory.RemoveItem(invSlot);
        }
        public bool ItemUsable(ref int invSlot)
        {
            throw new System.Exception("Not implemented");
        }
    }
}
