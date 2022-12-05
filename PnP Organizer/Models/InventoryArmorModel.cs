using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Calculators;
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
        [ObservableProperty]
        private bool _isShield = false;

        [ObservableProperty]
        private SymbolRegular _trueFalseSymbol = SymbolRegular.Dismiss12;

        public InventoryArmorModel() : this (new InventoryArmor()) { }

        public InventoryArmorModel(InventoryArmor inventoryArmor) : base(inventoryArmor)
        {
            IsInitialized = false;

            Armor = inventoryArmor.Armor;
            PutOnTime = inventoryArmor.PutOnTime;
            Weight = inventoryArmor.Weight;
            Loudness = inventoryArmor.Loudness;
            IsShield = inventoryArmor.IsShield;

            Brush = (SolidColorBrush)Application.Current.Resources["PaletteBrownBrush"];

            PropertyChanged += InventoryArmorModel_PropertyChanged;

            IsInitialized = true;
        }

        private void InventoryArmorModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var inventoryArmor = (InventoryArmor)InventoryItem;

            if (e.PropertyName is not nameof(TrueFalseSymbol))
            {
                inventoryArmor.Armor = Armor;
                inventoryArmor.PutOnTime = PutOnTime;
                inventoryArmor.Weight = Weight;
                inventoryArmor.Loudness = Loudness;
                inventoryArmor.IsShield = IsShield;

                TrueFalseSymbol = IsShield ? SymbolRegular.Checkmark12 : SymbolRegular.Dismiss12;
            }

        }
    }
}
