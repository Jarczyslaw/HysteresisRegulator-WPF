using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HysteresisRegulator.Converters
{
    public class MillisToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            else
            {
                var ts = TimeSpan.FromMilliseconds((int)value);
                return string.Format("{0:00} d {1:00} h {2:00} m {3:00} s", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
