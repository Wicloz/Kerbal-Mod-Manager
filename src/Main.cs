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

namespace Kerbal_Mod_Manager
{
    public partial class Main : Form
    {
        public static Main acces;
        private string cd = Directory.GetCurrentDirectory();
        private Settings settings = new Settings();

        public KspFolder kspFolder = new KspFolder();
        public ModFolder modFolders = new ModFolder();
        private InstanceSettings editInstanceSettings = new InstanceSettings();
        private EditCategories editCategories = new EditCategories();

        private int selectedModIndex = 0;
        private ModInfo selectedMod;
        private bool updatingFields = false;
        private bool modDownloadBusy = false;
        private bool modInstallBusy = false;
        private bool modDeinstallBusy = false;
        private bool action = false;

        public List<string> GetCategories()
        {
            return settings.categories;
        }

        // Initialising
        public Main()
        {
            InitializeComponent();
            acces = this;
            editInstanceSettings.FormClosing += new FormClosingEventHandler(editInstanceForm_FormClosing);
            editCategories.FormClosing += new FormClosingEventHandler(editCategories_FormClosing);
            //Initiate timer
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            timer.Start();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (File.Exists(cd + "\\settings.cfg"))
            {
                settings = SaveLoad.LoadFileXml<Settings>(cd + "\\settings.cfg");
            }
            SaveSettings();
            SetCategories();
            ReloadFolderUi();
            ReloadModlistview();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            modFolders.SaveFolders();
            kspFolder.SaveInstance();
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
                ReloadModlistview();
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
            else
            {
                modFolders.ChangeFolders(new List<string>());
            }
            ReloadModlistview();
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
                kspFolder.ChangeInstance(settings.kspFolders[settings.selectedKsp].folder);
            }
        }

        // Selection UI
        void editInstanceForm_FormClosing(object sender, FormClosingEventArgs e)
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

        // Edit categories
        private void buttonEditCat_Click(object sender, EventArgs e)
        {
            editCategories.SetCategoryList(settings.categories);
            editCategories.ShowDialog();
        }

        void editCategories_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetCategories();
        }

        private void SetCategories()
        {
            string[] defaultCats = new string[] { "API", "Core", "Tools", "Plugins", "Science", "Realism", "Planetary", "Graphics Packs", "Visuals", "Sounds", "Parts, Base", "Parts" };

            foreach (string category in defaultCats)
            {
                if (!settings.categories.Contains(category))
                {
                    settings.categories.Add(category);
                }
            }

            foreach (string category in settings.categories)
            {
                comboBoxCategory.Items.Add(category);
            }
        }

        private void buttonAddCat_Click(object sender, EventArgs e)
        {
            settings.categories.Add(comboBoxCategory.Text);
        }

        // Modlist UI
        private void ReloadModlistview()
        {
            listViewMods.Items.Clear();
            listViewMods.SelectedIndices.Clear();
            selectedModIndex = 0;

            foreach (ModInfo mod in modFolders.mods)
            {
                ListViewItem lvi = new ListViewItem(mod.modName);

                lvi.SubItems.Add(mod.category);
                lvi.SubItems.Add(mod.uriState);
                lvi.SubItems.Add(mod.versionLocalNumeric);
                lvi.SubItems.Add(mod.versionLatestNumeric);
                lvi.SubItems.Add(mod.updateState);

                lvi.SubItems.Add(mod.website1);
                lvi.SubItems.Add(mod.website2);
                lvi.SubItems.Add(mod.website3);
                lvi.SubItems.Add(mod.website4);
                lvi.SubItems.Add(mod.website5);

                if (mod.isInstalled)
                {
                    lvi.Checked = true;
                }

                listViewMods.Items.Add(lvi);
            }

            for (int i = 0; i < listViewMods.Items.Count; i++)
            {
                if (selectedMod != null && listViewMods.Items[i].SubItems[0].Text == selectedMod.modName)
                {
                    selectedModIndex = i;
                    break;
                }
            }

            if (listViewMods.Items.Count > 0)
            {
                listViewMods.SelectedIndices.Add(selectedModIndex);
                listViewMods.Select();
            }
        }

        private void listViewMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMods.SelectedIndices.Count == 1)
            {
                selectedModIndex = listViewMods.SelectedIndices[0];
                selectedMod = modFolders.mods[selectedModIndex];
            }
            else
            {
                selectedModIndex = 0;
                selectedMod = null;
            }
            SetModFields();
        }

        private void listViewMods_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ModInfo mod = modFolders.mods[e.Item.Index];
            
            if (e.Item.Checked && !mod.isInstalled)
            {
                mod.installQueued = true;
            }
            else
            {
                mod.installQueued = false;
            }

            if (!e.Item.Checked && mod.isInstalled)
            {
                mod.deinstallQueued = true;
            }
            else
            {
                mod.deinstallQueued = false;
            }
        }

        // Modlist fields UI
        private void SetModFields()
        {
            updatingFields = true;
            if (selectedMod != null)
            {
                textBoxSelName.Text = selectedMod.modName;
                textBoxSelWebsite.Text = selectedMod.website;
                comboBoxCategory.Text = selectedMod.category;
                textBoxSelDebug.Text = selectedMod.dlSite;
                checkBoxCanUpdate.Checked = selectedMod.canUpdate;
                UnlockModFields();
            }
            else
            {
                textBoxSelName.Text = "";
                textBoxSelWebsite.Text = "";
                comboBoxCategory.Text = "";
                textBoxSelDebug.Text = "";
                checkBoxCanUpdate.Checked = false;
                LockModFields();
            }
            updatingFields = false;
        }

        private void LockModFields()
        {
            textBoxSelName.Enabled = false;
            comboBoxCategory.Enabled = false;
            textBoxSelWebsite.Enabled = false;
            textBoxSelDebug.Enabled = false;
            checkBoxCanUpdate.Enabled = false;
            buttonDeleteSelected.Enabled = false;
            buttonFindSelected.Enabled = false;
            buttonCheckSelected.Enabled = false;
            buttonUpdateSelected.Enabled = false;
            buttonInstallSelected.Enabled = false;
            buttonReinstallSelected.Enabled = false;
            buttonGoogle.Enabled = false;
            buttonAddCat.Enabled = false;
            buttonOpenSite.Enabled = false;

            buttonInstallSelected.Text = "Install/Deinstall Mod";
        }
        private void UnlockModFields()
        {
            textBoxSelName.Enabled = true;
            comboBoxCategory.Enabled = true;
            textBoxSelWebsite.Enabled = true;
            textBoxSelDebug.Enabled = true;
            checkBoxCanUpdate.Enabled = true;
            buttonDeleteSelected.Enabled = true;
            buttonFindSelected.Enabled = true;
            buttonCheckSelected.Enabled = true;
            buttonUpdateSelected.Enabled = selectedMod.canUpdate;
            buttonInstallSelected.Enabled = true;
            buttonReinstallSelected.Enabled = selectedMod.isInstalled;
            buttonGoogle.Enabled = true;
            buttonAddCat.Enabled = true;
            buttonOpenSite.Enabled = true;

            if (selectedMod.isInstalled)
            {
                buttonInstallSelected.Text = "Deinstall Mod";
            }
            else
            {
                buttonInstallSelected.Text = "Install Mod";
            }
        }

        private void textBoxSelName_TextChanged(object sender, EventArgs e)
        {
            if (!updatingFields)
            {
                selectedMod.modName = textBoxSelName.Text;
                listViewMods.Items[selectedModIndex].SubItems[0].Text = selectedMod.modName;
            }
        }

        private void textBoxSelWebsite_TextChanged(object sender, EventArgs e)
        {
            if (!updatingFields)
            {
                selectedMod.website = textBoxSelWebsite.Text;
                selectedMod.UpdateModValues();
                listViewMods.Items[selectedModIndex].SubItems[2].Text = selectedMod.uriState;
            }
        }

        private void comboBoxCategory_TextUpdate(object sender, EventArgs e)
        {
            if (!updatingFields)
            {
                selectedMod.category = comboBoxCategory.Text;
                listViewMods.Items[selectedModIndex].SubItems[1].Text = selectedMod.category;
            }
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingFields)
            {
                selectedMod.category = comboBoxCategory.Text;
                listViewMods.Items[selectedModIndex].SubItems[1].Text = selectedMod.category;
            }
        }

        private void checkBoxCanUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingFields)
            {
                selectedMod.canUpdate = checkBoxCanUpdate.Checked;
                buttonUpdateSelected.Enabled = selectedMod.canUpdate;
            }
        }

        private void buttonOpenSite_Click(object sender, EventArgs e)
        {
            if (selectedMod.website != "NONE")
            {
                Process.Start(selectedMod.website);
            }
        }

        private void buttonGoogle_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.google.com/?gws_rd=ssl#q=" + selectedMod.modName.Replace(" ", "+") + "+ksp");
        }

        private void textBoxSelName_Leave(object sender, EventArgs e)
        {
            ReloadModlistview();
        }

        private void comboBoxCategory_Leave(object sender, EventArgs e)
        {
            ReloadModlistview();
        }

        // Timer
        private void timer_Tick(object sender, EventArgs e)
        {
            bool allWorkDone = true;
            bool allDownloadDone = true;
            bool allInstallDone = true;
            bool allDeinstallDone = true;

            for (int i = 0; i < modFolders.mods.Count; i++)
            {
                ModInfo mod = modFolders.mods[i];

                if (mod.updateState != listViewMods.Items[i].SubItems[5].Text)
                {
                    listViewMods.Items[i].SubItems[5].Text = mod.updateState;
                }

                if (mod.findQueued && !mod.isWorking)
                {
                    LockButtons();
                    mod.FindWebsiteUri();
                }
                else if (mod.checkQueued && !mod.isWorking)
                {
                    LockButtons();
                    mod.CheckForUpdate();
                }
                else if (mod.downloadQueued && !mod.isWorking && !modDownloadBusy)
                {
                    LockButtons();
                    modDownloadBusy = true;
                    mod.UpdateMod();
                }
                else if (mod.deinstallQueued && !mod.isWorking && !modDeinstallBusy)
                {
                    LockButtons();
                    modDeinstallBusy = true;
                    mod.DeinstallMod();
                }
                else if (mod.installQueued && !mod.isWorking && !modInstallBusy)
                {
                    LockButtons();
                    modInstallBusy = true;
                    mod.InstallMod();
                }

                if (mod.downloadBusy)
                {
                    allDownloadDone = false;
                }
                if (mod.installBusy)
                {
                    allInstallDone = false;
                }
                if (mod.deinstallBusy)
                {
                    allDeinstallDone = false;
                }
                if (mod.checkQueued || mod.downloadQueued || mod.findQueued || mod.installQueued || mod.deinstallQueued)
                {
                    allWorkDone = false;
                }

                if (mod.updateList)
                {
                    mod.updateList = false;

                    if (selectedModIndex == i)
                    {
                        SetModFields();
                    }

                    listViewMods.Items[i].SubItems[1].Text = mod.category;
                    listViewMods.Items[i].SubItems[2].Text = mod.uriState;
                    listViewMods.Items[i].SubItems[3].Text = mod.versionLocalNumeric;
                    listViewMods.Items[i].SubItems[4].Text = mod.versionLatestNumeric;

                    listViewMods.Items[i].SubItems[6].Text = mod.website1;
                    listViewMods.Items[i].SubItems[7].Text = mod.website2;
                    listViewMods.Items[i].SubItems[8].Text = mod.website3;
                    listViewMods.Items[i].SubItems[9].Text = mod.website4;
                    listViewMods.Items[i].SubItems[10].Text = mod.website5;
                }
            }

            if (allDownloadDone)
            {
                modDownloadBusy = false;
            }
            if (allInstallDone)
            {
                modInstallBusy = false;
            }
            if (allDeinstallDone)
            {
                modDeinstallBusy = false;
            }
            if (allWorkDone && action)
            {
                ReloadModlistview();
                UnlockButtons();
            }
        }

        private void LockButtons()
        {
            action = true;
            buttonAddFolder.Enabled = false;
            buttonAddKsp.Enabled = false;
            buttonCheckAll.Enabled = false;
            buttonCheckSelected.Enabled = false;
            buttonDeinstallAll.Enabled = false;
            buttonDeleteFolder.Enabled = false;
            buttonDeleteKsp.Enabled = false;
            buttonDeleteSelected.Enabled = false;
            buttonFindAll.Enabled = false;
            buttonFindSelected.Enabled = false;
            buttonInstallSelected.Enabled = false;
            buttonReinstallAll.Enabled = false;
            buttonReinstallSelected.Enabled = false;
            buttonSelectAllFolders.Enabled = false;
            buttonUpdateAll.Enabled = false;
            buttonUpdateAllForce.Enabled = false;
            buttonUpdateSelected.Enabled = false;
            listBoxModFolders.Enabled = false;
            listBoxKspFolders.Enabled = false;
        }
        private void UnlockButtons()
        {
            action = false;
            buttonAddFolder.Enabled = true;
            buttonAddKsp.Enabled = true;
            buttonCheckAll.Enabled = true;
            buttonCheckSelected.Enabled = true;
            buttonDeinstallAll.Enabled = true;
            buttonDeleteFolder.Enabled = true;
            buttonDeleteKsp.Enabled = true;
            buttonDeleteSelected.Enabled = true;
            buttonFindAll.Enabled = true;
            buttonFindSelected.Enabled = true;
            buttonInstallSelected.Enabled = true;
            buttonReinstallAll.Enabled = true;
            buttonReinstallSelected.Enabled = true;
            buttonSelectAllFolders.Enabled = true;
            buttonUpdateAll.Enabled = true;
            buttonUpdateAllForce.Enabled = true;
            buttonUpdateSelected.Enabled = selectedMod.canUpdate;
            listBoxModFolders.Enabled = true;
            listBoxKspFolders.Enabled = true;
        }

        // Perform mod actions
        private void buttonFindSelected_Click(object sender, EventArgs e)
        {
            selectedMod.findQueued = true;
        }

        private void buttonCheckSelected_Click(object sender, EventArgs e)
        {
            selectedMod.checkQueued = true;
        }

        private void buttonUpdateSelected_Click(object sender, EventArgs e)
        {
            if (selectedMod.canUpdate)
            {
                selectedMod.downloadQueued = true;
            }
        }

        private void buttonFindAll_Click(object sender, EventArgs e)
        {
            foreach (ModInfo mod in modFolders.mods)
            {
                if (mod.uriState == "")
                {
                    mod.findQueued = true;
                }
            }
        }

        private void buttonCheckAll_Click(object sender, EventArgs e)
        {
            foreach (ModInfo mod in modFolders.mods)
            {
                if (mod.uriState != "")
                {
                    mod.checkQueued = true;
                }
            }
        }

        private void buttonUpdateAll_Click(object sender, EventArgs e)
        {
            foreach (ModInfo mod in modFolders.mods)
            {
                if (mod.canUpdate)
                {
                    mod.downloadQueued = true;
                }
            }
        }

        private void buttonUpdateAllForce_Click(object sender, EventArgs e)
        {
            foreach (ModInfo mod in modFolders.mods)
            {
                if (mod.uriState != "")
                {
                    mod.checkQueued = true;
                    mod.downloadQueued = true;
                }
            }
        }

        private void buttonDeleteSelected_Click(object sender, EventArgs e)
        {
            modFolders.RemoveMod(selectedMod.modKey);
            try
            {
                selectedMod = modFolders.mods[selectedModIndex];
            }
            catch
            {
                try
                {
                    selectedMod = modFolders.mods[selectedModIndex - 1];
                }
                catch
                { }
            }
            ReloadModlistview();
        }

        private void buttonInstallSelected_Click(object sender, EventArgs e)
        {
            if (selectedMod.isInstalled)
            {
                selectedMod.deinstallQueued = true;
            }
            else
            {
                selectedMod.installQueued = true;
            }
        }

        private void buttonReinstallSelected_Click(object sender, EventArgs e)
        {
            if (selectedMod.isInstalled)
            {
                selectedMod.deinstallQueued = true;
                selectedMod.installQueued = true;
            }
        }

        private void buttonDeinstallAll_Click(object sender, EventArgs e)
        {
            foreach (ModInfo mod in modFolders.mods)
            {
                if (mod.isInstalled)
                {
                    mod.deinstallQueued = true;
                }
            }
        }

        private void buttonReinstallAll_Click(object sender, EventArgs e)
        {
            foreach (ModInfo mod in modFolders.mods)
            {
                if (mod.isInstalled)
                {
                    mod.deinstallQueued = true;
                    mod.installQueued = true;
                }
            }
        }

        private void buttonInstallAll_Click(object sender, EventArgs e)
        {
            foreach (ModInfo mod in modFolders.mods)
            {
                if (!mod.isInstalled)
                {
                    mod.installQueued = true;
                }
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
