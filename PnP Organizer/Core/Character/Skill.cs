using System;

namespace PnP_Organizer.Core.Character
{
    public struct Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillCategory SkillCategory { get; set; }

        /// <summary>
        /// Specifies if the skill modifies the base or the end damage.
        /// </summary>
        public ApplianceMode? ApplianceMode { get; private set; }
        public int SkillPoints { get; set; }
        public int MaxSkillPoints { get; set; }

        /// <summary>
        /// Delegate function which specifies how the player's damage is modified.
        /// Func<float,float> parameter: float damage
        ///                   returns: float modifiedDamage
        /// </summary>
        public Func<float, float>? DamageModifer { get; private set; }

        /// <summary>
        /// Names of skills which need to be skilled in order to unlock this skill.
        /// </summary>
        // TODO Direct Skill references would be better, but maybe won't work with the SkillModel
        public string[] DependendSkillNames { get; private set; }

        private  Func<bool>? _skillableDependencyPredicate;

        public Skill(string name, SkillCategory skillCategory, int maxSkillPoints, string description = "")
        {
            Name = name;
            SkillCategory = skillCategory;
            Description = description;
            MaxSkillPoints = maxSkillPoints;

            ApplianceMode = null;
            DamageModifer = null;
            SkillPoints = 0;
            DependendSkillNames = Array.Empty<string>();

            _skillableDependencyPredicate = null;
        }

        /// <summary>
        /// Checks if the skill is active, i.e. the max skill points are reached
        /// </summary>
        /// <returns></returns>
        public bool IsActive() => SkillPoints == MaxSkillPoints;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsSkillable() => _skillableDependencyPredicate == null || _skillableDependencyPredicate();

        public Skill AddSkillDependencies(string[] dependendSkillNames, Func<bool>? skillableDependencyPredicate = null)
        {
            DependendSkillNames = dependendSkillNames;
            _skillableDependencyPredicate = skillableDependencyPredicate;
            return this;
        }

        public Skill AddDamageModifier(Func<float, float> damageModifier, ApplianceMode applianceMode)
        {
            DamageModifer = damageModifier;
            ApplianceMode = applianceMode;
            return this;
        }
    }
}
