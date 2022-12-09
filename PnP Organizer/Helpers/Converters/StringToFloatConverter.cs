using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class StringToFloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(float))
                throw new ArgumentException("", nameof(value));
            return ((float)value).ToString("0.0", new CultureInfo("en-US"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(string))
                throw new ArgumentException("", nameof(value));
            return float.Parse((string)value);
        }
    }
}
