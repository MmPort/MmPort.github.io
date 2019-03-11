using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MmPort
{ 
    public class Common
    {
        public enum CommunicationType : int
        {
            SerialPort = 10000,
            Modbus = 10001
        }
    }
}
