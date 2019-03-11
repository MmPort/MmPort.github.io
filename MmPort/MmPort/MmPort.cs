
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace MmPort
{
    public  class MmPort
    {
        public SerialPort mmPort;

        public delegate void UpdateData(byte[] data);


        public MmPort()
        {
            mmPort = new SerialPort();
        }

    }
}
