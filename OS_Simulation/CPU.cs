using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;  // 导入Forms框架，以使用警告弹窗

namespace OS_Simulation
{
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

    // 描述进程状态
    public enum ProcessState
    {
        none,     // 无状态
        ready,    // 就绪
        block,    // 阻塞
        execute,  // 执行
        free      // 空闲
        
    }
    // 中断类型
    public enum Interrupt
    {
        none,   // 无中断
        clock,   // 时钟中断
        io,      // IO中断
        end     // 软中断
        
    }


    // PCB的模拟
    public struct PCB
    {
        public int num; // 内部标识符
        public string name; // 进程名，外部标识符

        // 处理机状态信息
        //程序状态寄存器PSW
        public Interrupt PSW;//时钟中断，输入输出中断,软中断         
        public string IR;//指令寄存器IR
        public int PC;// 程序计数器,存放将要取出的指令的在PCB中的地址
        public int DR;//数据缓冲寄存器DR,存放x的值

        // 所有指令的存储
        public string instructions;

        // 进程调度信息
        public ProcessState state; // 描述进程状态
        public int time; // 已用时间
        // 设备信息
        public int deviceType;  // 设备种类
        public int deviceNum;   // 设备序号

        //public int next;  // 表示在队列中的先后关系,存放下一个PCB的内部标识符
    }

    // 多设置一个PCB块，便于置空
    // 执行队列
    public class Execute
    {
        public int num = 0;// 执行队列中进程的数量
        public PCB[] PCBqueue = new PCB[2]; // 进程队列，只有PCB[0]用来存放正在执行的进程的PCB
        // 执行中的PCB永远为_execute.PCBqueue[0].
    }
    // 就绪队列
    public class Ready
    {
        public int num = 0;// 就绪队列中进程的数量
        public PCB[] PCBqueue = new PCB[9]; // 进程队列 
    }
    // 阻塞队列
    public class Block
    {
        public int num = 0;// 阻塞队列中进程的数量
        public PCB[] PCBqueue = new PCB[9]; // 进程队列 
         
    }
    // 空白PCB队列
    public class Free
    {
        public int num = 0;// 队列中空白PCB的数量
        public PCB[] PCBqueue = new PCB[9]; // 队列
    }

    // CPU类
    public class CPU
    {
        // 程序状态寄存器PSW
        public Interrupt PSW;//可以用1表示时钟中断，2表示输入输出中断，4表示软中断，可以组合1+2,1+4,,2+4,1+2+4
        // 指令寄存器IR 
        public string IR;//存放4个字符即可
        // 程序计数器 
        public int PC;// 存放将要取出的指令的地址
        // 数据缓冲寄存器DR
        public int DR;//存放x的值
        // 剩余时间片
        public int TIME;
        //public DateTime Sys_Time;// 系统运行时间

        // PCB队列
        public PCB[] PCBarray = new PCB[9];// 多设置一个PCB块，便于置空
        // PCB暂存块
        public PCB tempPCB = new PCB();

        

        #region 初始化cpu
        // 设置初始化cpu的方法
        public void init()
        {
            // 初始化空白的PCB
            for (int i = 0; i <= 8; i++)
            {
                //***********************************************************
                PCBarray[i].num = i+1;   // 内部表示符号 1 2 3 4 5 6 7 8    *
                //***********************************************************
                PCBarray[i].name = "process" + i;
                PCBarray[i].PSW = Interrupt.none;    // 无中断
                PCBarray[i].IR = "null";  // 无指令
                PCBarray[i].instructions = "null";
                PCBarray[i].PC = 0;   // 指令地址置零
                PCBarray[i].DR = 981212;   // x的值置一个比较特殊的值
                //PCBarray[i].state = ProcessState.none;   // 初始化进程状态为  空闲
                PCBarray[i].time = 0;  // 本进程已使用了0时间的处理机

                PCBarray[i].deviceType = 0;
                PCBarray[i].deviceNum = 0;
            }
            // 初始化硬件
            PSW = Interrupt.none;
            IR = "";
            PC = 0;
            DR = 981212;  // 置一个比较特殊的值
            //Sys_Time = Convert.ToDateTime("00:00:00");
        }
        #endregion

        #region 初始化队列
        // 初始化空白PCB队列的方法
        public void initFreePCB(Free _free)
        {
            for(int i = 0; i <= 8; i++)
            {
                // 将PCB放入空白PCB队列
                _free.num++;
                _free.PCBqueue[i] = PCBarray[i];  // 将第i号PCB块放入空白PCB队列
                _free.PCBqueue[i].state = ProcessState.free; // 进程状态设置为free
                
            }
            _free.num--;
        }
        public void initReadyPCB(Ready _ready)
        {
            for (int i = 0; i <= 8; i++)
            {
                // 将PCB放入空白PCB队列
                
                _ready.PCBqueue[i] = PCBarray[8];  // 将第i号PCB块放入空白PCB队列
                _ready.PCBqueue[i].state = ProcessState.none; // 进程状态设置为free
                
            }
        }
        public void initBlockPCB(Block _block)
        {
            for (int i = 0; i <= 8; i++)
            {
                // 将PCB放入空白PCB队列

                _block.PCBqueue[i] = PCBarray[8];  // 将第i号PCB块放入空白PCB队列
                _block.PCBqueue[i].state = ProcessState.none; // 进程状态设置为free

            }
        }
        public void initExecutePCB(Execute _execute)
        {
            for (int i = 0; i <= 1; i++)
            {

                _execute.PCBqueue[i] = PCBarray[8];  // 将第i号PCB块放入空白PCB队列
                _execute.PCBqueue[i].state = ProcessState.none; // 进程状态设置为free

            }
        }
        #endregion



        #region CPU构造函数
        // cpu构造函数
        public CPU()
        {
            init();  // 调用init函数，初始化cpu
        }
        #endregion





        // 调度函数
        #region getPcbFromFreequeue
        // 从空白PCB队列中取出一个空白的PCB块
        // 每次取free队列的第一个PCB
        public int getPcbFromFreequeue(Free _free,CPU _cpu)
        {
            int pcb_num = 0;
            if (_free.num > 0)  // 有剩余的空白PCB，取出
            {
                pcb_num = _free.PCBqueue[0].num;  
                _cpu.tempPCB = _free.PCBqueue[0];// 将要取出的pcb存在tempPCB里
                // 如果取出后，空白PCB队列不为空，就把后面的PCB块往前挪，最后一个复制空PCB块置空
                if (_free.num > 1) 
                {
                    for (int i = 0; i < _free.num; i++)
                    {
                        if (i < _free.num - 1)
                        {
                            _free.PCBqueue[i] = _free.PCBqueue[i+1];
                        }
                        else
                        {
                            _free.PCBqueue[i] = _free.PCBqueue[8];
                        }                       
                    }
                    
                }
                _free.num--;
            }
            return pcb_num;
        }
        #endregion
        
        #region recoveryPcbFromExecuteToFree
        // 执行完毕，回收PCB块到Free队列队尾
        public void fromExecuteToFree(Execute _execute,Free _free)
        {  
            // 当前进程位于执行队列中,执行队列只有一个块
            _free.PCBqueue[_free.num] = _execute.PCBqueue[0];
            //_free.PCBqueue[_free.num].state = ProcessState.free; // 修改进程状态
            _free.PCBqueue[_free.num] = _free.PCBqueue[8];  // 格式化处理
            _free.PCBqueue[_free.num].num = _execute.PCBqueue[0].num;
            _free.PCBqueue[_free.num].name = _execute.PCBqueue[0].name;
          //  _free.PCBqueue[_free.num - 1].next = _free.num;
            _free.num++;
            _execute.PCBqueue[0] = _execute.PCBqueue[1];  // 格式化处理
            _execute.num = 0;
        }
        #endregion

        #region blockPcbFromExecute
        // 执行->阻塞
        public void blockPcbFromExecute(Block _block,Execute _execute)
            // pcbNo表示该PCB在执行队列中的NO.
        {
            // 当前进程位于执行队列中
            _block.PCBqueue[_block.num] = _execute.PCBqueue[0];
            _block.PCBqueue[_block.num].state = ProcessState.block;  // 修改进程状态
            _block.num++;
            _execute.PCBqueue[0] = _execute.PCBqueue[1];  // 用空PCB位格式化
            _execute.num = 0;
        }
        #endregion

        #region fromBlockToReady
        //先把阻塞进程从阻塞队列中移出,再将该进程插入到就绪队列中
        public void fromBlockToReady(int pcbNo,Block _block,Ready _ready)
        {
            // 当前进程位于阻塞队列中
            
            _ready.PCBqueue[_ready.num] = _block.PCBqueue[pcbNo];
            _ready.PCBqueue[_ready.num].state = ProcessState.ready;  // 修改进程状态
            _ready.num++;

            for(int i = pcbNo; i < _block.num; i++)
            {
                _block.PCBqueue[i] = _block.PCBqueue[i + 1];
            }
            _block.num--;
        }
        #endregion

        #region fromExecuteToReady
        // 针对clock中断，将未执行完的进程链入ready队列队尾
        public void fromExecuteToReady(Ready _ready,Execute _execute)
        {
            
            // 当前进程位于执行队列中,执行队列只有一个块
            _ready.PCBqueue[_ready.num] = _execute.PCBqueue[0];
            _ready.PCBqueue[_ready.num].state = ProcessState.ready; // 修改进程状态
            _ready.PCBqueue[_ready.num].time = 0;
          
            _ready.num++;
            _execute.PCBqueue[0] = _execute.PCBqueue[1];  // 格式化处理
            _execute.num = 0;
        }
        #endregion

        #region fromReadyToExecute
        // 将进程从就绪队列调度到执行队列
        public void fromReadyToExecute(Ready _ready,Execute _execute)
        {
            _execute.num = 1;
            _execute.PCBqueue[0] = _ready.PCBqueue[0];
            
            // 向前覆盖
            for (int i = 0; i <= _ready.num - 1; i++)
            {
                _ready.PCBqueue[i] = _ready.PCBqueue[i + 1];
            }
           // _ready.PCBqueue[_ready.num] = _ready.PCBqueue[8];
            _ready.num--;
            // state延后修改，便于判断是不是才进入执行
            //_execute.PCBqueue[0].state = ProcessState.ready;
        }
        #endregion


        // 进程管理函数
        #region 创建进程Create
        // 创建进程
        public void create(Free _free,Ready _ready,Label[] label_storage,Storage _storage,CPU _cpu,string instructions)  // 创建进程时输入指令
        {
            
            // 申请空白进程控制块,得到空闲PCB的内部标识符
            int pcb_num = _cpu.getPcbFromFreequeue(_free,_cpu);
            
            if (pcb_num == 0)
            {
                MessageBox.Show("无可用空闲PCB块", "创建进程失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            else
            {
                // 主存申请，程序装入模拟主存
                _storage.allocate(pcb_num,instructions, _storage,label_storage);
               
                // 更改进程状态为ready
                _cpu.tempPCB.state = ProcessState.ready;   
                
                // 将进程链入就绪队列
                // 如果是第一个
                if(_ready.num == 0)
                {
                    _ready.PCBqueue[_ready.num] = _cpu.tempPCB;
                    // 创建进程时，将所有指令存在instructions字符串中
                    _ready.PCBqueue[_ready.num].instructions = instructions;
                    _ready.num++;
                }
                else if(_ready.num >= 1 && _ready.num < 8)
                {
                    _ready.PCBqueue[_ready.num] = _cpu.tempPCB;
                    // 创建进程时，将所有指令存在instructions字符串中
                    _ready.PCBqueue[_ready.num].instructions = instructions;
                    // 修改next，使头尾相接，成环形链
                    //_ready.PCBqueue[_ready.num - 1].next = _ready.num;
                    //_ready.PCBqueue[_ready.num].next = 0;
                    _ready.num++;
                }
                else
                {
                   MessageBox.Show(_ready.num.ToString(), "创建进程失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
                
            }
          
        }
        #endregion

        #region 终止进程 Destroy
        // 进程的终止
        public void destroy(int pcb_num, Free _free, Execute _execute, Label[] label_storage, Storage _storage,CPU _cpu)
        {
            // 将结果写入out文件
            // 回收主存
            _storage.recovery(pcb_num, _free, _execute, label_storage, _storage);
            // 回收进程控制块；
            _cpu.fromExecuteToFree(_execute, _free);
        }
        #endregion

        #region 阻塞进程 Block
        // 进程的阻塞
        public void block(Execute _execute,Block _block,CPU _cpu)
        {
            // 保存运行进程的CPU现场
            _execute.PCBqueue[0].IR = _cpu.IR;
            _execute.PCBqueue[0].PC = _cpu.PC;  
            _execute.PCBqueue[0].DR = _cpu.DR;   
            
            // 将进程链入对应的阻塞队列，然后转向进程调度
            _cpu.blockPcbFromExecute( _block, _execute);
        }
        #endregion

        #region 进程唤醒wakeup
        // 进程的唤醒
        public void wakeup(int pcbNo,Block _block,Ready _ready,CPU _cpu)
        {
            // 先把阻塞进程从阻塞队列中移出，将PCB中的状态由block变为ready
            _block.PCBqueue[_block.num].state = ProcessState.ready;  // 修改进程状态
            // 再将该进程插入到就绪队列中，
            _cpu.fromBlockToReady(pcbNo, _block, _ready);
        }
        #endregion
    }
}
