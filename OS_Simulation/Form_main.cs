using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace OS_Simulation
{
    
    // 应保证此类为本页面第一个类，否则会报错
    public partial class Form_main : Form
    {
       
        public Form_main()
        {          
              InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;// 解决“线程间操作无效：从不是创建控件的线程访问它”的问题
        }

        #region 对象实例化
        // 实例化CPU
        public CPU _cpu = new CPU();
        //实例化主存储器
        public Storage _storage = new Storage();
        // 实例化设备
        public Device _device = new Device();

       
        // 实例化三个队列
        public Block _block = new Block();
        public Execute _execute = new Execute();
        public Ready _ready = new Ready();
        // 实例化空白PCB队列
        public Free _free = new Free();

        #endregion

        // 设置计数器
        DateTime TimeNow = new DateTime();
        TimeSpan TimeCount = new TimeSpan();
        // 这两个是用来算时间片的
        public int tt = 4;
        public int time_now = 0;

       
        


        // 电源键
        public void button_power_Click(object sender, EventArgs e)
        {
            // 设置开机、关机的切换
            if (button_power.Text == "开机")
            {
                button_power.Text = "关机";
                button_power.BackColor = Color.Red;
                #region 初始化队列
                // 开机时，初始化空白PCB队列
                _cpu.initFreePCB(_free);
                // 初始化ready
                _ready.num = 0;
                _cpu.initReadyPCB(_ready);
                // 初始化execute
                _execute.num = 0;
                _cpu.initExecutePCB(_execute);
                // 初始化block
                _block.num = 0;
                _cpu.initBlockPCB(_block);
                // 初始化tempPCB
                _cpu.tempPCB = _cpu.PCBarray[8];
                
                #endregion

                // 开始计时
                this.timer1.Start();
                TimeNow = DateTime.Now;
                
                // 开辟新线程来运行cpu
                Thread thread = new Thread(cpu);
                thread.IsBackground = true;
                thread.Start();

            }
            else if (button_power.Text == "关机")
            {
                button_power.Text = "开机";
                button_power.BackColor = Color.Aquamarine;
                this.timer1.Stop();
                Application.Exit();
            }
        }

        #region Form_main_Load（内存加载）
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
        #endregion

        // 时间片轮转调度算法
        public void RR()
        {
            // 判断执行队列
            if(_execute.num == 0 && _cpu.PSW != Interrupt.io)  // 如果进程已经执行完毕  或还没有进程在执行
            {
                    // 软中断
                    _cpu.PSW = Interrupt.end;
                    
            }
            else if (_cpu.PSW == Interrupt.io)
            {
                    // 查看需要占用哪个设备用几秒
                    _cpu.PSW = Interrupt.io;
            }else
            {
               

                
               
                // 如果这个进程是才进入执行状态的话
                if (_execute.PCBqueue[0].state == ProcessState.ready)
                {
                    _execute.PCBqueue[0].state = ProcessState.execute;
                    // 恢复cpu现场
                    _cpu.PC = _execute.PCBqueue[0].PC;
                    _cpu.DR = _execute.PCBqueue[0].DR;
                    _cpu.IR = _execute.PCBqueue[0].IR;
                }
                else // 如果这个进程已经进入执行队列开始运行了
                {
                    _execute.PCBqueue[0].time++; // 时间片往上加
                }
                // 更改label_timeRest的显示
                _cpu.TIME = 4 - _execute.PCBqueue[0].time;
                this.label_timeRest.Text = _cpu.TIME.ToString();
                
                // 显示正在执行的进程名
                this.executeName_label.Text = _execute.PCBqueue[0].name;


                // 如果正在执行的进程的时间片到了4
                if (_execute.PCBqueue[0].time == 4)
                {
                    
                    // 设置时钟中断
                    _cpu.PSW = Interrupt.clock;
                    // 保护CPU现场
                    _execute.PCBqueue[0].IR = _cpu.IR;
                    _execute.PCBqueue[0].PC = _cpu.PC;
                    _execute.PCBqueue[0].DR = _cpu.DR;

                   
                }
            }
            #region 对block执行操作
            // 对block队列执行操作
            if (_block.num > 0)  // 如果有进程正在调用设备
            {
                for (int i = 0; i < _block.num; i++)
                {
                    if (_block.PCBqueue[i].deviceType == 1)  // A类设备
                    {
                        if (_block.PCBqueue[i].deviceNum == 1)  // A1
                        {
                            // 设备占用时间--
                            _device.deviceA[0].time--;
                            
                            // 如果设备占用时间到，就回收一个
                            if (_device.deviceA[0].time == 0)
                            {
                                deviceA1state.Text = "空闲";
                                ProcessnameA1.Text = "null";
                                _device.recoveryDevice(_cpu, _device, _ready, _block, i, _block.PCBqueue[i].deviceType, _block.PCBqueue[i].deviceNum);
                                // 寻找阻塞队列中有没有还没有获得设备的进程，想办法分配了
                                for (int j = 0; j < _block.num; j++)
                                {
                                    if (_block.PCBqueue[i].deviceType == 0)  // 如果这个进程还没有分配设备
                                    {
                                        int judge = _device.allocateDevice(_device, _block, j);
                                        if (judge == 1)
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                        else if (_block.PCBqueue[i].deviceNum == 2)// A2
                        {
                            // 设备占用时间--
                            _device.deviceA[1].time--;
                            // 如果设备占用时间到，就回收一个
                            if (_device.deviceA[1].time == 0)
                            {
                                _device.recoveryDevice(_cpu, _device, _ready, _block, i, _block.PCBqueue[i].deviceType, _block.PCBqueue[i].deviceNum);
                                deviceA2state.Text = "空闲";
                                ProcessnameA2.Text = "null";
                                // 寻找阻塞队列中有没有还没有获得设备的进程，想办法分配了
                                for (int j = 0; j < _block.num; j++)
                                {
                                    if (_block.PCBqueue[i].deviceType == 0)  // 如果这个进程还没有分配设备
                                    {
                                        int judge = _device.allocateDevice(_device, _block, j);
                                        if (judge == 1)  // 如果分配成功了，就跳出分配环节
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                        else if (_block.PCBqueue[i].deviceNum == 3)// A3
                        {
                            // 设备占用时间--
                            _device.deviceA[2].time--;
                            // 如果设备占用时间到，就回收一个
                            if (_device.deviceA[2].time == 0)
                            {
                                _device.recoveryDevice(_cpu, _device, _ready, _block, i, _block.PCBqueue[i].deviceType, _block.PCBqueue[i].deviceNum);
                                deviceA3state.Text = "空闲";
                                ProcessnameA3.Text = "null";
                                // 寻找阻塞队列中有没有还没有获得设备的进程，想办法分配了
                                for (int j = 0; j < _block.num; j++)
                                {
                                    if (_block.PCBqueue[i].deviceType == 0)  // 如果这个进程还没有分配设备
                                    {
                                        int judge = _device.allocateDevice(_device, _block, j);
                                        if (judge == 1)  // 如果分配成功了，就跳出分配环节
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                    else if (_block.PCBqueue[i].deviceType == 2)  // B类设备
                    {
                        if (_block.PCBqueue[i].deviceNum == 1)  // B1
                        {
                            // 设备占用时间--
                            _device.deviceB[0].time--;
                            // 如果设备占用时间到，就回收一个
                            if (_device.deviceB[0].time == 0)
                            {
                                _device.recoveryDevice(_cpu, _device, _ready, _block, i, _block.PCBqueue[i].deviceType, _block.PCBqueue[i].deviceNum);
                                deviceB1state.Text = "空闲";
                                ProcessnameB1.Text = "null";
                                // 寻找阻塞队列中有没有还没有获得设备的进程，想办法分配了
                                for (int j = 0; j < _block.num; j++)
                                {
                                    if (_block.PCBqueue[i].deviceType == 0)  // 如果这个进程还没有分配设备
                                    {
                                        int judge = _device.allocateDevice(_device, _block, j);
                                        if (judge == 1)
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                        else if (_block.PCBqueue[i].deviceNum == 2)// B2
                        {
                            // 设备占用时间--
                            _device.deviceB[1].time--;
                            // 如果设备占用时间到，就回收一个
                            if (_device.deviceB[1].time == 0)
                            {
                                _device.recoveryDevice(_cpu, _device, _ready, _block, i, _block.PCBqueue[i].deviceType, _block.PCBqueue[i].deviceNum);
                                deviceB2state.Text = "空闲";
                                ProcessnameB2.Text = "null";
                                // 寻找阻塞队列中有没有还没有获得设备的进程，想办法分配了
                                for (int j = 0; j < _block.num; j++)
                                {
                                    if (_block.PCBqueue[i].deviceType == 0)  // 如果这个进程还没有分配设备
                                    {
                                        int judge = _device.allocateDevice(_device, _block, j);
                                        if (judge == 1)  // 如果分配成功了，就跳出分配环节
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        // 核心函数 cpu
        public void cpu()
        {
            while (true)
            {
                // 时间触发算法
                while (time_now == TimeCount.Seconds)
                {
                    // 等时间
                }
                // 时间发生改变，立刻改时间
                time_now = TimeCount.Seconds;

                // 判断中断
                switch (_cpu.PSW)
                {
                    case Interrupt.clock:   //时间片中断
                       // 因时间片耗尽引起的中断，对执行中的进程进行处理
                        {
                            // 当前进程未完成未完成，阻塞该进程，并将进程链入就绪队列的末尾
                            // 执行中的PCB永远为_execute.PCBqueue[0].
                            _cpu.fromExecuteToReady(_ready, _execute);// fromExecuteToReady中已将_execute.num = 0
                            // 将ready队列的第一个调入execute
                            _cpu.fromReadyToExecute(_ready, _execute);
                            // 恢复cpu现场
                            _cpu.PC = _execute.PCBqueue[0].PC;
                            _cpu.DR = _execute.PCBqueue[0].DR;
                            _cpu.IR = _execute.PCBqueue[0].IR;
                            // 新进程调入，重置时间片
                            time_now = TimeCount.Seconds;
                            // PSW归位
                            _cpu.PSW = Interrupt.none;
                            break;
                        }
                    case Interrupt.io:    // 调用设备、io中断
                        {
                            // 将进程链入阻塞队列
                            _cpu.block(_execute, _block, _cpu);
                            // 分配设备,注意：_block.num要-1，因为在进程调度时，_block.num++了
                            int i = _device.allocateDevice(_device, _block, _block.num - 1);
                            if (i == 1) // 分配成功
                            {
                                // 识别设备号，改变设备显示
                                switch (_block.PCBqueue[_block.num - 1].deviceType)
                                {
                                    case 1:  // A
                                        switch (_block.PCBqueue[_block.num - 1].deviceNum)
                                        {
                                            case 1:  // A1
                                                deviceA1state.Text = "使用中";
                                                ProcessnameA1.Text = _block.PCBqueue[_block.num - 1].name;
                                                break;
                                            case 2:  // A2
                                                deviceA2state.Text = "使用中";
                                                ProcessnameA2.Text = _block.PCBqueue[_block.num - 1].name;
                                                break;
                                            case 3:  // A3
                                                deviceA3state.Text = "使用中";
                                                ProcessnameA3.Text = _block.PCBqueue[_block.num - 1].name;
                                                break;
                                        }
                                        break;
                                    case 2:   // B
                                        switch (_block.PCBqueue[_block.num - 1].deviceNum)
                                        {
                                            case 1:  // B1
                                                deviceB1state.Text = "使用中";
                                                ProcessnameB1.Text = _block.PCBqueue[_block.num - 1].name;
                                                break;
                                            case 2:  // B2
                                                deviceB2state.Text = "使用中";
                                                ProcessnameB2.Text = _block.PCBqueue[_block.num - 1].name;
                                                break;
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                // 放在block队列中，等有设备停止使用再判断
                            }

                            if(_ready.num > 0)
                            {
                                // 如果ready队列中有进程在等待，则将ready的第一个进程调入执行
                                _cpu.fromReadyToExecute(_ready, _execute);
                                // 恢复cpu现场
                                _cpu.PC = _execute.PCBqueue[0].PC;
                                _cpu.DR = _execute.PCBqueue[0].DR;
                                _cpu.IR = _execute.PCBqueue[0].IR;
                                // 新进程调入，重置时间片
                                //time_now = TimeCount.Seconds;
                            }
                            else
                            {
                                executeName_label.Text = "idle";
                                label_timeRest.Text = "0";
                                Label_executeInstruction.Text = "null";
                                result_Label.Text = "null";
                            }
                            

                            
                            // PSW归位
                            _cpu.PSW = Interrupt.none;
                            break;
                        }
                    case Interrupt.end:   // 软中断
                        {
                            // 如果是运行完了调用 软中断
                            if (_execute.PCBqueue[0].state == ProcessState.execute)
                            {
                                // 释放当前进程
                                _cpu.destroy(_execute.PCBqueue[0].num, _free, _execute, label_storage, _storage, _cpu);
                                // 如果ready队列中有就绪进程，将ready队列中的进程直接调入cpu执行
                                if(_ready.num > 0)
                                {
                                    _cpu.fromReadyToExecute(_ready, _execute); // 延后修改_execute.state,以便判断这个进程是不是才入执行
                                    // 恢复cpu现场
                                    _cpu.PC = _execute.PCBqueue[0].PC;
                                    _cpu.DR = _execute.PCBqueue[0].DR;
                                    _cpu.IR = _execute.PCBqueue[0].IR;
                                    //@@@@ 设置执行进程的时间片
                                    _execute.PCBqueue[0].time = 0;  // 已经运行的时间
                                   // time_now = TimeCount.Seconds;
                                    //RR();
                                }
                                else
                                {
                                    executeName_label.Text = "idle";
                                    label_timeRest.Text = "0";
                                    Label_executeInstruction.Text = "null";
                                    result_Label.Text = "null";
                                }

                            }
                            else if(_execute.PCBqueue[0].state == ProcessState.none)// 否则是一开始
                            {
                                // 如果有就绪进程，就执行它
                                if (_ready.num > 0)
                                {
                                    // 将ready队列中的进程直接调入cpu执行
                                    _cpu.fromReadyToExecute(_ready, _execute); // 延后修改_execute.state,以便判断这个进程是不是才入执行

                                    // 恢复cpu现场
                                    _cpu.IR = _execute.PCBqueue[0].IR;
                                    _cpu.PC = _execute.PCBqueue[0].PC;
                                    _cpu.DR = _execute.PCBqueue[0].DR;
                                    // 设置执行进程的时间片
                                    _execute.PCBqueue[0].time = 0; // 已经运行的时间
                                   // time_now = TimeCount.Seconds;
                                }
                            }
                           
                            // PSW归位
                            _cpu.PSW = Interrupt.none;
                            break;
                        }
                    default:
                        {
                            
                            break;
                        }
                }


                time_now = TimeCount.Seconds;
                // 如果执行队列中有进程，且进程state为execute（不是刚执行的）
                if (_execute.num == 1 && _execute.PCBqueue[0].state == ProcessState.execute)
                {
                    #region IR取指令
                    _cpu.IR = null; // 格式化IR
                    char r = new char();
                    while (r != ';')
                    {
                        r = _execute.PCBqueue[0].instructions[_cpu.PC];
                        _cpu.IR = _cpu.IR + r;
                        _cpu.PC++;
                    }
                    #endregion
                    // 显示正在执行的指令
                    Label_executeInstruction.Text = _cpu.IR;


                    // 开始读指令
                    for (int i = 0; i < 4; i++)
                    {
                        switch (_cpu.IR[i])
                        {
                            case '+':
                                if (_cpu.DR == 981212)
                                {
                                    MessageBox.Show("请对变量进行赋值", "指令读取失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    if(_cpu.IR[i-1] != '+')
                                    {
                                        _cpu.DR++;
                                    }
                                    result_Label.Text = _cpu.DR.ToString();
                                }
                                break;
                            case '-':
                                if (_cpu.DR == 981212)
                                {
                                    MessageBox.Show("请对变量进行赋值", "指令读取失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    if (_cpu.IR[i - 1] != '-')
                                    {
                                        _cpu.DR--;
                                    }
                                    result_Label.Text = _cpu.DR.ToString();
                                }
                                break;
                            case '=':
                                _cpu.DR = _cpu.IR[i+1] - 48;  // 用ascll码进行转换
                                result_Label.Text = _cpu.DR.ToString();
                                break;
                            case '!':
                                
                                _cpu.PSW = Interrupt.io;
                                break;
                            case 'e':
                                // 如果执行到“end;”，就令_execute.num == 0,不改state,然后调用RR，进行进程调度
                                _execute.num = 0;
                                break;

                            default:
                                break;
                        }
                    }
                    

                }



                RR();
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

        Form_File _file = new Form_File();

        // 创建进程1
        private void btn_createProcess1_Click(object sender, EventArgs e)
        {
            //string instruction = _file.instructions[0];
            //int num = 0;
            _cpu.create(_free, _ready, label_storage, _storage, _cpu, _file.instructions[0]);
            
        }

        // 创建进程2
        private void btn_createProcess2_Click(object sender, EventArgs e)
        {
            string instruction = _file.instructions[1];
            //int num = 0;
            _cpu.create(_free, _ready, label_storage, _storage, _cpu, instruction);

        }

        // 创建进程3
        private void btn_createProcess3_Click(object sender, EventArgs e)
        {
            string instruction = _file.instructions[2];
            //int num = 0;
            _cpu.create(_free, _ready, label_storage, _storage, _cpu, instruction);
        }

        // 创建进程4
        private void btn_createProcess4_Click(object sender, EventArgs e)
        {
            string instruction = _file.instructions[3];
            //int num = 0;
            _cpu.create(_free, _ready, label_storage, _storage, _cpu, instruction);
        }

        // 创建进程5
        private void btn_createProcess5_Click(object sender, EventArgs e)
        {
            string instruction = _file.instructions[4];
            //int num = 0;
            _cpu.create(_free, _ready, label_storage, _storage, _cpu, instruction);
        }

        // 创建进程6
        private void btn_createProcess6_Click(object sender, EventArgs e)
        {
            string instruction = _file.instructions[5];
            //int num = 0;
            _cpu.create(_free, _ready, label_storage, _storage, _cpu, instruction);
        }

        // 创建进程7
        private void btn_createProcess7_Click(object sender, EventArgs e)
        {
            string instruction = _file.instructions[6];
            //int num = 0;
            _cpu.create(_free, _ready, label_storage, _storage, _cpu, instruction);
        }

        // 创建进程8
        private void btn_createProcess8_Click(object sender, EventArgs e)
        {
            string instruction = _file.instructions[7];
            //int num = 0;
            _cpu.create(_free, _ready, label_storage, _storage, _cpu, instruction);
        }

        
        // 计时器控件
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            TimeCount = DateTime.Now - TimeNow;
            this.SystmTimerLabel.Text = string.Format("{0:00}:{1:00}:{2:00}", TimeCount.Hours, TimeCount.Minutes, TimeCount.Seconds);
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox_timeRest_Enter(object sender, EventArgs e)
        {

        }

        private void FileSystem_Button_Click(object sender, EventArgs e)
        {
           
            _file.Show();
        }
    }
}
