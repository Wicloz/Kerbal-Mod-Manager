﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace KSP_Mod_Manager
{
    class UpdateCheckEvents
    {
        public int progress = 0;
        public bool checkDone = false;

        private ModInfo modInfo;
        private string mode = "";

        public void CheckForUpdate(ModInfo ModInfo)
        {
            modInfo = ModInfo;
            string site = modInfo.website;

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
            else
            {
                Main.acces.LogMessage("Unsupported website, skipping '" + modInfo.name + "'!");
                return;
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
                    while (!newVersion.Contains("<div class=\"info-label\">Last Released File</div>"))
                    {
                        newVersion = sr.ReadLine();
                    }
                    newVersion = sr.ReadLine();

                    newVersion = MiscFunctions.RemoveLetters(newVersion);
                }

                if (mode == "forum")
                {
                    while (!newVersion.Contains("<title>"))
                    {
                        newVersion = sr.ReadLine();
                    }

                    newVersion = MiscFunctions.RemoveLetters(newVersion);
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
                    while (!newVersion.Contains("<a href=\"" + modInfo.website.Replace("https://github.com", "").Replace("/latest", "/download/")))
                    {
                        newVersion = sr.ReadLine();
                    }
                }

                GetVersion(siteString);

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

        private void GetVersion(string siteString)
        {
            StringReader sr1 = new StringReader(siteString);

            string versionLine = "";
            string newVersion = "N/A";

            if (modInfo.website.Contains("forum.kerbalspaceprogram.com"))
            {
                while (!versionLine.Contains("<title>"))
                {
                    versionLine = sr1.ReadLine();
                }

                newVersion = MiscFunctions.RemoveLetters(versionLine);
                if (newVersion == "")
                {
                    newVersion = "N/A";
                }
            }

            if (modInfo.website.Contains("kerbal.curseforge.com"))
            {
                while (!versionLine.Contains("<a class=\"overflow-tip\" href=\"/ksp-mods/"))
                {
                    versionLine = sr1.ReadLine();
                }

                List<char> endCharList = new List<char>();
                endCharList.Add('<');
                endCharList.Add('"');

                List<char> startCharList = new List<char>();
                startCharList.Add('>');

                newVersion = ExtractVersion(versionLine, endCharList, startCharList).Replace(".zip", "").Replace("1.Inline.Cockpit.", "").Replace("9.Aerospace.Pack.", "").Replace("3.Refit.Nazari.", "");
            }

            else if (modInfo.website.Contains("kerbalstuff.com"))
            {
                while (!versionLine.Contains("<h2>Version"))
                {
                    versionLine = sr1.ReadLine();
                }

                newVersion = ExtractVersion(versionLine, '<', '>');
            }

            else if (modInfo.website.Contains("github.com"))
            {
                while (!versionLine.Contains("aria-label=\"Code\" class=\"selected js-selected-navigation-item sunken-menu-item\""))
                {
                    versionLine = sr1.ReadLine();
                }

                List<char> endCharList = new List<char>();
                endCharList.Add('"');

                List<char> startCharList = new List<char>();
                startCharList.Add('t');
                startCharList.Add('r');
                startCharList.Add('e');
                startCharList.Add('e');
                startCharList.Add('/');

                newVersion = ExtractVersion(versionLine, endCharList, startCharList);
            }

            modInfo.vnOnline = newVersion;
            sr1.Dispose();
        }

        private string ExtractVersion(string s, char endChar, char startChar)
        {
            List<char> endCharList = new List<char>();
            endCharList.Add(endChar);

            List<char> startCharList = new List<char>();
            startCharList.Add(startChar);

            return ExtractVersion(s, endCharList, startCharList);
        }

        private string ExtractVersion(string s, List<char> endChars, List<char> startChars)
        {
            string returnString = "";
            bool start = false;
            bool foundChars = false;
            char[] charArray = s.Replace(" ", "").ToCharArray();
            int checkChar = 0;

            foreach (char c in charArray)
            {
                if (startChars[checkChar] == c)
                {
                    checkChar ++;
                }
                else
                {
                    checkChar = 0;
                }

                if (startChars.Count == checkChar)
                {
                    foundChars = true;
                    checkChar = 0;
                }

                if (foundChars && (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9'))
                {
                    start = true;
                }

                if (start && endChars.Contains(c))
                {
                    break;
                }
                else if (start)
                {
                    returnString += Convert.ToString(c);
                }
            }

            if (returnString == "")
            {
                returnString = "N/A";
            }

            return returnString.Replace("-", ".").Replace("_", ".");
        }
    }
}
