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
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl
    {
        public ImageButton()
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

        public bool EnabledButton
        {
            get { return (bool)GetValue(ImageButton.EnabledButtonProperty); }
            set { SetValue(ImageButton.EnabledButtonProperty, value); }
        }

        public BitmapImage EnabledImage
        {
            get { return (BitmapImage)GetValue(ImageButton.EnabledImageProperty); }
            set { SetValue(ImageButton.EnabledImageProperty, value); }
        }

        public BitmapImage DisabledImage
        {
            get { return (BitmapImage)GetValue(ImageButton.DisabledImageProperty); }
            set { SetValue(ImageButton.DisabledImageProperty, value); }
        }

        public BitmapImage DisplayedImage
        {
            get { return (BitmapImage)GetValue(ImageButton.DisplayedImageProperty); }
            set { SetValue(ImageButton.DisplayedImageProperty, value); }
        }

        /// <summary>
        /// Identifies the Enabled Button Dependency Property
        /// </summary>
        public static readonly DependencyProperty EnabledButtonProperty =
            DependencyProperty.Register("EnabledButton", typeof(bool), typeof(ImageButton),
             new PropertyMetadata(OnEnabledChanged));

        /// <summary>
        /// Identifies the Enabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty EnabledImageProperty =
            DependencyProperty.Register("EnabledImage", typeof(BitmapImage), typeof(ImageButton));

        /// <summary>
        /// Identifies the Disabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(BitmapImage), typeof(ImageButton));

        /// <summary>
        /// Identifies the Disabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty DisplayedImageProperty =
            DependencyProperty.Register("DisplayedImage", typeof(BitmapImage), typeof(ImageButton));

        private static void OnEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ImageButton imgBtn = sender as ImageButton;
            if (imgBtn != null)
            {
                if (imgBtn.EnabledButton)
                {
                    imgBtn.DisplayedImage = imgBtn.EnabledImage;
                }
                else
                {
                    imgBtn.DisplayedImage = imgBtn.DisabledImage;
                }
            }
        }
    }
}
