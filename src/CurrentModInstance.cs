using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KSP_Mod_Manager
{
    public class CurrentModInstance
    {
        public string modsPath = "";

        public List<ModInfo> modList = new List<ModInfo>();
        public List<SiteInfo> siteList = new List<SiteInfo>();

        public bool loaded = false;

        public void LoadInstance(string path)
        {
            modsPath = path;

            LoadFiles(modsPath);

            if (modsPath.Contains(":\\"))
            {
                SetupModFolder(modsPath);
                ManageModLists(modsPath);
                SaveFiles(modsPath);

                loaded = true;
            }
            else
            {
                loaded = false;
            }
        }

        public void UnloadInstance()
        {
            if (loaded)
            {
                SetupModFolder(modsPath);
                ManageModLists(modsPath);
                SaveFiles(modsPath);
            }

            modsPath = "";
        }

        public void LoadFiles(string path)
        {
            if (File.Exists(modsPath + "\\ModList.txt"))
            {
                modList = SaveLoad.LoadFileBf<List<ModInfo>>(path + "\\ModList.txt");
            }
            else
            {
                modList = new List<ModInfo>();
            }

            if (File.Exists(modsPath + "\\SiteList.txt"))
            {
                siteList = SaveLoad.LoadFileBf<List<SiteInfo>>(path + "\\SiteList.txt");
            }
            else
            {
                siteList = new List<SiteInfo>();
            }
        }

        public void SaveFiles(string path)
        {
            if (loaded)
            {
                SaveLoad.SaveFileBf(modList, path + "\\ModList.txt");
                SaveLoad.SaveFileBf(siteList, path + "\\SiteList.txt");
            }
        }

        private void SetupModFolder(string path)
        {
            Directory.CreateDirectory(path + "\\Overrides");

            if (Directory.Exists(path + "\\SiteDownload"))
            {
                Directory.Delete(path + "\\SiteDownload", true);
            }

            if (Directory.Exists(path + "\\ModDownloads"))
            {
                Functions.ProcessDirectory(path + "\\ModDownloads", true);
            }
        }

        private void ManageModLists(string path)
        {
            // Handle extra zip files
            foreach (string file in Directory.GetFiles(path, "*.zip", SearchOption.AllDirectories))
            {
                if (!file.Contains("ModDownloads"))
                {
                    bool exists = false;

                    foreach (ModInfo info in modList)
                    {
                        if (info.zipfile == file.Replace(path + "\\", ""))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        string zipName = file.Replace(path + "\\", "");
                        modList.Add(new ModInfo(zipName));
                    }
                }
            }

            // Handle missing zip files
            for (int i = 0; i < modList.Count; i++)
            {
                if (!modList[i].zipfile.Contains("Overrides"))
                {
                    if (!File.Exists(path + "\\" + modList[i].zipfile))
                    {
                        modList[i].zipfile = "none";
                    }
                }

                if (modList[i].zipfile.Contains("Overrides"))
                {
                    if (!File.Exists(path + "\\" + modList[i].zipfile))
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

                    foreach (SiteInfo site in siteList)
                    {
                        if (site.key == mod.key)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        siteList.Add(new SiteInfo(mod.key, "NONE"));
                    }
                }
            }

            foreach (ModInfo mod in modList)
            {
                mod.ManageMod();
            }
        }
    }
}
