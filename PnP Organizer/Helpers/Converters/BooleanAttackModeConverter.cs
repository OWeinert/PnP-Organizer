using PnP_Organizer.Core.BattleAssistant;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    /// <summary>
    /// Inverts the value of the given boolean
    /// </summary>
    public class BooleanAttackModeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not AttackMode)
                throw new ArgumentException("", nameof(value));

            return (AttackMode)value == AttackMode.Ranged;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool)
                throw new ArgumentException("", nameof(value));

            return (bool)value ? AttackMode.Ranged : AttackMode.Melee;
        }
    }
}
