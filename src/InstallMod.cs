using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Diagnostics;

namespace KSP_Mod_Manager
{
    class InstallMod
    {
        public void Install(ModInfo modInfo)
        {
            try
            {
                Directory.Delete(Main.acces.kspInfo.kspFolder + "\\KMM\\temp", true);
            }
            catch
            { }

            // Start
            string codeName = modInfo.zipfile.Replace(".zip", "");

            string tempExtractLocation = Main.acces.kspInfo.kspFolder + "\\KMM\\temp";
            Directory.CreateDirectory(tempExtractLocation);

            // Extract
            try
            {
                ZipFile.ExtractToDirectory(Main.acces.modInfo.modsPath + "\\" + modInfo.zipfile, Main.acces.kspInfo.kspFolder + "\\KMM\\temp");
            }
            catch
            {
                Main.acces.LogMessage("Extraction failed, aborting installation of '" + modInfo.name + "'!");

                Directory.Delete(tempExtractLocation, true);
                Functions.ProcessDirectory(Main.acces.kspInfo.kspFolder + "\\KMM\\overrides", false);
            }

            // Checking for GameData mode
            List<string> dirListTop = new List<string>();
            foreach (string directory in Directory.GetDirectories(tempExtractLocation, "*.*", SearchOption.TopDirectoryOnly))
            {
                dirListTop.Add(directory);
            }

            List<string> dirListAll = new List<string>();
            foreach (string directory in Directory.GetDirectories(tempExtractLocation, "*.*", SearchOption.AllDirectories))
            {
                dirListAll.Add(directory);
            }

            string mode = "noGameData";

            foreach (string entry in dirListTop)
            {
                if (entry.Contains("GameData") || entry.Contains("Gamedata"))
                {
                    mode = "";
                    break;
                }
            }

            if (mode == "noGameData")
            {
                foreach (string entry in dirListAll)
                {
                    if (entry.Contains("GameData") || entry.Contains("Gamedata"))
                    {
                        mode = "embeddedGameData";

                        foreach (string file in Directory.GetFiles(tempExtractLocation))
                        {
                            File.Delete(file);
                        }

                        break;
                    }
                }
            }

            // Create file list
            List<string> oldFilePaths = new List<string>();
            List<FileInfo> fileList = new List<FileInfo>();

            foreach (string file in Directory.GetFiles(tempExtractLocation, "*.*", SearchOption.AllDirectories))
            {
                oldFilePaths.Add(file);

                if (mode == "")
                {
                    fileList.Add(new FileInfo(file.Replace(tempExtractLocation, ""), codeName));
                }

                else if (mode == "noGameData")
                {
                    fileList.Add(new FileInfo("\\GameData" + file.Replace(tempExtractLocation, ""), codeName));
                }

                else if (mode == "embeddedGameData")
                {
                    fileList.Add(new FileInfo(file.Replace(Directory.GetDirectories(tempExtractLocation)[0], ""), codeName));
                }
            }

            // Cleanup file list
            for (int i = 0; i < fileList.Count; i++)
            {
                fileList[i].path = fileList[i].path.Replace("Gamedata", "GameData");
                fileList[i].path = fileList[i].path.Replace("gamedata", "GameData");

                if (!Path.GetDirectoryName(fileList[i].path).StartsWith("\\GameData") && !Path.GetDirectoryName(fileList[i].path).StartsWith("\\KSP_Data") && !Path.GetDirectoryName(fileList[i].path).StartsWith("\\KSP_x64_Data") && Path.GetExtension(fileList[i].path) != ".exe")
                {
                    oldFilePaths.RemoveAt(i);
                    fileList.RemoveAt(i);
                    i--;
                }

                else if (Path.GetDirectoryName(fileList[i].path).Replace("\\", "") == "GameData" && Path.GetExtension(fileList[i].path) != ".dll" && Path.GetExtension(fileList[i].path) != ".dat" && Path.GetExtension(fileList[i].path) != ".cfg")
                {
                    oldFilePaths.RemoveAt(i);
                    fileList.RemoveAt(i);
                    i--;
                }
            }

            // Removing extra MM.dll's
            if (codeName.Contains("ModuleManager") && !codeName.Contains("Override"))
            {
                for (int i = 0; i < Main.acces.kspInfo.installedFileList.Count; i++)
                {
                    if (Main.acces.kspInfo.installedFileList[i].path.Contains("ModuleManager"))
                    {
                        Main.acces.LogMessage("Overriding file '" + Main.acces.kspInfo.installedFileList[i].path + "'.");

                        string newPath = Main.acces.kspInfo.kspFolder + "\\KMM\\overrides\\" + codeName.Replace("\\", "()") + "\\" + Main.acces.kspInfo.installedFileList[i].modName.Replace("\\", "()") + Main.acces.kspInfo.installedFileList[i].path;

                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                        File.Move(Main.acces.kspInfo.kspFolder + Main.acces.kspInfo.installedFileList[i].path, newPath);

                        Main.acces.kspInfo.installedFileList.RemoveAt(i);
                        i--;
                    }
                }
            }

            for (int i = 0; i < fileList.Count; i++)
            {
                string oldFilePath = oldFilePaths[i];
                string newFilePath = Main.acces.kspInfo.kspFolder + fileList[i].path;

                // Backup overridden files
                if (!modInfo.name.Contains("override") && File.Exists(newFilePath))
                {
                    FileInfo overriddenFile = new FileInfo();
                    foreach (FileInfo file in Main.acces.kspInfo.installedFileList)
                    {
                        if (file.path == fileList[i].path)
                        {
                            overriddenFile = file;
                            break;
                        }
                    }

                    if (overriddenFile.modName != null)
                    {
                        Main.acces.LogMessage("Overriding file '" + fileList[i].path + "'.");

                        string newPath = Main.acces.kspInfo.kspFolder + "\\KMM\\overrides\\" + codeName + "\\" + overriddenFile.modName.Replace("\\", "()") + fileList[i].path;
                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                        File.Move(newFilePath, newPath);
                    }
                }

                // Move files
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newFilePath));
                    File.Delete(newFilePath);
                    File.Move(oldFilePath, newFilePath);
                    Main.acces.kspInfo.AddToFileList(fileList[i]);
                }
                catch
                {
                    Main.acces.LogMessage("Cannot move file '" + newFilePath + "', skipping ...");
                }
            }

            // Add entry to installed list
            Main.acces.kspInfo.installedModList.Add(new InstalledInfo(modInfo.name, modInfo.category, modInfo.key, codeName, modInfo.version));

            // Finalise
            if (modInfo.name.Contains("DMP"))
            {
                Process dmpUpdater = new System.Diagnostics.Process();
                try
                {
                    dmpUpdater.StartInfo.FileName = Directory.GetFiles(Main.acces.kspInfo.kspFolder, "DMPUpdater.exe", SearchOption.TopDirectoryOnly)[0];
                    dmpUpdater.StartInfo.Arguments = "--batch";
                    dmpUpdater.Start();
                }
                catch
                { }
            }

            Directory.Delete(tempExtractLocation, true);
            Functions.ProcessDirectory(Main.acces.kspInfo.kspFolder + "\\KMM\\overrides", false);
        }
    }
}
