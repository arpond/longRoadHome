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
            mc.handleAction(MainController.NEW_GAME);
        }

        private void changeUI_Click(object sender, RoutedEventArgs e)
        {
            ImageButton clicked = sender as ImageButton;

            if (clicked == worldMapBtn && screenState != WORLD_MAP)
            {
                screenState = WORLD_MAP;
                worldMapBtn.EnabledButton = false;
                subMapBtn.EnabledButton = true;
                inventoryBtn.EnabledButton = true;
                SublocationMapView.Visibility = Visibility.Hidden;
                InventoryView.Visibility = Visibility.Hidden;
                //WorldMap wm = new WorldMap();
                //GameSpace.Child = wm;
                if (DrawYesNoOption("Yes or No?"))
                {
                    DrawDialogueBox("YES!");
                }
                else
                {
                    DrawDialogueBox("No :(");
                }

            }
            else if (clicked.Name == "subMapBtn" && screenState != SUB_MAP)
            {
                mc.handleAction(MainController.VIEW_SUB_MAP);

            }
            else if (clicked.Name == "inventoryBtn" && screenState != INVENTORY)
            {
                mc.handleAction(MainController.VIEW_INVENTORY);
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

        public void DrawSublocationMap(List<Sublocation> subloc, int currentSubLocation)
        {
            screenState = SUB_MAP;
            worldMapBtn.EnabledButton = true;
            subMapBtn.EnabledButton = false;
            inventoryBtn.EnabledButton = true;
            SublocationMapView.Visibility = Visibility.Visible;
            InventoryView.Visibility = Visibility.Hidden;

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
                SublocationButton button = new SublocationButton();
                button.EnabledImage = temp;
                button.DisabledImage = temp2;
                button.DisplayedImage = temp;
                button.Scavenged = scavenged[i];
                button.Click += new RoutedEventHandler(SublocationClicked);

                Grid.SetRow(button, 0);
                Grid.SetColumn(button, i);
                Grid.SetRowSpan(button, 2);
                Sublocations.Children.Add(button);
            }
        }
        public void DrawDialogueBox(String text)
        {
            SimpleMessageBox.Show(text, string.Empty, MessageBoxButton.OK, Window.GetWindow(this));
        }
        public bool DrawYesNoOption(String text)
        {
            MessageBoxResult result = SimpleMessageBox.Show(text, string.Empty, MessageBoxButton.YesNo, Window.GetWindow(this));
            if(result == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
        public void DrawInventory(ArrayList inventory)
        {
            screenState = INVENTORY;
            worldMapBtn.EnabledButton = true;
            subMapBtn.EnabledButton = true;
            inventoryBtn.EnabledButton = false;
            SublocationMapView.Visibility = Visibility.Hidden;
            InventoryView.Visibility = Visibility.Visible;
            List<UIItem> inv = new List<UIItem>();

            InventoryGrid.Children.Clear();

            List<Item> dummy = new List<Item>();
            dummy.Add(new Item("ID:3,Name:TestItem,Amount:3,Description:test item 3,ActiveEffect,PassiveEffect,Requirements"));
            dummy.Add(new Item("ID:23,Name:TestItem,Amount:1,Description:test item 23,ActiveEffect,PassiveEffect,Requirements"));
            dummy.Add(new Item("ID:33,Name:TestItem,Amount:4,Description:test item 33,ActiveEffect,PassiveEffect,Requirements"));
            dummy.Add(new Item("ID:31,Name:TestItem,Amount:1,Description:test item 31,ActiveEffect,PassiveEffect,Requirements"));
            dummy.Add(new Item("ID:5,Name:TestItem,Amount:2,Description:test item 5,ActiveEffect,PassiveEffect,Requirements"));
            dummy.Add(new Item("ID:8,Name:TestItem,Amount:1,Description:test item 8,ActiveEffect,PassiveEffect,Requirements"));
            dummy.Add(new Item("ID:16,Name:TestItem,Amount:6,Description:test item 16,ActiveEffect,PassiveEffect,Requirements"));
            dummy.Add(new Item("ID:20,Name:TestItem,Amount:1,Description:test item 20,ActiveEffect,PassiveEffect,Requirements"));
            dummy.Add(new Item("ID:99,Name:TestItem,Amount:1,Description:test item 9,ActiveEffect,PassiveEffect,Requirements"));

            for (int i = 0; i < dummy.Count; i++ )
            {
                ItemButton button = new ItemButton();
                BitmapImage temp = new BitmapImage();
                temp.BeginInit();
                //temp.UriSource = new Uri("pack://application:,,,/LongRoadHome;Resources/" + inventory[i].GetImagePath());
                temp.UriSource = new Uri("pack://application:,,,/Resources/item_placeholder.png");
                temp.EndInit();
                button.ItemIcon = temp;
                button.Description = dummy[i].description;

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
            //throw new NotImplementedException();
            return 1;
        }
        public void DrawEventResult(String optionResult, List<String> results)
        {
            //throw new NotImplementedException();
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
            //throw new NotImplementedException();
        }
        public void DrawDiscovery(string discovery)
        {
            throw new NotImplementedException();
        }

        private void SublocationClicked(object sender, RoutedEventArgs e)
        {
            SublocationButton sbtn = sender as SublocationButton;
            if(sbtn != null)
            {
                int index = Sublocations.Children.IndexOf(sbtn);
                if (index+1 == _UIModel.SublocationModel.CurrentSublocation)
                {
                    if (!_UIModel.SublocationModel.Scavenged[index] && DrawYesNoOption("Do you wish to scavenge the location?"))
                    {
                        mc.ScavangeSublocation();
                    }
                    else if (!_UIModel.SublocationModel.Scavenged[index])
                    {
                        DrawDialogueBox("You are already at that location");
                    }
                }
                else if (DrawYesNoOption("Are you sure you wish to move to that location?"))
                {
                    mc.ChangeSubLocation(index + 1);
                    UpdatePlayer();
                }
            }
        }
    }
}










