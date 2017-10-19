using DeviceCommunication.Device.Thermometer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationSettings
{
    public class AppSettings
    {
        public event Action OnReset;

        private Settings settings;

        public string Location
        {
            get { return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath; }
        }

        public double Setpoint
        {
            get { return settings.Setpoint; }
            set
            {
                if (settings.Setpoint != value)
                    settings.Setpoint = value;
            }
        }

        public double InputOn
        {
            get { return settings.InputOn; }
            set
            {
                if (settings.InputOn != value)
                    settings.InputOn = value;
            }
        }

        public double InputOff
        {
            get { return settings.InputOff; }
            set
            {
                if (settings.InputOff != value)
                    settings.InputOff = value;
            }
        }

        public ThermometerResolution ThermometerResolution
        {
            get { return settings.ThermometerResolution; }
            set
            {
                if (settings.ThermometerResolution != value)
                    settings.ThermometerResolution = value;
            }
        }

        public int TimeHorizon
        {
            get { return settings.TimeHorizon; }
            set
            {
                if (settings.TimeHorizon != value)
                    settings.TimeHorizon = value;
            }
        }

        public bool ShowDataLabels
        {
            get { return settings.ShowDataLabels; }
            set
            {
                if (settings.ShowDataLabels != value)
                    settings.ShowDataLabels = value;
            }
        }

        public int PoolingInterval
        {
            get { return settings.PoolingInterval; }
            set
            {
                if (settings.PoolingInterval != value)
                    settings.PoolingInterval = value;
            }
        }

        public string SerialPort
        {
            get { return settings.SerialPort; }
            set
            {
                if (settings.SerialPort != value)
                    settings.SerialPort = value;
            }
        }

        public void Reset()
        {
            settings.Reset();
            OnReset?.Invoke();
        }

        public AppSettings()
        {
            settings = Settings.Default;
            if (settings.UpgradeRequired)
            {
                settings.Upgrade();
                settings.UpgradeRequired = false;
                settings.Save();
            }
            settings.PropertyChanged += (o, e) => settings.Save();
        }
    }
}
