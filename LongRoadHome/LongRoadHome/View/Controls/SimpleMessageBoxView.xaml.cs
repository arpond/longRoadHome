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
using System.Windows.Shapes;

namespace uk.ac.dundee.arpond.longRoadHome.View.Controls
{
    /// <summary>
    /// Interaction logic for SimpleMessageBoxView.xaml
    /// </summary>
    public partial class SimpleMessageBoxView : Window
    {
        public SimpleMessageBoxView()
        {
            InitializeComponent();
            SetButtonVisibility();
        }

        public MessageBoxButton Buttons
        {
            get { return (MessageBoxButton)GetValue(SimpleMessageBoxView.ButtonsProperty); }
            set { SetValue(SimpleMessageBoxView.ButtonsProperty, value); }
        }

        public MessageBoxResult Result
        {
            get { return (MessageBoxResult)GetValue(SimpleMessageBoxView.ResultProperty); }
            set { SetValue(SimpleMessageBoxView.ResultProperty, value); }
        }

        /// <summary>
        /// Identifies the buttons Dependency Property
        /// </summary>
        public static readonly DependencyProperty ButtonsProperty =
            DependencyProperty.Register("Buttons", typeof(MessageBoxButton), typeof(SimpleMessageBoxView));

        /// <summary>
        /// Identifies the result Dependency Property
        /// </summary>
        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register("Result", typeof(MessageBoxResult), typeof(SimpleMessageBoxView));

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            this.Close();
        }

        public void SetButtonVisibility()
        {
            switch (Buttons)
            {
                case MessageBoxButton.OK:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed;
                    btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.OKCancel:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNo:
                    btnOk.Visibility = Visibility.Collapsed;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNoCancel:
                    btnOk.Visibility = Visibility.Collapsed;
                    btnCancel.Visibility = Visibility.Visible;
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
