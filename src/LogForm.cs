using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSP_Mod_Manager
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            textBox1.Text = Main.acces.GetLog();
        }
    }
}
