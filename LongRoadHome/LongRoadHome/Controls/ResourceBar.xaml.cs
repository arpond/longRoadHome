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
    /// Interaction logic for ResourceBar.xaml
    /// </summary>
    public partial class ResourceBar : UserControl, INotifyPropertyChanged
    {
        public ResourceBar()
        {
            InitializeComponent();
        }
        private int _Resource;
        private ImageSource _ImgSource;

        public int Resource
        {
            get { return _Resource; }
            set
            {
                if (_Resource == value) return;
                _Resource = value;
                OnPropertyChanged("Resource");
            }
        }

        public ImageSource ImgSource
        {
            get { return _ImgSource; }
            set
            {
                if (_ImgSource == value) return;
                _ImgSource = value;
                OnPropertyChanged("ImgSource");
            }
        }

        public static readonly DependencyProperty ResourceProperty =
            DependencyProperty.Register("Resource", typeof(int), typeof(ResourceBar),
            new UIPropertyMetadata(0));

        public static readonly DependencyProperty ImgSourceProperty =
            DependencyProperty.Register("ImgSource", typeof(ImageSource), typeof(ResourceBar));        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
