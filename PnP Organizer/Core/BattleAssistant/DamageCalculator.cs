using PnP_Organizer.Core.BattleAssistant;
using PnP_Organizer.Core.Character.SkillSystem;
using PnP_Organizer.Logging;
using PnP_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP_Organizer.BattleAssistant
{
    internal class DamageCalculator
    {
        public static int CalculateDamage(int baseDamage, List<CalculatorModifierModel> modifiers, int baseMultiplier = 1, int endMultiplier = 1)
        { 
            try
            {
                float damage = baseDamage * baseMultiplier;

                var baseModifiers = modifiers.Where(modifier => modifier.ApplianceMode == ApplianceMode.BaseValue).ToList();
                foreach(var baseModifier in baseModifiers)
                {
                    baseModifier.ApplyModifier(ref damage);
                }

                var endModifiers = modifiers.Where(modifier => modifier.ApplianceMode == ApplianceMode.EndValue).ToList();
                foreach (var endModifier in endModifiers)
                {
                    endModifier.ApplyModifier(ref damage);
                }

                damage *= endMultiplier;
                var roundedDamage = (int)Math.Round(damage);
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
            var result = 0;
            for(var i = 0; i < rollCount; i++)
            {
                result += random.Next(1, dice.MaxValue + 1);
            }
            return result;
        }
    }
}
