using System;
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

namespace uk.ac.dundee.arpond.longRoadHome.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            this.ShowsNavigationUI = false;
        }

        private void debugBtn_Click(object sender, RoutedEventArgs e)
        {
            Debug debug = new Debug();
            this.NavigationService.Navigate(debug);
        }

        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            GameView gv = new GameView(this);
            this.NavigationService.Navigate(gv);
        }

        public void ReturnToMainMenu(MainMenu mainMenu)
        {
            NavigationService ns = mainMenu.NavigationService;
            if (ns == null)
            {
                (Application.Current.MainWindow as NavigationWindow).Navigate(mainMenu);
            }
        }
    }
}
