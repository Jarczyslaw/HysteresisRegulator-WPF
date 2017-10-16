using ApplicationSettings;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HysteresisRegulator.Charts
{
    public class ChartsConfiguration : ViewModelBase
    {
        public CartesianMapper<DateSample> DateSampleMapper { get; private set; }

        public int AxisCount { get; } = 10;

        public Func<double, string> TimeAxisFormatter { get; private set; }
        public Func<double, string> RelayChartFormatter { get; private set; }
        public Func<ChartPoint, string> LabelPointFormatter { get; private set; }

        private long ticksPerSecond = TimeSpan.FromSeconds(1).Ticks;

        private AppSettings appSettings;

        public ChartsConfiguration(AppSettings appSettings)
        {
            this.appSettings = appSettings;

            TimeHorizon = appSettings.TimeHorizon;
            ShowDataLabels = appSettings.ShowDataLabels;

            DateSampleMapper = new CartesianMapper<DateSample>()
                .X(d => (double)d.TimeStamp.Ticks / ticksPerSecond)
                .Y(d => d.Value);
            RelayChartFormatter = d => (d > 0.5d) ? "ON" : "OFF";
            LabelPointFormatter = p =>
            {
                if (p == null)
                    return string.Empty;
                return p.Y.ToString("0.0");
            };
            TimeAxisFormatter = d => new DateTime((long)d * ticksPerSecond).ToString("HH:mm:ss");
        }

        private int timeHorizon = 60;
        public int TimeHorizon
        {
            get { return timeHorizon; }
            set
            {
                Set(() => TimeHorizon, ref timeHorizon, value);
                AxisStep = value / AxisCount;
                appSettings.TimeHorizon = value;
            }
        }

        private int axisStep = 5;
        public int AxisStep
        {
            get { return axisStep; }
            set
            {
                Set(() => AxisStep, ref axisStep, value);
            }
        }

        private bool showDataLabels;
        public bool ShowDataLabels
        {
            get { return showDataLabels; }
            set
            {
                Set(() => ShowDataLabels, ref showDataLabels, value);
                appSettings.ShowDataLabels = value;
            }
        }
    }
}
