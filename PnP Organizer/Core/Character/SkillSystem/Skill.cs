﻿using PnP_Organizer.Core.Character.SkillSystem;
using PnP_Organizer.Core.Character.StatModifiers;
using System;

namespace PnP_Organizer.Core.Character
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillCategory SkillCategory { get; set; }
        public int SkillPoints { get; set; }
        public int MaxSkillPoints { get; set; }
        public int EnergyCost { get; set; }
        public int StaminaCost { get; set; }

        public bool IsRepeatable { get; set; }
        public ActivationType ActivationType { get; set; }

        public int UsesLeft { get; set; }
        /// <summary>
        /// -1 = infinite uses
        /// </summary>
        public int UsesPerBattle { get; set; }

        public IStatModifier[]? StatModifiers { get; private set; }

        /// <summary>
        /// Names of skills of which at least one has to be skilled in order to unlock this skill.
        /// </summary>
        // HACK Direct Skill references would be better, but maybe won't work with the SkillModel
        public string[] DependendSkillNames { get; private set; }

        /// <summary>
        /// A name of a skill which has to be skilled in order to unlock this skill.
        /// This skill AND one of the other dependend skills have to be skilled.
        /// </summary>
        public string ForcedDependendSkillName { get; private set; } = string.Empty;

        public Skill(string name, SkillCategory skillCategory, int maxSkillPoints, string description, IStatModifier[]? statModifiers = null,
            string[]? dependendSkillNames = null, ActivationType activationType = ActivationType.Passive, int energyCost = 0, int staminaCost = 0, int usesPerBattle = -1)
        {
            Name = name;
            SkillCategory = skillCategory;
            Description = description;
            MaxSkillPoints = maxSkillPoints;
            StatModifiers = statModifiers;
            SkillPoints = 0;

            ActivationType = activationType;
            EnergyCost = energyCost;
            StaminaCost = staminaCost;

            UsesLeft = UsesPerBattle = usesPerBattle;

            DependendSkillNames = dependendSkillNames ?? Array.Empty<string>();
        }

        /// <summary>
        /// Checks if the skill is active, i.e. the max skill points are reached
        /// </summary>
        /// <returns></returns>
        public bool IsActive() => SkillPoints == MaxSkillPoints;

        public Skill SetRepeatable(bool repeatable = true)
        {
            IsRepeatable = repeatable;
            return this;
        }

        public Skill AddForcedDependency(string forcedDependendSkillName)
        {
            ForcedDependendSkillName = forcedDependendSkillName;
            return this;
        }
    }
}
