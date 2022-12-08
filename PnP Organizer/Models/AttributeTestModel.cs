using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Calculators;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Properties;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace PnP_Organizer.Models
{
    public partial class AttributeTestModel : ObservableObject
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
        private int _pearlBonus = 0;
        [ObservableProperty]
        private ObservableCollection<int> _externalBoni = new();
        [ObservableProperty]
        private ObservableCollection<Dice> _externalDiceBoni = new();
        [ObservableProperty]
        private ObservableCollection<int> _professionBoni = new();
        [ObservableProperty]
        private int _bonusSum = 0;
        [ObservableProperty]
        private string _totalBonus = string.Empty;
        [ObservableProperty]
        private string _toolTip = string.Empty;
        [ObservableProperty]
        private SolidColorBrush _brush = Brushes.White;
        [ObservableProperty]
        private bool _hasToolTip = false;

        public AttributeTestModel(string name, AttributeType attributeType)
        {
            Name = name;
            AttributeType = attributeType;

            LocalizedAttributeType = (string)typeof(Resources)
                                        .GetProperty($"AttributeTests_Type{attributeType}", BindingFlags.Public | BindingFlags.Static)!
                                        .GetValue(null, null)!;

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
                UpdateToolTip();
            };

            ExternalBoni.CollectionChanged += (sender, e) =>
            {
                UpdateBonusSum();
                UpdateToolTip();
            };

            ExternalDiceBoni.CollectionChanged += (sender, e) =>
            {
                UpdateTotalBonus();
                UpdateToolTip();
            };

        }

        public void UpdateBonusSum() => BonusSum = BaseBonus + PearlBonus + ExternalBoni.Sum() + ProfessionBoni.Sum();

        public void UpdateTotalBonus()
        {
            var sb = new StringBuilder();
            if(BonusSum > 0 || !ExternalDiceBoni.Any())
                sb.Append($"{BonusSum} ");

            var sameDiceBoni = ExternalDiceBoni.GroupBy(dice => dice.Name);
            foreach (var diceGroup in sameDiceBoni)
            {
                sb.Append($"+ {diceGroup.Count()}D{diceGroup.First().Name} ");
            }

            TotalBonus = sb.ToString();
        }

        public void UpdateToolTip()
        {
            HasToolTip = GetHasToolTip();
            ToolTip = GetToolTip();
        }

        private string GetToolTip()
        {
            if (HasToolTip)
            {
                var sb = new StringBuilder();
                sb.Append($"{BaseBonus} ");
                AppendExtraBonus(ref sb, PearlBonus);

                foreach(int professionBonus in ProfessionBoni)
                {
                    AppendExtraBonus(ref sb, professionBonus);
                }
                foreach (int externalBonus in ExternalBoni)
                {
                    AppendExtraBonus(ref sb, externalBonus);
                }
                
                foreach(var diceBonus in ExternalDiceBoni)
                {
                    sb.Append($"+ 1D{diceBonus.Name} ");
                }

                if (ExternalDiceBoni.Any())
                {
                    int maxBonus = BonusSum + ExternalDiceBoni.Sum(dice => dice.MaxValue);
                    sb.Append($"= {BonusSum + ExternalDiceBoni.Count} <-> {maxBonus}");
                }
                else
                    sb.Append($"= {BonusSum}");

                return sb.ToString();
            }
            return string.Empty;
        }

        private static void AppendExtraBonus(ref StringBuilder sb, int bonus)
        {
            if (bonus != 0)
            {
                char plusMinus = bonus > 0 ? '+' : '-';
                sb.Append($"{plusMinus} {Math.Abs(bonus)} ");
            }
        }

        private bool GetHasToolTip() => PearlBonus != 0 || ExternalBoni.Any(bonus => bonus != 0) || ExternalDiceBoni.Any() || ProfessionBoni.Any(bonus => bonus != 0);
    }
}
