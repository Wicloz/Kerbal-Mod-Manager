using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kerbal_Mod_Manager
{
    public class KspFolder
    {
        private List<InstalledModInfo> installedMods = new List<InstalledModInfo>();
        private List<FileInfo> files = new List<FileInfo>();
        public string kspFolder;
        public string kmmFolder
        {
            get
            {
                return kspFolder + "\\KMM";
            }
        }

        public InstalledModInfo GetInstalledMod(string modKey)
        {
            foreach (InstalledModInfo mod in installedMods)
            {
                if (modKey == mod.modKey)
                {
                    return mod;
                }
            }
            return null;
        }
        public bool IsModInstalled(string modKey)
        {
            foreach (InstalledModInfo mod in installedMods)
            {
                if (modKey == mod.modKey)
                {
                    return true;
                }
            }
            return false;
        }

        // Loading and saving of instances
        public void ChangeInstance(string path)
        {
            SaveInstance();
            installedMods = new List<InstalledModInfo>();
            files = new List<FileInfo>();
            LoadInstance(path);
        }

        private void LoadInstance(string path)
        {
            kspFolder = path;
            if (!String.IsNullOrWhiteSpace(kspFolder))
            {
                if (File.Exists(kmmFolder + "\\files.dat"))
                {
                    files = SaveLoad.LoadFileXml<List<FileInfo>>(kmmFolder + "\\files.dat");
                }
                if (File.Exists(kmmFolder + "\\mods.dat"))
                {
                    installedMods = SaveLoad.LoadFileXml<List<InstalledModInfo>>(kmmFolder + "\\mods.dat");
                }
                InitialiseInstance();
            }
            SaveInstance();
        }

        private void InitialiseInstance()
        {
            foreach (string file in Directory.GetFiles(kspFolder + "\\GameData", "*.*", SearchOption.AllDirectories))
            {
                string relFile = file.Replace(kspFolder + "\\", "");

                bool exists = false;
                foreach (FileInfo listFile in files)
                {
                    if (listFile.relativeFilePath == relFile)
                    {
                        exists = true;
                        listFile.kspFolder = kspFolder;
                        break;
                    }
                }

                if (!exists)
                {
                    string modFolder = relFile.Replace("GameData\\", "").Split(new char[] { '\\' })[0];
                    if (!relFile.Contains("GameData\\") || string.IsNullOrWhiteSpace(modFolder))
                    {
                        modFolder = "default";
                    }

                    files.Add(new FileInfo(kspFolder, relFile, MiscFunctions.CleanString(modFolder), modFolder));
                }
            }

            List<string> modKeys = new List<string>();
            foreach (InstalledModInfo mod in installedMods)
            {
                modKeys.Add(mod.modKey);
            }

            for (int i = 0; i < files.Count; i++)
            {
                if (!files[i].FileExists())
                {
                    files.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (!modKeys.Contains(files[i].currentMod))
                    {
                        installedMods.Add(new InstalledModInfo(files[i].initValue, files[i].currentMod));
                        modKeys.Add(files[i].currentMod);
                    }
                }
            }
        }

        public void SaveInstance()
        {
            if (!String.IsNullOrWhiteSpace(kspFolder))
            {
                InitialiseInstance();
                Directory.CreateDirectory(kmmFolder);
                SaveLoad.SaveFileXml(files, kmmFolder + "\\files.dat");
                SaveLoad.SaveFileXml(installedMods, kmmFolder + "\\mods.dat");
            }
        }

        // Installing and deinstalling
        public void AddFile(string relFilePath, string modKey, string modName)
        {
            FileInfo file = new FileInfo(kspFolder, relFilePath, modKey, modName);

            foreach (FileInfo installedFile in files)
            {
                if (installedFile.relativeFilePath.ToLower() == file.relativeFilePath.ToLower())
                {
                    installedFile.OverWriteWith(file.currentMod);
                    return;
                }
            }

            files.Add(file);
        }

        public void AddMod(ModInfo mod)
        {
            InstalledModInfo installMod = new InstalledModInfo(mod.modName, mod.modKey);
            installMod.versionInstalledNumeric = mod.versionLocalNumeric;
            installMod.versionInstalledRaw = mod.versionLocalRaw;

            installedMods.Add(installMod);
        }

        public void DeinstallMod(string modKey)
        {
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].RemoveMod(modKey))
                {
                    files.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < installedMods.Count; i++)
            {
                if (installedMods[i].modKey == modKey)
                {
                    installedMods.RemoveAt(i);
                    i--;
                }
            }

            if (Directory.Exists(kmmFolder + "\\Overrides"))
            {
                foreach (string folder in Directory.GetDirectories(kmmFolder + "\\Overrides"))
                {
                    if (folder.ToLower().EndsWith(modKey))
                    {
                        Directory.Delete(folder, true);
                    }
                }
                MiscFunctions.ProcessDirectory(kmmFolder + "\\Overrides", true);
            }

            MiscFunctions.ProcessDirectory(kspFolder + "\\GameData", false);
        }
    }

    [Serializable]
    public class FileInfo
    {
        public string initValue;
        public string kspFolder;
        public string relativeFilePath;
        public string absoluteFilePath
        {
            get
            {
                return kspFolder + "\\" + relativeFilePath;
            }
        }
        private string GetOverwriteFile(string modKey)
        {
            return kspFolder + "\\KMM\\Overrides\\" + modKey + "\\" + relativeFilePath;
        }

        public string currentMod;
        public List<string> otherMods = new List<string>();

        public FileInfo()
        { }

        public FileInfo(string kspFolder, string relFilePath, string modKey, string modName)
        {
            this.kspFolder = kspFolder;
            relativeFilePath = relFilePath;
            currentMod = modKey;
            initValue = modName;
        }

        public void OverWriteWith(string modKey)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(GetOverwriteFile(currentMod)));
            File.Move(absoluteFilePath, GetOverwriteFile(currentMod));
            otherMods.Insert(0, currentMod);
            currentMod = modKey;
        }

        public bool RemoveMod(string modKey)
        {
            if (currentMod == modKey)
            {
                File.Delete(absoluteFilePath);

                if (otherMods.Count == 0)
                {
                    return true;
                }
                else
                {
                    RestoreOverwrite();
                    return false;
                }
            }

            else if (otherMods.Contains(modKey))
            {
                otherMods.Remove(modKey);
                return false;
            }
            else
            {
                return false;
            }
        }

        private void RestoreOverwrite()
        {
            string newMod = otherMods[0];
            File.Move(GetOverwriteFile(newMod), absoluteFilePath);
            currentMod = newMod;
            otherMods.RemoveAt(0);
        }

        public bool FileExists()
        {
            return File.Exists(absoluteFilePath);
        }
    }
}
