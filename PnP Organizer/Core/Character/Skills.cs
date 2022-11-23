using PnP_Organizer.Core.Calculators;
using PnP_Organizer.Core.Character.StatModifiers;
using PnP_Organizer.Properties;
using PnP_Organizer.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;

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
                new StatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SneakHide, Dice.D4) });
            Intimidate = CreateAndAddSkill(Resources.Skills_SkillIntimidate, SkillCategory.Character, 2, Resources.Skills_SkillIntimidateDescr, 
                new StatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Intimidate, Dice.D4) });
            Flirting = CreateAndAddSkill(Resources.Skills_SkillFlirting, SkillCategory.Character, 2, Resources.Skills_SkillFlirtingDescr, null);
            NatureStudy = CreateAndAddSkill(Resources.Skills_SkillNatureStudy, SkillCategory.Character, 2, Resources.Skills_SkillNatureStudyDescr,
                new StatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Nature, Dice.D4) });
            VirtuallyInvisible = CreateAndAddSkill(Resources.Skills_SkillVirtuallyInvisible, SkillCategory.Character, 2,
                Resources.Skills_SkillVirtuallyInvisibleDescr, 
                new StatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SneakHide, 1) }, 
                new string[] { Sneaking.Name });
            Theft = CreateAndAddSkill(Resources.Skills_SkillTheft, SkillCategory.Character, 2, 
                Resources.Skills_SkillTheftDescr, null, new string[] { Sneaking.Name });
            Lockpicking = CreateAndAddSkill(Resources.Skills_SkillLockpicking, SkillCategory.Character, 1, 
                Resources.Skills_SkillLockpickingDescr, null, new string[] { Sneaking.Name });
            Counterfeiting = CreateAndAddSkill(Resources.Skills_SkillCounterfeiting, SkillCategory.Character, 1, 
                Resources.Skills_SkillCounterfeitingDescr, null, new string[] { Sneaking.Name });
            KnowledgeOfPeople = CreateAndAddSkill(Resources.Skills_SkillKnowledgeOfPeople, SkillCategory.Character, 2, 
                Resources.Skills_SkillKnowledgeOfPeopleDescr, 
                new StatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Insight, 1) }, 
                new string[] { Intimidate.Name, Flirting.Name });
            ActorByBirth = CreateAndAddSkill(Resources.Skills_SkillActorByBirth, SkillCategory.Character, 3, 
                Resources.Skills_SkillActorByBirthDescr, 
                new StatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Performance, Dice.D4, 2) },
                new string[] { KnowledgeOfPeople.Name });
            Tracking = CreateAndAddSkill(Resources.Skills_SkillTracking, SkillCategory.Character, 1, 
                Resources.Skills_SkillTrackingDescr, null, new string[] { NatureStudy.Name });
            PoisonKnowledge = CreateAndAddSkill(Resources.Skills_SkillPoisonKnowledge, SkillCategory.Character, 2, 
                Resources.Skills_SkillPoisonKnowledgeDescr, null, new string[] { NatureStudy.Name });
            Gambling = CreateAndAddSkill(Resources.Skills_SkillGambling, SkillCategory.Character, 1, 
                Resources.Skills_SkillGamblingDescr, null, new string[] { Theft.Name, Lockpicking.Name, Counterfeiting.Name });
            SkilledLier = CreateAndAddSkill(Resources.Skills_SkillSkilledLier, SkillCategory.Character, 2, 
                Resources.Skills_SkillSkilledLierDescr, 
                new StatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Bluff, 1) },
                new string[] { Theft.Name, Lockpicking.Name, Counterfeiting.Name });
            LieDetector = CreateAndAddSkill(Resources.Skills_SkillLieDetector, SkillCategory.Character, 2, 
                Resources.Skills_SkillLieDetectorDescr, null, new string[] { SkilledLier.Name, KnowledgeOfPeople.Name });
            SkilledSpeaker = CreateAndAddSkill(Resources.Skills_SkillSkilledSpeaker, SkillCategory.Character, 3, 
                Resources.Skills_SkillSkilledSpeakerDescr,
                new StatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Persuade, Dice.D4, 1) }, 
                new string[] { KnowledgeOfPeople.Name });

            // Checkpoint 1
            Climbing = CreateAndAddSkill(Resources.Skills_SkillClimbing, SkillCategory.Character, 2, 
                Resources.Skills_SkillClimbingDescr, null,
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Teacher = CreateAndAddSkill(Resources.Skills_SkillTeacher, SkillCategory.Character, 2, 
                Resources.Skills_SkillTeacherDescr, null,
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Plunge = CreateAndAddSkill(Resources.Skills_SkillPlunge, SkillCategory.Character, 2, 
                Resources.Skills_SkillPlungeDescr, null,
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            HardToKill = CreateAndAddSkill(Resources.Skills_SkillHardToKill, SkillCategory.Character, 2, 
                Resources.Skills_SkillHardToKillDescr, null,
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Sympathic = CreateAndAddSkill(Resources.Skills_SkillSympathic, SkillCategory.Character, 2, 
                Resources.Skills_SkillSympathicDescr, null,
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Alertness = CreateAndAddSkill(Resources.Skills_SkillAlertness, SkillCategory.Character, 2, 
                Resources.Skills_SkillAlertnessDescr, 
                new StatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Perceive, 1), new AttributeTestStatModifier(Resources.AttributeTests_Inspect, 1) },
                new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            RescueIsNear = CreateAndAddSkill(Resources.Skills_SkillRescueIsNear, SkillCategory.Character, 2, 
                Resources.Skills_SkillRescueIsNearDescr, null, new string[] { Plunge.Name });
            Etiquette = CreateAndAddSkill(Resources.Skills_SkillEtiquette, SkillCategory.Character, 2, 
                Resources.Skills_SkillEtiquetteDescr, null, new string[] { Sympathic.Name });
            Trading = CreateAndAddSkill(Resources.Skills_SkillTrading, SkillCategory.Character, 2, 
                Resources.Skills_SkillTradingDescr, null, new string[] { Sympathic.Name });

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
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            CommandTone = CreateAndAddSkill(Resources.Skills_SkillCommandTone, SkillCategory.Character, 2, 
                Resources.Skills_SkillCommandToneDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            Luck = CreateAndAddSkill(Resources.Skills_SkillLuck, SkillCategory.Character, 2, 
                Resources.Skills_SkillLuckDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            Healing = CreateAndAddSkill(Resources.Skills_SkillHealing, SkillCategory.Character, 2, 
                Resources.Skills_SkillHealingDescr, null,
                new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            LastBreath = CreateAndAddSkill(Resources.Skills_SkillLastBreath, SkillCategory.Character, 2, 
                Resources.Skills_SkillLastBreathDescr, null, new string[] { Perseverence.Name });
            FutureMarket = CreateAndAddSkill(Resources.Skills_SkillFutureMarket, SkillCategory.Character, 2,
                Resources.Skills_SkillFutureMarketDescr, null, new string[] { Perseverence.Name });
            Avenger = CreateAndAddSkill(Resources.Skills_SkillAvenger, SkillCategory.Character, 2,
                Resources.Skills_SkillAvengerDescr, null, new string[] { Momentum.Name });

            // Repeatable Skills
            AddSkill(HP = new Skill(Resources.Skills_SkillHP, SkillCategory.Character, 3, Resources.Skills_SkillHPDescr, 
                new StatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxHealthModifierBonus), 6) }).SetRepeatable());
            AddSkill(Profession = new Skill(Resources.Skills_SkillProfession, SkillCategory.Character, 3, Resources.Skills_SkillProfessionDescr, null).SetRepeatable());
            AddSkill(Stamina2 = new Skill(Resources.Skills_SkillStamina2, SkillCategory.Character, 3, Resources.Skills_SkillStamina2Descr,
                new StatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxStaminaModifierBonus), 2) }).SetRepeatable());
            AddSkill(Stamina4 = new Skill(Resources.Skills_SkillStamina4, SkillCategory.Character, 5, Resources.Skills_SkillStamina4Descr,
                new StatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxStaminaModifierBonus), 4) }).SetRepeatable());
            AddSkill(Stats = new Skill(Resources.Skills_SkillStats, SkillCategory.Character, 5, Resources.Skills_SkillStatsDescr, null).SetRepeatable());
            AddSkill(Energy3 = new Skill(Resources.Skills_SkillEnergy3, SkillCategory.Character, 3, Resources.Skills_SkillEnergy3Descr,
                new StatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxEnergyModifierBonus), 3) }).SetRepeatable());
            AddSkill(Energy6 = new Skill(Resources.Skills_SkillEnergy6, SkillCategory.Character, 5, Resources.Skills_SkillEnergy6Descr,
                new StatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxEnergyModifierBonus), 6) }).SetRepeatable());
            AddSkill(NextLevel = new Skill(Resources.Skills_SkillNextLevel, SkillCategory.Character, 4, Resources.Skills_SkillNextLevelDescr, null).SetRepeatable());
            AddSkill(NextElemental = new Skill(Resources.Skills_SkillNextElemental, SkillCategory.Character, 5, Resources.Skills_SkillNextElementalDescr, null).SetRepeatable());
            AddSkill(ElementalProfessionGreen = new Skill(Resources.Skills_SkillElementalProfessionGreen, SkillCategory.Character, 1, Resources.Skills_SkillElementalProfessionGreenDescr, null).SetRepeatable());
            AddSkill(ElementalProfessionYellow = new Skill(Resources.Skills_SkillElementalProfessionYellow, SkillCategory.Character, 3, Resources.Skills_SkillElementalProfessionYellowDescr, null).SetRepeatable());
            AddSkill(ElementalProfessionRed = new Skill(Resources.Skills_SkillElementalProfessionRed, SkillCategory.Character, 5, Resources.Skills_SkillElementalProfessionRedDescr).SetRepeatable());
            AddSkill(FourthElemental = new Skill(Resources.Skills_SkillFourthElemental, SkillCategory.Character, 6, Resources.Skills_SkillFourthElementalDescr, null).SetRepeatable());

            #endregion Character

            #region Melee
            // Checkpoint 0
            LightBlow = CreateAndAddSkill(Resources.Skills_SkillLightBlow, SkillCategory.Melee, 1, Resources.Skills_SkillLightBlowDescr, 
                new StatModifier[] 
                {
                    new CalculatorModifierStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2.0),
                    new CalculatorModifierStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 0.5, CalculatorBonusType.Multiplicative)
                });
            Smithing = CreateAndAddSkill(Resources.Skills_SkillSmithing, SkillCategory.Melee, 1, Resources.Skills_SkillSmithingDescr, null);
            RunOver = CreateAndAddSkill(Resources.Skills_SkillRunOver, SkillCategory.Melee, 2, Resources.Skills_SkillRunOverDescr, 
                new StatModifier[] { new CalculatorModifierStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D4, 2) });
            AimedAttackMelee = CreateAndAddSkill(Resources.Skills_SkillAimedAttackMelee, SkillCategory.Melee, 2,
                Resources.Skills_SkillAimedAttackMeleeDescr, 
                new StatModifier[] { new CalculatorModifierStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -5) },
                new string[] { LightBlow.Name });
            WeaponsAndArmor = CreateAndAddSkill(Resources.Skills_SkillWeaponsAndArmor, SkillCategory.Melee, 2, 
                Resources.Skills_SkillWeaponsAndArmorDescr, null, new string[] { Smithing.Name });
            Kick = CreateAndAddSkill(Resources.Skills_SkillKick, SkillCategory.Melee, 2, 
                Resources.Skills_SkillKickDescr, null, new string[] { RunOver.Name });
            Taunt = CreateAndAddSkill(Resources.Skills_SkillTaunt, SkillCategory.Melee, 2, 
                Resources.Skills_SkillTauntDescr, null, new string[] { RunOver.Name });
            ArmorBreaker = CreateAndAddSkill(Resources.Skills_SkillArmorBreaker, SkillCategory.Melee, 2, 
                Resources.Skills_SkillArmorBreakerDescr, null, new string[] { AimedAttackMelee.Name });
            Assassinate = CreateAndAddSkill(Resources.Skills_SkillAssassinate, SkillCategory.Melee, 2, 
                Resources.Skills_SkillAssassinateDescr, null, new string[] { AimedAttackMelee.Name, Sneaking.Name });
            JumpAttack = CreateAndAddSkill(Resources.Skills_SkillJumpAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillJumpAttackDescr, 
                new StatModifier[] { new CalculatorModifierStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2) },
                new string[] { Kick.Name });

            // Checkpoint 1
            OneHandedCombat = CreateAndAddSkill(Resources.Skills_SkillOneHandedFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillOneHandedFightingDescr, 
                new StatModifier[]
                {
                    new OverviewStatModifier(nameof(OverviewViewModel.InitiativeModifierBonus), 1),
                    new CalculatorModifierStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 1),
                    new CalculatorModifierStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1),
                },
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            SecondHand = CreateAndAddSkill(Resources.Skills_SkillSecondHand, SkillCategory.Melee, 2, 
                Resources.Skills_SkillSecondHandDescr, null,
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            Shield = CreateAndAddSkill(Resources.Skills_SkillShield, SkillCategory.Melee, 2, 
                Resources.Skills_SkillShieldDescr, null,
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            DefensiveFighting = CreateAndAddSkill(Resources.Skills_SkillDefensiveFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDefensiveFightingDescr, null,
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            Nimble = CreateAndAddSkill(Resources.Skills_SkillNimble, SkillCategory.Melee, 2, 
                Resources.Skills_SkillNimbleDescr, new StatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.InitiativeModifierBonus), 2) },
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            AggressiveCombat = CreateAndAddSkill(Resources.Skills_SkillAgressiveFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillAgressiveFightingDescr, null,
                new string[] { AimedAttackMelee.Name, ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            Fencing = CreateAndAddSkill(Resources.Skills_SkillFencing, SkillCategory.Melee, 2, 
                Resources.Skills_SkillFencingDescr, null, new string[] { OneHandedCombat.Name });
            FullDamage = CreateAndAddSkill(Resources.Skills_SkillFullDamage, SkillCategory.Melee, 2, 
                Resources.Skills_SkillFullDamageDescr, null, new string[] { SecondHand.Name });
            ShieldBash = CreateAndAddSkill(Resources.Skills_SkillShieldBash, SkillCategory.Melee, 2, 
                Resources.Skills_SkillShieldBashDescr, null, new string[] { Shield.Name });
            Parade = CreateAndAddSkill(Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 2, 
                Resources.Skills_SkillParadeDescr, null, new string[] { DefensiveFighting.Name });
            ThereAndAway = CreateAndAddSkill(Resources.Skills_SkillThereAndAway, SkillCategory.Melee, 2,
                Resources.Skills_SkillThereAndAwayDescr, null, new string[] { Nimble.Name });
            RecklessAttack = CreateAndAddSkill(Resources.Skills_SkillRecklessAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillRecklessAttackDescr, null, new string[] { AggressiveCombat.Name });
            DuplexFerrum = CreateAndAddSkill(Resources.Skills_SkillDuplexFerrum, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDuplexFerrumDescr, null, new string[] { FullDamage.Name });
            SomethingWithShield = CreateAndAddSkill(Resources.Skills_SkillSomethingWithShield, SkillCategory.Melee, 2, 
                Resources.Skills_SkillSomethingWithShieldDescr, null, new string[] { ShieldBash.Name });
            QuickParade = CreateAndAddSkill(Resources.Skills_SkillQuickParade, SkillCategory.Melee, 2, 
                Resources.Skills_SkillQuickParadeDescr, null, new string[] { Parade.Name });
            SkillfulRetreat = CreateAndAddSkill(Resources.Skills_SkillSkillfulRetreat, SkillCategory.Melee, 2, 
                Resources.Skills_SkillSkillfulRetreatDescr, null, new string[] { DefensiveFighting.Name, ThereAndAway.Name });
            Feint = CreateAndAddSkill(Resources.Skills_SkillFeint, SkillCategory.Melee, 2, 
                Resources.Skills_SkillFeintDescr, null, new string[] { Nimble.Name });
            RoundHouseAttack = CreateAndAddSkill(Resources.Skills_SkillRoundHouseAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillRoundHouseAttackDescr, null, new string[] { RecklessAttack.Name });
            HeavyParade = CreateAndAddSkill(Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 2, 
                Resources.Skills_SkillHeavyParadeDescr, null, new string[] { QuickParade.Name });
            PerfectBlock = CreateAndAddSkill(Resources.Skills_SkillPerfectBlock, SkillCategory.Melee, 2, 
                Resources.Skills_SkillPerfectBlockDescr, null, new string[] { QuickParade.Name });
            DevastatingAttack = CreateAndAddSkill(Resources.Skills_SkillDevastatingAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDevastatingAttackDescr, null, new string[] { RoundHouseAttack.Name });

            // Checkpoint 2
            Cavallery = CreateAndAddSkill(Resources.Skills_SkillCavallery, SkillCategory.Melee, 2, 
                Resources.Skills_SkillCavalleryDescr, null,
                new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            DefensiveStance = CreateAndAddSkill(Resources.Skills_SkillDefensiveStance, SkillCategory.Melee, 2,
                Resources.Skills_SkillDefensiveStanceDescr, null,
                new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            ArmorUp = CreateAndAddSkill(Resources.Skills_SkillArmorUp, SkillCategory.Melee, 2,
                Resources.Skills_SkillArmorUpDescr, null, new string[] { DefensiveStance.Name });
            AccurateMelee = CreateAndAddSkill(Resources.Skills_SkillAccurateMelee, SkillCategory.Melee, 2, 
                Resources.Skills_SkillAccurateMeleeDescr, null,
                new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            RecognizeStyle = CreateAndAddSkill(Resources.Skills_SkillRecognizeStyle, SkillCategory.Melee, 2,
                Resources.Skills_SkillRecognizeStyleDescr, null,
                new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            CripplingBlow = CreateAndAddSkill(Resources.Skills_SkillCripplingBlow, SkillCategory.Melee, 2,
                Resources.Skills_SkillCripplingBlowDescr, null, new string[] { RecognizeStyle.Name });
            HijackerMelee = CreateAndAddSkill(Resources.Skills_SkillHijackerMelee, SkillCategory.Melee, 2, 
                Resources.Skills_SkillHijackerMeleeDescr, null, new string[] { Cavallery.Name });
            Armor = CreateAndAddSkill(Resources.Skills_SkillArmor, SkillCategory.Melee, 2, 
                Resources.Skills_SkillArmorDescr, null, new string[] { DefensiveStance.Name });
            Combo = CreateAndAddSkill(Resources.Skills_SkillCombo, SkillCategory.Melee, 2,
                Resources.Skills_SkillComboDescr, null, new string[] { AccurateMelee.Name });
            PerfectBlow = CreateAndAddSkill(Resources.Skills_SkillPerfectBlow, SkillCategory.Melee, 2, 
                Resources.Skills_SkillPerfectBlowDescr, null, new string[] { AccurateMelee.Name });
            EveryBlowAHit = CreateAndAddSkill(Resources.Skills_SkillEveryBlowAHit, SkillCategory.Melee, 2, 
                Resources.Skills_SkillEveryBlowAHitDescr, null, new string[] { AccurateMelee.Name });
            TakeAHit = CreateAndAddSkill(Resources.Skills_SkillTakeAHit, SkillCategory.Melee, 2,
                Resources.Skills_SkillTakeAHitDescr, null, new string[] { RecognizeStyle.Name });

            // Checkpoint 3
            AttackOfOpportunity = CreateAndAddSkill(Resources.Skills_SkillAttackOfOpportunity, SkillCategory.Melee, 2, 
                Resources.Skills_SkillAttackOfOpportunityDescr, null,
                new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            DisarmMelee = CreateAndAddSkill(Resources.Skills_SkillDisarmMelee, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDisarmMeleeDescr, null,
                new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
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
                Resources.Skills_SkillLoneWarriorDescr, null,
                new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            ChainAttack = CreateAndAddSkill(Resources.Skills_SkillChainAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillChainAttackDescr, null, new string[] { DisarmMelee.Name });
            ShieldBreaker = CreateAndAddSkill(Resources.Skills_SkillShieldBreaker, SkillCategory.Melee, 2, 
                Resources.Skills_SkillShieldBreakerDescr, null, new string[] { HeavyFighting.Name });
            BladeFan = CreateAndAddSkill(Resources.Skills_SkillBladeFan, SkillCategory.Melee, 2, 
                Resources.Skills_SkillBladeFanDescr, null, new string[] { Riposte.Name });

            #endregion Melee

            #region Ranged
            // Checkpoint 0
            LightShot = CreateAndAddSkill(Resources.Skills_SkillLightShot, SkillCategory.Ranged, 2, Resources.Skills_SkillLightShotDescr, null);
            Quickdraw = CreateAndAddSkill(Resources.Skills_SkillQuickdraw, SkillCategory.Ranged, 2, Resources.Skills_SkillQuickdrawDescr, null);
            SkilledWithThrowingWeapons = CreateAndAddSkill(Resources.Skills_SkillSkilledWithThrowingWeapons, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillSkilledWithThrowingWeaponsDescr);
            BetterThanThrowing = CreateAndAddSkill(Resources.Skills_SkillBetterThanThrowing, SkillCategory.Ranged, 2, Resources.Skills_SkillBetterThanThrowingDescr);
            CalmAiming = CreateAndAddSkill(Resources.Skills_SkillCalmAiming, SkillCategory.Ranged, 2,
                Resources.Skills_SkillCalmAimingDescr, null, new string[]{ LightShot.Name });
            DisarmRanged = CreateAndAddSkill(Resources.Skills_SkillDisarmRanged, SkillCategory.Ranged, 2,
                Resources.Skills_SkillDisarmRangedDescr, null, new string[]{ Quickdraw.Name });
            DualThrow = CreateAndAddSkill(Resources.Skills_SkillDualThrow, SkillCategory.Ranged, 2,
                Resources.Skills_SkillDualThrowDescr, null, new string[]{ SkilledWithThrowingWeapons.Name });
            SlingshotMarksman = CreateAndAddSkill(Resources.Skills_SkillSlingshotMarksman, SkillCategory.Ranged, 2,
                Resources.Skills_SkillSlingshotMarksmanDescr, null, new string[]{ BetterThanThrowing.Name });

            // Checkpoint 1
            ShootFromTheSaddle = CreateAndAddSkill(Resources.Skills_SkillShootFromTheSaddle, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillShootFromTheSaddleDescr, null, new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            BuildArrows = CreateAndAddSkill(Resources.Skills_SkillBuildArrows, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillBuildArrowsDescr, null, new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            PreciseThrow = CreateAndAddSkill(Resources.Skills_SkillPreciseThrow, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillPreciseThrowDescr, null, new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            AimedAttackRanged = CreateAndAddSkill(Resources.Skills_SkillAimedAttackRanged, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillAimedAttackRangedDescr, null, new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            AccurateRanged = CreateAndAddSkill(Resources.Skills_SkillAccurateRanged, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillAccurateRangedDescr, null, new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            HijackerRanged = CreateAndAddSkill(Resources.Skills_SkillHijackerRanged, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillHijackerRangedDescr, null, new string[]{ ShootFromTheSaddle.Name });
            BowMaking = CreateAndAddSkill(Resources.Skills_SkillBowMaking, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillBowMakingDescr, null, new string[]{ BuildArrows.Name });
            StrongThrow = CreateAndAddSkill(Resources.Skills_SkillStrongThrow, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillStrongThrowDescr, null, new string[]{ PreciseThrow.Name });
            Headshot = CreateAndAddSkill(Resources.Skills_SkillHeadshot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillHeadshotDescr, null, new string[]{ AimedAttackRanged.Name });
            BackLine = CreateAndAddSkill(Resources.Skills_SkillBackLine, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillBackLineDescr, null, new string[]{ AccurateRanged.Name });
            RoutinedWithThrowingWeapons = CreateAndAddSkill(Resources.Skills_SkillRoutinedWithThrowingWeapons, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillRoutinedWithThrowingWeaponsDescr, null, new string[]{ StrongThrow.Name });
            ProfessionalSlingshotMarksman = CreateAndAddSkill(Resources.Skills_SkillProfessionalSlingshotMarksman, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillProfessionalSlingshotMarksmanDescr, null, new string[]{ AccurateRanged.Name });
            PerfectShot = CreateAndAddSkill(Resources.Skills_SkillPerfectShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillPerfectShotDescr, null, new string[]{ BackLine.Name });

            // Checkpoint 2
            SurpriseAttack = CreateAndAddSkill(Resources.Skills_SkillSurpriseAttack, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillSurpriseAttackDescr, null,
                new string[]{ HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            NailDown = CreateAndAddSkill(Resources.Skills_SkillNailDown, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillNailDownDescr, null,
                new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            CurvedShot = CreateAndAddSkill(Resources.Skills_SkillCurvedShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillCurvedShotDescr, null,
                new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            QuickAim = CreateAndAddSkill(Resources.Skills_SkillQuickAim, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillQuickAimDescr, null,
                new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            StrongArrows = CreateAndAddSkill(Resources.Skills_SkillStrongArrows, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillStrongArrowsDescr, null,
                new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            PiercingArrow = CreateAndAddSkill(Resources.Skills_SkillPiercingArrow, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillPiercingArrowDescr, null, new string[]{ NailDown.Name });
            LuckyShot = CreateAndAddSkill(Resources.Skills_SkillLuckyShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillLuckyShotDescr, null, new string[]{ CurvedShot.Name });
            DoubleShot = CreateAndAddSkill(Resources.Skills_SkillDoubleShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillDoubleShotDescr, null, new string[]{ QuickAim.Name });
            MasterfulArcher = CreateAndAddSkill(Resources.Skills_SkillMasterfulArcher, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillMasterfulArcherDescr, null, new string[]{ StrongArrows.Name });
            Magazine = CreateAndAddSkill(Resources.Skills_SkillMagazine, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillMagazineDescr, null, new string[]{ DoubleShot.Name });

            // Checkpoint 3
            Oneshot = CreateAndAddSkill(Resources.Skills_SkillOneshot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillOneshotDescr, null,
                new string[]{ SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            LastShot = CreateAndAddSkill(Resources.Skills_SkillLastShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillLastShotDescr, null,
                new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            HuntersMark = CreateAndAddSkill(Resources.Skills_SkillHuntersMark, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillHuntersMarkDescr, null,
                new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            Readiness = CreateAndAddSkill(Resources.Skills_SkillReadiness, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillReadinessDescr, null,
                new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            MasterOfThrowingWeapons = CreateAndAddSkill(Resources.Skills_SkillMasterOfThrowingWeapons, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillMasterOfThrowingWeaponsDescr, null,
                new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            Trueshot = CreateAndAddSkill(Resources.Skills_SkillTrueshot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillTrueshotDescr, null, new string[]{ Oneshot.Name });
            Return = CreateAndAddSkill(Resources.Skills_SkillReturn, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillReturnDescr, null, new string[]{ MasterOfThrowingWeapons.Name });

            #endregion Ranged
            #endregion Skill Definitions
        }

        public int GetSkillIndexFromName(string name)
        {
            int l = 0;
            int r = SkillsList.Count - 1;
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

        private Skill CreateAndAddSkill(string name, SkillCategory skillCategory, int maxSkillPoints, 
            string description, StatModifier[]? skillModifiers = null, string[]? skillDependencies = null) 
        {
            var skill = new Skill(name, skillCategory, maxSkillPoints, description, skillModifiers, skillDependencies);
            return AddSkill(skill);
        }

        private Skill AddSkill(Skill skill)
        {
            SkillsList.Add(skill);
            return skill;
        }
    }
}
