using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MmPort
{
    public class MmException: ApplicationException
    {
             private string error;
             //无参数构造函数
                 public MmException()
   {
       
   }
    //带一个字符串参数的构造函数，作用：当程序员用Exception类获取异常信息而非 MyException时把自定义异常信息传递过去
     public MmException(string msg) : base(msg)
    {
 　    this.error=msg; 
    }
    public string GetError()
    {
 　    return error; 
    }
    }
}
