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

namespace uk.ac.dundee.arpond.longRoadHome.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        GameView gv;

        public bool Continue
        {
            get { return (bool)GetValue(MainMenu.ContinueProperty); }
            set { SetValue(MainMenu.ContinueProperty, value); }
        }

        /// <summary>
        /// Identifies the Enabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty ContinueProperty =
            DependencyProperty.Register("Continue", typeof(bool), typeof(MainMenu));


        public MainMenu()
        {
            InitializeComponent();
            this.ShowsNavigationUI = false;
            CheckIfContinue();
        }

        /// <summary>
        /// Shows the discoveries
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void discoveriesBtn_Click(object sender, RoutedEventArgs e)
        {
            //Debug debug = new Debug();
            //this.NavigationService.Navigate(debug);
            DiscoveriesView discoveries = new DiscoveriesView(this);
            this.NavigationService.Navigate(discoveries);
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            MainController tmc = new MainController();
            try
            {
                if (!tmc.CheckIfSaveExists())
                {
                    gv = new GameView(this, 0);
                    this.NavigationService.Navigate(gv);
                }
                else
                {
                    GameView temp = new GameView();
                    if (temp.DrawYesNoOption("This will overwrite your current save. Are you sure?"))
                    {
                        gv = new GameView(this, 0);
                        this.NavigationService.Navigate(gv);
                    }
                }
            }
            catch (Exception ex)
            {
                gv = new GameView(this, 0);
                this.NavigationService.Navigate(gv);
            }
        }

        /// <summary>
        /// Returns to the main menu
        /// </summary>
        /// <param name="mainMenu"></param>
        public void ReturnToMainMenu(MainMenu mainMenu)
        {
            NavigationService ns = mainMenu.NavigationService;
            if (ns == null)
            {
                (Application.Current.MainWindow as NavigationWindow).Navigate(mainMenu);
                CheckIfContinue();
            }
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        public void ExitGame()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Checks if there is an existing save game
        /// </summary>
        private void CheckIfContinue()
        {
            
            MainController tmc = new MainController();
            try
            {
                if (!tmc.CheckIfSaveExists())
                {
                    Continue = false;
                    continueBtn.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Continue = true;
                    continueBtn.Visibility = Visibility.Visible;
                }
            }
            catch (Exception e)
            {
                Continue = false;
                continueBtn.Visibility = Visibility.Collapsed;
            }
            
        }

        /// <summary>
        /// Loads a game from save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continueBtn_Click(object sender, RoutedEventArgs e)
        {
            gv = new GameView(this,1);
            this.NavigationService.Navigate(gv);
        }

        /// <summary>
        /// Shows the game isntructions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void instructionsBtn_Click(object sender, RoutedEventArgs e)
        {
            Tutorial tut = new Tutorial(this);
            this.NavigationService.Navigate(tut);
        }
    }
}
