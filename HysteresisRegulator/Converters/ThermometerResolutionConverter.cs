using DeviceCommunication.Device.Thermometer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Utilities;

namespace HysteresisRegulator.Converters
{
    public class ThermometerResolutionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string pattern = "Accuracy: {0}°C, Interval: {1}ms";
            string accuracy = string.Empty;
            string interval = string.Empty;
            if (value != null)
            {
                var resDesc = ((ThermometerResolution)value).GetAttribute<ThermometerResolutionAttibute>();
                accuracy = resDesc.Accuracy.ToString();
                interval = resDesc.Latency.ToString();
            }
            return string.Format(pattern, accuracy, interval);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
