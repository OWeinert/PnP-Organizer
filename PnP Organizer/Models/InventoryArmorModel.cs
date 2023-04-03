using CommunityToolkit.Mvvm.ComponentModel;
<<<<<<< HEAD
=======
using PnP_Organizer.Core;
>>>>>>> 49-rework-calculator-into-a-full-fledged-battle-round-calculator-and-info-page
using PnP_Organizer.Core.Character.Inventory;
using System.Windows;
using System.Windows.Media;
using Wpf.Ui.Common;

namespace PnP_Organizer.Models
{
    public partial class InventoryArmorModel : InventoryItemModel
    {
        [ObservableProperty]
        private int _armor = 0;
        [ObservableProperty]
        private float _putOnTime = 0.0f;
        [ObservableProperty]
        private float _weight = 1.0f;
        [ObservableProperty]
        private float _loudness = 0.0f;
<<<<<<< HEAD
        [ObservableProperty]
        private bool _isShield = false;

        [ObservableProperty]
        private SymbolRegular _trueFalseSymbol = SymbolRegular.Dismiss12;
=======
>>>>>>> 49-rework-calculator-into-a-full-fledged-battle-round-calculator-and-info-page

        public InventoryArmorModel() : this (new InventoryArmor()) { }

        public InventoryArmorModel(InventoryArmor inventoryArmor) : base(inventoryArmor)
        {
            IsInitialized = false;

            Armor = inventoryArmor.Armor;
            PutOnTime = inventoryArmor.PutOnTime;
            Weight = inventoryArmor.Weight;
            Loudness = inventoryArmor.Loudness;
<<<<<<< HEAD
            IsShield = inventoryArmor.IsShield;

            Brush = (SolidColorBrush)Application.Current.Resources["PaletteBrownBrush"];
=======

            if (inventoryArmor.Color != Utils.GetColorValue(((SolidColorBrush)Application.Current.Resources["PalettePrimaryBrush"]).Color)
                && inventoryArmor.Color != Utils.GetColorValue(((SolidColorBrush)Application.Current.Resources["PaletteBrownBrush"]).Color))
            {
                Brush = new SolidColorBrush(Utils.GetColorFromValue(inventoryArmor.Color));
            }
            else
                Brush = (SolidColorBrush)Application.Current.Resources["PaletteBrownBrush"];
>>>>>>> 49-rework-calculator-into-a-full-fledged-battle-round-calculator-and-info-page

            PropertyChanged += InventoryArmorModel_PropertyChanged;

            IsInitialized = true;
        }

        private void InventoryArmorModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var inventoryArmor = (InventoryArmor)InventoryItem;
<<<<<<< HEAD

            if (e.PropertyName is not nameof(TrueFalseSymbol))
            {
                inventoryArmor.Armor = Armor;
                inventoryArmor.PutOnTime = PutOnTime;
                inventoryArmor.Weight = Weight;
                inventoryArmor.Loudness = Loudness;
                inventoryArmor.IsShield = IsShield;

                TrueFalseSymbol = IsShield ? SymbolRegular.Checkmark12 : SymbolRegular.Dismiss12;
            }

=======
            inventoryArmor.Armor = Armor;
            inventoryArmor.PutOnTime = PutOnTime;
            inventoryArmor.Weight = Weight;
            inventoryArmor.Loudness = Loudness;
>>>>>>> 49-rework-calculator-into-a-full-fledged-battle-round-calculator-and-info-page
        }
    }
}
