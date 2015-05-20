using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace uk.ac.dundee.arpond.longRoadHome.View.UIObjects
{
    public class UIImageButton : DependencyObject, INotifyPropertyChanged
    {
        public bool EnabledButton
        {
            get { return (bool)GetValue(UIImageButton.EnabledButtonProperty); }
            set { SetValue(UIImageButton.EnabledButtonProperty, value); }
        }

        public BitmapImage EnabledImage
        {
            get { return (BitmapImage)GetValue(UIImageButton.EnabledImageProperty); }
            set { SetValue(UIImageButton.EnabledImageProperty, value); }
        }
        
        public BitmapImage DisabledImage
        {
            get { return (BitmapImage)GetValue(UIImageButton.DisabledImageProperty); }
            set { SetValue(UIImageButton.DisabledImageProperty, value); }
        }

        public BitmapImage DisplayedImage
        {
            get { return (BitmapImage)GetValue(UIImageButton.DisplayedImageProperty); }
            set { SetValue(UIImageButton.DisplayedImageProperty, value); }
        }

        /// <summary>
        /// Identifies the Enabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty EnabledButtonProperty =
            DependencyProperty.Register("EnabledButton", typeof(bool), typeof(UIItem),
             new FrameworkPropertyMetadata(false, new PropertyChangedCallback(EnabledChanged)));

        /// <summary>
        /// Identifies the Enabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty EnabledImageProperty =
            DependencyProperty.Register("EnabledImage", typeof(BitmapImage), typeof(UIItem));

        /// <summary>
        /// Identifies the Disabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(BitmapImage), typeof(UIItem));

        /// <summary>
        /// Identifies the Disabled Image Dependency Property
        /// </summary>
        public static readonly DependencyProperty DisplayedImageProperty =
            DependencyProperty.Register("DisplayedImage", typeof(BitmapImage), typeof(UIItem));

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private static void EnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UIImageButton button = sender as UIImageButton;
            if (button.EnabledButton)
            {
                button.DisplayedImage = button.EnabledImage;
            }
            else
            {
                button.DisplayedImage = button.DisabledImage;
            }
        }
    }
}
