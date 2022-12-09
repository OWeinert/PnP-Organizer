using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class StringAddColonConverter : IValueConverter
    {
        // Somehow the value has to be null to set the visibility to hidden?
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(string))
                throw new ArgumentException("", nameof(value));

            return $"{value}: ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
