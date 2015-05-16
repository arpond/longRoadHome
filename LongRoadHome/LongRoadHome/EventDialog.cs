using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uk.ac.dundee.arpond.longRoadHome
{
    public partial class EventDialog : Form
    {
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
                Label label = new Label();
                label.Text = i + ". " + option;
                label.Location = new System.Drawing.Point(20, i*50);
                this.Controls.Add(label);
                optionSelectionBox.Items.Add(i);
                i++;
            }
            if(result)
            {
                optionSelectionBox.Hide();
            }
        }

        public int GetSelected()
        {
            return Convert.ToInt32(optionSelectionBox.SelectedItem);
        }


    }
}
