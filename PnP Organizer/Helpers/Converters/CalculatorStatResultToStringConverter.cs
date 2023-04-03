using PnP_Organizer.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class CalculatorStatResultToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(CalculatorStatResultModel))
                throw new ArgumentException("", nameof(value));

            var cValue = (CalculatorStatResultModel)value;

            if(parameter != null && parameter.GetType() == typeof(string))
            {
                var sParameter = (string)parameter;

                if(sParameter == "name")
                {
                    return $"{cValue.StatName}: ";
                }
                else if(sParameter == "value")
                {
                    return $"{cValue.StatValue}";
                }
                else if(sParameter == "diff" && cValue.StatDifference != 0)
                {
                    var optionalPlus = cValue.StatDifference > 0 ? "+" : string.Empty;
                    return $"( {optionalPlus}{cValue.StatDifference} )";
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
