﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace KSP_Mod_Manager
{
    [Serializable]
    public class ModInfo : System.IComparable<ModInfo>
    {
        public string name;
        public string category;
        public string key;
        public string zipfile;
        private bool CanUpdate;
        public string version;
        public string vnLocal;
        public string vnOnline;

        public ModInfo()
        { }

        public ModInfo(string Zipfile)
        {
            name = Functions.CleanName(Zipfile);
            category = "none";
            key = name;
            zipfile = Zipfile;
            canUpdate = false;
            version = "none";
            vnLocal = "N/A";
            vnOnline = "N/A";
        }

        public bool canUpdate
        {
            get
            {
                return CanUpdate;
            }
            set
            {
                CanUpdate = value;
                string downloadFolder = Main.acces.modInfo.modsPath + "\\ModDownloads\\" + this.name.Replace(" ", "_");

                if (value)
                {
                    Directory.CreateDirectory(downloadFolder);
                }
                else
                {
                    try
                    {
                        Directory.Delete(downloadFolder, true);
                    }
                    catch
                    { }

                    try
                    {
                        if (Directory.GetDirectories(Main.acces.modInfo.modsPath + "\\ModDownloads").Length <= 0)
                        {
                            Directory.Delete(Main.acces.modInfo.modsPath + "\\ModDownloads", true);
                        }
                    }
                    catch
                    { }
                }
            }
        }

        public SiteInfo websites
        {
            get
            {
                SiteInfo returnval = null;

                foreach (SiteInfo site in Main.acces.modInfo.siteList)
                {
                    if (site.key == this.key)
                    {
                        returnval = site;
                        break;
                    }
                }

                return returnval;
            }
        }

        public FavInfo favorite
        {
            get
            {
                FavInfo returnval = null;

                foreach (FavInfo fav in Main.acces.kspInfo.favoritesList)
                {
                    if (this.key == fav.key)
                    {
                        returnval = fav;
                        break;
                    }
                }

                return returnval;
            }
        }

        public bool hasZipfile
        {
            get
            {
                if (String.IsNullOrEmpty(this.zipfile))
                {
                    return false;
                }
                else if (this.zipfile == "none")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool isInstalled
        {
            get
            {
                foreach (InstalledInfo installedMod in Main.acces.kspInfo.installedModList)
                {
                    if (installedMod.key == this.key)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public ModInfo GetOverride()
        {
            for (int i = 0; i < Main.acces.modInfo.modList.Count; i++)
            {
                if (("overrides\\" + Functions.CleanName(this.name)).Replace("x", "").Replace("v", "") == Functions.CleanName(Main.acces.modInfo.modList[i].name).Replace("x", "").Replace("v", ""))
                {
                    return Main.acces.modInfo.modList[i];
                }
            }

            return null;
        }

        public void ManageMod()
        {
            if (this.zipfile == "none" && (this.key == "none" || this.key == "" || this.key == null))
            {
                this.key = Functions.CleanName(this.name);
            }
            else if (this.key == "none" || this.key == "" || this.key == null)
            {
                this.key = Functions.CleanName(this.zipfile);
            }

            SiteInfo site = this.websites;

            if (site != null)
            {
                if (site.website.Contains("kerbal.curseforge.com"))
                {
                    site.dlSite = site.website + "/files/latest";
                }

                else if (site.website.Contains("kerbalstuff.com"))
                {
                    string newVersion = "";
                    StringReader sr = new StringReader(this.version.Replace("<h2>Version", "").Replace(" ", ""));

                    for (int i = 0; i < 111; i++)
                    {
                        char[] c = new char[1];
                        sr.Read(c, 0, 1);

                        if (c[0] == '<')
                        {
                            break;
                        }
                        else
                        {
                            newVersion += Convert.ToString(c[0]);
                        }
                    }

                    site.dlSite = site.website + "/download/" + newVersion;
                }

                else if (site.website.Contains("github.com"))
                {
                    string newVersion = "";
                    StringReader sr = new StringReader(this.version.Replace("<a href=\"/", "").Replace(" ", ""));

                    for (int i = 0; i < 111; i++)
                    {
                        char[] c = new char[1];
                        sr.Read(c, 0, 1);

                        if (c[0] == '"')
                        {
                            break;
                        }
                        else
                        {
                            newVersion += Convert.ToString(c[0]);
                        }
                    }

                    site.dlSite = "https://github.com/" + newVersion;
                }

                else
                {
                    site.dlSite = "NONE";
                }
            }

            if (this.zipfile == "none")
            {
                this.canUpdate = true;
            }
        }

        public int CompareTo(ModInfo other)
        {
            int thisCat = 999;
            int otherCat = 999;

            if (other == null)
            {
                return 1;
            }

            List<string> categoryList = Main.acces.GetCategories();

            for (int i = 0; i < categoryList.Count; i++)
            {
                if (this.category == categoryList[i])
                {
                    thisCat = i;
                }

                if (other.category == categoryList[i])
                {
                    otherCat = i;
                }
            }

            if (thisCat == otherCat)
            {
                char[] charArray1 = (this.name).ToCharArray();
                char[] charArray2 = (other.name).ToCharArray();

                for (int i = 0; i < charArray1.Length; i++)
                {
                    if (charArray1[i].GetHashCode() != charArray2[i].GetHashCode())
                    {
                        return charArray1[i].GetHashCode() - charArray2[i].GetHashCode();
                    }

                    if (i + 1 >= charArray2.Length)
                    {
                        return -1;
                    }
                }
            }
            else
            {
                return thisCat - otherCat;
            }

            return 0;
        }
    }
}