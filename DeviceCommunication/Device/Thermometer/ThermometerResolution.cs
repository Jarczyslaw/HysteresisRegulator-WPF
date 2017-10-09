using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication.Device.Thermometer
{
    public enum ThermometerResolution : ushort
    {
        [ThermometerResolutionAttibute(Accuracy = 0.5f, Latency = 75)]
        Resolution9Bit = 9,
        [ThermometerResolutionAttibute(Accuracy = 0.25f, Latency = 150)]
        Resolution10Bit = 10,
        [ThermometerResolutionAttibute(Accuracy = 0.125f, Latency = 300)]
        Resolution11Bit = 11,
        [ThermometerResolutionAttibute(Accuracy = 0.0625f, Latency = 600)]
        Resolution12Bit = 12
    }
}
