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
            if (value.GetType() != typeof(bool))
                throw new ArgumentException("", nameof(value));

            if (parameter.GetType() != typeof(string))
                throw new ArgumentException("", nameof(parameter));


            if (bool.TryParse(parameter.ToString(), out bool bParameter) && bParameter)
            {
                return (bool)value ? Visibility.Hidden : Visibility.Visible;
            }
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
