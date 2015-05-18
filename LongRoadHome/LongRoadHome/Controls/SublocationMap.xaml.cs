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
using uk.ac.dundee.arpond.longRoadHome.Model.Location;

namespace LongRoadHome.Controls
{
    /// <summary>
    /// Interaction logic for SublocationMap.xaml
    /// </summary>
    public partial class SublocationMap : UserControl, INotifyPropertyChanged
    {
        public SublocationMap()
        {
            InitializeComponent();
        }

        private int _CurrentSublocation;
        private List<Sublocation> _Sublocations;

        public int CurrentSublocation
        {
            get { return _CurrentSublocation; }
            set 
            {
                _CurrentSublocation = value;
                RaisePropertyChanged("CurrentSublocation");
            }
        }

        public static readonly DependencyProperty CurrentSublocationProperty =
            DependencyProperty.Register("CurrentSublocation", typeof(int), typeof(BaseInterface),
            new UIPropertyMetadata(0));

        public List<Sublocation> Sublocations
        {
            get { return _Sublocations; }
            set
            {
                _Sublocations = value;
                RaisePropertyChanged("ublocation");
            }
        }

        public static readonly DependencyProperty SublocationsProperty =
            DependencyProperty.Register("Sublocations", typeof(int), typeof(BaseInterface));


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}
