using ApplicationSettings;
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

        private int timeHorizon;
        public int TimeHorizon
        {
            get { return timeHorizon; }
            set
            {
                Charts.timeHorizon = TimeSpan.FromSeconds(value);
                Set(() => TimeHorizon, ref timeHorizon, value);
                appSettings.TimeHorizon = value;
            }
        }


        private double dummySetpoint = 20d;
        private double dummyOutput = 0d;
        private bool dummyRelayState = false;

        private AppSettings appSettings;

        public ChartsViewModel(AppSettings appSettings)
        {
            Charts = new ControlCharts();
            this.appSettings = appSettings;
            TimeHorizon = appSettings.TimeHorizon;
            AddDummyData();
        }

        private void AddDummyData()
        {
            Task.Run(async () =>
            {
                while(true)
                {
                    await Task.Delay(1000);
                    Charts.Push(dummyOutput, dummySetpoint, dummyRelayState);
                    dummyRelayState = !dummyRelayState;
                    dummyOutput++;
                }
            });
        }
    }
}
