using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KSP_Mod_Manager
{
    [Serializable]
    public class SettingsFixed
    {
        public string modsPath;
        public List<InstallInstance> instances;
        public int selectedInstance;

        public SettingsFixed()
        { }

        public SettingsFixed(string ModsPath, List<InstallInstance> Instances, int SelectedInstance)
        {
            modsPath = ModsPath;
            instances = Instances;
            selectedInstance = SelectedInstance;
        }
    }

    [Serializable]
    public class SettingsLocal
    {
        public List<string> categoryList;

        public SettingsLocal()
        { }

        public SettingsLocal(List<string> CategoryList)
        {
            categoryList = CategoryList;
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

        public InstalledInfo GetOverride()
        {
            for (int i = 0; i < Main.acces.kspInfo.installedModList.Count; i++)
            {
                if (("overrides\\" + MiscFunctions.CleanName(this.modName)).Replace("x", "").Replace("v", "") == MiscFunctions.CleanName(Main.acces.kspInfo.installedModList[i].modName).Replace("x", "").Replace("v", ""))
                {
                    return Main.acces.kspInfo.installedModList[i];
                }
            }

            return null;
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
