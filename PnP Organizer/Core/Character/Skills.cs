using PnP_Organizer.Properties;
using System;
using System.Collections.ObjectModel;

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
        public readonly Skill Perserverence;
        public readonly Skill Encouragement;
        public readonly Skill EnergyBoost;
        public readonly Skill Momentum;
        public readonly Skill CommandTone;
        public readonly Skill Luck;
        public readonly Skill Healing;
        public readonly Skill LastBreath;
        public readonly Skill FutureMarket;
        public readonly Skill Avenger;
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
            Sneaking = CreateAndAddSkill(Resources.Skills_SkillSneaking, SkillCategory.Character, 2, "");
            Intimidate = CreateAndAddSkill(Resources.Skills_SkillIntimidate, SkillCategory.Character, 2, "");
            Flirting = CreateAndAddSkill(Resources.Skills_SkillFlirting, SkillCategory.Character, 2, "");
            NatureStudy = CreateAndAddSkill(Resources.Skills_SkillNatureStudy, SkillCategory.Character, 2, "");
            VirtuallyInvisible = CreateAndAddSkill(Resources.Skills_SkillVirtuallyInvisible, SkillCategory.Character, 2,
                "", new string[] { Sneaking.Name });
            Theft = CreateAndAddSkill(Resources.Skills_SkillTheft, SkillCategory.Character, 2, 
                "", new string[] { Sneaking.Name });
            Lockpicking = CreateAndAddSkill(Resources.Skills_SkillLockpicking, SkillCategory.Character, 1, 
                "", new string[] { Sneaking.Name });
            Counterfeiting = CreateAndAddSkill(Resources.Skills_SkillCounterfeiting, SkillCategory.Character, 1, 
                "", new string[] { Sneaking.Name });
            KnowledgeOfPeople = CreateAndAddSkill(Resources.Skills_SkillKnowledgeOfPeople, SkillCategory.Character, 2, 
                "", new string[] { Intimidate.Name, Flirting.Name });
            ActorByBirth = CreateAndAddSkill(Resources.Skills_SkillActorByBirth, SkillCategory.Character, 3, 
                "", new string[] { KnowledgeOfPeople.Name });
            Tracking = CreateAndAddSkill(Resources.Skills_SkillTracking, SkillCategory.Character, 1, 
                "", new string[] { NatureStudy.Name });
            PoisonKnowledge = CreateAndAddSkill(Resources.Skills_SkillPoisonKnowledge, SkillCategory.Character, 2, 
                "", new string[] { NatureStudy.Name });
            Gambling = CreateAndAddSkill(Resources.Skills_SkillGambling, SkillCategory.Character, 1, 
                "", new string[] { Theft.Name, Lockpicking.Name, Counterfeiting.Name });
            SkilledLier = CreateAndAddSkill(Resources.Skills_SkillSkilledLier, SkillCategory.Character, 2, 
                "", new string[] { Theft.Name, Lockpicking.Name, Counterfeiting.Name });
            LieDetector = CreateAndAddSkill(Resources.Skills_SkillLieDetector, SkillCategory.Character, 2, 
                "", new string[] { SkilledLier.Name, KnowledgeOfPeople.Name });
            SkilledSpeaker = CreateAndAddSkill(Resources.Skills_SkillSkilledSpeaker, SkillCategory.Character, 3, 
                "", new string[] { KnowledgeOfPeople.Name });

            // Checkpoint 1
            Climbing = CreateAndAddSkill(Resources.Skills_SkillClimbing, SkillCategory.Character, 2, 
                "", new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Teacher = CreateAndAddSkill(Resources.Skills_SkillTeacher, SkillCategory.Character, 2, 
                "", new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Plunge = CreateAndAddSkill(Resources.Skills_SkillPlunge, SkillCategory.Character, 2, 
                "", new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            HardToKill = CreateAndAddSkill(Resources.Skills_SkillHardToKill, SkillCategory.Character, 2, 
                "", new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Sympathic = CreateAndAddSkill(Resources.Skills_SkillSympathic, SkillCategory.Character, 2, 
                "", new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            Alertness = CreateAndAddSkill(Resources.Skills_SkillAlertness, SkillCategory.Character, 2, 
                "", new string[] { Gambling.Name, SkilledLier.Name, LieDetector.Name, SkilledSpeaker.Name, ActorByBirth.Name, Tracking.Name, PoisonKnowledge.Name });
            RescueIsNear = CreateAndAddSkill(Resources.Skills_SkillRescueIsNear, SkillCategory.Character, 2, 
                "", new string[] { Plunge.Name });
            Etiquette = CreateAndAddSkill(Resources.Skills_SkillEtiquette, SkillCategory.Character, 2, 
                "", new string[] { Sympathic.Name });
            Trading = CreateAndAddSkill(Resources.Skills_SkillTrading, SkillCategory.Character, 2, 
                "", new string[] { Sympathic.Name });

            // Checkpoint 2
            Perserverence = CreateAndAddSkill(Resources.Skills_SkillPerseverence, SkillCategory.Character, 2, 
                "", new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            Encouragement = CreateAndAddSkill(Resources.Skills_SkillEncouragement, SkillCategory.Character, 2, 
                "", new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            EnergyBoost = CreateAndAddSkill(Resources.Skills_SkillEnergyBoost, SkillCategory.Character, 2, 
                "", new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            Momentum = CreateAndAddSkill(Resources.Skills_SkillMomentum, SkillCategory.Character, 2, 
                "", new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            CommandTone = CreateAndAddSkill(Resources.Skills_SkillCommandTone, SkillCategory.Character, 2, 
                "", new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            Luck = CreateAndAddSkill(Resources.Skills_SkillLuck, SkillCategory.Character, 2, 
                "", new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            Healing = CreateAndAddSkill(Resources.Skills_SkillHealing, SkillCategory.Character, 2, 
                "", new string[] { Climbing.Name, Teacher.Name, RescueIsNear.Name, HardToKill.Name, Etiquette.Name, Trading.Name, Alertness.Name });
            LastBreath = CreateAndAddSkill(Resources.Skills_SkillLastBreath, SkillCategory.Character, 2, 
                "", new string[] { Perserverence.Name });
            FutureMarket = CreateAndAddSkill(Resources.Skills_SkillFutureMarket, SkillCategory.Character, 2,
                "", new string[] { Perserverence.Name });
            Avenger = CreateAndAddSkill(Resources.Skills_SkillAvenger, SkillCategory.Character, 2,
                "", new string[] { Momentum.Name });
            #endregion Character

            #region Melee
            // Checkpoint 0
            LightBlow = CreateAndAddSkill(Resources.Skills_SkillLightBlow, SkillCategory.Melee, 1, "")
                .AddDamageModifier(damage => damage / 2, ApplianceMode.EndValue);
            Smithing = CreateAndAddSkill(Resources.Skills_SkillSmithing, SkillCategory.Melee, 1, "");
            RunOver = CreateAndAddSkill(Resources.Skills_SkillRunOver, SkillCategory.Melee, 2, "");
            AimedAttackMelee = CreateAndAddSkill(Resources.Skills_SkillAimedAttackMelee, SkillCategory.Melee, 2, 
                "", new string[] { LightBlow.Name })
                .AddSkillDependencies(new string[] { LightBlow.Name });
            WeaponsAndArmor = CreateAndAddSkill(Resources.Skills_SkillWeaponsAndArmor, SkillCategory.Melee, 2, 
                "", new string[] { WeaponsAndArmor.Name });
            Kick = CreateAndAddSkill(Resources.Skills_SkillKick, SkillCategory.Melee, 2, 
                "", new string[] { RunOver.Name });
            Taunt = CreateAndAddSkill(Resources.Skills_SkillTaunt, SkillCategory.Melee, 2, 
                "", new string[] { RunOver.Name });
            ArmorBreaker = CreateAndAddSkill(Resources.Skills_SkillArmorBreaker, SkillCategory.Melee, 2, 
                "", new string[] { AimedAttackMelee.Name });
            Assassinate = CreateAndAddSkill(Resources.Skills_SkillAssassinate, SkillCategory.Melee, 2, 
                "", new string[] { AimedAttackMelee.Name });
            JumpAttack = CreateAndAddSkill(Resources.Skills_SkillJumpAttack, SkillCategory.Melee, 2, 
                "", new string[] { Kick.Name });

            // Checkpoint 1
            OneHandedCombat = CreateAndAddSkill(Resources.Skills_SkillOneHandedFighting, SkillCategory.Melee, 2, 
                "", new string[] { ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            SecondHand = CreateAndAddSkill(Resources.Skills_SkillSecondHand, SkillCategory.Melee, 2, 
                "", new string[] { ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            Shield = CreateAndAddSkill(Resources.Skills_SkillShield, SkillCategory.Melee, 2, 
                "", new string[] { ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            DefensiveFighting = CreateAndAddSkill(Resources.Skills_SkillDefensiveFighting, SkillCategory.Melee, 2, 
                "", new string[] { ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            Nimble = CreateAndAddSkill(Resources.Skills_SkillNimble, SkillCategory.Melee, 2, 
                "", new string[] { ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            AggressiveCombat = CreateAndAddSkill(Resources.Skills_SkillAgressiveFighting, SkillCategory.Melee, 2, 
                "", new string[] { ArmorBreaker.Name, Assassinate.Name, WeaponsAndArmor.Name, JumpAttack.Name, Taunt.Name });
            Fencing = CreateAndAddSkill(Resources.Skills_SkillFencing, SkillCategory.Melee, 2, 
                "", new string[] { OneHandedCombat.Name });
            FullDamage = CreateAndAddSkill(Resources.Skills_SkillFullDamage, SkillCategory.Melee, 2, 
                "", new string[] { SecondHand.Name });
            ShieldBash = CreateAndAddSkill(Resources.Skills_SkillShieldBash, SkillCategory.Melee, 2, 
                "", new string[] { ShieldBash.Name });
            Parade = CreateAndAddSkill(Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 2, 
                "", new string[] { Parade.Name });
            ThereAndAway = CreateAndAddSkill(Resources.Skills_SkillThereAndAway, SkillCategory.Melee, 2, 
                "", new string[] { Nimble.Name });
            RecklessAttack = CreateAndAddSkill(Resources.Skills_SkillRecklessAttack, SkillCategory.Melee, 2, 
                "", new string[] { RecklessAttack.Name });
            DuplexFerrum = CreateAndAddSkill(Resources.Skills_SkillDuplexFerrum, SkillCategory.Melee, 2, 
                "", new string[] { FullDamage.Name });
            SomethingWithShield = CreateAndAddSkill(Resources.Skills_SkillSomethingWithShield, SkillCategory.Melee, 2, 
                "", new string[] { ShieldBash.Name });
            QuickParade = CreateAndAddSkill(Resources.Skills_SkillQuickParade, SkillCategory.Melee, 2, 
                "", new string[] { Parade.Name });
            SkillfulRetreat = CreateAndAddSkill(Resources.Skills_SkillSkillfulRetreat, SkillCategory.Melee, 2, 
                "", new string[] { DefensiveFighting.Name, ThereAndAway.Name });
            Feint = CreateAndAddSkill(Resources.Skills_SkillFeint, SkillCategory.Melee, 2, 
                "", new string[] { Nimble.Name });
            RoundHouseAttack = CreateAndAddSkill(Resources.Skills_SkillRoundHouseAttack, SkillCategory.Melee, 2, 
                "", new string[] { RecklessAttack.Name });
            HeavyParade = CreateAndAddSkill(Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 2, 
                "", new string[] { QuickParade.Name });
            PerfectBlock = CreateAndAddSkill(Resources.Skills_SkillPerfectBlock, SkillCategory.Melee, 2, 
                "", new string[] { QuickParade.Name });
            DevastatingAttack = CreateAndAddSkill(Resources.Skills_SkillDevastatingAttack, SkillCategory.Melee, 2, 
                "", new string[] { RoundHouseAttack.Name });

            // Checkpoint 2
            Cavallery = CreateAndAddSkill(Resources.Skills_SkillCavallery, SkillCategory.Melee, 2, 
                "", new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            DefensiveStance = CreateAndAddSkill(Resources.Skills_SkillDefensiveStance, SkillCategory.Melee, 2,
                "", new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            ArmorUp = CreateAndAddSkill(Resources.Skills_SkillArmorUp, SkillCategory.Melee, 2,
                "", new string[] { DefensiveStance.Name });
            AccurateMelee = CreateAndAddSkill(Resources.Skills_SkillAccurateMelee, SkillCategory.Melee, 2, 
                "", new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            RecognizeStyle = CreateAndAddSkill(Resources.Skills_SkillRecognizeStyle, SkillCategory.Melee, 2,
                "", new string[] { Fencing.Name, DuplexFerrum.Name, HeavyParade.Name, PerfectBlock.Name, SkillfulRetreat.Name, Feint.Name, DevastatingAttack.Name });
            CripplingBlow = CreateAndAddSkill(Resources.Skills_SkillCripplingBlow, SkillCategory.Melee, 2,
                "", new string[] { RecognizeStyle.Name });
            HijackerMelee = CreateAndAddSkill(Resources.Skills_SkillHijackerMelee, SkillCategory.Melee, 2, 
                "", new string[] { Cavallery.Name });
            Armor = CreateAndAddSkill(Resources.Skills_SkillArmor, SkillCategory.Melee, 2, 
                "", new string[] { DefensiveStance.Name });
            Combo = CreateAndAddSkill(Resources.Skills_SkillCombo, SkillCategory.Melee, 2,
                "", new string[] { AccurateMelee.Name });
            PerfectBlow = CreateAndAddSkill(Resources.Skills_SkillPerfectBlow, SkillCategory.Melee, 2, 
                "", new string[] { AccurateMelee.Name });
            EveryBlowAHit = CreateAndAddSkill(Resources.Skills_SkillEveryBlowAHit, SkillCategory.Melee, 2, 
                "", new string[] { AccurateMelee.Name });
            TakeAHit = CreateAndAddSkill(Resources.Skills_SkillTakeAHit, SkillCategory.Melee, 2,
                "", new string[] { RecognizeStyle.Name });

            // Checkpoint 3
            AttackOfOpportunity = CreateAndAddSkill(Resources.Skills_SkillAttackOfOpportunity, SkillCategory.Melee, 2, 
                "", new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            DisarmMelee = CreateAndAddSkill(Resources.Skills_SkillDisarmMelee, SkillCategory.Melee, 2, 
                "", new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            KillingSpree = CreateAndAddSkill(Resources.Skills_SkillKillingSpree, SkillCategory.Melee, 2, 
                "", new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            HeavyFighting = CreateAndAddSkill(Resources.Skills_SkillHeavyFighting, SkillCategory.Melee, 2, 
                "", new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            Riposte = CreateAndAddSkill(Resources.Skills_SkillRiposte, SkillCategory.Melee, 2, 
                "", new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            LoneWarrior = CreateAndAddSkill(Resources.Skills_SkillLoneWarrior, SkillCategory.Melee, 2, 
                "", new string[] { HijackerMelee.Name, Armor.Name, ArmorUp.Name, Combo.Name, PerfectBlow.Name, EveryBlowAHit.Name, TakeAHit.Name });
            ChainAttack = CreateAndAddSkill(Resources.Skills_SkillChainAttack, SkillCategory.Melee, 2, 
                "", new string[] { DisarmMelee.Name });
            ShieldBreaker = CreateAndAddSkill(Resources.Skills_SkillShieldBreaker, SkillCategory.Melee, 2, 
                "", new string[] { HeavyFighting.Name });
            BladeFan = CreateAndAddSkill(Resources.Skills_SkillBladeFan, SkillCategory.Melee, 2, 
                "", new string[] { Riposte.Name });

            #endregion Melee

            #region Ranged
            // Checkpoint 0
            LightShot = CreateAndAddSkill(Resources.Skills_SkillLightShot, SkillCategory.Ranged, 2, "");
            Quickdraw = CreateAndAddSkill(Resources.Skills_SkillQuickdraw, SkillCategory.Ranged, 2, "");
            SkilledWithThrowingWeapons = CreateAndAddSkill(Resources.Skills_SkillSkilledWithThrowingWeapons, SkillCategory.Ranged, 2, "");
            BetterThanThrowing = CreateAndAddSkill(Resources.Skills_SkillBetterThanThrowing, SkillCategory.Ranged, 2, "");
            CalmAiming = CreateAndAddSkill(Resources.Skills_SkillCalmAiming, SkillCategory.Ranged, 2,
                "", new string[]{ LightShot.Name });
            DisarmRanged = CreateAndAddSkill(Resources.Skills_SkillDisarmRanged, SkillCategory.Ranged, 2,
                "", new string[]{ Quickdraw.Name });
            DualThrow = CreateAndAddSkill(Resources.Skills_SkillDualThrow, SkillCategory.Ranged, 2,
                "", new string[]{ SkilledWithThrowingWeapons.Name });
            SlingshotMarksman = CreateAndAddSkill(Resources.Skills_SkillSlingshotMarksman, SkillCategory.Ranged, 2,
                "", new string[]{ BetterThanThrowing.Name });

            // Checkpoint 1
            ShootFromTheSaddle = CreateAndAddSkill(Resources.Skills_SkillShootFromTheSaddle, SkillCategory.Ranged, 2, 
                "", new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            BuildArrows = CreateAndAddSkill(Resources.Skills_SkillBuildArrows, SkillCategory.Ranged, 2, 
                "", new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            PreciseThrow = CreateAndAddSkill(Resources.Skills_SkillPreciseThrow, SkillCategory.Ranged, 2, 
                "", new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            AimedAttackRanged = CreateAndAddSkill(Resources.Skills_SkillAimedAttackRanged, SkillCategory.Ranged, 2, 
                "", new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            AccurateRanged = CreateAndAddSkill(Resources.Skills_SkillAccurateRanged, SkillCategory.Ranged, 2, 
                "", new string[]{ CalmAiming.Name, DisarmRanged.Name, DualThrow.Name, SlingshotMarksman.Name });
            HijackerRanged = CreateAndAddSkill(Resources.Skills_SkillHijackerRanged, SkillCategory.Ranged, 2, 
                "", new string[]{ ShootFromTheSaddle.Name });
            BowMaking = CreateAndAddSkill(Resources.Skills_SkillBowMaking, SkillCategory.Ranged, 2, 
                "", new string[]{ BuildArrows.Name });
            StrongThrow = CreateAndAddSkill(Resources.Skills_SkillStrongThrow, SkillCategory.Ranged, 2, 
                "", new string[]{ PreciseThrow.Name });
            Headshot = CreateAndAddSkill(Resources.Skills_SkillHeadshot, SkillCategory.Ranged, 2, 
                "", new string[]{ AimedAttackRanged.Name });
            BackLine = CreateAndAddSkill(Resources.Skills_SkillBackLine, SkillCategory.Ranged, 2, 
                "", new string[]{ AccurateRanged.Name });
            RoutinedWithThrowingWeapons = CreateAndAddSkill(Resources.Skills_SkillRoutinedWithThrowingWeapons, SkillCategory.Ranged, 2, 
                "", new string[]{ StrongThrow.Name });
            ProfessionalSlingshotMarksman = CreateAndAddSkill(Resources.Skills_SkillProfessionalSlingshotMarksman, SkillCategory.Ranged, 2, 
                "", new string[]{ AccurateRanged.Name });
            PerfectShot = CreateAndAddSkill(Resources.Skills_SkillPerfectShot, SkillCategory.Ranged, 2, 
                "", new string[]{ BackLine.Name });

            // Checkpoint 2
            SurpriseAttack = CreateAndAddSkill(Resources.Skills_SkillSurpriseAttack, SkillCategory.Ranged, 2, 
                "", new string[]{ HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            NailDown = CreateAndAddSkill(Resources.Skills_SkillNailDown, SkillCategory.Ranged, 2, 
                "", new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            CurvedShot = CreateAndAddSkill(Resources.Skills_SkillCurvedShot, SkillCategory.Ranged, 2, 
                "", new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            QuickAim = CreateAndAddSkill(Resources.Skills_SkillQuickAim, SkillCategory.Ranged, 2, 
                "", new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            StrongArrows = CreateAndAddSkill(Resources.Skills_SkillStrongArrows, SkillCategory.Ranged, 2, 
                "", new string[] { HijackerRanged.Name, BowMaking.Name, RoutinedWithThrowingWeapons.Name, Headshot.Name, ProfessionalSlingshotMarksman.Name, PerfectShot.Name });
            PiercingArrow = CreateAndAddSkill(Resources.Skills_SkillPiercingArrow, SkillCategory.Ranged, 2, 
                "", new string[]{ NailDown.Name });
            LuckyShot = CreateAndAddSkill(Resources.Skills_SkillLuckyShot, SkillCategory.Ranged, 2, 
                "", new string[]{ CurvedShot.Name });
            DoubleShot = CreateAndAddSkill(Resources.Skills_SkillDoubleShot, SkillCategory.Ranged, 2, 
                "", new string[]{ QuickAim.Name });
            MasterfulArcher = CreateAndAddSkill(Resources.Skills_SkillMasterfulArcher, SkillCategory.Ranged, 2, 
                "", new string[]{ StrongArrows.Name });
            Magazine = CreateAndAddSkill(Resources.Skills_SkillMagazine, SkillCategory.Ranged, 2, 
                "", new string[]{ DoubleShot.Name });

            // Checkpoint 3
            Oneshot = CreateAndAddSkill(Resources.Skills_SkillOneshot, SkillCategory.Ranged, 2, 
                "", new string[]{ SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            LastShot = CreateAndAddSkill(Resources.Skills_SkillLastShot, SkillCategory.Ranged, 2, 
                "", new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            HuntersMark = CreateAndAddSkill(Resources.Skills_SkillHuntersMark, SkillCategory.Ranged, 2, 
                "", new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            Readiness = CreateAndAddSkill(Resources.Skills_SkillReadiness, SkillCategory.Ranged, 2, 
                "", new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            MasterOfThrowingWeapons = CreateAndAddSkill(Resources.Skills_SkillMasterOfThrowingWeapons, SkillCategory.Ranged, 2, 
                "", new string[] { SurpriseAttack.Name, PiercingArrow.Name, LuckyShot.Name, Magazine.Name, MasterfulArcher.Name });
            Trueshot = CreateAndAddSkill(Resources.Skills_SkillTrueshot, SkillCategory.Ranged, 2, 
                "", new string[]{ Oneshot.Name });
            Return = CreateAndAddSkill(Resources.Skills_SkillReturn, SkillCategory.Ranged, 2, 
                "", new string[]{ MasterOfThrowingWeapons.Name });

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
            string description = "", string[]? skillDependencies = null) 
        {
            var skill = new Skill(name, skillCategory, maxSkillPoints, description);
            if(skillDependencies != null)
                skill.AddSkillDependencies(skillDependencies);
            return AddSkill(skill);
        }

        private Skill AddSkill(Skill skill)
        {
            SkillsList.Add(skill);
            return skill;
        }
    }
}
