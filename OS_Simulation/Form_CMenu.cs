using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OS_Simulation
{
    public partial class Form_CMenu : Form
    {
        public string menuname;

        public Form_CMenu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menuname = textBox_menuname.Text;
            if(menuname.Length!=3)
            {
                MessageBox.Show("文件名为3个字符！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.Close();
        }
    }
}