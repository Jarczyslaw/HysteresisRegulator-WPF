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
        public delegate void CommunicationStartHandler();
        public event CommunicationStartHandler CommunicationStart;
        public delegate void CommunicationStopHandler();
        public event CommunicationStopHandler CommunicationStop;

        public DeviceReader Reader { get; private set; }
        public DeviceWriter Writer { get; private set; }

        public DevicePolling Pooling { get; private set; }

        public bool Connected
        {
            get
            {
                if (port == null)
                    return false;
                else
                    return port.IsOpen;
            }
        }

        public string PortName
        {
            get
            {
                if (port == null)
                    return null;
                else
                    return port.PortName;
            }
        }

        private SerialPort port;
        private IModbusSerialMaster master;
        private byte slaveAddress = 1;

        public Communication()
        {
            SetupSerialPort();
            master = ModbusSerialMaster.CreateRtu(port);
            SetTimeout(500);
            Reader = new DeviceReader(master, slaveAddress);
            Writer = new DeviceWriter(master, slaveAddress);
            Pooling = new DevicePolling(Reader);
        }

        private void SetupSerialPort()
        {
            port = new SerialPort();
            port.BaudRate = 115200;
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.RtsEnable = true;
            port.DtrEnable = true; 
        }

        public void Start(string portName)
        {
            port.PortName = portName;
            port.Open();
            CommunicationStart?.Invoke();
            Pooling.Start();
        }

        public void Stop()
        {

        }

        public void SetTimeout(int timeout)
        {
            master.Transport.WriteTimeout = timeout;
            master.Transport.ReadTimeout = timeout;
        }

        public string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
