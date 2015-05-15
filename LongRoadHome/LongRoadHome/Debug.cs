using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uk.ac.dundee.arpond.longRoadHome.Controller;
using uk.ac.dundee.arpond.longRoadHome.View;

namespace uk.ac.dundee.arpond.longRoadHome
{
    public partial class Debug : Form
    {
        private MainController mc;

        public Debug()
        {
            InitializeComponent();
            mc = new MainController(0);
        }
    }
}
