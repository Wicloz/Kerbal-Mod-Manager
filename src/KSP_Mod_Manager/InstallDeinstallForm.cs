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

        private List<ModInfo> installModList = new List<ModInfo>();
        private List<InstalledInfo> deinstallModList = new List<InstalledInfo>();

        private float stepSize = 0;

        private string mode;
        private int currentMod = -1;

        private Thread stuff;

        private void EmptyFunction()
        { }

        public InstallDeinstallForm(List<ModInfo> ModList)
        {
            InitializeComponent();

            installModList = ModList;
            mode = "install";
        }

        public InstallDeinstallForm(List<InstalledInfo> ModList)
        {
            InitializeComponent();

            deinstallModList = ModList;
            mode = "deinstall";
        }

        private void InstallDeinstall_Form_Shown(object sender, EventArgs e)
        {
            stepSize = 1000 / (installModList.Count + deinstallModList.Count);
            stuff = new Thread(EmptyFunction);

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 20;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!stuff.IsAlive)
            {
                if (currentMod > installModList.Count + deinstallModList.Count - 1)
                {
                    this.Close();
                    return;
                }

                HandleNextMod();
            }
        }

        private void HandleNextMod()
        {
            allModProgress.Style = ProgressBarStyle.Continuous;
            allModProgress.Value = Convert.ToInt32(Math.Max(0, stepSize * (currentMod + 1)));

            progressLabel2.Text = "(" + (currentMod + 1) + "/" + (installModList.Count + deinstallModList.Count) + ")";

            if (currentMod == installModList.Count + deinstallModList.Count - 1)
            {
                currentMod++;
                progressLabel1.Text = "Done!";

                stuff = new Thread(DoneWait);
                stuff.Start();

                return;
            }

            if (mode == "install")
            {
                progressLabel1.Text = "Installing Mod '" + installModList[currentMod+1].name + "'";
            }

            else if (mode == "deinstall")
            {
                progressLabel1.Text = "Deinstalling Mod '" + deinstallModList[currentMod + 1].modName + "'";
            }

            stuff = new Thread(HandleModAsync);
            stuff.Start();
        }

        private void HandleModAsync()
        {
            currentMod++;

            if (mode == "install")
            {
                im.Install(installModList[currentMod]);
            }

            else if (mode == "deinstall")
            {
                dm.Deinstall(deinstallModList[currentMod].codeName);
            }
        }

        private void DoneWait()
        {
            Thread.Sleep(100);
        }
    }
}
