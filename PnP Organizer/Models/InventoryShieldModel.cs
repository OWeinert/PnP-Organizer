using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.BattleAssistant;
using PnP_Organizer.Core.Character.Inventory;
using System.Collections.Generic;
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

        [ObservableProperty]
        private List<Dice>? _dices;

        public InventoryShieldModel() : this (new InventoryShield()) { }

        public InventoryShieldModel(InventoryShield inventoryShield) : base(inventoryShield)
        {
            IsInitialized = false;

            Dices = Dice.Dices;

            ParadeBonus = inventoryShield.ParadeBonus;
            ParadeDiceBonus = inventoryShield.ParadeDiceBonus;
            Weight = inventoryShield.Weight;

            Brush = (SolidColorBrush)Application.Current.Resources["PaletteDeepPurpleBrush"];

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
