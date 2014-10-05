using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSP_Mod_Manager
{
    [Serializable]
    public class Settings
    {
        public string modsPath;
        public List<InstallInstance> instances;
        public int selectedInstance;
        public List<string> categoryList;

        public Settings()
        { }

        public Settings(string ModsPath, List<InstallInstance> Instances, int SelectedInstance, List<string> CategoryList)
        {
            modsPath = ModsPath;
            instances = Instances;
            selectedInstance = SelectedInstance;
            categoryList = CategoryList;
        }
    }

    [Serializable]
    public class ModInfo : System.IComparable<ModInfo>
    {
        public string name;
        public string category;
        public string key;
        public string zipfile;
        public bool canUpdate;
        public string version;

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

        public ModInfo()
        { }

        public ModInfo(string Zipfile)
        {
            name = Functions.CleanName(Zipfile);
            if (name.Contains("overrides\\"))
            {
                name = name.Replace("overrides\\", "") + "\\Override";
            }

            category = "none";
            key = name;
            zipfile = Zipfile;
            canUpdate = false;
            version = "none";
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

    [Serializable]
    public class InstalledInfo : System.IComparable<InstalledInfo>
    {
        public string modName;
        public string category;
        public string key;
        public string codeName;
        public string version;

        public InstalledInfo()
        { }

        public InstalledInfo(string ModName, string Category, string Key, string CodeName, string Version)
        {
            modName = ModName;
            category = Category;
            key = Key;
            codeName = CodeName;
            version = Version;
        }

        public ModInfo GetModInfo()
        {
            ModInfo returnval = null;

            foreach (ModInfo mod in Main.acces.modInfo.modList)
            {
                if (mod.key == this.key)
                {
                    returnval = mod;
                    break;
                }
            }

            return returnval;
        }

        public int CompareTo(InstalledInfo other)
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
                char[] charArray1 = (this.modName).ToCharArray();
                char[] charArray2 = (other.modName).ToCharArray();

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

    [Serializable]
    public class InstallInstance
    {
        public string kspPath;
        public string name;

        public InstallInstance()
        { }

        public InstallInstance(string Name)
        {
            kspPath = "";
            name = Name;
        }
    }

    [Serializable]
    public class SiteInfo
    {
        public string key;
        public string website;
        public string dlSite;

        public SiteInfo()
        { }

        public SiteInfo(string Key, string Website)
        {
            key = Key.ToLower();
            website = Website;
            dlSite = "NONE";

            if (Website.Contains("curse"))
            {
                dlSite = Website + "\\files\\latest";
            }
        }
    }

    [Serializable]
    public class FileInfo
    {
        public string path;
        public string modName;
        public List<string> overrideList;

        public FileInfo()
        { }

        public FileInfo(string Path, string ModName)
        {
            path = Path;
            modName = ModName;
            overrideList = new List<string>();
        }
    }

    [Serializable]
    public class FavInfo
    {
        public string key;
        public bool isFav;
        public int order;

        public FavInfo()
        { }

        public FavInfo(string Key)
        {
            key = Key;
            isFav = false;
            order = 0;
        }
    }
}
