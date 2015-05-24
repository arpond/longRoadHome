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
        public const int LOCATION_MOVE_COST = 10;
        public const int SUBLOCATION_MOVE_COST = 2;
        
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
        /// Gets the result of the option selected
        /// </summary>
        /// <param name="gs">Current games state</param>
        /// <param name="optionSelected">The selected option</param>
        /// <returns>The string of the main option result</returns>
        public String GetOptionResult(GameState gs, int optionSelected)
        {
            EventModel em = gs.GetEM();

            String optionResult = em.GetOptionResult(optionSelected);
            return optionResult;
        }

        public List<String> GetOptionEffectResults(GameState gs, int optionSelected)
        {
            EventModel em = gs.GetEM();
            List<EventEffect> effects = em.GetOptionEffects(optionSelected);
            List<String> effectsResults = new List<string>();

            foreach (EventEffect effect in effects)
            {
                effectsResults.Add(effect.GetResult());
            }
            return effectsResults;
        }

        /// <summary>
        /// Resolve the current event with the option selected and the event modififer passed
        /// </summary>
        /// <param name="gs">Current Game state</param>
        /// <param name="optionSelected">The selected option</param>
        /// <param name="eventModifier">The event modifier</param>
        public void ResolveEvent(GameState gs,  int optionSelected,  double eventModifier)
        {
            EventModel em = gs.GetEM();
            PCModel pcm = gs.GetPCM();

            List<EventEffect> effects = em.GetOptionEffects(optionSelected); 
            
            foreach (EventEffect effect in effects)
            {
                effect.ResolveEffect(eventModifier, pcm);
            }
        }

        public int GetCurrentSublocation(GameState gs)
        {
            LocationModel lm = gs.GetLM();
            Sublocation sub = lm.GetSubLocation();
            if (sub != null)
            {
                return sub.GetSublocationID();
            }
            else
            {
                return 0;
            }
        }

        public int GetCurrentLocation(GameState gs)
        {
            LocationModel lm = gs.GetLM();
            return lm.GetCurentLocation().GetLocationID();
        }

        /// <summary>
        /// Calculates the movement cost
        /// </summary>
        /// <param name="gs"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public double CalculateMoveCost(GameState gs, int locationID)
        {
            LocationModel lm = gs.GetLM();
            int current = lm.GetCurentLocation().GetLocationID();
            var buttonAreas = lm.GetButtonAreas();
            
            System.Windows.Point source;
            System.Windows.Point target;
            if(buttonAreas.TryGetValue(current, out source) && buttonAreas.TryGetValue(locationID, out target))
            {
                double xDistance = source.X - target.X;
                double yDistance = source.Y - target.Y;
                double distance = Math.Sqrt(xDistance * xDistance + yDistance * yDistance);

                return distance/8;
            }

            return 0;
        }

        /// <summary>
        /// Checks if the Player in this game state can afford to move
        /// </summary>
        /// <param name="gs">The game state to check</param>
        /// <returns>If the player can afford to move</returns>
        public bool CanAffordMove(GameState gs, int cost)
        {
            PCModel pcm = gs.GetPCM();
            return pcm.CanAffordToMove(cost, cost);
        }

        /// <summary>
        /// Reduces the player in this game states resources (hunger and thirst) by the move cost 
        /// </summary>
        /// <param name="gs">Games State to alter</param>
        public void ReduceResourcesByMoveCost(GameState gs, int cost)
        {
            PCModel pcm = gs.GetPCM();
            pcm.ModifyPrimaryResource(PlayerCharacter.PlayerCharacter.HUNGER, -cost);
            pcm.ModifyPrimaryResource(PlayerCharacter.PlayerCharacter.THIRST, -cost);
        }

        public bool LocationVisited(GameState gs, int locationID)
        {
            LocationModel lm = gs.GetLM();
            return lm.LocationVisited(locationID);
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
        /// <returns>List of scavenged items</returns>
        public List<Item> ScavangeSubLocation(GameState gs)
        {
            LocationModel lm = gs.GetLM();
            PCModel pcm = gs.GetPCM();
            ItemCatalogue ic = pcm.GetItemCatalogue();
            List<Item> scavengedItems = new List<Item>();

            if (lm.IsScavenged() || pcm.GetInventory().IsInventoryFull())
            {
                return scavengedItems;
            }
            List<Item> itemSelection = new List<Item>();
            for (int i = 0; i < 100; i++ )
            {
                itemSelection.Add(ic.GetRandomItem());
            }
            scavengedItems = lm.Scavenge(itemSelection);
            foreach(Item item in scavengedItems)
            {
                pcm.ModifyInventory(item, item.GetAmount());
            }
            return scavengedItems;
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

        /// <summary>
        /// Gets the inventory as an arraylist of items
        /// </summary>
        /// <param name="gs">The current gamestate</param>
        /// <returns>Arraylist of the items</returns>
        public ArrayList GetInventory(GameState gs)
        {
            return gs.GetPCM().GetInventory().GetInventory();
        }

        /// <summary>
        /// Gets a list of current character resources
        /// </summary>
        /// <param name="gs">The current gamestate</param>
        /// <returns>List of current chracter resources</returns>
        public SortedList<string,int> GetPlayerCharacterResources(GameState gs)
        {
            PCModel pcm = gs.GetPCM();
            return pcm.GetPlayerCharacterResources();
        }

        /// <summary>
        /// Gets a list of current sublocations
        /// </summary>
        /// <param name="gs">The current gamestate</param>
        /// <returns>All sublocations at this location</returns>
        public List<Sublocation> GetCurrentSublocations(GameState gs)
        {
            LocationModel lm = gs.GetLM();
            var subs = lm.GetCurentLocation().GetSublocations().Values;
            List<Sublocation> sublocations = new List<Sublocation>();
            foreach(Sublocation sub in subs)
            {
                sublocations.Add(sub);
            }

            return sublocations;
        }

        /// <summary>
        /// Gets all the visited locations
        /// </summary>
        /// <param name="gs">The current gamestate</param>
        /// <returns>All visited locations</returns>
        public List<Location.Location> GetVisitedLocations(GameState gs)
        {
            LocationModel lm = gs.GetLM();
            IList<Location.Location> locs = lm.GetVisited().Values;
            var locations = new List<Location.Location>();
            foreach (Location.Location loc in locs)
            {
                locations.Add(loc);
            }
            return locations;
        }

        /// <summary>
        /// Gets all the unvisited locations
        /// </summary>
        /// <param name="gs">the current gamestate</param>
        /// <returns>All unvisited locations</returns>
        public List<DummyLocation> GetUnvisitedLocations(GameState gs)
        {
            LocationModel lm = gs.GetLM();
            IList<DummyLocation> locs = lm.GetUnvisited().Values;
            var locations = new List<DummyLocation>();
            foreach (Location.DummyLocation loc in locs)
            {
                locations.Add(loc);
            }
            return locations;
        }

        /// <summary>
        /// Gets the value of the inventory in this game state
        /// </summary>
        /// <param name="gs">The game state to get the inventory value from</param>
        /// <returns>The total value of the inventory in the game state</returns>
        public double GetValueOfInventory(GameState gs)
        {
            PCModel pcm = gs.GetPCM();
            return pcm.GetInventoryValue();
        }

        public string TryToMakeNewDiscovery(GameState gs)
        {
            LocationModel lm = gs.GetLM();
            DiscoveryModel dm = gs.GetDM();

            string discoveryText = dm.GetNewDiscovery(lm.GetVisited().Count);
            return discoveryText;
        }

        public SortedList<int, System.Windows.Point> GetButtonAreas(GameState gs)
        {
            return gs.GetLM().GetButtonAreas();
        }

        public System.Drawing.Bitmap GetWorldMap(GameState gs)
        {
            return gs.GetLM().GetWorldMap();
        }
    }

}
