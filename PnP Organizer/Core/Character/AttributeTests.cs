using PnP_Organizer.Models;
using PnP_Organizer.Properties;
using System.Collections.Generic;

namespace PnP_Organizer.Core.Character
{
    public static class AttributeTests
    {
        public static readonly List<AttributeTestModel> Models = new()
        {
             new(Resources.AttributeTests_Acrobatic, AttributeType.Dexterity),
             new(Resources.AttributeTests_Athletic, AttributeType.Strength),
             new(Resources.AttributeTests_Insight, AttributeType.Wisdom),
             new(Resources.AttributeTests_Intimidate, AttributeType.Charisma),
             new(Resources.AttributeTests_SleightOfHand, AttributeType.Dexterity),
             new(Resources.AttributeTests_History, AttributeType.Intelligence),
             new(Resources.AttributeTests_Physique, AttributeType.Constitution),
             new(Resources.AttributeTests_FirstAid, AttributeType.Wisdom),
             new(Resources.AttributeTests_Nature, AttributeType.Intelligence),
             new(Resources.AttributeTests_Performance, AttributeType.Charisma),
             new(Resources.AttributeTests_SneakHide, AttributeType.Dexterity),
             new(Resources.AttributeTests_Bluff, AttributeType.Charisma),
             new(Resources.AttributeTests_HandleAnimals, AttributeType.Wisdom),
             new(Resources.AttributeTests_Inspect, AttributeType.Intelligence),
             new(Resources.AttributeTests_Persuade, AttributeType.Charisma),
             new(Resources.AttributeTests_Perceive, AttributeType.Wisdom)
        };
    }
}
