using ApplicationSettings;
using DeviceCommunication;
using DeviceCommunication.Device;
using DeviceCommunication.Device.Thermometer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace HysteresisRegulator.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private AppSettings appSettings;
        private Communication communication;

        public SettingsViewModel(AppSettings appSettings, Communication communication)
        {
            this.appSettings = appSettings;
            this.communication = communication;

            SetParametersCommand = new RelayCommand(SetParameters);
            Resolutions = (ThermometerResolution[])Enum.GetValues(typeof(ThermometerResolution));

            input = new DeviceInput();
            SelectedResolution = appSettings.ThermometerResolution;
            Setpoint = appSettings.Setpoint;
            InputOff = appSettings.InputOff;
            InputOn = appSettings.InputOn;
        }

        private void SetParameters()
        {
            if (input.SetValuesPending())
                communication.Writer.SetParameters(input);
        }

        private DeviceInput input;

        public bool setSetpoint = false;
        public bool SetSetpoint
        {
            get { return setSetpoint; }
            set
            {
                Set(() => SetSetpoint, ref setSetpoint, value);
                input.SetSetpoint = value;
            }
        }

        public bool setInputOn = false;
        public bool SetInputOn
        {
            get { return setInputOn; }
            set
            {
                Set(() => SetInputOn, ref setInputOn, value);
                input.SetInputOn = value;
            }
        }

        public bool setInputOff = false;
        public bool SetInputOff
        {
            get { return setInputOff; }
            set
            {
                Set(() => SetInputOff, ref setInputOff, value);
                input.SetInputOff = value;
            }
        }

        public bool setResolution = false;
        public bool SetResolution
        {
            get { return setResolution; }
            set
            {
                Set(() => SetResolution, ref setResolution, value);
                input.SetResolution = value;
            }
        }

        public double setpoint;
        public double Setpoint
        {
            get { return setpoint; }
            set
            {
                Set(() => Setpoint, ref setpoint, value);
                input.SetpointValue = (float)value;
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
                input.InputOnValue = (float)value;
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
                input.InputOffValue = (float)value;
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
                input.ResolutionValue = value;
                appSettings.ThermometerResolution = value;

                var resDesc = value.GetAttribute<ThermometerResolutionAttibute>();
                ResolutionDescription = string.Format("Accuracy: {0}°C, Interval: {1}ms", resDesc.Accuracy, resDesc.Latency);
            }
        }

        public string resolutionDescription;
        public string ResolutionDescription
        {
            get { return resolutionDescription; }
            set { Set(() => ResolutionDescription, ref resolutionDescription, value); }
        }

        public ThermometerResolution[] Resolutions { get; private set; }

        public RelayCommand SetParametersCommand { get; private set; }
    }
}
