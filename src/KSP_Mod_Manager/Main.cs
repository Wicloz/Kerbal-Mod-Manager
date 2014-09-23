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
        public List<InstallInstance> instanceList = new List<InstallInstance>();

        ModInfo selectedMod = new ModInfo();
        InstallInfo selectedInstalledMod = new InstallInfo();
        FavInfo selectedFav = new FavInfo();

        public Main()
        {
            InitializeComponent();
            acces = this;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(settingFolder);

            string savedModsPath = "";
            int selectedIndex = 0;

            if(File.Exists(settingFolder + "\\settings.txt"))
            {
                Settings settings = SaveLoad.LoadFileXml<Settings>(settingFolder + "\\settings.txt");

                instanceList = settings.instances;
                selectedIndex = settings.selectedInstance;
                savedModsPath = settings.modsPath;

                if (string.IsNullOrEmpty(savedModsPath))
                {
                    savedModsPath = "";
                }
            }

            ChangeModFolder(savedModsPath);
            ChangeKspFolder(instanceList[selectedIndex].kspPath);

            UpdateInstallInstanceList(selectedIndex);
            UpdateModList("");

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
            SaveLoad.SaveFileXml(new Settings(modInfo.modsPath, instanceList, installationBox.SelectedIndex), settingFolder + "\\settings.txt");
        }

        private void ChangeModFolder(string newPath)
        {
            modFolderBox.Text = newPath;

            modInfo.UnloadInstance();
            modInfo.LoadInstance(newPath);
        }

        private void ChangeKspFolder(string newPath)
        {
            kspInfo.UnloadInstance();
            kspInfo.LoadInstance(newPath);
        }

        public void Reinstall(string installedModName, ModInfo info)
        {
            dm.Deinstall(installedModName);
            im.Install(info);
        }

        public bool IsModInstalled(string version)
        {
            foreach (InstallInfo installedMod in kspInfo.installedModList)
            {
                if (installedMod.version == version)
                {
                    return true;
                }
            }

            return false;
        }

        // UI Interaction
        public void SortLists()
        {
            kspInfo.installedModList.Sort();
            modInfo.modList.Sort();
        }

        public void LogMessage(string message)
        {
        }

        private void UpdateInstallInstanceList(int index)
        {
            installationBox.Items.Clear();

            foreach (InstallInstance install in instanceList)
            {
                installationBox.Items.Add(install.name);
            }

            installationBox.SelectedIndex = index;
        }

        private void UpdateModList(string modName)
        {
            modBox.Items.Clear();
            SortLists();

            if (kspInfo.installedModList.Count > 0)
            {
                modBox.Items.Add("Installed Mods");
            }

            foreach (InstallInfo installedMod in kspInfo.installedModList)
            {
                ModInfo mod = new ModInfo("none");
                string addedString = "";

                for (int i = 0; i < modInfo.modList.Count; i++)
                {
                    if (modInfo.modList[i].name == installedMod.modName)
                    {
                        mod = modInfo.modList[i];
                        break;
                    }
                }

                if (mod.canUpdate)
                {
                    addedString = " - Update Available";
                }

                modBox.Items.Add(installedMod.modName + addedString);
            }

            if (modInfo.modList.Count > 0)
            {
                modBox.Items.Add("Downloaded Mods");
            }

            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides") && !IsModInstalled(mod.version))
                {
                    string addedString = "";

                    if (mod.canUpdate)
                    {
                        addedString = " - Update Available";
                    }

                    modBox.Items.Add(mod.name + addedString);
                }
            }

            if (modName != "")
            {
                for (int i = 0; i < modInfo.modList.Count; i++)
                {
                    if (modInfo.modList[i].name == modName)
                    {
                        modBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                modBox.SelectedIndex = 0;
            }
        }

        // Buttons and stuff
        private void selectModFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                ChangeModFolder(fbd.SelectedPath);
            }
        }

        private void updateModFolderButton_Click(object sender, EventArgs e)
        {
            ChangeModFolder(modFolderBox.Text);
        }

        private void checkUpdateButton_Click(object sender, EventArgs e)
        {

        }

        private void addInstanceButton_Click(object sender, EventArgs e)
        {
            instanceList.Add(new InstallInstance("New Instance"));
            UpdateInstallInstanceList(installationBox.SelectedIndex);
        }

        private void removeInstanceButton_Click(object sender, EventArgs e)
        {
            instanceList.RemoveAt(installationBox.SelectedIndex);
            UpdateInstallInstanceList(installationBox.SelectedIndex - 1);

            LoadKspInstance();
        }

        private void installationBox_Click(object sender, EventArgs e)
        {
            LoadKspInstance();
        }

        private void LoadKspInstance()
        {
            InstallInstance instance = instanceList[installationBox.SelectedIndex];

            ChangeKspFolder(instance.kspPath);
            UpdateModList("");
        }

        EditInstanceSettings editInstanceForm;
        private void installationBox_DoubleClick(object sender, EventArgs e)
        {
            InstallInstance instance = instanceList[installationBox.SelectedIndex];

            editInstanceForm = new EditInstanceSettings(instance.kspPath, instance.name);
            editInstanceForm.FormClosing += new FormClosingEventHandler(EditInstanceForm_FormClosing);

            editInstanceForm.ShowDialog();
        }

        void EditInstanceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            InstallInstance instance = instanceList[installationBox.SelectedIndex];

            instance.name = editInstanceForm.name;
            instance.kspPath = editInstanceForm.kspPath;

            UpdateInstallInstanceList(installationBox.SelectedIndex);
        }

        private void modBox_DoubleClick(object sender, EventArgs e)
        {
            InstallDeinstallSelected();
        }

        private void opInstallButton_Click(object sender, EventArgs e)
        {
            InstallDeinstallSelected();
        }

        private void topButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < kspInfo.installedModList.Count; i++)
            {
                dm.Deinstall(kspInfo.installedModList[i].codeName);
                i--;
            }

            UpdateModList(selectedMod.name);
        }

        // Functions for the buttons
        private void InstallDeinstallSelected()
        {
            bool isInstalled = IsModInstalled(selectedMod.version);

            if (!isInstalled)
            {
                im.Install(selectedMod);
            }
            else if (isInstalled)
            {
                dm.Deinstall(selectedInstalledMod.codeName);
            }
            
            UpdateModList(selectedMod.name);
        }

        // ModInfo editing stuff
        private bool isChangingSelection = true;
        private void modBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            isChangingSelection = false;
            bool modExists = false;

            for (int i = 0; i < kspInfo.installedModList.Count; i++)
            {
                string itemName = (string)modBox.SelectedItem;

                if (kspInfo.installedModList[i].modName == itemName.Replace(" - Update Available", ""))
                {
                    selectedInstalledMod = kspInfo.installedModList[i];
                    break;
                }
            }

            for (int i = 0; i < modInfo.modList.Count; i++)
            {
                string itemName = (string) modBox.SelectedItem;

                if (modInfo.modList[i].name == itemName.Replace(" - Update Available", ""))
                {
                    selectedMod = modInfo.modList[i];
                    modExists = true;
                    break;
                }
            }

            for (int i = 0; i < kspInfo.favoritesList.Count; i++)
            {
                if (selectedMod.key == kspInfo.favoritesList[i].key)
                {
                    selectedFav = kspInfo.favoritesList[i];
                    break;
                }
            }

            if (modExists)
            {
                opNameBox.Text = selectedMod.name;
                opCategoryBox.Text = selectedMod.category;
                opSiteBox.Text = selectedMod.websites.website;
                opDlSiteBox.Text = selectedMod.websites.dlSite;

                opIsFavoriteBox.Checked = selectedFav.isFav;
                opCanDownloadBox.Checked = selectedMod.canUpdate;

                UnlockSettingEditor();
            }
            else
            {
                opNameBox.Text = "";
                opCategoryBox.Text = "";
                opSiteBox.Text = "";
                opDlSiteBox.Text = "";

                opIsFavoriteBox.Checked = false;
                opCanDownloadBox.Checked = false;

                BlockSettingEditor();
            }

            isChangingSelection = true;
        }

        private void BlockSettingEditor()
        {
            opNameBox.Enabled = false;
            opCategoryBox.Enabled = false;
            opSiteBox.Enabled = false;
            opDlSiteBox.Enabled = false;

            opIsFavoriteBox.Enabled = false;
            opCanDownloadBox.Enabled = false;

            opInstallButton.Enabled = false;
            opCheckUpdateButton.Enabled = false;
            opDownloadButton.Enabled = false;

            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
        }

        private void UnlockSettingEditor()
        {
            opNameBox.Enabled = true;
            opCategoryBox.Enabled = true;
            opSiteBox.Enabled = true;
            opDlSiteBox.Enabled = true;

            opIsFavoriteBox.Enabled = true;
            opCanDownloadBox.Enabled = true;

            opInstallButton.Enabled = true;
            opCheckUpdateButton.Enabled = true;
            opDownloadButton.Enabled = true;

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
        }

        private void opNameBox_TextChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.name = opNameBox.Text;
                UpdateModList(selectedMod.name);
            }
        }

        private void opCategoryBox_TextChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.category = opCategoryBox.Text;
                UpdateModList(selectedMod.name);
            }
        }

        private void opSiteBox_TextChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.websites.website = opSiteBox.Text;
                UpdateModList(selectedMod.name);
            }
        }

        private void opDlSiteBox_TextChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.websites.dlSite = opDlSiteBox.Text;
                UpdateModList(selectedMod.name);
            }
        }

        private void opIsFavoriteBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedFav.isFav = opIsFavoriteBox.Checked;
                UpdateModList(selectedMod.name);
            }
        }

        private void opCanDownloadBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.canUpdate = opCanDownloadBox.Checked;
                UpdateModList(selectedMod.name);
            }
        }
    }
}
