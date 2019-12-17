using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OS_Simulation
{
    public partial class Form_EFile : Form
    {
        byte[] fname;
        byte fattribute;
        string fcontent;

        public byte[] filecontent;
        public int number;
        public FCB buffer = new FCB();

        public int flag = 1;

        public Form_EFile()
        {

        }

        public Form_EFile(string content, byte attribute, byte[] name)
        {
            fcontent = content;
            fname = name;
            fattribute = attribute;

            InitializeComponent();

            UTF8Encoding utf = new UTF8Encoding();
            this.textBox_content.Text = fcontent;
            this.textBox_name.Text = utf.GetString(fname);

            if (fattribute == 2)
            {
                radioButton_readwrite.Checked = true;
            }
            else if (fattribute == 3)
            {
                radioButton_readonly.Checked = true;
                textBox_content.ReadOnly = true;
                textBox_name.ReadOnly = true;
            }
            else
            {
                radioButton_readonly.Enabled = false;
                radioButton_readwrite.Enabled = false;
            }
        }

        private void button_取消_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_确定_Click(object sender, EventArgs e)
        {
            UTF8Encoding utf = new UTF8Encoding();
            buffer.Name = utf.GetBytes(textBox_name.Text);
            if (textBox_name.Text.Length != 3)
            {
                DialogResult result = MessageBox.Show("文件名为3个字符！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    flag = 0;
                }
            }
            if (radioButton_readwrite.Checked == true)
            {
                buffer.Attribute = 2;
            }
            if (radioButton_readonly.Checked == true)
            {
                buffer.Attribute = 3;
            }
            byte[] econtent = utf.GetBytes(textBox_content.Text);
            int n;
            if (econtent.Length == 0)
            {
                n = 0;
            }
            else
            {
                n = econtent.Length / 64;
                if (econtent.Length % 64 != 0)
                {
                    n = n + 1;
                }
            }
            number = n;
            filecontent = econtent;
            this.Close();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_content.Text = "";
        }

        private void radioButton_readwrite_CheckedChanged(object sender, EventArgs e)
        {
            textBox_content.ReadOnly = false;
            textBox_name.ReadOnly = false;
            button_clear.Enabled = true;
        }

        private void radioButton_readonly_CheckedChanged(object sender, EventArgs e)
        {
            button_clear.Enabled = false;
            textBox_name.ReadOnly = true;
            textBox_content.ReadOnly = true;
        }
    }
}