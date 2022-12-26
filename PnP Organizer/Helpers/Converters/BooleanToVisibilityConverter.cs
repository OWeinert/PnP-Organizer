using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        // Somehow the value has to be null to set the visibility to hidden?
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(bool))
                throw new ArgumentException("", nameof(value));

            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
