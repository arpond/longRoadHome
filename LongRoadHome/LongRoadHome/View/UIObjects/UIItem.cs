using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace uk.ac.dundee.arpond.longRoadHome.View.UIObjects
{
    public class UIItem : DependencyObject, INotifyPropertyChanged
    {
        public int ID
        {
            get { return (int)GetValue(UIItem.IDProperty); }
            set { SetValue(UIItem.IDProperty, value); }
        }

        public String Description
        {
            get { return (String)GetValue(UIItem.DescriptionProperty); }
            set { SetValue(UIItem.DescriptionProperty, value); }
        }

        public String IconPath
        {
            get { return (String)GetValue(UIItem.IconPathProperty); }
            set { SetValue(UIItem.IconPathProperty, value); }
        }


        /// <summary>
        /// Identifies the Inventory Dependency Property
        /// </summary>
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(UIItem),
            new UIPropertyMetadata(0));

        /// <summary>
        /// Identifies the Inventory Dependency Property
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(UIItem),
            new UIPropertyMetadata(String.Empty));

        /// <summary>
        /// Identifies the Inventory Dependency Property
        /// </summary>
        public static readonly DependencyProperty IconPathProperty =
            DependencyProperty.Register("IconPath", typeof(string), typeof(UIItem),
            new UIPropertyMetadata(String.Empty));

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
