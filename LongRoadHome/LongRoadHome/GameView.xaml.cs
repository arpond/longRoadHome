using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.View;

namespace LongRoadHome
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Page, IGameView
    {
        private static Action EmptyDelegate = delegate() { };

        MainController mc;


        public GameView()
        {
            InitializeComponent();
            mc = new MainController(this);
        }

        /// <summary>
        /// Starts a new game
        /// </summary>
        public void StartNewGame()
        {
            mc.handleAction(MainController.NEW_GAME);
        }

        public void UpdatePlayer()
        {
            SortedList<string, int> temp = mc.GetPlayerResources();
            int health, hunger, thirst, sanity;

            temp.TryGetValue(PlayerCharacter.HEALTH, out health);
            temp.TryGetValue(PlayerCharacter.HUNGER, out hunger);
            temp.TryGetValue(PlayerCharacter.THIRST, out thirst);
            temp.TryGetValue(PlayerCharacter.SANITY, out sanity);

            baseUI.Health = health + "";
            baseUI.Hunger = hunger + "";
            baseUI.Thirst = thirst + "";
            baseUI.Sanity = sanity + "";
        }

        public void UpdateSublocation()
        {

        }

        public void UpdateInventory()
        {

        }

        /// <summary>
        /// Loads the saved game
        /// </summary>
        public void LoadGame()
        {
            mc.handleAction(MainController.CONTINUE);
        }

        public void DrawMainMenu()
        {
            throw new NotImplementedException();
        }

        public void DrawDiscoveries(List<Discovery> discs)
        {
            throw new NotImplementedException();
        }
        public void DrawWorldMap(List<Location> visited, List<DummyLocation> unvisited)
        {
            throw new NotImplementedException();
        }
        public void DrawSublocationMap(List<Sublocation> subloc)
        {
            
        }
        public void DrawDialogueBox(String text)
        {
            throw new NotImplementedException();
        }
        public bool DrawYesNoOption(String text)
        {
            throw new NotImplementedException();
        }
        public void DrawInventory(ArrayList inventory)
        {
            throw new NotImplementedException();
        }
        public void DrawGameOver()
        {
            throw new NotImplementedException();
        }
        public void DrawVictory()
        {
            throw new NotImplementedException();
        }
        public int DrawEvent(String eventText, List<String> options)
        {
            throw new NotImplementedException();
        }
        public void DrawEventResult(String optionResult, List<String> results)
        {
            throw new NotImplementedException();
        }
        public void PlayAudio(String audioFile)
        {
            throw new NotImplementedException();
        }
        public void Animate(List<String> imageFileNames)
        {
            throw new NotImplementedException();
        }
        public void DrawScavengeResults(List<Item> scavenged)
        {
            throw new NotImplementedException();
        }
        public void DrawDiscovery(string discovery)
        {
            throw new NotImplementedException();
        }
    }
}
