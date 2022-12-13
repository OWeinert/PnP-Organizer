using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.BattleAssistant;
using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using PnP_Organizer.IO;
using PnP_Organizer.Views.Pages;
using PnP_Organizer.Core.Character.SkillSystem;
using System.Collections.Generic;
using System;
using CommunityToolkit.Mvvm.Input;

namespace PnP_Organizer.ViewModels
{
    public partial class CalculatorViewModel : ObservableObject, INavigationAware
    {
        public List<BattleAction> BattleActions { get; }

        [ObservableProperty]
        private BattleTurn? _currentTurn;
        [ObservableProperty]
        private int _currentTurnCount = 0;

        [ObservableProperty]
        private ObservableCollection<ItemSelectorModel> _itemSelectorModels = new();

        [ObservableProperty]
        private ObservableCollection<Skill> _passiveSkills = new();
        [ObservableProperty]
        private ObservableCollection<Skill> _activeSkills = new();

        [ObservableProperty]
        private ObservableCollection<CalculatorSkillModel> _calculatorSkillModels = new();

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
        private int _healthDiff = 0;
        [ObservableProperty]
        private int _energyDiff = 0;
        [ObservableProperty]
        private int _staminaDiff = 0;

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

        [ObservableProperty]
        private TurnPhase _turnPhase = TurnPhase.PreTurn;
        [ObservableProperty]
        private BattlePhase _battlePhase = BattlePhase.BetweenBattles;

        private readonly IPageService _pageService;

        public CalculatorViewModel(IPageService pageService)
        {
            _pageService = pageService;
            PropertyChanged += CalculatorViewModel_PropertyChanged;

            BattleActions = Enum.GetValues<BattleAction>().ToList();
        }

        public void OnNavigatedTo()
        {
            if (BattlePhase == BattlePhase.BetweenBattles)
        {
            LoadItems();
            if (BattlePhase == BattlePhase.PreBattle)
            {
                LoadCharacterStats();
                UpdateSkillsList();
            }
        }

        public void OnNavigatedFrom() 
        {
            SaveCharacterStats();
        }

        [RelayCommand]
        private void StartNewBattle()
        {
            BattlePhase = BattlePhase.InBattle;
            foreach (var passiveSkill in PassiveSkills)
            {
                passiveSkill.UsesLeft = passiveSkill.UsesPerBattle;
            }
            foreach (var activeSkill in ActiveSkills)
            {
                activeSkill.UsesLeft = activeSkill.UsesPerBattle;
            }
            CurrentTurnCount = -1; // NewTurn() increases the CurrentTurnCount by one, so the start count must be one less than 0
            UpdateSkillsList();
            NewTurn();
        }

        [RelayCommand]
        private void RestartBattle()
        {
            LoadCharacterStats();
            StartNewBattle();
        }

        [RelayCommand]
        private void EndBattle()
        {
            EndTurn();
            BattlePhase = BattlePhase.PreBattle;
        }

        [RelayCommand]
        private void ManageTurn()
        {
            if (TurnPhase == TurnPhase.PreTurn)
                EndTurn();
            else if (TurnPhase == TurnPhase.PostTurn)
                NewTurn();
        }

        private void EndTurn()
        {
            var usedSkills = CalculatorSkillModels.Where(calcSkillModel => calcSkillModel.IsActive && calcSkillModel.Skill.UsesLeft > 0)
                .ToList().ConvertAll(calcSkillModel => calcSkillModel.Skill).Concat(PassiveSkills);

            var selectedWeapon = (InventoryWeapon?)ItemSelectorModels.FirstOrDefault(selectorModel => selectorModel.Type == typeof(InventoryWeapon))?.SelectedItem;
            var selectedArmor = (InventoryArmor?)ItemSelectorModels.FirstOrDefault(selectorModel => selectorModel.Type == typeof(InventoryArmor))?.SelectedItem;
            var selectedShield = (InventoryShield?)ItemSelectorModels.FirstOrDefault(selectorModel => selectorModel.Type == typeof(InventoryShield))?.SelectedItem;

            CurrentTurn = new BattleTurn(_pageService, selectedWeapon, selectedArmor, selectedShield, usedSkills.ToList(),
                Action, Health, Energy, Stamina, Initiative, IncomingDamage);

            foreach (var skill in usedSkills)
            {
                if (skill.UsesPerBattle > 0)
                    skill.UsesLeft--;
            }

            Health = CurrentTurn.HealthAfter;
            Energy = CurrentTurn.EnergyAfter;
            Stamina = CurrentTurn.StaminaAfter;
            Initiative = CurrentTurn.ModifiedInitiative;

            HealthDiff = Health - CurrentTurn.HealthBefore;
            EnergyDiff = Energy - CurrentTurn.EnergyBefore;
            StaminaDiff = Stamina - CurrentTurn.StaminaBefore;

            if (CurrentTurn.Action == BattleAction.Attack)
            {
                Damage = CurrentTurn.Damage;
                Hit = CurrentTurn.Hit;
                Armorpen = CurrentTurn.Armorpen;
            }
            else if (CurrentTurn.Action == BattleAction.Defend)
            {
                Armor = CurrentTurn.Armor;
                Dodge = CurrentTurn.Dodge;
                Parade = CurrentTurn.Parade;
            }

            TurnPhase = TurnPhase.PostTurn;
        }

        private void NewTurn()
        {
            CurrentTurnCount++;
            TurnPhase = TurnPhase.PreTurn;
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
            var skillModels = _pageService.GetPage<SkillsPage>()!.ViewModel!.SkillModels;
            var skilledSkills = skillModels!.Where(skillModel => skillModel.IsActive && skillModel is not RepeatableSkillModel)
                .ToList().ConvertAll(skillModel => skillModel.Skill!);
            PassiveSkills = new ObservableCollection<Skill>(skilledSkills!.Where(skill => skill.ActivationType == ActivationType.Passive));
            ActiveSkills = new ObservableCollection<Skill>(skilledSkills!.Where(skill => skill.ActivationType == ActivationType.Active));

            CalculatorSkillModels = new ObservableCollection<CalculatorSkillModel>(ActiveSkills.ToList().ConvertAll(skill => new CalculatorSkillModel(skill)));
        }

        private void LoadItems()
        {
            var inventoryItems = _pageService.GetPage<InventoryPage>()!.ViewModel!.Items?
                .Where(itemModel => itemModel is InventoryWeaponModel or InventoryArmorModel or InventoryShieldModel)
                .ToList().ConvertAll(itemModel => itemModel.InventoryItem);

            if (inventoryItems != null)
            {
                ItemSelectorModels.Clear();

                var weapons = inventoryItems!.Where(item => item is InventoryWeapon).Cast<InventoryWeapon>();
                if (weapons.Any())
                    ItemSelectorModels.Add(new ItemSelectorModel(weapons));

                var armors = inventoryItems!.Where(item => item is InventoryArmor).Cast<InventoryArmor>();
                if (armors.Any())
                    ItemSelectorModels.Add(new ItemSelectorModel(armors));

                var shields = inventoryItems!.Where(item => item is InventoryShield).Cast<InventoryShield>();
                if (shields.Any())
                    ItemSelectorModels.Add(new ItemSelectorModel(shields));
            }
        }
    }

    public enum TurnPhase
    {
        PreTurn,
        PostTurn
    }

    public enum BattlePhase
    {
        BetweenBattles,
        InBattle
    }
}
