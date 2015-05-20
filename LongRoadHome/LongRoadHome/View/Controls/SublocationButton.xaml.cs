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

        public SublocationButton(UIImageButton _ImageButtonModel)
        {
            InitializeComponent();
            this.DataContext = _ImageButtonModel;
        }

        public event RoutedEventHandler Click;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }
    }
}
