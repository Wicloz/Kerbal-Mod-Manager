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
using System.Diagnostics;

namespace KSP_Mod_Manager
{
    public partial class Main : Form
    {
        public static Main acces;

        public CurrentKspInstance kspInfo = new CurrentKspInstance();
        public CurrentModInstance modInfo = new CurrentModInstance();
        private UpdateMod um = new UpdateMod();
        private UpdateCheck uc = new UpdateCheck();

        private string settingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KMM\\settings";
        public List<InstallInstance> instanceList = new List<InstallInstance>();

        private ModInfo selectedMod = new ModInfo();
        private InstalledInfo selectedInstalledMod = new InstalledInfo();
        private FavInfo selectedFav = new FavInfo();

        private string log = "Welcome to KMM!";

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

            if (File.Exists(settingFolder + "\\settings.txt"))
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
            modBox.Enabled = kspInfo.LoadInstance(newPath);
        }

        // UI Interaction
        public void SortLists()
        {
            kspInfo.installedModList.Sort();
            modInfo.modList.Sort();
        }

        public void LogMessage(string message)
        {
            log += "\n\n";
            log += message;
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
                modBox.Items.Add("INSTALLED MODS");
            }

            foreach (InstalledInfo installedMod in kspInfo.installedModList)
            {
                if (!installedMod.codeName.Contains("Overrides\\"))
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
            }

            if (modInfo.modList.Count > 0)
            {
                modBox.Items.Add("DOWNLOADED MODS");
            }

            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\") && !Functions.IsModInstalled(mod))
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
                for (int i = 0; i < modBox.Items.Count; i++)
                {
                    string selectedmodname = (string)modBox.Items[i];

                    if (selectedmodname.Replace(" - Update Available", "") == modName)
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

            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.canUpdate)
                {
                    Directory.CreateDirectory(modInfo.modsPath + "\\ModDownloads\\" + mod.name.Replace(" ", "_"));
                }
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
            List<ModInfo> sendList = new List<ModInfo>();

            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\") && mod.websites.website != "NONE")
                {
                    sendList.Add(mod);
                }
            }

            if (sendList.Count > 0)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(sendList, 1);
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
        }

        private void downloadModButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();

            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.canUpdate && !mod.zipfile.Contains("Overrides\\") && mod.websites.dlSite != "NONE")
                {
                    sendList.Add(mod);
                }
            }

            if (sendList.Count > 0)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(sendList, 2);
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
        }

        private void openLogButton_Click(object sender, EventArgs e)
        {
            LogForm form = new LogForm(log);
            form.Show();
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
            LoadKspInstance();
        }

        private void modBox_DoubleClick(object sender, EventArgs e)
        {
            InstallDeinstallSelected();
        }

        private void opInstallButton_Click(object sender, EventArgs e)
        {
            InstallDeinstallSelected();
        }

        private void deleteMod_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < modInfo.modList.Count; i++)
            {
                if (modInfo.modList[i].key == selectedMod.key)
                {
                    modInfo.modList.RemoveAt(i);
                    break;
                }
            }

            UpdateModList("");
        }

        private void topButton1_Click(object sender, EventArgs e)
        {
            List<InstalledInfo> sendList = new List<InstalledInfo>();

            for (int i = 0; i < kspInfo.installedModList.Count; i++)
            {
                if (!kspInfo.installedModList[i].codeName.Contains("Overrides\\"))
                {
                    sendList.Add(kspInfo.installedModList[i]);
                }
            }

            if (sendList.Count > 0)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(sendList);
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
        }

        private void topButton2_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();

            foreach (FavInfo fav in kspInfo.favoritesList)
            {
                if (fav.isFav)
                {
                    foreach (ModInfo mod in modInfo.modList)
                    {
                        if (fav.key == mod.key)
                        {
                            if (!Functions.IsModInstalled(mod))
                            {
                                sendList.Add(mod);
                            }
                            break;
                        }
                    }
                }
            }

            if (sendList.Count > 0)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(sendList, 0);
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
        }

        // Functions for the buttons
        private void InstallDeinstallSelected()
        {
            bool isInstalled = Functions.IsModInstalled(selectedMod);

            if (!isInstalled)
            {
                List<ModInfo> sendList = new List<ModInfo>();
                sendList.Add(selectedMod);

                InstallDeinstallForm form = new InstallDeinstallForm(sendList, 0);
                form.ShowDialog();
            }
            else if (isInstalled)
            {
                List<InstalledInfo> sendList = new List<InstalledInfo>();
                sendList.Add(selectedInstalledMod);

                InstallDeinstallForm form = new InstallDeinstallForm(sendList);
                form.ShowDialog();
            }

            UpdateModList(selectedMod.name);
        }

        // ModInfo editing stuff
        private bool isChangingSelection = true;
        private void modBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            isChangingSelection = false;
            bool modExists = false;

            for (int i = 0; i < modInfo.modList.Count; i++)
            {
                string itemName = (string)modBox.SelectedItem;

                if (modInfo.modList[i].name == itemName.Replace(" - Update Available", ""))
                {
                    selectedMod = modInfo.modList[i];
                    modExists = true;
                    break;
                }
            }

            selectedInstalledMod = Functions.GetInstalledMod(selectedMod);

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

            if (modExists && selectedMod.websites.dlSite == "NONE")
            {
                opDownloadButton.Text = "Open Site";
            }
            else
            {
                opDownloadButton.Text = "Update Mod";
            }

            isChangingSelection = true;
        }

        private void BlockSettingEditor()
        {
            opNameBox.Enabled = false;
            opCategoryBox.Enabled = false;
            opSiteBox.Enabled = false;
            opDlSiteBox.Enabled = false;

            opOpenSiteButton.Enabled = false;

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

            opOpenSiteButton.Enabled = true;

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

        private void opOpenSite_Click(object sender, EventArgs e)
        {
            Process.Start(selectedMod.websites.website);
        }

        private void modBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                selectedFav.isFav = !selectedFav.isFav;
                UpdateModList(selectedMod.name);
            }
        }

        private void opCheckUpdateButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();

            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.zipfile == selectedMod.zipfile && mod.websites.website != "NONE")
                {
                    sendList.Add(mod);
                }
            }

            if (sendList.Count > 0)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(sendList, 1);
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
        }

        private void opDownloadButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();

            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.zipfile == selectedMod.zipfile && mod.websites.dlSite != "NONE")
                {
                    sendList.Add(mod);
                }
            }

            if (sendList.Count > 0)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(sendList, 2);
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
            else
            {
                Process.Start(selectedMod.websites.website);
            }
        }
    }
}
