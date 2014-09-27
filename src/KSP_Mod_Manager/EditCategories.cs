using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSP_Mod_Manager
{
    public partial class EditCategories : Form
    {
        public List<string> categoryList = new List<string>();

        public EditCategories(List<string> CategoryList)
        {
            InitializeComponent();
            categoryList = CategoryList;
            RefreshList(0);
        }

        private void RefreshList(int index)
        {
            categoryBox.Items.Clear();

            foreach (string category in categoryList)
            {
                categoryBox.Items.Add(category);
            }

            categoryBox.SelectedIndex = index;
        }

        private void categoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editNameBox.Text = categoryList[categoryBox.SelectedIndex];
        }

        private void editNameBox_TextChanged(object sender, EventArgs e)
        {
            categoryList[categoryBox.SelectedIndex] = editNameBox.Text;
            RefreshList(categoryBox.SelectedIndex);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (addBox.Text != "")
            {
                categoryList.Add(addBox.Text);
                addBox.Text = "";
                RefreshList(categoryBox.SelectedIndex);
            }
        }

        private void deleteCatButton_Click(object sender, EventArgs e)
        {
            categoryList.RemoveAt(categoryBox.SelectedIndex);
            RefreshList(categoryBox.SelectedIndex - 1);
        }
    }
}
