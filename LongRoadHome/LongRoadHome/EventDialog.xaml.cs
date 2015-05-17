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
using System.Windows.Shapes;

namespace LongRoadHome
{
    /// <summary>
    /// Interaction logic for EventDialog.xaml
    /// </summary>
    public partial class EventDialog : Window
    {
        List<Button> buttons = new List<Button>();
        int selected;

        public EventDialog()
        {
            InitializeComponent();
        }

        public EventDialog(String text, List<String> options, bool result)
        {
            InitializeComponent();
            eventText.Text = text;
            int i = 1;
            foreach (String option in options)
            {
                TextBlock tb = new TextBlock();
                tb.Text = i + ". " + option;
                stackPanel.Children.Add(tb);
                
                if (!result)
                {
                    Button button = new Button();
                    button.Content = "Option " + i;
                    button.Click += new RoutedEventHandler(OnButtonClick);
                    buttons.Add(button);
                    stackPanel.Children.Add(button);
                }
                i++;
            }
        }

        void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            selected = buttons.IndexOf(btn);
            this.Close();
        }

        public int GetSelected()
        {
            return selected + 1;
        }
    }
}
