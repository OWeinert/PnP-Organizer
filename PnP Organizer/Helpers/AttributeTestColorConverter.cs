﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PnP_Organizer.Helpers
{
    /// <summary>
    /// Returns a SolidColorBrush depending on the value of the given Attribute Test Bonus.
    /// </summary>
    public class AttributeTestColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int bonusSum = (int)values[0];
            string totalBonus = (string)values[1];

            var regex = new Regex("D");
            int minDiceSum = regex.Matches(totalBonus).Count;

            if (bonusSum + minDiceSum < 0)
                return (Brush)Application.Current.FindResource("PaletteRedBrush");
            else if (bonusSum > 0 || (minDiceSum > 0 && bonusSum + minDiceSum >= 0))
                return (Brush)Application.Current.FindResource("PaletteGreenBrush");

            return (Brush)Application.Current.FindResource("TextFillColorTertiaryBrush");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
