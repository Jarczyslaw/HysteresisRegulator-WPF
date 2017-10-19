using ApplicationSettings;
using DeviceCommunication;
using DeviceCommunication.Device;
using DeviceCommunication.Device.Thermometer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace HysteresisRegulator.ViewModels
{
    public class DeviceSettingsViewModel : ViewModelBase
    {
        private AppSettings appSettings;
        private Communication communication;

        public DeviceSettingsViewModel(AppSettings appSettings, Communication communication)
        {
            this.appSettings = appSettings;
            this.appSettings.OnReset += () => LoadSettings();
            this.communication = communication;

            SendParametersCommand = new RelayCommand(SendParameters, SendParametersEnabled);
            Resolutions = (ThermometerResolution[])Enum.GetValues(typeof(ThermometerResolution));

            communication.CommunicationStart += () => SendParametersCommand.RaiseCanExecuteChanged();

            Input = new DeviceInput();
            LoadSettings();
        }

        public void LoadSettings()
        {
            SelectedResolution = appSettings.ThermometerResolution;
            Setpoint = appSettings.Setpoint;
            InputOff = appSettings.InputOff;
            InputOn = appSettings.InputOn;
        }

        private void SendParameters()
        {
            if (Input.ReadyToSend())
                communication.Writer.SendParametersAsync(Input);
        }

        private bool SendParametersEnabled()
        {
            return communication.Connected;
        }

        public DeviceInput Input { get; private set; }

        public double setpoint;
        public double Setpoint
        {
            get { return setpoint; }
            set
            {
                Set(() => Setpoint, ref setpoint, value);
                Input.SetpointValue = (float)value;
                appSettings.Setpoint = value;
            }
        }

        public double inputOn;
        public double InputOn
        {
            get { return inputOn; }
            set
            {
                Set(() => InputOn, ref inputOn, value);
                Input.InputOnValue = (float)value;
                appSettings.InputOn = value;
            }
        }

        public double inputOff;
        public double InputOff
        {
            get { return inputOff; }
            set
            {
                Set(() => InputOff, ref inputOff, value);
                Input.InputOffValue = (float)value;
                appSettings.InputOff = value;
            }
        }

        public ThermometerResolution selectedResolution;
        public ThermometerResolution SelectedResolution
        {
            get { return selectedResolution; }
            set
            {
                Set(() => SelectedResolution, ref selectedResolution, value);
                Input.ResolutionValue = value;
                appSettings.ThermometerResolution = value;
            }
        }

        public ThermometerResolution[] Resolutions { get; private set; }

        public RelayCommand SendParametersCommand { get; private set; }
    }
}
