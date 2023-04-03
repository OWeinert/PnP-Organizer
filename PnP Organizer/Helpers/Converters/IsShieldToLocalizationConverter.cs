using PnP_Organizer.Properties;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace PnP_Organizer.Helpers.Converters
{
    /// <summary>
    /// Returns a SolidColorBrush depending on the value of the given AttributeBonus.
    /// Bonus < 0 => Red, Bonus = 0 => White, Bonus > 0 => Green 
    /// </summary>
    public class IsShieldToLocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var bValue = (bool)value;
            return bValue ?  Resources.Inventory_ParryBonusWColon : Resources.Inventory_ArmorWColon;
        }

        public object? ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
