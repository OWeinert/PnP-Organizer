using PnP_Organizer.Core.BattleAssistant;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class BattleActionToBooleanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not BattleAction)
                throw new ArgumentException("", nameof(value));

            if (parameter != null && parameter.GetType() == typeof(string))
            {
                if (Enum.TryParse(typeof(BattleAction), (string)parameter, out var battleAction))
                    return (BattleAction)value == (BattleAction)battleAction!;
            }
            return (BattleAction)value == BattleAction.Defend;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
