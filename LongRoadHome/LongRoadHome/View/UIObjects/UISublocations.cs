using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace uk.ac.dundee.arpond.longRoadHome.View.UIObjects
{
    public class UISublocations : DependencyObject, INotifyPropertyChanged
    {
        public int CurrentSublocation
        {
            get { return (int)GetValue(UISublocations.CurrentSublocationProperty); }
            set { SetValue(UISublocations.CurrentSublocationProperty, value); }
        }

        public List<String> ImagePaths
        {
            get { return (List<String>)GetValue(UISublocations.ImagePathsProperty); }
            set { SetValue(UISublocations.ImagePathsProperty, value); }
        }

        public List<bool> Scavenged
        {
            get { return (List<bool>)GetValue(UISublocations.ScavengedProperty); }
            set { SetValue(UISublocations.ScavengedProperty, value); }
        }

        /// <summary>
        /// Identifies the Current Sublocation Dependency Property
        /// </summary>
        public static readonly DependencyProperty CurrentSublocationProperty =
            DependencyProperty.Register("CurrentSublocation", typeof(int), typeof(UISublocations),
            new UIPropertyMetadata(0));

        /// <summary>
        /// Identifies the Image Path Dependency Property
        /// </summary>
        public static readonly DependencyProperty ImagePathsProperty =
            DependencyProperty.Register("ImagePaths", typeof(List<String>), typeof(UISublocations));

        /// <summary>
        /// Identifies the Image Path Dependency Property
        /// </summary>
        public static readonly DependencyProperty ScavengedProperty =
            DependencyProperty.Register("Scavenged", typeof(List<bool>), typeof(UISublocations));

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
