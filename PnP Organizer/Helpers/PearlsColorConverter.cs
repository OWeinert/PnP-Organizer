using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PnP_Organizer.Helpers
{
    /// <summary>
    /// Returns a SolidColorBrush depending on the given pearl count.
    /// count > 0 => Green, count > 3 => Yellow, count > 7 => Red.
    /// </summary>
    public class PearlsColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            double dValue = System.Convert.ToDouble(value);
            if(dValue > 0)
            {
                if(dValue > 3)
                {
                    if(dValue > 7)
                    {
                        return (Brush)Application.Current.FindResource("PaletteRedBrush");
                    }
                    return (Brush)Application.Current.FindResource("PaletteAmberBrush");
                }
                return (Brush)Application.Current.FindResource("PaletteGreenBrush");
            }
            
            return new SolidColorBrush((Color)Application.Current.FindResource("TextFillColorPrimary"));
        }

        public object? ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
