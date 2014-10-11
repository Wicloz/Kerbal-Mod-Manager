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
            this.opGoogleButton = new System.Windows.Forms.Button();
            this.editCategoryButton = new System.Windows.Forms.Button();
            this.opAddCategoryButton = new System.Windows.Forms.Button();
            this.opOpenSiteButton = new System.Windows.Forms.Button();
            this.opIsFavoriteBox = new System.Windows.Forms.CheckBox();
            this.updateModFolderButton = new System.Windows.Forms.Button();
            this.checkUpdateButton = new System.Windows.Forms.Button();
            this.topButton1 = new System.Windows.Forms.Button();
            this.topButton2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.opCheckUpdateButton = new System.Windows.Forms.Button();
            this.opReinstallButton = new System.Windows.Forms.Button();
            this.addInstanceButton = new System.Windows.Forms.Button();
            this.removeInstanceButton = new System.Windows.Forms.Button();
            this.downloadModButton = new System.Windows.Forms.Button();
            this.openLogButton = new System.Windows.Forms.Button();
            this.deleteModButton = new System.Windows.Forms.Button();
            this.reinstallAllButton = new System.Windows.Forms.Button();
            this.modsListView = new System.Windows.Forms.ListView();
            this.modName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.category = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.installStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.updateStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.favStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteZipButton = new System.Windows.Forms.Button();
            this.addModButton = new System.Windows.Forms.Button();
            this.catButton1 = new System.Windows.Forms.Button();
            this.catButton2 = new System.Windows.Forms.Button();
            this.catButton4 = new System.Windows.Forms.Button();
            this.catButton3 = new System.Windows.Forms.Button();
            this.catButton6 = new System.Windows.Forms.Button();
            this.catButton5 = new System.Windows.Forms.Button();
            this.catButton7 = new System.Windows.Forms.Button();
            this.favAllButton = new System.Windows.Forms.Button();
            this.fdaButton = new System.Windows.Forms.Button();
            this.versionLocal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.versionOnline = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.installationBox.Size = new System.Drawing.Size(129, 667);
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
            this.opNameBox.Size = new System.Drawing.Size(489, 20);
            this.opNameBox.TabIndex = 6;
            this.opNameBox.TextChanged += new System.EventHandler(this.opNameBox_TextChanged);
            // 
            // opSiteBox
            // 
            this.opSiteBox.Location = new System.Drawing.Point(107, 72);
            this.opSiteBox.Name = "opSiteBox";
            this.opSiteBox.Size = new System.Drawing.Size(496, 20);
            this.opSiteBox.TabIndex = 8;
            this.opSiteBox.TextChanged += new System.EventHandler(this.opSiteBox_TextChanged);
            // 
            // opDlSiteBox
            // 
            this.opDlSiteBox.Location = new System.Drawing.Point(107, 98);
            this.opDlSiteBox.Name = "opDlSiteBox";
            this.opDlSiteBox.Size = new System.Drawing.Size(546, 20);
            this.opDlSiteBox.TabIndex = 9;
            this.opDlSiteBox.TextChanged += new System.EventHandler(this.opDlSiteBox_TextChanged);
            // 
            // opInstallButton
            // 
            this.opInstallButton.Location = new System.Drawing.Point(147, 194);
            this.opInstallButton.Name = "opInstallButton";
            this.opInstallButton.Size = new System.Drawing.Size(403, 43);
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
            this.opDownloadButton.Size = new System.Drawing.Size(130, 37);
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
            "Tools",
            "Plugins",
            "Graphic Install Packs",
            "Graphic Mods",
            "Sound Mods",
            "Parts"});
            this.opCategoryBox.Location = new System.Drawing.Point(107, 45);
            this.opCategoryBox.Name = "opCategoryBox";
            this.opCategoryBox.Size = new System.Drawing.Size(446, 21);
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
            this.groupBox1.Location = new System.Drawing.Point(147, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(659, 150);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings for Mod";
            // 
            // opGoogleButton
            // 
            this.opGoogleButton.Location = new System.Drawing.Point(602, 19);
            this.opGoogleButton.Name = "opGoogleButton";
            this.opGoogleButton.Size = new System.Drawing.Size(51, 20);
            this.opGoogleButton.TabIndex = 21;
            this.opGoogleButton.Text = "Google";
            this.opGoogleButton.UseVisualStyleBackColor = true;
            this.opGoogleButton.Click += new System.EventHandler(this.opGoogleButton_Click);
            // 
            // editCategoryButton
            // 
            this.editCategoryButton.Location = new System.Drawing.Point(609, 45);
            this.editCategoryButton.Name = "editCategoryButton";
            this.editCategoryButton.Size = new System.Drawing.Size(44, 21);
            this.editCategoryButton.TabIndex = 20;
            this.editCategoryButton.Text = "Edit";
            this.editCategoryButton.UseVisualStyleBackColor = true;
            this.editCategoryButton.Click += new System.EventHandler(this.opAddCategoryButton_Click);
            // 
            // opAddCategoryButton
            // 
            this.opAddCategoryButton.Location = new System.Drawing.Point(559, 45);
            this.opAddCategoryButton.Name = "opAddCategoryButton";
            this.opAddCategoryButton.Size = new System.Drawing.Size(44, 21);
            this.opAddCategoryButton.TabIndex = 20;
            this.opAddCategoryButton.Text = "Add";
            this.opAddCategoryButton.UseVisualStyleBackColor = true;
            this.opAddCategoryButton.Click += new System.EventHandler(this.opAddCategoryButton_Click_1);
            // 
            // opOpenSiteButton
            // 
            this.opOpenSiteButton.Location = new System.Drawing.Point(609, 72);
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
            this.checkUpdateButton.Location = new System.Drawing.Point(960, 217);
            this.checkUpdateButton.Name = "checkUpdateButton";
            this.checkUpdateButton.Size = new System.Drawing.Size(192, 53);
            this.checkUpdateButton.TabIndex = 20;
            this.checkUpdateButton.Text = "Check For Updates On All Mods";
            this.checkUpdateButton.UseVisualStyleBackColor = true;
            this.checkUpdateButton.Click += new System.EventHandler(this.checkUpdateButton_Click);
            // 
            // topButton1
            // 
            this.topButton1.Location = new System.Drawing.Point(960, 38);
            this.topButton1.Name = "topButton1";
            this.topButton1.Size = new System.Drawing.Size(192, 34);
            this.topButton1.TabIndex = 21;
            this.topButton1.Text = "Deinstall all mods";
            this.topButton1.UseVisualStyleBackColor = true;
            this.topButton1.Click += new System.EventHandler(this.topButton1_Click);
            // 
            // topButton2
            // 
            this.topButton2.Location = new System.Drawing.Point(960, 78);
            this.topButton2.Name = "topButton2";
            this.topButton2.Size = new System.Drawing.Size(192, 34);
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
            this.groupBox2.Location = new System.Drawing.Point(812, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 150);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Updating";
            // 
            // opCheckUpdateButton
            // 
            this.opCheckUpdateButton.Location = new System.Drawing.Point(6, 19);
            this.opCheckUpdateButton.Name = "opCheckUpdateButton";
            this.opCheckUpdateButton.Size = new System.Drawing.Size(130, 37);
            this.opCheckUpdateButton.TabIndex = 13;
            this.opCheckUpdateButton.Text = "Check For Update";
            this.opCheckUpdateButton.UseVisualStyleBackColor = true;
            this.opCheckUpdateButton.Click += new System.EventHandler(this.opCheckUpdateButton_Click);
            // 
            // opReinstallButton
            // 
            this.opReinstallButton.Location = new System.Drawing.Point(556, 194);
            this.opReinstallButton.Name = "opReinstallButton";
            this.opReinstallButton.Size = new System.Drawing.Size(398, 43);
            this.opReinstallButton.TabIndex = 14;
            this.opReinstallButton.Text = "Reinstall Mod";
            this.opReinstallButton.UseVisualStyleBackColor = true;
            this.opReinstallButton.Click += new System.EventHandler(this.opReinstallButton_Click);
            // 
            // addInstanceButton
            // 
            this.addInstanceButton.Location = new System.Drawing.Point(12, 757);
            this.addInstanceButton.Name = "addInstanceButton";
            this.addInstanceButton.Size = new System.Drawing.Size(129, 33);
            this.addInstanceButton.TabIndex = 24;
            this.addInstanceButton.Text = "Add Instance";
            this.addInstanceButton.UseVisualStyleBackColor = true;
            this.addInstanceButton.Click += new System.EventHandler(this.addInstanceButton_Click);
            // 
            // removeInstanceButton
            // 
            this.removeInstanceButton.Location = new System.Drawing.Point(12, 718);
            this.removeInstanceButton.Name = "removeInstanceButton";
            this.removeInstanceButton.Size = new System.Drawing.Size(129, 33);
            this.removeInstanceButton.TabIndex = 25;
            this.removeInstanceButton.Text = "Delete Instance";
            this.removeInstanceButton.UseVisualStyleBackColor = true;
            this.removeInstanceButton.Click += new System.EventHandler(this.removeInstanceButton_Click);
            // 
            // downloadModButton
            // 
            this.downloadModButton.Location = new System.Drawing.Point(960, 276);
            this.downloadModButton.Name = "downloadModButton";
            this.downloadModButton.Size = new System.Drawing.Size(192, 53);
            this.downloadModButton.TabIndex = 26;
            this.downloadModButton.Text = "Update All Mods";
            this.downloadModButton.UseVisualStyleBackColor = true;
            this.downloadModButton.Click += new System.EventHandler(this.downloadModButton_Click);
            // 
            // openLogButton
            // 
            this.openLogButton.Location = new System.Drawing.Point(960, 742);
            this.openLogButton.Name = "openLogButton";
            this.openLogButton.Size = new System.Drawing.Size(192, 48);
            this.openLogButton.TabIndex = 27;
            this.openLogButton.Text = "Open Log";
            this.openLogButton.UseVisualStyleBackColor = true;
            this.openLogButton.Click += new System.EventHandler(this.openLogButton_Click);
            // 
            // deleteModButton
            // 
            this.deleteModButton.Location = new System.Drawing.Point(147, 758);
            this.deleteModButton.Name = "deleteModButton";
            this.deleteModButton.Size = new System.Drawing.Size(248, 32);
            this.deleteModButton.TabIndex = 28;
            this.deleteModButton.Text = "Delete Mod";
            this.deleteModButton.UseVisualStyleBackColor = true;
            this.deleteModButton.Click += new System.EventHandler(this.deleteMod_Click);
            // 
            // reinstallAllButton
            // 
            this.reinstallAllButton.Location = new System.Drawing.Point(960, 118);
            this.reinstallAllButton.Name = "reinstallAllButton";
            this.reinstallAllButton.Size = new System.Drawing.Size(192, 34);
            this.reinstallAllButton.TabIndex = 30;
            this.reinstallAllButton.Text = "Reinstall All Mods";
            this.reinstallAllButton.UseVisualStyleBackColor = true;
            this.reinstallAllButton.Click += new System.EventHandler(this.reinstallAllButton_Click);
            // 
            // modsListView
            // 
            this.modsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.modName,
            this.category,
            this.installStatus,
            this.updateStatus,
            this.favStatus,
            this.versionLocal,
            this.versionOnline});
            this.modsListView.ContextMenuStrip = this.contextMenuStrip1;
            this.modsListView.FullRowSelect = true;
            this.modsListView.GridLines = true;
            this.modsListView.Location = new System.Drawing.Point(147, 268);
            this.modsListView.Name = "modsListView";
            this.modsListView.Size = new System.Drawing.Size(807, 484);
            this.modsListView.TabIndex = 31;
            this.modsListView.UseCompatibleStateImageBehavior = false;
            this.modsListView.View = System.Windows.Forms.View.Details;
            this.modsListView.SelectedIndexChanged += new System.EventHandler(this.downloadedListView_SelectedIndexChanged);
            this.modsListView.DoubleClick += new System.EventHandler(this.downloadedListView_DoubleClick);
            this.modsListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.downloadedListView_KeyPress);
            // 
            // modName
            // 
            this.modName.Text = "Mod Name";
            this.modName.Width = 228;
            // 
            // category
            // 
            this.category.Text = "Category";
            this.category.Width = 148;
            // 
            // installStatus
            // 
            this.installStatus.Text = "Installed";
            this.installStatus.Width = 70;
            // 
            // updateStatus
            // 
            this.updateStatus.Text = "Update Status";
            this.updateStatus.Width = 123;
            // 
            // favStatus
            // 
            this.favStatus.Text = "Is Favorite";
            this.favStatus.Width = 79;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(124, 32);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.toolStripMenuItem1.Text = "Reset List";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
            // 
            // deleteZipButton
            // 
            this.deleteZipButton.Location = new System.Drawing.Point(401, 758);
            this.deleteZipButton.Name = "deleteZipButton";
            this.deleteZipButton.Size = new System.Drawing.Size(266, 32);
            this.deleteZipButton.TabIndex = 32;
            this.deleteZipButton.Text = "Delete Zip File";
            this.deleteZipButton.UseVisualStyleBackColor = true;
            this.deleteZipButton.Click += new System.EventHandler(this.deleteZipButton_Click);
            // 
            // addModButton
            // 
            this.addModButton.Location = new System.Drawing.Point(673, 758);
            this.addModButton.Name = "addModButton";
            this.addModButton.Size = new System.Drawing.Size(281, 32);
            this.addModButton.TabIndex = 33;
            this.addModButton.Text = "Add Mod";
            this.addModButton.UseVisualStyleBackColor = true;
            // 
            // catButton1
            // 
            this.catButton1.Location = new System.Drawing.Point(147, 243);
            this.catButton1.Name = "catButton1";
            this.catButton1.Size = new System.Drawing.Size(110, 27);
            this.catButton1.TabIndex = 34;
            this.catButton1.Text = "All Mods";
            this.catButton1.UseVisualStyleBackColor = true;
            this.catButton1.Click += new System.EventHandler(this.catButton1_Click);
            // 
            // catButton2
            // 
            this.catButton2.Location = new System.Drawing.Point(263, 243);
            this.catButton2.Name = "catButton2";
            this.catButton2.Size = new System.Drawing.Size(110, 27);
            this.catButton2.TabIndex = 35;
            this.catButton2.Text = "Core";
            this.catButton2.UseVisualStyleBackColor = true;
            this.catButton2.Click += new System.EventHandler(this.catButton2_Click);
            // 
            // catButton4
            // 
            this.catButton4.Location = new System.Drawing.Point(495, 243);
            this.catButton4.Name = "catButton4";
            this.catButton4.Size = new System.Drawing.Size(110, 27);
            this.catButton4.TabIndex = 36;
            this.catButton4.Text = "Plugins";
            this.catButton4.UseVisualStyleBackColor = true;
            this.catButton4.Click += new System.EventHandler(this.catButton4_Click);
            // 
            // catButton3
            // 
            this.catButton3.Location = new System.Drawing.Point(379, 243);
            this.catButton3.Name = "catButton3";
            this.catButton3.Size = new System.Drawing.Size(110, 27);
            this.catButton3.TabIndex = 37;
            this.catButton3.Text = "Tools";
            this.catButton3.UseVisualStyleBackColor = true;
            this.catButton3.Click += new System.EventHandler(this.catButton3_Click);
            // 
            // catButton6
            // 
            this.catButton6.Location = new System.Drawing.Point(727, 243);
            this.catButton6.Name = "catButton6";
            this.catButton6.Size = new System.Drawing.Size(110, 27);
            this.catButton6.TabIndex = 38;
            this.catButton6.Text = "Base Parts";
            this.catButton6.UseVisualStyleBackColor = true;
            this.catButton6.Click += new System.EventHandler(this.catButton6_Click);
            // 
            // catButton5
            // 
            this.catButton5.Location = new System.Drawing.Point(611, 243);
            this.catButton5.Name = "catButton5";
            this.catButton5.Size = new System.Drawing.Size(110, 27);
            this.catButton5.TabIndex = 39;
            this.catButton5.Text = "Graphics";
            this.catButton5.UseVisualStyleBackColor = true;
            this.catButton5.Click += new System.EventHandler(this.catButton5_Click);
            // 
            // catButton7
            // 
            this.catButton7.Location = new System.Drawing.Point(843, 243);
            this.catButton7.Name = "catButton7";
            this.catButton7.Size = new System.Drawing.Size(110, 27);
            this.catButton7.TabIndex = 40;
            this.catButton7.Text = "Parts";
            this.catButton7.UseVisualStyleBackColor = true;
            this.catButton7.Click += new System.EventHandler(this.catButton7_Click);
            // 
            // favAllButton
            // 
            this.favAllButton.Location = new System.Drawing.Point(960, 158);
            this.favAllButton.Name = "favAllButton";
            this.favAllButton.Size = new System.Drawing.Size(192, 53);
            this.favAllButton.TabIndex = 41;
            this.favAllButton.Text = "Favorite All Mods";
            this.favAllButton.UseVisualStyleBackColor = true;
            this.favAllButton.Click += new System.EventHandler(this.favAllButton_Click);
            // 
            // fdaButton
            // 
            this.fdaButton.Location = new System.Drawing.Point(961, 336);
            this.fdaButton.Name = "fdaButton";
            this.fdaButton.Size = new System.Drawing.Size(191, 41);
            this.fdaButton.TabIndex = 42;
            this.fdaButton.Text = "Force Update All Mods";
            this.fdaButton.UseVisualStyleBackColor = true;
            this.fdaButton.Click += new System.EventHandler(this.fdaButton_Click);
            // 
            // versionLocal
            // 
            this.versionLocal.Text = "Local Version";
            this.versionLocal.Width = 76;
            // 
            // versionOnline
            // 
            this.versionOnline.Text = "Online Version";
            this.versionOnline.Width = 80;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 802);
            this.Controls.Add(this.fdaButton);
            this.Controls.Add(this.opReinstallButton);
            this.Controls.Add(this.favAllButton);
            this.Controls.Add(this.catButton7);
            this.Controls.Add(this.catButton5);
            this.Controls.Add(this.catButton6);
            this.Controls.Add(this.catButton3);
            this.Controls.Add(this.catButton4);
            this.Controls.Add(this.catButton2);
            this.Controls.Add(this.catButton1);
            this.Controls.Add(this.addModButton);
            this.Controls.Add(this.deleteZipButton);
            this.Controls.Add(this.modsListView);
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
        private System.Windows.Forms.ListView modsListView;
        private System.Windows.Forms.ColumnHeader modName;
        private System.Windows.Forms.ColumnHeader updateStatus;
        private System.Windows.Forms.ColumnHeader favStatus;
        private System.Windows.Forms.ColumnHeader category;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button opGoogleButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader installStatus;
        private System.Windows.Forms.Button deleteZipButton;
        private System.Windows.Forms.Button addModButton;
        private System.Windows.Forms.Button catButton1;
        private System.Windows.Forms.Button catButton2;
        private System.Windows.Forms.Button catButton4;
        private System.Windows.Forms.Button catButton3;
        private System.Windows.Forms.Button catButton6;
        private System.Windows.Forms.Button catButton5;
        private System.Windows.Forms.Button catButton7;
        private System.Windows.Forms.Button favAllButton;
        private System.Windows.Forms.Button fdaButton;
        private System.Windows.Forms.ColumnHeader versionLocal;
        private System.Windows.Forms.ColumnHeader versionOnline;
    }
}

