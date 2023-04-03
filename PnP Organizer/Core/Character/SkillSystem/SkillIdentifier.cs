using PnP_Organizer.Core.Character;

namespace PnP_Organizer.Core.Character.SkillSystem
{
    public record SkillIdentifier
    {
        public SkillCategory SkillCategory { get; }
        public string Name { get; }

        public SkillIdentifier(SkillCategory skillTree, string name)
        {
            SkillCategory = skillTree;
            Name = name;
        }
    }
}
