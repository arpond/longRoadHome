using System;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using System.Collections.Generic;
using System.Collections;
namespace uk.ac.dundee.arpond.longRoadHome.Model
{
    public class ModelFacade
    {
        private const int MOVE_COST = 10;

        
        /// <summary>
        /// Sets a new random event as the current event and returns it
        /// </summary>
        /// <param name="gs">The current game state</param>
        /// <returns>The new current event</returns>
        public Event GetNewRandomEvent(GameState gs)
        {
            EventModel em = gs.GetEM();
            em.FetchNewCurrentEvent();
            return em.GetCurrentEvent();
        }

        /// <summary>
        /// Sets a specific event as the current event
        /// </summary>
        /// <param name="gs">The current game state</param>
        /// <param name="eventID">The event ID of the specific event</param>
        /// <returns>The new current event</returns>
        public Event GetNewSpecificEvent(GameState gs,  int eventID)
        {
            EventModel em = gs.GetEM();
            em.FetchSpecificEvent(eventID);
            return em.GetCurrentEvent();
        }

        /// <summary>
        /// Gets ths current event text
        /// </summary>
        /// <param name="gs">Current Game state</param>
        /// <returns>The current event text</returns>
        public String GetCurrentEventText(GameState gs)
        {
            EventModel em = gs.GetEM();
            return em.GetCurrentEventText();
        }
        
        /// <summary>
        /// Gets the current option text
        /// </summary>
        /// <param name="gs">Current game state</param>
        /// <returns>The curren event option text</returns>
        public List<String> GetCurrentEventOptionText(GameState gs)
        {
            EventModel em = gs.GetEM();
            return em.GetCurrentEventOptionsText();
        }

        /// <summary>
        /// Resolve the current event with the option selected and the event modififer passed
        /// </summary>
        /// <param name="gs">Current Game state</param>
        /// <param name="optionSelected">The selected option</param>
        /// <param name="eventModifier">The event modifier</param>
        public void ResolveEvent(GameState gs,  int optionSelected,  float eventModifier)
        {
            EventModel em = gs.GetEM();
            PCModel pcm = gs.GetPCM();

            List<EventEffect> effects = em.GetOptionEffects(optionSelected); 
            
            foreach (EventEffect effect in effects)
            {
                effect.ResolveEffect(eventModifier, pcm);
            }
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

        /// <summary>
        /// Checks if the item in the inventory slot is useable
        /// </summary>
        /// <param name="gs">The games state to check</param>
        /// <param name="invSlot">The inventory slot to check</param>
        /// <returns>If the slot is useable</returns>
        public bool ItemUsable(GameState gs,  int invSlot)
        {
            PCModel pcm = gs.GetPCM();
            return pcm.ItemUsable(invSlot);
        }

        /// <summary>
        /// Uses the item in the inventory slot specified
        /// </summary>
        /// <param name="gs">The games state to use the item on</param>
        /// <param name="invSlot">The inventory slot to use</param>
        /// <returns>If the item was succesfully used</returns>
        public bool UseItem(GameState gs,  int invSlot)
        {
            PCModel pcm = gs.GetPCM();
            return pcm.UseItem(invSlot);
        }

        /// <summary>
        /// Discards an Item from an inventory slot
        /// </summary>
        /// <param name="gs">The game state to remove the item from</param>
        /// <param name="invSlot">The inventory slot to remove the item from</param>
        /// <returns>If the item was succesfully removed</returns>
        public bool DiscardItem(GameState gs,  int invSlot)
        {
            PCModel pcm = gs.GetPCM();
            return pcm.DiscardItem(invSlot);
        }

        /// <summary>
        /// Checks if game is over
        /// </summary>
        /// <param name="gs">The current game state</param>
        /// <returns>If the game is over</returns>
        public bool IsGameOver(GameState gs)
        {
            PCModel pcm = gs.GetPCM();
            return pcm.IsGameOver(); 
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


        public ArrayList GetInventory(GameState gs)
        {
            return gs.GetPCM().GetInventory().GetInventory();
        }
    }

}
