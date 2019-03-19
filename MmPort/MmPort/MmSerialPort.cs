
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MmPort
{ 
    public  class MmSerialPort : MmPort
    {

        public event UpdateData update;

        public override  void addDataReceiveHander(MmDataReceiveHander hander)
        {
            update += hander.dataReceive;
        }

        public override void setConfig(Common.CommunicationType tYPE,MmConfiguration mmConfiguration)
        {
            if (tYPE == Common.CommunicationType.SerialPort)
            {
                if (mmConfiguration.isAllCustom)
                {
                    mmPort.PortName = mmConfiguration.portName;
                    mmPort.BaudRate = mmConfiguration.baudRate;
                    mmPort.Parity = mmConfiguration.parity;
                    mmPort.DataBits = mmConfiguration.dataBits;
                    mmPort.StopBits = mmConfiguration.stopBits;
                    mmPort.ReceivedBytesThreshold = mmConfiguration.ReceivedBytesThreshold;

                }
                else
                {
                    mmPort.PortName = mmConfiguration.portName;
                    mmPort.BaudRate = mmConfiguration.baudRate;
                }
                mmPort.DataReceived += spDataReceived;
            }
            else
            {
                throw new MmException("枚举类型不匹配");
            }
        }


        static System.Timers.Timer timerTOA; //timerTimeOut A 总体观察（暂无数据时）

        byte[] buff; //临时数组，存放每次的返回结果

        int delayTime; //允许的超时次数

        int offset; //当前偏移量

        static int lasetDataSendOffset; //上次数据发送当前偏移量

        static byte[] lastLataSend;//上次发送的数据

        static int lasrDataSendLength;//上次发送的数据长度

        int bytesNum; //本次读取到的数量


        public int resendCount = 3;//重发次数

        public int resendSeconds = 150;//发送间隔ms


        //public delegate void OnReceiveHandler(object sender);

        public delegate void OnTimeOutHandler();


        /// <summary>
        /// 串口接收事件
        /// </summary>
      //  public event OnReceiveHandler onReceive;

        /// <summary>
        /// 串口接收超时事件
        /// </summary>
        public event OnTimeOutHandler onTimeOut;

        public MmSerialPort()
        {

            initData();
            bindEvent();
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        private void initData()
        {
            timerTOA = new System.Timers.Timer();
            timerTOA.Interval = resendSeconds; //100ms超时      
            timerTOA.AutoReset = false;
            timerTOA.Enabled = false;

            buff = new byte[1024];
            offset = 0;
            bytesNum = 0;
            delayTime = 0;


        }


        /// <summary>
        /// 事件绑定
        /// </summary>
        private void bindEvent()
        {
            timerTOA.Elapsed += new System.Timers.ElapsedEventHandler(end100ms);
            mmPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(spDataReceived);
        }





        /// <summary>
        /// 串口发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public void Write(byte[] data, int offset, int count)
        {
            try
            {
                //串口发送
                mmPort.Write(data, offset, count);
                //保存已经发送的数据
                lastLataSend = data.Skip(offset).Take(count).ToArray();
                lasetDataSendOffset = offset;
                lasrDataSendLength = count;
                timerTOA.Start();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// timerTOA的100ms结束时触发的方法
        /// </summary>
        private void end100ms(Object sender, EventArgs e)
        {
            bytesNum = mmPort.BytesToRead;

            if (bytesNum == 0 && delayTime <= 10) //再给10次机会
            {
                delayTime++;
                if (delayTime >= resendCount)
                {
                    timerTOA.Stop();
                    reset();
                    //超时提醒
                    onTimeOut();
                    return;
                }
                else
                {
                    try
                    {
                        Write(lastLataSend, lasetDataSendOffset, lasrDataSendLength);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
        }

        /// <summary>
        /// 串口收到下位机返回的数据时触发的方法
        /// </summary>
        public void spDataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                timerTOA.Stop();
                reset();
                //  onReceive(sender);
                if (mmPort.BytesToRead > 0)
                {
                    mmPort.Read(buff,0,mmPort.BytesToRead);
                   update(buff);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        /// <summary>
        /// 重置
        /// </summary>
        public void reset()
        {
            buff = new byte[1024];
            offset = 0;
            bytesNum = 0;
            delayTime = 0;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            try
            {
                if (!this.mmPort.IsOpen)
                {
                    this.mmPort.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            try
            {
                if (this.mmPort.IsOpen)
                {
                    this.mmPort.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
