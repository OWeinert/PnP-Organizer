using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PnP_Organizer.Models
{
    [INotifyPropertyChanged]
    public partial class ItemSelectorModel
    {
        public Type Type { get; }
        public string Name { get; }

        [ObservableProperty]
        private IEnumerable<InventoryItem>? _items;
        [ObservableProperty]
        private InventoryItem? _selectedItem;

        public ItemSelectorModel(IEnumerable<InventoryItem> items)
        {
            Type = items.First().GetType();

            var typeName = Type.Name.Replace("Inventory", string.Empty);
            Name = Resources.ResourceManager.GetString($"Calculator_{typeName}")!;
            
            Items = items;
        }
    }
}
