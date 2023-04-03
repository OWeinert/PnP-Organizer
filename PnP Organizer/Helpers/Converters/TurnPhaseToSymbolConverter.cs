using PnP_Organizer.Properties;
using PnP_Organizer.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;
using Wpf.Ui.Common;

namespace PnP_Organizer.Helpers.Converters
{
    public class TurnPhaseToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not TurnPhase)
                throw new ArgumentException("", nameof(value));

            var turnPhase = (TurnPhase)value;

            if (turnPhase == TurnPhase.PreTurn)
                return SymbolRegular.Checkmark24;
            return SymbolRegular.Next24;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
