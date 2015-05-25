using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace uk.ac.dundee.arpond.longRoadHome.View.UIObjects
{
    public class UIInventory : DependencyObject, INotifyPropertyChanged
    {
        public List<UIItem> Inventory
        {
            get { return (List<UIItem>)GetValue(UIInventory.InventoryProperty); }
            set { SetValue(UIInventory.InventoryProperty, value); }
        }

        /// <summary>
        /// Identifies the Inventory Dependency Property
        /// </summary>
        public static readonly DependencyProperty InventoryProperty =
            DependencyProperty.Register("Inventory", typeof(List<UIItem>), typeof(UIInventory));

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
