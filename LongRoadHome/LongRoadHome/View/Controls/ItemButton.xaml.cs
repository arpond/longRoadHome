using System;
using System.Collections.Generic;
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

namespace uk.ac.dundee.arpond.longRoadHome.View.Controls
{
    /// <summary>
    /// Interaction logic for ItemButton.xaml
    /// </summary>
    public partial class ItemButton : UserControl
    {
        public ItemButton()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler DiscardClick;
        public event RoutedEventHandler UseClick;

        private void DiscardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DiscardClick != null)
            {
                this.DiscardClick(this, e);
            }
        }

        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.UseClick != null)
            {
                this.UseClick(this, e);
            }
        }

        public String Description
        {
            get { return (String)GetValue(ItemButton.DesscriptionProperty); }
            set { SetValue(ItemButton.DesscriptionProperty, value); }
        }

        public int ItemSlot
        {
            get { return (int)GetValue(ItemButton.ItemSlotProperty); }
            set { SetValue(ItemButton.ItemSlotProperty, value); }
        }

        public bool Usable
        {
            get { return (bool)GetValue(ItemButton.UsableProperty); }
            set { SetValue(ItemButton.UsableProperty, value); }
        }

        public int Amount
        {
            get { return (int)GetValue(ItemButton.AmountProperty); }
            set { SetValue(ItemButton.AmountProperty, value); }
        }

        public BitmapImage ItemIcon
        {
            get { return (BitmapImage)GetValue(ItemButton.ItemIconProperty); }
            set { SetValue(ItemButton.ItemIconProperty, value); }
        }

        /// <summary>
        /// Identifies the Description Dependency Property
        /// </summary>
        public static readonly DependencyProperty DesscriptionProperty =
            DependencyProperty.Register("Description", typeof(String), typeof(ItemButton),
             new PropertyMetadata(OnEnabledChanged));

        /// <summary>
        /// Identifies the Item Slot Dependency Property
        /// </summary>
        public static readonly DependencyProperty ItemSlotProperty =
            DependencyProperty.Register("ItemSlot", typeof(int), typeof(ItemButton));

        /// <summary>
        /// Identifies the Usable Dependency Property
        /// </summary>
        public static readonly DependencyProperty UsableProperty =
            DependencyProperty.Register("Usable", typeof(bool), typeof(ItemButton));

        /// <summary>
        /// Identifies the Amount Dependency Property
        /// </summary>
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(int), typeof(ItemButton));

        /// <summary>
        /// Identifies the Item Icon Dependency Property
        /// </summary>
        public static readonly DependencyProperty ItemIconProperty =
            DependencyProperty.Register("ItemIcon", typeof(BitmapImage), typeof(ItemButton));

        private static void OnEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
