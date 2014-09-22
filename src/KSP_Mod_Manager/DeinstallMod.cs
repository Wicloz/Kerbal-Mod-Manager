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

namespace KSP_Mod_Manager
{
    class DeinstallMod
    {
        public void Deinstall(string modName)
        {
            // Check for overrides
            for (int i = 0; i < Main.acces.install.installedModList.Count; i++)
            {
                string name = Main.acces.install.installedModList[i].codeName;

                if (Functions.CleanName(name) == Functions.CleanName("Overrides\\" + modName.Replace(" ", "")))
                {
                    Deinstall(name);
                }
            }

            // Start
            Main.acces.LogMessage("Deinstalling '" + modName + "'.");
            string overrideFolder = Main.acces.install.kspFolder + "\\KMM\\overrides\\" + modName.Replace("\\", "()");

            // Restore overridden files
            if (Directory.Exists(overrideFolder))
            {
                foreach (string folder in Directory.GetDirectories(overrideFolder))
                {
                    foreach (string file in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
                    {
                        string oldFileName = file;
                        string newFileName = Main.acces.install.kspFolder + file.Replace(folder, "");

                        if (File.Exists(newFileName))
                        {
                            Main.acces.LogMessage("Restoring file '" + newFileName + "'.");

                            File.Delete(newFileName);
                            File.Move(oldFileName, newFileName);

                            Main.acces.install.AddToFileList(new FileInfo(newFileName.Replace(Main.acces.install.kspFolder, ""), folder.Replace(overrideFolder + "\\", "").Replace("()", "\\")));
                        }
                    }
                }
            }

            try
            {
                Directory.Delete(overrideFolder, true);
            }
            catch
            { }

            // Delete all override folders
            foreach (string folder1 in Directory.GetDirectories(Main.acces.install.kspFolder + "\\KMM\\overrides"))
            {
                foreach (string folder2 in Directory.GetDirectories(folder1))
                {
                    if (folder2.Replace("()", "\\").Replace(folder1 + "\\", "") == modName)
                    {
                        Directory.Delete(folder2, true);
                    }
                }
            }

            // Delete files in the list
            for (int i = 0; i < Main.acces.install.installedFileList.Count; i++)
            {
                string fileName = Main.acces.install.kspFolder + Main.acces.install.installedFileList[i].path;

                if (Main.acces.install.installedFileList[i].modName == modName)
                {
                    try
                    {
                        File.Delete(fileName);
                        Main.acces.install.installedFileList.RemoveAt(i);
                        i--;
                    }
                    catch
                    {
                        Main.acces.LogMessage("File '" + fileName + "' could not be deleted, skipping ...");
                    }
                }
            }

            // Remove entry from installed list
            for (int i = 0; i < Main.acces.install.installedModList.Count; i++)
            {
                if (modName == Main.acces.install.installedModList[i].codeName)
                {
                    Main.acces.install.installedModList.RemoveAt(i);
                    break;
                }
            }

            // Finalise
            Functions.ProcessDirectory(Main.acces.install.kspFolder + "\\GameData", false);
            Functions.ProcessDirectory(Main.acces.install.kspFolder + "\\KMM\\overrides", false);

            Main.acces.SortLists();
        }
    }
}
