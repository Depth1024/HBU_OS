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

        #region 对象实例化
        // 实例化CPU
        public CPU _cpu = new CPU();
        // 实例化三个队列
        public Block _block = new Block();
        Execute _execute = new Execute();
        Ready _ready = new Ready();
        // 实例化空白PCB队列
        Free _free = new Free();
        //实例化主存储器
        Storage _storage = new Storage();
        #endregion

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

        //显示内存
        public Label[] label_storage = new Label[128];
        // 主窗口加载时调用的方法
        private void Form_main_Load(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 32; i++)      //载入内存条
            {
                for(int j = 0; j < 4; j++)
                {
                    label_storage[4 * i + j] = new Label();
                    label_storage[4 * i + j].BackColor = Color.Aquamarine;
                    label_storage[4 * i + j].Width = 50;
                    label_storage[4 * i + j].Height = 5;
                    label_storage[4 * i + j].Location = new Point(15 + 60 * j, i * 10 + 25);
                    this.groupBox_storage.Controls.Add(label_storage[4 * i + j]);
                   
                }
               
            }

        }

        // 核心函数 cpu
        public void cpu()
        {
            if (true)
            {
                switch (_cpu.PSW)
                {
                    case Interrupt.clock:   //时间片中断
                        {

                            break;
                        }
                    case Interrupt.io:    // 调用设备、io中断
                        {

                            break;
                        }
                    case Interrupt.end:   // 软中断
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                if (_execute.num < 8)
                {
                    // IR取指令
                    // PC改地址
                }
            }

        }

        private void groupBox_storage_Enter(object sender, EventArgs e)
        {

        }
    }
}
