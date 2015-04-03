using System;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PCModel
    {
        private PlayerCharacter currentPC;
        private ItemCatalogue itemCatalogue;
        private Inventory inventory;

        public PCModel(ref String pc, ref String catalogue)
        {
            throw new System.Exception("Not implemented");
        }
        public String ParseToString()
        {
            throw new System.Exception("Not implemented");
        }
        public void ModifyPrimaryResource(ref PrimaryResource resource, ref int amount)
        {
            throw new System.Exception("Not implemented");
        }
        public void ModifyInventory(ref Item item, ref int amount)
        {
            throw new System.Exception("Not implemented");
        }
        public PlayerCharacter GetPC()
        {
            throw new System.Exception("Not implemented");
        }
        public bool CheckForGameOver()
        {
            throw new System.Exception("Not implemented");
        }
        public void UseItem(ref int invSlot)
        {
            throw new System.Exception("Not implemented");
        }
        public bool ItemUsable(ref int invSlot)
        {
            throw new System.Exception("Not implemented");
        }

        private PlayerCharacter playerCharacter;

        private uk.ac.dundee.arpond.longRoadHome.Model.GameState gameState;

    }
}
