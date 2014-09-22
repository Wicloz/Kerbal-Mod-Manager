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

namespace KSP_Mod_Manager
{
    public partial class Main : Form
    {
        public static Main acces;

        public CurrentKspInstance kspInfo = new CurrentKspInstance();
        public CurrentModInstance modInfo = new CurrentModInstance();
        private InstallMod im = new InstallMod();
        private DeinstallMod dm = new DeinstallMod();
        private UpdateMod um = new UpdateMod();
        private UpdateCheck uc = new UpdateCheck();

        private string settingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KMM\\settings";
        private List<InstallInstance> instanceList = new List<InstallInstance>();
        private int selectedInstance = 0;

        public Main()
        {
            InitializeComponent();
            acces = this;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(settingFolder);
            string savedModsPath = "";

            if(File.Exists(settingFolder + "\\settings.txt"))
            {
                Settings settings = SaveLoad.LoadFileXml<Settings>(settingFolder + "\\settings.txt");

                instanceList = settings.instances;
                selectedInstance = settings.selectedInstance;
                savedModsPath = settings.modsPath;

                if (string.IsNullOrEmpty(savedModsPath))
                {
                    savedModsPath = "";
                }
            }

            ChangeModFolder(savedModsPath);
            kspInfo.LoadInstance(@"D:\KerbalSpaceProgram\Testing\KSP_win");

            SaveFiles();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFiles();
            modInfo.UnloadInstance();
            kspInfo.UnloadInstance();
        }

        private void SaveFiles()
        {
            SaveLoad.SaveFileXml(new Settings(modInfo.modsPath, instanceList, selectedInstance), settingFolder + "\\settings.txt");
        }

        private void ChangeModFolder(string newPath)
        {
            modFolderBox.Text = newPath;

            modInfo.UnloadInstance();
            modInfo.LoadInstance(newPath);

            // Handle mod list here
        }

        public void Reinstall(string installedModName, ModInfo info)
        {
            dm.Deinstall(installedModName);
            im.Install(info);
        }

        public void SortLists()
        {
            kspInfo.installedModList.Sort();
            modInfo.modList.Sort();
        }

        public void LogMessage(string message)
        {
        }

        private void selectModFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                ChangeModFolder(fbd.SelectedPath);
            }
        }
    }
}
