using System;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter
{
    public class PCModel
    {
        private PlayerCharacter currentPC;
        private ItemCatalogue itemCatalogue;
        private Inventory currentInventory;

        /// <summary>
        /// Constructor for a standard PCModel
        /// Creates PC and Inv with standard values
        /// </summary>
        /// <param name="catalogue">Item Catalogue String from file</param>
        public PCModel(String catalogue)
        {
            currentPC = new PlayerCharacter();
            currentInventory = new Inventory();
            itemCatalogue = new ItemCatalogue(catalogue);
        }

        /// <summary>
        /// Constructor for a PCModel with strings from file
        /// </summary>
        /// <param name="pc">PC string</param>
        /// <param name="inventory">Inventory string</param>
        /// <param name="catalogue">Item catalogue string</param>
        public PCModel(String pc, String inventory, String catalogue)
        {
            currentPC = new PlayerCharacter(pc);
            currentInventory = new Inventory(inventory);
            itemCatalogue = new ItemCatalogue(catalogue);
        }

        /// <summary>
        /// Parses the current PC to a String
        /// </summary>
        /// <returns>String representing the PC</returns>
        public String ParsePCToString()
        {
            return currentPC.ParseToString();
        }

        /// <summary>
        /// Parses the current inventory to a string
        /// </summary>
        /// <returns>String representing the inventory</returns>
        public String ParseInventoryToString()
        {
            return currentInventory.ParseToString();
        }

        /// <summary>
        /// Modifies a primary resource of the current PC
        /// </summary>
        /// <param name="resource">Primary resource to modify</param>
        /// <param name="amount">Amount to modify the resource by</param>
        public void ModifyPrimaryResource(PrimaryResource resource, int amount)
        {
            currentPC.AdjustResource(resource.GetName(), amount);
        }

        /// <summary>
        /// Modifies the inventory
        /// </summary>
        /// <param name="item">The item to modify</param>
        /// <param name="amount">The amount to modify the item by</param>
        public void ModifyInventory(Item item, int amount)
        {
            if(amount > 0)
            {
                item.SetAmount(amount);
                currentInventory.AddItem(item);
            }
            else
            {
                for (int i = 0; i < Math.Abs(amount); i++ )
                {
                    int invSlot = currentInventory.GetInventorySlot(item);
                    if (invSlot != -1)
                    {
                        currentInventory.RemoveItem(invSlot);
                    }
                }    
            }
        }

        /// <summary>
        /// Accessor method for current PC
        /// </summary>
        /// <returns>Current PC</returns>
        public PlayerCharacter GetPC()
        {
            return currentPC;
        }

        /// <summary>
        /// Accessor method for current inventory
        /// </summary>
        /// <returns>Current Inventory</returns>
        public Inventory GetInventory()
        {
            return currentInventory;
        }

        /// <summary>
        /// Accessor method for Item Catalogue
        /// </summary>
        /// <returns>Item Catalogue</returns>
        public ItemCatalogue GetItemCatalogue()
        {
            return itemCatalogue;
        }

        /// <summary>
        /// Checks if a string is a valid item catalogue
        /// </summary>
        /// <param name="catalogue">String to check</param>
        /// <returns>If the string is a valid item catalogue</returns>
        public static bool IsValidItemCatalogue(String catalogue)
        {
            return ItemCatalogue.IsValidItemCatalogue(catalogue);
        }

        /// <summary>
        /// Checks if the Strings passed are valid
        /// </summary>
        /// <param name="pc">PC String to check</param>
        /// <param name="inventory">Inventory string to check</param>
        /// <param name="catalogue">Item Catalogue string to check</param>
        /// <returns>If all the strings are valid</returns>
        public static bool IsValidPCModel(String pc, String inventory, String catalogue)
        {
            return PlayerCharacter.IsValidPC(pc) && Inventory.IsValidInventory(inventory) && ItemCatalogue.IsValidItemCatalogue(catalogue);
        }

        /// <summary>
        /// Checks if the current PC has 0 in any primary resource
        /// </summary>
        /// <returns>If the Game over condition has been met</returns>
        public bool IsGameOver()
        {
            return currentPC.IsDead();
        }

        /// <summary>
        /// Attempts to use the item in the inventory slot
        /// </summary>
        /// <param name="invSlot">Slot of the item to use</param>
        /// <returns>bool if the use was succesful</returns>
        public bool UseItem(int invSlot)
        {
            Item toUse = currentInventory.RemoveItem(invSlot);
            if (toUse.HasRequirements() && !toUse.CheckReqs(currentInventory.GetItemIDs()))
            {
                currentInventory.AddItem(toUse);
                return false;
            }
            List<ActiveEffect> effects = toUse.GetActiveEffects();
            foreach (ActiveEffect effect in effects)
            {
                effect.ResolveEffect(1.0f, this);
            }
            return true;
        }

        /// <summary>
        /// Checks if an item is usable in the inventory slot
        /// </summary>
        /// <param name="invSlot">Slot of the item to check</param>
        /// <returns>bool if the item is usable</returns>
        public bool ItemUsable(int invSlot)
        {
            Item toCheck = currentInventory.GetItemSlot(invSlot);
            if (toCheck.HasRequirements() && !toCheck.CheckReqs(currentInventory.GetItemIDs()))
            {
                return false;
            }
            return true;
        }
    }
}
