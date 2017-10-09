using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication.ModbusAddresses
{
    public static class HoldingRegisters
    {
        public static readonly int NewSetpointHi = 0;
        public static readonly int NewSetpointLo = 1;
        public static readonly int NewInputOnHi = 2;
        public static readonly int NewInputOnLo = 3;
        public static readonly int NewInputOffHi = 4;
        public static readonly int NewInputOffLo = 5;
        public static readonly int NewResolution = 6;
    }
}
