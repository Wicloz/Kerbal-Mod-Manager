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
        private string selectedItem = "";
        private bool initialised = false;

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

            initialised = true;
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
            List<string> sendList = new List<string>();

            foreach (string category in opCategoryBox.Items)
            {
                sendList.Add(category);
            }

            SaveLoad.SaveFileXml(new Settings(modInfo.modsPath, instanceList, installationBox.SelectedIndex, sendList), settingFolder + "\\settings.txt");
        }

        public List<string> GetCategories()
        {
            List<string> sendList = new List<string>();

            foreach (string category in opCategoryBox.Items)
            {
                sendList.Add(category);
            }

            return sendList;
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

            if (modInfo.loaded && initialised)
            {
                UpdateModList("");
            }
        }

        private void ChangeKspFolder(string newPath)
        {
            kspInfo.UnloadInstance();
            kspInfo.LoadInstance(newPath);
            downloadedListView.Enabled = kspInfo.loaded;
            installedListView.Enabled = kspInfo.loaded;

            if (kspInfo.loaded)
            {
                UnlockSettingEditor();
            }
            else
            {
                BlockSettingEditor();
            }

            if (kspInfo.loaded && initialised)
            {
                UpdateModList("");
            }
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
            downloadedListView.Items.Clear();
            installedListView.Items.Clear();
            SortLists();

            // Istalled Mod List
            foreach (InstalledInfo installedMod in kspInfo.installedModList)
            {
                if (!installedMod.codeName.Contains("Overrides\\"))
                {
                    ListViewItem lvi = new ListViewItem(installedMod.modName);
                    lvi.SubItems.Add(installedMod.category);

                    ModInfo mod = installedMod.GetModInfo();

                    string updateStatus = "";
                    string isFav = "";

                    if (mod != null)
                    {
                        if (mod.canUpdate)
                        {
                            updateStatus = "Update Required";
                        }
                        else if (mod.version != installedMod.version)
                        {
                            updateStatus = "Reinstall Required";
                        }
                        else
                        {
                            updateStatus = "Mod up to date";
                        }

                        if (mod.GetFav().isFav)
                        {
                            isFav = "True";
                        }
                        else
                        {
                            isFav = "False";
                        }
                    }
                    else
                    {
                        updateStatus = "Not Available";
                        isFav = "N/A";
                    }

                    lvi.SubItems.Add(updateStatus);
                    lvi.SubItems.Add(isFav);

                    installedListView.Items.Add(lvi);
                }
            }

            // Downloaded Mod List
            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\") && !mod.isInstalled && mod.hasZipfile)
                {
                    ListViewItem lvi = new ListViewItem(mod.name);
                    lvi.SubItems.Add(mod.category);

                    string updateStatus = "";
                    if (mod.canUpdate)
                    {
                        updateStatus = "Update Required";
                    }
                    else
                    {
                        updateStatus = "Mod up to date";
                    }
                    lvi.SubItems.Add(updateStatus);

                    string isFav = "";
                    if (mod.GetFav().isFav)
                    {
                        isFav = "True";
                    }
                    else
                    {
                        isFav = "False";
                    }
                    lvi.SubItems.Add(isFav);

                    downloadedListView.Items.Add(lvi);
                }
            }

            // Selection
            if (modName != "")
            {
                for (int i = 0; i < installedListView.Items.Count; i++)
                {
                    string item = installedListView.Items[i].SubItems[0].Text;

                    if (item == modName)
                    {
                        installedListView.SelectedIndices.Clear();
                        installedListView.SelectedIndices.Add(i);
                    }
                }

                for (int i = 0; i < downloadedListView.Items.Count; i++)
                {
                    string item = downloadedListView.Items[i].SubItems[0].Text;

                    if (item == modName)
                    {
                        downloadedListView.SelectedIndices.Clear();
                        downloadedListView.SelectedIndices.Add(i);
                    }
                }
            }
            else
            {
                if (installedListView.Items.Count > 0)
                {
                    installedListView.SelectedIndices.Add(0);
                }
                if (downloadedListView.Items.Count > 0)
                {
                    downloadedListView.SelectedIndices.Add(0);
                }
            }

            // Updating
            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.canUpdate)
                {
                    Directory.CreateDirectory(modInfo.modsPath + "\\ModDownloads\\" + mod.name.Replace(" ", "_"));
                }
            }

            kspInfo.SaveFiles(kspInfo.kspFolder);
            modInfo.SaveFiles(modInfo.modsPath);
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

        private void InstallDeinstallSelected()
        {
            bool isInstalled = false;

            if (selectedInstalledMod != null)
            {
                isInstalled = true;
            }

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

            UpdateModList(selectedItem);
        }

        // Buttons and stuff
        private void reinstallAllButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendListA = new List<ModInfo>();
            List<InstalledInfo> sendListB = new List<InstalledInfo>();

            foreach (ModInfo mod in modInfo.modList)
            {
                foreach (InstalledInfo installedMod in kspInfo.installedModList)
                {
                    if (mod.key == installedMod.key && !mod.zipfile.Contains("Overrides\\"))
                    {
                        sendListA.Add(mod);
                        sendListB.Add(installedMod);
                        break;
                    }
                }
            }

            if (sendListA.Count + sendListB.Count > 0 && sendListA.Count == sendListB.Count)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), new List<ModInfo>(), sendListB, sendListA);
                form.ShowDialog();

                UpdateModList(selectedItem);
            }
        }

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

                UpdateModList(selectedItem);
            }
        }

        private void downloadModButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();
            List<ModInfo> sendListA = new List<ModInfo>();
            List<InstalledInfo> sendListB = new List<InstalledInfo>();

            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.canUpdate && !mod.zipfile.Contains("Overrides\\"))
                {
                    sendList.Add(mod);
                }

                foreach (InstalledInfo installedMod in kspInfo.installedModList)
                {
                    if (mod.key == installedMod.key)
                    {
                        if (mod.version != installedMod.version)
                        {
                            sendListA.Add(mod);
                            sendListB.Add(installedMod);
                        }
                        break;
                    }
                }
            }

            if (sendList.Count + sendListA.Count + sendListB.Count > 0)
            {
                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), sendList, sendListB, sendListA);
                form.ShowDialog();

                UpdateModList(selectedItem);
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
                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), new List<ModInfo>(), sendList, new List<ModInfo>());
                form.ShowDialog();

                UpdateModList(selectedItem);
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
                        if (fav.isFav && !modInfo.modList[i].isInstalled)
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

                UpdateModList(selectedItem);
            }
        }

        // ModInfo editing stuff
        private bool isChangingSelection = true;

        private void installedListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (installedListView.SelectedItems.Count > 0)
            {
                UpdateOpSettings(installedListView.SelectedItems[0].SubItems[0].Text, true);
            }
        }

        private void downloadedListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (downloadedListView.SelectedItems.Count > 0)
            {
                UpdateOpSettings(downloadedListView.SelectedItems[0].SubItems[0].Text, false);
            }
        }

        private void UpdateOpSettings(string itemName, bool installed)
        {
            isChangingSelection = true;

            selectedInstalledMod = Functions.GetInstalledMod(itemName);
            selectedMod = Functions.GetDownloadedMod(itemName);

            if (selectedMod != null)
            {
                selectedItem = selectedMod.name;
            }
            else if (selectedInstalledMod != null)
            {
                selectedItem = selectedInstalledMod.modName;
            }
            else
            {
                selectedItem = "";
            }

            if (selectedMod != null)
            {
                selectedFav = selectedMod.GetFav();
            }
            else
            {
                selectedFav = null;
            }

            if (selectedMod != null)
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

            if (installed)
            {
                opInstallButton.Text = "Deinstall Mod";
            }
            else if (selectedMod != null && !installed)
            {
                opInstallButton.Text = "Install Mod";
            }

            if (selectedMod != null && selectedMod.websites.dlSite == "NONE")
            {
                opDownloadButton.Text = "Open Site";
            }
            else if (selectedMod != null)
            {
                opDownloadButton.Text = "Update Mod";
            }

            isChangingSelection = false;
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

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            deleteModButton.Enabled = true;
        }

        private void opNameBox_TextChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.name = opNameBox.Text;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(opNameBox.Text);
            }
        }

        private void opCategoryBox_TextChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.category = opCategoryBox.Text;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedItem);
            }
        }

        private void opSiteBox_TextChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.websites.website = opSiteBox.Text;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedItem);
            }
        }

        private void opDlSiteBox_TextChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.websites.dlSite = opDlSiteBox.Text;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedItem);
            }
        }

        private void opIsFavoriteBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedFav.isFav = opIsFavoriteBox.Checked;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedItem);
            }
        }

        private void opCanDownloadBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.canUpdate = opCanDownloadBox.Checked;

                modInfo.ManageModInfo(selectedMod);
                UpdateModList(selectedItem);
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

            UpdateModList(selectedItem);
        }

        private void opDownloadButton_Click(object sender, EventArgs e)
        {
            List<ModInfo> sendList = new List<ModInfo>();

            if (selectedMod.websites.dlSite != "NONE")
            {
                sendList.Add(selectedMod);

                InstallDeinstallForm form = new InstallDeinstallForm(new List<ModInfo>(), sendList, new List<InstalledInfo>(), new List<ModInfo>());
                form.ShowDialog();

                UpdateModList(selectedItem);
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

            UpdateModList(selectedItem);
        }

        private void opAddCategoryButton_Click_1(object sender, EventArgs e)
        {
            if (opCategoryBox.Text != "" && !opCategoryBox.Items.Contains(opCategoryBox.Text))
            {
                opCategoryBox.Items.Add(opCategoryBox.Text);
            }
        }

        private void installedListView_DoubleClick(object sender, EventArgs e)
        {
            InstallDeinstallSelected();
        }

        private void downloadedListView_DoubleClick(object sender, EventArgs e)
        {
            InstallDeinstallSelected();
        }

        private void downloadedListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Char.ConvertFromUtf32(32).ToCharArray()[0])
            {
                try
                {
                    selectedFav.isFav = !selectedFav.isFav;
                }
                catch
                { }

                UpdateModList(selectedItem);
            }
        }

        private void installedListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Char.ConvertFromUtf32(32).ToCharArray()[0])
            {
                try
                {
                    selectedFav.isFav = !selectedFav.isFav;
                }
                catch
                { }

                UpdateModList(selectedItem);
            }
        }
    }
}
