using PnP_Organizer.Core.Character.StatModifiers;
using System;
using System.Drawing;

namespace PnP_Organizer.Core.Character
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillCategory SkillCategory { get; set; }
        public int SkillPoints { get; set; }
        public int MaxSkillPoints { get; set; }

        public StatModifier[]? StatModifiers { get; private set; }

        /// <summary>
        /// Names of skills which need to be skilled in order to unlock this skill.
        /// </summary>
        // HACK Direct Skill references would be better, but maybe won't work with the SkillModel
        public string[] DependendSkillNames { get; private set; }

        public Skill(string name, SkillCategory skillCategory, int maxSkillPoints, string description, StatModifier[]? statModifiers = null, string[]? dependendSkillNames = null)
        {
            Name = name;
            SkillCategory = skillCategory;
            Description = description;
            MaxSkillPoints = maxSkillPoints;
            StatModifiers = statModifiers;
            SkillPoints = 0;

            DependendSkillNames = dependendSkillNames != null ? dependendSkillNames : Array.Empty<string>();
        }

        /// <summary>
        /// Checks if the skill is active, i.e. the max skill points are reached
        /// </summary>
        /// <returns></returns>
        public bool IsActive() => SkillPoints == MaxSkillPoints;
    }
}
