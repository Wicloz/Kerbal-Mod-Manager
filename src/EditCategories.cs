using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kerbal_Mod_Manager
{
    public partial class EditCategories : Form
    {
        public List<string> categoryList;

        public EditCategories()
        {
            InitializeComponent();
        }

        public void SetCategoryList(List<string> categoryList)
        {
            this.categoryList = categoryList;
            RefreshList(0);
        }

        private void RefreshList(int index)
        {
            categoryBox.Items.Clear();
            categoryBox.SelectedIndices.Clear();
            categoryBox.SelectedItems.Clear();

            foreach (string category in categoryList)
            {
                categoryBox.Items.Add(category);
            }

            if (index < 0)
            {
                index = 0;
            }
            if (categoryBox.Items.Count > 0)
            {
                categoryBox.SelectedIndices.Add(index);
            }
        }

        private void categoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryBox.SelectedIndices.Count == 1)
            {
                editNameBox.Enabled = true;
                deleteCatButton.Enabled = true;
                editNameBox.Text = categoryList[categoryBox.SelectedIndices[0]];

                if (categoryBox.SelectedIndices[0] == 0)
                {
                    upButton.Enabled = false;
                }
                else
                {
                    upButton.Enabled = true;
                }

                if (categoryBox.SelectedIndices[0] == categoryList.Count - 1)
                {
                    downButton.Enabled = false;
                }
                else
                {
                    downButton.Enabled = true;
                }
            }
            else
            {
                editNameBox.Text = "";
                editNameBox.Enabled = false;
                deleteCatButton.Enabled = false;
                upButton.Enabled = false;
                downButton.Enabled = false;
            }
        }

        private void editNameBox_TextChanged(object sender, EventArgs e)
        {
            categoryList[categoryBox.SelectedIndices[0]] = editNameBox.Text;
            RefreshList(categoryBox.SelectedIndices[0]);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (addBox.Text != "" && !categoryList.Contains(addBox.Text))
            {
                categoryList.Add(addBox.Text);
                addBox.Text = "";
                RefreshList(categoryBox.SelectedIndices[0]);
            }
        }

        private void deleteCatButton_Click(object sender, EventArgs e)
        {
            categoryList.RemoveAt(categoryBox.SelectedIndices[0]);
            RefreshList(categoryBox.SelectedIndices[0] - 1);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            string category = categoryList[categoryBox.SelectedIndices[0]];
            int index = categoryBox.SelectedIndices[0];

            categoryList.RemoveAt(index);
            categoryList.Insert(index - 1, category);

            RefreshList(index - 1);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            string category = categoryList[categoryBox.SelectedIndices[0]];
            int index = categoryBox.SelectedIndices[0];

            categoryList.RemoveAt(index);
            categoryList.Insert(index + 1, category);

            RefreshList(index + 1);
        }
    }
}
