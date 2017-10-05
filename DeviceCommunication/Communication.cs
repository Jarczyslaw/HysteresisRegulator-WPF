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
        public string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
