using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.BattleAssistant;
using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Logging;
using PnP_Organizer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using PnP_Organizer.IO;
using PnP_Organizer.Views.Pages;

namespace PnP_Organizer.ViewModels
{
    public partial class CalculatorViewModel : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private BattleTurn? _currentTurn;
        [ObservableProperty]
        private int _currentTurnCount = 0;

        [ObservableProperty]
        private ObservableCollection<InventoryWeapon> _weapons = new();
        [ObservableProperty]
        private InventoryWeapon? _selectedWeapon;

        [ObservableProperty]
        private ObservableCollection<InventoryArmor> _armors = new();
        [ObservableProperty]
        private InventoryArmor? _selectedArmor;

        [ObservableProperty]
        private ObservableCollection<InventoryShield> _shields = new();
        [ObservableProperty]
        private InventoryShield? _selectedShield;

        [ObservableProperty]
        private ObservableCollection<Skill> _activeSkills = new();

        [ObservableProperty]
        private BattleAction _action;

        [ObservableProperty]
        private int _incomingDamage;

        [ObservableProperty]
        private int _health = 0;
        [ObservableProperty]
        private int _energy = 0;
        [ObservableProperty]
        private int _stamina = 0;
        [ObservableProperty]
        private int _initiative = 0;

        [ObservableProperty]
        private int _damage = 0;
        [ObservableProperty]
        private int _hit = 0;
        [ObservableProperty]
        private int _armorpen = 0;
        [ObservableProperty]
        private int _armor = 0;
        [ObservableProperty]
        private int _parade = 0;
        [ObservableProperty]
        private int _dodge = 0;

        private readonly IPageService _pageService;

        public CalculatorViewModel(IPageService pageService)
        {
            _pageService = pageService;
            PropertyChanged += CalculatorViewModel_PropertyChanged;
        }

        private void CalculatorViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {

        }

        public void OnNavigatedTo()
        {
            LoadItems();
            LoadCharacterStats();
            UpdateSkillsList();
        }

        public void OnNavigatedFrom() 
        {
            SaveCharacterStats();
        }

        private void LoadCharacterStats()
        {
            var character = FileIO.LoadedCharacter;

            Health = character.CurrentHealth;
            Energy = character.CurrentEnergy;
            Stamina = character.CurrentStamina;
            Initiative = _pageService.GetPage<OverviewPage>()!.ViewModel!.Initiative + character.InitiativeBonus;
        }

        private void SaveCharacterStats()
        {
            FileIO.LoadedCharacter.CurrentHealth = Health;
            FileIO.LoadedCharacter.CurrentEnergy = Energy;
            FileIO.LoadedCharacter.CurrentStamina = Stamina;
        }

        private void UpdateSkillsList()
        {
            
        }

        private void LoadItems()
        {
            var inventoryItems = _pageService.GetPage<InventoryPage>()!.ViewModel!.Items?
                .Where(itemModel => itemModel is InventoryWeaponModel or InventoryArmorModel or InventoryShieldModel)
                .ToList().ConvertAll(itemModel => itemModel.InventoryItem);

            if (inventoryItems != null)
            {
                Weapons = new ObservableCollection<InventoryWeapon>(inventoryItems!.Where(item => item is InventoryWeapon).Cast<InventoryWeapon>());
                Armors = new ObservableCollection<InventoryArmor>(inventoryItems!.Where(item => item is InventoryArmor).Cast<InventoryArmor>());
                Shields = new ObservableCollection<InventoryShield>(inventoryItems!.Where(item => item is InventoryShield).Cast<InventoryShield>());
            }
        }

        private void StartNewBattle()
        {
            CurrentTurnCount = 0;
            CurrentTurn = new BattleTurn(SelectedWeapon, SelectedArmor, SelectedShield, ActiveSkills.ToList(), 
                Action, Health, Energy, Stamina, Initiative, IncomingDamage);
        }
    }
}
