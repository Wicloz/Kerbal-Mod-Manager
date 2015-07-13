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
        private UpdateModEvents um = new UpdateModEvents();
        private UpdateCheckEvents uc = new UpdateCheckEvents();
        public TemplateManager tm = new TemplateManager();

        private string settingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KMM\\settings";
        public List<InstallInstance> instanceList = new List<InstallInstance>();

        private ModInfo selectedMod = new ModInfo();
        private InstalledInfo selectedInstalledMod = new InstalledInfo();

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

            string fixedSettingsFile = settingFolder + "\\settings.txt";
            if (File.Exists(fixedSettingsFile))
            {
                try
                {
                    SettingsFixed settings = SaveLoad.LoadFileXml<SettingsFixed>(fixedSettingsFile);
                    instanceList = settings.instances;
                    selectedIndex = settings.selectedInstance;
                    savedModsPath = settings.modsPath;
                }
                catch
                {
                    File.Delete(fixedSettingsFile);
                    instanceList = new List<InstallInstance>();
                    instanceList.Add(new InstallInstance("New Instance"));
                }
            }
            else
            {
                instanceList = new List<InstallInstance>();
                instanceList.Add(new InstallInstance("New Instance"));
            }

            string localSettingsFile = Environment.CurrentDirectory + "\\" + "KMM_Settings.txt";
            if (File.Exists(localSettingsFile))
            {
                try
                {
                    SettingsLocal savedCategories = SaveLoad.LoadFileXml<SettingsLocal>(localSettingsFile);

                    if (savedCategories.categoryList.Count > 0)
                    {
                        opCategoryBox.Items.Clear();
                        foreach (string category in savedCategories.categoryList)
                        {
                            opCategoryBox.Items.Add(category);
                        }
                    }
                }
                catch
                {
                    File.Delete(localSettingsFile);
                }
            }

            HandleCategories();
            filterList.Add("ALL");

            tm.LoadTemplates();

            ChangeModFolder(savedModsPath);
            ChangeKspFolder(instanceList[selectedIndex].kspPath);

            initialised = true;
            UpdateInstallInstanceList(selectedIndex);
            UpdateModList("", true);

            SaveSettings();
            tm.SaveTemplates();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            tm.SaveTemplates();
            modInfo.UnloadInstance();
            kspInfo.UnloadInstance();
        }

        private void SaveSettings()
        {
            List<string> sendList = new List<string>();

            foreach (string category in opCategoryBox.Items)
            {
                sendList.Add(category);
            }

            SaveLoad.SaveFileXml(new SettingsFixed(modInfo.modsPath, instanceList, installationBox.SelectedIndex), settingFolder + "\\settings.txt");
            SaveLoad.SaveFileXml(new SettingsLocal(sendList), Environment.CurrentDirectory + "\\" + "KMM_Settings.txt");

            LogMessage("Setting Files Saved");
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
        private void ChangeModFolder(string newPath)
        {
            modFolderBox.Text = newPath;

            modInfo.UnloadInstance();
            modInfo.LoadInstance(newPath);

            if (modInfo.loaded && initialised)
            {
                UpdateModList("", true);
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
                UpdateModList("", true);
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

        private bool updatingModList = false;
        private void UpdateModList(string modName, bool reselect)
        {
            updatingModList = true;
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
                        lvi.Checked = true;
                        lvi.SubItems.Add(installedMod.category);

                        lvi.SubItems.Add("Not Available");
                        lvi.SubItems.Add("N/A");
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
                    InstalledInfo installedMod = MiscFunctions.GetInstalledMod(mod);

                    ListViewItem lvi = new ListViewItem(mod.name);
                    lvi.Checked = mod.isInstalled;
                    lvi.SubItems.Add(mod.category);

                    string updateStatus = "";
                    if (mod.website == "NONE")
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

                    lvi.SubItems.Add(mod.vnLocal);
                    lvi.SubItems.Add(mod.vnOnline);

                    modsListView.Items.Add(lvi);
                }
            }

            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\") && !mod.isInstalled && !mod.hasZipfile && (filterList.Contains(mod.category) || filterList.Contains("ALL")))
                {
                    ListViewItem lvi = new ListViewItem(mod.name);
                    lvi.Checked = false;
                    lvi.SubItems.Add(mod.category);

                    lvi.SubItems.Add("Not Downloaded");

                    lvi.SubItems.Add("N/A");
                    lvi.SubItems.Add(mod.vnOnline);

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

            if (reselect)
            {
                modsListView.Select();
            }

            updatingModList = false;
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

        private void SwitchModState(ModInfo dlMod, InstalledInfo isMod)
        {
            bool isInstalled = false;

            if (isMod != null)
            {
                isInstalled = true;
            }

            if (!isInstalled)
            {
                InstallDeinstallForm form = new InstallDeinstallForm();

                form.AddInstallMod(dlMod);

                if (form.HasActions())
                {
                    form.ShowDialog();
                    UpdateModList(selectedItem, true);
                }
            }
            else if (isInstalled)
            {
                InstallDeinstallForm form = new InstallDeinstallForm();

                form.AddDeinstallMod(isMod);

                if (form.HasActions())
                {
                    form.ShowDialog();
                    UpdateModList(selectedItem, true);
                }
            }
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

            UpdateModList("", true);
        }

        private void reinstallAllButton_Click(object sender, EventArgs e)
        {
            InstallDeinstallForm form = new InstallDeinstallForm();

            foreach (ModInfo mod in modInfo.modList)
            {
                foreach (InstalledInfo installedMod in kspInfo.installedModList)
                {
                    if (mod.key == installedMod.key && !mod.zipfile.Contains("Overrides\\"))
                    {
                        form.AddInstallMod(mod);
                        form.AddDeinstallMod(installedMod);
                        break;
                    }
                }
            }

            if (form.HasActions())
            {
                form.ShowDialog();
                UpdateModList(selectedItem, true);
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
            ChangeKspFolder(instanceList[installationBox.SelectedIndex].kspPath);
            tm.SaveTemplates();

            initialised = true;
            UpdateModList("", true);
        }

        private void checkUpdateButton_Click(object sender, EventArgs e)
        {
            InstallDeinstallForm form = new InstallDeinstallForm();

            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\") && mod.website != "NONE")
                {
                    form.AddCheckUpdateMod(mod);
                }
            }

            if (form.HasActions())
            {
                form.ShowDialog();
                UpdateModList(selectedItem, true);
            }
        }

        private void downloadModButton_Click(object sender, EventArgs e)
        {
            InstallDeinstallForm form = new InstallDeinstallForm();

            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.canUpdate && !mod.zipfile.Contains("Overrides\\"))
                {
                    form.AddUpdateMod(mod);
                }
            }

            foreach (ModInfo mod in modInfo.modList)
            {
                if (mod.isInstalled)
                {
                    InstalledInfo installedMod = MiscFunctions.GetInstalledMod(mod);

                    if (mod.version != installedMod.version)
                    {
                        form.AddInstallMod(mod);
                        form.AddDeinstallMod(installedMod);
                    }
                }
            }

            if (form.HasActions())
            {
                form.ShowDialog();
                UpdateModList(selectedItem, true);
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

            ChangeKspFolder(instanceList[installationBox.SelectedIndex].kspPath);
        }

        private void installationBox_Click(object sender, EventArgs e)
        {
            ChangeKspFolder(instanceList[installationBox.SelectedIndex].kspPath);
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
            ChangeKspFolder(instanceList[installationBox.SelectedIndex].kspPath);
        }

        private void opInstallButton_Click(object sender, EventArgs e)
        {
            SwitchModState(selectedMod, selectedInstalledMod);
        }

        private void deleteMod_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < modInfo.modList.Count; i++)
            {
                if (modInfo.modList[i].key == selectedMod.key)
                {
                    modInfo.modList[i].canUpdate = false;
                    modInfo.modList.RemoveAt(i);
                    break;
                }
            }

            if (selectedMod.hasZipfile)
            {
                File.Delete(modInfo.modsPath + "\\" + selectedMod.zipfile);
            }

            UpdateModList("", true);
        }

        private void deleteZipButton_Click(object sender, EventArgs e)
        {
            if (selectedMod.hasZipfile)
            {
                File.Delete(modInfo.modsPath + "\\" + selectedMod.zipfile);

                ChangeModFolder(modFolderBox.Text);
                UpdateModList("", true);
            }
        }

        private void topButton1_Click(object sender, EventArgs e)
        {
            InstallDeinstallForm form = new InstallDeinstallForm();

            for (int i = 0; i < kspInfo.installedModList.Count; i++)
            {
                if (!kspInfo.installedModList[i].codeName.Contains("Overrides\\"))
                {
                    form.AddDeinstallMod(kspInfo.installedModList[i]);
                }
            }

            if (form.HasActions())
            {
                form.ShowDialog();
                UpdateModList(selectedItem, true);
            }
        }

        private void fdaButton_Click(object sender, EventArgs e)
        {
            checkUpdateButton_Click(null, null);

            InstallDeinstallForm form = new InstallDeinstallForm();
            List<ModInfo> sendList = new List<ModInfo>();

            foreach (ModInfo mod in modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\") && mod.dlSite != "NONE" && !mod.dlSite.Contains("forum.kerbalspaceprogram.com"))
                {
                    sendList.Add(mod);
                    form.AddUpdateMod(mod);
                }
            }

            foreach (ModInfo mod in sendList)
            {
                if (mod.isInstalled)
                {
                    InstalledInfo installedMod = MiscFunctions.GetInstalledMod(mod);

                    if (mod.version != installedMod.version)
                    {
                        form.AddInstallMod(mod);
                        form.AddDeinstallMod(installedMod);
                    }
                }
            }

            if (form.HasActions())
            {
                form.ShowDialog();
                UpdateModList(selectedItem, true);
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
            bool hasZip = true;

            selectedInstalledMod = MiscFunctions.GetInstalledMod(itemName);
            selectedMod = MiscFunctions.GetDownloadedMod(itemName);

            if (selectedMod == null || selectedMod.isInstalled)
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

            if (selectedMod == null || !selectedMod.hasZipfile)
            {
                hasZip = false;
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

            // Managing Option Editor
            if (selectedMod != null)
            {
                selectedMod.ManageMod();

                opNameBox.Text = selectedMod.name;
                opCategoryBox.Text = selectedMod.category;
                opSiteBox.Text = selectedMod.website;

                opTempBox.Text = selectedMod.dlSite;

                opCanDownloadBox.Checked = selectedMod.canUpdate;

                UnlockSettingEditor();

                if (!hasZip)
                {
                    opInstallButton.Enabled = false;
                }
            }
            else
            {
                opNameBox.Text = "";
                opCategoryBox.Text = "";
                opSiteBox.Text = "";
                opTempBox.Text = "";

                opCanDownloadBox.Checked = false;

                BlockSettingEditor();
            }

            if (mode == "installed")
            {
                opInstallButton.Text = "Deinstall Mod";
                opInstallButton.Enabled = true;
            }
            else if (selectedMod != null && mode != "installed")
            {
                opInstallButton.Text = "Install Mod";
            }

            if (mode == "installed" && hasZip)
            {
                opReinstallButton.Enabled = true;
            }
            else
            {
                opReinstallButton.Enabled = false;
            }

            if (selectedMod != null && selectedMod.dlSite == "NONE")
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
            opReinstallButton.Enabled = false;

            groupBox1.Enabled = false;
            groupBox2.Enabled = false;

            deleteModButton.Enabled = false;
        }

        private void UnlockSettingEditor()
        {
            opInstallButton.Enabled = true;
            opReinstallButton.Enabled = true;

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            deleteModButton.Enabled = true;
        }

        private void opNameBox_TextChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.name = opNameBox.Text;

                selectedMod.ManageMod();
                UpdateModList(opNameBox.Text, false);
            }
        }

        private void opCategoryBox_TextChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.category = opCategoryBox.Text;

                selectedMod.ManageMod();
                UpdateModList(selectedItem, false);
            }
        }

        private void opSiteBox_TextChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.website = opSiteBox.Text;

                selectedMod.ManageMod();
                UpdateOpSettings(selectedMod.name);
            }
        }

        private void opCanDownloadBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isChangingSelection)
            {
                selectedMod.canUpdate = opCanDownloadBox.Checked;

                selectedMod.ManageMod();
                UpdateModList(selectedItem, true);
            }
        }

        private void opOpenSite_Click(object sender, EventArgs e)
        {
            Process.Start(selectedMod.website);
        }

        private void opCheckUpdateButton_Click(object sender, EventArgs e)
        {
            InstallDeinstallForm form = new InstallDeinstallForm();

            form.AddCheckUpdateMod(selectedMod);

            if (form.HasActions())
            {
                form.ShowDialog();
                UpdateModList(selectedItem, true);
            }
        }

        private void opDownloadButton_Click(object sender, EventArgs e)
        {
            InstallDeinstallForm form = new InstallDeinstallForm();

            if (selectedMod.dlSite != "NONE")
            {
                form.AddCheckUpdateMod(selectedMod);
                form.AddUpdateMod(selectedMod);

                if (form.HasActions())
                {
                    form.ShowDialog();
                    UpdateModList(selectedItem, true);
                }
            }

            else
            {
                Process.Start(selectedMod.website);
            }
        }

        private void opReinstallButton_Click(object sender, EventArgs e)
        {
            InstallDeinstallForm form = new InstallDeinstallForm();

            form.AddInstallMod(selectedMod);
            form.AddDeinstallMod(selectedInstalledMod);

            if (form.HasActions())
            {
                form.ShowDialog();
                UpdateModList(selectedItem, true);
            }
        }

        private void opAddCategoryButton_Click_1(object sender, EventArgs e)
        {
            if (opCategoryBox.Text != "" && !opCategoryBox.Items.Contains(opCategoryBox.Text))
            {
                opCategoryBox.Items.Add(opCategoryBox.Text);
            }

            HandleCategories();
        }

        private void opGoogleButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.google.com/?#q=ksp+" + selectedMod.name);
        }

        private void downloadedListView_DoubleClick(object sender, EventArgs e)
        {
            SwitchModState(selectedMod, selectedInstalledMod);
        }

        private void modsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!updatingModList)
            {
                modsListView.SelectedIndices.Clear();
                modsListView.SelectedIndices.Add(e.Item.Index);

                SwitchModState(selectedMod, selectedInstalledMod);
            }
        }

        private void downloadedListView_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void catButton1_Click(object sender, EventArgs e)
        {
            filterList.Clear();
            filterList.Add("ALL");
            UpdateModList("", true);
        }

        private void catButton2_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("API");
            filterList.Add("Core");

            UpdateModList("", true);
        }

        private void catButton3_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Tools");

            UpdateModList("", true);
        }

        private void catButton4_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Plugins");
            filterList.Add("Science");
            filterList.Add("Realism");
            filterList.Add("Planet Stuff");

            UpdateModList("", true);
        }

        private void catButton5_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Graphic Install Packs");
            filterList.Add("Graphic Mods");
            filterList.Add("Sound Mods");

            UpdateModList("", true);
        }

        private void catButton6_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Parts, Base");

            UpdateModList("", true);
        }

        private void catButton7_Click(object sender, EventArgs e)
        {
            filterList.Clear();

            filterList.Add("Parts");

            UpdateModList("", true);
        }
    }
}
