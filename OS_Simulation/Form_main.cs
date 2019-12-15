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
        //实例化主存储器
        public Storage _storage = new Storage();
        // 实例化三个队列
        public Block _block = new Block();
        public Execute _execute = new Execute();
        public Ready _ready = new Ready();
        // 实例化空白PCB队列
        public Free _free = new Free();
        
        #endregion

        // 电源键
        public void button_power_Click(object sender, EventArgs e)
        {
            // 设置开机、关机的切换
            if (button_power.Text == "开机")
            {
                button_power.Text = "关机";
                button_power.BackColor = Color.Red;
                // 开机时，初始化空白PCB队列
                _cpu.initFreePCB(_free);
                // 开始计时
                this.timer1.Start();
                // 启动cpu
                cpu(); 
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
                    // 因时间片耗尽引起的中断，对执行中的进程进行处理
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

        #region useless
        // 内存显示的groupBox
        private void groupBox_storage_Enter(object sender, EventArgs e)
        {

        }
        // 运行结果显示的groupBox
        private void groupBox_result_Enter(object sender, EventArgs e)
        {

        }
        // 设备状态显示的groupBox
        private void groupBox_device_Enter(object sender, EventArgs e)
        {

        }
        // 三个描述设备占用状态的label
        private void deviceA1state_Click(object sender, EventArgs e)
        {

        }

        private void deviceA2state_Click(object sender, EventArgs e)
        {

        }

        private void deviceA3state_Click(object sender, EventArgs e)
        {

        }


        #endregion

        // 创建进程1
        private void btn_createProcess1_Click(object sender, EventArgs e)
        {
            string instruction1 = "x=0;x++;x++;x--;end;";
            int num = 0;
            num = _cpu.create(_free, _ready, label_storage, _storage, _cpu, instruction1);
            deviceA1state.Text = num.ToString();
        }

        // 创建进程2
        private void btn_createProcess2_Click(object sender, EventArgs e)
        {
            string instruction2 = "x=0;x++;x++;x--;end;";
            int num = 0;
            num = _cpu.create(_free, _ready, label_storage, _storage, _cpu, instruction2);
            deviceA1state.Text = num.ToString();
        }

        // 创建进程3
        private void btn_createProcess3_Click(object sender, EventArgs e)
        {
            string instruction3 = "x=0;x++;x++;x--;end;";
        }

        // 创建进程4
        private void btn_createProcess4_Click(object sender, EventArgs e)
        {
            string instruction4 = "x=0;x++;x++;x--;end;";
        }

        // 创建进程5
        private void btn_createProcess5_Click(object sender, EventArgs e)
        {
            string instruction5 = "x=0;x++;x++;x--;end;";
        }

        // 创建进程6
        private void btn_createProcess6_Click(object sender, EventArgs e)
        {
            string instruction6 = "x=0;x++;x++;x--;end;";
        }

        // 创建进程7
        private void btn_createProcess7_Click(object sender, EventArgs e)
        {
            string instruction7 = "x=0;x++;x++;x--;end;";
        }

        // 创建进程8
        private void btn_createProcess8_Click(object sender, EventArgs e)
        {
            string instruction8 = "x=0;x++;x++;x--;end;";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _cpu.TIME = _cpu.TIME.AddSeconds(1);
            this.SystmTimerLabel.Text = _cpu.TIME.ToString("HH:mm:ss");
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
