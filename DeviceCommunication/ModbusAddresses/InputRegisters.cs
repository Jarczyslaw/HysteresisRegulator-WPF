using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication.ModbusAddresses
{
    public static class InputRegisters
    {
        public static readonly int TimeHi = 0;
        public static readonly int TimeLo = 1;
        public static readonly int TemperatureHi = 2;
        public static readonly int TemperatureLo = 3;
        public static readonly int MeasureTimeHi = 4;
        public static readonly int MeasureTimeLo = 5;
        public static readonly int SetpointHi = 6;
        public static readonly int SetpointLo = 7;
        public static readonly int InputOnHi = 8;
        public static readonly int InputOnLo = 9;
        public static readonly int InputOffHi = 10;
        public static readonly int InputOffLo = 11;
        public static readonly int Resolution = 12;
    }
}
