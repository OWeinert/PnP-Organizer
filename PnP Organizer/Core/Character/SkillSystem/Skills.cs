using PnP_Organizer.Core.BattleAssistant;
using PnP_Organizer.Core.Character.SkillSystem;
using PnP_Organizer.Core.Character.StatModifiers;
using PnP_Organizer.IO;
using PnP_Organizer.Properties;
using PnP_Organizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

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
        public readonly Skill Sneaking;
        public readonly Skill VirtuallyInvisible;
        public readonly Skill Intimidate;
        public readonly Skill Flirting;
        public readonly Skill NatureStudy;
        public readonly Skill Theft;
        public readonly Skill Lockpicking;
        public readonly Skill Counterfeiting;
        public readonly Skill KnowledgeOfPeople;
        public readonly Skill ActorByBirth;
        public readonly Skill Tracking;
        public readonly Skill PoisonKnowledge;
        public readonly Skill Gambling;
        public readonly Skill SkilledLier;
        public readonly Skill LieDetector;
        public readonly Skill SkilledSpeaker;
        public readonly Skill Climbing;
        public readonly Skill Teacher;
        public readonly Skill Plunge;
        public readonly Skill HardToKill;
        public readonly Skill Sympathic;
        public readonly Skill Alertness;
        public readonly Skill RescueIsNear;
        public readonly Skill Etiquette;
        public readonly Skill Trading;
        public readonly Skill Perseverence;
        public readonly Skill Encouragement;
        public readonly Skill EnergyBoost;
        public readonly Skill Momentum;
        public readonly Skill CommandTone;
        public readonly Skill Luck;
        public readonly Skill Healing;
        public readonly Skill LastBreath;
        public readonly Skill FutureMarket;
        public readonly Skill Avenger;

        public readonly Skill HP;
        public readonly Skill Profession;
        public readonly Skill Stamina2;
        public readonly Skill Stamina4;
        public readonly Skill Stats;
        public readonly Skill Energy3;
        public readonly Skill Energy6;
        public readonly Skill NextLevel;
        public readonly Skill NextElemental;
        public readonly Skill ElementalProfessionGreen;
        public readonly Skill ElementalProfessionYellow;
        public readonly Skill ElementalProfessionRed;
        public readonly Skill FourthElemental;
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
        public readonly Skill OneHandedCombat;
        public readonly Skill SecondHand;
        public readonly Skill Shield;
        public readonly Skill DefensiveFighting;
        public readonly Skill Nimble;
        public readonly Skill AggressiveCombat;
        public readonly Skill Fencing;
        public readonly Skill FullDamage;
        public readonly Skill ShieldBash;
        public readonly Skill Parade;
        public readonly Skill ThereAndAway;
        public readonly Skill RecklessAttack;
        public readonly Skill DuplexFerrum;
        public readonly Skill SomethingWithShield;
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
        public readonly Skill SurpriseAttack;
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

        // TODO Skills: implement and/or add every type of skill bonus
        private Skills()
        {
            SkillsList = new();

            #region Skill Definitions
            #region Character
            // Checkpoint 0
            Sneaking = CreateAndAddSkill(Resources.Skills_SkillSneaking, SkillCategory.Character, 2, Resources.Skills_SkillSneakingDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SneakHide, Dice.D4) });
            Intimidate = CreateAndAddSkill(Resources.Skills_SkillIntimidate, SkillCategory.Character, 2, Resources.Skills_SkillIntimidateDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Intimidate, Dice.D4) });
            Flirting = CreateAndAddSkill(Resources.Skills_SkillFlirting, SkillCategory.Character, 2, Resources.Skills_SkillFlirtingDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Performance, 0, true) }); // Dummy StatModifier to show the skill on the AttributeTestsPage
            NatureStudy = CreateAndAddSkill(Resources.Skills_SkillNatureStudy, SkillCategory.Character, 2, Resources.Skills_SkillNatureStudyDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Nature, Dice.D4) });
            VirtuallyInvisible = CreateAndAddSkill(Resources.Skills_SkillVirtuallyInvisible, SkillCategory.Character, 2,
                Resources.Skills_SkillVirtuallyInvisibleDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SneakHide, 1) }, 
                new string[] { Sneaking.Name });
            Theft = CreateAndAddSkill(Resources.Skills_SkillTheft, SkillCategory.Character, 2, 
                Resources.Skills_SkillTheftDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SleightOfHand, Dice.D4, 0, true) },
                new string[] { Sneaking.Name });
            Lockpicking = CreateAndAddSkill(Resources.Skills_SkillLockpicking, SkillCategory.Character, 1, 
                Resources.Skills_SkillLockpickingDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SleightOfHand, Dice.D4, 0, true) }, 
                new string[] { Sneaking.Name });
            Counterfeiting = CreateAndAddSkill(Resources.Skills_SkillCounterfeiting, SkillCategory.Character, 1, 
                Resources.Skills_SkillCounterfeitingDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SleightOfHand, Dice.D4, 0, true) }, 
                new string[] { Sneaking.Name });
            KnowledgeOfPeople = CreateAndAddSkill(Resources.Skills_SkillKnowledgeOfPeople, SkillCategory.Character, 2, 
                Resources.Skills_SkillKnowledgeOfPeopleDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Insight, 1) }, 
                new string[] { Intimidate.Name, Flirting.Name });
            ActorByBirth = CreateAndAddSkill(Resources.Skills_SkillActorByBirth, SkillCategory.Character, 3, 
                Resources.Skills_SkillActorByBirthDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Performance, Dice.D4, 2) },
                new string[] { KnowledgeOfPeople.Name });
            Tracking = CreateAndAddSkill(Resources.Skills_SkillTracking, SkillCategory.Character, 1, 
                Resources.Skills_SkillTrackingDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Nature, Dice.D4, 0, true) }, 
                new string[] { NatureStudy.Name });
            PoisonKnowledge = CreateAndAddSkill(Resources.Skills_SkillPoisonKnowledge, SkillCategory.Character, 2, 
                Resources.Skills_SkillPoisonKnowledgeDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Nature, Dice.D4, 0, true) },
                new string[] { NatureStudy.Name });
            Gambling = CreateAndAddSkill(Resources.Skills_SkillGambling, SkillCategory.Character, 1, 
                Resources.Skills_SkillGamblingDescr,
                new IStatModifier[] 
                { 
                    new AttributeTestStatModifier(Resources.AttributeTests_SleightOfHand, Dice.D4, 0, true),
                    new AttributeTestStatModifier(Resources.AttributeTests_Performance, Dice.D4, 0, true)
                }, 
                new string[] { Theft.Name, Lockpicking.Name, Counterfeiting.Name });
            SkilledLier = CreateAndAddSkill(Resources.Skills_SkillSkilledLier, SkillCategory.Character, 2, 
                Resources.Skills_SkillSkilledLierDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Bluff, 1) },
                new string[] { Theft.Name, Lockpicking.Name, Counterfeiting.Name });
            LieDetector = CreateAndAddSkill(Resources.Skills_SkillLieDetector, SkillCategory.Character, 2, 
                Resources.Skills_SkillLieDetectorDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Insight, Dice.D4, 0, true) }, 
                new string[] { SkilledLier.Name, KnowledgeOfPeople.Name });
            SkilledSpeaker = CreateAndAddSkill(Resources.Skills_SkillSkilledSpeaker, SkillCategory.Character, 3, 
                Resources.Skills_SkillSkilledSpeakerDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Persuade, Dice.D4, 1) }, 
                new string[] { KnowledgeOfPeople.Name });

            // Checkpoint 1
            Climbing = CreateAndAddSkill(Resources.Skills_SkillClimbing, SkillCategory.Character, 2, 
                Resources.Skills_SkillClimbingDescr, 
                new IStatModifier[] 
                {
                    new AttributeTestStatModifier(Resources.AttributeTests_Athletic, Dice.D4, 0, true),
                    new AttributeTestStatModifier(Resources.AttributeTests_Acrobatic, Dice.D4, 0, true)
                },
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Teacher = CreateAndAddSkill(Resources.Skills_SkillTeacher, SkillCategory.Character, 2, 
                Resources.Skills_SkillTeacherDescr, null,
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Plunge = CreateAndAddSkill(Resources.Skills_SkillPlunge, SkillCategory.Character, 2, 
                Resources.Skills_SkillPlungeDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 8) },
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name },
                activationType: ActivationType.Active, staminaCost: 3);
            HardToKill = CreateAndAddSkill(Resources.Skills_SkillHardToKill, SkillCategory.Character, 2, 
                Resources.Skills_SkillHardToKillDescr, null,
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Sympathic = CreateAndAddSkill(Resources.Skills_SkillSympathic, SkillCategory.Character, 2, 
                Resources.Skills_SkillSympathicDescr, null,
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Alertness = CreateAndAddSkill(Resources.Skills_SkillAlertness, SkillCategory.Character, 2, 
                Resources.Skills_SkillAlertnessDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Perceive, 1), new AttributeTestStatModifier(Resources.AttributeTests_Inspect, 1) },
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            RescueIsNear = CreateAndAddSkill(Resources.Skills_SkillRescueIsNear, SkillCategory.Character, 2, 
                Resources.Skills_SkillRescueIsNearDescr, null, new string[] { Plunge.Name }, activationType: ActivationType.Active);
            Etiquette = CreateAndAddSkill(Resources.Skills_SkillEtiquette, SkillCategory.Character, 2, 
                Resources.Skills_SkillEtiquetteDescr, null, new string[] { Sympathic.Name });
            Trading = CreateAndAddSkill(Resources.Skills_SkillTrading, SkillCategory.Character, 2, 
                Resources.Skills_SkillTradingDescr, new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Persuade, Dice.D4, 0, true) },
                new string[] { Sympathic.Name });

            // Checkpoint 2
            Perseverence = CreateAndAddSkill(Resources.Skills_SkillPerseverence, SkillCategory.Character, 2, 
                Resources.Skills_SkillPerseverenceDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            Encouragement = CreateAndAddSkill(Resources.Skills_SkillEncouragement, SkillCategory.Character, 2, 
                Resources.Skills_SkillEncouragementDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            EnergyBoost = CreateAndAddSkill(Resources.Skills_SkillEnergyBoost, SkillCategory.Character, 2, 
                Resources.Skills_SkillEnergyBoostDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            Momentum = CreateAndAddSkill(Resources.Skills_SkillMomentum, SkillCategory.Character, 2, 
                Resources.Skills_SkillMomentumDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name },
                activationType: ActivationType.Active);
            CommandTone = CreateAndAddSkill(Resources.Skills_SkillCommandTone, SkillCategory.Character, 2, 
                Resources.Skills_SkillCommandToneDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            Luck = CreateAndAddSkill(Resources.Skills_SkillLuck, SkillCategory.Character, 2, 
                Resources.Skills_SkillLuckDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name },
                activationType: ActivationType.Active);
            Healing = CreateAndAddSkill(Resources.Skills_SkillHealing, SkillCategory.Character, 2, 
                Resources.Skills_SkillHealingDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            LastBreath = CreateAndAddSkill(Resources.Skills_SkillLastBreath, SkillCategory.Character, 2, 
                Resources.Skills_SkillLastBreathDescr, null, new string[] { Perseverence.Name }, activationType: ActivationType.Active, usesPerBattle: 1);
            FutureMarket = CreateAndAddSkill(Resources.Skills_SkillFutureMarket, SkillCategory.Character, 2,
                Resources.Skills_SkillFutureMarketDescr, null, new string[] { Perseverence.Name }, activationType: ActivationType.Active, usesPerBattle: 1);
            Avenger = CreateAndAddSkill(Resources.Skills_SkillAvenger, SkillCategory.Character, 2,
                Resources.Skills_SkillAvengerDescr, null, new string[] { Momentum.Name }, activationType: ActivationType.Active);

            // Repeatable Skills
            HP = AddSkill(new Skill(Resources.Skills_SkillHP, SkillCategory.Character, 3, Resources.Skills_SkillHPDescr, 
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxHealthModifierBonus), 6) }).SetRepeatable());
            Profession = AddSkill(new Skill(Resources.Skills_SkillProfession, SkillCategory.Character, 3, Resources.Skills_SkillProfessionDescr, null).SetRepeatable());
            Stamina2 = AddSkill(new Skill(Resources.Skills_SkillStamina2, SkillCategory.Character, 3, Resources.Skills_SkillStamina2Descr,
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxStaminaModifierBonus), 2) }).SetRepeatable());
            Stamina4 = AddSkill(Stamina4 = new Skill(Resources.Skills_SkillStamina4, SkillCategory.Character, 5, Resources.Skills_SkillStamina4Descr,
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxStaminaModifierBonus), 4) }).SetRepeatable());
            Stats = (new Skill(Resources.Skills_SkillStats, SkillCategory.Character, 5, Resources.Skills_SkillStatsDescr, null).SetRepeatable());
            Energy3 = (new Skill(Resources.Skills_SkillEnergy3, SkillCategory.Character, 3, Resources.Skills_SkillEnergy3Descr,
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxEnergyModifierBonus), 3) }).SetRepeatable());
            Energy6 = AddSkill(new Skill(Resources.Skills_SkillEnergy6, SkillCategory.Character, 5, Resources.Skills_SkillEnergy6Descr,
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxEnergyModifierBonus), 6) }).SetRepeatable());
            NextLevel = AddSkill(new Skill(Resources.Skills_SkillNextLevel, SkillCategory.Character, 4, Resources.Skills_SkillNextLevelDescr, null).SetRepeatable());
            NextElemental = AddSkill(new Skill(Resources.Skills_SkillNextElemental, SkillCategory.Character, 5, Resources.Skills_SkillNextElementalDescr, null).SetRepeatable());
            ElementalProfessionGreen = AddSkill(new Skill(Resources.Skills_SkillElementalProfessionGreen, SkillCategory.Character, 1, Resources.Skills_SkillElementalProfessionGreenDescr, null).SetRepeatable());
            ElementalProfessionYellow = AddSkill(new Skill(Resources.Skills_SkillElementalProfessionYellow, SkillCategory.Character, 3, Resources.Skills_SkillElementalProfessionYellowDescr, null).SetRepeatable());
            ElementalProfessionRed = AddSkill(new Skill(Resources.Skills_SkillElementalProfessionRed, SkillCategory.Character, 5, Resources.Skills_SkillElementalProfessionRedDescr).SetRepeatable());
            FourthElemental = AddSkill(new Skill(Resources.Skills_SkillFourthElemental, SkillCategory.Character, 6, Resources.Skills_SkillFourthElementalDescr, null).SetRepeatable());

            #endregion Character

            #region Melee
            // Checkpoint 0
            LightBlow = CreateAndAddSkill(Resources.Skills_SkillLightBlow, SkillCategory.Melee, 1, Resources.Skills_SkillLightBlowDescr, 
                new IStatModifier[] 
                {
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2.0),
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 0.5, CalculatorBonusType.Multiplicative)
                }, activationType: ActivationType.Active, staminaCost: 1);
            Smithing = CreateAndAddSkill(Resources.Skills_SkillSmithing, SkillCategory.Melee, 1, Resources.Skills_SkillSmithingDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Performance, 0, true) }); // Dummy StatModifier to show the skill on the AttributeTestsPage
            RunOver = CreateAndAddSkill(Resources.Skills_SkillRunOver, SkillCategory.Melee, 2, Resources.Skills_SkillRunOverDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D4, 2) },
                activationType: ActivationType.Active, staminaCost: 3);
            AimedAttackMelee = CreateAndAddSkill(Resources.Skills_SkillAimedAttackMelee, SkillCategory.Melee, 2,
                Resources.Skills_SkillAimedAttackMeleeDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -5) },
                new string[] { LightBlow.Name }, activationType: ActivationType.Active, staminaCost: 2);
            WeaponsAndArmor = CreateAndAddSkill(Resources.Skills_SkillWeaponsAndArmor, SkillCategory.Melee, 2, 
                Resources.Skills_SkillWeaponsAndArmorDescr, null, new string[] { Smithing.Name });
            Kick = CreateAndAddSkill(Resources.Skills_SkillKick, SkillCategory.Melee, 2, 
                Resources.Skills_SkillKickDescr, null, new string[] { RunOver.Name }, activationType: ActivationType.Active, staminaCost: 1);
            Taunt = CreateAndAddSkill(Resources.Skills_SkillTaunt, SkillCategory.Melee, 2, 
                Resources.Skills_SkillTauntDescr, null, new string[] { RunOver.Name });
            ArmorBreaker = CreateAndAddSkill(Resources.Skills_SkillArmorBreaker, SkillCategory.Melee, 2, 
                Resources.Skills_SkillArmorBreakerDescr, null, new string[] { AimedAttackMelee.Name }, activationType: ActivationType.Active,
                staminaCost: 5);
            Assassinate = AddSkill(new Skill(Resources.Skills_SkillAssassinate, SkillCategory.Melee, 2, 
                Resources.Skills_SkillAssassinateDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 3, CalculatorBonusType.Multiplicative)}, 
                new string[] { AimedAttackMelee.Name }, activationType: ActivationType.Active, staminaCost: 1)
                .AddForcedDependency(Sneaking.Name));
            JumpAttack = CreateAndAddSkill(Resources.Skills_SkillJumpAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillJumpAttackDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2) },
                new string[] { Kick.Name }, activationType: ActivationType.Active, staminaCost: 1);

            // Checkpoint 1
            OneHandedCombat = CreateAndAddSkill(Resources.Skills_SkillOneHandedFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillOneHandedFightingDescr, 
                new IStatModifier[]
                {
                    new OverviewStatModifier(nameof(OverviewViewModel.InitiativeModifierBonus), 1),
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1),
                },
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            SecondHand = CreateAndAddSkill(Resources.Skills_SkillSecondHand, SkillCategory.Melee, 2, 
                Resources.Skills_SkillSecondHandDescr, null,
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            Shield = CreateAndAddSkill(Resources.Skills_SkillShield, SkillCategory.Melee, 2, 
                Resources.Skills_SkillShieldDescr, null,
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            DefensiveFighting = CreateAndAddSkill(Resources.Skills_SkillDefensiveFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDefensiveFightingDescr, 
                new IStatModifier[] 
                { 
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 2),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -2),
                },
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name },
                activationType: ActivationType.Active);
            Nimble = CreateAndAddSkill(Resources.Skills_SkillNimble, SkillCategory.Melee, 2, 
                Resources.Skills_SkillNimbleDescr, new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.InitiativeModifierBonus), 2) },
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            AggressiveCombat = CreateAndAddSkill(Resources.Skills_SkillAgressiveFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillAgressiveFightingDescr,
                new IStatModifier[]
                {
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, -2),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2),
                },
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name },
                activationType: ActivationType.Active);
            Fencing = CreateAndAddSkill(Resources.Skills_SkillFencing, SkillCategory.Melee, 2, 
                Resources.Skills_SkillFencingDescr, null, new string[] { OneHandedCombat.Name });
            FullDamage = CreateAndAddSkill(Resources.Skills_SkillFullDamage, SkillCategory.Melee, 2, 
                Resources.Skills_SkillFullDamageDescr, null, new string[] { SecondHand.Name });
            ShieldBash = CreateAndAddSkill(Resources.Skills_SkillShieldBash, SkillCategory.Melee, 2, 
                Resources.Skills_SkillShieldBashDescr, null, new string[] { Shield.Name }, activationType: ActivationType.Active, staminaCost: 2);
            Parade = CreateAndAddSkill(Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 2, 
                Resources.Skills_SkillParadeDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, Dice.D4) },
                new string[] { DefensiveFighting.Name });
            ThereAndAway = CreateAndAddSkill(Resources.Skills_SkillThereAndAway, SkillCategory.Melee, 2,
                Resources.Skills_SkillThereAndAwayDescr, null, new string[] { Nimble.Name });
            RecklessAttack = CreateAndAddSkill(Resources.Skills_SkillRecklessAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillRecklessAttackDescr, null, new string[] { AggressiveCombat.Name }, activationType: ActivationType.Active,
                staminaCost: 3);
            DuplexFerrum = CreateAndAddSkill(Resources.Skills_SkillDuplexFerrum, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDuplexFerrumDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, Dice.D4) },
                new string[] { FullDamage.Name });
            SomethingWithShield = CreateAndAddSkill(Resources.Skills_SkillSomethingWithShield, SkillCategory.Melee, 2, 
                Resources.Skills_SkillSomethingWithShieldDescr, null, new string[] { ShieldBash.Name }, activationType: ActivationType.Active,
                staminaCost: 3);
            QuickParade = CreateAndAddSkill(Resources.Skills_SkillQuickParade, SkillCategory.Melee, 2, 
                Resources.Skills_SkillQuickParadeDescr, null, new string[] { Parade.Name }, activationType: ActivationType.Active,
                staminaCost: 1);
            SkillfulRetreat = CreateAndAddSkill(Resources.Skills_SkillSkillfulRetreat, SkillCategory.Melee, 2, 
                Resources.Skills_SkillSkillfulRetreatDescr, null, new string[] { DefensiveFighting.Name, ThereAndAway.Name }, activationType: ActivationType.Active,
                staminaCost: 1);
            Feint = CreateAndAddSkill(Resources.Skills_SkillFeint, SkillCategory.Melee, 2, 
                Resources.Skills_SkillFeintDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 4) },
                new string[] { Nimble.Name });
            RoundHouseAttack = CreateAndAddSkill(Resources.Skills_SkillRoundHouseAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillRoundHouseAttackDescr, null, new string[] { RecklessAttack.Name }, activationType: ActivationType.Active, staminaCost: 3);
            HeavyParade = CreateAndAddSkill(Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 2, 
                Resources.Skills_SkillHeavyParadeDescr, null, new string[] { QuickParade.Name }, activationType: ActivationType.Active, staminaCost: 2);
            PerfectBlock = CreateAndAddSkill(Resources.Skills_SkillPerfectBlock, SkillCategory.Melee, 2, 
                Resources.Skills_SkillPerfectBlockDescr, null, new string[] { QuickParade.Name }, activationType: ActivationType.Active, staminaCost: 5);
            DevastatingAttack = CreateAndAddSkill(Resources.Skills_SkillDevastatingAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDevastatingAttackDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 2, CalculatorBonusType.Multiplicative) },
                new string[] { RoundHouseAttack.Name }, activationType: ActivationType.Active, staminaCost: 3);

            // Checkpoint 2
            Cavallery = CreateAndAddSkill(Resources.Skills_SkillCavallery, SkillCategory.Melee, 2, 
                Resources.Skills_SkillCavalleryDescr, null,
                new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            DefensiveStance = CreateAndAddSkill(Resources.Skills_SkillDefensiveStance, SkillCategory.Melee, 2,
                Resources.Skills_SkillDefensiveStanceDescr, 
                new IStatModifier[] 
                {
                    new CalculatorStatModifier(CalculatorValueType.Armor, ApplianceMode.EndValue, 2, CalculatorBonusType.Multiplicative),
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, Dice.D6, 2),
                },
                new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name },
                activationType: ActivationType.Active, staminaCost: 1);
            ArmorUp = CreateAndAddSkill(Resources.Skills_SkillArmorUp, SkillCategory.Melee, 2,
                Resources.Skills_SkillArmorUpDescr, null, new string[] { DefensiveStance.Name }, ActivationType.Active, staminaCost: 1);
            AccurateMelee = CreateAndAddSkill(Resources.Skills_SkillAccurateMelee, SkillCategory.Melee, 2, 
                Resources.Skills_SkillAccurateMeleeDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 2) },
                new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            RecognizeStyle = CreateAndAddSkill(Resources.Skills_SkillRecognizeStyle, SkillCategory.Melee, 2,
                Resources.Skills_SkillRecognizeStyleDescr, 
                new IStatModifier[] 
                {
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 1)
                },
                new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            CripplingBlow = CreateAndAddSkill(Resources.Skills_SkillCripplingBlow, SkillCategory.Melee, 2,
                Resources.Skills_SkillCripplingBlowDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -1) }, 
                new string[] { RecognizeStyle.Name }, activationType: ActivationType.Active, staminaCost: 2);
            HijackerMelee = CreateAndAddSkill(Resources.Skills_SkillHijackerMelee, SkillCategory.Melee, 2, 
                Resources.Skills_SkillHijackerMeleeDescr, null, new string[] { Cavallery.Name }, activationType: ActivationType.Active,
                staminaCost: 3);
            Armor = CreateAndAddSkill(Resources.Skills_SkillArmor, SkillCategory.Melee, 2, 
                Resources.Skills_SkillArmorDescr, null, new string[] { DefensiveStance.Name });
            Combo = CreateAndAddSkill(Resources.Skills_SkillCombo, SkillCategory.Melee, 2,
                Resources.Skills_SkillComboDescr, null, new string[] { AccurateMelee.Name });
            PerfectBlow = CreateAndAddSkill(Resources.Skills_SkillPerfectBlow, SkillCategory.Melee, 2, 
                Resources.Skills_SkillPerfectBlowDescr, null, new string[] { AccurateMelee.Name }, activationType: ActivationType.Active,
                staminaCost: 5).SetOnlySoloUsable();
            EveryBlowAHit = CreateAndAddSkill(Resources.Skills_SkillEveryBlowAHit, SkillCategory.Melee, 2, 
                Resources.Skills_SkillEveryBlowAHitDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2) },
                new string[] { AccurateMelee.Name });
            TakeAHit = CreateAndAddSkill(Resources.Skills_SkillTakeAHit, SkillCategory.Melee, 2,
                Resources.Skills_SkillTakeAHitDescr, null, new string[] { RecognizeStyle.Name }, activationType: ActivationType.Active);

            // Checkpoint 3
            AttackOfOpportunity = CreateAndAddSkill(Resources.Skills_SkillAttackOfOpportunity, SkillCategory.Melee, 2, 
                Resources.Skills_SkillAttackOfOpportunityDescr, null,
                new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name },
                activationType: ActivationType.Active, staminaCost: 1);
            DisarmMelee = CreateAndAddSkill(Resources.Skills_SkillDisarmMelee, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDisarmMeleeDescr, null,
                new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name },
                activationType: ActivationType.Active, staminaCost: 1);
            KillingSpree = CreateAndAddSkill(Resources.Skills_SkillKillingSpree, SkillCategory.Melee, 2, 
                Resources.Skills_SkillKillingSpreeDescr, null,
                new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            HeavyFighting = CreateAndAddSkill(Resources.Skills_SkillHeavyFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillHeavyFightingDescr, null,
                new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            Riposte = CreateAndAddSkill(Resources.Skills_SkillRiposte, SkillCategory.Melee, 2, 
                Resources.Skills_SkillRiposteDescr, null,
                new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            LoneWarrior = CreateAndAddSkill(Resources.Skills_SkillLoneWarrior, SkillCategory.Melee, 2, 
                Resources.Skills_SkillLoneWarriorDescr, 
                new IStatModifier[] 
                { 
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Armor, ApplianceMode.BaseValue, 1),
                },
                new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name },
                activationType: ActivationType.Active);
            ChainAttack = CreateAndAddSkill(Resources.Skills_SkillChainAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillChainAttackDescr, null, new string[] { DisarmMelee.Name },
                activationType: ActivationType.Active, staminaCost: 1);
            ShieldBreaker = CreateAndAddSkill(Resources.Skills_SkillShieldBreaker, SkillCategory.Melee, 2, 
                Resources.Skills_SkillShieldBreakerDescr, null, new string[] { HeavyFighting.Name },
                activationType: ActivationType.Active);
            BladeFan = CreateAndAddSkill(Resources.Skills_SkillBladeFan, SkillCategory.Melee, 2, 
                Resources.Skills_SkillBladeFanDescr, null, new string[] { Riposte.Name },
                activationType: ActivationType.Active, staminaCost: 1, energyCost: 1);

            #endregion Melee

            #region Ranged
            // Checkpoint 0
            LightShot = CreateAndAddSkill(Resources.Skills_SkillLightShot, SkillCategory.Ranged, 2, Resources.Skills_SkillLightShotDescr,
                new IStatModifier[]
                {
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2.0),
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 0.5, CalculatorBonusType.Multiplicative)
                }, activationType: ActivationType.Active, staminaCost: 1);
            Quickdraw = CreateAndAddSkill(Resources.Skills_SkillQuickdraw, SkillCategory.Ranged, 2, Resources.Skills_SkillQuickdrawDescr, null,
                activationType: ActivationType.Active, staminaCost:1);
            SkilledWithThrowingWeapons = CreateAndAddSkill(Resources.Skills_SkillSkilledWithThrowingWeapons, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillSkilledWithThrowingWeaponsDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1) });
            BetterThanThrowing = CreateAndAddSkill(Resources.Skills_SkillBetterThanThrowing, SkillCategory.Ranged, 2, Resources.Skills_SkillBetterThanThrowingDescr);
            CalmAiming = CreateAndAddSkill(Resources.Skills_SkillCalmAiming, SkillCategory.Ranged, 2,
                Resources.Skills_SkillCalmAimingDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2) }, 
                new string[]{ LightShot.Name }, activationType: ActivationType.Active);
            DisarmRanged = CreateAndAddSkill(Resources.Skills_SkillDisarmRanged, SkillCategory.Ranged, 2,
                Resources.Skills_SkillDisarmRangedDescr, null, new string[]{ Quickdraw.Name },
                activationType: ActivationType.Active, staminaCost: 2);
            DualThrow = CreateAndAddSkill(Resources.Skills_SkillDualThrow, SkillCategory.Ranged, 2,
                Resources.Skills_SkillDualThrowDescr, null, new string[]{ SkilledWithThrowingWeapons.Name });
            SlingshotMarksman = CreateAndAddSkill(Resources.Skills_SkillSlingshotMarksman, SkillCategory.Ranged, 2,
                Resources.Skills_SkillSlingshotMarksmanDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2) },
                new string[]{ BetterThanThrowing.Name });

            // Checkpoint 1
            ShootFromTheSaddle = CreateAndAddSkill(Resources.Skills_SkillShootFromTheSaddle, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillShootFromTheSaddleDescr, null, new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            BuildArrows = CreateAndAddSkill(Resources.Skills_SkillBuildArrows, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillBuildArrowsDescr, null, new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            PreciseThrow = CreateAndAddSkill(Resources.Skills_SkillPreciseThrow, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillPreciseThrowDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -4) },
                new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name }, 
                activationType: ActivationType.Active, staminaCost: 1);
            AimedAttackRanged = CreateAndAddSkill(Resources.Skills_SkillAimedAttackRanged, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillAimedAttackRangedDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -5) },
                new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name },
                activationType: ActivationType.Active, staminaCost: 2);
            AccurateRanged = CreateAndAddSkill(Resources.Skills_SkillAccurateRanged, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillAccurateRangedDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1) },
                new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            HijackerRanged = CreateAndAddSkill(Resources.Skills_SkillHijackerRanged, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillHijackerRangedDescr, null, new string[]{ ShootFromTheSaddle.Name });
            BowMaking = CreateAndAddSkill(Resources.Skills_SkillBowMaking, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillBowMakingDescr, null, new string[]{ BuildArrows.Name });
            StrongThrow = CreateAndAddSkill(Resources.Skills_SkillStrongThrow, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillStrongThrowDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, Dice.D6) },
                new string[]{ PreciseThrow.Name }, activationType: ActivationType.Active);
            Headshot = CreateAndAddSkill(Resources.Skills_SkillHeadshot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillHeadshotDescr, null, new string[]{ AimedAttackRanged.Name });
            BackLine = CreateAndAddSkill(Resources.Skills_SkillBackLine, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillBackLineDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1) },
                new string[]{ AccurateRanged.Name });
            RoutinedWithThrowingWeapons = CreateAndAddSkill(Resources.Skills_SkillRoutinedWithThrowingWeapons, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillRoutinedWithThrowingWeaponsDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2) },
                new string[]{ StrongThrow.Name });
            ProfessionalSlingshotMarksman = CreateAndAddSkill(Resources.Skills_SkillProfessionalSlingshotMarksman, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillProfessionalSlingshotMarksmanDescr, 
                new IStatModifier[]
                { 
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2),
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, Dice.D6, 2)
                }, 
                new string[]{ AccurateRanged.Name });
            PerfectShot = CreateAndAddSkill(Resources.Skills_SkillPerfectShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillPerfectShotDescr, null, new string[]{ BackLine.Name }, activationType: ActivationType.Active,
                staminaCost: 5);

            // Checkpoint 2
            SurpriseAttack = CreateAndAddSkill(Resources.Skills_SkillSurpriseAttack, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillSurpriseAttackDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, Dice.D4) },
                new string[]{ HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name }, 
                activationType: ActivationType.Active);
            NailDown = CreateAndAddSkill(Resources.Skills_SkillNailDown, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillNailDownDescr, null,
                new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name },
                activationType: ActivationType.Active);
            CurvedShot = CreateAndAddSkill(Resources.Skills_SkillCurvedShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillCurvedShotDescr, null,
                new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name },
                activationType: ActivationType.Active);
            QuickAim = CreateAndAddSkill(Resources.Skills_SkillQuickAim, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillQuickAimDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -3) },
                new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name },
                activationType: ActivationType.Active);
            StrongArrows = CreateAndAddSkill(Resources.Skills_SkillStrongArrows, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillStrongArrowsDescr, null,
                new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            PiercingArrow = CreateAndAddSkill(Resources.Skills_SkillPiercingArrow, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillPiercingArrowDescr, null, new string[]{ NailDown.Name }, activationType: ActivationType.Active);
            LuckyShot = CreateAndAddSkill(Resources.Skills_SkillLuckyShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillLuckyShotDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -6) },
                new string[]{ CurvedShot.Name }, activationType: ActivationType.Active);
            DoubleShot = CreateAndAddSkill(Resources.Skills_SkillDoubleShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillDoubleShotDescr, null, new string[]{ QuickAim.Name }, activationType: ActivationType.Active, usesPerBattle: 1);
            MasterfulArcher = CreateAndAddSkill(Resources.Skills_SkillMasterfulArcher, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillMasterfulArcherDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2) },
                new string[]{ StrongArrows.Name });
            Magazine = CreateAndAddSkill(Resources.Skills_SkillMagazine, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillMagazineDescr, null, new string[]{ DoubleShot.Name }, activationType: ActivationType.Active);

            // Checkpoint 3
            Oneshot = CreateAndAddSkill(Resources.Skills_SkillOneshot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillOneshotDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 2, CalculatorBonusType.Multiplicative) },
                new string[]{ SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name },
                activationType: ActivationType.Active);
            LastShot = CreateAndAddSkill(Resources.Skills_SkillLastShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillLastShotDescr, 
                new IStatModifier[] 
                { 
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6),
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, FileIO.LoadedCharacter.Pearls.Wood * 2.5)
                },
                new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name },
                activationType: ActivationType.Active);
            HuntersMark = CreateAndAddSkill(Resources.Skills_SkillHuntersMark, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillHuntersMarkDescr, null,
                new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name },
                activationType: ActivationType.Active);
            Readiness = CreateAndAddSkill(Resources.Skills_SkillReadiness, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillReadinessDescr, null,
                new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name },
                activationType: ActivationType.Active);
            MasterOfThrowingWeapons = CreateAndAddSkill(Resources.Skills_SkillMasterOfThrowingWeapons, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillMasterOfThrowingWeaponsDescr, 
                new IStatModifier[] 
                { 
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2)
                },
                new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            Trueshot = CreateAndAddSkill(Resources.Skills_SkillTrueshot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillTrueshotDescr, null, new string[]{ Oneshot.Name },
                activationType: ActivationType.Active);
            Return = CreateAndAddSkill(Resources.Skills_SkillReturn, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillReturnDescr, null, new string[]{ MasterOfThrowingWeapons.Name },
                activationType: ActivationType.Active);

            #endregion Ranged
            #endregion Skill Definitions
        }

        public int GetSkillIndexFromName(string name)
        {
            var l = 0;
            var r = SkillsList.Count - 1;
            while(l <= r)
            {
                if (SkillsList[l].Name == name)
                    return l;
                if (SkillsList[r].Name == name)
                    return r;
                l++;
                r--;
            }
            return -1;
        }

        public Skill? GetSkillFromStatModifier(IStatModifier statModifier) => SkillsList.FirstOrDefault(skill => skill.StatModifiers!.Contains(statModifier));

        private Skill CreateAndAddSkill(string name, SkillCategory skillCategory, int maxSkillPoints, 
            string description, IStatModifier[]? skillModifiers = null, string[]? skillDependencies = null,
            ActivationType activationType = ActivationType.Passive, int energyCost = 0, int staminaCost = 0, int usesPerBattle = -1) 
        {
            var skill = new Skill(name, skillCategory, maxSkillPoints, description, skillModifiers, skillDependencies,
                activationType, energyCost, staminaCost, usesPerBattle);
            return AddSkill(skill);
        }

        private Skill AddSkill(Skill skill)
        {
            SkillsList.Add(skill);
            return skill;
        }
    }
}