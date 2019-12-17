using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simulation
{
    public struct device
    {
        public string state; // 设备状态
        public string process; // 占用的进程
    }
    public class Device
    {
        public device A1;
        public device A2;
        public device A3;
        public device B1;
        public device B2;
    }
}
