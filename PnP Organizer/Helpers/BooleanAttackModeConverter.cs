using PnP_Organizer.Core.Calculators;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers
{
    /// <summary>
    /// Inverts the value of the given boolean
    /// </summary>
    public class BooleanAttackModeConverter : IValueConverter
    {
        public BooleanAttackModeConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not AttackMode)
                throw new ArgumentException();

            var aValue = (AttackMode)value;
            return aValue == AttackMode.Ranged;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool)
                throw new ArgumentException();

            var bValue = (bool)value;
            return bValue ? AttackMode.Ranged : AttackMode.Melee;
            
        }
    }
}
