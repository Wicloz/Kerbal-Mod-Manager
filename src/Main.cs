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
        public static Main acces;
        private string cd = Directory.GetCurrentDirectory();
        private Settings settings = new Settings();

        private KspFolder kspFolder = new KspFolder();
        private ModFolder modFolders = new ModFolder();
        private InstanceSettings editInstanceSettings = new InstanceSettings();

        public List<string> GetCategories()
        {
            return settings.categories;
        }

        // Initialising
        public Main()
        {
            InitializeComponent();
            acces = this;
            editInstanceSettings.FormClosing += new FormClosingEventHandler(EditInstanceForm_FormClosing);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (File.Exists(cd + "\\settings.cfg"))
            {
                settings = SaveLoad.LoadFileXml<Settings>(cd + "\\settings.cfg");
            }
            SaveSettings();
            ReloadFolderUi();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            SaveLoad.SaveFileXml(settings, cd + "\\settings.cfg");
        }

        // Mod folder selection
        private void buttonAddFolder_Click(object sender, EventArgs e)
        {
            Instance instance = new Instance();
            settings.modFolders.Add(instance);
            editInstanceSettings.SetFields("Add Mod Folder", instance);
            editInstanceSettings.ShowDialog();
        }

        private void buttonDeleteFolder_Click(object sender, EventArgs e)
        {
            if (settings.selectedMods.Count > 0)
            {
                int modifier = 0;
                List<int> selectedMods = new List<int>(settings.selectedMods);
                foreach (int i in selectedMods)
                {
                    settings.modFolders.RemoveAt(i - modifier);
                    settings.selectedMods.Remove(i);
                    modifier++;
                }
                modFolders.ChangeFolders(new List<string>());
                ReloadFolderUi();
            }
        }

        private void listBoxModFolders_DoubleClick(object sender, EventArgs e)
        {
            if (settings.selectedMods.Count == 1)
            {
                Instance instance = settings.modFolders[settings.selectedMods[0]];
                editInstanceSettings.SetFields("Edit Mod Folder", instance);
                editInstanceSettings.ShowDialog();
            }
        }

        private void listBoxModFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxModFolders.SelectedIndices.Count > 0)
            {
                settings.selectedMods = new List<int>();
                List<string> selectedFolders = new List<string>();

                foreach (int i in listBoxModFolders.SelectedIndices)
                {
                    settings.selectedMods.Add(i);
                    selectedFolders.Add(settings.modFolders[i].folder);
                }
                modFolders.ChangeFolders(selectedFolders);
                settings.selectedMods.Sort();
            }
        }

        private void buttonSelectAllFolders_Click(object sender, EventArgs e)
        {
            settings.selectedMods.Clear();
            for (int i = 0; i < listBoxModFolders.Items.Count; i++)
            {
                settings.selectedMods.Add(i);
            }
            ReloadFolderUi();
        }

        // KSP folder selection
        private void buttonAddKsp_Click(object sender, EventArgs e)
        {
            Instance instance = new Instance();
            settings.kspFolders.Add(instance);
            editInstanceSettings.SetFields("Add KSP Instance", instance);
            editInstanceSettings.ShowDialog();
        }

        private void buttonDeleteKsp_Click(object sender, EventArgs e)
        {
            if (settings.selectedKsp >= 0)
            {
                settings.kspFolders.RemoveAt(settings.selectedKsp);
                settings.selectedKsp--;
                ReloadFolderUi();
            }
        }

        private void listBoxKspFolders_DoubleClick(object sender, EventArgs e)
        {
            if (settings.selectedKsp >= 0)
            {
                Instance instance = settings.kspFolders[settings.selectedKsp];
                editInstanceSettings.SetFields("Edit KSP Instance", instance);
                editInstanceSettings.ShowDialog();
            }
        }

        private void listBoxKspFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxKspFolders.SelectedIndices.Count > 0)
            {
                settings.selectedKsp = listBoxKspFolders.SelectedIndices[0];
            }
        }

        // Selection UI
        void EditInstanceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (settings.selectedKsp < 0)
            {
                settings.selectedKsp = 0;
            }
            ReloadFolderUi();
        }

        private void ReloadFolderUi()
        {
            listBoxModFolders.Items.Clear();
            listBoxKspFolders.Items.Clear();

            foreach (Instance instance in settings.modFolders)
            {
                listBoxModFolders.Items.Add(instance.name);
            }
            foreach (Instance instance in settings.kspFolders)
            {
                listBoxKspFolders.Items.Add(instance.name);
            }

            foreach (int i in settings.selectedMods)
            {
                listBoxModFolders.SelectedIndices.Add(i);
            }
            if (settings.selectedKsp >= 0)
            {
                listBoxKspFolders.SelectedIndices.Add(settings.selectedKsp);
            }
        }
    }

    [Serializable]
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

    [Serializable]
    public class Settings
    {
        public List<Instance> kspFolders = new List<Instance>();
        public List<Instance> modFolders = new List<Instance>();
        public List<string> categories = new List<string>();

        public List<int> selectedMods = new List<int>();
        public int selectedKsp = 0;

        public Settings()
        { }
    }
}
