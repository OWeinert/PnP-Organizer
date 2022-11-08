using PnP_Organizer.Core.Calculators;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Debug;
using PnP_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP_Organizer.Calculators
{
    internal class DamageCalculator
    {
        public static int CalculateDamage(int baseDamage, List<CalculatorModifierModel> modifiers, int baseMultiplier = 1, int endMultiplier = 1)
        { 
            try
            {
                float damage = baseDamage * baseMultiplier;

                List<CalculatorModifierModel> baseModifiers = modifiers.Where(modifier => modifier.ApplianceMode == ApplianceMode.BaseValue).ToList();
                foreach(CalculatorModifierModel baseModifier in baseModifiers)
                {
                    baseModifier.ApplyModifier(ref damage);
                }

                List<CalculatorModifierModel> endModifiers = modifiers.Where(modifier => modifier.ApplianceMode == ApplianceMode.EndValue).ToList();
                foreach (CalculatorModifierModel endModifier in endModifiers)
                {
                    endModifier.ApplyModifier(ref damage);
                }

                damage *= endMultiplier;
                int roundedDamage = (int)Math.Round(damage);
                return roundedDamage;
            }
            catch (Exception e)
            {
                Logger.LogException(e, message: "Failed to calculate Damage");
                return -1;
            }
        }

        public static int RollBaseDamage(int rollCount, Dice dice)
        {
            if (dice.MaxValue == 1)
                return 1;

            Random random = new();
            int result = 0;
            for(int i = 0; i < rollCount; i++)
            {
                result += random.Next(1, dice.MaxValue + 1);
            }
            return result;
        }
    }
}
