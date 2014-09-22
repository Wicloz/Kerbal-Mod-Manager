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

        public CurrentInstance install = new CurrentInstance();
        private InstallMod im = new InstallMod();
        private DeinstallMod dm = new DeinstallMod();
        private UpdateMod um = new UpdateMod();
        private UpdateCheck uc = new UpdateCheck();

        private string settingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KMM\\settings";
        public string modsPath = "";

        public List<ModInfo> modList;
        private List<SiteInfo> siteList;

        private List<InstallInstance> instanceList;
        private int selectedInstance = 0;

        public Main()
        {
            InitializeComponent();
            acces = this;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(settingFolder);

            if(File.Exists(settingFolder + "\\settings.txt"))
            {
                Settings settings = SaveLoad.LoadFileXml<Settings>(settingFolder + "\\settings.txt");
                modsPath = settings.modsPath;
                instanceList = settings.instances;
                selectedInstance = settings.selectedInstance;

                modFolderBox.Text = modsPath;
            }
            else
            {
                instanceList = new List<InstallInstance>();
            }

            // Handle instance list here

            install.LoadInstance(@"D:\KerbalSpaceProgram\Testing\KSP_win");
            OnModFolderChange();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetupModFolder();
            SaveFiles();
            install.UnloadInstance();
        }

        private void SaveFiles()
        {
            SaveLoad.SaveFileXml(new Settings(modsPath, instanceList, selectedInstance), settingFolder + "\\settings.txt");
            if (modsPath != "")
            {
                SaveLoad.SaveFileXml(modList, modsPath + "\\ModList.txt");
                SaveLoad.SaveFileXml(siteList, modsPath + "\\SiteList.txt");
            }
        }

        private void SetupModFolder()
        {
            Directory.CreateDirectory(modsPath + "\\Overrides");

            if (Directory.Exists(modsPath + "\\ModDownloads"))
            {
                Directory.Delete(modsPath + "\\ModDownloads", true);
            }

            if (Directory.Exists(modsPath + "\\SiteDownload"))
            {
                Directory.Delete(modsPath + "\\SiteDownload", true);
            }
        }

        private void OnModFolderChange()
        {
            if (File.Exists(modsPath + "\\ModList.txt"))
            {
                modList = SaveLoad.LoadFileXml<List<ModInfo>>(modsPath + "\\ModList.txt");
            }
            else
            {
                modList = new List<ModInfo>();
            }

            if (File.Exists(modsPath + "\\SiteList.txt"))
            {
                siteList = SaveLoad.LoadFileXml<List<SiteInfo>>(modsPath + "\\SiteList.txt");
            }
            else
            {
                siteList = new List<SiteInfo>();
            }

            if (modsPath != "")
            {
                SetupModFolder();
                ManageModLists();
            }
            SaveFiles();

            // Handle mod list here
        }

        private void ManageModLists()
        {
            // Handle extra zip files
            foreach (string file in Directory.GetFiles(modsPath, "*.zip", SearchOption.AllDirectories))
            {
                if (!file.Contains("ModDownloads"))
                {
                    bool exists = false;

                    foreach (ModInfo info in modList)
                    {
                        if (info.zipfile == file.Replace(modsPath + "\\", ""))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        string zipName = file.Replace(modsPath + "\\", "");
                        modList.Add(new ModInfo(zipName));
                    }
                }
            }

            // Handle missing zip files
            for (int i = 0; i < modList.Count; i++)
            {
                if (!modList[i].zipfile.Contains("Overrides"))
                {
                    if (!File.Exists(modsPath + "\\" + modList[i].zipfile))
                    {
                        modList[i].zipfile = "none";
                    }
                }

                if (modList[i].zipfile.Contains("Overrides"))
                {
                    if (!File.Exists(modsPath + "\\" + modList[i].zipfile))
                    {
                        modList.RemoveAt(i);
                        i--;
                    }
                }
            }

            // Manage Site List
            foreach (ModInfo mod in modList)
            {
                if (!mod.zipfile.Contains("Overrides"))
                {
                    bool exists = false;

                    if (mod.zipfile == "none" && (mod.key == "none" || mod.key == "" || mod.key == null))
                    {
                        mod.key = Functions.CleanName(mod.name);
                    }
                    else if (mod.key == "none" || mod.key == "" || mod.key == null)
                    {
                        mod.key = Functions.CleanName(mod.zipfile);
                    }

                    foreach (SiteInfo site in siteList)
                    {
                        if (site.key == mod.key)
                        {
                            exists = true;

                            if (site.website.Contains("http://kerbal.curseforge.com"))
                            {
                                site.dlSite = site.website + "/files/latest";
                            }

                            mod.websites = site;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        siteList.Add(new SiteInfo(mod.key, "NONE"));
                    }
                }
            }
        }

        public void Reinstall(string installedModName, ModInfo info)
        {
            dm.Deinstall(installedModName);
            im.Install(info);
        }

        public void SortLists()
        {
            install.installedModList.Sort();
            modList.Sort();
        }

        public void LogMessage(string message)
        {
        }

        private void selectModFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                modFolderBox.Text = fbd.SelectedPath;
                modsPath = fbd.SelectedPath;
                OnModFolderChange();
            }
        }
    }
}
