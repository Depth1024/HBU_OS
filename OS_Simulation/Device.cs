using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Simulation
{
    public enum deviceState
    {
        free,  // 空闲
        working   // 被进程占用
    }
    public struct device
    {
        public deviceState state; // 设备状态
        public int pcb_num; // 占用的进程的内部表示符号
        public string name; // 占用进程的名字
        public int time;
    }
    public class Device
    {
        // 三个A设备，两个B设备
        public device[] deviceA = new device[3];
        public device[] deviceB = new device[2];

        // 分配设备，根据IR选择运行哪一个设备
        public int allocateDevice(Device _device,Block _block,int pcbNo)
        {
            int flag = 0;
            // 如果指令请求调用A设备
            if (_block.PCBqueue[pcbNo].IR[1] == 'A')
            {
                
                // 寻找空闲设备
                for(int i = 0; i < 3; i++)
                {
                    // 有可用设备
                    if(_device.deviceA[i].state == deviceState.free)
                    {
                        // 设置设备状态
                        _device.deviceA[i].state = deviceState.working;
                        _device.deviceA[i].pcb_num = _block.PCBqueue[pcbNo].num;
                        _device.deviceA[i].name = _block.PCBqueue[pcbNo].name;
                        _device.deviceA[i].time = _block.PCBqueue[pcbNo].IR[3] - 48;
                        // 设置进程块
                        _block.PCBqueue[pcbNo].deviceType = 1;
                        _block.PCBqueue[pcbNo].deviceNum = i + 1;   // 第1~3个设备
                        flag = 1;
                        
                        break;
                    }
                }
                
            }else if(_block.PCBqueue[pcbNo].IR[1] == 'B')
            {
               
                // 寻找空闲设备
                for (int i = 0; i < 2; i++)
                {
                    // 有可用设备
                    if (_device.deviceB[i].state == deviceState.free)
                    {
                        // 设置设备状态
                        _device.deviceB[i].state = deviceState.working;
                        _device.deviceB[i].pcb_num = _block.PCBqueue[pcbNo].num;
                        _device.deviceB[i].name = _block.PCBqueue[pcbNo].name;
                        _device.deviceB[i].time = _block.PCBqueue[pcbNo].IR[3] - 48;
                        // 设置进程块
                        _block.PCBqueue[pcbNo].deviceType = 2;
                        _block.PCBqueue[pcbNo].deviceNum = i + 1;   // 第1~3个设备
                        flag = 1;
                        
                        break;
                    }
                }
            }
            else if(_block.PCBqueue[pcbNo].IR[1] == 'u')
            {

            }else
            {
                // 报错
                MessageBox.Show("设备无法识别", "获取设备失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
            return flag;
        }


        // 占用完毕设备，回收设备，将进程插入ready
        public void recoveryDevice(CPU _cpu,Device _device,Ready _ready,Block _block,int pcbNo,int deviceType,int deviceNum)
        {
            // 修改设备状态
            if (deviceType == 1)
            {
                _device.deviceA[deviceNum - 1].name = "null";
                _device.deviceA[deviceNum - 1].pcb_num = 0;
                _device.deviceA[deviceNum - 1].state = deviceState.free;
                _device.deviceA[deviceNum - 1].time = 0;
            }
            else if (deviceType == 2)
            {
                _device.deviceB[deviceNum - 1].name = "null";
                _device.deviceB[deviceNum - 1].pcb_num = 0;
                _device.deviceB[deviceNum - 1].state = deviceState.free;
                _device.deviceB[deviceNum - 1].time = 0;
            }
            // 修改pcb中的设备属性
            _block.PCBqueue[pcbNo].deviceNum = 0;
            _block.PCBqueue[pcbNo].deviceType = 0;
            // 唤醒进程
            _cpu.wakeup(pcbNo, _block, _ready, _cpu);
            

        }
    }
}
