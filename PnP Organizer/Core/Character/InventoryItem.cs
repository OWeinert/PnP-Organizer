using PnP_Organizer.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PnP_Organizer.Core.Character
{
    public struct InventoryItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? ItemImage { get; set; }
        public string ItemImageFileExt { get; set; }
        public int Color { get; set; }

        public InventoryItem(InventoryItemModel inventoryItemModel)
        {
            Color = Utils.GetColorValue(inventoryItemModel.Brush!.Color);
            ItemImage = Utils.BitmapImageToBytes(inventoryItemModel.ItemImage);
            ItemImageFileExt = inventoryItemModel.ItemImage != null ? Path.GetExtension(inventoryItemModel.ItemImage.UriSource.AbsolutePath) : string.Empty;
            Name = inventoryItemModel.Name;
            Description = inventoryItemModel.Description;
        }

        public InventoryItem()
        {
            Color = Utils.GetColorValue((Color)((SolidColorBrush)Application.Current.Resources["PalettePrimaryBrush"]).Color);
            ItemImage = Array.Empty<byte>();
            ItemImageFileExt = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
