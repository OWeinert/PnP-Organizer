using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core;
using PnP_Organizer.Core.Character;
using PnP_Organizer.IO;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace PnP_Organizer.Models
{
    /// <summary>
    /// Data for Inventory items
    /// </summary>
    public partial class InventoryItemModel : ObservableObject
    {
        private InventoryItem _inventoryItem;
        public InventoryItem InventoryItem
        {
            get { return _inventoryItem; }
            set { _inventoryItem = value; }
        }

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private BitmapImage? _itemImage;
        private string _itemImageFileExt = string.Empty;

        [ObservableProperty]
        private SolidColorBrush? _brush;

        [ObservableProperty]
        private SolidColorBrush? foreground;

        private bool _isInitialized = false;

        public InventoryItemModel() : this(new InventoryItem()) { }

        public InventoryItemModel(InventoryItem inventoryItem)
        {
            PropertyChanged += OnItemPropertyChanged;
            InventoryItem = inventoryItem;

            Name = _inventoryItem.Name;
            Description = _inventoryItem.Description;
            if(_inventoryItem.ItemImage != null)
                ItemImage = Utils.BitmapImageFromBytes(_inventoryItem.ItemImage);
            _itemImageFileExt = inventoryItem.ItemImageFileExt;
            Brush = new SolidColorBrush(Utils.GetColorFromValue(_inventoryItem.Color));
            Foreground = (SolidColorBrush)Application.Current.Resources["TextFillColorPrimaryBrush"];

            _isInitialized = true;
        }

        private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_isInitialized)
            {
                if(ItemImage?.UriSource != null)
                    _itemImageFileExt = Path.GetExtension(ItemImage.UriSource.AbsolutePath);

                switch (e.PropertyName)
                {
                    case nameof(Name):
                        _inventoryItem.Name = Name;
                        break;
                    case nameof(Description):
                        _inventoryItem.Description = Description;
                        break;
                    case nameof(ItemImage):
                        _inventoryItem.ItemImage = Utils.BitmapImageToBytes(ItemImage, _itemImageFileExt);
                        _inventoryItem.ItemImageFileExt = _itemImageFileExt;
                        break;
                    case nameof(Brush):
                        _inventoryItem.Color = Utils.GetColorValue(Brush!.Color);
                        break;
                    default:
                        break;
                }

                FileIO.IsCharacterSaved = false;
            }            
        }
    }
}
