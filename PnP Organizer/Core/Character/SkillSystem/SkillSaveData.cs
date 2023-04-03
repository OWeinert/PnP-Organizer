using PnP_Organizer.Core.Character.SkillSystem;

namespace PnP_Organizer.Core.Character
{
    /// <summary>
    /// Simplified Skill which only contains it's index in the Skills.SkilList and the currently invested SkillPoints
    /// to decrease character save data size.
    /// </summary>
    public struct SkillSaveData
    {
        public SkillIdentifier Identifier { get; set; } = new SkillIdentifier(SkillCategory.Character, string.Empty);
        public int SkillPoints { get; set; } = 0;
        public int? Repetition { get; set; } = null;

        public SkillSaveData() { }
    }
}
