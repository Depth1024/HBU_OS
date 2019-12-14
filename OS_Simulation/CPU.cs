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
        ready,    // 就绪
        block,    // 阻塞
        execute,  // 执行
        free      // 空闲
    }
    // 中断类型
    public enum Interrupt
    {
        clock,   // 时钟中断
        io,      // IO中断
        end,      // 软中断
        none     // 无中断
    }


    // PCB的模拟
    public struct PCB
    {
        public int num; // 内部标识符
        public string name; // 进程名，外部标识符

        // 处理机状态信息
        //程序状态寄存器PSW
        public Interrupt PSW;//时钟中断，输入输出中断,软中断         
        public string IR;//指令寄存器IR,存放4个字符即可
        public int PC;// 程序计数器,存放将要取出的指令的在PCB中的地址
        public int DR;//数据缓冲寄存器DR,存放x的值

        // 进程调度信息
        public ProcessState state; // 描述进程状态
        public int time; // 已用时间

        public int next;  // 表示在队列中的先后关系
    }

    // 多设置一个PCB块，便于置空
    // 执行队列
    public class Execute
    {
        public int num;// 执行队列中进程的数量
        public PCB[] PCBqueue = new PCB[9]; // 进程队列 
    }
    // 就绪队列
    public class Ready
    {
        public int num;// 就绪队列中进程的数量
        public PCB[] PCBqueue = new PCB[9]; // 进程队列 
    }
    // 阻塞队列
    public class Block
    {
        public int num;// 阻塞队列中进程的数量
        public PCB[] PCBqueue = new PCB[9]; // 进程队列 
    }
    // 空白PCB队列
    public class Free
    {
        public int num;// 队列中空白PCB的数量
        public PCB[] PCBqueue = new PCB[9]; // 队列
    }
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
        // 计时器
        public int TIME;

        // PCB队列
        public PCB[] PCBarray = new PCB[9];// 多设置一个PCB块，便于置空



        #region 初始化cpu
        // 设置初始化cpu的方法
        public void init()
        {
            // 初始化空白的PCB
            for (int i = 0; i <= 7; i++)
            {
                PCBarray[i].num = i+1;   // 内部表示符号 1 2 3 4 5 6 7 8
                PCBarray[i].name = "process" + i;
                PCBarray[i].PSW = Interrupt.none;    // 无中断
                PCBarray[i].IR = "";  // 无指令
                PCBarray[i].PC = 0;   // 指令地址置零
                PCBarray[i].DR = 0;   // x的值置零
                PCBarray[i].state = ProcessState.free;   // 初始化进程状态为  空闲
                PCBarray[i].time = 0;  // 本进程已使用了0时间的处理机

                // 因为时间片轮转算法，所以将其设置为一个循环队列
                if (i == 7)
                {
                    PCBarray[i].next = 0;
                }
                else
                {
                    PCBarray[i].next = i + 1;
                }

               
            }
            // 初始化硬件
            PSW = Interrupt.none;
            IR = "";
            PC = 0;
            DR = 0;
        }
        #endregion
        // 初始化空白PCB队列的方法
        public void initFreePCB(Free _free)
        {
            for(int i = 0; i <= 7; i++)
            {
                // 将PCB放入空白PCB队列
                _free.num++;
                _free.PCBqueue[i] = PCBarray[i];  // 将第i号PCB块放入空白PCB队列
            }
        }
        
        // cpu构造函数
        public CPU()
        {
            init();  // 调用init函数，初始化cpu
        }

        // 调度函数
        #region getPcbFromFreequeue
        // 从空白PCB队列中取出一个空白的PCB块
        public int getPcbFromFreequeue(Free _free)
        {
            int pcbNo = 0;
            if (_free.num > 0)  // 有剩余的空白PCB，取出
            {
                pcbNo = _free.PCBqueue[0].num;  // 将要取出的pcb块的内部标识符存在pcbNo里
                _free.num--;
                // 如果空白PCB队列不为空，就把后面的PCB块往前挪，最后一个复制空PCB块置空
                if (_free.num > 0) 
                {
                    for (int i = 1; i <= _free.num+1; i++)
                    {
                        _free.PCBqueue[i - 1] = _free.PCBqueue[i];
                    }
                }
            }
            return pcbNo;
        }
        #endregion
        
        #region recoveryPcbFromExecuteToFree
        // 执行完毕，回收PCB块到Free队列队尾
        public void fromExecuteToFree(Execute _execute,Free _free,int pcbNo)  
        // pcb_num为PCB块的内部标识符
        {
            // 用空PCB块PCB[8]格式化PCB块
            PCBarray[pcbNo] = PCBarray[8];
            // 将该PCB块插入Free队列
            _free.PCBqueue[_free.num] = PCBarray[pcbNo];
            _free.num++;
        }
        #endregion

        public void blockPcbFromExecute(int pcbNo,Block _block,Execute _execute)
        {

        }


        // 进程管理函数
        #region 创建进程Create
        // 创建进程
        public void create(Free _free,Ready _ready,Label[] label_storage,Storage _storage,CPU _cpu,string instructions)  // 创建进程时输入指令
        {
            
            // 申请空白进程控制块,得到空闲PCB的内部标识符
            int pcbNo = _cpu.getPcbFromFreequeue(_free);
            if (pcbNo == 0)
            {
                MessageBox.Show("无可用空闲PCB块", "创建进程失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            else
            {
                // 主存申请，程序装入模拟主存
                _storage.allocate(pcbNo,instructions, _storage,label_storage);
                // 初始化进程控制块
                PCBarray[pcbNo].PSW = Interrupt.none;    // 无中断
                PCBarray[pcbNo].IR = "";  // IR存指令
                PCBarray[pcbNo].PC = 0;   // 指令地址置零
                PCBarray[pcbNo].DR = 0;   // x的值置零
                PCBarray[pcbNo].state = ProcessState.ready;   // 初始化进程状态为  空闲
                PCBarray[pcbNo].time = 0;  // 本进程已使用了0时间的处理机
                // 将进程链入就绪队列
                _ready.num++;
                _ready.PCBqueue[_ready.num - 1] = PCBarray[pcbNo];
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
            _cpu.fromExecuteToFree(_execute, _free, pcb_num);
        }
        #endregion


        // 进程的阻塞
        public void block(int pcbNo,Execute _execute,Block _block,CPU _cpu)
        {
            // 保存运行进程的CPU现场
            PCBarray[pcbNo].PSW = _cpu.PSW;    // 无中断
            PCBarray[pcbNo].IR = _cpu.IR;  // 无指令
            PCBarray[pcbNo].PC = _cpu.PC;   // 指令地址置零
            PCBarray[pcbNo].DR = _cpu.DR;   // x的值置零
            // 修改进程状态
            PCBarray[pcbNo].state = ProcessState.block;
            // 将进程链入对应的阻塞队列，然后转向进程调度

        }
        // 进程的唤醒
        public void wakeup()
        {
            // 先把阻塞进程从阻塞队列中移出，将PCB中的状态由block变为ready
            // 再将该进程插入到就绪队列中，
        }
        
    }
}
