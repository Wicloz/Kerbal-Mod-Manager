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

        private List<string> filterList = new List<string>();

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

            HandleCategories();
            filterList.Add("ALL");

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
            modsListView.Enabled = kspInfo.loaded;

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
            modsListView.Items.Clear();
            SortLists();

            // Istalled Mod List
            foreach (InstalledInfo installedMod in kspInfo.installedModList)
            {
                if (!installedMod.codeName.Contains("Overrides\\") && (filterList.Contains(installedMod.category) || filterList.Contains("ALL")))
                {
                    ModInfo mod = installedMod.GetModInfo();

                    if (mod == null)
                    {
                        ListViewItem lvi = new ListViewItem(installedMod.modName);
                        lvi.SubItems.Add(installedMod.category);

                        lvi.SubItems.Add("Yes");
                        lvi.SubItems.Add("Not Available");
                        lvi.SubItems.Add("N/A");

                        modsListView.Items.Add(lvi);
                    }
                }
            }

            // Downloaded Mod List
            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\") && mod.hasZipfile && (filterList.Contains(mod.category) || filterList.Contains("ALL")))
                {
                    InstalledInfo installedMod = Functions.GetInstalledMod(mod);

                    ListViewItem lvi = new ListViewItem(mod.name);
                    lvi.SubItems.Add(mod.category);

                    if (mod.isInstalled)
                    {
                        lvi.SubItems.Add("Yes");
                    }
                    else
                    {
                        lvi.SubItems.Add("No");
                    }

                    string updateStatus = "";
                    if (mod.websites.website == "NONE")
                    {
                        updateStatus = "No Website";
                    }
                    else if (mod.canUpdate)
                    {
                        updateStatus = "Update Required";
                    }
                    else if (installedMod != null && mod.version != installedMod.version)
                    {
                        updateStatus = "Reinstall Required";
                    }
                    else
                    {
                        updateStatus = "Mod up to date";
                    }
                    lvi.SubItems.Add(updateStatus);

                    if (mod.favorite.isFav)
                    {
                        lvi.SubItems.Add("True");
                    }
                    else
                    {
                        lvi.SubItems.Add("False");
                    }

                    modsListView.Items.Add(lvi);
                }
            }

            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\") && !mod.isInstalled && !mod.hasZipfile && (filterList.Contains(mod.category) || filterList.Contains("ALL")))
                {
                    ListViewItem lvi = new ListViewItem(mod.name);
                    lvi.SubItems.Add(mod.category);

                    lvi.SubItems.Add("No");
                    lvi.SubItems.Add("Not Downloaded");

                    string isFav = "";
                    if (mod.favorite.isFav)
                    {
                        isFav = "True";
                    }
                    else
                    {
                        isFav = "False";
                    }
                    lvi.SubItems.Add(isFav);

                    modsListView.Items.Add(lvi);
                }
            }

            // Selection
            if (modName != "")
            {
                for (int i = 0; i < modsListView.Items.Count; i++)
                {
                    string item = modsListView.Items[i].SubItems[0].Text;

                    if (item == modName)
                    {
                        modsListView.SelectedIndices.Clear();
                        modsListView.SelectedIndices.Add(i);
                    }
                }
            }
            else if (modsListView.Items.Count > 0)
            {
                modsListView.SelectedIndices.Add(0);
            }

            // Updating
            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.canUpdate)
                {
                    Directory.CreateDirectory(modInfo.modsPath + "\\ModDownloads\\" + mod.name.Replace(" ", "_"));
                }
            }
        }

        private void HandleCategories()
        {
            contextMenuStrip1.Items.Clear();
            foreach (string cat in opCategoryBox.Items)
            {
                contextMenuStrip1.Items.Add(cat);
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
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (filterList.Contains(e.ClickedItem.Text))
            {
                filterList.Remove(e.ClickedItem.Text);
            }
            else
            {
                filterList.Add(e.ClickedItem.Text);
            }

            UpdateModList("");
        }

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
            initialised = false;

            ChangeModFolder(modFolderBox.Text);
            LoadSelectedKspInstance();

            initialised = true;
            UpdateModList("");
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

        private void favAllButton_Click(object sender, EventArgs e)
        {
            foreach (FavInfo fav in kspInfo.favoritesList)
            {
                fav.isFav = true;
            }
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

            HandleCategories();
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

        private void deleteZipButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < modInfo.modList.Count; i++)
            {
                if (modInfo.modList[i].key == selectedMod.key)
                {
                    File.Delete(modInfo.modsPath + "\\" + modInfo.modList[i].zipfile);
                    break;
                }
            }

            ChangeModFolder(modFolderBox.Text);
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

        private void downloadedListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modsListView.SelectedItems.Count > 0)
            {
                UpdateOpSettings(modsListView.SelectedItems[0].SubItems[0].Text);
            }
        }

        private void UpdateOpSettings(string itemName)
        {
            isChangingSelection = true;
            string mode = "";

            selectedInstalledMod = Functions.GetInstalledMod(itemName);
            selectedMod = Functions.GetDownloadedMod(itemName);

            if (selectedMod.isInstalled || selectedMod == null)
            {
                mode = "installed";
            }
            else if (selectedMod.hasZipfile)
            {
                mode = "downloaded";
            }
            else
            {
                mode = "missing";
            }

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
                selectedFav = selectedMod.favorite;
            }
            else
            {
                selectedFav = null;
            }

            // Managing Option Editor
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

                if (mode == "mssing")
                {
                    opInstallButton.Enabled = false;
                }
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

            if (mode == "installed")
            {
                opInstallButton.Text = "Deinstall Mod";
            }
            else if (selectedMod != null && mode != "installed")
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

                UpdateOpSettings(selectedMod.name);
            }
        }

        private void opDlSiteBox_TextChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.websites.dlSite = opDlSiteBox.Text;
                modInfo.ManageModInfo(selectedMod);

                UpdateOpSettings(selectedMod.name);
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

            HandleCategories();
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

        private void opGoogleButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.google.com/?#q=ksp+" + selectedMod.name);
        }

        private void catButton1_Click(object sender, EventArgs e)
        {
            filterList.Clear();
            filterList.Add("ALL");
            UpdateModList("");
        }

        private void catButton2_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("API");
            filterList.Add("Core");

            UpdateModList("");
        }

        private void catButton3_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Tools");

            UpdateModList("");
        }

        private void catButton4_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Plugins");
            filterList.Add("Science");
            filterList.Add("Realism");
            filterList.Add("Planet Stuff");

            UpdateModList("");
        }

        private void catButton5_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Graphic Install Packs");
            filterList.Add("Graphic Mods");
            filterList.Add("Sound Mods");

            UpdateModList("");
        }

        private void catButton6_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Parts, Base");

            UpdateModList("");
        }

        private void catButton7_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Parts, Stock Extension");
            filterList.Add("Parts");

            UpdateModList("");
        }
    }
}
