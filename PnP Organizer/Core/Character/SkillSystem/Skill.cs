using PnP_Organizer.Core.Character.SkillSystem;
using PnP_Organizer.Core.Character.StatModifiers;
using PnP_Organizer.Logging;
using PnP_Organizer.Properties;
using System;
using System.Collections.Generic;
using System.Text;

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

        public bool UsableWithOtherSkills { get; private set; }
        public bool HasRoundDependendModifiers { get; private set; }

        public int UsesLeft { get; set; }
        /// <summary>
        /// -1 = infinite uses
        /// </summary>
        public int UsesPerBattle { get; set; }

        public IStatModifier[]? StatModifiers { get; private set; }
        public List<IStatModifier[]> RoundDependendStatModifiers { get; private set; }

        /// <summary>
        /// True if the skill has round dependend StatModifiers AND does not depend on UsesLeft
        /// </summary>
        public bool IsRoundDependend { get; private set; }
        public int CurrentRound { get; set; }

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

            IsRoundDependend = false;
            HasRoundDependendModifiers = false;
            UsableWithOtherSkills = true;

            UsesLeft = UsesPerBattle = usesPerBattle;

            DependendSkillNames = dependendSkillNames ?? Array.Empty<string>();

            RoundDependendStatModifiers = new List<IStatModifier[]>();
        }

        public static string CreateTooltip(Skill skill)
        {
            var sb = new StringBuilder(skill.Description);
            if (skill.StaminaCost > 0 || skill.EnergyCost > 0)
            {
                sb.Append($"\n{Resources.Skills_Cost}: ");

                if (skill.StaminaCost > 0)
                    sb.Append($"{skill.StaminaCost} {Resources.Overview_Stamina}");

                if (skill.StaminaCost > 0 && skill.EnergyCost > 0)
                    sb.Append(", ");

                if (skill.EnergyCost > 0)
                    sb.Append($"{skill.StaminaCost} {Resources.Overview_Energy}");
            }
            return sb.ToString();
        }

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

        public Skill AddRoundDependendModifiers(int round, IStatModifier[] roundStatModifiers)
        {
            RoundDependencyModifierCheck();
            RoundDependendStatModifiers.Insert(round, roundStatModifiers);
            HasRoundDependendModifiers = true;
            return this;
        }

        public Skill AddRoundDependendModifier(int round, IStatModifier roundStatModifier) => AddRoundDependendModifiers(round, new IStatModifier[] { roundStatModifier });

        public Skill SetDependendOnUsesLeft()
        {
            IsRoundDependend = false;
            return this;
        }

        public Skill SetOnlySoloUsable()
        {
            UsableWithOtherSkills = false;
            return this;
        }

        /// <summary>
        /// Checks if the skill is active, which means that the max skill points are reached
        /// </summary>
        /// <returns></returns>
        public bool IsActive() => SkillPoints == MaxSkillPoints;

        private void RoundDependencyModifierCheck()
        {
            if (!IsRoundDependend && RoundDependendStatModifiers.Count > UsesPerBattle)
            {
                Logger.LogWarning($"Skill \"{Name}\" has more round dependend StatModifiers than uses per battle!\n" +
                    $"The excess StatModifiers will not be used!");
            }
            if (StatModifiers != null)
            {
                Logger.LogWarning($"Skill \"{Name}\" has set StatModifiers! They will be discarded in favor of round dependend StatModifiers.");
                StatModifiers = null; // Setting StatModifiers to null here, so they can't be used if set before, to prevent unwanted behaviour
            }
        }
    }
}
