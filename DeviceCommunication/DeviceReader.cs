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
    public class DeviceReader
    {
        public delegate void ReadsChangedHandler(int readsCount);
        public event ReadsChangedHandler OnRead;

        public int ReadsCount { get; private set; } = 0;

        private IModbusSerialMaster master;
        private byte slaveAddress;

        public DeviceReader(IModbusSerialMaster master, byte slaveAddress)
        {
            this.master = master;
            this.slaveAddress = slaveAddress;
        }

        public void Start()
        {
            ReadsCount = 0;
            OnRead?.Invoke(ReadsCount);
        }

        public DeviceStatus GetStatus()
        {
            bool[] contacts = master.ReadInputs(slaveAddress, 0, 1);
            ushort[] registers = master.ReadInputRegisters(slaveAddress, 0, 13);

            ReadsCount++;
            OnRead?.Invoke(ReadsCount);
            return ParseStatus(contacts, registers);
        }

        public Task<DeviceStatus> GetStatusAsync()
        {
            return Task.Run(() => GetStatus());
        }

        private DeviceStatus ParseStatus(bool[] inputContacts, ushort[] inputRegisters)
        {
            var status = new DeviceStatus();
            status.RelayState = inputContacts[InputContacts.OutputState];
            status.Time = ModbusConvert.GetInt(inputRegisters[InputRegisters.TimeHi], inputRegisters[InputRegisters.TimeLo]);
            status.Temperature = ModbusConvert.GetFloat(inputRegisters[InputRegisters.TemperatureHi], inputRegisters[InputRegisters.TemperatureLo]);
            status.MeasureTime = ModbusConvert.GetInt(inputRegisters[InputRegisters.MeasureTimeHi], inputRegisters[InputRegisters.MeasureTimeLo]);
            status.Setpoint = ModbusConvert.GetFloat(inputRegisters[InputRegisters.SetpointHi], inputRegisters[InputRegisters.SetpointLo]);
            status.InputOn = ModbusConvert.GetFloat(inputRegisters[InputRegisters.InputOnHi], inputRegisters[InputRegisters.InputOnLo]);
            status.InputOff = ModbusConvert.GetFloat(inputRegisters[InputRegisters.InputOffHi], inputRegisters[InputRegisters.InputOffLo]);
            status.Resolution = (ThermometerResolution)inputRegisters[InputRegisters.Resolution];
            return status;
        }
    }
}
