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
    public partial class LogForm : Form
    {
        public LogForm(string log)
        {
            InitializeComponent();
            char[] logArray = log.ToCharArray();

            string line = "";
            int lineIndex = 0;

            for (int i = 0; i < logArray.Length; i++)
            {
                if (Char.ConvertToUtf32(log, i) != 13)
                {
                    line += logArray[i];
                }
                else
                {
                    textBox1.Lines[lineIndex] = line;

                    i++;
                    lineIndex++;
                    line = "";
                }
            }
        }
    }
}
