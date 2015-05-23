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
    public partial class TransparentButton : UserControl
    {
        public TransparentButton()
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

        public int data { get; set; }

        public bool ImageSwitch
        {
            get { return (bool)GetValue(TransparentButton.ImageSwitchProperty); }
            set { SetValue(TransparentButton.ImageSwitchProperty, value); }
        }

        public BitmapImage EnabledImage
        {
            get { return (BitmapImage)GetValue(TransparentButton.EnabledImageProperty); }
            set { SetValue(TransparentButton.EnabledImageProperty, value); }
        }

        public BitmapImage DisabledImage
        {
            get { return (BitmapImage)GetValue(TransparentButton.DisabledImageProperty); }
            set { SetValue(TransparentButton.DisabledImageProperty, value); }
        }

        public BitmapImage DisplayedImage
        {
            get { return (BitmapImage)GetValue(TransparentButton.DisplayedImageProperty); }
            set { SetValue(TransparentButton.DisplayedImageProperty, value); }
        }

        /// <summary>
        /// Identifies the Enabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty ImageSwitchProperty =
            DependencyProperty.Register("ImageSwitch", typeof(bool), typeof(TransparentButton),
             new PropertyMetadata(OnEnabledChanged));

        /// <summary>
        /// Identifies the Enabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty EnabledImageProperty =
            DependencyProperty.Register("EnabledImage", typeof(BitmapImage), typeof(TransparentButton));

        /// <summary>
        /// Identifies the Disabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(BitmapImage), typeof(TransparentButton));

        /// <summary>
        /// Identifies the Disabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty DisplayedImageProperty =
            DependencyProperty.Register("DisplayedImage", typeof(BitmapImage), typeof(TransparentButton));

        private static void OnEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TransparentButton imgBtn = sender as TransparentButton;
            if (imgBtn != null)
            {
                if (imgBtn.ImageSwitch)
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
