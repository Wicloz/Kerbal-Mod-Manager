using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;

namespace Kerbal_Mod_Manager
{
    [Serializable]
    class ModInfo : System.IComparable<ModInfo>
    {
        private string downloadFolder
        {
            get
            {
                return modFolder + "\\ModDownloads\\" + modName.Replace(" ", "_");
            }
        }
        private string downloadedFile
        {
            get
            {
                return downloadFolder + "\\" + newFileName;
            }
        }
        private string kerbstuffIdentifier = "kerbalstuff.com/mod/";
        private string curseIdentifier = "kerbal.curseforge.com/ksp-mods/";
        private string forumIdentifier = "forum.kerbalspaceprogram.com/threads/";
        private string githubIdentifier = "github.com";

        public string modFolder = "";
        public string modName = "";
        public string modKey = "";
        public string category = "none";
        public string currentFileName = "";
        public string newFileName = "";

        public string versionLocalRaw = "";
        public string versionLatestRaw = "";
        public string versionLocalNumeric = "N/A";
        public string versionLatestNumeric = "N/A";
        public string releaseDate = "N/A";
        public bool _canUpdate = false;

        public string website = "NONE";
        public string siteMode = "NONE";
        public string dlSite = "NONE";

        public string website1 = "NONE";
        public string website2 = "NONE";
        public string website3 = "NONE";
        public string website4 = "NONE";
        public string website5 = "NONE";

        //Dowload + Check info
        public bool updateList = false;
        public int progress = 0;
        public int findMode = 0;
        public bool findQueued = false;
        public bool checkQueued = false;
        public bool downloadQueued = false;
        private bool _findBusy = false;
        private bool _checkBusy = false;
        private bool _downloadBusy = false;

        public bool findBusy
        {
            get
            {
                return _findBusy;
            }
            set
            {
                _findBusy = value;
            }
        }
        public bool checkBusy
        {
            get
            {
                return _checkBusy;
            }
            set
            {
                _checkBusy = value;
            }
        }
        public bool downloadBusy
        {
            get
            {
                return _downloadBusy;
            }
            set
            {
                _downloadBusy = value;
            }
        }
        public bool isWorking
        {
            get
            {
                return (findBusy || checkBusy || downloadBusy);
            }
        }

        public string uriState
        {
            get
            {
                if (website == "NONE")
                {
                    return "";
                }
                else if (dlSite != "NONE")
                {
                    return "Automatic";
                }
                else if (siteMode != "NONE")
                {
                    return "Manual";
                }
                else
                {
                    return "Unsupported";
                }
            }
        }
        public string updateState
        {
            get
            {
                if (checkQueued)
                {
                    return "Checking for Update ...";
                }
                else if (findQueued)
                {
                    return "Searching for Website ...";
                }
                else if (downloadQueued && progress != 0)
                {
                    return "Downloading: " + progress + "%";
                }
                else if (downloadQueued)
                {
                    return "Awaiting Download ...";
                }
                else if (canUpdate)
                {
                    return "Update Available";
                }
                else
                {
                    return "";
                }
            }
        }
        public bool canUpdate
        {
            get
            {
                return _canUpdate;
            }
            set
            {
                _canUpdate = value;
                if (canUpdate)
                {
                    Directory.CreateDirectory(downloadFolder);
                }
                else
                {
                    try
                    {
                        Directory.Delete(downloadFolder, true);
                        if (Directory.GetDirectories(modFolder + "\\ModDownloads").Length == 0)
                        {
                            Directory.Delete(modFolder + "\\ModDownloads", false);
                        }
                    }
                    catch
                    { }
                }
            }
        }

        public ModInfo()
        { }

        public ModInfo(string fileName)
        {
            currentFileName = fileName;
            modName = MiscFunctions.CleanString(fileName);
            modKey = modName;
        }

        public void UpdateModValues()
        {
            //Manage local version
            versionLocalNumeric = MiscFunctions.RemoveLetters(currentFileName);

            //Determine site mode
            if (website == "")
            {
                website = "NONE";
            }

            if (website.Contains(kerbstuffIdentifier))
            {
                siteMode = "kerbstuff";
                website = ParseKerbstuffUri(website);
            }
            else if (website.Contains(curseIdentifier))
            {
                siteMode = "curse";
                website = ParseCurseUri(website);
            }
            else if (website.Contains(forumIdentifier))
            {
                siteMode = "forum";
                website = ParseForumUri(website);
            }
            else if (website.Contains(githubIdentifier))
            {
                siteMode = "github";
                website = ParseGithubUri(website);
            }
            else
            {
                siteMode = "NONE";
            }

            //Manage download site
            if (siteMode == "curse")
            {
                char[] startCharList = new char[] { 'k', 's', 'p', '-', 'm', 'o', 'd', 's', '/' };
                char[] endCharList = new char[] { };
                string modBit = MiscFunctions.ExtractSection(website, endCharList, startCharList) + "/";

                startCharList = modBit.ToCharArray();
                endCharList = new char[] { '"' };
                string appendage = MiscFunctions.ExtractSection(versionLatestRaw, endCharList, startCharList);

                dlSite = website + "/" + appendage;
            }

            else if (siteMode == "kerbstuff")
            {
                char[] endCharList = new char[] { '<' };
                string appendage = MiscFunctions.ExtractSection(versionLatestRaw.Replace("<h2>Version", ""), endCharList);

                dlSite = website + "/download/" + appendage;
            }

            else if (siteMode == "github")
            {
                string bit = website.Replace("https://github.com", "").Replace("/latest", "/download");
                string appendage = versionLatestRaw.Replace("<a href=\"", "").Replace(bit, "").Replace("\" rel=\"nofollow\">", "");
                dlSite = website.Replace("/latest", "/download") + appendage;
            }

            else
            {
                dlSite = "NONE";
            }
        }

        public void FindWebsiteUri()
        {
            findBusy = true;
            WebClient client1 = new WebClient();
            client1.DownloadStringCompleted += new DownloadStringCompletedEventHandler(findKerbstuffCompleted);
            WebClient client2 = new WebClient();
            client2.DownloadStringCompleted += new DownloadStringCompletedEventHandler(findCurseCompleted);
            WebClient client3 = new WebClient();
            client3.DownloadStringCompleted += new DownloadStringCompletedEventHandler(findGoogleCompleted);
            WebClient client4 = new WebClient();
            client4.DownloadStringCompleted += new DownloadStringCompletedEventHandler(findYahooCompleted);
            progress = 0;
            findMode++;

            switch (findMode)
            {
                case 1:
                    website = "NONE"; //debug
                    client1.DownloadStringAsync(new Uri("https://kerbalstuff.com/search?query=" + modName.Replace(" ", "+")));
                    break;
                case 2:
                    website1 = website; //debug
                    website = "NONE"; //debug
                    client2.DownloadStringAsync(new Uri("http://kerbal.curseforge.com/search?search=" + modName.Replace(" ", "+")));
                    break;
                case 3:
                    website2 = website;
                    website = "NONE"; //debug
                    client3.DownloadStringAsync(new Uri("http://www.google.com/search?sourceid=navclient&btnI=I&q=" + currentFileName.Replace(" ", "") + "+ksp"));
                    break;
                case 4:
                    website3 = website; //debug
                    website = "NONE"; //debug
                    client4.DownloadStringAsync(new Uri("https://search.yahoo.com/search?p=" + currentFileName.Replace(" ", "") + "+ksp"));
                    break;
                case 5:
                    website4 = website; //debug
                    website = "NONE"; //debug
                    client4.DownloadStringAsync(new Uri("https://search.yahoo.com/search?p=" + modName.Replace(" ", "+") + "+ksp"));
                    break;

                default:
                    website5 = website; //debug
                    website = "NONE"; //debug
                    if (website1 != "NONE")
                    {
                        website = website1;
                    }
                    else if (website2 != "NONE")
                    {
                        website = website2;
                    }
                    else if (website3 != "NONE")
                    {
                        website = website3;
                    }
                    else if (website4 != "NONE")
                    {
                        website = website4;
                    }
                    else if (website5 != "NONE")
                    {
                        website = website5;
                    }
                    website1 = website1.Replace(forumIdentifier, "").Replace(curseIdentifier, "").Replace(kerbstuffIdentifier, "").Replace(githubIdentifier, "").Replace("http://", "").Replace("www.", "");
                    website2 = website2.Replace(forumIdentifier, "").Replace(curseIdentifier, "").Replace(kerbstuffIdentifier, "").Replace(githubIdentifier, "").Replace("http://", "").Replace("www.", "");
                    website3 = website3.Replace(forumIdentifier, "").Replace(curseIdentifier, "").Replace(kerbstuffIdentifier, "").Replace(githubIdentifier, "").Replace("http://", "").Replace("www.", "");
                    website4 = website4.Replace(forumIdentifier, "").Replace(curseIdentifier, "").Replace(kerbstuffIdentifier, "").Replace(githubIdentifier, "").Replace("http://", "").Replace("www.", "");
                    website5 = website5.Replace(forumIdentifier, "").Replace(curseIdentifier, "").Replace(kerbstuffIdentifier, "").Replace(githubIdentifier, "").Replace("http://", "").Replace("www.", "");

                    progress = 0;
                    findMode = 0;
                    findQueued = false;
                    findBusy = false;
                    updateList = true; //debug
                    break;
            }
        }

        private void findKerbstuffCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e != null && e.Error == null && !String.IsNullOrEmpty(e.Result))
            {
                using (StringReader sr = new StringReader(e.Result))
                {
                    List<string> results = new List<string>();

                    while (true)
                    {
                        try
                        {
                            string currentline = sr.ReadLine().Trim();
                            if (currentline.Contains("<a href=\"/mc-mods/"))
                            {
                                results.Add(currentline);
                            }
                        }
                        catch
                        {
                            break;
                        }
                    }

                    foreach (string result in results)
                    {
                        char[] startCharList = new char[] { '"', '>' };
                        char[] endCharList = new char[] { '<' };
                        string foundModname = MiscFunctions.ExtractSection(result, endCharList, startCharList);

                        if (MiscFunctions.CleanString(foundModname) == MiscFunctions.CleanString(currentFileName))
                        {
                            startCharList = new char[] { '=', '"' };
                            endCharList = new char[] { '"' };
                            string linkSection = MiscFunctions.ExtractSection(result, endCharList, startCharList);
                            website = ParseCurseUri("http://minecraft.curseforge.com" + linkSection);
                            break;
                        }
                    }
                }

                UpdateModValues();
                FindWebsiteUri(); //debug
                return; //debug

                if (siteMode == "NONE")
                {
                    FindWebsiteUri();
                }
                else
                {
                    progress = 0;
                    findMode = 0;
                    findQueued = false;
                    findBusy = false;
                    updateList = true;
                }
            }
            else
            {
                FindWebsiteUri();
            }
        }

        private void findCurseCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e != null && e.Error == null && !String.IsNullOrEmpty(e.Result))
            {
                using (StringReader sr = new StringReader(e.Result))
                {
                    List<string> results = new List<string>();

                    while (true)
                    {
                        try
                        {
                            string currentline = sr.ReadLine().Trim();
                            if (currentline.Contains("<a href=\"/mc-mods/"))
                            {
                                results.Add(currentline);
                            }
                        }
                        catch
                        {
                            break;
                        }
                    }

                    foreach (string result in results)
                    {
                        char[] startCharList = new char[] { '"', '>' };
                        char[] endCharList = new char[] { '<' };
                        string foundModname = MiscFunctions.ExtractSection(result, endCharList, startCharList);

                        if (MiscFunctions.CleanString(foundModname) == MiscFunctions.CleanString(currentFileName))
                        {
                            startCharList = new char[] { '=', '"' };
                            endCharList = new char[] { '"' };
                            string linkSection = MiscFunctions.ExtractSection(result, endCharList, startCharList);
                            website = ParseCurseUri("http://minecraft.curseforge.com" + linkSection);
                            break;
                        }
                    }
                }

                UpdateModValues();
                FindWebsiteUri(); //debug
                return; //debug

                if (siteMode == "NONE")
                {
                    FindWebsiteUri();
                }
                else
                {
                    progress = 0;
                    findMode = 0;
                    findQueued = false;
                    findBusy = false;
                    updateList = true;
                }
            }
            else
            {
                FindWebsiteUri();
            }
        }

        private void findGoogleCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e != null && e.Error == null && !String.IsNullOrEmpty(e.Result))
            {
                char[] startCharList = new char[] { 'q', '=' };
                char[] endCharList = new char[] { '&' };
                website = GetWebsiteFromResults(e.Result, "<a href=\"", startCharList, endCharList);
                UpdateModValues();

                FindWebsiteUri(); //debug
                return; //debug

                if (siteMode == "NONE")
                {
                    FindWebsiteUri();
                }
                else
                {
                    progress = 0;
                    findMode = 0;
                    findQueued = false;
                    findBusy = false;
                    updateList = true;
                }
            }
            else
            {
                FindWebsiteUri();
            }
        }

        private void findYahooCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e != null && e.Error == null && !String.IsNullOrEmpty(e.Result))
            {
                char[] startCharList = new char[] { };
                char[] endCharList = new char[] { '"' };
                website = GetWebsiteFromResults(e.Result, "href=\"", startCharList, endCharList);
                UpdateModValues();

                FindWebsiteUri(); //debug
                return; //debug

                if (siteMode == "NONE")
                {
                    FindWebsiteUri();
                }
                else
                {
                    progress = 0;
                    findMode = 0;
                    findQueued = false;
                    findBusy = false;
                    updateList = true;
                }
            }
            else
            {
                FindWebsiteUri();
            }
        }

        private string GetWebsiteFromResults(string webpage, string delim, char[] startChars, char[] endChars)
        {
            List<string> results = new List<string>();
            using (StringReader sr = new StringReader(webpage))
            {
                string currentLine = "";
                while (true)
                {
                    currentLine = sr.ReadLine();

                    if (currentLine != null)
                    {
                        if (currentLine.Contains(delim))
                        {
                            string[] delims = new string[] { delim };
                            results.AddRange(currentLine.Split(delims, StringSplitOptions.RemoveEmptyEntries));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                string curseLink = "NONE";
                string forumLink = "NONE";

                for (int i = 1; i < results.Count; i++)
                {
                    results[i] = MiscFunctions.ExtractSection(results[i], endChars, startChars).Replace("%2f", "/").Replace("%3a", ":");

                    char[] startCharList = new char[] { 'm', 'o', 'd', 's', '/' };
                    char[] endCharList = new char[] { };
                    string foundModname = MiscFunctions.ExtractSection(results[i], endCharList, startCharList);

                    if (curseLink == "NONE" && results[i].Contains(curseIdentifier) && MiscFunctions.PartialMatch(currentFileName, foundModname))
                    {
                        curseLink = ParseCurseUri(results[i]);
                    }
                    if (forumLink == "NONE" && results[i].Contains(forumIdentifier) && MiscFunctions.PartialMatch(currentFileName, foundModname))
                    {
                        forumLink = ParseForumUri(results[i]);
                    }
                }

                if (curseLink != "NONE")
                {
                    return curseLink;
                }
                else
                {
                    return forumLink;
                }
            }
        }

        public void CheckForUpdate()
        {
            if (siteMode != "NONE")
            {
                checkBusy = true;
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(checkDownloadCompleted);
                progress = 10;
                client.DownloadStringAsync(new Uri(website));
            }
            else
            {
                checkQueued = false;
                versionLatestNumeric = "N/A";
                releaseDate = "N/A";
                newFileName = "";
            }
        }

        private void checkDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e != null && e.Error == null && !String.IsNullOrEmpty(e.Result))
            {
                progress = 100;
                using (StringReader sr = new StringReader(e.Result))
                {
                    string newVersion = "";
                    releaseDate = "N/A";
                    versionLatestRaw = "N/A";
                    versionLatestNumeric = "N/A";

                    if (siteMode == "kerbstuff")
                    {
                        while (!newVersion.Contains("<h2>Version"))
                        {
                            newVersion = sr.ReadLine().Trim();
                        }

                        char[] startCharList = new char[] { '>' };
                        char[] endCharList = new char[] { '<' };
                        versionLatestNumeric = MiscFunctions.CleanString(MiscFunctions.ExtractSection(newVersion, endCharList, startCharList));

                        string dateLine = sr.ReadLine().Trim();
                        startCharList = new char[] { '"', '>' };
                        endCharList = new char[] { '<' };
                        releaseDate = MiscFunctions.ExtractSection(dateLine, endCharList, startCharList).Replace("Released on ", "");
                    }

                    if (siteMode == "curse")
                    {
                        while (!newVersion.Contains("<a class=\"overflow-tip\" href=\"/ksp-mods/"))
                        {
                            newVersion = sr.ReadLine().Trim();
                        }

                        char[] startCharList = new char[] { '"', '>' };
                        char[] endCharList = new char[] { '<' };
                        versionLatestNumeric = MiscFunctions.CleanString(MiscFunctions.ExtractSection(newVersion, endCharList, startCharList));

                        string dateLine = sr.ReadLine().Trim();
                        startCharList = new char[] { '"', '>' };
                        endCharList = new char[] { '<' };
                        releaseDate = MiscFunctions.ExtractSection(dateLine, endCharList, startCharList);
                    }

                    if (siteMode == "forum")
                    {
                        while (!newVersion.Contains("<title>"))
                        {
                            newVersion = sr.ReadLine().Trim();
                        }
                        versionLatestNumeric = MiscFunctions.RemoveLetters(newVersion);
                    }

                    if (siteMode == "github")
                    {
                        while (!newVersion.Contains("<a href=\"" + website.Replace("https://github.com", "").Replace("/latest", "/download/")))
                        {
                            newVersion = sr.ReadLine().Trim();
                        }

                        char[] startCharList = new char[] { '/' };
                        char[] endCharList = new char[] { '"' };
                        versionLatestNumeric = MiscFunctions.ExtractSection(newVersion, endCharList, startCharList).Replace(" ", "");
                    }

                    versionLatestRaw = newVersion;

                    if (versionLatestRaw != versionLocalRaw)
                    {
                        canUpdate = true;
                    }
                    else
                    {
                        canUpdate = false;
                    }
                }

                UpdateModValues();
                progress = 0;
                checkQueued = false;
                checkBusy = false;
                updateList = true;
            }
            else
            {
                progress = 0;
                checkQueued = false;
                checkBusy = false;
            }
        }

        public void UpdateMod()
        {
            newFileName = modName.Replace(" ", "_") + "-" + versionLatestNumeric + ".zip";

            if (Directory.Exists(downloadFolder) && Directory.GetFiles(downloadFolder).Length > 0)
            {
                if (newFileName.EndsWith(".zip"))
                {
                    downloadBusy = true;
                    MoveDownloadedMod();
                }
                else
                {
                    downloadQueued = false;
                }
            }

            else if (uriState == "Automatic")
            {
                downloadBusy = true;
                if (Directory.Exists(downloadFolder))
                {
                    Directory.Delete(downloadFolder, true);
                }
                Directory.CreateDirectory(downloadFolder);

                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(modDownloadCompleted);
                progress = 10;
                client.DownloadFileAsync(new Uri(dlSite), downloadedFile);
            }

            else
            {
                downloadQueued = false;
            }
        }

        private void modDownloadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                if (File.Exists(downloadedFile))
                {
                    ZipFile.ExtractToDirectory(downloadedFile, downloadFolder + "\\extract");
                    progress = 100;
                    MoveDownloadedMod();
                }
            }
            catch
            {
                if (Directory.Exists(downloadFolder))
                {
                    Directory.Delete(downloadFolder, true);
                }
                Directory.CreateDirectory(downloadFolder);

                progress = 0;
                downloadQueued = false;
                downloadBusy = false;
            }
        }

        private void MoveDownloadedMod()
        {
            string newModLocation = modFolder + newFileName;
            string oldModLocation = modFolder + currentFileName;

            File.Delete(newModLocation);
            File.Delete(oldModLocation);
            File.Move(downloadedFile, newModLocation);

            currentFileName = newFileName;
            versionLocalRaw = versionLatestRaw;

            UpdateModValues();
            progress = 0;
            downloadQueued = false;
            downloadBusy = false;
            updateList = true;
            canUpdate = false;
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress = 10 + Convert.ToInt32(e.ProgressPercentage * 0.8);
        }

        private string ParseKerbstuffUri(string uri)
        {
            if (!uri.Contains(kerbstuffIdentifier))
            {
                uri = "NONE";
            }

            return uri;
        }

        private string ParseCurseUri(string uri)
        {
            if (!uri.Contains(curseIdentifier) || uri == "http://minecraft.curseforge.com/mc-mods/minecraft")
            {
                uri = "NONE";
            }
            else if (uri.EndsWith("/files"))
            {
                uri = uri.Replace("/files", "");
            }
            else if (uri.Contains("/files/"))
            {
                char[] startCharList = new char[] { 'f', 'i', 'l', 'e', 's', '/' };
                char[] endCharList = new char[] { };
                string garbage = "/files/" + MiscFunctions.ExtractSection(uri, endCharList, startCharList);
                uri = uri.Replace(garbage, "");
            }

            return uri;
        }

        private string ParseForumUri(string uri)
        {
            if (!uri.Contains(forumIdentifier) || uri == "http://www.minecraftforum.net/forums/mapping-and-modding/minecraft-mods")
            {
                uri = "NONE";
            }
            else if (uri.Contains("?page="))
            {
                char[] endCharList = new char[] { '?' };
                uri = MiscFunctions.ExtractSection(uri, endCharList);
            }

            return uri;
        }

        private string ParseGithubUri(string uri)
        {
            if (!uri.Contains(githubIdentifier))
            {
                uri = "NONE";
            }

            return uri;
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
                char[] charArray1 = (this.modName).ToCharArray();
                char[] charArray2 = (other.modName).ToCharArray();

                if (charArray1.Length == 0 || charArray2.Length == 0)
                {
                    return 0;
                }

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
