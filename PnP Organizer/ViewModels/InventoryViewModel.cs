using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.IO;
using PnP_Organizer.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using Wpf.Ui.Common;

namespace PnP_Organizer.ViewModels
{
    public partial class InventoryViewModel : ObservableObject
    {
        [ObservableProperty]
        private ICollectionView? _itemsView;
        [ObservableProperty]
        private ObservableCollection<InventoryItemModel>? _items;
        [ObservableProperty]
        private string _searchBarText = string.Empty;

        private bool _isInitialized = false;

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
            var item = (InventoryItemModel)obj;
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

        [RelayCommand]
        private void AddItem() => Items!.Add(new InventoryItemModel());

        [RelayCommand]
        private void AddWeapon() => Items!.Add(new InventoryWeaponModel());

        [RelayCommand]
        private void AddArmor() => Items!.Add(new InventoryArmorModel());

        public void SaveCharacterInventory()
        {
            if(_isInitialized)
            {
                FileIO.IsCharacterSaved = false;
                var inventory = Items!.ToList().ConvertAll(itemModel => itemModel.InventoryItem);
                FileIO.LoadedCharacter.Inventory = inventory;
            }           
        }

        public void LoadCharacterInventory()
        {
            var itemModels = new ObservableCollection<InventoryItemModel>();
            foreach(var item in FileIO.LoadedCharacter.Inventory!)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    Debug.WriteLine($"{property.Name}: {property.GetValue(item)}");
                }

                InventoryItemModel? model;
                if (item is InventoryWeapon)
                    model = new InventoryWeaponModel((InventoryWeapon)item);
                else if (item is InventoryArmor)
                    model = new InventoryArmorModel((InventoryArmor)item);
                else
                    model = new InventoryItemModel(item);

                itemModels.Add(model);
            }
            Items = itemModels;

            ItemsView = CollectionViewSource.GetDefaultView(Items);
            ItemsView.Filter += ItemsView_Filter;
        }
    }
}
