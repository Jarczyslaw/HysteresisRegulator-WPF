using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication.ModbusAddresses
{
    public static class Coils
    {
        public static readonly int NewSetpoint = 0;
        public static readonly int NewInputOn = 1;
        public static readonly int NewInputOff = 2;
        public static readonly int NewResolution = 3;
    }
}
