using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kerbal_Mod_Manager
{
    public class ModFolder
    {
        private List<string> folders = new List<string>();
        private List<ModList> modLists = new List<ModList>();

        // Get and set mods
        public List<ModInfo> mods
        {
            get
            {
                List<ModInfo> retList = new List<ModInfo>();
                foreach (ModList list in modLists)
                {
                    retList.AddRange(list.modList);
                }
                retList.Sort();
                return retList;
            }
        }

        public void RemoveMod(string modKey)
        {
            foreach (ModList modList in modLists)
            {
                for (int i = 0; i < modList.modList.Count; i++)
                {
                    if (modList.modList[i].modKey == modKey)
                    {
                        File.Delete(modList.modList[i].modFolder + "\\" + modList.modList[i].currentFileName);
                        modList.modList.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        // Loading and saving of folders
        public void ChangeFolders(List<string> folders)
        {
            SaveFolders();
            this.folders = folders;
            LoadFolders();
        }

        private void LoadFolders()
        {
            modLists.Clear();
            foreach (string folder in folders)
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                ModList list;
                if (File.Exists(folder + "\\modlist.lst"))
                {
                    list = SaveLoad.LoadFileBf<ModList>(folder + "\\modlist.lst");
                    list.folderPath = folder;
                }
                else
                {
                    list = new ModList(folder);
                }

                InitialiseFolder(list);
                list.modList.Sort();
                modLists.Add(list);
            }
            SaveFolders();
        }

        private void InitialiseFolder(ModList modList)
        {
            List<string> fileList = new List<string>(Directory.GetFiles(modList.folderPath, "*.zip", SearchOption.TopDirectoryOnly));

            foreach (string filePath in fileList)
            {
                string file = Path.GetFileName(filePath);
                bool exists = false;

                foreach (ModInfo mod in modList.modList)
                {
                    if (mod.currentFileName == file)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    modList.modList.Add(new ModInfo(file));
                }
            }

            for (int i = 0; i < modList.modList.Count; i++)
            {
                bool exists = false;
                foreach (string filePath in fileList)
                {
                    string file = Path.GetFileName(filePath);
                    if (modList.modList[i].currentFileName == file)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    modList.modList.RemoveAt(i);
                    i--;
                }
                else
                {
                    modList.modList[i].modFolder = modList.folderPath;
                    modList.modList[i].UpdateModValues();
                }
            }
        }

        public void SaveFolders()
        {
            foreach (ModList list in modLists)
            {
                SaveLoad.SaveFileBf(list, list.folderPath + "\\modlist.lst");
            }
        }
    }

    [Serializable]
    class ModList
    {
        public string folderPath = "";
        public List<ModInfo> modList = new List<ModInfo>();

        public ModList()
        { }

        public ModList(string folder)
        {
            folderPath = folder;
        }
    }
}
