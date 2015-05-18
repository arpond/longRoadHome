using System;
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
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter;

namespace LongRoadHome.Controls
{
    /// <summary>
    /// Interaction logic for BaseInterface.xaml
    /// </summary>
    public partial class BaseInterface : UserControl, INotifyPropertyChanged
    {

        public BaseInterface()
        {
            InitializeComponent();
        }

        int screenState = 5;
        private const int WORLD_MAP = 0, SUB_MAP = 1, INVENTORY = 2;

        private String _Health;
        private String _Hunger;
        private String _Thirst;
        private String _Sanity;
        private Location _CurrentLocation;
        private uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter.Inventory _CurrentInventory;

        public Location CurrentLocation
        {
            get { return _CurrentLocation;  }
            set
            {
                _CurrentLocation = value;
                RaisePropertyChanged("CurrentLocation");
            }
        }

        public static readonly DependencyProperty CurrentLocationProperty =
            DependencyProperty.Register("CurrentLocation", typeof(Location), typeof(BaseInterface));

        public uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter.Inventory CurrentInventory
        {
            get { return _CurrentInventory; }
            set
            {
                _CurrentInventory = value;
                RaisePropertyChanged("CurrentInventory");
            }
        }

        public static readonly DependencyProperty CurrentInventoryProperty =
            DependencyProperty.Register("CurrentInventory", typeof(uk.ac.dundee.arpond.longRoadHome.Model.PlayerCharacter.Inventory), typeof(BaseInterface));

        public string Health
        {
            get { return _Health; }
            set 
            { 
                _Health = value;
                RaisePropertyChanged("Health"); 
            }
        }

        public static readonly DependencyProperty HealthProperty = 
            DependencyProperty.Register("Health", typeof(string), typeof(BaseInterface),
            new UIPropertyMetadata(string.Empty));

        public string Thirst
        {
            get { return _Thirst; }
            set
            {
                _Thirst = value;
                RaisePropertyChanged("Thirst");
            }
        }

        public static readonly DependencyProperty ThirstProperty =
            DependencyProperty.Register("Thirst", typeof(string), typeof(BaseInterface),
            new UIPropertyMetadata(string.Empty));

        public string Hunger
        {
            get { return _Hunger; }
            set
            {
                _Hunger = value;
                RaisePropertyChanged("Hunger");
            }
        }

        public static readonly DependencyProperty HungerProperty =
            DependencyProperty.Register("Hunger", typeof(string), typeof(BaseInterface),
            new UIPropertyMetadata(string.Empty));

        public string Sanity
        {
            get { return _Sanity; }
            set
            {
                _Sanity = value;
                RaisePropertyChanged("Sanity");
            }
        }

        public static readonly DependencyProperty SanityProperty =
            DependencyProperty.Register("Sanity", typeof(string), typeof(BaseInterface),
            new UIPropertyMetadata(string.Empty));

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void changeUI_Click(object sender, RoutedEventArgs e)
        {
            ImageButton clicked = sender as ImageButton;

            if (clicked == worldMapBtn && screenState != WORLD_MAP)
            {
                screenState = WORLD_MAP;
                worldMapBtn.IsEnabled = false;
                subMapBtn.IsEnabled = true;
                inventoryBtn.IsEnabled = true;
                WorldMap wm = new WorldMap();
                GameSpace.Child = wm;
            }
            else if (clicked.Name == "subMapBtn" && screenState != SUB_MAP)
            {
                screenState = SUB_MAP;
                worldMapBtn.IsEnabled = true;
                subMapBtn.IsEnabled = false;
                inventoryBtn.IsEnabled = true;
                SublocationMap slm = new SublocationMap();
                GameSpace.Child = slm;
            }
            else if (clicked.Name == "inventoryBtn" && screenState != INVENTORY)
            {
                screenState = INVENTORY;
                worldMapBtn.IsEnabled = true;
                subMapBtn.IsEnabled = true;
                inventoryBtn.IsEnabled = false;
                Inventory inv = new Inventory();
                GameSpace.Child = inv;
            }
        }

    }
}
