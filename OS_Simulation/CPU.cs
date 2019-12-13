using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    // 执行队列
    public class ExecuteQueue
    {
        public int num;// 执行队列中进程的数量
        public int[] process = new int[8]; // 进程队列 
    }
    // 就绪队列
    public class ReadyQueue
    {
        public int num;// 就绪队列中进程的数量
        public int[] process = new int[8]; // 进程队列 
    }
    // 阻塞队列
    public class BlockQueue
    {
        public int num;// 阻塞队列中进程的数量
        public int[] process = new int[8]; // 进程队列 
    }
    // 空闲队列
    public class FreeQueue
    {
        public int num;// 空闲队列中进程的数量
        public int[] process = new int[8]; // 进程队列
    }
    class CPU
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
        public PCB[] PCBqueue = new PCB[8];

        // 实例化三个队列
        BlockQueue blockqueue = new BlockQueue();
        ExecuteQueue executequeue = new ExecuteQueue();
        ReadyQueue readyqueue = new ReadyQueue();
        FreeQueue freequeue = new FreeQueue();

        // 设置初始化cpu的方法
        public void init()
        {
            // 初始化空白的PCB
            for (int i = 0; i <= 7; i++)
            {
                PCBqueue[i].num = i;   // 内部表示符号
                PCBqueue[i].name = "process" + i;    // 命名为  “process i”
                PCBqueue[i].PSW = Interrupt.none;    // 无中断
                PCBqueue[i].IR = "";  // 无指令
                PCBqueue[i].PC = 0;   // 指令地址置零
                PCBqueue[i].DR = 0;   // x的值置零
                PCBqueue[i].state = ProcessState.free;   // 初始化进程状态为  空闲
                PCBqueue[i].time = 0;  // 本进程已使用了0时间的处理机

                // 因为时间片轮转算法，所以将其设置为一个循环队列
                if (i == 7)
                {
                    PCBqueue[i].next = 0;
                }
                else
                {
                    PCBqueue[i].next = i + 1;
                }

            }

            PSW = Interrupt.none;
            IR = "";
            PC = 0;
            DR = 0;
        }

        // cpu构造函数
        public CPU()
        {
            init();  // 调用init函数，初始化cpu
        }

        // 创建进程
        public void create()
        {
            // 申请空白PCB块
            // 为新进程分配资源
            // 初始化PCB
            // 将新进程插入Ready队列
        }
        // 进程的终止
        public void destroy()
        {
            //根据被终止的进程的标识符，从PCB集合中检索出该进程的PCB，从中读出该进程的状态
            // 若处于execute，应立即终止该进程的执行，并设置调度标志为true（用于指示该进程被终止后
            // 应该重新进行调度）。选择一新进程，把处理机分配给它
            // 该进程的子孙进程全部终止
            // 将该进程的全部资源归还给系统
            // 将被终止进程的PCB从所在队列移出，等待其他程序来搜集信息
        }
        // 进程的阻塞
        public void block()
        {
            // 立即停止执行，把PCB的现行状态由execute改为block 
            // 将其插入阻塞队列
            // 最后，转到调度程序进行重新调度，将处理机分配给另一就绪进程并进行切换；在PCB中保留被阻塞进程的处理机
            // 状态，再按新进程的PCB中的处理机状态设置处理机环境
        }
        // 进程的唤醒
        public void wakeup()
        {
            // 先把阻塞进程从阻塞队列中移出，将PCB中的状态由block变为ready
            // 再将该进程插入到就绪队列中，
        }
        // 核心函数 cpu
        public void cpu()
        {
            if (true)
            {
                switch (PSW)
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

                if (executequeue.num < 8)
                {
                    // IR取指令
                    // PC改地址
                }
            }

        }
    }
}
