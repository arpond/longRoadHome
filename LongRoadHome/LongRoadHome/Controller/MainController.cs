using System;
using System.Collections;
using System.Threading;
using uk.ac.dundee.arpond.longRoadHome.Model;
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

        public MainController()
        {
            gameView = new GameView();
        }

        public MainController(int mode)
        {
            switch (mode)
            {
                case 0:
                    gameView = new DebugView();
                    break;
                case 1:
                    gameView = new GameView();
                    break;
            }
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
        public void ChangeLocation(int locationID)
        {
            throw new System.Exception("Not implemented");
        }
        public void ChangeSubLocation(int sublocationID)
        {
            throw new System.Exception("Not implemented");
        }
        private void TriggerEvent()
        {
            throw new System.Exception("Not implemented");
        }
        public void ResolveEvent(ref object int_optionSelected)
        {
            throw new System.Exception("Not implemented");
        }
        public void ScavangeLocation()
        {
            throw new System.Exception("Not implemented");
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
        public void DisplaySubLocations()
        {
            throw new System.Exception("Not implemented");
        }
        public void DisplayLocations()
        {
            throw new System.Exception("Not implemented");
        }
        public void DisplayEndGameScreen()
        {
            throw new System.Exception("Not implemented");
        }
        public void ExitGame()
        {
            throw new System.Exception("Not implemented");
        }

        public GameState GetGameState()
        {
            return gs;
        }

    }

}
