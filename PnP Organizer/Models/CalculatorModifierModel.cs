using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Calculators;
using PnP_Organizer.Core.Character.SkillSystem;
using PnP_Organizer.IO;
using System;
using System.Collections.Generic;

namespace PnP_Organizer.Models
{
    /// <summary>
    /// Data for CalculatorModifiers
    /// </summary>
    public partial class CalculatorModifierModel : ObservableObject
    {
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;
        [ObservableProperty]
        private bool _isActive = false;

        private ApplianceMode? _applianceTime;
        public ApplianceMode? ApplianceMode
        {
            get { return _applianceTime; }
            set { _applianceTime = value; }
        }

        private readonly Func<float, float>? _modifierFunction;

        public CalculatorModifierModel(string name, string description, ApplianceMode? applianceTime = null, Func<float, float>? modifierFunction = null) 
        {
            Name = name;
            Description = description;
            IsActive = false;
            ApplianceMode = applianceTime;
            _modifierFunction = modifierFunction;
        }

        public void ApplyModifier(ref float damageValue)
        {
            if(_modifierFunction != null)
                damageValue = _modifierFunction(damageValue);
        }
    }

    public class ConditionalCalculatorModifierModel : CalculatorModifierModel
    {
        public AttackMode AttackMode { get; set; }

        private Func<bool> _isUsablePrediate;

        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="applianceTime"></param>
        /// <param name="modifierFunction"></param>
        /// <param name="isUsablePredicate"></param>
        /// <param name="isRangedOnly"></param>
        /// <param name="isMeleeOnly"></param>
        public ConditionalCalculatorModifierModel(string name, string description, ApplianceMode applianceTime, Func<float, float> modifierFunction, Func<bool> isUsablePredicate, AttackMode attackMode) : base(name, description, applianceTime, modifierFunction)
        {
            _isUsablePrediate = isUsablePredicate;
            AttackMode = attackMode;
        }

        public bool IsUsable() => _isUsablePrediate();
    }

    /// <summary>
    /// Contains non-skill Modifiers which are either always available or
    /// depend on the amount of pearls the player has.
    /// </summary>
    public class StaticModifierModels
    {
        public static readonly List<CalculatorModifierModel> Modifiers = new();

        public static readonly CalculatorModifierModel OnGround = Add(new ConditionalCalculatorModifierModel(Properties.Resources.Skill_OnGround, Properties.Resources.Skill_OnGroundDescription, ApplianceMode.BaseValue, damage => damage += FileIO.LoadedCharacter.Pearls.Earth, () => FileIO.LoadedCharacter.Pearls.Earth > 0, AttackMode.Ranged | AttackMode.Melee));
        public static readonly CalculatorModifierModel Speeded = Add(new ConditionalCalculatorModifierModel(Properties.Resources.Skill_Speeded, Properties.Resources.Skill_SpeededDescription, ApplianceMode.BaseValue, damage => damage += FileIO.LoadedCharacter.Pearls.Air, () => FileIO.LoadedCharacter.Pearls.Air > 0, AttackMode.Ranged | AttackMode.Melee));
        public static readonly CalculatorModifierModel BurningBlade = Add(new ConditionalCalculatorModifierModel(Properties.Resources.Skill_BurningBlade, Properties.Resources.Skill_BurningBladeDescription, ApplianceMode.BaseValue, damage => damage += FileIO.LoadedCharacter.Pearls.Fire, () => FileIO.LoadedCharacter.Pearls.Fire > 0, AttackMode.Melee));
        public static readonly CalculatorModifierModel EarthBow = Add(new ConditionalCalculatorModifierModel(Properties.Resources.Skill_EarthBow, Properties.Resources.Skill_EarthBowDescription, ApplianceMode.BaseValue, damage => damage += FileIO.LoadedCharacter.Pearls.Earth, () => FileIO.LoadedCharacter.Pearls.Earth > 0, AttackMode.Ranged));
        public static readonly CalculatorModifierModel MetalActive = Add(new ConditionalCalculatorModifierModel(Properties.Resources.Skill_MetalActive, Properties.Resources.Skill_MetalActiveDescription, ApplianceMode.BaseValue, damage => damage += FileIO.LoadedCharacter.Pearls.Metal, () => FileIO.LoadedCharacter.Pearls.Metal > 0, AttackMode.Melee));
        public static readonly CalculatorModifierModel WeaponProfession = Add(new CalculatorModifierModel(Properties.Resources.Skill_WeaponProfession, Properties.Resources.Skill_WeaponProfessionDescription, ApplianceMode.BaseValue, damage => damage + 1));
        private static CalculatorModifierModel Add(CalculatorModifierModel modifierModel)
        {
            Modifiers.Add(modifierModel);
            return modifierModel;
        }
    }
}
