using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers
{
    /// <summary>
    /// Inverts the value of the given boolean
    /// </summary>
    public class BooleanInvertConverter : IValueConverter
    {
        public BooleanInvertConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Invert(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Invert(value);

        private static bool Invert(object value)
        {
            if (value is not bool)
                throw new ArgumentException("BooleanInvertConverter: value must be a boolean!");
            return !(bool)value;
        }
    }
}
