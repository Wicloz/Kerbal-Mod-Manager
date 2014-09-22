using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace KSP_Mod_Manager
{
    class UpdateMod
    {
        public void Update(ModInfo modInfo, bool isInstalled, string installedMod)
        {
            Main.acces.SortLists();

            string modFolder = "\\ModDownloads\\" + modInfo.name.Replace(" ", "_");
            string downloadFolder = Main.acces.modsPath + modFolder;
            Directory.CreateDirectory(downloadFolder);

            if (modInfo.websites.dlSite != "NONE")
            {
                Main.acces.LogMessage("Downloading '" + modInfo.name + "'...");

                Functions.DownloadSite(modInfo.websites.dlSite, downloadFolder + "\\" + modInfo.name.Replace(" ", "") + ".zip");

                Main.acces.LogMessage("'" + modInfo.name + "' has finished downloading.");
            }
            else
            {
                Process.Start(modInfo.websites.website);

                bool done = false;
                while (!done)
                {
                    try
                    {
                        FileStream fs = File.Open(Directory.GetFiles(downloadFolder, "*.zip")[0], FileMode.Open, FileAccess.ReadWrite);
                        fs.Close();
                        done = true;
                    }
                    catch
                    {
                        done = false;
                    }

                }
            }

            string newModFile;
            try
            {
                newModFile = Directory.GetFiles(downloadFolder, "*.zip")[0];
            }
            catch
            {
                Main.acces.LogMessage("Mod failed to download, aborting updating of '" + modInfo.name + "'...");
                return;
            }

            string newModLocation = newModFile.Replace(modFolder, "");
            File.Delete(newModLocation);
            File.Delete(Main.acces.modsPath + "\\" + modInfo.zipfile);

            File.Move(newModFile, newModLocation);
            modInfo.zipfile = newModLocation.Replace(Main.acces.modsPath + "\\", "");

            if (isInstalled)
            {
                Main.acces.Reinstall(installedMod, modInfo);
            }

            Main.acces.LogMessage("'" + modInfo.name + "' has been updated.");

            try
            {
                Directory.Delete(downloadFolder, true);
                Functions.ProcessDirectory(Main.acces.modsPath + "\\ModDownloads", true);
            }
            catch
            { }

            modInfo.canUpdate = false;
        }
    }
}
