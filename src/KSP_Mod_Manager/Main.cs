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

                if (settings.categoryList.Count > 0)
                {
                    opCategoryBox.Items.Clear();
                    foreach (string category in settings.categoryList)
                    {
                        opCategoryBox.Items.Add(category);
                    }
                }
            }

            else
            {
                instanceList = new List<InstallInstance>();
                instanceList.Add(new InstallInstance("New Instance"));
            }

            ChangeModFolder(savedModsPath);
            ChangeKspFolder(instanceList[selectedIndex].kspPath);

            UpdateInstallInstanceList(selectedIndex);

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
            List<string> sendList = new List<string>();

            foreach (string category in opCategoryBox.Items)
            {
                sendList.Add(category);
            }

            SaveLoad.SaveFileXml(new Settings(modInfo.modsPath, instanceList, installationBox.SelectedIndex, sendList), settingFolder + "\\settings.txt");
        }

        // Updating Mod and KSP folder
        private void LoadSelectedKspInstance()
        {
            ChangeKspFolder(instanceList[installationBox.SelectedIndex].kspPath);
        }

        private void ChangeModFolder(string newPath)
        {
            modFolderBox.Text = newPath;

            modInfo.UnloadInstance();
            modInfo.LoadInstance(newPath);

            UpdateModList("");
        }

        private void ChangeKspFolder(string newPath)
        {
            kspInfo.UnloadInstance();
            bool hasLoaded = kspInfo.LoadInstance(newPath);
            downloadedModBox.Enabled = hasLoaded;

            if (hasLoaded)
            {
                UnlockSettingEditor();
            }
            else
            {
                BlockSettingEditor();
            }

            UpdateModList("");
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
            downloadedModBox.Items.Clear();
            installedModBox.Items.Clear();
            SortLists();
            string lastCat = "";

            if (kspInfo.installedModList.Count > 0)
            {
                installedModBox.Items.Add("INSTALLED MODS:");
                lastCat = "";
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

                    if (installedMod.category.ToUpper() != lastCat)
                    {
                        lastCat = installedMod.category.ToUpper();
                        installedModBox.Items.Add("-- " + lastCat + " --");
                    }

                    if (mod.canUpdate)
                    {
                        addedString = " - Update Available";
                    }

                    installedModBox.Items.Add(installedMod.modName + addedString);
                }
            }

            if (modInfo.modList.Count > 0)
            {
                downloadedModBox.Items.Add("DOWNLOADED MODS:");
                lastCat = "";
            }

            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\") && !Functions.IsModInstalled(mod) && !String.IsNullOrEmpty(mod.zipfile))
                {
                    string addedString = "";

                    if (mod.category.ToUpper() != lastCat)
                    {
                        lastCat = mod.category.ToUpper();
                        downloadedModBox.Items.Add("-- " + lastCat + " --");
                    }

                    if (mod.canUpdate)
                    {
                        addedString = " - Update Available";
                    }

                    downloadedModBox.Items.Add(mod.name + addedString);
                }
            }

            if (modInfo.modList.Count > 0)
            {
                downloadedModBox.SelectedIndex = 2;
            }

            if (modName != "")
            {
                for (int i = 0; i < downloadedModBox.Items.Count; i++)
                {
                    string selectedmodname = (string)downloadedModBox.Items[i];

                    if (selectedmodname.Replace(" - Update Available", "") == modName)
                    {
                        downloadedModBox.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (kspInfo.installedModList.Count > 0)
            {
                installedModBox.SelectedIndex = 2;
            }

            if (modName != "")
            {
                for (int i = 0; i < installedModBox.Items.Count; i++)
                {
                    string selectedmodname = (string)installedModBox.Items[i];

                    if (selectedmodname.Replace(" - Update Available", "") == modName)
                    {
                        installedModBox.SelectedIndex = i;
                        break;
                    }
                }
            }

            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.canUpdate)
                {
                    Directory.CreateDirectory(modInfo.modsPath + "\\ModDownloads\\" + mod.name.Replace(" ", "_"));
                }
            }
        }

        // Misc UI functions
        public void SortLists()
        {
            kspInfo.installedModList.Sort();
            modInfo.modList.Sort();
        }

        public void LogMessage(string message)
        {
            log += "\r\n";
            log += message;

            File.WriteAllText(Environment.CurrentDirectory + "\\KMM.log", log);
        }

        public string GetLog()
        {
            return log;
        }

        private void InstallDeinstallSelectedModbox()
        {
            bool isInstalled = Functions.IsModInstalled(selectedMod);

            if (!isInstalled)
            {
                List<ModInfo> sendList = new List<ModInfo>();
                sendList.Add(selectedMod);

                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), new List<ModInfo>(), new List<InstalledInfo>(), sendList);
                form.ShowDialog();
            }
            else if (isInstalled)
            {
                List<InstalledInfo> sendList = new List<InstalledInfo>();
                sendList.Add(selectedInstalledMod);

                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), new List<ModInfo>(), sendList, new List<ModInfo>());
                form.ShowDialog();
            }

            UpdateModList(selectedMod.name);
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
                InstallDeinstallForm form = new InstallDeinstallForm(sendList, new List<ModInfo>(), new List<InstalledInfo>(), new List<ModInfo>());
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
        }

        private void downloadModButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();

            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.canUpdate && !mod.zipfile.Contains("Overrides\\"))
                {
                    sendList.Add(mod);
                }
            }

            if (sendList.Count > 0)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), sendList, new List<InstalledInfo>(), new List<ModInfo>());
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
        }

        private void openLogButton_Click(object sender, EventArgs e)
        {
            LogForm form = new LogForm();
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

            LoadSelectedKspInstance();
        }

        private void installationBox_Click(object sender, EventArgs e)
        {
            LoadSelectedKspInstance();
        }

        EditCategories editCategories;
        private void opAddCategoryButton_Click(object sender, EventArgs e)
        {
            List<string> sendList = new List<string>();

            foreach (string category in opCategoryBox.Items)
            {
                sendList.Add(category);
            }

            editCategories = new EditCategories(sendList);
            editCategories.FormClosing += new FormClosingEventHandler(editCategories_FormClosing);

            editCategories.ShowDialog();
        }

        void editCategories_FormClosing(object sender, FormClosingEventArgs e)
        {
            opCategoryBox.Items.Clear();

            foreach (string category in editCategories.categoryList)
            {
                opCategoryBox.Items.Add(category);
            }
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
            LoadSelectedKspInstance();
        }

        private void modBox_DoubleClick(object sender, EventArgs e)
        {
            InstallDeinstallSelectedModbox();
        }

        private void opInstallButton_Click(object sender, EventArgs e)
        {
            string selectedDlmItem = (string)downloadedModBox.SelectedItem;

            if (selectedMod.name == selectedDlmItem.Replace(" - Update Available", ""))
            {
                InstallDeinstallSelectedModbox();
            }
            else
            {
                List<InstalledInfo> sendList = new List<InstalledInfo>();
                sendList.Add(selectedInstalledMod);

                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), new List<ModInfo>(), sendList, new List<ModInfo>());
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
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
                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), new List<ModInfo>(), sendList, new List<ModInfo>());
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
        }

        private void topButton2_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();

            for (int i = modInfo.modList.Count - 1; i >= 0; i--)
            {
                foreach (FavInfo fav in kspInfo.favoritesList)
                {
                    if (modInfo.modList[i].key == fav.key)
                    {
                        if (fav.isFav && !Functions.IsModInstalled(modInfo.modList[i]))
                        {
                            sendList.Add(modInfo.modList[i]);
                        }

                        break;
                    }
                }
            }

            if (sendList.Count > 0)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), new List<ModInfo>(), new List<InstalledInfo>(), sendList);
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }
        }

        // ModInfo editing stuff
        private bool isChangingSelection = true;

        private void installedModBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOpSettings((string)installedModBox.SelectedItem, true);
        }

        private void modBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOpSettings((string)downloadedModBox.SelectedItem, false);
        }

        private void UpdateOpSettings(string itemName, bool installed)
        {
            isChangingSelection = false;
            bool modExists = false;

            selectedInstalledMod = Functions.GetInstalledMod((string)installedModBox.SelectedItem);

            selectedMod = null;
            for (int i = 0; i < modInfo.modList.Count; i++)
            {
                if (modInfo.modList[i].name == itemName.Replace(" - Update Available", ""))
                {
                    selectedMod = modInfo.modList[i];
                    modExists = true;
                    break;
                }
            }

            if (modExists)
            {
                for (int i = 0; i < kspInfo.favoritesList.Count; i++)
                {
                    if (selectedMod.key == kspInfo.favoritesList[i].key)
                    {
                        selectedFav = kspInfo.favoritesList[i];
                        break;
                    }
                }
            }
            else
            {
                selectedFav = null;
            }

            if (modExists)
            {
                modInfo.ManageModInfo(selectedMod);

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

            if (modExists && installed)
            {
                opInstallButton.Text = "Deinstall Mod";
            }
            else if (modExists && !installed)
            {
                opInstallButton.Text = "Install Mod";
            }
            else
            {
                opInstallButton.Text = "Install Mod / Deinstall Mod";
            }

            if (modExists && installed)
            {
                opReinstallLabel.Enabled = true;

                if (selectedMod.version != selectedInstalledMod.version)
                {
                    opReinstallLabel.Text = "Reinstall Needed";
                }
                else
                {
                    opReinstallLabel.Text = "Reinstall Not Needed";
                }
            }
            else
            {
                opReinstallLabel.Text = "Reinstall Not Possible";
            }

            if (selectedMod.websites.dlSite == "NONE")
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
            opInstallButton.Enabled = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;

            deleteModButton.Enabled = false;
        }

        private void UnlockSettingEditor()
        {
            opInstallButton.Enabled = true;
            opReinstallButton.Enabled = Functions.IsModInstalled(selectedMod);

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            deleteModButton.Enabled = true;
        }

        private void opNameBox_TextChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.name = opNameBox.Text;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedMod.name);
            }
        }

        private void opCategoryBox_TextChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.category = opCategoryBox.Text;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedMod.name);
            }
        }

        private void opSiteBox_TextChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.websites.website = opSiteBox.Text;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedMod.name);
            }
        }

        private void opDlSiteBox_TextChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.websites.dlSite = opDlSiteBox.Text;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedMod.name);
            }
        }

        private void opIsFavoriteBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedFav.isFav = opIsFavoriteBox.Checked;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedMod.name);
            }
        }

        private void opCanDownloadBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isChangingSelection)
            {
                selectedMod.canUpdate = opCanDownloadBox.Checked;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedMod.name);
            }
        }

        private void modBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                selectedFav.isFav = !selectedFav.isFav;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedMod.name);
            }
        }

        private void opOpenSite_Click(object sender, EventArgs e)
        {
            Process.Start(selectedMod.websites.website);
        }

        private void opCheckUpdateButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();
            sendList.Add(selectedMod);

            InstallDeinstallForm form = new InstallDeinstallForm(sendList, new List<ModInfo>(), new List<InstalledInfo>(), new List<ModInfo>());
            form.ShowDialog();

            UpdateModList(selectedMod.name);
        }

        private void opDownloadButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();

            if (selectedMod.websites.dlSite != "NONE")
            {
                sendList.Add(selectedMod);

                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), sendList, new List<InstalledInfo>(), new List<ModInfo>());
                form.ShowDialog();

                UpdateModList(selectedMod.name);
            }

            else
            {
                Process.Start(selectedMod.websites.website);
            }
        }

        private void opReinstallButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendListA = new List<ModInfo>();
            List<InstalledInfo> sendListB = new List<InstalledInfo>();
            sendListA.Add(selectedMod);
            sendListB.Add(selectedInstalledMod);

            InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), new List<ModInfo>(), sendListB, sendListA);
            form.ShowDialog();

            UpdateModList(selectedMod.name);
        }

        private void opAddCategoryButton_Click_1(object sender, EventArgs e)
        {
            if (opCategoryBox.Text != "" && !opCategoryBox.Items.Contains(opCategoryBox.Text))
            {
                opCategoryBox.Items.Add(opCategoryBox.Text);
            }
        }
    }
}
