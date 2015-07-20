using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Kerbal_Mod_Manager
{
    public partial class Main : Form
    {
        private string cd = Directory.GetCurrentDirectory();
        private Settings settings = new Settings();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (File.Exists(cd + "\\settings.cfg"))
            {
                settings = SaveLoad.LoadFileXml<Settings>(cd + "\\settings.cfg");
            }
            SaveSettings();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void SaveSettings()
        {
            SaveLoad.SaveFileXml(settings, cd + "\\settings.cfg");
        }
    }

    [System.Serializable]
    public class Instance
    {
        public string name = "";
        public string folder = "";

        public Instance()
        { }

        public Instance(string name, string folder)
        {
            this.name = name;
            this.folder = folder;
        }
    }

    [System.Serializable]
    public class Settings
    {
        List<Instance> kspFolders = new List<Instance>();
        List<Instance> modFolders = new List<Instance>();

        public Settings()
        { }
    }
}
