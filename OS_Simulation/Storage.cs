using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;  // 导入Forms框架，以使用警告弹窗
using System.Drawing;


namespace OS_Simulation
{
    
    // 存储管理
    public class Storage
    {
        // 用128*4大小的数组模拟主存
        public int[] freeStorage = new int[128];
        // 记录每一块内存被哪个进程占用
        public int[] pcbStorage = new int[128];
        #region 内存分配
        // 内存分配算法
        public void allocate(int pcb_num,string instructions,Storage _storage,Label[] label_storage)  
        //  process_num为PCB内标；instructions为指令串，通过计算指令串的长度来分配大小
        {
            int length = instructions.Length;
            int need = length / 4;   // 4个字节为一个内存块
            int record = 0;  // 计算有多少连续的空余内存的变量
            int min = 128;// 最小分区大小
            int storage_num = 0;
            
            for(int i = 0; i < 128; i++)
            {
                if(_storage.freeStorage[i] == 0)
                {
                    // 如果这个内存块没有被使用
                    record++;
                    // 如果这片内存区可用，计算它的大小
                    if(record >= need && _storage.freeStorage[i+1] == 1)
                    {
                        if(min > record)
                        {
                            min = record;
                            storage_num = i - record + 1; // 记录最小内存块的开始地址
                        }
                    }
                }
                else
                {
                    // 如果这个内存块被使用了，就将record归零，重新计数
                    record = 0;
                }
            }
            if (record >= need)// 如果有连续need大小的空间
            {
                
                for(int i = storage_num;i < storage_num + need; i++)
                {
                    _storage.freeStorage[i] = 1;  // 将内存块状态改为1
                    pcbStorage[i] = pcb_num;
                    // 把对应的内存块颜色变一下@@@@@@@@@@@@@@@@@@@@
                    label_storage[i].BackColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("无可用内存空间", "创建进程失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
        #endregion
        
        // 内存回收
        public void recovery(int pcb_num,Free _free,Execute _execute,Label[] label_storage,Storage _storage)
        {
            for(int i = 0; i < 128; i++)
            {
                if(_storage.pcbStorage[i] == pcb_num)
                {
                    _storage.pcbStorage[i] = 0;  // 将这个进程占用的内存块清空
                    label_storage[i].BackColor = Color.Aquamarine;  // 内存块颜色更改

                }
            }
            
            
        }
    }
}
