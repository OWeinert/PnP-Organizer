using PnP_Organizer.Core.BattleAssistant.Events;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.Core.Character.StatModifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.Core.BattleAssistant
{
    public class BattleTurn
    {
        #region Events
        public delegate void OnStatsCalculated(StatsCalculatedEventArgs e);
        /// <summary>
        /// Invoked after the stats of one attack in this turn are calculated
        /// </summary>
        public event OnStatsCalculated? StatsCalculated;
        #endregion Events

        public InventoryWeapon? WeaponItem { get; }
        public InventoryArmor? ArmorItem { get; } 
        public InventoryShield? ShieldItem { get; } 

        public List<Skill> UsedSkillsBefore { get; }
        public List<Skill>? UsedSkillsAfter { get; private set; }
        
        public BattleAction Action { get; }

        public int IncomingDamage { get; }

        public int HealthBefore { get; }
        private int _healthAfter;
        public int HealthAfter { get => _healthAfter; }

        public int EnergyBefore { get; }
        private int _energyAfter;
        public int EnergyAfter { get => _energyAfter; }

        public int StaminaBefore { get; }
        private int _staminaAfter;
        public int StaminaAfter { get => _staminaAfter; }

        public int BaseInitiative { get; }
        public int ModifiedInitiative { get; private set; }

        public int Damage { get; private set; }
        public int Hit { get; private set; }
        public int Armorpen { get; private set; }
        public int Armor { get; private set; }
        public int Parade { get; private set; }
        public int Dodge { get; private set; }

        public int Attacks { get; private set; }

        private readonly List<CalculatorStatModifier> _modifiers;
        private readonly List<CalculatorActionStatModifier> _actionModifiers;

        private readonly IPageService _pageService;

        public BattleTurn(IPageService pageService, InventoryWeapon? weaponItem, InventoryArmor? armorItem, InventoryShield? shieldItem, List<Skill> usedSkills,
            BattleAction action, int currentHealth, int currentEnergy, int currentStamina, int initiative, int incomingDamage = 0)
        {
            _pageService = pageService;

            WeaponItem = weaponItem;
            ArmorItem = armorItem;
            ShieldItem = shieldItem;
            UsedSkillsBefore = usedSkills;

            Action = action;

            IncomingDamage = incomingDamage;

            HealthBefore = currentHealth;
            EnergyBefore = currentEnergy;
            StaminaBefore = currentStamina;
            BaseInitiative = initiative;

            Attacks = 1;

            _modifiers = GetStatModifiers<CalculatorStatModifier>(usedSkills);
            _actionModifiers = GetStatModifiers<CalculatorActionStatModifier>(usedSkills);
        }

        public void CalculateTurnResults()
        {
            for(var i = 0; i < Attacks; i++)
            {
                CalculateBattleStats();
                CalculateCharacterStats();
                ExecuteModifierActions();
                DecreaseUsesLeft();
                IncreaseSkillRound();
                FilterStillActiveSkills();

                StatsCalculated?.Invoke(new StatsCalculatedEventArgs(this));
            }
        }

        private void CalculateBattleStats() 
        {
            if(Action == BattleAction.Attack)
            {
                Damage = StatCalculators.CalculateDamage(WeaponItem, _modifiers);
                Hit = StatCalculators.CalculateHit(WeaponItem, _modifiers);
                Armorpen = StatCalculators.CalculateArmorpen(WeaponItem, _modifiers);
            }
            else if(Action == BattleAction.Defend)
            {
                Armor = StatCalculators.CalculateArmor(ArmorItem, _modifiers);
                Dodge = StatCalculators.CalculateDodge(_modifiers);
                Parade = StatCalculators.CalculateParry(ShieldItem, _modifiers);
            }
        }

        private void CalculateCharacterStats()
        {
            // Health
            _healthAfter = HealthBefore;
            if (Action == BattleAction.Defend)
                _healthAfter -= Math.Clamp(IncomingDamage - Armor, 0, IncomingDamage);
            AddStatBoni(ref _healthAfter, CalculatorValueType.Health);

            // Energy
            _energyAfter = EnergyBefore - UsedSkillsBefore.Sum(skill => skill.EnergyCost);
            AddStatBoni(ref _energyAfter, CalculatorValueType.Energy);

            // Stamina
            _staminaAfter = StaminaBefore - UsedSkillsBefore.Sum(skill => skill.StaminaCost);
            AddStatBoni(ref _staminaAfter, CalculatorValueType.Stamina);

            // Initiative
            ModifiedInitiative = BaseInitiative;
            var initiativeModifiers = _modifiers.Where(modifier => modifier.CalculatorValueType == CalculatorValueType.Initiative);
            if (initiativeModifiers.Any())
            {
                var initiativeBonusSum = initiativeModifiers.Sum(modifier => modifier.Bonus);
                ModifiedInitiative += (int)Math.Ceiling(initiativeBonusSum);
            }
        }

        private void AddStatBoni(ref int statAfter, CalculatorValueType valueType)
        {
            var statModifiers = _modifiers.Where(modifier => modifier.CalculatorValueType == valueType);
            if (statModifiers.Any())
            {
                var bonusSum = statModifiers.Sum(modifier => modifier.Bonus);
                var diceBoni = statModifiers.Where(modifier => modifier.Dice.MaxValue > 1)
                    .ToList().ConvertAll(modifier => modifier.Dice);

                statAfter += (int)Math.Ceiling(bonusSum);

                foreach (var diceBonus in diceBoni)
                {
                    var random = new Random();
                    var rolledBonus = random.Next(1, diceBonus.MaxValue + 1);
                    statAfter += rolledBonus;
                }
            }
        }

        private void ExecuteModifierActions()
        {
            foreach(var modifier in _actionModifiers)
            {
                modifier.Action(_pageService, this);
            }
        }

        private void DecreaseUsesLeft()
        {
            foreach(var skill in UsedSkillsBefore)
            {
                if (skill.UsesLeft > 0)
                    skill.UsesLeft--;
            }
        }

        private void IncreaseSkillRound()
        {
            foreach(var skill in UsedSkillsBefore)
            {
                skill.CurrentRound++;
            }
        }

        private void FilterStillActiveSkills() => UsedSkillsAfter = UsedSkillsBefore.Where(skill => skill.UsesLeft != 0).ToList();

        private static List<TStatMod> GetStatModifiers<TStatMod>(List<Skill> skills) where TStatMod : IStatModifier
        {
            var validSkills = skills.Where(skill => (skill.StatModifiers != null // StatModifiers
               && skill.StatModifiers.Any(modifier => modifier is TStatMod))
               || (skill.HasRoundDependendModifiers && skill.RoundDependendStatModifiers != null // RoundDependendStatModifiers
               && skill.RoundDependendStatModifiers.Any(modifier => modifier is TStatMod)));

            var statModifiers = new List<TStatMod>();

            foreach(var skill in validSkills)
            {
                if (skill.HasRoundDependendModifiers)
                {
                    if (skill.IsRoundDependend && skill.CurrentRound >= skill.RoundDependendStatModifiers!.Count)
                        skill.CurrentRound = 0;

                    var round = skill.IsRoundDependend ? skill.CurrentRound : skill.UsesPerBattle - skill.UsesLeft;

                    var roundModifiers = skill.RoundDependendStatModifiers[round];
                    foreach(var modifier in roundModifiers)
                    {
                        if(modifier is TStatMod statMod)
                            statModifiers.Add(statMod);
                    }
                }
                else
                {
                    foreach(var modifier in skill.StatModifiers!)
                    {
                        if(modifier is TStatMod statMod)
                            statModifiers.Add(statMod);
                    }
                }
            }
            return statModifiers;
        }
    }
}