
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MmPort
{ 
    public class MmSerialPort : MmPort
    {

        public event UpdateData update;

        public  void addDataReceiveHander(MmDataReceiveHander hander)
        {
            update += hander.dataReceive;
        }

        public  void setConfig(Common.CommunicationType tYPE,MmConfiguration mmConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
