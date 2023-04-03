using Microsoft.Extensions.Logging;
using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.Core.Character.SkillSystem;
using PnP_Organizer.Core.Character.StatModifiers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP_Organizer.Core.BattleAssistant
{
    public class StatCalculators
    {

        public static int CalculateDamage(InventoryWeapon? weaponItem, IEnumerable<CalculatorStatModifier> statModifiers)
        {
            var baseRollCount = weaponItem?.DiceRollCount ?? 1;
            var baseDamageBonus = weaponItem?.BaseDamageBonus ?? 0;
            var baseDice = weaponItem?.BaseDamageDice ?? Dice.D4;

            var damageModifiers = statModifiers.Where(modifier => modifier.CalculatorValueType is CalculatorValueType.Damage);
            var damageDiceModifiers = statModifiers.Where(modifier => modifier.CalculatorValueType is CalculatorValueType.DamageDice);

            var currentDice = baseDice;
            if (damageDiceModifiers.Any())
            {
                if (damageDiceModifiers.Count() > 1)
                    App.GetService<ILogger<StatCalculators>>()?.LogError(new ApplicationException(), "There is only one skill, which modifies the BaseDice, but there are more than one modifiers in the given Enumerable!");

                var diceModifier = damageDiceModifiers.First();
                if (diceModifier.ApplianceMode == ApplianceMode.EndValue)
                    App.GetService<ILogger<StatCalculators>>()?.LogError(new ApplicationException(), "ApplianceMode can't be \"EndValue\" for Base Dice boni");
                currentDice = diceModifier.Dice;
            }

            var random = new Random();
            var rollSum = 0;
            for (var roll = 0; roll < baseRollCount; roll++)
            {
                random = new Random();
                rollSum += random.Next(0, currentDice.MaxValue + 1) * currentDice.Multiplier;
            }

            double baseDamage = rollSum + baseDamageBonus;
            ApplyMultipleModifierBoni(ref baseDamage, damageModifiers!, ApplianceMode.BaseValue);

            var currentDamage = baseDamage;
            ApplyMultipleModifierBoni(ref currentDamage, damageModifiers!, ApplianceMode.EndValue);

            return (int)Math.Ceiling(currentDamage);
        }

        public static int CalculateHit(InventoryWeapon? weaponItem, IEnumerable<CalculatorStatModifier> statModifiers)
        {
            double hitBonus = weaponItem?.HitBonus ?? 0;
            return CalculateD20Value(hitBonus, statModifiers, CalculatorValueType.Hit);
        }

        public static int CalculateArmorpen(InventoryWeapon? weaponItem, IEnumerable<CalculatorStatModifier> statModifiers)
        {
            double armorpen = weaponItem?.Armorpen ?? 0;
            return CalculateValueDualBoni(armorpen, statModifiers, CalculatorValueType.ArmorPen);
        }

        public static int CalculateArmor(InventoryArmor? armorItem, IEnumerable<CalculatorStatModifier> statModifiers)
        {
            double armor = armorItem?.Armor ?? 0;
            return CalculateValueDualBoni(armor, statModifiers, CalculatorValueType.Armor);
        }

        public static int CalculateDodge(List<CalculatorStatModifier> statModifiers) => CalculateD20Value(0, statModifiers, CalculatorValueType.Dodge);

        public static int CalculateParry(InventoryShield? shieldItem, IEnumerable<CalculatorStatModifier> statModifiers)
        {
            double paradeBonus = shieldItem?.ParadeBonus ?? 0;
            var baseParadeDice = shieldItem?.ParadeDiceBonus ?? Dice.D4;

            var hitModifiers = statModifiers.Where(modifier => modifier.CalculatorValueType == CalculatorValueType.Hit);

            var additiveBaseModifiers = hitModifiers.Where(modifier => modifier.CalculatorBonusType == CalculatorBonusType.Additive);
            foreach (var additiveModifier in additiveBaseModifiers)
            {
                ApplyModifierBonus(ref paradeBonus, additiveModifier, CalculatorBonusType.Additive);
            }
            var random = new Random();
            var parade = random.Next(0, baseParadeDice.MaxValue + 1) + paradeBonus;

            return (int)Math.Ceiling(parade);
        }

        private static int CalculateValueDualBoni(double baseValue, IEnumerable<CalculatorStatModifier> statModifiers, CalculatorValueType valueType)
        {
            var valueTypeModifiers = statModifiers.Where(modifier => modifier.CalculatorValueType == valueType);

            var additiveBaseModifiers = valueTypeModifiers.Where(modifier => modifier.CalculatorBonusType == CalculatorBonusType.Additive);
            foreach (var additiveModifier in additiveBaseModifiers)
            {
                ApplyModifierBonus(ref baseValue, additiveModifier, CalculatorBonusType.Additive);
            }
            var multiplicativeEndModifiers = valueTypeModifiers.Where(modifier => modifier.CalculatorBonusType == CalculatorBonusType.Multiplicative);
            foreach (var multiplicativeModifier in multiplicativeEndModifiers)
            {
                ApplyModifierBonus(ref baseValue, multiplicativeModifier, CalculatorBonusType.Multiplicative);
            }

            return (int)Math.Ceiling(baseValue);
        }

        private static int CalculateD20Value(double baseBonus, IEnumerable<CalculatorStatModifier> statModifiers, CalculatorValueType valueType)
        {
            var valueModifiers = statModifiers.Where(modifier => modifier.CalculatorValueType == valueType);

            var additiveBaseModifiers = valueModifiers.Where(modifier => modifier.CalculatorBonusType == CalculatorBonusType.Additive);
            foreach (var additiveModifier in additiveBaseModifiers)
            {
                ApplyModifierBonus(ref baseBonus, additiveModifier, CalculatorBonusType.Additive);
            }
            var random = new Random();
            var finaleValue = random.Next(0, Dice.D20.MaxValue + 1) + baseBonus;

            return (int)Math.Ceiling(finaleValue);
        }

        private static void ApplyModifierBonus(ref double baseValue, CalculatorStatModifier modifier, CalculatorBonusType bonusType)
        {
            Random random;
            if(bonusType == CalculatorBonusType.Additive)
                baseValue += modifier.Bonus;
            else
                baseValue *= modifier.Bonus;
            if (modifier.Dice.MaxValue > 1)
            {
                random = new Random();
                if(bonusType == CalculatorBonusType.Additive)
                    baseValue += random.Next(0, modifier.Dice.MaxValue + 1);
                else
                    baseValue *= random.Next(0, modifier.Dice.MaxValue + 1);
            }
        }

        private static void ApplyMultipleModifierBoni(ref double baseValue, IEnumerable<CalculatorStatModifier> statModifiers, ApplianceMode applianceMode)
        {
            var additiveEndModifiers = statModifiers.Where(modifier => modifier.ApplianceMode == applianceMode
                && modifier.CalculatorBonusType == CalculatorBonusType.Additive);
            foreach (var additiveModifier in additiveEndModifiers)
            {
                ApplyModifierBonus(ref baseValue, additiveModifier, CalculatorBonusType.Additive);
            }

            var multiplicativeEndModifiers = statModifiers.Where(modifier => modifier.ApplianceMode == applianceMode
                 && modifier.CalculatorBonusType == CalculatorBonusType.Multiplicative);
            foreach (var multiplicativeModifier in multiplicativeEndModifiers)
            {
                ApplyModifierBonus(ref baseValue, multiplicativeModifier, CalculatorBonusType.Multiplicative);
            }
        }
    }
}