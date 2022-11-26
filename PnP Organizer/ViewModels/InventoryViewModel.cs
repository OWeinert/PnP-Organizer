using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.IO;
using PnP_Organizer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace PnP_Organizer.ViewModels
{
    // TODO InventoryViewModel: Implement Searchable Items
    public partial class InventoryViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private ICollectionView? _itemsView;

        [ObservableProperty]
        private ObservableCollection<InventoryItemModel>? _items;

        [ObservableProperty]
        private string _searchBarText = string.Empty;

        public InventoryViewModel()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            PropertyChanged += InventoryViewModel_PropertyChanged;

            Items = new ObservableCollection<InventoryItemModel>();
            Items.CollectionChanged += OnInventoryChanged;
            InitializeInventoryItemModels();
            FileIO.OnNewCharacterCreated += (sender, e) => InitializeInventoryItemModels();

            _isInitialized = true;
        }

        private void InitializeInventoryItemModels()
        {
            ObservableCollection<InventoryItemModel> itemsCollection = new()
            {
                new InventoryItemModel()
            };
            Items = itemsCollection;

            ItemsView = CollectionViewSource.GetDefaultView(Items);
            ItemsView.Filter += ItemsView_Filter;
        }

        private void InventoryViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e) => ItemsView?.Refresh();

        private void InventoryItemModel_PropertyChanged(object? sender, PropertyChangedEventArgs e) => ItemsView?.Refresh();

        private bool ItemsView_Filter(object obj)
        {
            InventoryItemModel item = (InventoryItemModel)obj;
            return string.IsNullOrWhiteSpace(SearchBarText) || item.Name.Contains(SearchBarText) || item.Description.Contains(SearchBarText);
        }

        private void OnInventoryChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            FileIO.IsCharacterSaved = false;
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (InventoryItemModel item in e.NewItems!)
                {
                    item.PropertyChanged += new PropertyChangedEventHandler(InventoryItemModel_PropertyChanged);
                }
            }
        }

        public void SaveCharacterInventory()
        {
            if(_isInitialized)
            {
                FileIO.IsCharacterSaved = false;
                List<InventoryItem> inventory = Items!.ToList().ConvertAll(itemModel => itemModel.InventoryItem);
                FileIO.LoadedCharacter.Inventory = inventory;
            }           
        }

        public void LoadCharacterInventory()
        {
            if(FileIO.LoadedCharacter.Inventory.Count > 0)
                Items?.Clear(); // Clear inventory first if the character has saved items to remove
                                // the default empty item
            ObservableCollection<InventoryItemModel> itemModels = new();
            foreach(InventoryItem item in FileIO.LoadedCharacter.Inventory!)
            {
                itemModels.Add(new InventoryItemModel(item));
            }
            Items = itemModels;

            ItemsView = CollectionViewSource.GetDefaultView(Items);
            ItemsView.Filter += ItemsView_Filter;
        }
    }
}
