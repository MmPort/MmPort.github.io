# MmPort  
#### 官网：<https://MmPort.github.io>
#### github仓库地址<https://github.com/MmPort/MmPort.github.io.git>
MmPort旨在简化工业控制领域上位机通信模块的工作，目前提供传统的串口通信和modbus通信方式。

## 文件说明
```
MmPortFactory.cs  --> 抽象工厂基类
ModBusAbstractFactory.cd --> Modbus派生类，继承MmPortFactory类。
SerialPortAbstractFactory.cs --> SerialPort派生类，继承MmPortFactory类。
MmConfiguration.cs --> 配置类，配置串口。
MmException.cs -->自定义异常类。
MmDataReceiveHander.cs  --> 串口接收事件，继承该类，重写数据接收方法，即可自定义处理数据。
MmPort.cs --> 串口基类
MmModbus.cs  --> modbus组件，提供modbus协议的多种读写功能。
MmSerialPort.cs --> 普通串口协议组件，支持自定义协议，包头，数据长度，校验，超时，重发等。
MmUtils.cs --> 工具类，提供常用的数据格式转换，校验方法。
```
## 用法

#### 添加引用
从文件中复制MmPort.dll文件，在项目工程添加改dll的引用。
```
Useing MmPort;

```
#### 获取串口对象

```
MmPortAbstractFactory modBusAbstractFactory = new ModBusAbstractFactory();//实例化一个modBusAbstractFactory工厂
MmModbus mmModbus mmModbus =(MmModbus) modBusAbstractFactory.creatMmPort();//获取基于Modbus的串口对象
MmConfiguration mmConfiguration = new MmConfiguration("COM3",9600);//实例化配置类
mmModbus.setConfig(Common.CommunicationType.Modbus, mmConfiguration);//设置配置信息
mmModbus.openPort();//打开串口
mmModbus.addDataReceiveHander(new myDataReceiveHander());//添加串口接受数据的自定义hander。

//示例
byte[] data = mmModbus.ReadKeepReg(18, 19, 1);//向编号为18的modbus设备的20号寄存器获取1个字节的数据。
```


#### 使用
根据得到的串口对象选择合适的方法进行数据收发即可，自定义数据处理的hander需要继承MmDataReceiveHander类，并重写dataReceive方法。


## 注意
其中Common.CommunicationType枚举对应两个值，选择Modbus或者SerialPort。



# ***水平有限，如有错误或不足，请多多指教，持续更新中。***