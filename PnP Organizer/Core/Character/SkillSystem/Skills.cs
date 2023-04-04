using PnP_Organizer.Core.BattleAssistant;
using PnP_Organizer.Core.Character.SkillSystem;
using PnP_Organizer.Core.Character.StatModifiers;
using PnP_Organizer.IO;
using PnP_Organizer.Properties;
using PnP_Organizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP_Organizer.Core.Character
{
    public class Skills
    {
        /// <summary>
        /// Contains every skill
        /// </summary>
        public Dictionary<SkillIdentifier, Skill> Registry { get; }

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
            Registry = new();

            #region Skill Definitions
            #region Character
            // Checkpoint 0
            Sneaking = CreateAndAddSkill(nameof(Sneaking), Resources.Skills_SkillSneaking, SkillCategory.Character, 2, Resources.Skills_SkillSneakingDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SneakHide, Dice.D4) });
            Intimidate = CreateAndAddSkill(nameof(Intimidate), Resources.Skills_SkillIntimidate, SkillCategory.Character, 2, Resources.Skills_SkillIntimidateDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Intimidate, Dice.D4) });
            Flirting = CreateAndAddSkill(nameof(Flirting), Resources.Skills_SkillFlirting, SkillCategory.Character, 2, Resources.Skills_SkillFlirtingDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Performance, 0, true) }); // Dummy StatModifier to show the skill on the AttributeTestsPage
            NatureStudy = CreateAndAddSkill(nameof(NatureStudy), Resources.Skills_SkillNatureStudy, SkillCategory.Character, 2, Resources.Skills_SkillNatureStudyDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Nature, Dice.D4) });
            VirtuallyInvisible = CreateAndAddSkill(nameof(VirtuallyInvisible), Resources.Skills_SkillVirtuallyInvisible, SkillCategory.Character, 2,
                Resources.Skills_SkillVirtuallyInvisibleDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SneakHide, 1) }, 
                new SkillIdentifier[] { Sneaking.Identifier });
            Theft = CreateAndAddSkill(nameof(Theft), Resources.Skills_SkillTheft, SkillCategory.Character, 2, 
                Resources.Skills_SkillTheftDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SleightOfHand, Dice.D4, 0, true) },
                new SkillIdentifier[] { Sneaking.Identifier });
            Lockpicking = CreateAndAddSkill(nameof(Lockpicking), Resources.Skills_SkillLockpicking, SkillCategory.Character, 1, 
                Resources.Skills_SkillLockpickingDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SleightOfHand, Dice.D4, 0, true) }, 
                new SkillIdentifier[] { Sneaking.Identifier });
            Counterfeiting = CreateAndAddSkill(nameof(Counterfeiting), Resources.Skills_SkillCounterfeiting, SkillCategory.Character, 1, 
                Resources.Skills_SkillCounterfeitingDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_SleightOfHand, Dice.D4, 0, true) }, 
                new SkillIdentifier[] { Sneaking.Identifier });
            KnowledgeOfPeople = CreateAndAddSkill(nameof(KnowledgeOfPeople), Resources.Skills_SkillKnowledgeOfPeople, SkillCategory.Character, 2, 
                Resources.Skills_SkillKnowledgeOfPeopleDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Insight, 1) }, 
                new SkillIdentifier[] { Intimidate.Identifier, Flirting.Identifier });
            ActorByBirth = CreateAndAddSkill(nameof(ActorByBirth), Resources.Skills_SkillActorByBirth, SkillCategory.Character, 3, 
                Resources.Skills_SkillActorByBirthDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Performance, Dice.D4, 2) },
                new SkillIdentifier[] { KnowledgeOfPeople.Identifier });
            Tracking = CreateAndAddSkill(nameof(Tracking), Resources.Skills_SkillTracking, SkillCategory.Character, 1, 
                Resources.Skills_SkillTrackingDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Nature, Dice.D4, 0, true) }, 
                new SkillIdentifier[] { NatureStudy.Identifier });
            PoisonKnowledge = CreateAndAddSkill(nameof(PoisonKnowledge), Resources.Skills_SkillPoisonKnowledge, SkillCategory.Character, 2, 
                Resources.Skills_SkillPoisonKnowledgeDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Nature, Dice.D4, 0, true) },
                new SkillIdentifier[] { NatureStudy.Identifier });
            Gambling = CreateAndAddSkill(nameof(Gambling), Resources.Skills_SkillGambling, SkillCategory.Character, 1, 
                Resources.Skills_SkillGamblingDescr,
                new IStatModifier[] 
                { 
                    new AttributeTestStatModifier(Resources.AttributeTests_SleightOfHand, Dice.D4, 0, true),
                    new AttributeTestStatModifier(Resources.AttributeTests_Performance, Dice.D4, 0, true)
                }, 
                new SkillIdentifier[] { Theft.Identifier, Lockpicking.Identifier, Counterfeiting.Identifier });
            SkilledLier = CreateAndAddSkill(nameof(SkilledLier), Resources.Skills_SkillSkilledLier, SkillCategory.Character, 2, 
                Resources.Skills_SkillSkilledLierDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Bluff, 1) },
                new SkillIdentifier[] { Theft.Identifier, Lockpicking.Identifier, Counterfeiting.Identifier });
            LieDetector = CreateAndAddSkill(nameof(LieDetector), Resources.Skills_SkillLieDetector, SkillCategory.Character, 2, 
                Resources.Skills_SkillLieDetectorDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Insight, Dice.D4, 0, true) }, 
                new SkillIdentifier[] { SkilledLier.Identifier, KnowledgeOfPeople.Identifier });
            SkilledSpeaker = CreateAndAddSkill(nameof(SkilledSpeaker), Resources.Skills_SkillSkilledSpeaker, SkillCategory.Character, 3, 
                Resources.Skills_SkillSkilledSpeakerDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Persuade, Dice.D4, 1) }, 
                new SkillIdentifier[] { KnowledgeOfPeople.Identifier });

            // Checkpoint 1
            Climbing = CreateAndAddSkill(nameof(Climbing), Resources.Skills_SkillClimbing, SkillCategory.Character, 2, 
                Resources.Skills_SkillClimbingDescr, 
                new IStatModifier[] 
                {
                    new AttributeTestStatModifier(Resources.AttributeTests_Athletic, Dice.D4, 0, true),
                    new AttributeTestStatModifier(Resources.AttributeTests_Acrobatic, Dice.D4, 0, true)
                },
                new SkillIdentifier[] { Gambling.Identifier, SkilledLier.Identifier, LieDetector.Identifier, SkilledSpeaker.Identifier, ActorByBirth.Identifier, Tracking.Identifier, PoisonKnowledge.Identifier });
            Teacher = CreateAndAddSkill(nameof(Teacher), Resources.Skills_SkillTeacher, SkillCategory.Character, 2, 
                Resources.Skills_SkillTeacherDescr, null,
                new SkillIdentifier[] { Gambling.Identifier, SkilledLier.Identifier, LieDetector.Identifier, SkilledSpeaker.Identifier, ActorByBirth.Identifier, Tracking.Identifier, PoisonKnowledge.Identifier });
            Plunge = CreateAndAddSkill(nameof(Plunge), Resources.Skills_SkillPlunge, SkillCategory.Character, 2, 
                Resources.Skills_SkillPlungeDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 8) },
                new SkillIdentifier[] { Gambling.Identifier, SkilledLier.Identifier, LieDetector.Identifier, SkilledSpeaker.Identifier, ActorByBirth.Identifier, Tracking.Identifier, PoisonKnowledge.Identifier },
                activationType: ActivationType.Active, staminaCost: 3);
            HardToKill = CreateAndAddSkill(nameof(HardToKill), Resources.Skills_SkillHardToKill, SkillCategory.Character, 5, 
                Resources.Skills_SkillHardToKillDescr, null,
                new SkillIdentifier[] { Gambling.Identifier, SkilledLier.Identifier, LieDetector.Identifier, SkilledSpeaker.Identifier, ActorByBirth.Identifier, Tracking.Identifier, PoisonKnowledge.Identifier });
            Sympathic = CreateAndAddSkill(nameof(Sympathic), Resources.Skills_SkillSympathic, SkillCategory.Character, 2, 
                Resources.Skills_SkillSympathicDescr, null,
                new SkillIdentifier[] { Gambling.Identifier, SkilledLier.Identifier, LieDetector.Identifier, SkilledSpeaker.Identifier, ActorByBirth.Identifier, Tracking.Identifier, PoisonKnowledge.Identifier });
            Alertness = CreateAndAddSkill(nameof(Alertness), Resources.Skills_SkillAlertness, SkillCategory.Character, 2, 
                Resources.Skills_SkillAlertnessDescr, 
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Perceive, 1), new AttributeTestStatModifier(Resources.AttributeTests_Inspect, 1) },
                new SkillIdentifier[] { Gambling.Identifier, SkilledLier.Identifier, LieDetector.Identifier, SkilledSpeaker.Identifier, ActorByBirth.Identifier, Tracking.Identifier, PoisonKnowledge.Identifier });
            RescueIsNear = CreateAndAddSkill(nameof(RescueIsNear), Resources.Skills_SkillRescueIsNear, SkillCategory.Character, 2, 
                Resources.Skills_SkillRescueIsNearDescr, null, new SkillIdentifier[] { Plunge.Identifier }, activationType: ActivationType.Active);
            Etiquette = CreateAndAddSkill(nameof(Etiquette), Resources.Skills_SkillEtiquette, SkillCategory.Character, 1, 
                Resources.Skills_SkillEtiquetteDescr, null, new SkillIdentifier[] { Sympathic.Identifier });
            Trading = CreateAndAddSkill(nameof(Trading), Resources.Skills_SkillTrading, SkillCategory.Character, 2, 
                Resources.Skills_SkillTradingDescr, new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Persuade, Dice.D4, 0, true) },
                new SkillIdentifier[] { Sympathic.Identifier });

            // Checkpoint 2
            Perseverence = CreateAndAddSkill(nameof(Perseverence), Resources.Skills_SkillPerseverence, SkillCategory.Character, 2, 
                Resources.Skills_SkillPerseverenceDescr, null,
                new SkillIdentifier[] { Climbing.Identifier, Teacher.Identifier, RescueIsNear.Identifier, HardToKill.Identifier, Etiquette.Identifier, Trading.Identifier, Alertness.Identifier });
            Encouragement = CreateAndAddSkill(nameof(Encouragement), Resources.Skills_SkillEncouragement, SkillCategory.Character, 2, 
                Resources.Skills_SkillEncouragementDescr, null,
                new SkillIdentifier[] { Climbing.Identifier, Teacher.Identifier, RescueIsNear.Identifier, HardToKill.Identifier, Etiquette.Identifier, Trading.Identifier, Alertness.Identifier });
            EnergyBoost = CreateAndAddSkill(nameof(EnergyBoost), Resources.Skills_SkillEnergyBoost, SkillCategory.Character, 2, 
                Resources.Skills_SkillEnergyBoostDescr, null,
                new SkillIdentifier[] { Climbing.Identifier, Teacher.Identifier, RescueIsNear.Identifier, HardToKill.Identifier, Etiquette.Identifier, Trading.Identifier, Alertness.Identifier });
            Momentum = CreateAndAddSkill(nameof(Momentum), Resources.Skills_SkillMomentum, SkillCategory.Character, 2, 
                Resources.Skills_SkillMomentumDescr, null,
                new SkillIdentifier[] { Climbing.Identifier, Teacher.Identifier, RescueIsNear.Identifier, HardToKill.Identifier, Etiquette.Identifier, Trading.Identifier, Alertness.Identifier },
                activationType: ActivationType.Active);
            CommandTone = CreateAndAddSkill(nameof(CommandTone), Resources.Skills_SkillCommandTone, SkillCategory.Character, 2, 
                Resources.Skills_SkillCommandToneDescr, null,
                new SkillIdentifier[] { Climbing.Identifier, Teacher.Identifier, RescueIsNear.Identifier, HardToKill.Identifier, Etiquette.Identifier, Trading.Identifier, Alertness.Identifier });
            Luck = CreateAndAddSkill(nameof(Luck), Resources.Skills_SkillLuck, SkillCategory.Character, 2, 
                Resources.Skills_SkillLuckDescr, null,
                new SkillIdentifier[] { Climbing.Identifier, Teacher.Identifier, RescueIsNear.Identifier, HardToKill.Identifier, Etiquette.Identifier, Trading.Identifier, Alertness.Identifier },
                activationType: ActivationType.Active);
            Healing = CreateAndAddSkill(nameof(Healing), Resources.Skills_SkillHealing, SkillCategory.Character, 2, 
                Resources.Skills_SkillHealingDescr, null,
                new SkillIdentifier[] { Climbing.Identifier, Teacher.Identifier, RescueIsNear.Identifier, HardToKill.Identifier, Etiquette.Identifier, Trading.Identifier, Alertness.Identifier });
            LastBreath = CreateAndAddSkill(nameof(LastBreath), Resources.Skills_SkillLastBreath, SkillCategory.Character, 2, 
                Resources.Skills_SkillLastBreathDescr, null, new SkillIdentifier[] { Perseverence.Identifier }, activationType: ActivationType.Active, usesPerBattle: 1);
            FutureMarket = CreateAndAddSkill(nameof(FutureMarket), Resources.Skills_SkillFutureMarket, SkillCategory.Character, 2,
                Resources.Skills_SkillFutureMarketDescr, null, new SkillIdentifier[] { Perseverence.Identifier }, activationType: ActivationType.Active, usesPerBattle: 1);
            Avenger = CreateAndAddSkill(nameof(Avenger), Resources.Skills_SkillAvenger, SkillCategory.Character, 2,
                Resources.Skills_SkillAvengerDescr, null, new SkillIdentifier[] { Momentum.Identifier }, activationType: ActivationType.Active);

            // Repeatable Skills
            var hpID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character, 
                Name = nameof(HP)
            };
            HP = AddSkill(hpID, new Skill(hpID, Resources.Skills_SkillHP, 3, Resources.Skills_SkillHPDescr, 
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxHealthModifierBonus), 6) }).SetRepeatable());
            var professionID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(Profession)
            };
            Profession = AddSkill(professionID, new Skill(professionID, Resources.Skills_SkillProfession, 3, Resources.Skills_SkillProfessionDescr, null).SetRepeatable());
            var stamina2ID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(Stamina2)
            };
            Stamina2 = AddSkill(stamina2ID, new Skill(stamina2ID, Resources.Skills_SkillStamina2, 3, Resources.Skills_SkillStamina2Descr,
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxStaminaModifierBonus), 2) }).SetRepeatable());
            var stamina4ID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(Stamina4)
            };
            Stamina4 = AddSkill(stamina4ID, new Skill(stamina4ID, Resources.Skills_SkillStamina4, 5, Resources.Skills_SkillStamina4Descr,
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxStaminaModifierBonus), 4) }).SetRepeatable());
            var statsID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(Stats)
            };
            Stats = AddSkill(statsID, new Skill(statsID, Resources.Skills_SkillStats, 5, Resources.Skills_SkillStatsDescr, null).SetRepeatable());
            var energy3ID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(Energy3)
            };
            Energy3 = (new Skill(energy3ID, Resources.Skills_SkillEnergy3, 3, Resources.Skills_SkillEnergy3Descr,
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxEnergyModifierBonus), 3) }).SetRepeatable());
            var energy6ID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(Energy6)
            };
            Energy6 = AddSkill(energy6ID, new Skill(energy6ID, Resources.Skills_SkillEnergy6, 5, Resources.Skills_SkillEnergy6Descr,
                new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.MaxEnergyModifierBonus), 6) }).SetRepeatable());
            var nextLevelID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(NextLevel)
            };
            NextLevel = AddSkill(nextLevelID, new Skill(nextLevelID, Resources.Skills_SkillNextLevel, 4, Resources.Skills_SkillNextLevelDescr, null).SetRepeatable());
            var nextElementalID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(NextElemental)
            };
            NextElemental = AddSkill(nextElementalID, new Skill(nextElementalID, Resources.Skills_SkillNextElemental, 5, Resources.Skills_SkillNextElementalDescr, null).SetRepeatable());
            var elemProfGreenID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(ElementalProfessionGreen)
            };
            ElementalProfessionGreen = AddSkill(elemProfGreenID, new Skill(elemProfGreenID, Resources.Skills_SkillElementalProfessionGreen, 1, Resources.Skills_SkillElementalProfessionGreenDescr, null).SetRepeatable());
            var elemProfYellowID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(ElementalProfessionYellow)
            };
            ElementalProfessionYellow = AddSkill(elemProfYellowID, new Skill(elemProfYellowID, Resources.Skills_SkillElementalProfessionYellow, 3, Resources.Skills_SkillElementalProfessionYellowDescr, null).SetRepeatable());
            var elemProfRedID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(ElementalProfessionRed)
            };
            ElementalProfessionRed = AddSkill(elemProfRedID, new Skill(elemProfRedID, Resources.Skills_SkillElementalProfessionRed, 5, Resources.Skills_SkillElementalProfessionRedDescr).SetRepeatable());
            var fourthElementalID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Character,
                Name = nameof(FourthElemental)
            };
            FourthElemental = AddSkill(fourthElementalID, new Skill(fourthElementalID, Resources.Skills_SkillFourthElemental, 6, Resources.Skills_SkillFourthElementalDescr, null).SetRepeatable());

            #endregion Character

            #region Melee
            // Checkpoint 0
            LightBlow = CreateAndAddSkill(nameof(LightBlow), Resources.Skills_SkillLightBlow, SkillCategory.Melee, 1, Resources.Skills_SkillLightBlowDescr, 
                new IStatModifier[] 
                {
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2.0),
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 0.5, CalculatorBonusType.Multiplicative)
                }, activationType: ActivationType.Active, staminaCost: 1);
            Smithing = CreateAndAddSkill(nameof(Smithing), Resources.Skills_SkillSmithing, SkillCategory.Melee, 1, Resources.Skills_SkillSmithingDescr,
                new IStatModifier[] { new AttributeTestStatModifier(Resources.AttributeTests_Performance, 0, true) }); // Dummy StatModifier to show the skill on the AttributeTestsPage
            RunOver = CreateAndAddSkill(nameof(RunOver), Resources.Skills_SkillRunOver, SkillCategory.Melee, 2, Resources.Skills_SkillRunOverDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D4, 2) },
                activationType: ActivationType.Active, staminaCost: 3);
            AimedAttackMelee = CreateAndAddSkill(nameof(AimedAttackMelee), Resources.Skills_SkillAimedAttackMelee, SkillCategory.Melee, 2,
                Resources.Skills_SkillAimedAttackMeleeDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -5) },
                new SkillIdentifier[] { LightBlow.Identifier }, activationType: ActivationType.Active, staminaCost: 2);
            WeaponsAndArmor = CreateAndAddSkill(nameof(WeaponsAndArmor), Resources.Skills_SkillWeaponsAndArmor, SkillCategory.Melee, 2, 
                Resources.Skills_SkillWeaponsAndArmorDescr, null, new SkillIdentifier[] { Smithing.Identifier });
            Kick = CreateAndAddSkill(nameof(Kick), Resources.Skills_SkillKick, SkillCategory.Melee, 1, 
                Resources.Skills_SkillKickDescr, null, new SkillIdentifier[] { RunOver.Identifier }, activationType: ActivationType.Active, staminaCost: 1);
            Taunt = CreateAndAddSkill(nameof(Taunt), Resources.Skills_SkillTaunt, SkillCategory.Melee, 2, 
                Resources.Skills_SkillTauntDescr, null, new SkillIdentifier[] { RunOver.Identifier });
            ArmorBreaker = CreateAndAddSkill(nameof(ArmorBreaker), Resources.Skills_SkillArmorBreaker, SkillCategory.Melee, 3, 
                Resources.Skills_SkillArmorBreakerDescr, null, new SkillIdentifier[] { AimedAttackMelee.Identifier }, activationType: ActivationType.Active,
                staminaCost: 5);
            var assassinateID = new SkillIdentifier
            {
                SkillCategory = SkillCategory.Melee,
                Name = nameof(Assassinate)
            };
            Assassinate = AddSkill(assassinateID, new Skill(assassinateID, Resources.Skills_SkillAssassinate, 2, 
                Resources.Skills_SkillAssassinateDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 3, CalculatorBonusType.Multiplicative)}, 
                new SkillIdentifier[] { AimedAttackMelee.Identifier }, activationType: ActivationType.Active,staminaCost: 1)
                .AddForcedDependency(Sneaking.Identifier));
            JumpAttack = CreateAndAddSkill(nameof(JumpAttack), Resources.Skills_SkillJumpAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillJumpAttackDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2) },
                new SkillIdentifier[] { Kick.Identifier }, activationType: ActivationType.Active, staminaCost: 1);

            // Checkpoint 1
            OneHandedCombat = CreateAndAddSkill(nameof(OneHandedCombat), Resources.Skills_SkillOneHandedFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillOneHandedFightingDescr, 
                new IStatModifier[]
                {
                    new OverviewStatModifier(nameof(OverviewViewModel.InitiativeModifierBonus), 1),
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1),
                },
                new SkillIdentifier[] { AimedAttackMelee.Identifier, ArmorBreaker.Identifier, Assassinate.Identifier, WeaponsAndArmor.Identifier, JumpAttack.Identifier, Taunt.Identifier });
            SecondHand = CreateAndAddSkill(nameof(SecondHand), Resources.Skills_SkillSecondHand, SkillCategory.Melee, 3, 
                Resources.Skills_SkillSecondHandDescr, null,
                new SkillIdentifier[] { AimedAttackMelee.Identifier, ArmorBreaker.Identifier, Assassinate.Identifier, WeaponsAndArmor.Identifier, JumpAttack.Identifier, Taunt.Identifier });
            Shield = CreateAndAddSkill(nameof(Shield), Resources.Skills_SkillShield, SkillCategory.Melee, 2, 
                Resources.Skills_SkillShieldDescr, null,
                new SkillIdentifier[] { AimedAttackMelee.Identifier, ArmorBreaker.Identifier, Assassinate.Identifier, WeaponsAndArmor.Identifier, JumpAttack.Identifier, Taunt.Identifier });
            DefensiveFighting = CreateAndAddSkill(nameof(DefensiveFighting), Resources.Skills_SkillDefensiveFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDefensiveFightingDescr, 
                new IStatModifier[] 
                { 
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 2),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -2),
                },
                new SkillIdentifier[] { AimedAttackMelee.Identifier, ArmorBreaker.Identifier, Assassinate.Identifier, WeaponsAndArmor.Identifier, JumpAttack.Identifier, Taunt.Identifier },
                activationType: ActivationType.Active);
            Nimble = CreateAndAddSkill(nameof(Nimble), Resources.Skills_SkillNimble, SkillCategory.Melee, 2, 
                Resources.Skills_SkillNimbleDescr, new IStatModifier[] { new OverviewStatModifier(nameof(OverviewViewModel.InitiativeModifierBonus), 2) },
                new SkillIdentifier[] { AimedAttackMelee.Identifier, ArmorBreaker.Identifier, Assassinate.Identifier, WeaponsAndArmor.Identifier, JumpAttack.Identifier, Taunt.Identifier });
            AggressiveCombat = CreateAndAddSkill(nameof(AggressiveCombat), Resources.Skills_SkillAgressiveFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillAgressiveFightingDescr,
                new IStatModifier[]
                {
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, -2),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2),
                },
                new SkillIdentifier[] { AimedAttackMelee.Identifier, ArmorBreaker.Identifier, Assassinate.Identifier, WeaponsAndArmor.Identifier, JumpAttack.Identifier, Taunt.Identifier },
                activationType: ActivationType.Active);
            Fencing = CreateAndAddSkill(nameof(Fencing), Resources.Skills_SkillFencing, SkillCategory.Melee, 3, 
                Resources.Skills_SkillFencingDescr, null, new SkillIdentifier[] { OneHandedCombat.Identifier });
            FullDamage = CreateAndAddSkill(nameof(FullDamage), Resources.Skills_SkillFullDamage, SkillCategory.Melee, 3, 
                Resources.Skills_SkillFullDamageDescr, null, new SkillIdentifier[] { SecondHand.Identifier });
            ShieldBash = CreateAndAddSkill(nameof(ShieldBash), Resources.Skills_SkillShieldBash, SkillCategory.Melee, 2, 
                Resources.Skills_SkillShieldBashDescr, null, new SkillIdentifier[] { Shield.Identifier }, activationType: ActivationType.Active, staminaCost: 2);
            Parade = CreateAndAddSkill(nameof(Parade), Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 2, 
                Resources.Skills_SkillParadeDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, Dice.D4) },
                new SkillIdentifier[] { DefensiveFighting.Identifier });
            ThereAndAway = CreateAndAddSkill(nameof(ThereAndAway), Resources.Skills_SkillThereAndAway, SkillCategory.Melee, 2,
                Resources.Skills_SkillThereAndAwayDescr, null, new SkillIdentifier[] { Nimble.Identifier });
            RecklessAttack = CreateAndAddSkill(nameof(RecklessAttack), Resources.Skills_SkillRecklessAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillRecklessAttackDescr, null, new SkillIdentifier[] { AggressiveCombat.Identifier }, activationType: ActivationType.Active,
                staminaCost: 3);
            DuplexFerrum = CreateAndAddSkill(nameof(DuplexFerrum), Resources.Skills_SkillDuplexFerrum, SkillCategory.Melee, 3, 
                Resources.Skills_SkillDuplexFerrumDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, Dice.D4) },
                new SkillIdentifier[] { FullDamage.Identifier });
            SomethingWithShield = CreateAndAddSkill(nameof(SomethingWithShield), Resources.Skills_SkillSomethingWithShield, SkillCategory.Melee, 2, 
                Resources.Skills_SkillSomethingWithShieldDescr, null, new SkillIdentifier[] { ShieldBash.Identifier }, activationType: ActivationType.Active,
                staminaCost: 3);
            QuickParade = CreateAndAddSkill(nameof(QuickParade), Resources.Skills_SkillQuickParade, SkillCategory.Melee, 2, 
                Resources.Skills_SkillQuickParadeDescr, null, new SkillIdentifier[] { Parade.Identifier }, activationType: ActivationType.Active,
                staminaCost: 1);
            SkillfulRetreat = CreateAndAddSkill(nameof(SkillfulRetreat), Resources.Skills_SkillSkillfulRetreat, SkillCategory.Melee, 2, 
                Resources.Skills_SkillSkillfulRetreatDescr, null, new SkillIdentifier[] { DefensiveFighting.Identifier, ThereAndAway.Identifier }, activationType: ActivationType.Active,
                staminaCost: 1);
            Feint = CreateAndAddSkill(nameof(Feint), Resources.Skills_SkillFeint, SkillCategory.Melee, 2, 
                Resources.Skills_SkillFeintDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 4) },
                new SkillIdentifier[] { Nimble.Identifier });
            RoundHouseAttack = CreateAndAddSkill(nameof(RoundHouseAttack), Resources.Skills_SkillRoundHouseAttack, SkillCategory.Melee, 3, 
                Resources.Skills_SkillRoundHouseAttackDescr, null, new SkillIdentifier[] { RecklessAttack.Identifier }, activationType: ActivationType.Active, staminaCost: 3);
            HeavyParade = CreateAndAddSkill(nameof(HeavyParade), Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 5, 
                Resources.Skills_SkillHeavyParadeDescr, null, new SkillIdentifier[] { QuickParade.Identifier }, activationType: ActivationType.Active, staminaCost: 2);
            PerfectBlock = CreateAndAddSkill(nameof(PerfectBlock), Resources.Skills_SkillPerfectBlock, SkillCategory.Melee, 4, 
                Resources.Skills_SkillPerfectBlockDescr, null, new SkillIdentifier[] { QuickParade.Identifier }, activationType: ActivationType.Active, staminaCost: 5);
            DevastatingAttack = CreateAndAddSkill(nameof(DevastatingAttack), Resources.Skills_SkillDevastatingAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillDevastatingAttackDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 2, CalculatorBonusType.Multiplicative) },
                new SkillIdentifier[] { RoundHouseAttack.Identifier }, activationType: ActivationType.Active, staminaCost: 3);

            // Checkpoint 2
            Cavallery = CreateAndAddSkill(nameof(Cavallery), Resources.Skills_SkillCavallery, SkillCategory.Melee, 3,
                Resources.Skills_SkillCavalleryDescr, null,
                new SkillIdentifier[] { Fencing.Identifier, DuplexFerrum.Identifier, HeavyParade.Identifier, PerfectBlock.Identifier, SkillfulRetreat.Identifier, Feint.Identifier, DevastatingAttack.Identifier });
            DefensiveStance = CreateAndAddSkill(nameof(DefensiveStance), Resources.Skills_SkillDefensiveStance, SkillCategory.Melee, 2,
                Resources.Skills_SkillDefensiveStanceDescr, 
                new IStatModifier[] 
                {
                    new CalculatorStatModifier(CalculatorValueType.Armor, ApplianceMode.EndValue, 2, CalculatorBonusType.Multiplicative),
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, Dice.D6, 2),
                },
                new SkillIdentifier[] { Fencing.Identifier, DuplexFerrum.Identifier, HeavyParade.Identifier, PerfectBlock.Identifier, SkillfulRetreat.Identifier, Feint.Identifier, DevastatingAttack.Identifier },
                activationType: ActivationType.Active, staminaCost: 1);
            ArmorUp = CreateAndAddSkill(nameof(ArmorUp), Resources.Skills_SkillArmorUp, SkillCategory.Melee, 2,
                Resources.Skills_SkillArmorUpDescr, null, new SkillIdentifier[] { DefensiveStance.Identifier }, ActivationType.Active, staminaCost: 1);
            AccurateMelee = CreateAndAddSkill(nameof(AccurateMelee), Resources.Skills_SkillAccurateMelee, SkillCategory.Melee, 1, 
                Resources.Skills_SkillAccurateMeleeDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 2) },
                new SkillIdentifier[] { Fencing.Identifier, DuplexFerrum.Identifier, HeavyParade.Identifier, PerfectBlock.Identifier, SkillfulRetreat.Identifier, Feint.Identifier, DevastatingAttack.Identifier });
            RecognizeStyle = CreateAndAddSkill(nameof(RecognizeStyle), Resources.Skills_SkillRecognizeStyle, SkillCategory.Melee, 2,
                Resources.Skills_SkillRecognizeStyleDescr, 
                new IStatModifier[] 
                {
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 1)
                },
                new SkillIdentifier[] { Fencing.Identifier, DuplexFerrum.Identifier, HeavyParade.Identifier, PerfectBlock.Identifier, SkillfulRetreat.Identifier, Feint.Identifier, DevastatingAttack.Identifier });
            CripplingBlow = CreateAndAddSkill(nameof(CripplingBlow), Resources.Skills_SkillCripplingBlow, SkillCategory.Melee, 2,
                Resources.Skills_SkillCripplingBlowDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -1) }, 
                new SkillIdentifier[] { RecognizeStyle.Identifier }, activationType: ActivationType.Active, staminaCost: 2);
            HijackerMelee = CreateAndAddSkill(nameof(HijackerMelee), Resources.Skills_SkillHijackerMelee, SkillCategory.Melee, 1, 
                Resources.Skills_SkillHijackerMeleeDescr, null, new SkillIdentifier[] { Cavallery.Identifier }, activationType: ActivationType.Active,
                staminaCost: 3);
            Armor = CreateAndAddSkill(nameof(Armor), Resources.Skills_SkillArmor, SkillCategory.Melee, 2, 
                Resources.Skills_SkillArmorDescr, null, new SkillIdentifier[] { DefensiveStance.Identifier });
            Combo = CreateAndAddSkill(nameof(Combo), Resources.Skills_SkillCombo, SkillCategory.Melee, 4,
                Resources.Skills_SkillComboDescr, null, new SkillIdentifier[] { AccurateMelee.Identifier });
            PerfectBlow = CreateAndAddSkill(nameof(PerfectBlow), Resources.Skills_SkillPerfectBlow, SkillCategory.Melee, 4, 
                Resources.Skills_SkillPerfectBlowDescr, null, new SkillIdentifier[] { AccurateMelee.Identifier }, activationType: ActivationType.Active,
                staminaCost: 5).SetOnlySoloUsable();
            EveryBlowAHit = CreateAndAddSkill(nameof(EveryBlowAHit), Resources.Skills_SkillEveryBlowAHit, SkillCategory.Melee, 2, 
                Resources.Skills_SkillEveryBlowAHitDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2) },
                new SkillIdentifier[] { AccurateMelee.Identifier });
            TakeAHit = CreateAndAddSkill(nameof(TakeAHit), Resources.Skills_SkillTakeAHit, SkillCategory.Melee, 1,
                Resources.Skills_SkillTakeAHitDescr, null, new SkillIdentifier[] { RecognizeStyle.Identifier }, activationType: ActivationType.Active);

            // Checkpoint 3
            AttackOfOpportunity = CreateAndAddSkill(nameof(AttackOfOpportunity), Resources.Skills_SkillAttackOfOpportunity, SkillCategory.Melee, 3, 
                Resources.Skills_SkillAttackOfOpportunityDescr, null,
                new SkillIdentifier[] { HijackerMelee.Identifier, Armor.Identifier, ArmorUp.Identifier, Combo.Identifier, PerfectBlow.Identifier, EveryBlowAHit.Identifier, TakeAHit.Identifier },
                activationType: ActivationType.Active, staminaCost: 1);
            DisarmMelee = CreateAndAddSkill(nameof(DisarmMelee), Resources.Skills_SkillDisarmMelee, SkillCategory.Melee, 3, 
                Resources.Skills_SkillDisarmMeleeDescr, null,
                new SkillIdentifier[] { HijackerMelee.Identifier, Armor.Identifier, ArmorUp.Identifier, Combo.Identifier, PerfectBlow.Identifier, EveryBlowAHit.Identifier, TakeAHit.Identifier },
                activationType: ActivationType.Active, staminaCost: 1);
            KillingSpree = CreateAndAddSkill(nameof(KillingSpree), Resources.Skills_SkillKillingSpree, SkillCategory.Melee, 3, 
                Resources.Skills_SkillKillingSpreeDescr, null,
                new SkillIdentifier[] { HijackerMelee.Identifier, Armor.Identifier, ArmorUp.Identifier, Combo.Identifier, PerfectBlow.Identifier, EveryBlowAHit.Identifier, TakeAHit.Identifier });
            HeavyFighting = CreateAndAddSkill(nameof(HeavyFighting), Resources.Skills_SkillHeavyFighting, SkillCategory.Melee, 2, 
                Resources.Skills_SkillHeavyFightingDescr, null,
                new SkillIdentifier[] { HijackerMelee.Identifier, Armor.Identifier, ArmorUp.Identifier, Combo.Identifier, PerfectBlow.Identifier, EveryBlowAHit.Identifier, TakeAHit.Identifier });
            Riposte = CreateAndAddSkill(nameof(Riposte), Resources.Skills_SkillRiposte, SkillCategory.Melee, 2, 
                Resources.Skills_SkillRiposteDescr, null,
                new SkillIdentifier[] { HijackerMelee.Identifier, Armor.Identifier, ArmorUp.Identifier, Combo.Identifier, PerfectBlow.Identifier, EveryBlowAHit.Identifier, TakeAHit.Identifier });
            LoneWarrior = CreateAndAddSkill(nameof(LoneWarrior), Resources.Skills_SkillLoneWarrior, SkillCategory.Melee, 2, 
                Resources.Skills_SkillLoneWarriorDescr, 
                new IStatModifier[] 
                { 
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Parry, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Armor, ApplianceMode.BaseValue, 1),
                },
                new SkillIdentifier[] { HijackerMelee.Identifier, Armor.Identifier, ArmorUp.Identifier, Combo.Identifier, PerfectBlow.Identifier, EveryBlowAHit.Identifier, TakeAHit.Identifier },
                activationType: ActivationType.Active);
            ChainAttack = CreateAndAddSkill(nameof(ChainAttack), Resources.Skills_SkillChainAttack, SkillCategory.Melee, 2, 
                Resources.Skills_SkillChainAttackDescr, null, new SkillIdentifier[] { DisarmMelee.Identifier },
                activationType: ActivationType.Active, staminaCost: 1);
            ShieldBreaker = CreateAndAddSkill(nameof(ShieldBreaker), Resources.Skills_SkillShieldBreaker, SkillCategory.Melee, 2, 
                Resources.Skills_SkillShieldBreakerDescr, null, new SkillIdentifier[] { HeavyFighting.Identifier },
                activationType: ActivationType.Active);
            BladeFan = CreateAndAddSkill(nameof(BladeFan), Resources.Skills_SkillBladeFan, SkillCategory.Melee, 3, 
                Resources.Skills_SkillBladeFanDescr, null, new SkillIdentifier[] { Riposte.Identifier },
                activationType: ActivationType.Active, staminaCost: 1, energyCost: 1);

            #endregion Melee

            #region Ranged
            // Checkpoint 0
            LightShot = CreateAndAddSkill(nameof(LightShot), Resources.Skills_SkillLightShot, SkillCategory.Ranged, 1, Resources.Skills_SkillLightShotDescr,
                new IStatModifier[]
                {
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2.0),
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 0.5, CalculatorBonusType.Multiplicative)
                }, activationType: ActivationType.Active, staminaCost: 1);
            Quickdraw = CreateAndAddSkill(nameof(Quickdraw), Resources.Skills_SkillQuickdraw, SkillCategory.Ranged, 1, Resources.Skills_SkillQuickdrawDescr, null,
                activationType: ActivationType.Active, staminaCost:1);
            SkilledWithThrowingWeapons = CreateAndAddSkill(nameof(SkilledWithThrowingWeapons), Resources.Skills_SkillSkilledWithThrowingWeapons, SkillCategory.Ranged, 1, 
                Resources.Skills_SkillSkilledWithThrowingWeaponsDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1) });
            BetterThanThrowing = CreateAndAddSkill(nameof(BetterThanThrowing), Resources.Skills_SkillBetterThanThrowing, SkillCategory.Ranged, 1, Resources.Skills_SkillBetterThanThrowingDescr);
            CalmAiming = CreateAndAddSkill(nameof(CalmAiming), Resources.Skills_SkillCalmAiming, SkillCategory.Ranged, 2,
                Resources.Skills_SkillCalmAimingDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2) }, 
                new SkillIdentifier[]{ LightShot.Identifier }, activationType: ActivationType.Active);
            DisarmRanged = CreateAndAddSkill(nameof(DisarmRanged), Resources.Skills_SkillDisarmRanged, SkillCategory.Ranged, 1,
                Resources.Skills_SkillDisarmRangedDescr, null, new SkillIdentifier[]{ Quickdraw.Identifier },
                activationType: ActivationType.Active, staminaCost: 2);
            DualThrow = CreateAndAddSkill(nameof(DualThrow), Resources.Skills_SkillDualThrow, SkillCategory.Ranged, 2,
                Resources.Skills_SkillDualThrowDescr, null, new SkillIdentifier[]{ SkilledWithThrowingWeapons.Identifier });
            SlingshotMarksman = CreateAndAddSkill(nameof(SlingshotMarksman), Resources.Skills_SkillSlingshotMarksman, SkillCategory.Ranged, 2,
                Resources.Skills_SkillSlingshotMarksmanDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6, 2) },
                new SkillIdentifier[]{ BetterThanThrowing.Identifier });

            // Checkpoint 1
            ShootFromTheSaddle = CreateAndAddSkill(nameof(ShootFromTheSaddle), Resources.Skills_SkillShootFromTheSaddle, SkillCategory.Ranged, 3, 
                Resources.Skills_SkillShootFromTheSaddleDescr, null, new SkillIdentifier[]{ CalmAiming.Identifier, DisarmRanged.Identifier, DualThrow.Identifier, SlingshotMarksman.Identifier });
            BuildArrows = CreateAndAddSkill(nameof(BuildArrows), Resources.Skills_SkillBuildArrows, SkillCategory.Ranged, 1, 
                Resources.Skills_SkillBuildArrowsDescr, null, new SkillIdentifier[]{ CalmAiming.Identifier, DisarmRanged.Identifier, DualThrow.Identifier, SlingshotMarksman.Identifier });
            PreciseThrow = CreateAndAddSkill(nameof(PreciseThrow), Resources.Skills_SkillPreciseThrow, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillPreciseThrowDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -4) },
                new SkillIdentifier[]{ CalmAiming.Identifier, DisarmRanged.Identifier, DualThrow.Identifier, SlingshotMarksman.Identifier }, 
                activationType: ActivationType.Active, staminaCost: 1);
            AimedAttackRanged = CreateAndAddSkill(nameof(AimedAttackRanged), Resources.Skills_SkillAimedAttackRanged, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillAimedAttackRangedDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -5) },
                new SkillIdentifier[]{ CalmAiming.Identifier, DisarmRanged.Identifier, DualThrow.Identifier, SlingshotMarksman.Identifier },
                activationType: ActivationType.Active, staminaCost: 2);
            AccurateRanged = CreateAndAddSkill(nameof(AccurateRanged), Resources.Skills_SkillAccurateRanged, SkillCategory.Ranged, 1, 
                Resources.Skills_SkillAccurateRangedDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1) },
                new SkillIdentifier[]{ CalmAiming.Identifier, DisarmRanged.Identifier, DualThrow.Identifier, SlingshotMarksman.Identifier });
            HijackerRanged = CreateAndAddSkill(nameof(HijackerRanged), Resources.Skills_SkillHijackerRanged, SkillCategory.Ranged, 1, 
                Resources.Skills_SkillHijackerRangedDescr, null, new SkillIdentifier[]{ ShootFromTheSaddle.Identifier });
            BowMaking = CreateAndAddSkill(nameof(BowMaking), Resources.Skills_SkillBowMaking, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillBowMakingDescr, null, new SkillIdentifier[]{ BuildArrows.Identifier });
            StrongThrow = CreateAndAddSkill(nameof(StrongThrow), Resources.Skills_SkillStrongThrow, SkillCategory.Ranged, 1, 
                Resources.Skills_SkillStrongThrowDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, Dice.D6) },
                new SkillIdentifier[]{ PreciseThrow.Identifier }, activationType: ActivationType.Active);
            Headshot = CreateAndAddSkill(nameof(Headshot), Resources.Skills_SkillHeadshot, SkillCategory.Ranged, 3, 
                Resources.Skills_SkillHeadshotDescr, null, new SkillIdentifier[]{ AimedAttackRanged.Identifier });
            BackLine = CreateAndAddSkill(nameof(BackLine), Resources.Skills_SkillBackLine, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillBackLineDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 1) },
                new SkillIdentifier[]{ AccurateRanged.Identifier });
            RoutinedWithThrowingWeapons = CreateAndAddSkill(nameof(RoutinedWithThrowingWeapons), Resources.Skills_SkillRoutinedWithThrowingWeapons, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillRoutinedWithThrowingWeaponsDescr,
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2) },
                new SkillIdentifier[]{ StrongThrow.Identifier });
            ProfessionalSlingshotMarksman = CreateAndAddSkill(nameof(ProfessionalSlingshotMarksman), Resources.Skills_SkillProfessionalSlingshotMarksman, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillProfessionalSlingshotMarksmanDescr, 
                new IStatModifier[]
                { 
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2),
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, Dice.D6, 2)
                }, 
                new SkillIdentifier[]{ AccurateRanged.Identifier });
            PerfectShot = CreateAndAddSkill(nameof(PerfectShot), Resources.Skills_SkillPerfectShot, SkillCategory.Ranged, 4, 
                Resources.Skills_SkillPerfectShotDescr, null, new SkillIdentifier[]{ BackLine.Identifier }, activationType: ActivationType.Active,
                staminaCost: 5);

            // Checkpoint 2
            SurpriseAttack = CreateAndAddSkill(nameof(SurpriseAttack), Resources.Skills_SkillSurpriseAttack, SkillCategory.Ranged, 3, 
                Resources.Skills_SkillSurpriseAttackDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, Dice.D4) },
                new SkillIdentifier[]{ HijackerRanged.Identifier, BowMaking.Identifier, RoutinedWithThrowingWeapons.Identifier, Headshot.Identifier, ProfessionalSlingshotMarksman.Identifier, PerfectShot.Identifier }, 
                activationType: ActivationType.Active);
            NailDown = CreateAndAddSkill(nameof(NailDown), Resources.Skills_SkillNailDown, SkillCategory.Ranged, 1, 
                Resources.Skills_SkillNailDownDescr, null,
                new SkillIdentifier[] { HijackerRanged.Identifier, BowMaking.Identifier, RoutinedWithThrowingWeapons.Identifier, Headshot.Identifier, ProfessionalSlingshotMarksman.Identifier, PerfectShot.Identifier },
                activationType: ActivationType.Active);
            CurvedShot = CreateAndAddSkill(nameof(CurvedShot), Resources.Skills_SkillCurvedShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillCurvedShotDescr, null,
                new SkillIdentifier[] { HijackerRanged.Identifier, BowMaking.Identifier, RoutinedWithThrowingWeapons.Identifier, Headshot.Identifier, ProfessionalSlingshotMarksman.Identifier, PerfectShot.Identifier },
                activationType: ActivationType.Active);
            QuickAim = CreateAndAddSkill(nameof(QuickAim), Resources.Skills_SkillQuickAim, SkillCategory.Ranged, 1, 
                Resources.Skills_SkillQuickAimDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -3) },
                new SkillIdentifier[] { HijackerRanged.Identifier, BowMaking.Identifier, RoutinedWithThrowingWeapons.Identifier, Headshot.Identifier, ProfessionalSlingshotMarksman.Identifier, PerfectShot.Identifier },
                activationType: ActivationType.Active);
            StrongArrows = CreateAndAddSkill(nameof(StrongArrows), Resources.Skills_SkillStrongArrows, SkillCategory.Ranged, 1, 
                Resources.Skills_SkillStrongArrowsDescr, null,
                new SkillIdentifier[] { HijackerRanged.Identifier, BowMaking.Identifier, RoutinedWithThrowingWeapons.Identifier, Headshot.Identifier, ProfessionalSlingshotMarksman.Identifier, PerfectShot.Identifier });
            PiercingArrow = CreateAndAddSkill(nameof(PiercingArrow), Resources.Skills_SkillPiercingArrow, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillPiercingArrowDescr, null, new SkillIdentifier[]{ NailDown.Identifier }, activationType: ActivationType.Active);
            LuckyShot = CreateAndAddSkill(nameof(LuckyShot), Resources.Skills_SkillLuckyShot, SkillCategory.Ranged, 3, 
                Resources.Skills_SkillLuckyShotDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, -6) },
                new SkillIdentifier[]{ CurvedShot.Identifier }, activationType: ActivationType.Active);
            DoubleShot = CreateAndAddSkill(nameof(DoubleShot), Resources.Skills_SkillDoubleShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillDoubleShotDescr, null, new SkillIdentifier[]{ QuickAim.Identifier }, activationType: ActivationType.Active, usesPerBattle: 1);
            MasterfulArcher = CreateAndAddSkill(nameof(MasterfulArcher), Resources.Skills_SkillMasterfulArcher, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillMasterfulArcherDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2) },
                new SkillIdentifier[]{ StrongArrows.Identifier });
            Magazine = CreateAndAddSkill(nameof(Magazine), Resources.Skills_SkillMagazine, SkillCategory.Ranged, 3, 
                Resources.Skills_SkillMagazineDescr, null, new SkillIdentifier[]{ DoubleShot.Identifier }, activationType: ActivationType.Active);

            // Checkpoint 3
            Oneshot = CreateAndAddSkill(nameof(Oneshot), Resources.Skills_SkillOneshot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillOneshotDescr, 
                new IStatModifier[] { new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.EndValue, 2, CalculatorBonusType.Multiplicative) },
                new SkillIdentifier[]{ SurpriseAttack.Identifier, PiercingArrow.Identifier, LuckyShot.Identifier, Magazine.Identifier, MasterfulArcher.Identifier },
                activationType: ActivationType.Active);
            LastShot = CreateAndAddSkill(nameof(LastShot), Resources.Skills_SkillLastShot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillLastShotDescr, 
                new IStatModifier[] 
                { 
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, Dice.D6),
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, FileIO.LoadedCharacter.Pearls.Wood * 2.5)
                },
                new SkillIdentifier[] { SurpriseAttack.Identifier, PiercingArrow.Identifier, LuckyShot.Identifier, Magazine.Identifier, MasterfulArcher.Identifier },
                activationType: ActivationType.Active);
            HuntersMark = CreateAndAddSkill(nameof(HuntersMark), Resources.Skills_SkillHuntersMark, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillHuntersMarkDescr, null,
                new SkillIdentifier[] { SurpriseAttack.Identifier, PiercingArrow.Identifier, LuckyShot.Identifier, Magazine.Identifier, MasterfulArcher.Identifier },
                activationType: ActivationType.Active);
            Readiness = CreateAndAddSkill(nameof(Readiness), Resources.Skills_SkillReadiness, SkillCategory.Ranged, 3, 
                Resources.Skills_SkillReadinessDescr, null,
                new SkillIdentifier[] { SurpriseAttack.Identifier, PiercingArrow.Identifier, LuckyShot.Identifier, Magazine.Identifier, MasterfulArcher.Identifier },
                activationType: ActivationType.Active);
            MasterOfThrowingWeapons = CreateAndAddSkill(nameof(MasterOfThrowingWeapons), Resources.Skills_SkillMasterOfThrowingWeapons, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillMasterOfThrowingWeaponsDescr, 
                new IStatModifier[] 
                { 
                    new CalculatorStatModifier(CalculatorValueType.Damage, ApplianceMode.BaseValue, 1),
                    new CalculatorStatModifier(CalculatorValueType.Hit, ApplianceMode.BaseValue, 2)
                },
                new SkillIdentifier[] { SurpriseAttack.Identifier, PiercingArrow.Identifier, LuckyShot.Identifier, Magazine.Identifier, MasterfulArcher.Identifier });
            Trueshot = CreateAndAddSkill(nameof(Trueshot), Resources.Skills_SkillTrueshot, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillTrueshotDescr, null, new SkillIdentifier[]{ Oneshot.Identifier },
                activationType: ActivationType.Active);
            Return = CreateAndAddSkill(nameof(Return), Resources.Skills_SkillReturn, SkillCategory.Ranged, 2, 
                Resources.Skills_SkillReturnDescr, null, new SkillIdentifier[]{ MasterOfThrowingWeapons.Identifier },
                activationType: ActivationType.Active);

            #endregion Ranged
            #endregion Skill Definitions
        }

        public int GetSkillIndex(SkillIdentifier identifier) => Registry.Keys.ToList().IndexOf(identifier);

        public Skill? GetSkillFromStatModifier(IStatModifier statModifier) => Registry.FirstOrDefault(entry => entry.Value.StatModifiers!.Contains(statModifier)).Value;

        private Skill CreateAndAddSkill(string name, string displayName, SkillCategory skillCategory, int maxSkillPoints, 
            string description, IStatModifier[]? skillModifiers = null, SkillIdentifier[]? skillDependencies = null,
            ActivationType activationType = ActivationType.Passive, int energyCost = 0, int staminaCost = 0, int usesPerBattle = -1,
            int skillTreeCheckpoint = 0) 
        {
            var identifier = new SkillIdentifier
            {
                SkillCategory = skillCategory,
                Name = name
            };
            var skill = new Skill(identifier, displayName, maxSkillPoints, description, skillModifiers, skillDependencies,
                activationType, energyCost, staminaCost, usesPerBattle , skillTreeCheckpoint);
            return AddSkill(identifier, skill);
        }

        private Skill AddSkill(SkillIdentifier identifier, Skill skill)
        {
            Registry.Add(identifier, skill);
            return skill;
        }
    }
}