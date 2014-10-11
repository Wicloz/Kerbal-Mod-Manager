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
        private List<ModInfo> checkUpdateList = new List<ModInfo>();
        private List<ModInfo> updateModList = new List<ModInfo>();

        private float stepSize = 0;

        private int currentMod = -1;
        private int currentIndex = -1;

        private bool actionDone = true;
        private bool formDone = false;

        private Thread stuff;

        private void EmptyFunction()
        { }

        public InstallDeinstallForm()
        {
            InitializeComponent();
        }

        public void AddInstallMod(ModInfo m)
        {
            installModList.Add(m);
        }

        public void AddDeinstallMod(InstalledInfo m)
        {
            deinstallModList.Add(m);
        }

        public void AddCheckUpdateMod(ModInfo m)
        {
            checkUpdateList.Add(m);
        }

        public void AddUpdateMod(ModInfo m)
        {
            updateModList.Add(m);
        }

        public bool HasActions()
        {
            if (checkUpdateList.Count + updateModList.Count + deinstallModList.Count + installModList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InstallDeinstallForm_Shown(object sender, EventArgs e)
        {
            if (checkUpdateList.Count + updateModList.Count + deinstallModList.Count + installModList.Count <= 0)
            {
                this.Close();
                return;
            }

            // Reinstall updating mods
            for (int i = 0; i < updateModList.Count; i++)
            {
                if (updateModList[i].isInstalled)
                {
                    installModList.Add(updateModList[i]);
                    deinstallModList.Add(Functions.GetInstalledMod(updateModList[i]));
                }
            }

            // Check for overrides
            for (int i = 0; i < deinstallModList.Count; i++)
            {
                if (deinstallModList[i].GetOverride() != null)
                {
                    deinstallModList.Insert(0, deinstallModList[i].GetOverride());
                    i++;
                }
            }

            // Check for overrides
            for (int i = 0; i < installModList.Count; i++)
            {
                if (installModList[i].GetOverride() != null)
                {
                    installModList.Add(installModList[i].GetOverride());
                }
            }

            // Initiate values
            stuff = new Thread(EmptyFunction);
            stepSize = 1000 / (checkUpdateList.Count + updateModList.Count + deinstallModList.Count + installModList.Count);

            // Initiate timer
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 20;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            thisModProgress.Value = um.progress + uc.progress;

            if (um.updateDone)
            {
                actionDone = true;
                um.updateDone = false;
                um.progress = 0;
            }

            if (uc.checkDone)
            {
                actionDone = true;
                uc.checkDone = false;
                uc.progress = 0;
            }

            if (actionDone)
            {
                Main.acces.kspInfo.SaveFiles(Main.acces.kspInfo.kspFolder);
                Main.acces.modInfo.SaveFiles(Main.acces.modInfo.modsPath);
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

            progressLabel2.Text = "(" + currentMod + "/" + (checkUpdateList.Count + updateModList.Count + deinstallModList.Count + installModList.Count) + ")";

            if (currentMod == checkUpdateList.Count + updateModList.Count + deinstallModList.Count + installModList.Count)
            {
                progressLabel1.Text = "Done!";
                formDone = true;

                stuff = new Thread(DoneWait);
                stuff.Start();

                return;
            }

            else if (currentMod < checkUpdateList.Count)
            {
                currentIndex = currentMod;

                progressLabel1.Text = "Checking Update For '" + checkUpdateList[currentIndex].name + "'";
                this.Text = "Checking for Updates ...";
                thisModProgress.Style = ProgressBarStyle.Continuous;

                Main.acces.LogMessage("Checking Update For '" + checkUpdateList[currentIndex].name + "'...");

                stuff = new Thread(CheckUpdate);
                stuff.Start();
            }

            else if (currentMod < checkUpdateList.Count + updateModList.Count)
            {
                currentIndex = currentMod - checkUpdateList.Count;

                progressLabel1.Text = "Updating Mod '" + updateModList[currentIndex].name + "'";
                this.Text = "Updating Mods ...";
                thisModProgress.Style = ProgressBarStyle.Continuous;

                Main.acces.LogMessage("Updating '" + updateModList[currentIndex].name + "'.");

                stuff = new Thread(UpdateMod);
                stuff.Start();
            }

            else if (currentMod < checkUpdateList.Count + updateModList.Count + deinstallModList.Count)
            {
                currentIndex = currentMod - checkUpdateList.Count - updateModList.Count;

                progressLabel1.Text = "Deinstalling Mod '" + deinstallModList[currentIndex].modName + "'";
                this.Text = "Deinstalling Mods ...";

                Main.acces.LogMessage("Deinstalling '" + deinstallModList[currentIndex].codeName + "'...");

                stuff = new Thread(Deinstall);
                stuff.Start();
            }

            else if (currentMod < checkUpdateList.Count + updateModList.Count + deinstallModList.Count + installModList.Count)
            {
                currentIndex = currentMod - checkUpdateList.Count - updateModList.Count - deinstallModList.Count;

                progressLabel1.Text = "Installing Mod '" + installModList[currentIndex].name + "'";
                this.Text = "Installing Mods ...";

                Main.acces.LogMessage("Installing '" + installModList[currentIndex].zipfile + "'...");

                stuff = new Thread(Install);
                stuff.Start();
            }
        }

        private void Install()
        {
            im.Install(installModList[currentIndex]);
            actionDone = true;
        }

        private void Deinstall()
        {
            dm.Deinstall(deinstallModList[currentIndex].codeName);
            actionDone = true;
        }

        private void CheckUpdate()
        {
            uc.CheckForUpdate(checkUpdateList[currentIndex]);
        }

        private void UpdateMod()
        {
            um.Update(updateModList[currentIndex]);
        }

        private void DoneWait()
        {
            Thread.Sleep(100);
            actionDone = true;
        }
    }
}
