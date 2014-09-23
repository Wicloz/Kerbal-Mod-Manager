using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KSP_Mod_Manager
{
    public class CurrentKspInstance
    {
        public string kspFolder = "";

        public List<InstallInfo> installedModList;
        public List<FileInfo> installedFileList;
        public List<FavInfo> favoritesList;

        private bool loaded = false;

        public void LoadInstance(string path)
        {
            kspFolder = path;

            if (File.Exists(path + "\\KMM\\Favorites.txt"))
            {
                favoritesList = SaveLoad.LoadFileXml<List<FavInfo>>(path + "\\KMM\\Favorites.txt");
            }
            else
            {
                favoritesList = new List<FavInfo>();
            }

            if (File.Exists(path + "\\KMM\\FileList.txt"))
            {
                installedFileList = SaveLoad.LoadFileXml<List<FileInfo>>(path + "\\KMM\\FileList.txt");
            }
            else
            {
                installedFileList = new List<FileInfo>();
            }

            if (File.Exists(path + "\\KMM\\ModList.txt"))
            {
                installedModList = SaveLoad.LoadFileXml<List<InstallInfo>>(path + "\\KMM\\ModList.txt");
            }
            else
            {
                installedModList = new List<InstallInfo>();
            }

            if (kspFolder != "")
            {
                SetupKspFolder(kspFolder);
                ManageFileList(kspFolder);
                ManageFavoriteList();
                SaveFiles(kspFolder);
            }

            loaded = true;
        }

        public void UnloadInstance()
        {
            if (kspFolder != "" && loaded)
            {
                SetupKspFolder(kspFolder);
                ManageFileList(kspFolder);
                ManageFavoriteList();
                SaveFiles(kspFolder);
            }

            kspFolder = "";
        }

        private void SetupKspFolder(string path)
        {
            Directory.CreateDirectory(path + "\\KMM\\overrides");

            if (Directory.Exists(path + "\\KMM\\temp"))
            {
                Directory.Delete(path + "\\KMM\\temp", true);
            }
        }

        private void SaveFiles(string path)
        {
            SaveLoad.SaveFileXml(favoritesList, path + "\\KMM\\Favorites.txt");
            SaveLoad.SaveFileXml(installedFileList, path + "\\KMM\\FileList.txt");
            SaveLoad.SaveFileXml(installedModList, path + "\\KMM\\ModList.txt");
        }

        public void ManageFileList(string path)
        {
            string fileFolder = path + "\\GameData";
            string[] files = Directory.GetFiles (fileFolder, "*.*", SearchOption.AllDirectories);

			// Add to file list
			for (int i = 0; i < files.Length; i++)
			{
                string file1 = files[i].Replace(path, "");
                bool exists = false;

                foreach (FileInfo file2 in installedFileList)
                {
                    if (file1 == file2.path)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    AddToFileList(new FileInfo(file1, "default"));
                }
			}

            // Remove from file list
            for (int i = 0; i < installedFileList.Count; i++)
            {
                if (!File.Exists(path + installedFileList[i].path))
                {
                    installedFileList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ManageFavoriteList()
        {
            // Manage Favorite List
            foreach (ModInfo mod in Main.acces.modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides"))
                {
                    bool exists = false;

                    foreach (FavInfo fav in favoritesList)
                    {
                        if (fav.key == mod.key)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        favoritesList.Add(new FavInfo(mod.key));
                    }
                }
            }
        }

        public void AddToFileList(FileInfo fileInfo)
        {
            for (int i = 0; i < installedFileList.Count; i++)
            {
                if (installedFileList[i].path == fileInfo.path)
                {
                    installedFileList[i] = fileInfo;
                    return;
                }
            }

            installedFileList.Add(fileInfo);
        }
    }
}
