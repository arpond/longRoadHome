using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace uk.ac.dundee.arpond.longRoadHome.View.UIObjects
{
    public class UIPlayer : DependencyObject, INotifyPropertyChanged
    {
        public int Health
        {
            get { return (int)GetValue(UIPlayer.HealthProperty); }
            set { SetValue(UIPlayer.HealthProperty, value); }
        }

        public int Hunger
        {
            get { return (int)GetValue(UIPlayer.HungerProperty); }
            set { SetValue(UIPlayer.HungerProperty, value); }
        }

        public int Thirst
        {
            get { return (int)GetValue(UIPlayer.ThirstProperty); }
            set { SetValue(UIPlayer.ThirstProperty, value); }
        }

        public int Sanity
        {
            get { return (int)GetValue(UIPlayer.SanityProperty); }
            set { SetValue(UIPlayer.SanityProperty, value); }
        }

        /// <summary>
        /// Identifies the Health Dependency Property
        /// </summary>
        public static readonly DependencyProperty HealthProperty =
            DependencyProperty.Register("Health", typeof(int), typeof(UIPlayer),
            new UIPropertyMetadata(0));

        /// <summary>
        /// Identifies the Hunger Dependency Property
        /// </summary>
        public static readonly DependencyProperty HungerProperty =
            DependencyProperty.Register("Hunger", typeof(int), typeof(UIPlayer),
            new UIPropertyMetadata(0));

        /// <summary>
        /// Identifies the Thirst Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThirstProperty =
            DependencyProperty.Register("Thirst", typeof(int), typeof(UIPlayer),
            new UIPropertyMetadata(0));
        
        /// <summary>
        /// Identifies the Sanity Dependency Property
        /// </summary>
        public static readonly DependencyProperty SanityProperty =
            DependencyProperty.Register("Sanity", typeof(int), typeof(UIPlayer),
            new UIPropertyMetadata(0));

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
