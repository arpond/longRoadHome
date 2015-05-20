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
using uk.ac.dundee.arpond.longRoadHome.View.UIObjects;

namespace uk.ac.dundee.arpond.longRoadHome.View.Controls
{
    /// <summary>
    /// Interaction logic for SublocationButton.xaml
    /// </summary>
    public partial class SublocationButton : UserControl
    {
        public SublocationButton()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Click;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

        public bool Scavenged
        {
            get { return (bool)GetValue(SublocationButton.EnabledButtonProperty); }
            set { SetValue(SublocationButton.EnabledButtonProperty, value); }
        }

        public BitmapImage EnabledImage
        {
            get { return (BitmapImage)GetValue(SublocationButton.EnabledImageProperty); }
            set { SetValue(SublocationButton.EnabledImageProperty, value); }
        }

        public BitmapImage DisabledImage
        {
            get { return (BitmapImage)GetValue(SublocationButton.DisabledImageProperty); }
            set { SetValue(SublocationButton.DisabledImageProperty, value); }
        }

        public BitmapImage DisplayedImage
        {
            get { return (BitmapImage)GetValue(SublocationButton.DisplayedImageProperty); }
            set { SetValue(SublocationButton.DisplayedImageProperty, value); }
        }

        /// <summary>
        /// Identifies the Enabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty EnabledButtonProperty =
            DependencyProperty.Register("EnabledButton", typeof(bool), typeof(SublocationButton),
             new PropertyMetadata(OnEnabledChanged));

        /// <summary>
        /// Identifies the Enabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty EnabledImageProperty =
            DependencyProperty.Register("EnabledImage", typeof(BitmapImage), typeof(SublocationButton));

        /// <summary>
        /// Identifies the Disabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(BitmapImage), typeof(SublocationButton));

        /// <summary>
        /// Identifies the Disabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty DisplayedImageProperty =
            DependencyProperty.Register("DisplayedImage", typeof(BitmapImage), typeof(SublocationButton));

        private static void OnEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SublocationButton imgBtn = sender as SublocationButton;
            if (imgBtn != null)
            {
                if (imgBtn.Scavenged)
                {
                    imgBtn.DisplayedImage = imgBtn.DisabledImage;
                }
                else
                {
                    imgBtn.DisplayedImage = imgBtn.EnabledImage;
                }
            }
        }
    }
}
