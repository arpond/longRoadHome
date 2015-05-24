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

namespace uk.ac.dundee.arpond.longRoadHome.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Page, IGameView
    {
        private static Action EmptyDelegate = delegate() { };

        MainController mc;
        int screenState = 5;
        private const int WORLD_MAP = 0, SUB_MAP = 1, INVENTORY = 2;
        private UIModel _UIModel;

        private int _CurrentSublocation;
        private Location _CurrentLocation;
        private Inventory _CurrentInventory;
        //private WorldMap wm;
        private SortedList<int, TransparentButton> worldMapButtons;
        private SortedList<int, System.Windows.Point> buttonAreas;
        private System.Drawing.Bitmap worldMapBM;
        private MainMenu mainMenu;

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

        public void InitialiseWorldMap(System.Drawing.Bitmap worldMapBM, SortedList<int, System.Windows.Point> buttonAreas)
        {
            this.worldMapBM = worldMapBM;
            this.buttonAreas = buttonAreas;

            worldMapButtons = new SortedList<int, TransparentButton>();

            BitmapImage unvisitedBMI = new BitmapImage();
            unvisitedBMI.BeginInit();
            unvisitedBMI.UriSource = new Uri("pack://application:,,,/Resources/unvisited_location.png");
            unvisitedBMI.EndInit();

            BitmapImage visitedBMI = new BitmapImage();
            visitedBMI.BeginInit();
            visitedBMI.UriSource = new Uri("pack://application:,,,/Resources/visited_location.png");
            visitedBMI.EndInit();

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
            mapView.Source = Bitmap2BitmapSource(worldMapBM);
            worldMap.Height = mapView.Source.Height * 1.65;
            worldMap.Width = mapView.Source.Width * 1.65;
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
        /// Starts a new game
        /// </summary>
        public void StartNewGame()
        {
            UpdatePlayer();
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
            mc.handlePotentAction(MainController.CONTINUE, 0);
        }

        public void DrawMainMenu()
        {
            throw new NotImplementedException();
        }

        public void DrawDiscoveries(List<Discovery> discs)
        {
            throw new NotImplementedException();
        }

        public void DrawWorldMap(List<Location> visited, int currentLoc)
        {
            screenState = WORLD_MAP;
            worldMapBtn.EnabledButton = false;
            subMapBtn.EnabledButton = true;
            inventoryBtn.EnabledButton = true;
            SublocationMapView.Visibility = Visibility.Hidden;
            InventoryView.Visibility = Visibility.Hidden;
            WorldMapView.Visibility = Visibility.Visible;
            foreach(Location vist in visited)
            {
                TransparentButton tb;
                if(worldMapButtons.TryGetValue(vist.GetLocationID(), out tb))
                {
                    tb.ImageSwitch = true;
                }
            }
            Point point;
            if (buttonAreas.TryGetValue(currentLoc, out point))
            {
                worldMap.Children.Remove(characterPointer);
                zoomBorder.charLoc = point;
                zoomBorder.Reset();
                BitmapImage temp = new BitmapImage();
                temp.BeginInit();
                temp.UriSource = new Uri("pack://application:,,,/Resources/Character-stand.png");
                temp.EndInit();
                characterPointer.Source = temp;
                characterPointer.Height = 35;
                characterPointer.Width = 10;
                Canvas.SetLeft(characterPointer, point.X + 4);
                Canvas.SetTop(characterPointer, point.Y - 20);
                worldMap.Children.Add(characterPointer);
            }
        }

        /// <summary>
        /// Draws the sublocation map
        /// </summary>
        /// <param name="subloc">The list of sublocations to draw</param>
        /// <param name="currentSubLocation">The current sublocation</param>
        public void DrawSublocationMap(List<Sublocation> subloc, int currentSubLocation)
        {
            screenState = SUB_MAP;
            worldMapBtn.EnabledButton = true;
            subMapBtn.EnabledButton = false;
            inventoryBtn.EnabledButton = true;
            SublocationMapView.Visibility = Visibility.Visible;
            InventoryView.Visibility = Visibility.Hidden;
            WorldMapView.Visibility = Visibility.Hidden;

            List<String> imagePaths = subloc.Select(value => value.GetImagePath()).ToList();
            List<bool> scavenged = subloc.Select(value => value.GetScavenged()).ToList();

            _UIModel.SublocationModel.ImagePaths = imagePaths;
            _UIModel.SublocationModel.Scavenged = scavenged;
            _UIModel.SublocationModel.CurrentSublocation = currentSubLocation;

            Sublocations.Children.Clear();

            for (int i = 0; i < subloc.Count; i++ )
            {
                BitmapImage temp = new BitmapImage();
                temp.BeginInit();
                //temp.UriSource = new Uri("pack://application:,,,/LongRoadHome;Resources/" + subloc[i].GetImagePath());
                temp.UriSource = new Uri("pack://application:,,,/Resources/Loc_Res_PlaceHolder.png");
                temp.EndInit();
                BitmapImage temp2 = new BitmapImage();
                temp2.BeginInit();
                temp2.UriSource = new Uri("pack://application:,,,/Resources/Loc_Res_PlaceHolder_Scavenged.png");
                temp2.EndInit();
                //Image image = new Image();
                //image.Source = temp;
                TransparentButton button = new TransparentButton();
                button.EnabledImage = temp;
                button.DisabledImage = temp2;
                button.DisplayedImage = temp;
                button.ImageSwitch = scavenged[i];
                button.Click += new RoutedEventHandler(SublocationClicked);

                Grid.SetRow(button, 0);
                Grid.SetColumn(button, i);
                Grid.SetRowSpan(button, 2);
                Sublocations.Children.Add(button);
            }
        }

        /// <summary>
        /// Draws the inventory passed
        /// </summary>
        /// <param name="inventory">The inventory to draw</param>
        public void DrawInventory(ArrayList inventory)
        {
            screenState = INVENTORY;
            worldMapBtn.EnabledButton = true;
            subMapBtn.EnabledButton = true;
            inventoryBtn.EnabledButton = false;
            SublocationMapView.Visibility = Visibility.Hidden;
            InventoryView.Visibility = Visibility.Visible;
            WorldMapView.Visibility = Visibility.Hidden;
            List<UIItem> inv = new List<UIItem>();

            InventoryGrid.Children.Clear();

            for (int i = 0; i < inventory.Count; i++ )
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

                Grid.SetRow(button, i/4 + 1);
                Grid.SetColumn(button, (i % 4) + 1 );

                InventoryGrid.Children.Add(button);
            }

            //foreach (item i in inventory)
            //{
            //    uiitem t = new uiitem()
            //    {
            //        id = i.itemid,
            //        description = i.description,
            //        iconpath = "item_placeholder.png"
            //        iconpath = i.iconpath
            //    };
            //}
            //_uimodel.uiinventory = new uiinventory() { inventory = inv };
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
            List<String> buttonText = new List<string>(){ "Option 1", "Option 2", "Option 3", "Option 4"};

            string optionsText = "";
            for(int i = 1; i<=options.Count; i++)
            {
                optionsText += i + ". " + options[i-1] + "\n";
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

        public void PlayAudio(String audioFile)
        {
            throw new NotImplementedException();
        }
        public void Animate(List<String> imageFileNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays the results from scavenging
        /// </summary>
        /// <param name="scavenged">The items scavenged</param>
        public void DrawScavengeResults(List<Item> scavenged)
        {
            String results = "You scavenged the following:\n";
            foreach(Item item in scavenged)
            {
                results += String.Format("{0} x {1}\n", item.name, item.amount);
            }
            SimpleMessageBox.Show("Scavenging Results", results, Window.GetWindow(this));
        }

        public void DrawDiscovery(string discovery)
        {
            throw new NotImplementedException();
        }

        private void UseItemClicked(object sender, RoutedEventArgs e)
        {
            ItemButton itemBtn = sender as ItemButton;
            if (itemBtn != null)
            {
                mc.handlePotentAction(MainController.USE_ITEM, itemBtn.ItemSlot);
            }
        }

        private void DiscardItemClicked(object sender, RoutedEventArgs e)
        {
            ItemButton itemBtn = sender as ItemButton;
            if (itemBtn != null)
            {
                mc.handlePotentAction(MainController.DISCARD_ITEM, itemBtn.ItemSlot);
            }
        }

        void location_Click(object sender, RoutedEventArgs e)
        {
            TransparentButton tb = sender as TransparentButton;
            int id = tb.data;
            mc.handlePotentAction(MainController.CHANGE_LOC, id);
        }

        private void SublocationClicked(object sender, RoutedEventArgs e)
        {
            TransparentButton sbtn = sender as TransparentButton;
            if(sbtn != null)
            {
                int index = Sublocations.Children.IndexOf(sbtn);
                if (index+1 == _UIModel.SublocationModel.CurrentSublocation)
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

        private void game_Loaded(object sender, RoutedEventArgs e)
        {
            zoomBorder.Reset();
        }

        public void ReturnToMainMenu()
        {
            mainMenu.ReturnToMainMenu();
        }
    }


}










