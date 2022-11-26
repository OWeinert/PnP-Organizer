using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(int))
                throw new ArgumentException("", nameof(value));
            return ((int)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(string))
                throw new ArgumentException("", nameof(value));
            return int.Parse((string)value);
        }
    }
}
