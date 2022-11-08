using Microsoft.VisualBasic;
using PnP_Organizer.IO;
using PnP_Organizer.Properties;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Xml.Linq;

namespace PnP_Organizer.Core.Character
{
    public class Skills
    {
        /// <summary>
        /// Contains every skill
        /// </summary>
        public ObservableCollection<Skill> SkillsList { get; }

        public static Skills Instance { get => _instance.Value; }
        private static readonly Lazy<Skills> _instance = new(() => new Skills());

        #region Skill Declarations

        #region Character

        #endregion Character

        #region Melee
        public readonly Skill LightBlow;
        public readonly Skill Smithing;
        public readonly Skill RunOver;
        public readonly Skill AimedAttackMelee;
        public readonly Skill WeaponsAndArmor;
        public readonly Skill Kick;
        public readonly Skill Taunt;
        public readonly Skill ArmorBreaker;
        public readonly Skill Assassinate;
        public readonly Skill JumpAttack;
        public readonly Skill OneHandedFighting;
        public readonly Skill SecondHand;
        public readonly Skill Shield;
        public readonly Skill DefensiveFighting;
        public readonly Skill Nimble;
        public readonly Skill AggressiveFighting;
        public readonly Skill Fencing;
        public readonly Skill FullDamage;
        public readonly Skill ShieldBash;
        public readonly Skill Parade;
        public readonly Skill ThereAndAway;
        public readonly Skill RecklessAttack;
        public readonly Skill DuplexFerrum;
        public readonly Skill SomethingWithShield; // TODO Skill: SomethingWithShield needs a real name
        public readonly Skill QuickParade;
        public readonly Skill SkillfulRetreat;
        public readonly Skill Feint;
        public readonly Skill RoundHouseAttack;
        public readonly Skill HeavyParade;
        public readonly Skill PerfectBlock;
        public readonly Skill DevastatingAttack;
        public readonly Skill Cavallery;
        public readonly Skill DefensiveStance;
        public readonly Skill ArmorUp;
        public readonly Skill AccurateMelee;
        public readonly Skill RecognizeStyle;
        public readonly Skill CripplingBlow;
        public readonly Skill HijackerMelee;
        public readonly Skill Armor;
        public readonly Skill Combo;
        public readonly Skill PerfectBlow;
        public readonly Skill EveryBlowAHit;
        public readonly Skill TakeAHit;
        public readonly Skill AttackOfOpportunity;
        public readonly Skill DisarmMelee;
        public readonly Skill KillingSpree;
        public readonly Skill HeavyFighting;
        public readonly Skill Riposte;
        public readonly Skill LoneWarrior;
        public readonly Skill ChainAttack;
        public readonly Skill ShieldBreaker;
        public readonly Skill BladeFan;
        #endregion Melee

        #region Ranged
        public readonly Skill LightShot;
        public readonly Skill Quickdraw;
        public readonly Skill SkilledWithThrowingWeapons;
        public readonly Skill BetterThanThrowing;
        public readonly Skill CalmAiming;
        public readonly Skill DisarmRanged;
        public readonly Skill DualThrow;
        public readonly Skill SlingshotMarksman;
        public readonly Skill ShootFromTheSaddle;
        public readonly Skill BuildArrows;
        public readonly Skill PreciseThrow;
        public readonly Skill AimedAttackRanged;
        public readonly Skill AccurateRanged;
        public readonly Skill HijackerRanged;
        public readonly Skill BowMaking;
        public readonly Skill StrongThrow;
        public readonly Skill Headshot;
        public readonly Skill BackLine;
        public readonly Skill RoutinedWithThrowingWeapons;
        public readonly Skill ProfessionalSlingshotMarksman;
        public readonly Skill PerfectShot;
        public readonly Skill SupriseAttack;
        public readonly Skill NailDown;
        public readonly Skill CurvedShot;
        public readonly Skill QuickAim;
        public readonly Skill StrongArrows;
        public readonly Skill PiercingArrow;
        public readonly Skill LuckyShot;
        public readonly Skill DoubleShot;
        public readonly Skill MasterfulArcher;
        public readonly Skill Magazine;
        public readonly Skill Oneshot;
        public readonly Skill LastShot;
        public readonly Skill HuntersMark;
        public readonly Skill Readiness;
        public readonly Skill MasterOfThrowingWeapons;
        public readonly Skill Trueshot;
        public readonly Skill Return;
        #endregion Ranged

        #endregion Skill Declarations

        // TODO Skills: Implement all Skills
        private Skills()
        {
            SkillsList = new();

            #region Skill Definitions
            #region Melee
            LightBlow = AddSkill(
                CreateSkill(0, Resources.Skills_SkillLightBlow, SkillCategory.Melee, 1, SkillUsableType.Maxed, "")
                .AddDamageModifier(damage => damage / 2, ApplianceMode.EndValue));
            Smithing = AddSkill(CreateSkill(1, Resources.Skills_SkillSmithing, SkillCategory.Melee, 1, SkillUsableType.Maxed, ""));
            RunOver = AddSkill(CreateSkill(2, Resources.Skills_SkillRunOver, SkillCategory.Melee, 2, SkillUsableType.Maxed, ""));
            AimedAttackMelee = AddSkill(
                CreateSkill(3, Resources.Skills_SkillAimedAttackMelee, SkillCategory.Melee, 2, SkillUsableType.Maxed, "")
                .AddSkillDependencies(new string[] { LightBlow.Name }, () => true));
            #endregion Melee

            #region Ranged

            #endregion Ranged
            #endregion Skill Definitions

        }

        private static Skill CreateSkill(int id, string name, SkillCategory skillCategory, int maxSkillPoints, SkillUsableType skillActivationType, string description = "") 
            => new(id, name, skillCategory, maxSkillPoints, skillActivationType, description);

        private Skill AddSkill(Skill skill)
        {
            SkillsList.Add(skill);
            return skill;
        }

        public int GetSkillIndexFromName(string name)
        {
            int l = 0;
            int r = SkillsList.Count - 1;
            while (l <= r)
            {
                if (SkillsList[l].Name == name || SkillsList[r].Name == name)
                    return SkillsList[l].ID;
                else
                {
                    l++;
                    r--;
                }
            }
            return -1;
        }
    }
}
