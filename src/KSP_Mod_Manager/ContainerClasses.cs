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

        public Settings()
        { }

        public Settings(string ModsPath, List<InstallInstance> Instances, int SelectedInstance)
        {
            modsPath = ModsPath;
            instances = Instances;
            selectedInstance = SelectedInstance;
        }
    }

    [Serializable]
    public class ModInfo : System.IComparable<ModInfo>
    {
        public string name;
        public string category;
        public string key;
        public SiteInfo websites;
        public string zipfile;
        public bool canUpdate;
        public string version;

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
            websites = null;
            zipfile = Zipfile;
            canUpdate = false;
            version = "none";
        }

        public int CompareTo(ModInfo other)
        {
            if (other == null)
            {
                return 1;
            }

            char[] charArray1 = (category + "|" + name).ToCharArray();
            char[] charArray2 = (other.category + "|" + other.name).ToCharArray();

            for (int i = 0; i < charArray1.Length; i++)
            {
                if (charArray1[i].GetHashCode() != charArray2[i].GetHashCode())
                {
                    return charArray1[i].GetHashCode() - charArray2[i].GetHashCode();
                }
            }

            return 0;
        }
    }

    [Serializable]
    public class InstalledInfo : System.IComparable<InstalledInfo>
    {
        public string modName;
        public string category;
        public string codeName;
        public string version;

        public InstalledInfo()
        { }

        public InstalledInfo(string ModName, string Category, string CodeName, string Version)
        {
            modName = ModName;
            category = Category;
            codeName = CodeName;
            version = Version;
        }

        public int CompareTo(InstalledInfo other)
        {
            if (other == null)
            {
                return 1;
            }

            char[] charArray1 = (category + "|" + modName).ToCharArray();
            char[] charArray2 = (other.category + "|" + other.modName).ToCharArray();

            for (int i = 0; i < charArray1.Length; i++)
            {
                if (charArray1[i].GetHashCode() != charArray2[i].GetHashCode())
                {
                    return charArray1[i].GetHashCode() - charArray2[i].GetHashCode();
                }
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
