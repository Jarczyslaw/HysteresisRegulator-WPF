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
        private Settings settings;

        public string Location
        {
            get { return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath; }
        }

        public double Setpoint
        {
            get { return settings.Setpoint; }
            set { settings.Setpoint = value; }
        }

        public double InputOn
        {
            get { return settings.InputOn; }
            set { settings.InputOn = value; }
        }

        public double InputOff
        {
            get { return settings.InputOff; }
            set { settings.InputOff = value; }
        }

        public Resolution Resolution
        {
            get { return settings.Resolution; }
            set { settings.Resolution = value; }
        }

        public int TimeHorizon
        {
            get { return settings.TimeHorizon; }
            set { settings.TimeHorizon = value; }
        }

        public AppSettings()
        {
            settings = Settings.Default;
            if (settings.UpgradeRequired)
            {
                settings.Upgrade();
                settings.UpgradeRequired = false;
            }
            settings.PropertyChanged += (o, e) => settings.Save();
        }
    }
}
