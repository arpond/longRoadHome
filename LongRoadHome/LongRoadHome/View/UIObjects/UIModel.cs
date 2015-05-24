using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace uk.ac.dundee.arpond.longRoadHome.View.UIObjects
{
    public class UIModel : DependencyObject, INotifyPropertyChanged
    {
        public UIPlayer PlayerModel
        {
            get { return (UIPlayer)GetValue(UIModel.PlayerModelProperty); }
            set { SetValue(UIModel.PlayerModelProperty, value); }
        }

        public UISublocations SublocationModel
        {
            get { return (UISublocations)GetValue(UIModel.SublocationModelProperty); }
            set { SetValue(UIModel.SublocationModelProperty, value); }
        }

        public UIInventory UIInventory
        {
            get { return (UIInventory)GetValue(UIModel.UIInventoryProperty); }
            set { SetValue(UIModel.UIInventoryProperty, value); }
        }

        /// <summary>
        /// Identifies the Player Model Dependency Property
        /// </summary>
        public static readonly DependencyProperty PlayerModelProperty =
            DependencyProperty.Register("PlayerModel", typeof(UIPlayer), typeof(UIModel));

        /// <summary>
        /// Identifies the Sublocation Model Dependency Property
        /// </summary>
        public static readonly DependencyProperty SublocationModelProperty =
            DependencyProperty.Register("SublocationModel", typeof(UISublocations), typeof(UIModel));

        /// <summary>
        /// Identifies the Sublocation Model Dependency Property
        /// </summary>
        public static readonly DependencyProperty UIInventoryProperty =
            DependencyProperty.Register("UIInventory", typeof(UIInventory), typeof(UIModel));

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
