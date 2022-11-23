using PnP_Organizer.Core.Calculators;
using System;

namespace PnP_Organizer.Core.Character.StatModifiers
{
    public class AttributeTestStatModifier : StatModifier
    {
        public string AttributeTestName { get; private set; }
        public Dice Dice { get; private set; }
        public int Bonus { get; private set; }

        public AttributeTestStatModifier(string attributeTestName, int bonus) 
            : this(attributeTestName, Dice.D1, bonus) { }

        public AttributeTestStatModifier(string attributeTestName, Dice dice, int bonus = 0)
        {
            AttributeTestName = attributeTestName;
            Dice = dice;
            Bonus = bonus;
        }
    }
}
