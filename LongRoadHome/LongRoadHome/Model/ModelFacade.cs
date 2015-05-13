using System;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using System.Collections.Generic;
namespace uk.ac.dundee.arpond.longRoadHome.Model
{
    public class ModelFacade
    {
        private const int MOVE_COST = 10;

        public void TriggerNewEvent(GameState gs,  float eventModifier)
        {
            throw new System.Exception("Not implemented");
        }
        public void TriggerEvent(GameState gs,  int eventID)
        {
            throw new System.Exception("Not implemented");
        }
        public void ResolveEvent(GameState gs,  int optionSelected)
        {
            throw new System.Exception("Not implemented");
        }

        /// <summary>
        /// Checks if the Player in this game state can afford to move
        /// </summary>
        /// <param name="gs">The game state to check</param>
        /// <returns>If the player can afford to move</returns>
        public bool CanAffordMove(GameState gs)
        {
            PCModel pcm = gs.GetPCM();
            return pcm.CanAffordToMove(MOVE_COST, MOVE_COST);
        }

        /// <summary>
        /// Reduces the player in this game states resources (hunger and thirst) by the move cost 
        /// </summary>
        /// <param name="gs">Games State to alter</param>
        public void ReduceResourcesByMoveCost(GameState gs)
        {
            PCModel pcm = gs.GetPCM();
            pcm.ModifyPrimaryResource(PlayerCharacter.PlayerCharacter.HUNGER, -MOVE_COST);
            pcm.ModifyPrimaryResource(PlayerCharacter.PlayerCharacter.THIRST, -MOVE_COST);
        }

        /// <summary>
        /// Changes the current location of the player to the new locaiton
        /// </summary>
        /// <param name="gs">The game state to change the player</param>
        /// <param name="locationID">The location to change to</param>
        /// <returns>If the move was succesful</returns>
        public bool ChangeLocation(GameState gs,  int locationID)
        {   
            LocationModel lm = gs.GetLM();
            return lm.MoveToLocation(locationID);
        }

        /// <summary>
        /// Changes the current sublocation of the player to the new sublocation
        /// </summary>
        /// <param name="gs">The games state to change the player in</param>
        /// <param name="subLocationID">The sublocation to change to</param>
        /// <returns></returns>
        public bool ChangeSubLocation(GameState gs,  int subLocationID)
        {
            LocationModel lm = gs.GetLM();
            return lm.ChangeSubLocation(subLocationID);
        }
        public bool ItemUsable(GameState gs,  int invSlot)
        {
            throw new System.Exception("Not implemented");
        }
        public void UseItem(GameState gs,  int invSlot)
        {
            throw new System.Exception("Not implemented");
        }
        public void DiscardItem(GameState gs,  int invSlot)
        {
            throw new System.Exception("Not implemented");
        }

        /// <summary>
        /// Scavenges a sublocation putting found items into the inventory of this PC
        /// </summary>
        /// <param name="gs">The game state to modify</param>
        /// <returns>If scavenging was succesful</returns>
        public bool ScavangeSubLocation(GameState gs)
        {
            LocationModel lm = gs.GetLM();
            PCModel pcm = gs.GetPCM();
            ItemCatalogue ic = pcm.GetItemCatalogue();

            if (lm.IsScavenged() || pcm.GetInventory().IsInventoryFull())
            {
                return false;
            }
            List<Item> itemSelection = new List<Item>();
            for (int i = 0; i < 100; i++ )
            {
                itemSelection.Add(ic.GetRandomItem());
            }
            List<Item> scavengedItems = lm.Scavenge(itemSelection);
            foreach(Item item in scavengedItems)
            {
                pcm.ModifyInventory(item, item.GetAmount());
            }
            return true;
        }

        /// <summary>
        /// Checks if the current sublocation is scavenged
        /// </summary>
        /// <param name="gs">Current Game State</param>
        /// <returns>If current sub location is scavenged</returns>
        public bool IsScavenged(GameState gs)
        {
            LocationModel lm = gs.GetLM();
            return lm.IsScavenged();
        }

        /// <summary>
        /// Checks if the sublocation ID is scavenged
        /// </summary>
        /// <param name="gs">Current Game State</param>
        /// <param name="subLocID">Sublocation ID to check</param>
        /// <returns>If the sub location ID is scavenged</returns>
        public bool IsScavenged(GameState gs, int subLocID)
        {
            LocationModel lm = gs.GetLM();
            return lm.IsScavenged(subLocID);
        }

    }

}
