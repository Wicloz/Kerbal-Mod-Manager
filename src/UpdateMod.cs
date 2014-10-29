using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.IO.Compression;

namespace KSP_Mod_Manager
{
    class UpdateMod
    {
        public int progress = 0;
        public bool updateDone = false;

        private ModInfo modInfo;

        public void Update(ModInfo ModInfo)
        {
            modInfo = ModInfo;

            string modFolder = "\\ModDownloads\\" + modInfo.name.Replace(" ", "_");
            string downloadFolder = Main.acces.modInfo.modsPath + modFolder;
            Directory.CreateDirectory(downloadFolder);
            progress = 10;

            if (modInfo.dlSite != "NONE")
            {
                Main.acces.LogMessage("Downloading '" + modInfo.name + "'.");

                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);

                client.DownloadFileAsync(new Uri(modInfo.dlSite), downloadFolder + "\\" + modInfo.name.Replace(" ", "") + ".zip");
            }
            else
            {
                PostDownload();
                Exit();
            }
        }

        private void PostDownload()
        {
            progress = 90;

            string modFolder = "\\ModDownloads\\" + modInfo.name.Replace(" ", "_");
            string downloadFolder = Main.acces.modInfo.modsPath + modFolder;

            string newModFile;
            try
            {
                newModFile = Directory.GetFiles(downloadFolder, "*.zip")[0];
            }
            catch
            {
                Main.acces.LogMessage("Mod failed to download, aborting updating of '" + modInfo.name + "'!");
                return;
            }

            if (newModFile != null)
            {
                modInfo.vnLocal = modInfo.vnOnline;
            }

            string newModLocation = Main.acces.modInfo.modsPath + "\\" + modInfo.name.Replace(" ", "") + "_v" + modInfo.vnLocal.Replace("/", "") + ".zip";
            File.Delete(newModLocation);
            File.Delete(Main.acces.modInfo.modsPath + "\\" + modInfo.zipfile);

            File.Move(newModFile, newModLocation);
            modInfo.zipfile = newModLocation.Replace(Main.acces.modInfo.modsPath + "\\", "");

            Main.acces.LogMessage("'" + modInfo.name + "' has been updated.");

            try
            {
                Directory.Delete(downloadFolder, true);
            }
            catch
            { }

            modInfo.canUpdate = false;
        }

        private void Exit()
        {
            progress = 100;
            updateDone = true;
        }

        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string modFolder = "\\ModDownloads\\" + modInfo.name.Replace(" ", "_");
            string downloadFolder = Main.acces.modInfo.modsPath + modFolder;

            Main.acces.LogMessage("'" + modInfo.name + "' has finished downloading.");

            try
            {
                ZipFile.ExtractToDirectory(downloadFolder + "\\" + modInfo.name.Replace(" ", "") + ".zip", downloadFolder + "\\temp");
                PostDownload();
            }
            catch
            {
                Main.acces.LogMessage("Mod failed to download, aborting updating of '" + modInfo.name + "'!");
            }

            Exit();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress = 10 + Convert.ToInt32(e.ProgressPercentage * 0.8);
        }
    }
}
