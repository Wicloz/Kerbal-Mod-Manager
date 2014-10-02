using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace KSP_Mod_Manager
{
    class Functions
    {
        public static void ProcessDirectory(string startLocation, bool includeFirst)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                ProcessDirectory(directory, false);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }

            if (includeFirst)
            {
                Directory.Delete(startLocation, false);
            }
        }

        public static string CleanName(string originalName)
        {
            return originalName
                .ToLower()
                    .Replace("0", "")
                    .Replace("1", "")
                    .Replace("2", "")
                    .Replace("3", "")
                    .Replace("4", "")
                    .Replace("5", "")
                    .Replace("6", "")
                    .Replace("7", "")
                    .Replace("8", "")
                    .Replace("9", "")
                    .Replace(".zip", "")
                    .Replace(".txt", "")
                    .Replace("-", "")
                    .Replace("_", "")
                    .Replace(" ", "")
                    .Replace(".", "");
        }

        public static InstalledInfo GetInstalledMod(ModInfo mod)
        {
            InstalledInfo returnMod = null;

            for (int i = 0; i < Main.acces.kspInfo.installedModList.Count; i++)
            {
                if (Main.acces.kspInfo.installedModList[i].key == mod.key)
                {
                    returnMod = Main.acces.kspInfo.installedModList[i];
                    break;
                }
            }

            return returnMod;
        }

        public static InstalledInfo GetInstalledMod(string name)
        {
            InstalledInfo returnMod = null;

            for (int i = 0; i < Main.acces.kspInfo.installedModList.Count; i++)
            {
                if (Main.acces.kspInfo.installedModList[i].modName == name)
                {
                    returnMod = Main.acces.kspInfo.installedModList[i];
                    break;
                }
            }

            return returnMod;
        }
    }
}
