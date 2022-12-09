using PnP_Organizer.Core.Calculators;
using PnP_Organizer.Properties;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class AttackModeLocalizationConverter : IValueConverter
    {
        public AttackModeLocalizationConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not AttackMode)
                throw new ArgumentException("", nameof(value));

            return Resources.ResourceManager.GetString($"Inventory_{(AttackMode)value}")!;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
