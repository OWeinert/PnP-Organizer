using PnP_Organizer.Core.BattleAssistant
using PnP_Organizer.Core.Character.SkillSystem;

namespace PnP_Organizer.Core.Character.StatModifiers
{
    public class CalculatorModifierStatModifier : StatModifier
    {
        public CalculatorValueType CalculatorValueType { get; private set; }
        public ApplianceMode ApplianceMode { get; private set; }
        public Dice Dice { get; private set; }
        public double Bonus { get; private set; }
        public CalculatorBonusType CalculatorBonusType { get; private set; }

        public CalculatorModifierStatModifier(CalculatorValueType calculatorValueType, ApplianceMode applianceMode, double bonus,
            CalculatorBonusType calculatorBonusType = CalculatorBonusType.Additive)
            : this(calculatorValueType, applianceMode, Dice.D1, bonus, calculatorBonusType) { }

        public CalculatorModifierStatModifier(CalculatorValueType calculatorValueType, ApplianceMode applianceMode, Dice dice, double bonus = 0, CalculatorBonusType calculatorBonusType = CalculatorBonusType.Additive)
        {
            CalculatorValueType = calculatorValueType;
            ApplianceMode = applianceMode;
            Dice = dice;
            Bonus = bonus;
            CalculatorBonusType = calculatorBonusType;
        }
    }

    public enum CalculatorValueType
    {
        Damage,
        Hit,
        ArmorPen,
        Parry,
        Armor
    }

    public enum CalculatorBonusType
    {
        Additive,
        Multiplicative
    }
}
