using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        // Somehow the value has to be null to set the visibility to hidden?
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value == null ? Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
