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

        public InstallDeinstallForm(List<ModInfo> CheckUpdateModList, List<ModInfo> UpdateModList,  List<InstalledInfo> DeinstallModList, List<ModInfo> InstallModList)
        {
            InitializeComponent();

            checkUpdateList = CheckUpdateModList;
            updateModList = UpdateModList;
            deinstallModList = DeinstallModList;
            installModList = InstallModList;
        }

        private void InstallDeinstall_Form_Shown(object sender, EventArgs e)
        {
            // Reinstall updating mods
            for (int i = 0; i < updateModList.Count; i++)
            {
                if (Functions.IsModInstalled(updateModList[i]))
                {
                    installModList.Add(updateModList[i]);
                    deinstallModList.Add(Functions.GetInstalledMod(updateModList[i]));
                }
            }

            // Check for overrides
            for (int i = 0; i < deinstallModList.Count; i++)
            {
                for (int j = 0; j < Main.acces.kspInfo.installedModList.Count; j++)
                {
                    if (Functions.CleanName(deinstallModList[i].modName + "\\Override").Replace("x", "").Replace("v", "") == Functions.CleanName(Main.acces.kspInfo.installedModList[j].modName).Replace("x", "").Replace("v", ""))
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
                    if (Functions.CleanName(installModList[i].name + "\\Override").Replace("x", "").Replace("v", "") == Functions.CleanName(Main.acces.modInfo.modList[j].name).Replace("x", "").Replace("v", ""))
                    {
                        installModList.Add(Main.acces.modInfo.modList[j]);
                        break;
                    }
                }
            }

            // Initiate values
            stuff = new Thread(EmptyFunction);
            stepSize = 100 / (checkUpdateList.Count + updateModList.Count + deinstallModList.Count + installModList.Count);

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
                Main.acces.kspInfo.SaveFiles(Main.acces.kspInfo.kspFolder);
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
            actionDone = true;
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
