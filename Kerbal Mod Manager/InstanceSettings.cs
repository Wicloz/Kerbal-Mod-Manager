using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kerbal_Mod_Manager
{
    public partial class InstanceSettings : Form
    {
        public Instance instance;

        public InstanceSettings()
        {
            InitializeComponent();
        }

        public void SetFields(string title, Instance instance)
        {
            this.instance = instance;
            Text = title;
            textBox1.Text = this.instance.name;
            textBox2.Text = this.instance.folder;
        }

        private void InstanceSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance.name = textBox1.Text;
            instance.folder = textBox2.Text;
        }
    }
}
