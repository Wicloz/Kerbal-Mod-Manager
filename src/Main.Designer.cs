namespace Kerbal_Mod_Manager
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.buttonOpenSite = new System.Windows.Forms.Button();
            this.buttonAddCat = new System.Windows.Forms.Button();
            this.buttonGoogle = new System.Windows.Forms.Button();
            this.textBoxSelDebug = new System.Windows.Forms.TextBox();
            this.textBoxSelWebsite = new System.Windows.Forms.TextBox();
            this.textBoxSelName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewMods = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonUpdateSelected = new System.Windows.Forms.Button();
            this.buttonCheckSelected = new System.Windows.Forms.Button();
            this.checkBoxCanUpdate = new System.Windows.Forms.CheckBox();
            this.buttonFindSelected = new System.Windows.Forms.Button();
            this.buttonInstallSelected = new System.Windows.Forms.Button();
            this.buttonReinstallSelected = new System.Windows.Forms.Button();
            this.buttonEditCat = new System.Windows.Forms.Button();
            this.buttonDeinstallAll = new System.Windows.Forms.Button();
            this.buttonFindAll = new System.Windows.Forms.Button();
            this.buttonCheckAll = new System.Windows.Forms.Button();
            this.buttonUpdateAll = new System.Windows.Forms.Button();
            this.buttonUpdateAllForce = new System.Windows.Forms.Button();
            this.buttonReinstallAll = new System.Windows.Forms.Button();
            this.buttonDeleteSelected = new System.Windows.Forms.Button();
            this.buttonOpenLog = new System.Windows.Forms.Button();
            this.buttonDeleteFolder = new System.Windows.Forms.Button();
            this.buttonDeleteKsp = new System.Windows.Forms.Button();
            this.buttonAddKsp = new System.Windows.Forms.Button();
            this.buttonAddFolder = new System.Windows.Forms.Button();
            this.listBoxModFolders = new System.Windows.Forms.ListBox();
            this.listBoxKspFolders = new System.Windows.Forms.ListBox();
            this.buttonSelectAllFolders = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBoxCategory);
            this.groupBox1.Controls.Add(this.buttonOpenSite);
            this.groupBox1.Controls.Add(this.buttonAddCat);
            this.groupBox1.Controls.Add(this.buttonGoogle);
            this.groupBox1.Controls.Add(this.textBoxSelDebug);
            this.groupBox1.Controls.Add(this.textBoxSelWebsite);
            this.groupBox1.Controls.Add(this.textBoxSelName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(251, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1089, 128);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mod Settings";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(97, 44);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(905, 21);
            this.comboBoxCategory.TabIndex = 11;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            this.comboBoxCategory.TextUpdate += new System.EventHandler(this.comboBoxCategory_TextUpdate);
            this.comboBoxCategory.Leave += new System.EventHandler(this.comboBoxCategory_Leave);
            // 
            // buttonOpenSite
            // 
            this.buttonOpenSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenSite.Location = new System.Drawing.Point(1008, 71);
            this.buttonOpenSite.Name = "buttonOpenSite";
            this.buttonOpenSite.Size = new System.Drawing.Size(75, 20);
            this.buttonOpenSite.TabIndex = 10;
            this.buttonOpenSite.Text = "Open";
            this.buttonOpenSite.UseVisualStyleBackColor = true;
            this.buttonOpenSite.Click += new System.EventHandler(this.buttonOpenSite_Click);
            // 
            // buttonAddCat
            // 
            this.buttonAddCat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddCat.Location = new System.Drawing.Point(1008, 44);
            this.buttonAddCat.Name = "buttonAddCat";
            this.buttonAddCat.Size = new System.Drawing.Size(75, 21);
            this.buttonAddCat.TabIndex = 9;
            this.buttonAddCat.Text = "Add";
            this.buttonAddCat.UseVisualStyleBackColor = true;
            this.buttonAddCat.Click += new System.EventHandler(this.buttonAddCat_Click);
            // 
            // buttonGoogle
            // 
            this.buttonGoogle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGoogle.Location = new System.Drawing.Point(1008, 19);
            this.buttonGoogle.Name = "buttonGoogle";
            this.buttonGoogle.Size = new System.Drawing.Size(75, 20);
            this.buttonGoogle.TabIndex = 8;
            this.buttonGoogle.Text = "Google";
            this.buttonGoogle.UseVisualStyleBackColor = true;
            this.buttonGoogle.Click += new System.EventHandler(this.buttonGoogle_Click);
            // 
            // textBoxSelDebug
            // 
            this.textBoxSelDebug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSelDebug.Location = new System.Drawing.Point(97, 97);
            this.textBoxSelDebug.Name = "textBoxSelDebug";
            this.textBoxSelDebug.ReadOnly = true;
            this.textBoxSelDebug.Size = new System.Drawing.Size(986, 20);
            this.textBoxSelDebug.TabIndex = 7;
            // 
            // textBoxSelWebsite
            // 
            this.textBoxSelWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSelWebsite.Location = new System.Drawing.Point(97, 71);
            this.textBoxSelWebsite.Name = "textBoxSelWebsite";
            this.textBoxSelWebsite.Size = new System.Drawing.Size(905, 20);
            this.textBoxSelWebsite.TabIndex = 6;
            this.textBoxSelWebsite.TextChanged += new System.EventHandler(this.textBoxSelWebsite_TextChanged);
            // 
            // textBoxSelName
            // 
            this.textBoxSelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSelName.Location = new System.Drawing.Point(97, 19);
            this.textBoxSelName.Name = "textBoxSelName";
            this.textBoxSelName.Size = new System.Drawing.Size(905, 20);
            this.textBoxSelName.TabIndex = 4;
            this.textBoxSelName.TextChanged += new System.EventHandler(this.textBoxSelName_TextChanged);
            this.textBoxSelName.Leave += new System.EventHandler(this.textBoxSelName_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Download Site:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Website:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Category:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mod Name:";
            // 
            // listViewMods
            // 
            this.listViewMods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMods.CheckBoxes = true;
            this.listViewMods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.listViewMods.FullRowSelect = true;
            this.listViewMods.GridLines = true;
            this.listViewMods.HideSelection = false;
            this.listViewMods.Location = new System.Drawing.Point(251, 191);
            this.listViewMods.MultiSelect = false;
            this.listViewMods.Name = "listViewMods";
            this.listViewMods.Size = new System.Drawing.Size(1235, 391);
            this.listViewMods.TabIndex = 3;
            this.listViewMods.UseCompatibleStateImageBehavior = false;
            this.listViewMods.View = System.Windows.Forms.View.Details;
            this.listViewMods.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewMods_ItemChecked);
            this.listViewMods.SelectedIndexChanged += new System.EventHandler(this.listViewMods_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mod Name";
            this.columnHeader1.Width = 189;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Category";
            this.columnHeader2.Width = 89;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Link Status";
            this.columnHeader3.Width = 111;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Local Version";
            this.columnHeader4.Width = 89;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Latest Version";
            this.columnHeader5.Width = 93;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Update Status";
            this.columnHeader6.Width = 142;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 96;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Width = 107;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Width = 98;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Width = 99;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Width = 101;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonUpdateSelected);
            this.groupBox2.Controls.Add(this.buttonCheckSelected);
            this.groupBox2.Controls.Add(this.checkBoxCanUpdate);
            this.groupBox2.Controls.Add(this.buttonFindSelected);
            this.groupBox2.Location = new System.Drawing.Point(1346, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 128);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mod Actions";
            // 
            // buttonUpdateSelected
            // 
            this.buttonUpdateSelected.Location = new System.Drawing.Point(6, 96);
            this.buttonUpdateSelected.Name = "buttonUpdateSelected";
            this.buttonUpdateSelected.Size = new System.Drawing.Size(128, 20);
            this.buttonUpdateSelected.TabIndex = 6;
            this.buttonUpdateSelected.Text = "Update Mod";
            this.buttonUpdateSelected.UseVisualStyleBackColor = true;
            this.buttonUpdateSelected.Click += new System.EventHandler(this.buttonUpdateSelected_Click);
            // 
            // buttonCheckSelected
            // 
            this.buttonCheckSelected.Location = new System.Drawing.Point(6, 70);
            this.buttonCheckSelected.Name = "buttonCheckSelected";
            this.buttonCheckSelected.Size = new System.Drawing.Size(128, 20);
            this.buttonCheckSelected.TabIndex = 2;
            this.buttonCheckSelected.Text = "Check For Updates";
            this.buttonCheckSelected.UseVisualStyleBackColor = true;
            this.buttonCheckSelected.Click += new System.EventHandler(this.buttonCheckSelected_Click);
            // 
            // checkBoxCanUpdate
            // 
            this.checkBoxCanUpdate.AutoSize = true;
            this.checkBoxCanUpdate.Location = new System.Drawing.Point(6, 18);
            this.checkBoxCanUpdate.Name = "checkBoxCanUpdate";
            this.checkBoxCanUpdate.Size = new System.Drawing.Size(83, 17);
            this.checkBoxCanUpdate.TabIndex = 1;
            this.checkBoxCanUpdate.Text = "Can Update";
            this.checkBoxCanUpdate.UseVisualStyleBackColor = true;
            this.checkBoxCanUpdate.CheckedChanged += new System.EventHandler(this.checkBoxCanUpdate_CheckedChanged);
            // 
            // buttonFindSelected
            // 
            this.buttonFindSelected.Location = new System.Drawing.Point(6, 43);
            this.buttonFindSelected.Name = "buttonFindSelected";
            this.buttonFindSelected.Size = new System.Drawing.Size(128, 22);
            this.buttonFindSelected.TabIndex = 0;
            this.buttonFindSelected.Text = "Find Website";
            this.buttonFindSelected.UseVisualStyleBackColor = true;
            this.buttonFindSelected.Click += new System.EventHandler(this.buttonFindSelected_Click);
            // 
            // buttonInstallSelected
            // 
            this.buttonInstallSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInstallSelected.Location = new System.Drawing.Point(251, 147);
            this.buttonInstallSelected.Name = "buttonInstallSelected";
            this.buttonInstallSelected.Size = new System.Drawing.Size(982, 38);
            this.buttonInstallSelected.TabIndex = 6;
            this.buttonInstallSelected.Text = "Install/Deinstall Mod";
            this.buttonInstallSelected.UseVisualStyleBackColor = true;
            // 
            // buttonReinstallSelected
            // 
            this.buttonReinstallSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReinstallSelected.Location = new System.Drawing.Point(1239, 147);
            this.buttonReinstallSelected.Name = "buttonReinstallSelected";
            this.buttonReinstallSelected.Size = new System.Drawing.Size(247, 38);
            this.buttonReinstallSelected.TabIndex = 7;
            this.buttonReinstallSelected.Text = "Reinstall Mod";
            this.buttonReinstallSelected.UseVisualStyleBackColor = true;
            // 
            // buttonEditCat
            // 
            this.buttonEditCat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditCat.Location = new System.Drawing.Point(1492, 12);
            this.buttonEditCat.Name = "buttonEditCat";
            this.buttonEditCat.Size = new System.Drawing.Size(158, 39);
            this.buttonEditCat.TabIndex = 10;
            this.buttonEditCat.Text = "Edit Categories";
            this.buttonEditCat.UseVisualStyleBackColor = true;
            this.buttonEditCat.Click += new System.EventHandler(this.buttonEditCat_Click);
            // 
            // buttonDeinstallAll
            // 
            this.buttonDeinstallAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeinstallAll.Location = new System.Drawing.Point(1492, 57);
            this.buttonDeinstallAll.Name = "buttonDeinstallAll";
            this.buttonDeinstallAll.Size = new System.Drawing.Size(158, 39);
            this.buttonDeinstallAll.TabIndex = 11;
            this.buttonDeinstallAll.Text = "Deinstall All Mods";
            this.buttonDeinstallAll.UseVisualStyleBackColor = true;
            // 
            // buttonFindAll
            // 
            this.buttonFindAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindAll.Location = new System.Drawing.Point(1492, 145);
            this.buttonFindAll.Name = "buttonFindAll";
            this.buttonFindAll.Size = new System.Drawing.Size(158, 38);
            this.buttonFindAll.TabIndex = 12;
            this.buttonFindAll.Text = "Find All Websites";
            this.buttonFindAll.UseVisualStyleBackColor = true;
            this.buttonFindAll.Click += new System.EventHandler(this.buttonFindAll_Click);
            // 
            // buttonCheckAll
            // 
            this.buttonCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheckAll.Location = new System.Drawing.Point(1492, 189);
            this.buttonCheckAll.Name = "buttonCheckAll";
            this.buttonCheckAll.Size = new System.Drawing.Size(158, 39);
            this.buttonCheckAll.TabIndex = 13;
            this.buttonCheckAll.Text = "Check All Updates";
            this.buttonCheckAll.UseVisualStyleBackColor = true;
            this.buttonCheckAll.Click += new System.EventHandler(this.buttonCheckAll_Click);
            // 
            // buttonUpdateAll
            // 
            this.buttonUpdateAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateAll.Location = new System.Drawing.Point(1492, 234);
            this.buttonUpdateAll.Name = "buttonUpdateAll";
            this.buttonUpdateAll.Size = new System.Drawing.Size(158, 39);
            this.buttonUpdateAll.TabIndex = 14;
            this.buttonUpdateAll.Text = "Update All Mods";
            this.buttonUpdateAll.UseVisualStyleBackColor = true;
            this.buttonUpdateAll.Click += new System.EventHandler(this.buttonUpdateAll_Click);
            // 
            // buttonUpdateAllForce
            // 
            this.buttonUpdateAllForce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateAllForce.Location = new System.Drawing.Point(1492, 279);
            this.buttonUpdateAllForce.Name = "buttonUpdateAllForce";
            this.buttonUpdateAllForce.Size = new System.Drawing.Size(158, 39);
            this.buttonUpdateAllForce.TabIndex = 15;
            this.buttonUpdateAllForce.Text = "Force-Update All Mods";
            this.buttonUpdateAllForce.UseVisualStyleBackColor = true;
            this.buttonUpdateAllForce.Click += new System.EventHandler(this.buttonUpdateAllForce_Click);
            // 
            // buttonReinstallAll
            // 
            this.buttonReinstallAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReinstallAll.Location = new System.Drawing.Point(1492, 102);
            this.buttonReinstallAll.Name = "buttonReinstallAll";
            this.buttonReinstallAll.Size = new System.Drawing.Size(158, 37);
            this.buttonReinstallAll.TabIndex = 16;
            this.buttonReinstallAll.Text = "Reinstall All Mods";
            this.buttonReinstallAll.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteSelected
            // 
            this.buttonDeleteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteSelected.Location = new System.Drawing.Point(251, 588);
            this.buttonDeleteSelected.Name = "buttonDeleteSelected";
            this.buttonDeleteSelected.Size = new System.Drawing.Size(1235, 38);
            this.buttonDeleteSelected.TabIndex = 17;
            this.buttonDeleteSelected.Text = "Delete Mod";
            this.buttonDeleteSelected.UseVisualStyleBackColor = true;
            this.buttonDeleteSelected.Click += new System.EventHandler(this.buttonDeleteSelected_Click);
            // 
            // buttonOpenLog
            // 
            this.buttonOpenLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenLog.Location = new System.Drawing.Point(1492, 560);
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.Size = new System.Drawing.Size(158, 66);
            this.buttonOpenLog.TabIndex = 18;
            this.buttonOpenLog.Text = "Open Log";
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteFolder
            // 
            this.buttonDeleteFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteFolder.Location = new System.Drawing.Point(131, 588);
            this.buttonDeleteFolder.Name = "buttonDeleteFolder";
            this.buttonDeleteFolder.Size = new System.Drawing.Size(113, 38);
            this.buttonDeleteFolder.TabIndex = 19;
            this.buttonDeleteFolder.Text = "Remove Folder";
            this.buttonDeleteFolder.UseVisualStyleBackColor = true;
            this.buttonDeleteFolder.Click += new System.EventHandler(this.buttonDeleteFolder_Click);
            // 
            // buttonDeleteKsp
            // 
            this.buttonDeleteKsp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteKsp.Location = new System.Drawing.Point(13, 588);
            this.buttonDeleteKsp.Name = "buttonDeleteKsp";
            this.buttonDeleteKsp.Size = new System.Drawing.Size(112, 37);
            this.buttonDeleteKsp.TabIndex = 20;
            this.buttonDeleteKsp.Text = "Remove Instance";
            this.buttonDeleteKsp.UseVisualStyleBackColor = true;
            this.buttonDeleteKsp.Click += new System.EventHandler(this.buttonDeleteKsp_Click);
            // 
            // buttonAddKsp
            // 
            this.buttonAddKsp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddKsp.Location = new System.Drawing.Point(13, 545);
            this.buttonAddKsp.Name = "buttonAddKsp";
            this.buttonAddKsp.Size = new System.Drawing.Size(112, 37);
            this.buttonAddKsp.TabIndex = 21;
            this.buttonAddKsp.Text = "Add Instance";
            this.buttonAddKsp.UseVisualStyleBackColor = true;
            this.buttonAddKsp.Click += new System.EventHandler(this.buttonAddKsp_Click);
            // 
            // buttonAddFolder
            // 
            this.buttonAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddFolder.Location = new System.Drawing.Point(131, 545);
            this.buttonAddFolder.Name = "buttonAddFolder";
            this.buttonAddFolder.Size = new System.Drawing.Size(113, 37);
            this.buttonAddFolder.TabIndex = 22;
            this.buttonAddFolder.Text = "Add Folder";
            this.buttonAddFolder.UseVisualStyleBackColor = true;
            this.buttonAddFolder.Click += new System.EventHandler(this.buttonAddFolder_Click);
            // 
            // listBoxModFolders
            // 
            this.listBoxModFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxModFolders.FormattingEnabled = true;
            this.listBoxModFolders.IntegralHeight = false;
            this.listBoxModFolders.Location = new System.Drawing.Point(131, 42);
            this.listBoxModFolders.Name = "listBoxModFolders";
            this.listBoxModFolders.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxModFolders.Size = new System.Drawing.Size(113, 497);
            this.listBoxModFolders.TabIndex = 23;
            this.listBoxModFolders.SelectedIndexChanged += new System.EventHandler(this.listBoxModFolders_SelectedIndexChanged);
            this.listBoxModFolders.DoubleClick += new System.EventHandler(this.listBoxModFolders_DoubleClick);
            // 
            // listBoxKspFolders
            // 
            this.listBoxKspFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxKspFolders.FormattingEnabled = true;
            this.listBoxKspFolders.IntegralHeight = false;
            this.listBoxKspFolders.Location = new System.Drawing.Point(13, 13);
            this.listBoxKspFolders.Name = "listBoxKspFolders";
            this.listBoxKspFolders.Size = new System.Drawing.Size(112, 526);
            this.listBoxKspFolders.TabIndex = 24;
            this.listBoxKspFolders.SelectedIndexChanged += new System.EventHandler(this.listBoxKspFolders_SelectedIndexChanged);
            this.listBoxKspFolders.DoubleClick += new System.EventHandler(this.listBoxKspFolders_DoubleClick);
            // 
            // buttonSelectAllFolders
            // 
            this.buttonSelectAllFolders.Location = new System.Drawing.Point(131, 13);
            this.buttonSelectAllFolders.Name = "buttonSelectAllFolders";
            this.buttonSelectAllFolders.Size = new System.Drawing.Size(113, 23);
            this.buttonSelectAllFolders.TabIndex = 25;
            this.buttonSelectAllFolders.Text = "Select All";
            this.buttonSelectAllFolders.UseVisualStyleBackColor = true;
            this.buttonSelectAllFolders.Click += new System.EventHandler(this.buttonSelectAllFolders_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1662, 638);
            this.Controls.Add(this.buttonSelectAllFolders);
            this.Controls.Add(this.listBoxKspFolders);
            this.Controls.Add(this.listBoxModFolders);
            this.Controls.Add(this.buttonAddFolder);
            this.Controls.Add(this.buttonAddKsp);
            this.Controls.Add(this.buttonDeleteKsp);
            this.Controls.Add(this.buttonDeleteFolder);
            this.Controls.Add(this.buttonOpenLog);
            this.Controls.Add(this.buttonDeleteSelected);
            this.Controls.Add(this.buttonReinstallAll);
            this.Controls.Add(this.buttonUpdateAllForce);
            this.Controls.Add(this.buttonUpdateAll);
            this.Controls.Add(this.buttonCheckAll);
            this.Controls.Add(this.buttonFindAll);
            this.Controls.Add(this.buttonDeinstallAll);
            this.Controls.Add(this.buttonEditCat);
            this.Controls.Add(this.buttonReinstallSelected);
            this.Controls.Add(this.buttonInstallSelected);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listViewMods);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "Kerbal Mod Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOpenSite;
        private System.Windows.Forms.Button buttonAddCat;
        private System.Windows.Forms.Button buttonGoogle;
        private System.Windows.Forms.TextBox textBoxSelDebug;
        private System.Windows.Forms.TextBox textBoxSelWebsite;
        private System.Windows.Forms.TextBox textBoxSelName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewMods;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonFindSelected;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Button buttonUpdateSelected;
        private System.Windows.Forms.Button buttonCheckSelected;
        private System.Windows.Forms.CheckBox checkBoxCanUpdate;
        private System.Windows.Forms.Button buttonInstallSelected;
        private System.Windows.Forms.Button buttonReinstallSelected;
        private System.Windows.Forms.Button buttonEditCat;
        private System.Windows.Forms.Button buttonDeinstallAll;
        private System.Windows.Forms.Button buttonFindAll;
        private System.Windows.Forms.Button buttonCheckAll;
        private System.Windows.Forms.Button buttonUpdateAll;
        private System.Windows.Forms.Button buttonUpdateAllForce;
        private System.Windows.Forms.Button buttonReinstallAll;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button buttonDeleteSelected;
        private System.Windows.Forms.Button buttonOpenLog;
        private System.Windows.Forms.Button buttonDeleteFolder;
        private System.Windows.Forms.Button buttonDeleteKsp;
        private System.Windows.Forms.Button buttonAddKsp;
        private System.Windows.Forms.Button buttonAddFolder;
        private System.Windows.Forms.ListBox listBoxModFolders;
        private System.Windows.Forms.ListBox listBoxKspFolders;
        private System.Windows.Forms.Button buttonSelectAllFolders;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
    }
}

