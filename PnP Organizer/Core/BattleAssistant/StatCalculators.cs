using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.Core.Character.SkillSystem;
using PnP_Organizer.Core.Character.StatModifiers;
using PnP_Organizer.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP_Organizer.Core.BattleAssistant
{
    public class StatCalculators
    {

        public static int CalculateDamage(InventoryWeapon? weaponItem, List<CalculatorStatModifier> statModifiers)
        {
            var baseRollCount = weaponItem != null ? weaponItem.DiceRollCount : 1;
            var baseDamageBonus = weaponItem != null ? weaponItem.BaseDamageBonus : 0;
            var baseDice = weaponItem != null ? weaponItem.BaseDamageDice : Dice.D4;

            var damageModifiers = statModifiers.Where(modifier => modifier.CalculatorValueType is CalculatorValueType.Damage);
            var damageDiceModifiers = statModifiers.Where(modifier => modifier.CalculatorValueType is CalculatorValueType.DamageDice);

            var currentDice = baseDice;
            if (damageDiceModifiers.Any())
            {
                if (damageDiceModifiers.Count() > 1)
                    Logger.LogException(new ArgumentOutOfRangeException(nameof(damageDiceModifiers), "There is only one skill, which modifies the BaseDice, but there are more than one modifiers in the given Enumerable!"));

                var diceModifier = damageDiceModifiers.First();
                if (diceModifier.ApplianceMode == ApplianceMode.EndValue)
                    Logger.LogException(new ArgumentException("ApplianceMode can't be \"EndValue\" for Base Dice boni"));
                currentDice = diceModifier.Dice;
            }

            var random = new Random();
            var rollSum = 0;
            for (var roll = 0; roll < baseRollCount; roll++)
            {
                rollSum += random.Next(0, currentDice.MaxValue + 1) * currentDice.Multiplier;
            }
            double baseDamage = rollSum + baseDamageBonus;

            var additiveBaseModifiers = damageModifiers.Where(modifier => modifier.ApplianceMode == ApplianceMode.BaseValue
                && modifier.CalculatorBonusType == CalculatorBonusType.Additive);
            foreach (var additiveModifier in additiveBaseModifiers)
            {
                baseDamage += additiveModifier.Bonus;
                if (additiveModifier.Dice.MaxValue > 1)
                {
                    random = new Random();
                    baseDamage += random.Next(0, additiveModifier.Dice.MaxValue + 1);
                }
            }

            var multiplicativeBaseModifiers = damageModifiers.Where(modifier => modifier.ApplianceMode == ApplianceMode.BaseValue
                 && modifier.CalculatorBonusType == CalculatorBonusType.Multiplicative);
            foreach (var multiplicativeModifier in multiplicativeBaseModifiers)
            {
                baseDamage *= multiplicativeModifier.Bonus;
                if (multiplicativeModifier.Dice.MaxValue > 1)
                {
                    random = new Random();
                    baseDamage *= random.Next(0, multiplicativeModifier.Dice.MaxValue + 1);
                }
            }

            var currentDamage = baseDamage;

            var additiveEndModifiers = damageModifiers.Where(modifier => modifier.ApplianceMode == ApplianceMode.EndValue
                && modifier.CalculatorBonusType == CalculatorBonusType.Additive);
            foreach (var additiveModifier in additiveEndModifiers)
            {
                currentDamage += additiveModifier.Bonus;
                if (additiveModifier.Dice.MaxValue > 1)
                {
                    random = new Random();
                    currentDamage += random.Next(0, additiveModifier.Dice.MaxValue + 1);
                }
            }

            var multiplicativeEndModifiers = damageModifiers.Where(modifier => modifier.ApplianceMode == ApplianceMode.EndValue
                 && modifier.CalculatorBonusType == CalculatorBonusType.Multiplicative);
            foreach (var multiplicativeModifier in additiveEndModifiers)
            {
                currentDamage *= multiplicativeModifier.Bonus;
                if (multiplicativeModifier.Dice.MaxValue > 1)
                {
                    random = new Random();
                    currentDamage *= random.Next(0, multiplicativeModifier.Dice.MaxValue + 1);
                }
            }

            return (int)Math.Ceiling(currentDamage);
        }

        public static int CalculateHit(InventoryWeapon? weaponItem, List<CalculatorStatModifier> statModifiers)
        {
            return 0;
        }

        public static int CalculateArmorpen(InventoryWeapon? weaponItem, List<CalculatorStatModifier> statModifiers)
        {
            return 0;
        }

        public static int CalculateArmor(InventoryArmor? armorItem, List<CalculatorStatModifier> statModifiers)
        {
            return 0;
        }

        public static int CalculateDodge(List<CalculatorStatModifier> statModifiers)
        {
            return 0;
        }

        public static int CalculateParry(InventoryShield? shieldItem, List<CalculatorStatModifier> statModifiers)
        {
            return 0;
        }
    }
}
