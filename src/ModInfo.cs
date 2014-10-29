using System;
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
        public string website;
        public string dlSite;
        public bool templated;

        public ModInfo()
        { }

        public ModInfo(string Zipfile)
        {
            this.name = Functions.CleanName(Zipfile);
            this.category = "none";
            this.key = name;
            this.zipfile = Zipfile;
            this.canUpdate = false;
            this.version = "none";
            this.vnLocal = "N/A";
            this.vnOnline = "N/A";
            this.website = "NONE";
            this.dlSite = "NONE";
            this.templated = false;
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
            // Manage key
            if (this.zipfile == "none" && (this.key == "none" || this.key == "" || this.key == null))
            {
                this.key = Functions.CleanName(this.name);
            }
            else if (this.key == "none" || this.key == "" || this.key == null)
            {
                this.key = Functions.CleanName(this.zipfile);
            }

            // Load template
            if (!this.templated)
            {
                foreach (TemplateInfo template in Main.acces.tm.templates)
                {
                    if (Functions.CleanName(this.zipfile).Replace("v", "") == Functions.CleanName(template.name).Replace("v", "") || Functions.CleanName(this.zipfile) == Functions.CleanName(template.originalZip))
                    {
                        this.name = template.name;
                        this.category = template.category;
                        this.website = template.website;
                        this.dlSite = template.dlSite;
                        break;
                    }
                }

                this.templated = true;
            }

            // Manage download site
            if (this.website.Contains("kerbal.curseforge.com"))
            {
                this.dlSite = this.website + "/files/latest";
            }

            else if (this.website.Contains("kerbalstuff.com"))
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

                this.dlSite = this.website + "/download/" + newVersion;
            }

            else if (this.website.Contains("github.com"))
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

                this.dlSite = "https://github.com/" + newVersion;
            }

            else
            {
                this.dlSite = "NONE";
            }

            // Do this thing
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
