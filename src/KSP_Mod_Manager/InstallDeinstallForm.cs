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

namespace KSP_Mod_Manager
{
    public partial class InstallDeinstallForm : Form
    {
        private InstallMod im = new InstallMod();
        private DeinstallMod dm = new DeinstallMod();
        private UpdateCheck uc = new UpdateCheck();
        private UpdateMod um = new UpdateMod();

        private List<ModInfo> installModList = new List<ModInfo>();
        private List<InstalledInfo> deinstallModList = new List<InstalledInfo>();
        private List<ModInfo> updateModList = new List<ModInfo>();

        private float stepSize = 0;

        private string mode;
        private int currentMod = -1;

        private bool actionDone = true;
        private bool formDone = false;

        private Thread stuff;

        private void EmptyFunction()
        { }

        public InstallDeinstallForm(List<ModInfo> ModList, int Mode)
        {
            InitializeComponent();

            if (Mode == 0)
            {
                installModList = ModList;
                mode = "install";
                this.Text = "Installing Mods ...";
            }

            else if (Mode == 1)
            {
                updateModList = ModList;
                mode = "checkUpdate";
                this.Text = "Checking for Updates ...";
            }

            else if (Mode == 2)
            {
                updateModList = ModList;
                mode = "update";
                this.Text = "Updating Mods ...";
            }
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
            stepSize = 100 / (installModList.Count + deinstallModList.Count + updateModList.Count);

            // Initiate timer
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 20;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            thisModProgress.Value = um.progress;
            if (um.updateDone)
            {
                actionDone = true;
                um.updateDone = false;
            }

            if (actionDone)
            {
                HandleNextMod();
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

            thisModProgress.Style = ProgressBarStyle.Marquee;
            allModProgress.Style = ProgressBarStyle.Continuous;
            allModProgress.Value = Convert.ToInt32(Math.Max(0, stepSize * currentMod));

            progressLabel2.Text = "(" + currentMod + "/" + (installModList.Count + deinstallModList.Count + updateModList.Count) + ")";

            if (currentMod == installModList.Count + deinstallModList.Count + updateModList.Count)
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

                stuff = new Thread(Install);
                stuff.Start();
            }

            else if (mode == "deinstall")
            {
                progressLabel1.Text = "Deinstalling Mod '" + deinstallModList[currentMod].modName + "'";

                Main.acces.LogMessage("Deinstalling '" + deinstallModList[currentMod].codeName + "'.");

                stuff = new Thread(Deinstall);
                stuff.Start();
            }

            else if (mode == "checkUpdate")
            {
                progressLabel1.Text = "Checking Update For '" + updateModList[currentMod].name + "'";

                Main.acces.LogMessage("Checking Update For '" + updateModList[currentMod].name + "'.");

                stuff = new Thread(CheckUpdate);
                stuff.Start();
            }

            else if (mode == "update")
            {
                progressLabel1.Text = "Updating Mod '" + updateModList[currentMod].name + "'";
                thisModProgress.Style = ProgressBarStyle.Continuous;

                Main.acces.LogMessage("Updating '" + updateModList[currentMod].name + "'.");

                stuff = new Thread(UpdateMod);
                stuff.Start();
            }
        }

        private void Install()
        {
            im.Install(installModList[currentMod]);
            actionDone = true;
        }

        private void Deinstall()
        {
            dm.Deinstall(deinstallModList[currentMod].codeName);
            actionDone = true;
        }

        private void CheckUpdate()
        {
            uc.CheckForUpdate(updateModList[currentMod]);
            actionDone = true;
        }

        private void UpdateMod()
        {
            um.Update(updateModList[currentMod]);
        }

        private void DoneWait()
        {
            Thread.Sleep(100);
            actionDone = true;
        }
    }
}
