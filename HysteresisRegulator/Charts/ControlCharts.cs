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

        public CartesianMapper<DateSample> TimeMapper { get; private set; }
        public Func<double, string> TimeFormatter { get; private set; }
        public Func<double, string> ControlFormatter { get; private set; }
        public Func<ChartPoint, string> LabelFormatter { get; private set; }

        public TimeSpan TimeHorizon { get; set; } = TimeSpan.FromSeconds(25);
        public bool ShowValues { get; set; } = true;

        private long ticksPerSecond = TimeSpan.FromSeconds(1).Ticks;

        public ControlCharts()
        {
            OutputValues = new ChartValues<DateSample>();
            SetpointValues = new ChartValues<DateSample>();
            RelayValues = new ChartValues<DateSample>();

            TimeMapper = new CartesianMapper<DateSample>()
                .X(d => (double)d.TimeStamp.Ticks / ticksPerSecond)
                .Y(d => d.Value);
            ControlFormatter = d => (d > 0.5d) ? "ON" : "OFF";
            LabelFormatter = p =>
            {
                if (!ShowValues)
                    return string.Empty;
                if (p == null)
                    return string.Empty;
                return p.Y.ToString("0.0");
            };
            TimeFormatter = d => new DateTime((long)d * ticksPerSecond).ToString("HH:mm:ss");
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
            var outdated = values.Where(v => DateTime.Now - v.TimeStamp > TimeHorizon);
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
