using Octokit;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.Core.Character.SkillSystem;
using PnP_Organizer.Core.Character.StatModifiers;
using PnP_Organizer.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP_Organizer.Core.BattleAssistant
{
    public class BattleTurn
    {
        public InventoryWeapon? WeaponItem { get; }
        public InventoryArmor? ArmorItem { get; } 
        public InventoryShield? ShieldItem { get; } 

        public List<Skill> ActiveSkills { get; }

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

        private readonly List<CalculatorStatModifier> _modifiers;

        public BattleTurn(InventoryWeapon? weaponItem, InventoryArmor? armorItem, InventoryShield? shieldItem, List<Skill> activeSkills,
            BattleAction action, int currentHealth, int currentEnergy, int currentStamina, int initiative,
            int incomingDamage = 0)
        {
            WeaponItem = weaponItem;
            ArmorItem = armorItem;
            ShieldItem = shieldItem;
            ActiveSkills = activeSkills;

            Action = action;

            IncomingDamage = incomingDamage;

            HealthBefore = currentHealth;
            EnergyBefore = currentEnergy;
            StaminaBefore = currentStamina;
            BaseInitiative = initiative;

            _modifiers = GetCalculatorModifiers();

            CalculateBattleStats();
            CalculateCharacterStats();

            DecreaseSkillUses();
        }

        private List<CalculatorStatModifier> GetCalculatorModifiers()
        {
            var validSkills = ActiveSkills.Where(skill => skill.StatModifiers != null
                && skill.StatModifiers.Any(modifier => modifier is CalculatorStatModifier));

            var statModifiers = validSkills.SelectMany(skill => skill.StatModifiers!, (skill, modifier) => modifier is CalculatorStatModifier)
                .Cast<CalculatorStatModifier>().ToList();

            return statModifiers;
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
            _energyAfter = EnergyBefore - ActiveSkills.Sum(skill => skill.EnergyCost);
            AddStatBoni(ref _energyAfter, CalculatorValueType.Energy);

            // Stamina
            _staminaAfter = StaminaBefore - ActiveSkills.Sum(skill => skill.StaminaCost);
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

        private void DecreaseSkillUses()
        {

        }
    }
}
