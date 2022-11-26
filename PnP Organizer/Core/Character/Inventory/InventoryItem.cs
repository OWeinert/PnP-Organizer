using PnP_Organizer.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace PnP_Organizer.Core.Character.Inventory
{
    [XmlInclude(typeof(InventoryWeapon))]
    [XmlInclude(typeof(InventoryArmor))]
    public class InventoryItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? ItemImage { get; set; }
        public string ItemImageFileExt { get; set; }
        public int Color { get; set; }

        public InventoryItem(string name, string description)
        {
            Name = name;
            Description = description;
            ItemImage = Array.Empty<byte>();
            ItemImageFileExt = string.Empty;
            Color = Utils.GetColorValue(((SolidColorBrush)Application.Current.Resources["PalettePrimaryBrush"]).Color);
        }

        public InventoryItem(InventoryItemModel inventoryItemModel)
        {
            Color = Utils.GetColorValue(inventoryItemModel.Brush!.Color);
            ItemImage = Utils.BitmapImageToBytes(inventoryItemModel.ItemImage);
            ItemImageFileExt = inventoryItemModel.ItemImage != null ? Path.GetExtension(inventoryItemModel.ItemImage.UriSource.AbsolutePath) : string.Empty;
            Name = inventoryItemModel.Name;
            Description = inventoryItemModel.Description;
        }

        public InventoryItem() : this(string.Empty, string.Empty) { }

        public void SetItemImage(BitmapImage image)
        {
            ItemImage = Utils.BitmapImageToBytes(image);
            ItemImageFileExt = Path.GetExtension(image.UriSource.AbsolutePath);
        }
    }
}
