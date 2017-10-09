using DeviceCommunication.Device.Thermometer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication.Device
{
    public class DeviceStatus
    {
        public bool RelayState { get; set; }
        public int Time { get; set; }
        public float Temperature { get; set; }
        public int MeasureTime { get; set; }
        public float Setpoint { get; set; }
        public float InputOn { get; set; }
        public float InputOff { get; set; }
        public ThermometerResolution Resolution { get; set; }
    }
}
