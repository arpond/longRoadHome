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
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.Model.Discovery;

namespace uk.ac.dundee.arpond.longRoadHome.View
{
    /// <summary>
    /// Interaction logic for DiscoveriesView.xaml
    /// </summary>
    public partial class DiscoveriesView : Page
    {
        MainMenu mainMenu;
        public DiscoveriesView(MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            MainController mc = new MainController();
            mc.IntialiseDiscoveries();
            DrawDiscoveries(mc.GetDiscovered(), mc.GetMaxNumberOfDiscoveries());
        }

        public void DrawDiscoveries(List<Discovery> discs, int max)
        {
            discoveriesView.Children.Clear();
            for (int i = 1; i <= max; i++)
            {
                String discText = String.Format("No. {0} - {1}\n", i, "UNDISCOVERED");
                foreach (Discovery disc in discs)
                {
                    int id = disc.GetDiscoveryID();
                    if (i == id)
                    {
                        String text = disc.GetDiscoveryText();
                        discText = String.Format("No. {0} - {1}\n", i, text);
                        break;
                    }
                }
                TextBlock tb = new TextBlock();
                tb.Text = discText;
                tb.FontFamily = new FontFamily("Oswald");
                tb.FontSize = 22;
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                tb.Foreground = new SolidColorBrush(Colors.LightGray);
                tb.Margin = new Thickness(10, 5, 5, 5);
                discoveriesView.Children.Add(tb);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.ReturnToMainMenu(mainMenu);
        }
    }
}
