using ApplicationSettings;
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
        public SettingsViewModel(AppSettings appSettings)
        {
            this.appSettings = appSettings;
            ChangeParametersCommand = new RelayCommand(ChangeParameters);
            Resolutions = (Resolution[])Enum.GetValues(typeof(Resolution));

            SelectedResolution = appSettings.Resolution;
            Setpoint = appSettings.Setpoint;
            InputOff = appSettings.InputOff;
            InputOn = appSettings.InputOn;
        }

        private void ChangeParameters()
        {
            Debug.WriteLine("Change parameters");
            Debug.WriteLine(string.Format("Setpoint: {0}, input on: {1}, input off: {2}, resolution: {3}", SetSetpoint, SetInputOn, SetInputOff, SetResolution));
        }

        private AppSettings appSettings;

        public bool setSetpoint = false;
        public bool SetSetpoint
        {
            get { return setSetpoint; }
            set { Set(() => SetSetpoint, ref setSetpoint, value); }
        }

        public bool setInputOn = false;
        public bool SetInputOn
        {
            get { return setInputOn; }
            set { Set(() => SetInputOn, ref setInputOn, value); }
        }

        public bool setInputOff = false;
        public bool SetInputOff
        {
            get { return setInputOff; }
            set { Set(() => SetInputOff, ref setInputOff, value); }
        }

        public bool setResolution = false;
        public bool SetResolution
        {
            get { return setResolution; }
            set { Set(() => SetResolution, ref setResolution, value); }
        }

        public double setpoint;
        public double Setpoint
        {
            get { return setpoint; }
            set
            {
                Set(() => Setpoint, ref setpoint, value);
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
                appSettings.InputOff = value;
            }
        }

        public Resolution selectedResolution;
        public Resolution SelectedResolution
        {
            get { return selectedResolution; }
            set
            {
                Set(() => SelectedResolution, ref selectedResolution, value);
                appSettings.Resolution = value;

                var resDesc = value.GetAttribute<ResolutionAttibute>();
                ResolutionDescription = string.Format("Accuracy: {0}°C, Interval: {1}ms", resDesc.Accuracy, resDesc.Latency);
            }
        }

        public string resolutionDescription;
        public string ResolutionDescription
        {
            get { return resolutionDescription; }
            set { Set(() => ResolutionDescription, ref resolutionDescription, value); }
        }

        public Resolution[] Resolutions { get; private set; }

        public RelayCommand ChangeParametersCommand { get; private set; }
    }
}
