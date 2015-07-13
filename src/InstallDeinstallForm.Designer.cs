namespace KSP_Mod_Manager
{
    partial class InstallDeinstallForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressLabel1 = new System.Windows.Forms.Label();
            this.allModProgress = new System.Windows.Forms.ProgressBar();
            this.progressLabel2 = new System.Windows.Forms.Label();
            this.thisModProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // progressLabel1
            // 
            this.progressLabel1.AutoSize = true;
            this.progressLabel1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressLabel1.Location = new System.Drawing.Point(12, 9);
            this.progressLabel1.Name = "progressLabel1";
            this.progressLabel1.Size = new System.Drawing.Size(108, 19);
            this.progressLabel1.TabIndex = 0;
            this.progressLabel1.Text = "Loading ...";
            // 
            // allModProgress
            // 
            this.allModProgress.Location = new System.Drawing.Point(12, 111);
            this.allModProgress.MarqueeAnimationSpeed = 50;
            this.allModProgress.Maximum = 100000;
            this.allModProgress.Name = "allModProgress";
            this.allModProgress.Size = new System.Drawing.Size(799, 36);
            this.allModProgress.Step = 1;
            this.allModProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.allModProgress.TabIndex = 2;
            // 
            // progressLabel2
            // 
            this.progressLabel2.AutoSize = true;
            this.progressLabel2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressLabel2.Location = new System.Drawing.Point(12, 89);
            this.progressLabel2.Name = "progressLabel2";
            this.progressLabel2.Size = new System.Drawing.Size(108, 19);
            this.progressLabel2.TabIndex = 3;
            this.progressLabel2.Text = "Loading ...";
            // 
            // thisModProgress
            // 
            this.thisModProgress.Location = new System.Drawing.Point(12, 31);
            this.thisModProgress.MarqueeAnimationSpeed = 50;
            this.thisModProgress.Name = "thisModProgress";
            this.thisModProgress.Size = new System.Drawing.Size(799, 36);
            this.thisModProgress.Step = 100;
            this.thisModProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.thisModProgress.TabIndex = 4;
            // 
            // InstallDeinstallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 161);
            this.Controls.Add(this.thisModProgress);
            this.Controls.Add(this.progressLabel2);
            this.Controls.Add(this.allModProgress);
            this.Controls.Add(this.progressLabel1);
            this.Cursor = System.Windows.Forms.Cursors.No;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "InstallDeinstallForm";
            this.Text = "InstallDeinstall_Form";
            this.Shown += new System.EventHandler(this.InstallDeinstallForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label progressLabel1;
        private System.Windows.Forms.ProgressBar allModProgress;
        private System.Windows.Forms.Label progressLabel2;
        private System.Windows.Forms.ProgressBar thisModProgress;
    }
}