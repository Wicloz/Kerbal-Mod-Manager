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
    class DeinstallEvents
    {
        public void Deinstall(string zipName)
        {
            // Start
            string overrideFolder = Main.acces.kspInfo.kspFolder + "\\KMM\\overrides\\" + zipName.Replace("\\", "()");

            // Restore overridden files
            if (Directory.Exists(overrideFolder))
            {
                foreach (string folder in Directory.GetDirectories(overrideFolder))
                {
                    foreach (string file in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
                    {
                        string oldFileName = file;
                        string newFileName = Main.acces.kspInfo.kspFolder + file.Replace(folder, "");

                        if (File.Exists(newFileName))
                        {
                            Main.acces.LogMessage("Restoring file '" + newFileName + "'.");

                            File.Delete(newFileName);
                            File.Move(oldFileName, newFileName);

                            Main.acces.kspInfo.AddToFileList(new FileInfo(newFileName.Replace(Main.acces.kspInfo.kspFolder, ""), folder.Replace(overrideFolder + "\\", "").Replace("()", "\\")));
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
            foreach (string folder1 in Directory.GetDirectories(Main.acces.kspInfo.kspFolder + "\\KMM\\overrides"))
            {
                foreach (string folder2 in Directory.GetDirectories(folder1))
                {
                    if (folder2.Replace("()", "\\").Replace(folder1 + "\\", "") == zipName)
                    {
                        Directory.Delete(folder2, true);
                    }
                }
            }

            // Delete files in the list
            for (int i = 0; i < Main.acces.kspInfo.installedFileList.Count; i++)
            {
                string fileName = Main.acces.kspInfo.kspFolder + Main.acces.kspInfo.installedFileList[i].path;

                if (Main.acces.kspInfo.installedFileList[i].modName == zipName)
                {
                    try
                    {
                        File.Delete(fileName);
                        Main.acces.kspInfo.installedFileList.RemoveAt(i);
                        i--;
                    }
                    catch
                    {
                        Main.acces.LogMessage("File '" + fileName + "' could not be deleted, skipping ...");
                    }
                }
            }

            // Remove entry from installed list
            for (int i = 0; i < Main.acces.kspInfo.installedModList.Count; i++)
            {
                if (zipName == Main.acces.kspInfo.installedModList[i].codeName)
                {
                    Main.acces.kspInfo.installedModList.RemoveAt(i);
                    break;
                }
            }

            // Finalise
            MiscFunctions.ProcessDirectory(Main.acces.kspInfo.kspFolder + "\\GameData", false);
            MiscFunctions.ProcessDirectory(Main.acces.kspInfo.kspFolder + "\\KMM\\overrides", false);
        }
    }
}
