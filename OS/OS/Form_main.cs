using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#region 题目要求
/*
 * 题目要求
 * x=?;   给x赋值一位数
 * x++;   x加1
 * x--;    x减1
 * !??；   第一个？表示阻塞原因A,B(I/O申请），第二个？为一位数，表示阻塞时间（cpu循环次数）；
 * end.   表示程序结束，其中包括文件路径名和x的值（软中断方式处理）。
 * 准备10个文本文件，文件放程序（程序可以相同）
 */
#endregion 

namespace OS
{
    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();
        }

        // 电源键
        private void powerButton_Click(object sender, EventArgs e)
        {
            // 设置开机、关机的切换
            if(powerButton.Text == "开机")
            {
                powerButton.Text = "关机";
                powerButton.BackColor = Color.Red;
            }
            else if(powerButton.Text == "关机")
            {
                powerButton.Text = "开机";
                powerButton.BackColor = Color.Aquamarine;
            }
        }


    }
}
