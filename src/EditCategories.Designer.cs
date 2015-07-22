namespace Kerbal_Mod_Manager
{
    partial class EditCategories
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
            this.categoryBox = new System.Windows.Forms.ListBox();
            this.addBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteCatButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.editNameBox = new System.Windows.Forms.TextBox();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // categoryBox
            // 
            this.categoryBox.FormattingEnabled = true;
            this.categoryBox.Location = new System.Drawing.Point(13, 13);
            this.categoryBox.Name = "categoryBox";
            this.categoryBox.Size = new System.Drawing.Size(242, 394);
            this.categoryBox.TabIndex = 0;
            this.categoryBox.SelectedIndexChanged += new System.EventHandler(this.categoryBox_SelectedIndexChanged);
            // 
            // addBox
            // 
            this.addBox.Location = new System.Drawing.Point(342, 15);
            this.addBox.Name = "addBox";
            this.addBox.Size = new System.Drawing.Size(234, 20);
            this.addBox.TabIndex = 1;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(261, 13);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.downButton);
            this.groupBox1.Controls.Add(this.upButton);
            this.groupBox1.Controls.Add(this.deleteCatButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.editNameBox);
            this.groupBox1.Location = new System.Drawing.Point(262, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 126);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Category";
            // 
            // deleteCatButton
            // 
            this.deleteCatButton.Location = new System.Drawing.Point(9, 46);
            this.deleteCatButton.Name = "deleteCatButton";
            this.deleteCatButton.Size = new System.Drawing.Size(299, 30);
            this.deleteCatButton.TabIndex = 2;
            this.deleteCatButton.Text = "Remove";
            this.deleteCatButton.UseVisualStyleBackColor = true;
            this.deleteCatButton.Click += new System.EventHandler(this.deleteCatButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // editNameBox
            // 
            this.editNameBox.Location = new System.Drawing.Point(50, 20);
            this.editNameBox.Name = "editNameBox";
            this.editNameBox.Size = new System.Drawing.Size(258, 20);
            this.editNameBox.TabIndex = 0;
            this.editNameBox.TextChanged += new System.EventHandler(this.editNameBox_TextChanged);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(9, 83);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(144, 35);
            this.upButton.TabIndex = 3;
            this.upButton.Text = "Move Up";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(159, 82);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(148, 35);
            this.downButton.TabIndex = 4;
            this.downButton.Text = "Move Down";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // EditCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 418);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.addBox);
            this.Controls.Add(this.categoryBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditCategories";
            this.Text = "EditCategories";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox categoryBox;
        private System.Windows.Forms.TextBox addBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editNameBox;
        private System.Windows.Forms.Button deleteCatButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
    }
}