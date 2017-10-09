using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication.Device.Thermometer
{
    public class ThermometerResolutionAttibute : Attribute
    {
        public float Accuracy { get; set; }
        public int Latency { get; set; }
    }
}
