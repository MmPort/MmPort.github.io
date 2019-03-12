
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace MmPort
{
    public abstract class MmPort
    {
        public SerialPort mmPort;

        public delegate void UpdateData(byte[] data);


        public MmPort()
        {
            mmPort = new SerialPort();
        }

        public abstract void addDataReceiveHander(MmDataReceiveHander hander);

        public abstract void setConfig(Common.CommunicationType tYPE, MmConfiguration mmConfiguration);
    }
}
