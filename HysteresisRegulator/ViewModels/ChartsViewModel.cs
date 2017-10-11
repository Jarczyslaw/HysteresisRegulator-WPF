using ApplicationSettings;
using DeviceCommunication;
using DeviceCommunication.Device;
using GalaSoft.MvvmLight;
using HysteresisRegulator.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HysteresisRegulator.ViewModels
{
    public class ChartsViewModel : ViewModelBase
    {
        public ControlCharts Charts { get; private set; }

        private double dummySetpoint = 20d;
        private double dummyOutput = 0d;
        private bool dummyRelayState = false;

        private AppSettings appSettings;
        private Communication communication;

        public ChartsViewModel(AppSettings appSettings, Communication communication)
        {
            Charts = new ControlCharts();
            this.appSettings = appSettings;
            this.communication = communication;

            TimeHorizon = appSettings.TimeHorizon;
            ShowValues = appSettings.ShowValues;

            communication.Pooling.UpdateStatus += (s) => Charts.Push(s.Temperature, s.Setpoint, s.RelayState);
        }

        private int timeHorizon;
        public int TimeHorizon
        {
            get { return timeHorizon; }
            set
            {
                Charts.TimeHorizon = TimeSpan.FromSeconds(value);
                Set(() => TimeHorizon, ref timeHorizon, value);
                appSettings.TimeHorizon = value;
            }
        }

        private bool showValues;
        public bool ShowValues
        {
            get { return showValues; }
            set
            {
                Charts.ShowValues = value;
                Set(() => ShowValues, ref showValues, value);
                appSettings.ShowValues = value;
            }
        }
    }
}
