using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MmPort
{
    public  class SerialPortAbstractFactory : MmPortAbstractFactory
    {
        public override MmPort creatMmPort()
        {
            return new MmSerialPort();
        }
    }
}
