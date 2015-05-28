using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
namespace uk.ac.dundee.arpond.longRoadHome.View
{
    public interface IGameView
    {
        #region General UI Functions
        void StartNewGame();
        /// <summary>
        /// Returns to the main menu
        /// </summary>
        void ReturnToMainMenu();
        void UpdatePlayer();
        Dispatcher GetDispatcher();
        #endregion

        #region Dialogue Boxes
        /// <summary>
        /// Draws the game over dialogue
        /// </summary>
        void DrawGameOver();
        /// <summary>
        /// Draws the victory dialogue
        /// </summary>
        void DrawVictory();
        /// <summary>
        /// Draws a simple dialogue box with the text supplied
        /// </summary>
        /// <param name="text">The text to display</param>
        void DrawDialogueBox(String text);
        /// <summary>
        /// Draws a Yes No dialogue box with the text supplied
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <returns>If yes was selected</returns>
        bool DrawYesNoOption(String text);
        /// <summary>
        /// Creates a dialogue box for the event and displays it
        /// </summary>
        /// <param name="eventText">The main text for the event</param>
        /// <param name="options">The text for each option</param>
        /// <returns>The selected option</returns>
        int DrawEvent(String eventText, List<String> options);
        /// <summary>
        /// Displays the results of the event based on the option chosen
        /// </summary>
        /// <param name="optionResult">The Main option result</param>
        /// <param name="results">The text from the effects of the result</param>
        void DrawEventResult(String optionResult, List<String> results);
        /// <summary>
        /// Displays the results from scavenging
        /// </summary>
        /// <param name="scavenged">The items scavenged</param>
        void DrawScavengeResults(List<Item> scavenged);
        #endregion

        #region Discovery Functions
        void DrawDiscoveries(List<Discovery> discs, int max);
        void DrawDiscovery(string discovery);
        #endregion

        #region Inventory Functions
        /// <summary>
        /// Initialising the inventory with the items in the inventory
        /// </summary>
        /// <param name="inventory">The inventory to et up</param>
        void InitialiseInventory(ArrayList inventory);
        /// <summary>
        /// Draws the inventory passed
        /// </summary>
        void DisplayInventory();
        #endregion

        #region Sublocation Map Functions
        /// <summary>
        /// Initialises the sublocation map
        /// </summary>
        /// <param name="subloc">List of sublocation for this map</param>
        /// <param name="currentSubLocation">The current sublocation which the player is at</param>
        void InitialiseSublocationMap(List<Sublocation> subloc, int currentSubLocation);
        /// <summary>
        /// Draws the sublocation map
        /// </summary>
        void DisplaySublocationMap();
        /// <summary>
        /// Updates the data behind the sublocation map
        /// </summary>
        /// <param name="sublocation">The sublocation to update</param>
        /// <param name="mode">The mode for the update, 
        /// 0 = change current location to sublocation passed
        /// 1 = update sublocation so that it is scavenged</param>
        void UpdateSublocationMap(int sublocation, int mode);
        #endregion

        #region World Map Function
        /// <summary>
        /// Initialise the world map
        /// </summary>
        /// <param name="worldMapBM">Bit representing the background of the map</param>
        /// <param name="buttonAreas">The Points on the map for the location buttons</param>
        void InitialiseWorldMap(System.Drawing.Bitmap worldMapBM, SortedList<int, System.Windows.Point> buttonAreas);
        /// <summary>
        /// Displays the WorldMap
        /// </summary>
        void DisplayWorldMap();
        /// <summary>
        /// Updates the world map
        /// </summary>
        /// <param name="newLocation"></param>
        void UpdateWorldMap(int newLocation);

        void UpdateFromSave(int currentLcoation, List<int> visited);
        #endregion

        #region Animation Functions
        void AnimateFrames(List<String> imageFileNames);
        void EndAnimation();
        #endregion

        #region Audio Functions
        void PlayAudio(String audioFile);
        #endregion

        void ExitGame();
    }

}
