using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication
{
    public class Communication
    {
        public DeviceReader Reader { get; private set; }
        public DeviceWriter Writer { get; private set; }

        private SerialPort port;
        private IModbusSerialMaster master;
        private byte slaveAddress = 1;

        public void Start(string portName)
        {
            port = new SerialPort(portName);
            port.BaudRate = 115200;
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.RtsEnable = true;
            port.DtrEnable = true;
            port.Open();
            master = ModbusSerialMaster.CreateRtu(port);
            Reader = new DeviceReader(master, slaveAddress);
            Writer = new DeviceWriter(master, slaveAddress);
        }

        public string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
