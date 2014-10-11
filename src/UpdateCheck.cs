using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace KSP_Mod_Manager
{
    class UpdateCheck
    {
        public int progress = 0;
        public bool checkDone = false;

        private ModInfo modInfo;
        private string mode = "";

        public void CheckForUpdate(ModInfo ModInfo)
        {
            modInfo = ModInfo;
            string site = modInfo.websites.website;

            if (site.Contains("kerbal.curseforge.com"))
            {
                mode = "curse";
            }
            else if (site.Contains("forum.kerbalspaceprogram.com"))
            {
                mode = "forum";
            }
            else if (site.Contains("kerbalstuff.com"))
            {
                mode = "kstuff";
            }
            else if (site.Contains("github.com"))
            {
                mode = "github";
            }

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            progress = 10;

            client.DownloadStringAsync(new Uri(site));
        }

        private void PostDownload(string siteString)
        {
            progress = 90;

            try
            {
                StringReader sr = new StringReader(siteString);

                string oldVersion = modInfo.version;
                string newVersion = "";

                if (mode == "curse")
                {
                    while (!newVersion.Contains("<li>Last Released File:"))
                    {
                        newVersion = sr.ReadLine();
                    }
                }

                if (mode == "forum")
                {
                    while (!newVersion.Contains("<title>"))
                    {
                        newVersion = sr.ReadLine();
                    }
                }

                if (mode == "kstuff")
                {
                    while (!newVersion.Contains("<h2>Version"))
                    {
                        newVersion = sr.ReadLine();
                    }
                }

                if (mode == "github")
                {
                    while (!newVersion.Contains("\" rel=\"nofollow\" class=\"button primary\">"))
                    {
                        newVersion = sr.ReadLine();
                    }
                }

                modInfo.GetVersion(siteString, false);

                if (oldVersion != newVersion)
                {
                    modInfo.canUpdate = true;
                    modInfo.version = newVersion;
                    Main.acces.LogMessage("Update found for '" + modInfo.name + "'.");
                }
            }
            catch
            {
                Main.acces.LogMessage("Site failed to download, skipping '" + modInfo.name + "'!");
                return;
            }
        }

        private void Exit()
        {
            progress = 100;
            checkDone = true;
        }

        private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.Result))
                {
                    PostDownload(e.Result);
                }
                else
                {
                    Main.acces.LogMessage("Site failed to download, skipping '" + modInfo.name + "'!");
                }
            }
            catch
            {
                Main.acces.LogMessage("Site failed to download, skipping '" + modInfo.name + "'!");
            }

            Exit();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress = 10 + Convert.ToInt32(e.ProgressPercentage * 0.8);
        }
    }
}
