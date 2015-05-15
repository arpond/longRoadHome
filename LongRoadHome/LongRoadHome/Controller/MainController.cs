using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using uk.ac.dundee.arpond.longRoadHome.Model;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.View;
namespace uk.ac.dundee.arpond.longRoadHome.Controller
{
    public class MainController
    {
        private DifficultyController dc;
        private GameState gs;
        private ModelFacade mf;
        private IGameView gameView;
        private AutoResetEvent difficultyEvent;
        private AutoResetEvent guiEvent;
        private AutoResetEvent modelEvent;
        private Random rnd;

        public MainController()
        {
            mf = new ModelFacade();
            dc = new DifficultyController();
            //gameView = new GameView();
        }

        public MainController(IGameView gameView)
        {
            mf = new ModelFacade();
            dc = new DifficultyController();
            this.gameView = gameView;
        }

        /// <summary>
        /// Initialises the game state of a new game
        /// </summary>
        /// <returns>If the game state was sucessfully initialised</returns>
        public bool InitialiseNewGame()
        {
            FileReadWriter frw = new FileReadWriter();
            String itemCatalogue = frw.ReadCatalogueFile(FileReadWriter.ITEM_CATALOGUE);
            String eventCatalogue = frw.ReadCatalogueFile(FileReadWriter.EVENT_CATALOGUE);
            String discoveryCatalogue = frw.ReadCatalogueFile(FileReadWriter.DISCOVERY_CATALOGUE);

            if (GameState.AreValidCatalogues(itemCatalogue, eventCatalogue, discoveryCatalogue))
            {
                gs = new GameState(itemCatalogue, eventCatalogue, discoveryCatalogue);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Initialises the game state from a save
        /// </summary>
        /// <returns>If the game state was sucessfully initialised</returns>
        public bool InitialiseGameFromSave()
        {
            FileReadWriter frw = new FileReadWriter();
            String itemCatalogue = frw.ReadCatalogueFile(FileReadWriter.ITEM_CATALOGUE);
            String eventCatalogue = frw.ReadCatalogueFile(FileReadWriter.EVENT_CATALOGUE);
            String discoveryCatalogue = frw.ReadCatalogueFile(FileReadWriter.DISCOVERY_CATALOGUE);
            String pc = frw.ReadSaveDataFile(FileReadWriter.PLAYER_CHARACTER);
            String inventory = frw.ReadSaveDataFile(FileReadWriter.INVENTORY);
            String usedEvents = frw.ReadSaveDataFile(FileReadWriter.USED_EVENTS);
            String currentEvent = frw.ReadSaveDataFile(FileReadWriter.CURRENT_EVENT);
            String discovered = frw.ReadSaveDataFile(FileReadWriter.DISCOVERED);
            String visitedLocs = frw.ReadSaveDataFile(FileReadWriter.VISITED);
            String unvisitedLocs = frw.ReadSaveDataFile(FileReadWriter.UNVISISTED);
            String currLoc = frw.ReadSaveDataFile(FileReadWriter.CURRENT_LOCATION);
            String currSLoc = frw.ReadSaveDataFile(FileReadWriter.CURRENT_SUBLOCATION);

            if  (GameState.IsValidGameState(pc, inventory, itemCatalogue, usedEvents, currentEvent, eventCatalogue,discovered, 
                discoveryCatalogue,visitedLocs, unvisitedLocs, currLoc, currSLoc))
            {
                gs = new GameState(pc, inventory, itemCatalogue,
                usedEvents, currentEvent, eventCatalogue,
                discovered, discoveryCatalogue,
                visitedLocs, unvisitedLocs, currLoc, currSLoc);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Writes Save Data to files
        /// </summary>
        /// <returns>If the save data was written sucessfully</returns>
        public bool WriteSaveData()
        {
            bool saveSucessful = true;
            FileReadWriter frw = new FileReadWriter();
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.PLAYER_CHARACTER, gs.ParsePCToString());
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.INVENTORY, gs.ParseInventoryToString());
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.USED_EVENTS, gs.ParseUsedEventsToString());
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.CURRENT_EVENT, gs.ParseCurrentEventToString());
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.DISCOVERED, gs.ParseDiscoveredToString());
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.VISITED, gs.ParseVisitedToString());
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.UNVISISTED, gs.ParseUnvisitedToString());
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.CURRENT_LOCATION, gs.ParseCurrLocationToString());
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.CURRENT_SUBLOCATION, gs.ParseCurrSublocationToString());

            return saveSucessful;
        }

        /// <summary>
        /// Change location to location provided
        /// </summary>
        /// <param name="locationID">Id of the location to change to</param>
        /// <returns>If the move was sucessful</returns>
        public bool ChangeLocation(int locationID)
        {
            if (!mf.CanAffordMove(gs, ModelFacade.LOCATION_MOVE_COST) && gameView.DrawYesNoOption("You do not have sufficient resources. Do you wish to risk it all?"))
            {
                int risk = rnd.Next(1, 101);
                if(risk <= 25)
                {
                    PerformMove(locationID);
                }
                else
                {
                    mf.ReduceResourcesByMoveCost(gs, ModelFacade.LOCATION_MOVE_COST);
                    return false;
                }
            }
            else if (mf.CanAffordMove(gs, ModelFacade.LOCATION_MOVE_COST))
            {
                mf.ReduceResourcesByMoveCost(gs, ModelFacade.LOCATION_MOVE_COST);
                PerformMove(locationID);
            }
            else
            {
                return false;
            }

            //Check if event happens
            if (CheckIfEventTriggered())
            {
                TriggerEvent();
                // Check if game over
                if (mf.IsGameOver(gs))
                {
                    // Display the Game over screen
                    // Clean Up save files etc
                    return false;
                }
                // Check if discovery is made
                if (CheckIfDiscoveryTriggered())
                {
                    // Thread 1
                    // Update discovery
                    // Save Game
                    // Thread 2
                    // Display discovery
                }
            }
            return true;
        }

        /// <summary>
        /// Changes the current location dependant on if the new location has been visisted previously or not
        /// </summary>
        /// <param name="locationID">The location to move to</param>
        private void PerformMove(int locationID)
        {
            if (mf.LocationVisited(gs, locationID))
            {
                // Thread 1
                // Recalculate Difficulty
                // Thread 2
                mf.ChangeLocation(gs, locationID);
                // Thread 3
                //gameView.Animate();
            }
            else
            {
                // Thread 1
                mf.ChangeLocation(gs, locationID);
                // Thread 2
                //gameView.Animate();
            }
        }

        /// <summary>
        /// Change sublocation to sublocation provided
        /// </summary>
        /// <param name="sublocationID">ID of the sublocation to move to</param>
        /// <returns>If the move was sucessful</returns>
        public bool ChangeSubLocation(int sublocationID)
        {
            if(mf.CanAffordMove(gs, ModelFacade.SUBLOCATION_MOVE_COST))
            {
                mf.ReduceResourcesByMoveCost(gs, ModelFacade.SUBLOCATION_MOVE_COST);

                // Thread 1
                mf.ChangeSubLocation(gs, sublocationID);
                // Recalculate Difficulty Values
                // Thread 2
                // gameView.Animate();
                return true;
            }
            else
            {
                if (gameView.DrawYesNoOption("You do not have sufficient resources. Do you wish to risk it all?"))
                {
                    int risk = rnd.Next(1, 101);
                    if (risk <= 25)
                    {
                        // Thread 1
                        mf.ChangeSubLocation(gs, sublocationID);
                        // Recalculate Difficulty Values
                        // Thread 2
                        // gameView.Animate();
                        return true;
                    }
                    mf.ReduceResourcesByMoveCost(gs, ModelFacade.SUBLOCATION_MOVE_COST);
                    return false;
                }
                return false;
            }
        }

        /// <summary>
        /// Checks if an event is triggered
        /// </summary>
        /// <returns>If the event is triggered</returns>
        private bool CheckIfEventTriggered()
        {
            float chance = 1.0f;
            //chance = dc.GetEventChance();
            int triggerpoint = Convert.ToInt32(chance * 100);
            int eventTrigger = rnd.Next(1, 101);
            return eventTrigger >= triggerpoint;
        }

        /// <summary>
        /// Checks if a Discovery is triggered
        /// </summary>
        /// <returns>If the discovery is triggered</returns>
        private bool CheckIfDiscoveryTriggered()
        {
            return false;
        }

        /// <summary>
        /// Triggers a new event
        /// </summary>
        private void TriggerEvent()
        {
            mf.GetNewRandomEvent(gs);
            int selected = gameView.DrawEvent(mf.GetCurrentEventText(gs), mf.GetCurrentEventOptionText(gs));
            ResolveEvent(selected);
        }

        /// <summary>
        /// Resolves the event based on the option selected
        /// </summary>
        /// <param name="optionSelected">The selected option</param>
        public void ResolveEvent(int optionSelected)
        {
            float eventMod = 1.0f;
            //eventMod = dc.GetEventModifier();
            //Thread 1
            mf.ResolveEvent(gs, optionSelected, eventMod);
            //Save Game
            //WriteSaveData();

            //Thread 2
            DisplayEventResults(optionSelected);
        }

        /// <summary>
        /// Displays the results of the event based on the option selected
        /// </summary>
        /// <param name="optionSelected">The selected option</param>
        public void DisplayEventResults(int optionSelected)
        {
            String optionResult = mf.GetOptionResult(gs, optionSelected);
            List<String> effectResults = mf.GetOptionEffectResults(gs, optionSelected);
            gameView.DrawEventResult(optionResult, effectResults);
        }

        public bool ScavangeLocation()
        {
            if(CheckIfEventTriggered())
            {
                TriggerEvent();
                if (mf.IsGameOver(gs))
                {
                    // Display the Game over screen
                    // Clean Up save files etc
                    return false;
                }
            }

            List<Item> scavenged = mf.ScavangeSubLocation(gs);
            // Thread 1
            // Save Game
            // Thread 2
            gameView.DrawScavengeResults(scavenged);
            return true;
        }

        /// <summary>
        /// Opens and draws the inventory
        /// </summary>
        public void OpenInventory()
        {
            ArrayList inv = mf.GetInventory(gs);
            gameView.DrawInventory(inv);
        }

        /// <summary>
        /// Attempts to use the item in the inventory slot 
        /// </summary>
        /// <param name="inventorySlot">The inventory slot number</param>
        /// <returns>If the item was used sucessfully</returns>
        public bool UseItem(int inventorySlot)
        {
            if (mf.ItemUsable(gs, inventorySlot))
            {
                gameView.DrawDialogueBox("Item is not usable");
                return false;
            }
            else
            {
                return mf.UseItem(gs, inventorySlot);
            }
        }

        /// <summary>
        /// Attempts to discard the item
        /// </summary>
        /// <param name="inventorySlot">The inventory slot to discard items from</param>
        /// <returns>If it was sucessfully discarded</returns>
        public bool DiscardItem(int inventorySlot)
        {
            return mf.DiscardItem(gs, inventorySlot);
        }

        public void DisplayDiscoveries()
        {
            throw new System.Exception("Not implemented");
        }

        /// <summary>
        /// Draws the sublocations map
        /// </summary>
        public void DisplaySubLocationsMap()
        {
            List<Sublocation> sublocations = mf.GetCurrentSublocations(gs);
            gameView.DrawSublocationMap(sublocations);
        }

        /// <summary>
        /// Draws the world map
        /// </summary>
        public void DisplayLocations()
        {
            List<Location> visited = mf.GetVisitedLocations(gs);
            List<DummyLocation> unvisited = mf.GetUnvisitedLocations(gs);

            gameView.DrawWorldMap(visited, unvisited);
        }

        /// <summary>
        /// Draws the end of game screen
        /// </summary>
        public void DisplayEndGameScreen()
        {
            if (mf.IsGameOver(gs))
            {
                gameView.DrawGameOver();
            }
            else
            {
                gameView.DrawVictory();
            }
        }

        public void ExitGame()
        {
            throw new System.Exception("Not implemented");
        }

        public GameState GetGameState()
        {
            return gs;
        }

        public SortedList<string,int> GetPlayerResources()
        {
            return mf.GetPlayerCharacterResources(gs);
        }

    }

}
