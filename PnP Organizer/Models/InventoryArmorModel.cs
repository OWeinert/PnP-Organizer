using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core;
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

        public InventoryArmorModel() : this (new InventoryArmor()) { }

        public InventoryArmorModel(InventoryArmor inventoryArmor) : base(inventoryArmor)
        {
            IsInitialized = false;

            Armor = inventoryArmor.Armor;
            PutOnTime = inventoryArmor.PutOnTime;
            Weight = inventoryArmor.Weight;
            Loudness = inventoryArmor.Loudness;

            if (inventoryArmor.Color != Utils.GetColorValue(((SolidColorBrush)Application.Current.Resources["PalettePrimaryBrush"]).Color)
                && inventoryArmor.Color != Utils.GetColorValue(((SolidColorBrush)Application.Current.Resources["PaletteBrownBrush"]).Color))
            {
                Brush = new SolidColorBrush(Utils.GetColorFromValue(inventoryArmor.Color));
            }
            else
                Brush = (SolidColorBrush)Application.Current.Resources["PaletteBrownBrush"];

            PropertyChanged += InventoryArmorModel_PropertyChanged;

            IsInitialized = true;
        }

        private void InventoryArmorModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var inventoryArmor = (InventoryArmor)InventoryItem;
            inventoryArmor.Armor = Armor;
            inventoryArmor.PutOnTime = PutOnTime;
            inventoryArmor.Weight = Weight;
            inventoryArmor.Loudness = Loudness;
        }
    }
}
