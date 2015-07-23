﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerbal_Mod_Manager
{
    [Serializable]
    public class InstalledModInfo
    {
        public string modName;
        public string modKey;
        public string versionInstalledRaw = "";
        public string versionInstalledNumeric = "N/A";

        public InstalledModInfo()
        { }

        public InstalledModInfo(string modName, string modKey)
        {
            this.modName = modName;
            this.modKey = modKey;
        }

        public ModInfo ToModInfo()
        {
            ModInfo modInfo = new ModInfo();
            modInfo.installedOnly = true;

            modInfo.modName = modName;
            modInfo.modKey = modKey;
            modInfo.versionLocalNumeric = versionInstalledNumeric;
            modInfo.versionLocalRaw = versionInstalledRaw;

            return modInfo;
        }
    }
}
