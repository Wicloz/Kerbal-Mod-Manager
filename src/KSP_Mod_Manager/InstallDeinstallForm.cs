using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Compression;

namespace KSP_Mod_Manager
{
    public partial class InstallDeinstallForm : Form
    {
        private InstallMod im = new InstallMod();
        private DeinstallMod dm = new DeinstallMod();

        private List<ModInfo> installModList = new List<ModInfo>();
        private List<InstalledInfo> deinstallModList = new List<InstalledInfo>();

        private float stepSize = 0;

        private string mode;
        private int currentMod = -1;

        private bool actionDone = true;
        private bool formDone = false;
        private bool mustUnzip = false;

        private Thread stuff;
        private Thread preUnzip;

        private void EmptyFunction()
        { }

        public InstallDeinstallForm(List<ModInfo> ModList)
        {
            InitializeComponent();

            installModList = ModList;
            mode = "install";
            this.Text = "Installing Mods ...";
        }

        public InstallDeinstallForm(List<InstalledInfo> ModList)
        {
            InitializeComponent();

            deinstallModList = ModList;
            mode = "deinstall";
            this.Text = "Deinstalling Mods ...";
        }

        private void InstallDeinstall_Form_Shown(object sender, EventArgs e)
        {
            // Check for overrides
            for (int i = 0; i < deinstallModList.Count; i++)
            {
                for (int j = 0; j < Main.acces.kspInfo.installedModList.Count; j++)
                {
                    if (Functions.CleanName("Overrides\\" + deinstallModList[i].codeName) == Functions.CleanName(Main.acces.kspInfo.installedModList[j].codeName))
                    {
                        deinstallModList.Insert(0, Main.acces.kspInfo.installedModList[j]);
                        i++;
                        break;
                    }
                }
            }

            // Check for overrides
            for (int i = 0; i < installModList.Count; i++)
            {
                for (int j = 0; j < Main.acces.modInfo.modList.Count; j++)
                {
                    if (Functions.CleanName(installModList[i].name + "\\Override") == Functions.CleanName(Main.acces.modInfo.modList[j].name))
                    {
                        installModList.Add(Main.acces.modInfo.modList[j]);
                        break;
                    }
                }
            }

            // Initiate values
            stuff = new Thread(EmptyFunction);
            preUnzip = new Thread(EmptyFunction);
            stepSize = 1000 / (installModList.Count + deinstallModList.Count);

            // Initiate timer
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 20;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (actionDone)
            {
                HandleNextMod();
            }

            else if(mustUnzip && !formDone)
            {
                stuff = new Thread(Unzip);
                stuff.Start();
            }
        }

        private void HandleNextMod()
        {
            actionDone = false;
            currentMod++;

            if (formDone)
            {
                this.Close();
                return;
            }

            allModProgress.Style = ProgressBarStyle.Continuous;
            allModProgress.Value = Convert.ToInt32(Math.Max(0, stepSize * currentMod));

            progressLabel2.Text = "(" + currentMod + "/" + (installModList.Count + deinstallModList.Count) + ")";

            if (currentMod == installModList.Count + deinstallModList.Count)
            {
                progressLabel1.Text = "Done!";
                formDone = true;

                stuff = new Thread(DoneWait);
                stuff.Start();

                return;
            }

            else if (mode == "install")
            {
                progressLabel1.Text = "Installing Mod '" + installModList[currentMod].name + "'";
                Main.acces.LogMessage("Installing '" + installModList[currentMod].zipfile + "'.");

                preUnzip = new Thread(PreUnzip);
                preUnzip.Start();
            }

            else if (mode == "deinstall")
            {
                progressLabel1.Text = "Deinstalling Mod '" + deinstallModList[currentMod].modName + "'";
                Main.acces.LogMessage("Deinstalling '" + deinstallModList[currentMod].codeName + "'.");

                stuff = new Thread(Deinstall);
                stuff.Start();
            }
        }

        private void PreUnzip()
        {
            im.PreUnzip();
            mustUnzip = true;
        }

        private void Unzip()
        {
            mustUnzip = false;

            ZipFile.ExtractToDirectory(Main.acces.modInfo.modsPath + "\\" + installModList[currentMod].zipfile, Main.acces.kspInfo.kspFolder + "\\KMM\\temp");
            PostUnzip();
        }

        private void PostUnzip()
        {
            im.PostUnzip(installModList[currentMod]);
            actionDone = true;
        }

        private void Deinstall()
        {
            dm.Deinstall(deinstallModList[currentMod].codeName);
            actionDone = true;
        }

        private void DoneWait()
        {
            Thread.Sleep(100);
            actionDone = true;
        }
    }
}
