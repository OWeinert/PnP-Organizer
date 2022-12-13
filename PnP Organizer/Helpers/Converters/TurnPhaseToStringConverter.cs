using PnP_Organizer.Properties;
using PnP_Organizer.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class TurnPhaseToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not TurnPhase)
                throw new ArgumentException("", nameof(value));

            var turnPhase = (TurnPhase)value;

            if (turnPhase == TurnPhase.PostTurn)
                return Resources.Calculator_ButtonEndTurn;
            return Resources.Calculator_ButtonNextTurn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
