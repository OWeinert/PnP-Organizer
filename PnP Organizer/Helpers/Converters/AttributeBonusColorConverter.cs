using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PnP_Organizer.Helpers.Converters
{
    /// <summary>
    /// Returns a SolidColorBrush depending on the value of the given AttributeBonus.
    /// Bonus < 0 => Red, Bonus = 0 => White, Bonus > 0 => Green 
    /// </summary>
    public class AttributeBonusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var dValue = System.Convert.ToDouble(value);
            if (dValue < 0)
                return (Brush)Application.Current.FindResource("PaletteRedBrush");
            else if (dValue > 0)
                return (Brush)Application.Current.FindResource("PaletteGreenBrush");

            return new SolidColorBrush((Color)Application.Current.FindResource("TextFillColorTertiary"));
        }

        public object? ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
