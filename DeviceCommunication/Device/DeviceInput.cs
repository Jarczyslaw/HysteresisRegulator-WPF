using DeviceCommunication.Device.Thermometer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication.Device
{
    public class DeviceInput
    {
        public bool SetSetpoint { get; set; }
        public bool SetInputOn { get; set; }
        public bool SetInputOff { get; set; }
        public bool SetResolution { get; set; }

        public float SetpointValue { get; set; }
        public float InputOnValue { get; set; }
        public float InputOffValue { get; set; }
        public ThermometerResolution ResolutionValue { get; set; }

        public bool SetValuesPending()
        {
            return SetSetpoint || SetInputOn || SetInputOff || SetResolution;
        }
    }
}
