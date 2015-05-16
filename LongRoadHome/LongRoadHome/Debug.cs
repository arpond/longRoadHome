using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.Model;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;

namespace uk.ac.dundee.arpond.longRoadHome.View
{
    public partial class Debug : Form, IGameView
    {
        private MainController mc;
        //private int currentDisplay;

        public Debug()
        {
            InitializeComponent();
            mc = new MainController(this);
        }

        private void startNewGameBtn_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void loadGameBtn_Click(object sender, EventArgs e)
        {
            LoadGame();
        }

        /// <summary>
        /// Change location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeLocationBtn_Click(object sender, EventArgs e)
        {
            int newLocationID = 0;
            int.TryParse(locationTextBox.Text, out newLocationID);
            mc.handleAction(MainController.CHANGE_LOC, newLocationID);
            
            DrawLocations();
            DrawCharacterResources();
            DrawDifficultyController();
        }

        /// <summary>
        /// Change sublocation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeSubBtn_Click(object sender, EventArgs e)
        {
            int newSubLocID = 0;
            int.TryParse(sublocTextBox.Text, out newSubLocID);
            mc.handleAction(MainController.CHANGE_SUB, newSubLocID);

            DrawLocations();
            DrawCharacterResources();
            DrawDifficultyController();
        }

        /// <summary>
        /// Display current event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawEventBtn_Click(object sender, EventArgs e)
        {
            int result = DrawEvent(mc.GetGameState().GetEM().GetCurrentEventText(), 
                mc.GetGameState().GetEM().GetCurrentEventOptionsText());
            mc.DisplayEventResults(result);
            DrawDifficultyController();
        }

        /// <summary>
        /// Starts a new game
        /// </summary>
        public void StartNewGame()
        {
            mc.handleAction(MainController.NEW_GAME);
            DrawCharacterResources();
            mc.handleAction(MainController.VIEW_INVENTORY);
            DrawItemCatalogue();
            DrawEvents();
            DrawEventCatalogue();
            DrawLocations();
            DrawDifficultyController();
        }

        /// <summary>
        /// Loads the saved game
        /// </summary>
        public void LoadGame()
        {
            mc.handleAction(MainController.CONTINUE);
            DrawCharacterResources();
            mc.handleAction(MainController.VIEW_INVENTORY);
            DrawItemCatalogue();
            DrawEvents();
            DrawEventCatalogue();
            DrawLocations();
            DrawDifficultyController();
        }

        /// <summary>
        /// Updates the character resources
        /// </summary>
        private void DrawCharacterResources()
        {
            SortedList<string, int> temp = mc.GetPlayerResources();
            int health, hunger, thirst, sanity;

            temp.TryGetValue(PlayerCharacter.HEALTH, out health);
            temp.TryGetValue(PlayerCharacter.HUNGER, out hunger);
            temp.TryGetValue(PlayerCharacter.THIRST, out thirst);
            temp.TryGetValue(PlayerCharacter.SANITY, out sanity);

            healthLabel.Text = "Health: " + health + "/100";
            hungerLabel.Text = "Hunger: " + hunger + "/100";
            thirstLabel.Text = "Thirst: " + thirst + "/100";
            sanityLabel.Text = "Sanity: " + sanity + "/100";
        }

        /// <summary>
        /// Updates the inventory
        /// </summary>
        /// <param name="inventory">The inventory</param>
        public void DrawInventory(ArrayList inventory)
        {
            List<String> temp =  new List<string>();
            foreach(Item item in inventory)
            {
                temp.Add(item.ToPrettyString());
            }
            inventoryListBox.DataSource = temp;
        }

        /// <summary>
        /// Updates the item catalogue
        /// </summary>
        public void DrawItemCatalogue()
        {
            GameState gs = mc.GetGameState();
            PCModel pcm = gs.GetPCM();
            List<Item> items = pcm.GetItemCatalogue().GetItems();
            List<string> temp = new List<string>();
            foreach(Item item in items)
            {
                temp.Add(item.ToPrettyString());
            }
            itemCatalogue.DataSource = temp;
        }

        /// <summary>
        /// Updates the events section
        /// </summary>
        public void DrawEvents()
        {
            GameState gs = mc.GetGameState();
            EventModel em = gs.GetEM();

            String curr = em.ParseCurrentEventToString();
            String used = em.ParseUsedEventsToString();

            currentEventLabel.Text = "Current Event " + curr;
            usedEventsLabel.Text = "Used Events " + used;
        }

        /// <summary>
        /// Updates the event catalogue
        /// </summary>
        public void DrawEventCatalogue()
        {
            GameState gs = mc.GetGameState();
            EventModel em = gs.GetEM();
            IList<Event> events = em.GetEventCatalogue().GetEvents();
            List<String> temp = new List<string>();

            foreach(Event evt in events)
            {
                temp.Add(evt.ParseToString());
            }
            eventCatalogue.DataSource = temp;
        }

        /// <summary>
        /// Updates the dc
        /// </summary>
        public void DrawDifficultyController()
        {
            DifficultyController dc = mc.GetDifficultyController();
            playerStatusLabel.Text = "Player Status " + dc.GetPlayerStatus();
            eventModifierLabel.Text = "Event Modifier " + dc.GetEventModifier();
            endLocLabel.Text = "End Location Chance " + dc.GetEndLocationChance();
            eventChanceLabel.Text = "Event Chance " + dc.GetEventChance();

            List<double> tracker = dc.GetPlayerStatusTracker();

            trackerListBox.DataSource = tracker.ToArray();

            if (tracker.Count > 0)
            {
                var bestFit = dc.GenerateBestFitLine();
                List<double> yValues = new List<double>();
                foreach (var fit in bestFit)
                {
                    yValues.Add(fit.Item2);
                }
                bestFitLine.DataSource = yValues;
            }
        }

        /// <summary>
        /// Draw location details
        /// </summary>
        public void DrawLocations()
        {
            GameState gs = mc.GetGameState();
            LocationModel lm = gs.GetLM();

            String curr = lm.ParseCurrLocationToString();
            String currS = lm.ParseCurrSubLocToString();
            String connections = "Current Connections: ";
            String subloc = "Sublocations Available: ";

            HashSet<int> cons = lm.GetCurentLocation().GetConnections();

            foreach(int con in cons)
            {
                connections += con + ", ";
            }

            var subs = lm.GetCurentLocation().GetSublocations();

            foreach (Sublocation sub in subs.Values)
            {
                subloc += sub.GetSublocationID() + ", ";
            }

            IList<Location> visited = lm.GetVisited().Values;
            IList<DummyLocation> unvisited = lm.GetUnvisited().Values;

            List<String> vis = new List<string>();
            List<String> unvis = new List<string>();

            foreach(Location lc in visited)
            {
                vis.Add(lc.ParseToString());
            }

            foreach(DummyLocation dl in unvisited)
            {
                unvis.Add(dl.ParseToString());
            }

            currentLocationLabel.Text = "Current Location " + curr;
            currentSublocationLabel.Text = "Current Sublocation " + currS;
            currentConnectionsLabel.Text = connections;
            sublocationsAvailLabel.Text = subloc;

            visitedListBox.DataSource = vis;
            unvisitedListBox.DataSource = unvis;
        }



        private void ChangeLocationAnimation()
        {
            throw new System.Exception("Not implemented");
        }
        private void GuiReady()
        {
            throw new System.Exception("Not implemented");
        }
        public void Animate(List<String> imageFileNames)
        {
            throw new System.Exception("Not implemented");
        }
        public void PlayAudio(String audioFile)
        {
            throw new System.Exception("Not implemented");
        }

        /// <summary>
        /// Draws an event
        /// </summary>
        /// <param name="eventText">The main event text</param>
        /// <param name="options">The options available for the event</param>
        /// <returns>The selected option by the used</returns>
        public int DrawEvent(String eventText, List<String> options)
        {
            EventDialog ed = new EventDialog(eventText, options, false);
            ed.ShowDialog();
            int selected = ed.GetSelected();
            return selected;
        }

        /// <summary>
        /// Draws the results of the event
        /// </summary>
        /// <param name="optionResult">The main option result text</param>
        /// <param name="results">The results of the selected option</param>
        public void DrawEventResult(String optionResult, List<String> results)
        {
            EventDialog ed = new EventDialog(optionResult, results, true);
            ed.ShowDialog();
        }

        public void DrawVictory()
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawGameOver()
        {
            throw new System.Exception("Not implemented");
        }
        
        /// <summary>
        /// Draw Dialogue box with Yes No option and text supplied
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <returns>If yes was pressed</returns>
        public bool DrawYesNoOption(String text)
        {
            DialogResult result = MessageBox.Show(text, "", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Draw dialogue box with text supplied
        /// </summary>
        /// <param name="text">The text to be displayed</param>
        public void DrawDialogueBox(String text)
        {
            MessageBox.Show(text);
        }

        public void DrawSublocationMap(List<Sublocation> subloc)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawWorldMap(List<Location> visited, List<DummyLocation> unvisited)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawDiscoveries(List<Discovery> discs)
        {
            throw new System.Exception("Not implemented");
        }
        public void DrawMainMenu()
        {
            throw new System.Exception("Not implemented");
        }

        public void DrawScavengeResults(List<Item> scavanged)
        {
            
        }

        public void DrawDiscovery(String discovery)
        {
            DrawDialogueBox(discovery);
        }
       
    }
}
