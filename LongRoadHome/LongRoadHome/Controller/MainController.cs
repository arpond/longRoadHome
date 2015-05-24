using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using uk.ac.dundee.arpond.longRoadHome.Model;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.View;
namespace uk.ac.dundee.arpond.longRoadHome.Controller
{
    public class MainController
    {
        #region Public Constant Fields
        public const String NEW_GAME = "New Game", CONTINUE = "Continue", VIEW_LOC_MAP = "View Location Map", VIEW_SUB_MAP = "View Sublocation Map",
            VIEW_INVENTORY = "View Inventory", CHANGE_LOC = "Change Location", CHANGE_SUB = "Change Sublocation", USE_ITEM = "Use Item", 
            DISCARD_ITEM = "Discard Item", SCAVENGE = "Scavenge", EVENT="Event", GAME_OVER = "Game Over", QUIT = "Quit";
        #endregion

        #region Private Fields
        private DifficultyController dc;
        private GameState gs;
        private ModelFacade mf;
        private IGameView gameView;
        private Random rnd = new Random();
        private Dictionary<string,int> commandMap = new Dictionary<string, int>();
        #endregion

        #region Constructors
        public MainController()
        {
            mf = new ModelFacade();
            dc = new DifficultyController();
            //gameView = new GameView();
            IntializeCommandMap();
            
        }

        public MainController(IGameView gameView)
        {
            mf = new ModelFacade();
            dc = new DifficultyController();
            this.gameView = gameView;
            IntializeCommandMap();
        }
        #endregion

        private void IntializeCommandMap()
        {
            commandMap.Add(NEW_GAME, 0);
            commandMap.Add(CONTINUE, 1);
            commandMap.Add(VIEW_LOC_MAP, 2);
            commandMap.Add(VIEW_SUB_MAP, 3);
            commandMap.Add(VIEW_INVENTORY, 4);
            commandMap.Add(CHANGE_LOC, 5);
            commandMap.Add(CHANGE_SUB, 6);
            commandMap.Add(USE_ITEM, 7);
            commandMap.Add(DISCARD_ITEM, 8);
            commandMap.Add(SCAVENGE, 9);
            commandMap.Add(EVENT, 10);
            commandMap.Add(GAME_OVER, 11);
            commandMap.Add(QUIT, 12);
        }

        
        /// <summary>
        /// Initialises the game state of a new game
        /// </summary>
        /// <returns>If the game state was sucessfully initialised</returns>
        public void InitialiseNewGame()
        {
            FileReadWriter frw = new FileReadWriter();
            String itemCatalogue = frw.ReadCatalogueFile(FileReadWriter.ITEM_CATALOGUE);
            String eventCatalogue = frw.ReadCatalogueFile(FileReadWriter.EVENT_CATALOGUE);
            String discoveryCatalogue = frw.ReadCatalogueFile(FileReadWriter.DISCOVERY_CATALOGUE);

            if (GameState.AreValidCatalogues(itemCatalogue, eventCatalogue, discoveryCatalogue))
            {
                gs = new GameState(itemCatalogue, eventCatalogue, discoveryCatalogue);
                dc = new DifficultyController();
                var worldMap = mf.GetWorldMap(gs);
                var buttonAreas = mf.GetButtonAreas(gs);

                //Dispatcher.Invoke(new Action(() => createTransparentButtons(buttonAreas, unvisitedBMI, visitedBMI)));

                //Dispatcher dispatcher = gameView.GetDispatcher();
                //dispatcher.Invoke(new Action(() => gameView.InitialiseWorldMap(worldMap, buttonAreas)));
                gameView.InitialiseWorldMap(worldMap, buttonAreas);
                gameView.InitialiseSublocationMap(mf.GetCurrentSublocations(gs), mf.GetCurrentSublocation(gs));
                gameView.InitialiseInventory(mf.GetInventory(gs));
            }
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
            String buttonAreas = frw.ReadSaveDataFile(FileReadWriter.BUTTONS_AREA);
            String difficultyController = frw.ReadSaveDataFile(FileReadWriter.DIFFICULTY_CONTROLLER);

            System.Drawing.Bitmap worldMap = frw.ReadBitmap(FileReadWriter.WORLD_MAP);

            if  (GameState.IsValidGameState(pc, inventory, itemCatalogue, usedEvents, currentEvent, eventCatalogue,discovered,
                discoveryCatalogue, visitedLocs, unvisitedLocs, currLoc, currSLoc) && DifficultyController.IsValidDifficultyController(difficultyController))
            {
                gs = new GameState(pc, inventory, itemCatalogue,
                usedEvents, currentEvent, eventCatalogue,
                discovered, discoveryCatalogue,
                visitedLocs, unvisitedLocs, currLoc, currSLoc, buttonAreas, worldMap);
                dc = new DifficultyController(difficultyController);

                gameView.InitialiseWorldMap(mf.GetWorldMap(gs), mf.GetButtonAreas(gs));
                gameView.InitialiseSublocationMap(mf.GetCurrentSublocations(gs), mf.GetCurrentSublocation(gs));
                gameView.InitialiseInventory(mf.GetInventory(gs));
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
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.DIFFICULTY_CONTROLLER, dc.ParseToString());
            saveSucessful &= frw.WriteSaveDataFile(FileReadWriter.BUTTONS_AREA, gs.ParseButtonAreasToString());
            saveSucessful &= frw.WriteBitmap(FileReadWriter.WORLD_MAP, gs.GetLM().GetWorldMap());

            return saveSucessful;
        }

        public void handleIdepotentAction(string command)
        {
            int action;
            if (commandMap.TryGetValue(command, out action))
            {
                switch (action)
                {
                    // View location Map
                    case 2:
                        DisplayLocations();
                        break;
                    // View sublocation Map
                    case 3:
                        DisplaySubLocationsMap();
                        break;
                    // View Inventory
                    case 4:
                        DisplayInventory();
                        break;
                        // Game Over
                    case 11:
                        DisplayEndGameScreen();
                        break;
                    // Quit
                    case 12:
                        break;
                }
            }
        }

        public void handlePotentAction(string command, int variable)
        {
            int action;
            if (commandMap.TryGetValue(command, out action))
            {
                switch (action)
                {
                    // New Game
                    case 0:
                        // Main Thread - Start new game
                        InitialiseNewGame();
                        handleIdepotentAction(VIEW_LOC_MAP);

                        // Thread 1 - Display Instructions
                        gameView.StartNewGame();                      
                        break;
                    // Continue
                    case 1:
                        // Load game from save
                        if (!InitialiseGameFromSave())
                        {
                            InitialiseNewGame();
                        }
                        else
                        {
                            handleIdepotentAction(VIEW_LOC_MAP);
                        }
                        break;
                    // Change location
                    case 5:
                        if (ChangeLocation(variable))
                        {
                            gameView.UpdateWorldMap(variable);
                            gameView.InitialiseSublocationMap(mf.GetCurrentSublocations(gs), mf.GetCurrentSublocation(gs));
                        }
                        break;
                    // Change Sublocation
                    case 6:
                        if (ChangeSubLocation(variable))
                            gameView.UpdateSublocationMap(variable, 0);
                        break;
                    // Use Item
                    case 7:
                        if (UseItem(variable))
                            gameView.InitialiseInventory(mf.GetInventory(gs));
                        break;
                    // Discard Item
                    case 8:
                        if (DiscardItem(variable))
                            gameView.InitialiseInventory(mf.GetInventory(gs));
                        break;
                    // Scavenge
                    case 9:
                        if (ScavangeSublocation())
                            gameView.InitialiseInventory(mf.GetInventory(gs));
                        break;
                    case 10:
                        TriggerEvent();
                        break;
                }
                PostAction();
            }
        }

        private void PostAction()
        {
            //Update Player
            gameView.UpdatePlayer();
            //WriteSaveData
            //Check if Game Over
            if (mf.IsGameOver(gs) || IsEndLocation())
            {
                handleIdepotentAction(GAME_OVER);
            }
            
        }

        /// <summary>
        /// Change location to location provided
        /// </summary>
        /// <param name="locationID">Id of the location to change to</param>
        /// <returns>If the move was sucessful</returns>
        public bool ChangeLocation(int locationID)
        {
            bool visited = true;

            var cost = Convert.ToInt32(mf.CalculateMoveCost(gs, locationID));
            if (!gameView.DrawYesNoOption("Are you sure you wish to move to this location? It will cost you " + cost + " hunger and thirst"))
            {
                return false;
            }
            else if (!mf.CanAffordMove(gs, cost) && gameView.DrawYesNoOption("You do not have sufficient resources. Do you wish to risk it all?"))
            {
                int risk = rnd.Next(1, 101);
                if(risk <= 25)
                {
                    visited = PerformMove(locationID);
                }
                else
                {
                    mf.ReduceResourcesByMoveCost(gs, cost);
                    return false;
                }
            }
            else if (mf.CanAffordMove(gs, cost))
            {
                mf.ReduceResourcesByMoveCost(gs, cost);
                visited = PerformMove(locationID);
            }
            else
            {
                return false;
            }

            //Check if event happens
            if (!visited && CheckIfEventTriggered())
            {
                handlePotentAction(EVENT, 0);
                // Check if discovery is made
                if (CheckIfDiscoveryTriggered())
                {
                    
                    String discoveryText = mf.TryToMakeNewDiscovery(gs);
                    if (discoveryText != "")
                    {
                        // Thread 1 - Save Game
                        // WriteSaveData();
                        // Main Thread - Display Discovery
                        gameView.DrawDiscovery(discoveryText);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Changes the current location dependant on if the new location has been visisted previously or not
        /// </summary>
        /// <param name="locationID">The location to move to</param>
        /// <returns>Bool representing if the location was a visited one or not</returns>
        private bool PerformMove(int locationID)
        {
            if (mf.LocationVisited(gs, locationID))
            {
                // Thread 1 - Change location
                var task1 = Task.Factory.StartNew(() =>
                {
                    mf.ChangeLocation(gs, locationID);
                });
                // Main Thread - Animate movement
                //gameView.Animate();
                Task.WaitAll(task1);
                return true;
            }
            else
            {
                // Thread 1 - Recalculate Difficulty
                var task1 = Task.Factory.StartNew(() =>
                {
                    dc.UpdatePlayerStatus(gs.Clone() as GameState);
                    dc.UpdateStatusTracker();
                });

                // Thread 2 - Change location
                var task2 = Task.Factory.StartNew(() =>
                {
                    mf.ChangeLocation(gs, locationID);
                });

                // Main Thread - Animate movement
                //gameView.Animate();
                Task.WaitAll(task1, task2);
                return false;
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

                // Thread 1 - Change sublocation
                var task1 = Task.Factory.StartNew(() =>
                {
                    mf.ChangeSubLocation(gs, sublocationID);
                    dc.UpdatePlayerStatus(gs.Clone() as GameState);
                });
                // Main Thread - Animate movement
                // gameView.Animate();
                Task.WaitAll(task1);
                return true;
            }
            else
            {
                if (gameView.DrawYesNoOption("You do not have sufficient resources. Do you wish to risk it all?"))
                {
                    int risk = rnd.Next(1, 101);
                    if (risk <= 25)
                    {
                        // Thread 1 - Change sublocation
                        var task1 = Task.Factory.StartNew(() =>
                        {
                            mf.ChangeSubLocation(gs, sublocationID);
                            dc.UpdatePlayerStatus(gs.Clone() as GameState);
                        });
                        // Main Thread  - Animate movement
                        // gameView.Animate();
                        Task.WaitAll(task1);
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
            dc.CalcEventChance();
            double chance = dc.GetEventChance();
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
            double eventMod = dc.GetEventModifier();
            //Thread 1 - Resolve Event
            var task1 = Task.Factory.StartNew(() =>
            {
                mf.ResolveEvent(gs, optionSelected, eventMod);
                //Save Game
                //WriteSaveData();
            });
            //MainThread - Display Results
            DisplayEventResults(optionSelected);
            Task.WaitAll(task1);
        }

        /// <summary>
        /// Displays the results of the event based on the option selected
        /// </summary>
        /// <param name="optionSelected">The selected option</param>
        public void DisplayEventResults(int optionSelected)
        {
            String optionResult = mf.GetOptionResult(gs, optionSelected);
            List<String> effectResults = mf.GetOptionEffectResults(gs, optionSelected);

            Dispatcher dispatcher = gameView.GetDispatcher();
            dispatcher.Invoke(new Action(() => gameView.DrawEventResult(optionResult, effectResults)));
        }

        public bool ScavangeSublocation()
        {
            if(CheckIfEventTriggered())
            {
                handlePotentAction(EVENT, 0);
            }

            List<Item> scavenged = mf.ScavangeSubLocation(gs);
            // Thread 1
            // Save Game
            // Main Thread 
            //gameView.DrawSublocationMap(mf.GetCurrentSublocations(gs), mf.GetCurrentSublocation(gs));
            gameView.UpdateSublocationMap(mf.GetCurrentSublocation(gs), 1);
            gameView.DrawScavengeResults(scavenged);
            return true;
        }

        /// <summary>
        /// Opens and draws the inventory
        /// </summary>
        public void DisplayInventory()
        {
            gameView.DisplayInventory();
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
            
            gameView.DisplaySublocationMap();
        }

        /// <summary>
        /// Draws the world map
        /// </summary>
        public void DisplayLocations()
        {
            gameView.DisplayWorldMap();
        }

        /// <summary>
        /// Draws the end of game screen
        /// </summary>
        public void DisplayEndGameScreen()
        {
            if (mf.IsGameOver(gs))
            {    
                // Thread 1 - Save Game
                // Save Game
                // Main Thread
                gameView.DrawGameOver();
                gameView.ReturnToMainMenu();
            }
            else
            {
                // Thread 1 - Save Game
                // Save Game
                // Main Thread
                gameView.DrawVictory();
                gameView.ReturnToMainMenu();
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

        public DifficultyController GetDifficultyController()
        {
            return dc;
        }

        public SortedList<string,int> GetPlayerResources()
        {
            return mf.GetPlayerCharacterResources(gs);
        }

        private bool IsEndLocation()
        {
            return dc.IsEndLocation();
        }
    }

}
