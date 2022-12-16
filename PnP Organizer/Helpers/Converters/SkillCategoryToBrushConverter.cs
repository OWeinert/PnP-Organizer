using PnP_Organizer.Core.Character;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PnP_Organizer.Helpers.Converters
{
    public class SkillCategoryToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(SkillCategory))
                throw new ArgumentException("", nameof(value));

            var sCValue = (SkillCategory)value switch
            {
                SkillCategory.Melee => (SolidColorBrush)Application.Current.Resources["PaletteBlueBrush"],
                SkillCategory.Ranged => (SolidColorBrush)Application.Current.Resources["PaletteGreenBrush"],
                _ => (SolidColorBrush)Application.Current.Resources["ControlSolidFillColorDefaultBrush"]
            };
            return sCValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
