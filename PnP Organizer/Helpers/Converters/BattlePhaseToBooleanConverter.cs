using PnP_Organizer.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class BattlePhaseToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(BattlePhase))
                throw new ArgumentException("", nameof(value));

            return (BattlePhase)value == BattlePhase.InBattle;    
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(bool))
                throw new ArgumentException("", nameof(value));

            return (bool)value ? BattlePhase.InBattle : BattlePhase.PreBattle;
        }
    }
}
