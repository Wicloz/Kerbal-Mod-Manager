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
        public void CheckForUpdate(ModInfo modInfo)
        {
            string mode = "";
            string site = modInfo.websites.website;

            if (site.Contains("curse"))
            {
                mode = "curse";
            }
            else
            {
                mode = "forum";
            }

            Directory.CreateDirectory(Main.acces.modInfo.modsPath + "\\SiteDownload");
            string siteFile = Main.acces.modInfo.modsPath + "\\SiteDownload\\" + "download.html";

            WebClient client = new WebClient();
            client.DownloadFile(site, siteFile);

            if (File.Exists(siteFile))
            {
                FileStream file = File.Open(siteFile, FileMode.Open);

                StreamReader sf = new StreamReader(file);

                string oldVersion = modInfo.version;
                string newVersion = "";

                if (mode == "curse")
                {
                    while (!newVersion.Contains("<li>Last Released File:"))
                    {
                        newVersion = sf.ReadLine();
                    }
                }

                if (mode == "forum")
                {
                    while (!newVersion.Contains("<title>"))
                    {
                        newVersion = sf.ReadLine();
                    }
                }

                file.Close();
                sf.Close();

                if (oldVersion != newVersion)
                {
                    modInfo.canUpdate = true;
                    modInfo.version = newVersion;
                    Main.acces.LogMessage("Update found for '" + modInfo.name + "'.");
                }
            }
            else
            {
                Main.acces.LogMessage("Site failed to download, skipping '" + modInfo.name + "'!");
            }

            Directory.Delete(Main.acces.modInfo.modsPath + "\\SiteDownload", true);
        }
    }
}
