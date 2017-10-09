using DeviceCommunication.Device;
using DeviceCommunication.Device.Thermometer;
using DeviceCommunication.ModbusAddresses;
using DeviceCommunication.Utilities;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCommunication
{
    public class DeviceWriter
    {
        private IModbusSerialMaster master;
        private byte slaveAddress;

        public DeviceWriter(IModbusSerialMaster master, byte slaveAddress)
        {
            this.master = master;
            this.slaveAddress = slaveAddress;
        }

        public void SetParameters(DeviceInput input)
        {
            var coils = GetCoils(input);
            var regs = GetRegisters(input);
            master.WriteMultipleRegisters(slaveAddress, 0, regs);
            master.WriteMultipleCoils(slaveAddress, 0, coils);
        }

        private bool[] GetCoils(DeviceInput input)
        {
            var coils = new bool[4];
            coils[Coils.NewSetpoint] = input.SetSetpoint;
            coils[Coils.NewInputOn] = input.SetInputOn;
            coils[Coils.NewInputOff] = input.SetInputOff;
            coils[Coils.NewResolution] = input.SetResolution;
            return coils;
        }

        private ushort[] GetRegisters(DeviceInput input)
        {
            var registers = new ushort[7];
            ModbusConvert.InsertUShorts(input.SetpointValue, registers, HoldingRegisters.NewSetpointHi, HoldingRegisters.NewSetpointLo);
            ModbusConvert.InsertUShorts(input.InputOnValue, registers, HoldingRegisters.NewInputOnHi, HoldingRegisters.NewInputOnLo);
            ModbusConvert.InsertUShorts(input.InputOffValue, registers, HoldingRegisters.NewInputOffHi, HoldingRegisters.NewInputOffLo);
            registers[HoldingRegisters.NewResolution] = (ushort)input.ResolutionValue;
            return registers;
        }
    }
}
