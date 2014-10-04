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

            try
            {
                string siteString = client.DownloadString(site);

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
            }
        }
    }
}
