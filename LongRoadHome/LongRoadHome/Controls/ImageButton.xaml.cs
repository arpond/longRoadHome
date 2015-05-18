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

namespace LongRoadHome.Controls
{
    /// <summary>
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl, INotifyPropertyChanged
    {
        public ImageButton()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Click;
        private bool _Enabled;
        private ImageSource _EnabledImage;
        private ImageSource _DisabledImage;

        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;
                RaisePropertyChanged("Enabled");
            }
        }

        public ImageSource EnabledImage
        {
            get { return _EnabledImage; }
            set
            {
                if (_EnabledImage == value) return;
                _EnabledImage = value;
                RaisePropertyChanged("EnabledImage");
            }
        }

        public ImageSource DisabledImage
        {
            get { return _DisabledImage; }
            set
            {
                if (_DisabledImage == value) return;
                _DisabledImage = value;
                RaisePropertyChanged("DisabledImage");
            }
        }

        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.Register("Enabled", typeof(bool), typeof(ImageButton), new UIPropertyMetadata(true));

        public static readonly DependencyProperty ClickEventProperty =
            DependencyProperty.Register("ClickEvent", typeof(RoutedEventHandler), typeof(ImageButton));

        public static readonly DependencyProperty EnabledImageProperty =
            DependencyProperty.Register("EnabledImage", typeof(ImageSource), typeof(ImageButton));

        public static readonly DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(ImageSource), typeof(ImageButton));

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
                if (info == "Enabled" && Enabled == false)
                {
                    image.Source = DisabledImage;
                }
                else if (info == "Enabled")
                {
                    image.Source = EnabledImage;
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }
    }
}
