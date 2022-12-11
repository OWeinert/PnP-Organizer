using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.Core.Character.StatModifiers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP_Organizer.Core.BattleAssistant
{
    public class BattleTurn
    {
        public InventoryWeapon WeaponItem { get; }
        public InventoryArmor ArmorItem { get; }

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

        public int Damage { get; }
        public int Hit { get; }
        public int Armorpen { get; }
        public int Armor { get; }
        public int Parade { get; }
        public int Dodge { get; }

        private readonly List<CalculatorStatModifier> _modifiers;

        public BattleTurn(InventoryWeapon weaponItem, InventoryArmor armorItem, List<Skill> activeSkills,
            BattleAction action, int currentHealth, int currentEnergy, int currentStamina, int initiative,
            int incomingDamage = 0)
        {
            WeaponItem = weaponItem;
            ArmorItem = armorItem;
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

        }

        private void CalculateCharacterStats()
        {
            // Health
            _healthAfter = HealthBefore;
            if (Action == BattleAction.Defend)
                _healthAfter -= Math.Clamp(IncomingDamage - Armor, 0, IncomingDamage);

            var healthModifiers = _modifiers.Where(modifier => modifier.CalculatorValueType == CalculatorValueType.Health);
            if (healthModifiers.Any())
                AddStatBoni(ref _healthAfter, healthModifiers);

            // Energy
            _energyAfter = EnergyBefore - ActiveSkills.Sum(skill => skill.EnergyCost);
            var energyModifiers = _modifiers.Where(modifier => modifier.CalculatorValueType == CalculatorValueType.Energy);
            if (energyModifiers.Any())
                AddStatBoni(ref _energyAfter, energyModifiers);

            // Stamina
            _staminaAfter = StaminaBefore - ActiveSkills.Sum(skill => skill.StaminaCost);
            var staminaModifiers = _modifiers.Where(modifier => modifier.CalculatorValueType == CalculatorValueType.Stamina);
            if(staminaModifiers.Any())
                AddStatBoni(ref _staminaAfter, staminaModifiers);

            // Initiative
            ModifiedInitiative = BaseInitiative;
            var initiativeModifiers = _modifiers.Where(modifier => modifier.CalculatorValueType == CalculatorValueType.Initiative);
            if(initiativeModifiers.Any())
            {
                var initiativeBonusSum = initiativeModifiers.Sum(modifier => modifier.Bonus);
                ModifiedInitiative += (int)Math.Ceiling(initiativeBonusSum);
            }
        }

        private static void AddStatBoni(ref int statAfter, IEnumerable<CalculatorStatModifier> statModifiers)
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
}
