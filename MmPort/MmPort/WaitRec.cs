using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MmPort
{
    public class WaitRec
    {
        /// <summary>  
        /// 发送的内容  
        /// </summary>  
        public string strSend;

        /// <summary>  
        /// 发送一次等待的秒数  
        /// </summary>  
        public int second;

        /// <summary>  
        /// 超时时间  
        /// </summary>  
        public int time_To_Timeout;

        /// <summary>  
        /// 声明  
        /// </summary>  
        /// <param name="strSend">发送的数据</param>  
        /// <param name="second">发送之后进行判断的时间间隔</param>  
        /// <param name="time_To_Timeout">发送数据之后的超时时间</param>  
        public WaitRec(string strSend, int second, int time_To_Timeout)
        {
            this.strSend = strSend;
            this.second = second;
            this.time_To_Timeout = time_To_Timeout;
        }
    }
}
