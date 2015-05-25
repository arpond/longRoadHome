using uk.ac.dundee.arpond.longRoadHome.View.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using uk.ac.dundee.arpond.longRoadHome.View.UIObjects;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace uk.ac.dundee.arpond.longRoadHome.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Page, IGameView
    {
        #region Fields
        private static Action EmptyDelegate = delegate() { };

        MainController mc;
        int screenState = 5;
        private const int WORLD_MAP = 0, SUB_MAP = 1, INVENTORY = 2;
        private UIModel _UIModel;

        //private WorldMap wm;
        private SortedList<int, TransparentButton> worldMapButtons;
        private SortedList<int, System.Windows.Point> buttonAreas;
        private System.Drawing.Bitmap worldMapBM;
        private MainMenu mainMenu;
        #endregion

        #region Constructors
        public GameView()
        {
            InitializeComponent();
            _UIModel = new UIModel()
            {
                PlayerModel = new UIPlayer { Health = 90, Hunger = 90, Sanity = 90, Thirst = 90 },
                SublocationModel = new UISublocations { CurrentSublocation = 1, ImagePaths = new List<string>(), Scavenged = new List<bool>() },
                UIInventory = new UIInventory { Inventory = new List<UIItem>() }
            };

            this.DataContext = _UIModel;

            mc = new MainController(this);
            mc.handlePotentAction(MainController.NEW_GAME, 0);
        }

        public GameView(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
            InitializeComponent();
            _UIModel = new UIModel()
            {
                PlayerModel = new UIPlayer { Health = 90, Hunger = 90, Sanity = 90, Thirst = 90 },
                SublocationModel = new UISublocations { CurrentSublocation = 1, ImagePaths = new List<string>(), Scavenged = new List<bool>() },
                UIInventory = new UIInventory { Inventory = new List<UIItem>() }
            };

            this.DataContext = _UIModel;

            mc = new MainController(this);
            mc.handlePotentAction(MainController.NEW_GAME, 0);
        }
        #endregion

        #region General UI Functions
        /// <summary>
        /// Starts a new game
        /// </summary>
        public void StartNewGame()
        {
            Dispatcher.Invoke(new Action(() => DrawTutorial()));
        }

        /// <summary>
        /// Loads the saved game
        /// </summary>
        public void LoadGame()
        {
            mc.handlePotentAction(MainController.CONTINUE, 0);
        }

        /// <summary>
        /// Handles changing between various UI views
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeUI_Click(object sender, RoutedEventArgs e)
        {
            ImageButton clicked = sender as ImageButton;

            if (clicked == worldMapBtn && screenState != WORLD_MAP)
            {
                mc.handleIdepotentAction(MainController.VIEW_LOC_MAP);
            }
            else if (clicked.Name == "subMapBtn" && screenState != SUB_MAP)
            {
                mc.handleIdepotentAction(MainController.VIEW_SUB_MAP);

            }
            else if (clicked.Name == "inventoryBtn" && screenState != INVENTORY)
            {
                mc.handleIdepotentAction(MainController.VIEW_INVENTORY);
                //InventoryControl inv = new InventoryControl();
                //GameSpace.Child = inv;
            }
        }

        /// <summary>
        /// When UI is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void game_Loaded(object sender, RoutedEventArgs e)
        {
            zoomBorder.Reset();
            walkingStory.Storyboard.Stop();
            stop = true;
        }

        /// <summary>
        /// Returns to the main menu
        /// </summary>
        public void ReturnToMainMenu()
        {
            mainMenu.ReturnToMainMenu(mainMenu);
        }

        public Dispatcher GetDispatcher()
        {
            return Application.Current.Dispatcher;
        }

        public void UpdatePlayer()
        {
            SortedList<string, int> temp = mc.GetPlayerResources();
            int health, hunger, thirst, sanity;

            temp.TryGetValue(PlayerCharacter.HEALTH, out health);
            temp.TryGetValue(PlayerCharacter.HUNGER, out hunger);
            temp.TryGetValue(PlayerCharacter.THIRST, out thirst);
            temp.TryGetValue(PlayerCharacter.SANITY, out sanity);

            _UIModel.PlayerModel.Health = health;
            _UIModel.PlayerModel.Hunger = hunger;
            _UIModel.PlayerModel.Thirst = thirst;
            _UIModel.PlayerModel.Sanity = sanity;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private BitmapSource Bitmap2BitmapSource(System.Drawing.Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapSource retval;

            try
            {
                retval = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return retval;
        }
        #endregion

        #region Dialogue Boxes
        private void DrawTutorial()
        {
            SimpleMessageBox.Show("Tutorial", "The Tutorial will go here", Window.GetWindow(this));
        }
        /// <summary>
        /// Draws the game over dialogue
        /// </summary>
        public void DrawGameOver()
        {
            SimpleMessageBox.Show("Game Over", "I'm sorry you did not manage to make your way home.", Window.GetWindow(this));
        }

        /// <summary>
        /// Draws the victory dialogue
        /// </summary>
        public void DrawVictory()
        {
            SimpleMessageBox.Show("Congradulations", "You made it home.", Window.GetWindow(this));
        }

        /// <summary>
        /// Draws a simple dialogue box with the text supplied
        /// </summary>
        /// <param name="text">The text to display</param>
        public void DrawDialogueBox(String text)
        {
            SimpleMessageBox.Show(string.Empty, text, MessageBoxButton.OK, Window.GetWindow(this));
        }

        /// <summary>
        /// Draws a Yes No dialogue box with the text supplied
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <returns>If yes was selected</returns>
        public bool DrawYesNoOption(String text)
        {
            MessageBoxResult result = SimpleMessageBox.Show(text, string.Empty, MessageBoxButton.YesNo, Window.GetWindow(this));
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a dialogue box for the event and displays it
        /// </summary>
        /// <param name="eventText">The main text for the event</param>
        /// <param name="options">The text for each option</param>
        /// <returns>The selected option</returns>
        public int DrawEvent(String eventText, List<String> options)
        {
            List<String> buttonText = new List<string>() { "Option 1", "Option 2", "Option 3", "Option 4" };

            string optionsText = "";
            for (int i = 1; i <= options.Count; i++)
            {
                optionsText += i + ". " + options[i - 1] + "\n";
            }

            MessageBoxResult result = MessageBoxResult.None;
            switch (options.Count)
            {
                case 1:
                    SimpleMessageBox.Show(eventText, optionsText, MessageBoxButton.OK, Window.GetWindow(this));
                    result = MessageBoxResult.Yes;
                    break;
                case 2:
                    result = SimpleMessageBox.Show(eventText, optionsText, MessageBoxButton.YesNo, buttonText, Window.GetWindow(this));
                    break;
                case 3:
                    result = SimpleMessageBox.Show(eventText, optionsText, MessageBoxButton.YesNoCancel, buttonText, Window.GetWindow(this));
                    break;
                case 4:
                    result = SimpleMessageBox.Show(eventText, optionsText, MessageBoxButton.OKCancel, buttonText, Window.GetWindow(this));
                    break;
            }

            int selected = 1;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    selected = 1;
                    break;
                case MessageBoxResult.No:
                    selected = 2;
                    break;
                case MessageBoxResult.OK:
                    selected = 4;
                    break;
                case MessageBoxResult.Cancel:
                    if (options.Count == 3)
                    {
                        selected = 3;
                    }
                    else
                    {
                        selected = 4;
                    }
                    break;
            }
            return selected;
        }

        /// <summary>
        /// Displays the results of the event based on the option chosen
        /// </summary>
        /// <param name="optionResult">The Main option result</param>
        /// <param name="results">The text from the effects of the result</param>
        public void DrawEventResult(String optionResult, List<String> results)
        {
            String effectResults = "";
            foreach (String result in results)
            {
                effectResults += result + "\n";
            }

            SimpleMessageBox.Show(optionResult, effectResults, MessageBoxButton.OK, Window.GetWindow(this));
        }

        /// <summary>
        /// Displays the results from scavenging
        /// </summary>
        /// <param name="scavenged">The items scavenged</param>
        public void DrawScavengeResults(List<Item> scavenged)
        {
            String results = "You scavenged the following:\n";
            foreach (Item item in scavenged)
            {
                results += String.Format("{0} x {1}\n", item.name, item.amount);
            }
            SimpleMessageBox.Show("Scavenging Results", results, Window.GetWindow(this));
        }

        /// <summary>
        /// Draws a discovery dialogue box
        /// </summary>
        /// <param name="discovery">The discovery text</param>
        public void DrawDiscovery(string discovery)
        {
            SimpleMessageBox.Show("You made a discovery!", discovery, Window.GetWindow(this));
        }

        #endregion

        #region Discovery Functions
        public void DrawDiscoveries(List<Discovery> discs)
        {
            foreach(Discovery disc in discs)
            {
                int id = disc.GetDiscoveryID();
                String text = disc.GetDiscoveryText();
            }

            throw new NotImplementedException();
        }
        #endregion

        #region Inventory Functions
        /// <summary>
        /// Initialising the inventory with the items in the inventory
        /// </summary>
        /// <param name="inventory">The inventory to et up</param>
        public void InitialiseInventory(ArrayList inventory)
        {
            List<UIItem> inv = new List<UIItem>();

            InventoryGrid.Children.Clear();

            for (int i = 0; i < inventory.Count; i++)
            {
                Item item = inventory[i] as Item;

                ItemButton button = new ItemButton();
                BitmapImage temp = new BitmapImage();
                temp.BeginInit();
                //temp.UriSource = new Uri("pack://application:,,,/LongRoadHome;Resources/" + inventory[i].GetImagePath());
                temp.UriSource = new Uri("pack://application:,,,/Resources/item_placeholder.png");
                temp.EndInit();
                button.ItemIcon = temp;
                button.Description = item.description;
                button.ItemSlot = i;
                button.Amount = item.amount;
                button.UseClick += new RoutedEventHandler(UseItemClicked);
                button.DiscardClick += new RoutedEventHandler(DiscardItemClicked);

                Grid.SetRow(button, i / 4 + 1);
                Grid.SetColumn(button, (i % 4) + 1);

                InventoryGrid.Children.Add(button);
            }
        }

        /// <summary>
        /// Draws the inventory passed
        /// </summary>
        public void DisplayInventory()
        {
            screenState = INVENTORY;
            worldMapBtn.EnabledButton = true;
            subMapBtn.EnabledButton = true;
            inventoryBtn.EnabledButton = false;
            SublocationMapView.Visibility = Visibility.Hidden;
            InventoryView.Visibility = Visibility.Visible;
            WorldMapView.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Event click for a use item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseItemClicked(object sender, RoutedEventArgs e)
        {
            ItemButton itemBtn = sender as ItemButton;
            if (itemBtn != null)
            {
                mc.handlePotentAction(MainController.USE_ITEM, itemBtn.ItemSlot);
            }
        }

        /// <summary>
        /// Event click for a discard item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiscardItemClicked(object sender, RoutedEventArgs e)
        {
            ItemButton itemBtn = sender as ItemButton;
            if (itemBtn != null)
            {
                mc.handlePotentAction(MainController.DISCARD_ITEM, itemBtn.ItemSlot);
            }
        }
        #endregion

        #region Sublocation Map Functions
        /// <summary>
        /// Initialises the sublocation map
        /// </summary>
        /// <param name="subloc">List of sublocation for this map</param>
        /// <param name="currentSubLocation">The current sublocation which the player is at</param>
        public void InitialiseSublocationMap(List<Sublocation> subloc, int currentSubLocation)
        {
            List<String> imagePaths = subloc.Select(value => value.GetImagePath()).ToList();
            List<bool> scavenged = subloc.Select(value => value.GetScavenged()).ToList();

            _UIModel.SublocationModel.Scavenged = scavenged;
            _UIModel.SublocationModel.CurrentSublocation = currentSubLocation;

            Sublocations.Children.Clear();

            for (int i = 0; i < subloc.Count; i++)
            {
                BitmapImage sublocationIcon = new BitmapImage();
                sublocationIcon.BeginInit();
                //Cannot locate resource 'longroadhome;resources/civic-1.png
                sublocationIcon.UriSource = new Uri("pack://application:,,,/Resources/" + subloc[i].GetImagePath() + ".png");
                //sublocationIcon.UriSource = new Uri("pack://application:,,,/Resources/Civic_1.png");
                sublocationIcon.EndInit();
                BitmapImage scavengedIcon = new BitmapImage();
                scavengedIcon.BeginInit();
                scavengedIcon.UriSource = new Uri("pack://application:,,,/Resources/" + subloc[i].GetImagePath() + "_Scavenged.png");
                scavengedIcon.EndInit();

                TransparentButton button = new TransparentButton();
                button.EnabledImage = sublocationIcon;
                button.DisabledImage = scavengedIcon;
                button.DisplayedImage = sublocationIcon;
                button.ImageSwitch = scavenged[i];
                button.Click += new RoutedEventHandler(SublocationClicked);

                Grid.SetRow(button, 0);
                Grid.SetColumn(button, i);
                Grid.SetRowSpan(button, 2);
                Sublocations.Children.Add(button);
            }
        }
        /// <summary>
        /// Draws the sublocation map
        /// </summary>
        public void DisplaySublocationMap()
        {
            screenState = SUB_MAP;
            worldMapBtn.EnabledButton = true;
            subMapBtn.EnabledButton = false;
            inventoryBtn.EnabledButton = true;
            SublocationMapView.Visibility = Visibility.Visible;
            InventoryView.Visibility = Visibility.Hidden;
            WorldMapView.Visibility = Visibility.Hidden;            
        }
        /// <summary>
        /// Updates the data behind the sublocation map
        /// </summary>
        /// <param name="sublocation">The sublocation to update</param>
        /// <param name="mode">The mode for the update, 
        /// 0 = change current location to sublocation passed
        /// 1 = update sublocation so that it is scavenged</param>
        public void UpdateSublocationMap(int sublocation, int mode)
        {
            if (mode == 0)
            {
                _UIModel.SublocationModel.CurrentSublocation = sublocation;
            }
            else if (mode == 1)
            {
                TransparentButton tb = Sublocations.Children[sublocation-1] as TransparentButton;
                tb.ImageSwitch = true;
                _UIModel.SublocationModel.Scavenged[sublocation-1] = true;
            }
        }
        /// <summary>
        /// Determines what happens when a sublocation is clicked
        /// </summary>
        /// <param name="sender">The sender for this event</param>
        /// <param name="e">Event arguments</param>
        private void SublocationClicked(object sender, RoutedEventArgs e)
        {
            TransparentButton sbtn = sender as TransparentButton;
            if (sbtn != null)
            {
                int index = Sublocations.Children.IndexOf(sbtn);
                if (index + 1 == _UIModel.SublocationModel.CurrentSublocation)
                {
                    if (!_UIModel.SublocationModel.Scavenged[index] && DrawYesNoOption("Do you wish to scavenge the location?"))
                    {
                        mc.handlePotentAction(MainController.SCAVENGE, 0);
                    }
                    else if (!_UIModel.SublocationModel.Scavenged[index])
                    {
                        DrawDialogueBox("You are already at that location");
                    }
                }
                else if (DrawYesNoOption("Are you sure you wish to move to that location?"))
                {
                    mc.handlePotentAction(MainController.CHANGE_SUB, index + 1);
                    UpdatePlayer();
                }
            }
        }
        #endregion

        #region World Map Functions
        /// <summary>
        /// Initialise the world map
        /// </summary>
        /// <param name="worldMapBM">Bit representing the background of the map</param>
        /// <param name="buttonAreas">The Points on the map for the location buttons</param>
        public void InitialiseWorldMap(System.Drawing.Bitmap worldMapBM, SortedList<int, System.Windows.Point> buttonAreas)
        {
            this.worldMapBM = worldMapBM;
            this.buttonAreas = buttonAreas;
            worldMapButtons = new SortedList<int, TransparentButton>();

            // Get images for unvisited and visited locations
            BitmapImage unvisitedBMI = new BitmapImage();
            unvisitedBMI.BeginInit();
            unvisitedBMI.UriSource = new Uri("pack://application:,,,/Resources/unvisited_location.png");
            unvisitedBMI.EndInit();

            BitmapImage visitedBMI = new BitmapImage();
            visitedBMI.BeginInit();
            visitedBMI.UriSource = new Uri("pack://application:,,,/Resources/visited_location.png");
            visitedBMI.EndInit();

            // Add buttons for each button area
            foreach (var kvpair in buttonAreas)
            {
                TransparentButton tb = new TransparentButton();
                tb.EnabledImage = unvisitedBMI;
                tb.DisabledImage = visitedBMI;
                tb.DisplayedImage = unvisitedBMI;
                tb.data = kvpair.Key;
                tb.ImageSwitch = false;
                tb.Click += location_Click;

                Canvas.SetLeft(tb, kvpair.Value.X - 5);
                Canvas.SetTop(tb, kvpair.Value.Y - 4);
                worldMap.Children.Add(tb);
                worldMapButtons.Add(kvpair.Key, tb);

            }
            // Set up background
            mapView.Source = Bitmap2BitmapSource(worldMapBM);
            worldMap.Height = mapView.Source.Height * 1.65;
            worldMap.Width = mapView.Source.Width * 1.65;

            // Set start location to visited and set the character pointer to that location button
            TransparentButton startButton;
            if (worldMapButtons.TryGetValue(0, out startButton))
            {
                startButton.ImageSwitch = true;
            }

            Point characterLocation;
            if (buttonAreas.TryGetValue(0, out characterLocation))
            {
                zoomBorder.charLoc = characterLocation;
                zoomBorder.Reset();
                BitmapImage temp = new BitmapImage();
                temp.BeginInit();
                temp.UriSource = new Uri("pack://application:,,,/Resources/Character-stand.png");
                temp.EndInit();
                characterPointer.Source = temp;
                characterPointer.Height = 35;
                characterPointer.Width = 10;
                Canvas.SetLeft(characterPointer, characterLocation.X + 4);
                Canvas.SetTop(characterPointer, characterLocation.Y - 23);
                worldMap.Children.Remove(characterPointer);
                worldMap.Children.Add(characterPointer);
            }
        }

        /// <summary>
        /// Displays the WorldMap
        /// </summary>
        public void DisplayWorldMap()
        {
            screenState = WORLD_MAP;
            worldMapBtn.EnabledButton = false;
            subMapBtn.EnabledButton = true;
            inventoryBtn.EnabledButton = true;
            SublocationMapView.Visibility = Visibility.Hidden;
            InventoryView.Visibility = Visibility.Hidden;
            WorldMapView.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Updates the world map
        /// </summary>
        /// <param name="newLocation"></param>
        public void UpdateWorldMap(int newLocation)
        {
            TransparentButton tb;
            if (worldMapButtons.TryGetValue(newLocation, out tb))
            {
                tb.ImageSwitch = true;
            }

            Point characterLocation;
            if (buttonAreas.TryGetValue(newLocation, out characterLocation))
            {
                zoomBorder.charLoc = characterLocation;
                zoomBorder.Reset();
                Canvas.SetLeft(characterPointer, characterLocation.X + 4);
                Canvas.SetTop(characterPointer, characterLocation.Y - 23);
            }
        }

        /// <summary>
        /// Determines what happens when a location is clicked
        /// </summary>
        /// <param name="sender">The object which sent the click</param>
        /// <param name="e">Event arguments</param>
        private void location_Click(object sender, RoutedEventArgs e)
        {
            TransparentButton tb = sender as TransparentButton;
            int id = tb.data;
            mc.handlePotentAction(MainController.CHANGE_LOC, id);
        }
        #endregion

        #region Animation Functions

        int current = 0;
        bool stop = false;
        List<System.Drawing.Bitmap> images = new List<System.Drawing.Bitmap>();

        public void AnimateFrames(List<String> imageFileNames)
        {
            images.Add(uk.ac.dundee.arpond.longRoadHome.Properties.Resources.CharacterWalk_1);
            images.Add(uk.ac.dundee.arpond.longRoadHome.Properties.Resources.CharacterWalk_2);
            images.Add(uk.ac.dundee.arpond.longRoadHome.Properties.Resources.CharacterWalk_3);
            images.Add(uk.ac.dundee.arpond.longRoadHome.Properties.Resources.CharacterWalk_4);
            images.Add(uk.ac.dundee.arpond.longRoadHome.Properties.Resources.CharacterWalk_5);
            images.Add(uk.ac.dundee.arpond.longRoadHome.Properties.Resources.CharacterWalk_6);
            images.Add(uk.ac.dundee.arpond.longRoadHome.Properties.Resources.CharacterWalk_7);
            images.Add(uk.ac.dundee.arpond.longRoadHome.Properties.Resources.CharacterWalk_8);

            animationImage.Visibility = Visibility.Visible;
            animationBackground.Visibility = Visibility.Visible;
            healthBar.Visibility = Visibility.Hidden;
            hungerBar.Visibility = Visibility.Hidden;
            thirstbar.Visibility = Visibility.Hidden;
            sanityBar.Visibility = Visibility.Hidden;
            navMenu.Visibility = Visibility.Hidden;
            SublocationMapView.Visibility = Visibility.Hidden;
            stop = false;
            walkingStory.Storyboard.Begin();

            mountainsFarSV.Visibility = Visibility.Visible;
            mountainsFarSV.ScrollToBottom();
            MoveTo(mountainsFar, -120);

            mountainsSV.Visibility = Visibility.Visible;
            mountainsSV.ScrollToBottom();
            MoveTo(mountains, -200);

            treesSV.Visibility = Visibility.Visible;
            treesSV.ScrollToBottom();
            MoveTo(trees, -400);

            treesForeSV.Visibility = Visibility.Visible;
            treesForeSV.ScrollToBottom();
            MoveTo(treesForground, -700);

            bck.Visibility = Visibility.Visible;
        }



        private ImageSource ResourceToImageSource(System.Drawing.Bitmap res)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                      res.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());
        }


        public void MoveTo(Image target, double newX)
        {
            Vector offset = VisualTreeHelper.GetOffset(target);
            //var left = offset.X;
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim2 = new DoubleAnimation(0, newX, TimeSpan.FromSeconds(20));
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
        }

        public void EndAnimation()
        {
            animationImage.Visibility = Visibility.Hidden;
            animationBackground.Visibility = Visibility.Hidden;
            healthBar.Visibility = Visibility.Visible;
            hungerBar.Visibility = Visibility.Visible;
            thirstbar.Visibility = Visibility.Visible;
            sanityBar.Visibility = Visibility.Visible;
            navMenu.Visibility = Visibility.Visible;
            walkingStory.Storyboard.Stop();
            stop = true;
            mountainsFarSV.Visibility = Visibility.Collapsed;
            mountainsSV.Visibility = Visibility.Collapsed;
            treesSV.Visibility = Visibility.Collapsed;
            treesForeSV.Visibility = Visibility.Collapsed;

            bck.Visibility = Visibility.Hidden;
            if (screenState == SUB_MAP)
            {
                SublocationMapView.Visibility = Visibility.Visible;
            }
            
        }

        private void walkingAnimation_Completed(object sender, EventArgs e)
        {
            ShowImage();
            if (!stop)
            {
                walkingStory.Storyboard.Begin();
            }
        }

        private void ShowImage()
        {
            if (images != null && images.Count > 0)
            {
                animationImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
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
            
        }
        #endregion

        #region Audio Functions
        public void PlayAudio(String audioFile)
        {
            throw new NotImplementedException();
        }
        #endregion



        
    }
}










