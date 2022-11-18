using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Calculators;
using PnP_Organizer.Core.Calculators;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.Character.StatModifiers;
using PnP_Organizer.Logging;
using PnP_Organizer.Models;
using PnP_Organizer.Views.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.ViewModels
{
    public partial class CalculatorViewModel : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private ObservableCollection<CalculatorModifierModel>? _calculatorModifiers;

        [ObservableProperty]
        private bool _isMelee = true;
        [ObservableProperty]
        private bool _isRanged = false;

        [ObservableProperty]
        private int _baseDamage = 0;
        [ObservableProperty]
        private int _rollCount = 1;
        [ObservableProperty]
        private int _rollDamageBonus = 0;

        [ObservableProperty]
        private int _baseDamageMult = 1;
        [ObservableProperty]
        private int _endDamageMult = 1;

        [ObservableProperty]
        private int _baseArmor = 0;

        [ObservableProperty]
        private List<Dice>? _dices;

        [ObservableProperty]
        private Dice _selectedDice;

        [ObservableProperty]
        private int _calculatedDamage;
        [ObservableProperty]
        private int _calculatedHit;
        [ObservableProperty]
        private int _calculatedArmor;
        [ObservableProperty]
        private int _calculatedParry;


        private readonly IPageService? _pageService;

        public CalculatorViewModel(IPageService pageService)
        {
            _pageService = pageService;

            CalculatorModifiers = new();
            UpdateCalculatorModifiers();

            Dices = Dice.Dices;

            PropertyChanged += CalculatorViewModel_PropertyChanged;
        }

        private void CalculatorViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName is nameof(IsMelee) or nameof(IsRanged))
                UpdateCalculatorModifiers();
        }

        public void OnNavigatedTo()
        {
            UpdateCalculatorModifiers();
        }

        public void OnNavigatedFrom() { }


        private void UpdateCalculatorModifiers()
        {
            CalculatorModifiers = new();

            foreach (CalculatorModifierModel modifier in StaticModifierModels.Modifiers)
            {
                if(modifier is ConditionalCalculatorModifierModel conditionalModifier)
                {
                    if (conditionalModifier.IsUsable())
                    {
                        if ((IsRanged && conditionalModifier.AttackMode == AttackMode.Ranged)
                            || (IsMelee && conditionalModifier.AttackMode == AttackMode.Melee)
                            || conditionalModifier.AttackMode == (AttackMode.Melee | AttackMode.Ranged))
                        {
                            CalculatorModifiers?.Add(conditionalModifier);
                        }
                    }
                }
                else
                    CalculatorModifiers?.Add(modifier);
            }

            /*
            SkillsViewModel viewModel = _pageService!.GetPage<SkillsPage>()!.ViewModel;
            List<Skill> activeSkills = viewModel.SkillModels!.Where(skillModel => skillModel.Skill.IsActive() && skillModel.Skill.StatModifier is CalculatorModifierStatModifier)
                .ToList().ConvertAll(skillModel => skillModel.Skill);

            foreach (Skill skill in activeSkills)
            {
                CalculatorModifierModel modifierModel = new(skill.Name, skill.Description);
                CalculatorModifiers?.Add(modifierModel);
            }
            */
        }

        // TODO CalculateValues(): Implement calculation for hit, armor and dodge/parry
        public void CalculateValues()
        {
            List<CalculatorModifierModel> activeModifiers = CalculatorModifiers!.Where(modifier => modifier.IsActive && modifier.ApplianceMode != null).ToList();
            int baseDamageRoll = DamageCalculator.RollBaseDamage(RollCount, SelectedDice);
            int fullDamageRoll = (baseDamageRoll + RollDamageBonus) * BaseDamageMult;
            int endDamage = DamageCalculator.CalculateDamage(fullDamageRoll, activeModifiers);
            CalculatedDamage = endDamage * EndDamageMult;

            CalculatedHit = 0;

            CalculatedArmor = 0;

            CalculatedParry = 0;

            if (Properties.Settings.Default.LogCalculations)
            {
                string noActiveModifiers = activeModifiers.Count > 0 ? string.Empty : "none";

                StringBuilder sb = new();
                sb.AppendLine("Damage Calculation:");
                sb.AppendLine($"Calculated Damage: {CalculatedDamage}");
                sb.AppendLine($"Base Damage: {fullDamageRoll}");
                sb.AppendLine($"    Rolled Damage: {baseDamageRoll} ({RollCount}D{SelectedDice.Name})");
                sb.AppendLine($"    Base Damage Bonus: {RollDamageBonus}");
                sb.AppendLine($"    Base Damage Multiplier: {BaseDamageMult}");
                sb.AppendLine($"End Damage: {endDamage}");
                sb.AppendLine($"    End Damage Multiplier: {EndDamageMult}");
                sb.AppendLine($"----------------------------");
                sb.AppendLine($"Calculated Hit: {CalculatedHit}");
                sb.AppendLine($"----------------------------");
                sb.AppendLine($"Calculated Armor: {CalculatedArmor}");
                sb.AppendLine($"Base Armor: {BaseArmor}");
                sb.AppendLine($"----------------------------");
                sb.AppendLine($"Calculated Dodge/Parry: {CalculatedParry}");
                sb.AppendLine($"----------------------------");
                sb.Append($"Active Modifiers: {noActiveModifiers}");
                if(activeModifiers.Count > 0)
                {
                    foreach (CalculatorModifierModel modifier in activeModifiers)
                    {
                        sb.AppendLine($"    {modifier.Name}");
                    }
                }

                Logger.Log(sb);
            }
        }
    }
}
