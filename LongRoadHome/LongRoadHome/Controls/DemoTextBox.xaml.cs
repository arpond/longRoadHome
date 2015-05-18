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
    /// Interaction logic for DemoTextBox.xaml
    /// </summary>
    public partial class DemoTextBox : UserControl, INotifyPropertyChanged
    {
        private String text;
        public event PropertyChangedEventHandler PropertyChanged;

        public DemoTextBox()
        {
            InitializeComponent();
        }

        public String DemoText
        {
            get { return text;  }
            set
            {
                text = DemoText;
                OnPropertyChanged("DemoText");
            }
        }

        public static readonly DependencyProperty DemoTextProperty =
            DependencyProperty.Register("DemoText", typeof(String), typeof(DemoTextBox), new UIPropertyMetadata(String.Empty));


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
