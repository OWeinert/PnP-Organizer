using PnP_Organizer.ViewModels;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class BattlePhaseToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// <paramref name="parameter"/> is a bool which inverts the output if set to true
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(BattlePhase))
                throw new ArgumentException("", nameof(value));

            var invert = false;
            if (parameter != null && parameter.GetType() == typeof(string))
            {
                invert = bool.Parse((string)parameter);
            }
                
            if(invert)
                return (BattlePhase)value == BattlePhase.InBattle ? Visibility.Hidden : Visibility.Visible;
            else
                return (BattlePhase)value == BattlePhase.InBattle ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
