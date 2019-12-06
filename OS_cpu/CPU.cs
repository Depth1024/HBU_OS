using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_cpu
{
    #region 中断类型的定义enum
    // 中断类型
    public enum Interrupt
    {
        TimeOut,     // 时钟中断（时间片耗尽）
        IO,          // IO中断
        End          // 程序运行结束，软中断
    }
    #endregion

    #region 进程状态的模拟
    //进程状态
    public enum processState
    {
        Block,     // 阻塞
        Ready,     // 就绪
        Execute,   // 激活
        Free       // 空闲
    }
    #endregion

    #region PCB 定义进程控制块PCB的结构体
    public struct pcb
    {
        // 进程标识符
        public string id_outside;               // 外部标识符，由用户定义
        public int id_inside;                   // 内部标识符  (0-7),最多容纳8个进程
                                                // 主要寄存器内容
        public int PC;                          // 程序计数器，记录将要取出的指令的地址
        public string IR;                       // 指令寄存器，包含最近取出的指令
                                                // 进程状态
        public Interrupt PSW;                   // 程序状态字,时钟中断、IO中断、软中断
        public processState processStation;     // 进程当前的状态:就绪、阻塞、激活、空闲
        public int priority;                    // 进程优先级
        public int timePassed;                  // 已运行的时间
        public string blockReason;              // 进程由执行状态变为阻塞状态的阻塞原因
                                                // 进程控制信息
        public int next;                        // 队列信息 
    }
    #endregion

    #region PC 程序计数器
    public struct pc
    {
        public int PCB_num;                     // PCB的编号
        public int IR_num;                      // IR队列中的位置
    }
    #endregion

    // cpu类
    class CPU
    {
        public pc PC;           // 程序计数器，记录将要取出的指令的地址（指IR里的地址）
        public string IR;       // 指令寄存器，包含最近取出的指令
        public int DR;          // 数据缓冲寄存器，用于存放x的值
        public int TIME;        // 时钟模拟寄存器，用于记录时间
        public Interrupt PSW;   // 程序状态字，三种中断类型
        public pcb[] PCBArray = new pcb[8];   // pcb数组
        public int Free;        // 空闲进程的数量
        public int Ready;       // 就绪进程的数量 
        public int Block;       // 阻塞进程的数量
        public int Execute;     // 激活进程的数量

        // 初始化方法  init
        private void init()
        {
            Free = 8;  // 空闲进程数量为8
            Ready = Block = Execute = 0;   // 就绪、阻塞、激活
        }
        // 构造方法
        public CPU()
        {
            //init();
        }
        // 进程的创建方法
        public void create()
        {

        }
        // 进程撤销方法
        public void destroy()
        {

        }
        // 进程阻塞方法
        public void processBlock()
        {

        }
        // 进程唤醒方法
        public void wakeUp()
        {

        }

        // 核心函数  cpu    模拟中央处理器
        // 该函数主要负责解释“可执行文件”中的命令
        // 命令一：  x=?     给x赋值一位数
        // 命令二：  x++     x加1
        // 命令三：  x--     x减1
        // 命令四：  !??     第一个？表示阻塞原因A,B(I/O申请），第二个？为一位数，表示阻塞时间（cpu循环次数）；
        // 命令五：  end     表示程序结束，其中包括文件路径名和x的值（软中断方式处理）。
        // 注意：CPU只能解释指令寄存器IR中的指令。一个进程的运行时要根据进程执行的位置，将对应的指令存放到指令寄存器中。
        public void cpu()
        {
            // 使程序一直在本if语句中循环
            if (true)
            {
                // 检测有无中断，有则进行处理
                switch (PSW)
                {
                    // 时间中断——进程调度
                    case Interrupt.TimeOut:
                        {

                            break;
                        }
                    // IO中断——唤醒进程
                    case Interrupt.IO:
                        {
                            break;
                        }
                    // 软中断——撤销进程、进程调度
                    default:
                        {
                            break;
                        }
                }

            }
        }
    }
}
