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
using System.Windows.Media.Animation;
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.Model;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;
using uk.ac.dundee.arpond.longRoadHome.Model.Events;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;
using uk.ac.dundee.arpond.longRoadHome.View;
using System.Drawing;
using System.Windows.Threading;
using System.Timers;

namespace LongRoadHome
{


    /// <summary>
    /// Interaction logic for Debug.xaml
    /// </summary>
    public partial class Debug : Page, IGameView
    {
        private static Action EmptyDelegate = delegate() { };

        MainController mc;
        int current = 0;
        int current2 = 0;
        public Debug()
        {
            InitializeComponent();
            this.ShowsNavigationUI = false;
            mc = new MainController(this);
            ImageBrush temp = new ImageBrush(System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                      LongRoadHome.Properties.Resources.parallax_mountain_mountains.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions()));
            //temp.TileMode = TileMode.Tile;
            rectBck.Fill = temp;
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

            healthLabel.Content = health + "/100";
            hungerLabel.Content = hunger + "/100";
            thirstLabel.Content = thirst + "/100";
            sanityLabel.Content = sanity + "/100";
        }

        /// <summary>
        /// Updates the inventory
        /// </summary>
        /// <param name="inventory">The inventory</param>
        public void DrawInventory(ArrayList inventory)
        {
            inventoryLB.ItemsSource = inventory;
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
            foreach (Item item in items)
            {
                temp.Add(item.ToPrettyString());
            }
            itemCatalogueLB.ItemsSource = temp;
            //itemCatalogue.DataSource = temp;
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

            currentEventLabel.Content = curr;
            usedEventsLabel.Content = used;
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

            foreach (Event evt in events)
            {
                temp.Add(evt.ParseToString());
            }
            eventCatalogueLB.ItemsSource = temp;
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
            String connections = "";
            String subloc = "";

            HashSet<int> cons = lm.GetCurentLocation().GetConnections();

            foreach (int con in cons)
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

            foreach (Location lc in visited)
            {
                vis.Add(lc.ParseToString());
            }

            foreach (DummyLocation dl in unvisited)
            {
                unvis.Add(dl.ParseToString());
            }
            currentLocLbl.Content = curr;
            currentSubLbl.Content = currS;
            currentConLbl.Content = connections;
            availSubLbl.Content = subloc;

            visitedLB.ItemsSource = vis;
            unvisitedLB.ItemsSource = unvis;
        }

        /// <summary>
        /// Updates the dc
        /// </summary>
        public void DrawDifficultyController()
        {
            DifficultyController dc = mc.GetDifficultyController();
            playerStatusLabel.Content = dc.GetPlayerStatus();
            eventModifierLabel.Content = dc.GetEventModifier();
            endLocChanceLabel.Content = dc.GetEndLocationChance();
            eventChanceLabel.Content = dc.GetEventChance();

            List<double> tracker = dc.GetPlayerStatusTracker();

            trackerLB.ItemsSource = tracker;

            List<double> yValues = new List<double>();
            if (tracker.Count > 0)
            {
                var bestFit = dc.GenerateBestFitLine();
                
                foreach (var fit in bestFit)
                {
                    yValues.Add(fit.Item2);
                }
            }
            bestFitLineLB.ItemsSource = yValues;
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draw dialogue box with text supplied
        /// </summary>
        /// <param name="text">The text to be displayed</param>
        public void DrawDialogueBox(String text)
        {
            string sMessageBoxText = text;
            string sCaption = "";

            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        /// <summary>
        /// Draw Dialogue box with Yes No option and text supplied
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <returns>If yes was pressed</returns>
        public bool DrawYesNoOption(String text)
        {
            string sMessageBoxText = text;
            string sCaption = "";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
        public void DrawGameOver()
        {
            throw new NotImplementedException();
        }
        public void DrawVictory()
        {
            throw new NotImplementedException();
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
            return ed.GetSelected();
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
            DrawDialogueBox(discovery);
        }

        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void loadGameBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadGame();
        }

        /// <summary>
        /// Change location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeLocBtn_Click(object sender, RoutedEventArgs e)
        {
            int newLocationID = 0;

            int.TryParse(changeLocTB.Text, out newLocationID);
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
        private void changeSubLocBtn_Click(object sender, RoutedEventArgs e)
        {
            int newSubLocID = 0;
            int.TryParse(changeSubLocTB.Text, out newSubLocID);
            mc.handleAction(MainController.CHANGE_SUB, newSubLocID);

            DrawLocations();
            DrawCharacterResources();
            DrawDifficultyController();
        }

        private void TranslateX()
        {
            
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            ShowImage();	// Display Image
            TranslateX();
            testDA.BeginAnimation(System.Windows.Controls.Image.WidthProperty, testDA);	// Start Animation
        }

        private void ShowImage()
        {
            List<Bitmap> images = new List<Bitmap>();
            images.Add(LongRoadHome.Properties.Resources.CharacterWalk_1);
            images.Add(LongRoadHome.Properties.Resources.CharacterWalk_2);
            images.Add(LongRoadHome.Properties.Resources.CharacterWalk_3);
            images.Add(LongRoadHome.Properties.Resources.CharacterWalk_4);
            images.Add(LongRoadHome.Properties.Resources.CharacterWalk_5);
            images.Add(LongRoadHome.Properties.Resources.CharacterWalk_6);
            images.Add(LongRoadHome.Properties.Resources.CharacterWalk_7);
            images.Add(LongRoadHome.Properties.Resources.CharacterWalk_8);

            testAnimation.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                      images[current].GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());


            current++;
            if (current >= images.Count)
            {
                current = 0;
            }
        }


        private void startAnimationBtn_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush temp = new ImageBrush(System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                      LongRoadHome.Properties.Resources.parallax_mountain_mountains.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions()));
            temp.TileMode = TileMode.Tile;
            //temp.ViewportUnits = BrushMappingMode.Absolute;
            temp.Viewport = new Rect(0, 0, 1, 1);
            rectBck.Fill = temp;

            DoubleAnimation da = new DoubleAnimation();
            da.From = -current2;
            da.To = -current2 - 1;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(10));
            da.Completed += TranslateX;
            TranslateTransform transform = new TranslateTransform();
            rectBck.RenderTransform = transform;
            //testBackground.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.XProperty, da);
            current2++;
            //var transform = new TranslateTransform(0 - current2, 0);
            //testBackground.RenderTransform = transform;
            //testBackground.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
            //current2++;
        }

        private void TranslateX(object sender, EventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = -current2;
            da.To = -current2 - 1;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(10));
            da.Completed += TranslateX;
            TranslateTransform transform = new TranslateTransform();
            rectBck.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.XProperty, da);
            current2++;
        }
    }
}
