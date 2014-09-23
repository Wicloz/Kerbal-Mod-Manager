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
    public partial class EditInstanceSettings : Form
    {
        public string name = "";
        public string kspPath = "";

        public EditInstanceSettings(string KspPath, string Name)
        {
            InitializeComponent();

            textBox1.Text = Name;
            textBox2.Text = KspPath;
        }

        private void EditInstanceSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            name = textBox1.Text;
            kspPath = textBox2.Text;
        }
    }
}
