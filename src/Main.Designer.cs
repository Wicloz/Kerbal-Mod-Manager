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
            this.components = new System.ComponentModel.Container();
            this.installationBox = new System.Windows.Forms.ListBox();
            this.modFolderBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectModFolderButton = new System.Windows.Forms.Button();
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
            this.editCategoryButton = new System.Windows.Forms.Button();
            this.opAddCategoryButton = new System.Windows.Forms.Button();
            this.opOpenSiteButton = new System.Windows.Forms.Button();
            this.opIsFavoriteBox = new System.Windows.Forms.CheckBox();
            this.updateModFolderButton = new System.Windows.Forms.Button();
            this.checkUpdateButton = new System.Windows.Forms.Button();
            this.topButton1 = new System.Windows.Forms.Button();
            this.topButton2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.opReinstallButton = new System.Windows.Forms.Button();
            this.opCheckUpdateButton = new System.Windows.Forms.Button();
            this.addInstanceButton = new System.Windows.Forms.Button();
            this.removeInstanceButton = new System.Windows.Forms.Button();
            this.downloadModButton = new System.Windows.Forms.Button();
            this.openLogButton = new System.Windows.Forms.Button();
            this.deleteModButton = new System.Windows.Forms.Button();
            this.reinstallAllButton = new System.Windows.Forms.Button();
            this.downloadedListView = new System.Windows.Forms.ListView();
            this.modName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.category = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.updateStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.favStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.installedListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.opGoogleButton = new System.Windows.Forms.Button();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // installationBox
            // 
            this.installationBox.FormattingEnabled = true;
            this.installationBox.Location = new System.Drawing.Point(12, 38);
            this.installationBox.Name = "installationBox";
            this.installationBox.Size = new System.Drawing.Size(129, 550);
            this.installationBox.TabIndex = 0;
            this.installationBox.Click += new System.EventHandler(this.installationBox_Click);
            this.installationBox.DoubleClick += new System.EventHandler(this.installationBox_DoubleClick);
            // 
            // modFolderBox
            // 
            this.modFolderBox.Enabled = false;
            this.modFolderBox.Location = new System.Drawing.Point(81, 12);
            this.modFolderBox.Name = "modFolderBox";
            this.modFolderBox.Size = new System.Drawing.Size(892, 20);
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
            this.selectModFolderButton.Location = new System.Drawing.Point(1060, 12);
            this.selectModFolderButton.Name = "selectModFolderButton";
            this.selectModFolderButton.Size = new System.Drawing.Size(92, 20);
            this.selectModFolderButton.TabIndex = 4;
            this.selectModFolderButton.Text = "Select";
            this.selectModFolderButton.UseVisualStyleBackColor = true;
            this.selectModFolderButton.Click += new System.EventHandler(this.selectModFolderButton_Click);
            // 
            // opNameBox
            // 
            this.opNameBox.Location = new System.Drawing.Point(107, 19);
            this.opNameBox.Name = "opNameBox";
            this.opNameBox.Size = new System.Drawing.Size(323, 20);
            this.opNameBox.TabIndex = 6;
            this.opNameBox.TextChanged += new System.EventHandler(this.opNameBox_TextChanged);
            // 
            // opSiteBox
            // 
            this.opSiteBox.Location = new System.Drawing.Point(107, 72);
            this.opSiteBox.Name = "opSiteBox";
            this.opSiteBox.Size = new System.Drawing.Size(330, 20);
            this.opSiteBox.TabIndex = 8;
            this.opSiteBox.TextChanged += new System.EventHandler(this.opSiteBox_TextChanged);
            // 
            // opDlSiteBox
            // 
            this.opDlSiteBox.Location = new System.Drawing.Point(107, 98);
            this.opDlSiteBox.Name = "opDlSiteBox";
            this.opDlSiteBox.Size = new System.Drawing.Size(380, 20);
            this.opDlSiteBox.TabIndex = 9;
            this.opDlSiteBox.TextChanged += new System.EventHandler(this.opDlSiteBox_TextChanged);
            // 
            // opInstallButton
            // 
            this.opInstallButton.Location = new System.Drawing.Point(659, 194);
            this.opInstallButton.Name = "opInstallButton";
            this.opInstallButton.Size = new System.Drawing.Size(493, 47);
            this.opInstallButton.TabIndex = 10;
            this.opInstallButton.Text = "Install Mod / Deinstall Mod";
            this.opInstallButton.UseVisualStyleBackColor = true;
            this.opInstallButton.Click += new System.EventHandler(this.opInstallButton_Click);
            // 
            // opCanDownloadBox
            // 
            this.opCanDownloadBox.AutoSize = true;
            this.opCanDownloadBox.Location = new System.Drawing.Point(6, 62);
            this.opCanDownloadBox.Name = "opCanDownloadBox";
            this.opCanDownloadBox.Size = new System.Drawing.Size(107, 17);
            this.opCanDownloadBox.TabIndex = 11;
            this.opCanDownloadBox.Text = "Update Available";
            this.opCanDownloadBox.UseVisualStyleBackColor = true;
            this.opCanDownloadBox.CheckedChanged += new System.EventHandler(this.opCanDownloadBox_CheckedChanged);
            // 
            // opDownloadButton
            // 
            this.opDownloadButton.Location = new System.Drawing.Point(6, 85);
            this.opDownloadButton.Name = "opDownloadButton";
            this.opDownloadButton.Size = new System.Drawing.Size(481, 37);
            this.opDownloadButton.TabIndex = 12;
            this.opDownloadButton.Text = "Update Mod";
            this.opDownloadButton.UseVisualStyleBackColor = true;
            this.opDownloadButton.Click += new System.EventHandler(this.opDownloadButton_Click);
            // 
            // opCategoryBox
            // 
            this.opCategoryBox.FormattingEnabled = true;
            this.opCategoryBox.Items.AddRange(new object[] {
            "API",
            "Core",
            "Parts",
            "Tools",
            "Plugins",
            "Graphic Mods",
            "Sound Mods",
            "Graphic Install Packs"});
            this.opCategoryBox.Location = new System.Drawing.Point(107, 45);
            this.opCategoryBox.Name = "opCategoryBox";
            this.opCategoryBox.Size = new System.Drawing.Size(280, 21);
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
            this.groupBox1.Controls.Add(this.opGoogleButton);
            this.groupBox1.Controls.Add(this.editCategoryButton);
            this.groupBox1.Controls.Add(this.opAddCategoryButton);
            this.groupBox1.Controls.Add(this.opOpenSiteButton);
            this.groupBox1.Controls.Add(this.opIsFavoriteBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.opNameBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.opCategoryBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.opSiteBox);
            this.groupBox1.Controls.Add(this.opDlSiteBox);
            this.groupBox1.Location = new System.Drawing.Point(659, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 150);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings for Mod";
            // 
            // editCategoryButton
            // 
            this.editCategoryButton.Location = new System.Drawing.Point(443, 45);
            this.editCategoryButton.Name = "editCategoryButton";
            this.editCategoryButton.Size = new System.Drawing.Size(44, 21);
            this.editCategoryButton.TabIndex = 20;
            this.editCategoryButton.Text = "Edit";
            this.editCategoryButton.UseVisualStyleBackColor = true;
            this.editCategoryButton.Click += new System.EventHandler(this.opAddCategoryButton_Click);
            // 
            // opAddCategoryButton
            // 
            this.opAddCategoryButton.Location = new System.Drawing.Point(393, 45);
            this.opAddCategoryButton.Name = "opAddCategoryButton";
            this.opAddCategoryButton.Size = new System.Drawing.Size(44, 21);
            this.opAddCategoryButton.TabIndex = 20;
            this.opAddCategoryButton.Text = "Add";
            this.opAddCategoryButton.UseVisualStyleBackColor = true;
            this.opAddCategoryButton.Click += new System.EventHandler(this.opAddCategoryButton_Click_1);
            // 
            // opOpenSiteButton
            // 
            this.opOpenSiteButton.Location = new System.Drawing.Point(443, 72);
            this.opOpenSiteButton.Name = "opOpenSiteButton";
            this.opOpenSiteButton.Size = new System.Drawing.Size(44, 20);
            this.opOpenSiteButton.TabIndex = 19;
            this.opOpenSiteButton.Text = "Open";
            this.opOpenSiteButton.UseVisualStyleBackColor = true;
            this.opOpenSiteButton.Click += new System.EventHandler(this.opOpenSite_Click);
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
            this.updateModFolderButton.Location = new System.Drawing.Point(979, 12);
            this.updateModFolderButton.Name = "updateModFolderButton";
            this.updateModFolderButton.Size = new System.Drawing.Size(75, 20);
            this.updateModFolderButton.TabIndex = 19;
            this.updateModFolderButton.Text = "Update";
            this.updateModFolderButton.UseVisualStyleBackColor = true;
            this.updateModFolderButton.Click += new System.EventHandler(this.updateModFolderButton_Click);
            // 
            // checkUpdateButton
            // 
            this.checkUpdateButton.Location = new System.Drawing.Point(659, 609);
            this.checkUpdateButton.Name = "checkUpdateButton";
            this.checkUpdateButton.Size = new System.Drawing.Size(493, 54);
            this.checkUpdateButton.TabIndex = 20;
            this.checkUpdateButton.Text = "Check For Updates On All Mods";
            this.checkUpdateButton.UseVisualStyleBackColor = true;
            this.checkUpdateButton.Click += new System.EventHandler(this.checkUpdateButton_Click);
            // 
            // topButton1
            // 
            this.topButton1.Location = new System.Drawing.Point(147, 38);
            this.topButton1.Name = "topButton1";
            this.topButton1.Size = new System.Drawing.Size(263, 34);
            this.topButton1.TabIndex = 21;
            this.topButton1.Text = "Deinstall all mods";
            this.topButton1.UseVisualStyleBackColor = true;
            this.topButton1.Click += new System.EventHandler(this.topButton1_Click);
            // 
            // topButton2
            // 
            this.topButton2.Location = new System.Drawing.Point(416, 38);
            this.topButton2.Name = "topButton2";
            this.topButton2.Size = new System.Drawing.Size(237, 34);
            this.topButton2.TabIndex = 22;
            this.topButton2.Text = "Install all Favorites";
            this.topButton2.UseVisualStyleBackColor = true;
            this.topButton2.Click += new System.EventHandler(this.topButton2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.opReinstallButton);
            this.groupBox2.Controls.Add(this.opCheckUpdateButton);
            this.groupBox2.Controls.Add(this.opDownloadButton);
            this.groupBox2.Controls.Add(this.opCanDownloadBox);
            this.groupBox2.Location = new System.Drawing.Point(659, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 173);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Updating";
            // 
            // opReinstallButton
            // 
            this.opReinstallButton.Location = new System.Drawing.Point(6, 128);
            this.opReinstallButton.Name = "opReinstallButton";
            this.opReinstallButton.Size = new System.Drawing.Size(481, 36);
            this.opReinstallButton.TabIndex = 14;
            this.opReinstallButton.Text = "Reinstall Mod";
            this.opReinstallButton.UseVisualStyleBackColor = true;
            this.opReinstallButton.Click += new System.EventHandler(this.opReinstallButton_Click);
            // 
            // opCheckUpdateButton
            // 
            this.opCheckUpdateButton.Location = new System.Drawing.Point(6, 19);
            this.opCheckUpdateButton.Name = "opCheckUpdateButton";
            this.opCheckUpdateButton.Size = new System.Drawing.Size(481, 37);
            this.opCheckUpdateButton.TabIndex = 13;
            this.opCheckUpdateButton.Text = "Check For Update";
            this.opCheckUpdateButton.UseVisualStyleBackColor = true;
            this.opCheckUpdateButton.Click += new System.EventHandler(this.opCheckUpdateButton_Click);
            // 
            // addInstanceButton
            // 
            this.addInstanceButton.Location = new System.Drawing.Point(12, 630);
            this.addInstanceButton.Name = "addInstanceButton";
            this.addInstanceButton.Size = new System.Drawing.Size(129, 33);
            this.addInstanceButton.TabIndex = 24;
            this.addInstanceButton.Text = "Add Instance";
            this.addInstanceButton.UseVisualStyleBackColor = true;
            this.addInstanceButton.Click += new System.EventHandler(this.addInstanceButton_Click);
            // 
            // removeInstanceButton
            // 
            this.removeInstanceButton.Location = new System.Drawing.Point(12, 594);
            this.removeInstanceButton.Name = "removeInstanceButton";
            this.removeInstanceButton.Size = new System.Drawing.Size(129, 33);
            this.removeInstanceButton.TabIndex = 25;
            this.removeInstanceButton.Text = "Delete Instance";
            this.removeInstanceButton.UseVisualStyleBackColor = true;
            this.removeInstanceButton.Click += new System.EventHandler(this.removeInstanceButton_Click);
            // 
            // downloadModButton
            // 
            this.downloadModButton.Location = new System.Drawing.Point(659, 550);
            this.downloadModButton.Name = "downloadModButton";
            this.downloadModButton.Size = new System.Drawing.Size(253, 53);
            this.downloadModButton.TabIndex = 26;
            this.downloadModButton.Text = "Update All Mods";
            this.downloadModButton.UseVisualStyleBackColor = true;
            this.downloadModButton.Click += new System.EventHandler(this.downloadModButton_Click);
            // 
            // openLogButton
            // 
            this.openLogButton.Location = new System.Drawing.Point(659, 508);
            this.openLogButton.Name = "openLogButton";
            this.openLogButton.Size = new System.Drawing.Size(493, 36);
            this.openLogButton.TabIndex = 27;
            this.openLogButton.Text = "Open Log";
            this.openLogButton.UseVisualStyleBackColor = true;
            this.openLogButton.Click += new System.EventHandler(this.openLogButton_Click);
            // 
            // deleteModButton
            // 
            this.deleteModButton.Location = new System.Drawing.Point(148, 631);
            this.deleteModButton.Name = "deleteModButton";
            this.deleteModButton.Size = new System.Drawing.Size(506, 32);
            this.deleteModButton.TabIndex = 28;
            this.deleteModButton.Text = "Delete Mod";
            this.deleteModButton.UseVisualStyleBackColor = true;
            this.deleteModButton.Click += new System.EventHandler(this.deleteMod_Click);
            // 
            // reinstallAllButton
            // 
            this.reinstallAllButton.Location = new System.Drawing.Point(918, 550);
            this.reinstallAllButton.Name = "reinstallAllButton";
            this.reinstallAllButton.Size = new System.Drawing.Size(234, 53);
            this.reinstallAllButton.TabIndex = 30;
            this.reinstallAllButton.Text = "Reinstall All Mods";
            this.reinstallAllButton.UseVisualStyleBackColor = true;
            this.reinstallAllButton.Click += new System.EventHandler(this.reinstallAllButton_Click);
            // 
            // downloadedListView
            // 
            this.downloadedListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.modName,
            this.category,
            this.updateStatus,
            this.favStatus});
            this.downloadedListView.ContextMenuStrip = this.contextMenuStrip1;
            this.downloadedListView.FullRowSelect = true;
            this.downloadedListView.GridLines = true;
            this.downloadedListView.Location = new System.Drawing.Point(147, 309);
            this.downloadedListView.Name = "downloadedListView";
            this.downloadedListView.Size = new System.Drawing.Size(506, 316);
            this.downloadedListView.TabIndex = 31;
            this.downloadedListView.UseCompatibleStateImageBehavior = false;
            this.downloadedListView.View = System.Windows.Forms.View.Details;
            this.downloadedListView.SelectedIndexChanged += new System.EventHandler(this.downloadedListView_SelectedIndexChanged);
            this.downloadedListView.DoubleClick += new System.EventHandler(this.downloadedListView_DoubleClick);
            this.downloadedListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.downloadedListView_KeyPress);
            // 
            // modName
            // 
            this.modName.Text = "Mod Name";
            this.modName.Width = 181;
            // 
            // category
            // 
            this.category.Text = "Category";
            this.category.Width = 99;
            // 
            // updateStatus
            // 
            this.updateStatus.Text = "Update Status";
            this.updateStatus.Width = 119;
            // 
            // favStatus
            // 
            this.favStatus.Text = "Is Favorite";
            this.favStatus.Width = 86;
            // 
            // installedListView
            // 
            this.installedListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.installedListView.FullRowSelect = true;
            this.installedListView.GridLines = true;
            this.installedListView.Location = new System.Drawing.Point(147, 78);
            this.installedListView.Name = "installedListView";
            this.installedListView.Size = new System.Drawing.Size(506, 225);
            this.installedListView.TabIndex = 32;
            this.installedListView.UseCompatibleStateImageBehavior = false;
            this.installedListView.View = System.Windows.Forms.View.Details;
            this.installedListView.SelectedIndexChanged += new System.EventHandler(this.installedListView_SelectedIndexChanged);
            this.installedListView.DoubleClick += new System.EventHandler(this.installedListView_DoubleClick);
            this.installedListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.installedListView_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mod Name";
            this.columnHeader1.Width = 181;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Category";
            this.columnHeader2.Width = 99;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Update Status";
            this.columnHeader3.Width = 119;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Is Favorite";
            this.columnHeader4.Width = 86;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 54);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // opGoogleButton
            // 
            this.opGoogleButton.Location = new System.Drawing.Point(436, 19);
            this.opGoogleButton.Name = "opGoogleButton";
            this.opGoogleButton.Size = new System.Drawing.Size(51, 20);
            this.opGoogleButton.TabIndex = 21;
            this.opGoogleButton.Text = "Google";
            this.opGoogleButton.UseVisualStyleBackColor = true;
            this.opGoogleButton.Click += new System.EventHandler(this.opGoogleButton_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Reset List";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 675);
            this.Controls.Add(this.installedListView);
            this.Controls.Add(this.downloadedListView);
            this.Controls.Add(this.reinstallAllButton);
            this.Controls.Add(this.deleteModButton);
            this.Controls.Add(this.openLogButton);
            this.Controls.Add(this.downloadModButton);
            this.Controls.Add(this.removeInstanceButton);
            this.Controls.Add(this.addInstanceButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.topButton2);
            this.Controls.Add(this.topButton1);
            this.Controls.Add(this.checkUpdateButton);
            this.Controls.Add(this.updateModFolderButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.opInstallButton);
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox installationBox;
        private System.Windows.Forms.TextBox modFolderBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectModFolderButton;
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
        private System.Windows.Forms.Button downloadModButton;
        private System.Windows.Forms.Button opOpenSiteButton;
        private System.Windows.Forms.Button openLogButton;
        private System.Windows.Forms.Button deleteModButton;
        private System.Windows.Forms.Button opReinstallButton;
        private System.Windows.Forms.Button editCategoryButton;
        private System.Windows.Forms.Button opAddCategoryButton;
        private System.Windows.Forms.Button reinstallAllButton;
        private System.Windows.Forms.ListView downloadedListView;
        private System.Windows.Forms.ColumnHeader modName;
        private System.Windows.Forms.ColumnHeader updateStatus;
        private System.Windows.Forms.ColumnHeader favStatus;
        private System.Windows.Forms.ColumnHeader category;
        private System.Windows.Forms.ListView installedListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button opGoogleButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

