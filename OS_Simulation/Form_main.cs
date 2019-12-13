using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Simulation
{
    // 应保证此类为本页面第一个类，否则会报错
    public partial class Form_main : Form
    {
       
        public Form_main()
        {
            InitializeComponent();
        }

        // 电源键
        private void button_power_Click(object sender, EventArgs e)
        {
            // 设置开机、关机的切换
            if (button_power.Text == "开机")
            {
                button_power.Text = "关机";
                button_power.BackColor = Color.Red;
            }
            else if (button_power.Text == "关机")
            {
                button_power.Text = "开机";
                button_power.BackColor = Color.Aquamarine;
            }
        }

        private void Form_main_Load(object sender, EventArgs e)
        {

        }
    }
}
