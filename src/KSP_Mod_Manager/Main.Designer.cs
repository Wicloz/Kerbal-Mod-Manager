namespace KSP_Mod_Manager
{
    partial class Main
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
            this.installationBox = new System.Windows.Forms.ListBox();
            this.modFolderBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectModFolderButton = new System.Windows.Forms.Button();
            this.modBox = new System.Windows.Forms.ListBox();
            this.opNameBox = new System.Windows.Forms.TextBox();
            this.opSiteBox = new System.Windows.Forms.TextBox();
            this.opDlSiteBox = new System.Windows.Forms.TextBox();
            this.opInstallButton = new System.Windows.Forms.Button();
            this.opCanDownloadBox = new System.Windows.Forms.CheckBox();
            this.opDownloadButton = new System.Windows.Forms.Button();
            this.opCategoryBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.opIsFavoriteBox = new System.Windows.Forms.CheckBox();
            this.updateModFolderButton = new System.Windows.Forms.Button();
            this.checkUpdateButton = new System.Windows.Forms.Button();
            this.topButton1 = new System.Windows.Forms.Button();
            this.topButton2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.opCheckUpdateButton = new System.Windows.Forms.Button();
            this.addInstanceButton = new System.Windows.Forms.Button();
            this.removeInstanceButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // installationBox
            // 
            this.installationBox.FormattingEnabled = true;
            this.installationBox.Location = new System.Drawing.Point(15, 38);
            this.installationBox.Name = "installationBox";
            this.installationBox.Size = new System.Drawing.Size(94, 459);
            this.installationBox.TabIndex = 0;
            this.installationBox.Click += new System.EventHandler(this.installationBox_Click);
            this.installationBox.DoubleClick += new System.EventHandler(this.installationBox_DoubleClick);
            // 
            // modFolderBox
            // 
            this.modFolderBox.Enabled = false;
            this.modFolderBox.Location = new System.Drawing.Point(81, 12);
            this.modFolderBox.Name = "modFolderBox";
            this.modFolderBox.Size = new System.Drawing.Size(632, 20);
            this.modFolderBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mod Folder:";
            // 
            // selectModFolderButton
            // 
            this.selectModFolderButton.Location = new System.Drawing.Point(800, 12);
            this.selectModFolderButton.Name = "selectModFolderButton";
            this.selectModFolderButton.Size = new System.Drawing.Size(92, 20);
            this.selectModFolderButton.TabIndex = 4;
            this.selectModFolderButton.Text = "Select";
            this.selectModFolderButton.UseVisualStyleBackColor = true;
            this.selectModFolderButton.Click += new System.EventHandler(this.selectModFolderButton_Click);
            // 
            // modBox
            // 
            this.modBox.FormattingEnabled = true;
            this.modBox.Location = new System.Drawing.Point(116, 77);
            this.modBox.Name = "modBox";
            this.modBox.Size = new System.Drawing.Size(333, 498);
            this.modBox.TabIndex = 5;
            this.modBox.SelectedIndexChanged += new System.EventHandler(this.modBox_SelectedIndexChanged);
            this.modBox.DoubleClick += new System.EventHandler(this.modBox_DoubleClick);
            this.modBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.modBox_KeyPress);
            // 
            // opNameBox
            // 
            this.opNameBox.Location = new System.Drawing.Point(107, 19);
            this.opNameBox.Name = "opNameBox";
            this.opNameBox.Size = new System.Drawing.Size(324, 20);
            this.opNameBox.TabIndex = 6;
            this.opNameBox.TextChanged += new System.EventHandler(this.opNameBox_TextChanged);
            // 
            // opSiteBox
            // 
            this.opSiteBox.Location = new System.Drawing.Point(107, 72);
            this.opSiteBox.Name = "opSiteBox";
            this.opSiteBox.Size = new System.Drawing.Size(324, 20);
            this.opSiteBox.TabIndex = 8;
            this.opSiteBox.TextChanged += new System.EventHandler(this.opSiteBox_TextChanged);
            // 
            // opDlSiteBox
            // 
            this.opDlSiteBox.Location = new System.Drawing.Point(107, 98);
            this.opDlSiteBox.Name = "opDlSiteBox";
            this.opDlSiteBox.Size = new System.Drawing.Size(324, 20);
            this.opDlSiteBox.TabIndex = 9;
            this.opDlSiteBox.TextChanged += new System.EventHandler(this.opDlSiteBox_TextChanged);
            // 
            // opInstallButton
            // 
            this.opInstallButton.Location = new System.Drawing.Point(455, 194);
            this.opInstallButton.Name = "opInstallButton";
            this.opInstallButton.Size = new System.Drawing.Size(437, 47);
            this.opInstallButton.TabIndex = 10;
            this.opInstallButton.Text = "Install Mod / Deinstall Mod";
            this.opInstallButton.UseVisualStyleBackColor = true;
            this.opInstallButton.Click += new System.EventHandler(this.opInstallButton_Click);
            // 
            // opCanDownloadBox
            // 
            this.opCanDownloadBox.AutoSize = true;
            this.opCanDownloadBox.Location = new System.Drawing.Point(9, 62);
            this.opCanDownloadBox.Name = "opCanDownloadBox";
            this.opCanDownloadBox.Size = new System.Drawing.Size(107, 17);
            this.opCanDownloadBox.TabIndex = 11;
            this.opCanDownloadBox.Text = "Update Available";
            this.opCanDownloadBox.UseVisualStyleBackColor = true;
            this.opCanDownloadBox.CheckedChanged += new System.EventHandler(this.opCanDownloadBox_CheckedChanged);
            // 
            // opDownloadButton
            // 
            this.opDownloadButton.Location = new System.Drawing.Point(9, 85);
            this.opDownloadButton.Name = "opDownloadButton";
            this.opDownloadButton.Size = new System.Drawing.Size(422, 37);
            this.opDownloadButton.TabIndex = 12;
            this.opDownloadButton.Text = "Download Mod";
            this.opDownloadButton.UseVisualStyleBackColor = true;
            // 
            // opCategoryBox
            // 
            this.opCategoryBox.FormattingEnabled = true;
            this.opCategoryBox.Location = new System.Drawing.Point(107, 45);
            this.opCategoryBox.Name = "opCategoryBox";
            this.opCategoryBox.Size = new System.Drawing.Size(324, 21);
            this.opCategoryBox.TabIndex = 13;
            this.opCategoryBox.TextChanged += new System.EventHandler(this.opCategoryBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Mod Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Category:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Website:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Download Link:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.opIsFavoriteBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.opNameBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.opCategoryBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.opSiteBox);
            this.groupBox1.Controls.Add(this.opDlSiteBox);
            this.groupBox1.Location = new System.Drawing.Point(455, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 150);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings for Mod";
            // 
            // opIsFavoriteBox
            // 
            this.opIsFavoriteBox.AutoSize = true;
            this.opIsFavoriteBox.Location = new System.Drawing.Point(107, 124);
            this.opIsFavoriteBox.Name = "opIsFavoriteBox";
            this.opIsFavoriteBox.Size = new System.Drawing.Size(75, 17);
            this.opIsFavoriteBox.TabIndex = 18;
            this.opIsFavoriteBox.Text = "Is Favorite";
            this.opIsFavoriteBox.UseVisualStyleBackColor = true;
            this.opIsFavoriteBox.CheckedChanged += new System.EventHandler(this.opIsFavoriteBox_CheckedChanged);
            // 
            // updateModFolderButton
            // 
            this.updateModFolderButton.Location = new System.Drawing.Point(719, 12);
            this.updateModFolderButton.Name = "updateModFolderButton";
            this.updateModFolderButton.Size = new System.Drawing.Size(75, 20);
            this.updateModFolderButton.TabIndex = 19;
            this.updateModFolderButton.Text = "Update";
            this.updateModFolderButton.UseVisualStyleBackColor = true;
            this.updateModFolderButton.Click += new System.EventHandler(this.updateModFolderButton_Click);
            // 
            // checkUpdateButton
            // 
            this.checkUpdateButton.Location = new System.Drawing.Point(455, 521);
            this.checkUpdateButton.Name = "checkUpdateButton";
            this.checkUpdateButton.Size = new System.Drawing.Size(437, 54);
            this.checkUpdateButton.TabIndex = 20;
            this.checkUpdateButton.Text = "Check for updates";
            this.checkUpdateButton.UseVisualStyleBackColor = true;
            this.checkUpdateButton.Click += new System.EventHandler(this.checkUpdateButton_Click);
            // 
            // topButton1
            // 
            this.topButton1.Location = new System.Drawing.Point(116, 38);
            this.topButton1.Name = "topButton1";
            this.topButton1.Size = new System.Drawing.Size(166, 33);
            this.topButton1.TabIndex = 21;
            this.topButton1.Text = "Deinstall all mods";
            this.topButton1.UseVisualStyleBackColor = true;
            this.topButton1.Click += new System.EventHandler(this.topButton1_Click);
            // 
            // topButton2
            // 
            this.topButton2.Location = new System.Drawing.Point(288, 38);
            this.topButton2.Name = "topButton2";
            this.topButton2.Size = new System.Drawing.Size(161, 33);
            this.topButton2.TabIndex = 22;
            this.topButton2.Text = "Install all Favorites";
            this.topButton2.UseVisualStyleBackColor = true;
            this.topButton2.Click += new System.EventHandler(this.topButton2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.opCheckUpdateButton);
            this.groupBox2.Controls.Add(this.opDownloadButton);
            this.groupBox2.Controls.Add(this.opCanDownloadBox);
            this.groupBox2.Location = new System.Drawing.Point(455, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 131);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Updating";
            // 
            // opCheckUpdateButton
            // 
            this.opCheckUpdateButton.Location = new System.Drawing.Point(9, 19);
            this.opCheckUpdateButton.Name = "opCheckUpdateButton";
            this.opCheckUpdateButton.Size = new System.Drawing.Size(422, 37);
            this.opCheckUpdateButton.TabIndex = 13;
            this.opCheckUpdateButton.Text = "Check For Update";
            this.opCheckUpdateButton.UseVisualStyleBackColor = true;
            // 
            // addInstanceButton
            // 
            this.addInstanceButton.Location = new System.Drawing.Point(15, 542);
            this.addInstanceButton.Name = "addInstanceButton";
            this.addInstanceButton.Size = new System.Drawing.Size(94, 32);
            this.addInstanceButton.TabIndex = 24;
            this.addInstanceButton.Text = "Add Instance";
            this.addInstanceButton.UseVisualStyleBackColor = true;
            this.addInstanceButton.Click += new System.EventHandler(this.addInstanceButton_Click);
            // 
            // removeInstanceButton
            // 
            this.removeInstanceButton.Location = new System.Drawing.Point(15, 503);
            this.removeInstanceButton.Name = "removeInstanceButton";
            this.removeInstanceButton.Size = new System.Drawing.Size(94, 33);
            this.removeInstanceButton.TabIndex = 25;
            this.removeInstanceButton.Text = "Delete Instance";
            this.removeInstanceButton.UseVisualStyleBackColor = true;
            this.removeInstanceButton.Click += new System.EventHandler(this.removeInstanceButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 586);
            this.Controls.Add(this.removeInstanceButton);
            this.Controls.Add(this.addInstanceButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.topButton2);
            this.Controls.Add(this.topButton1);
            this.Controls.Add(this.checkUpdateButton);
            this.Controls.Add(this.updateModFolderButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.opInstallButton);
            this.Controls.Add(this.modBox);
            this.Controls.Add(this.selectModFolderButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modFolderBox);
            this.Controls.Add(this.installationBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Kerbal Mod Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox installationBox;
        private System.Windows.Forms.TextBox modFolderBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectModFolderButton;
        private System.Windows.Forms.ListBox modBox;
        private System.Windows.Forms.TextBox opNameBox;
        private System.Windows.Forms.TextBox opSiteBox;
        private System.Windows.Forms.TextBox opDlSiteBox;
        private System.Windows.Forms.Button opInstallButton;
        private System.Windows.Forms.CheckBox opCanDownloadBox;
        private System.Windows.Forms.Button opDownloadButton;
        private System.Windows.Forms.ComboBox opCategoryBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button updateModFolderButton;
        private System.Windows.Forms.Button checkUpdateButton;
        private System.Windows.Forms.Button topButton1;
        private System.Windows.Forms.Button topButton2;
        private System.Windows.Forms.CheckBox opIsFavoriteBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button opCheckUpdateButton;
        private System.Windows.Forms.Button addInstanceButton;
        private System.Windows.Forms.Button removeInstanceButton;
    }
}

