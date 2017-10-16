using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HysteresisRegulator.Charts
{
    public class ControlCharts
    {
        public ChartValues<DateSample> OutputValues { get; set; }
        public ChartValues<DateSample> SetpointValues { get; set; }
        public ChartValues<DateSample> RelayValues { get; set; }

        private ChartsConfiguration configuration;

        public ControlCharts(ChartsConfiguration configuration)
        {
            OutputValues = new ChartValues<DateSample>();
            SetpointValues = new ChartValues<DateSample>();
            RelayValues = new ChartValues<DateSample>();

            this.configuration = configuration;
        }

        private void PushToChart(ChartValues<DateSample> values, double value)
        {
            PushToChart(values, new DateSample()
            {
                Value = value,
                TimeStamp = DateTime.Now
            });
        }

        private void PushToChart(ChartValues<DateSample> values, DateSample sample)
        {
            var outdated = values.Where(v => DateTime.Now - v.TimeStamp > TimeSpan.FromSeconds(configuration.TimeHorizon));
            foreach (var value in outdated)
                values.Remove(value);
            values.Add(sample);
        }

        public void Push(double output, double setpoint, bool control)
        {
            PushToChart(OutputValues, output);
            PushToChart(SetpointValues, setpoint);
            PushToChart(RelayValues, control ? 1d : 0d);
        }

        public void Clear()
        {
            OutputValues.Clear();
            SetpointValues.Clear();
        }
    }
}
