using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.BattleAssistant;
using PnP_Organizer.Core.Character.Inventory;
using System.Windows;
using System.Windows.Media;
using Wpf.Ui.Common;

namespace PnP_Organizer.Models
{
    public partial class InventoryShieldModel : InventoryItemModel
    {
        [ObservableProperty]
        private int _paradeBonus = 0;
        [ObservableProperty]
        private Dice _paradeDiceBonus = Dice.D4;
        [ObservableProperty]
        private float _weight = 1.0f;

        public InventoryShieldModel() : this (new InventoryShield()) { }

        public InventoryShieldModel(InventoryShield inventoryShield) : base(inventoryShield)
        {
            IsInitialized = false;

            ParadeBonus = inventoryShield.ParadeBonus;
            ParadeDiceBonus = inventoryShield.ParadeDiceBonus;
            Weight = inventoryShield.Weight;

            Brush = (SolidColorBrush)Application.Current.Resources["PaletteBrownBrush"];

            PropertyChanged += InventoryShieldModel_PropertyChanged;

            IsInitialized = true;
        }

        private void InventoryShieldModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var inventoryShield = (InventoryShield)InventoryItem;

            inventoryShield.ParadeBonus = ParadeBonus;
            inventoryShield.ParadeDiceBonus = ParadeDiceBonus;
            inventoryShield.Weight = Weight;
        }
    }
}
