using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Properties;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace PnP_Organizer.Models
{
    [INotifyPropertyChanged]
    public partial class AttributeTestModel
    {
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private AttributeType _attributeType;
        [ObservableProperty]
        private string _localizedAttributeType = string.Empty;
        [ObservableProperty]
        private int _baseBonus = 0;
        [ObservableProperty]
        private int[] _externalBoni = Array.Empty<int>();
        [ObservableProperty]
        private int _bonusSum = 0;
        [ObservableProperty]
        private string _toolTip = string.Empty;
        [ObservableProperty]
        private SolidColorBrush _brush = Brushes.White;
        [ObservableProperty]
        private bool _hasToolTip = false;

        public AttributeTestModel(string name, AttributeType attributeType, int[]? externalBoni = null)
        {
            Name = name;
            AttributeType = attributeType;

            if(externalBoni != null)
                _externalBoni = externalBoni;

            LocalizedAttributeType = AttributeType switch
            {
                AttributeType.Strength => Resources.AttributeTests_TypeStrength,
                AttributeType.Constitution => Resources.AttributeTests_TypeConstitution,
                AttributeType.Dexterity => Resources.AttributeTests_TypeDexterity,
                AttributeType.Wisdom => Resources.AttributeTests_TypeWisdom,
                AttributeType.Intelligence => Resources.AttributeTests_TypeIntelligence,
                AttributeType.Charisma => Resources.AttributeTests_TypeCharisma,
                _ => string.Empty,
            };

            Brush = AttributeType switch
            {
                AttributeType.Strength => (SolidColorBrush)Application.Current.Resources["PaletteRedBrush"],
                AttributeType.Constitution => (SolidColorBrush)Application.Current.Resources["PaletteGreyBrush"],
                AttributeType.Dexterity => (SolidColorBrush)Application.Current.Resources["PaletteYellowBrush"],
                AttributeType.Wisdom => (SolidColorBrush)Application.Current.Resources["PaletteLightGreenBrush"],
                AttributeType.Intelligence => (SolidColorBrush)Application.Current.Resources["PaletteCyanBrush"],
                AttributeType.Charisma => (SolidColorBrush)Application.Current.Resources["PalettePinkBrush"],
                _ => Brushes.White,
            };

            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName is nameof(BaseBonus) or nameof(ExternalBoni))
                    UpdateBonusSum();

                HasToolTip = GetHasToolTip();
                ToolTip = GetToolTip();
            };
        }

        public void UpdateBonusSum() => BonusSum = BaseBonus + ExternalBoni.Sum();

        private string GetToolTip()
        {
            if (HasToolTip)
            {
                StringBuilder sb = new();
                sb.Append($"{BaseBonus} ");
                foreach (int externalBonus in ExternalBoni)
                {
                    if (externalBonus != 0)
                    {
                        string plusMinus = externalBonus > 0 ? "+" : "-";
                        sb.Append($"{plusMinus} {externalBonus} ");
                    }
                }
                sb.Append($"= {BonusSum}");
                return sb.ToString();
            }
            return string.Empty;
        }

        private bool GetHasToolTip() => ExternalBoni.Any(bonus => bonus != 0);
    }
}
