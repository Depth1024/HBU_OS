using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OS_Simulation
{
    public partial class Form_CFile : Form
    {
        public string filename;
        public byte attribute;
        public string content;
        public byte[] filecontent;

        public Form_CFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filename = textBox_Createname.Text;
            if (filename.Length != 3)
            {
                MessageBox.Show("文件名为3个字符！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(checkBox_readwrite.Checked==true)
            {
                attribute = 2;
            }
            if (checkBox_readonly.Checked == true)
            {
                attribute = 3;
            }
            content = textBox_content.Text;
            UTF8Encoding utf = new UTF8Encoding();
            filecontent= utf.GetBytes(content);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
        
    }
}