using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication.Utilities
{
    public static class ModbusConvert
    {
        public static float GetFloat(ushort hiReg, ushort loReg)
        {
            var bytes = ExtractBytes(hiReg, loReg);
            return BitConverter.ToSingle(bytes, 0);
        }

        public static int GetInt(ushort hiReg, ushort loReg)
        {
            var bytes = ExtractBytes(hiReg, loReg);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static ushort[] GetUShorts(float value)
        {
            ushort[] result = new ushort[2];
            byte[] bytes = BitConverter.GetBytes(value);
            result[1] = (ushort)(bytes[3] << 8 | bytes[2]);
            result[0] = (ushort)(bytes[1] << 8 | bytes[0]);
            return result;
        }

        public static void InsertUShorts(float value, ushort[] registers, int regHiIndex, int regLoIndex)
        {
            var regs = GetUShorts(value);
            registers[regHiIndex] = regs[1];
            registers[regLoIndex] = regs[0];
        }

        private static byte[] ExtractBytes(ushort hiReg, ushort loReg)
        {
            var bytes = new byte[4];
            bytes[3] = (byte)(hiReg >> 8);
            bytes[2] = (byte)(hiReg & 0xFF);
            bytes[1] = (byte)(loReg >> 8);
            bytes[0] = (byte)(loReg & 0xFF);
            return bytes;
        }
    }
}
