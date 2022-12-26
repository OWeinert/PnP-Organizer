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
using PnP_Organizer.Properties;
using System.Windows.Media.Media3D;
using System.Diagnostics;

namespace PnP_Organizer.ViewModels
{
    public partial class CalculatorViewModel : ObservableObject, INavigationAware
    {
        public List<LocalizedBattleAction> BattleActions { get; }

        [ObservableProperty]
        private BattleTurn? _currentTurn;
        [ObservableProperty]
        private int _currentTurnCount = 0;

        [ObservableProperty]
        private ObservableCollection<ItemSelectorModel> _itemSelectorModels = new();

        [ObservableProperty]
        private InventoryWeapon? _selectedWeapon;
        [ObservableProperty]
        private InventoryArmor? _selectedArmor;
        [ObservableProperty]
        private InventoryShield? _selectedShield;

        [ObservableProperty]
        private ObservableCollection<CalculatorSkillModel> _calculatorSkillModels = new();

        [ObservableProperty]
        private ObservableCollection<CalculatorStatResultModel> _calculatorResultModels = new();

        [ObservableProperty]
        private LocalizedBattleAction _action;

        [ObservableProperty]
        private int _incomingDamage;

        [ObservableProperty]
        private TurnPhase _turnPhase = TurnPhase.PreTurn;
        [ObservableProperty]
        private BattlePhase _battlePhase = BattlePhase.BetweenBattles;

        private int _health = 0;
        private int _energy = 0;
        private int _stamina = 0;
        private int _initiative = 0;

        private List<Skill> _passiveSkills = new();
        private List<Skill> _activeSkills = new();

        private readonly IPageService _pageService;

        public CalculatorViewModel(IPageService pageService)
        {
            _pageService = pageService;
            BattleActions = Enum.GetValues<BattleAction>().ToList()
                .ConvertAll(battleAction => new LocalizedBattleAction(battleAction, Resources.ResourceManager.GetString($"Calculator_BattleAction{battleAction}")!));
            PropertyChanged += CalculatorViewModel_PropertyChanged;

            CalculatorSkillModels.CollectionChanged += CalculatorSkillModels_CollectionChanged;
        }
        public void OnNavigatedTo()
        {
            if (BattlePhase == BattlePhase.BetweenBattles)
                LoadCharacterStats();
        }

        public void OnNavigatedFrom() 
        {
            // only uncomment after the calculator is working fully!
            //SaveCharacterStats();
        }

        [RelayCommand]
        public void AbortBattle()
        {
            // TODO add optional dialog
            CalculatorResultModels?.Clear();
            BattlePhase = BattlePhase.BetweenBattles;

            LoadCharacterStats();

            CurrentTurn = null;

            ItemSelectorModels?.Clear();
            CalculatorSkillModels?.Clear();
        }

        [RelayCommand]
        private void StartNewBattle()
        {
            BattlePhase = BattlePhase.InBattle;
            foreach (var passiveSkill in _passiveSkills)
            {
                passiveSkill.UsesLeft = passiveSkill.UsesPerBattle;
            }
            foreach (var activeSkill in _activeSkills)
            {
                activeSkill.UsesLeft = activeSkill.UsesPerBattle;
            }
            CurrentTurnCount = -1; // NewTurn() increases the CurrentTurnCount by one, so the start count must be one less than 0

            LoadItems();
            LoadCharacterSkills();
            NewTurn();
        }

        [RelayCommand]
        private void RestartBattle()
        {
            // TODO add dialog
            AbortBattle();
            StartNewBattle();
        }

        [RelayCommand]
        private void EndBattle()
        {
            // TODO add dialog
            EndTurn();
            BattlePhase = BattlePhase.BetweenBattles;
        }

        [RelayCommand]
        private void ManageTurn()
        {
            if (TurnPhase == TurnPhase.PreTurn)
                EndTurn();
            else if (TurnPhase == TurnPhase.PostTurn)
                NewTurn();
        }

        private void CalculatorViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SelectedWeapon) or nameof(SelectedArmor) or nameof(SelectedShield)
                or nameof(Action) or nameof(_passiveSkills) or nameof(_activeSkills))
            {
                PopulateCalculatorSkillModels();
            }
        }
        private void CalculatorSkillModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var obj in e.NewItems)
                {
                    var calculatorSkillModel = (CalculatorSkillModel)obj;
                    calculatorSkillModel.PropertyChanged += (sender, e) =>
                    {
                        // Checks if each CalculatorSkillModel is Activatable depending on the stamina and energy costs
                        // of the currently activated skills
                        if (e.PropertyName == nameof(CalculatorSkillModel.IsActive))
                        {
                            var validModels = CalculatorSkillModels.Where(model => model.IsActive
                                && (model.Skill.EnergyCost > 0 || model.Skill.StaminaCost > 0));

                            var energyCostSum = validModels.Sum(model => model.Skill.EnergyCost);
                            var staminaCostSum = validModels.Sum(model => model.Skill.StaminaCost);

                            Debug.WriteLine($"{_energy}:{energyCostSum} || {_stamina}:{staminaCostSum}");

                            var otherModels = CalculatorSkillModels.Except(validModels);
                            foreach (var otherModel in otherModels)
                            {
                                var enoughEnergy = otherModel.Skill.EnergyCost <= 0
                                    || otherModel.Skill.EnergyCost > 0 && energyCostSum + otherModel.Skill.EnergyCost <= _energy;
                                var enoughStamina = otherModel.Skill.StaminaCost <= 0
                                    || otherModel.Skill.StaminaCost > 0 && staminaCostSum + otherModel.Skill.StaminaCost <= _stamina;

                                otherModel.IsActivatable = enoughEnergy && enoughStamina;
                            }
                        }
                    };
                }
            }
        }

        private void PopulateCalculatorSkillModels()
        {
            var validWeaponSkills = _activeSkills.Where(skill =>
            {
                if (SelectedWeapon != null)
                {
                    if (SelectedWeapon.AttackMode == AttackMode.Ranged)
                        return skill.SkillCategory == SkillCategory.Ranged || skill.SkillCategory == SkillCategory.Character;

                    return skill.SkillCategory == SkillCategory.Melee || skill.SkillCategory == SkillCategory.Character;
                }
                return skill.SkillCategory == SkillCategory.Character;
            });

            // TODO Add checks for armor and shield specific skills

            // this has to be done to invoke the CollectionChanged event
            // populating the collection with the constructor will not invoke it
            var calculatorSkillModels = validWeaponSkills.ToList().ConvertAll(skill => new CalculatorSkillModel(skill, CalculatorSkillModels));
            CalculatorSkillModels.Clear();
            foreach (var model in calculatorSkillModels)
            {
                CalculatorSkillModels.Add(model);
            }
        }

        private void EndTurn()
        {
            var usedSkills = CalculatorSkillModels.Where(calcSkillModel => calcSkillModel.IsActive && calcSkillModel.Skill.UsesLeft > 0)
                .ToList().ConvertAll(calcSkillModel => calcSkillModel.Skill).Concat(_passiveSkills);

            var activatedPassiveSkills = _passiveSkills.Where(skill => IsSkillUsable(skill));

            CurrentTurn = new BattleTurn(_pageService, SelectedWeapon, SelectedArmor, SelectedShield, usedSkills.ToList(),
                Action.BattleAction, _health, _energy, _stamina, _initiative, IncomingDamage);

            CurrentTurn.StatsCalculated += e =>
            {
                // TODO Create a model for the stats of this calculation

                if (Action.BattleAction == BattleAction.Attack)
                {
                    // TODO implement ability to cycle through the stats of multiple attacks if needed
                }
                else
                {

                }

                var turnProperties = CurrentTurn.GetType().GetProperties().Where(propInfo => propInfo.PropertyType == typeof(int));
                var filteredProperties = turnProperties.Where(propInfo => !propInfo.Name.Contains("Before")
                                                                       && !propInfo.Name.Contains("Initiative")
                                                                       && !propInfo.Name.Contains("Attacks")
                                                                       && propInfo.Name != nameof(BattleTurn.IncomingDamage));
                foreach (var propertyInfo in filteredProperties)
                {
                    var propertyBaseName = propertyInfo.Name.Replace("After", "");
                    var propertyValue = (int)propertyInfo.GetValue(CurrentTurn)!;

                    if (propertyInfo.Name.Contains("After"))
                    {
                        var beforeProperty = turnProperties.First(propInfo => propInfo.Name == $"{propertyBaseName}Before");
                        var beforeValue = (int)beforeProperty.GetValue(CurrentTurn)!;

                        if(beforeValue != propertyValue)
                        {
                            var difference = propertyValue - beforeValue;
                            CalculatorResultModels.Add(new CalculatorStatResultModel(propertyBaseName, propertyValue, difference));
                        }
                    }
                    else
                        CalculatorResultModels.Add(new CalculatorStatResultModel(propertyBaseName, propertyValue, 0));
                }

                var usableSkills = CurrentTurn.UsedSkillsAfter;
                _passiveSkills = usableSkills!.Where(skill => skill.ActivationType == ActivationType.Passive).ToList();
                _activeSkills = usableSkills!.Where(skill => skill.ActivationType == ActivationType.Active).ToList();
            };
            CurrentTurn.CalculateTurnResults();
            TurnPhase = TurnPhase.PostTurn;
        }

        private void NewTurn()
        {
            CurrentTurnCount++;
            CalculatorResultModels?.Clear();
            TurnPhase = TurnPhase.PreTurn;
        }

        private void LoadCharacterStats()
        {
            var character = FileIO.LoadedCharacter;

            _health = character.CurrentHealth;
            _energy = character.CurrentEnergy;
            _stamina = character.CurrentStamina;
            _initiative = _pageService.GetPage<OverviewPage>()!.ViewModel!.Initiative + character.InitiativeBonus;
        }

        private void SaveCharacterStats()
        {
            FileIO.LoadedCharacter.CurrentHealth = _health;
            FileIO.LoadedCharacter.CurrentEnergy = _energy;
            FileIO.LoadedCharacter.CurrentStamina = _stamina;
        }

        private void LoadCharacterSkills()
        {
            var skillModels = _pageService.GetPage<SkillsPage>()!.ViewModel!.SkillModels;
            var skilledSkills = skillModels!.Where(skillModel => skillModel.IsActive && skillModel is not RepeatableSkillModel)
                .ToList().ConvertAll(skillModel => skillModel.Skill!);
            _passiveSkills = skilledSkills!.Where(skill => skill.ActivationType == ActivationType.Passive).ToList();
            _activeSkills = skilledSkills!.Where(skill => skill.ActivationType == ActivationType.Active).ToList();

            PopulateCalculatorSkillModels();
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

        private bool IsSkillUsable(Skill skill)
        {
            var action = Action.BattleAction;
            var isUsable = true;
            if(action == BattleAction.Attack)
            {
                var usableWithWeapon = true;
                if(SelectedWeapon != null)
                {
                    if (SelectedWeapon.AttackMode == AttackMode.Melee)
                    {
                        if (skill.Name == Skills.Instance.OneHandedCombat.Name)
                            usableWithWeapon = !SelectedWeapon.IsTwoHanded;
                        else
                            usableWithWeapon = skill.SkillCategory == SkillCategory.Melee;
                    }
                    else
                        usableWithWeapon = skill.SkillCategory == SkillCategory.Ranged && skill.SkillCategory == SkillCategory.Ranged;
                }
                var usableWithShield = true;
                if (SelectedShield != null)
                {
                    if (skill.Name == Skills.Instance.ShieldBash.Name || skill.Name == Skills.Instance.SomethingWithShield.Name)
                        usableWithShield = action == BattleAction.Attack;
                    else
                        usableWithShield = action == BattleAction.Defend;
                }
                isUsable = usableWithWeapon || usableWithShield;
            }
            else if(action == BattleAction.Defend) 
            { 
                // TODO
            }

            return isUsable;
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

    public struct LocalizedBattleAction
    {
        public BattleAction BattleAction { get; set; }
        public string LocalizedName { get; set; }

        public LocalizedBattleAction(BattleAction battleAction, string localizedName)
        {
            BattleAction = battleAction;
            LocalizedName = localizedName;
        }
    }
}
