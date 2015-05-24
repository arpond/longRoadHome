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
using LongRoadHome.Controls;

namespace LongRoadHome
{
    /// <summary>
    /// Interaction logic for baseUC.xaml
    /// </summary>
    public partial class BaseUC : UserControl, INotifyPropertyChanged
    {
        int screenState = 5;
        private const int WORLD_MAP = 0, SUB_MAP = 1, INVENTORY = 2;

        private String _Health;
        private String _Hunger;
        private String _Thirst;
        private String _Sanity;

        public BaseUC()
        {
            InitializeComponent();
        }

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
            DependencyProperty.Register("Health", typeof(string), typeof(BaseUC),
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

            if (clicked.Name == "worldMapBtn" && screenState != WORLD_MAP)
            {
                screenState = WORLD_MAP;
                clicked.Enabled = false;
            }
            else if (clicked.Name == "subMapBtn" && screenState != SUB_MAP)
            {
                screenState = SUB_MAP;
                clicked.Enabled = false;
            }
            else if (clicked.Name == "inventoryBtn" && screenState != INVENTORY)
            {
                screenState = INVENTORY;
                clicked.Enabled = false;
            }
        }


    }
}
