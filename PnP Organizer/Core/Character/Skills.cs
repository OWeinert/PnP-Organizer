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

        // TODO Skills: implement and/or add every type of skill bonus
        private Skills()
        {
            SkillsList = new();

            #region Skill Definitions
            #region Character
            // Checkpoint 0
            Sneaking = CreateSkill(Resources.Skills_SkillSneaking, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            VirtuallyInvisible = CreateSkill(Resources.Skills_SkillVirtuallyInvisible, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Intimidate = CreateSkill(Resources.Skills_SkillIntimidate, SkillCategory.Character, 2, SkillUsableType.Skilled, "");
            Flirting = CreateSkill(Resources.Skills_SkillFlirting, SkillCategory.Character, 2, SkillUsableType.Skilled, "");
            NatureStudy = CreateSkill(Resources.Skills_SkillNatureStudy, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Theft = CreateSkill(Resources.Skills_SkillTheft, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Lockpicking = CreateSkill(Resources.Skills_SkillLockpicking, SkillCategory.Character, 1, SkillUsableType.Maxed, "");
            Counterfeiting = CreateSkill(Resources.Skills_SkillCounterfeiting, SkillCategory.Character, 1, SkillUsableType.Maxed, "");
            KnowledgeOfPeople = CreateSkill(Resources.Skills_SkillKnowledgeOfPeople, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            ActorByBirth = CreateSkill(Resources.Skills_SkillActorByBirth, SkillCategory.Character, 3, SkillUsableType.Maxed, "");
            Tracking = CreateSkill(Resources.Skills_SkillTracking, SkillCategory.Character, 1, SkillUsableType.Maxed, "");
            PoisonKnowledge = CreateSkill(Resources.Skills_SkillPoisonKnowledge, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Gambling = CreateSkill(Resources.Skills_SkillGambling, SkillCategory.Character, 1, SkillUsableType.Maxed, "");
            SkilledLier = CreateSkill(Resources.Skills_SkillSkilledLier, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            LieDetector = CreateSkill(Resources.Skills_SkillLieDetector, SkillCategory.Character, 2, SkillUsableType.Maxed, ""); ;
            SkilledSpeaker = CreateSkill(Resources.Skills_SkillSkilledSpeaker, SkillCategory.Character, 3, SkillUsableType.Maxed, "");

            // Checkpoint 1
            Climbing = CreateSkill(Resources.Skills_SkillClimbing, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Teacher = CreateSkill(Resources.Skills_SkillTeacher, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Plunge = CreateSkill(Resources.Skills_SkillPlunge, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            HardToKill = CreateSkill(Resources.Skills_SkillHardToKill, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Sympathic = CreateSkill(Resources.Skills_SkillSympathic, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Alertness = CreateSkill(Resources.Skills_SkillAlertness, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            RescueIsNear = CreateSkill(Resources.Skills_SkillRescueIsNear, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Etiquette = CreateSkill(Resources.Skills_SkillEtiquette, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Trading = CreateSkill(Resources.Skills_SkillTrading, SkillCategory.Character, 2, SkillUsableType.Maxed, "");

            // Checkpoint 2
            Perserverence = CreateSkill(Resources.Skills_SkillPerseverence, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Encouragement = CreateSkill(Resources.Skills_SkillEncouragement, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            EnergyBoost = CreateSkill(Resources.Skills_SkillEnergyBoost, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Momentum = CreateSkill(Resources.Skills_SkillMomentum, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            CommandTone = CreateSkill(Resources.Skills_SkillCommandTone, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Luck = CreateSkill(Resources.Skills_SkillLuck, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Healing = CreateSkill(Resources.Skills_SkillHealing, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            LastBreath = CreateSkill(Resources.Skills_SkillLastBreath, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            FutureMarket = CreateSkill(Resources.Skills_SkillFutureMarket, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            Avenger = CreateSkill(Resources.Skills_SkillAvenger, SkillCategory.Character, 2, SkillUsableType.Maxed, "");
            #endregion Character

            #region Melee
            // Checkpoint 0
            LightBlow = CreateSkill(Resources.Skills_SkillLightBlow, SkillCategory.Melee, 1, SkillUsableType.Maxed, "")
                .AddDamageModifier(damage => damage / 2, ApplianceMode.EndValue);
            Smithing = CreateSkill(Resources.Skills_SkillSmithing, SkillCategory.Melee, 1, SkillUsableType.Maxed, "");
            RunOver = CreateSkill(Resources.Skills_SkillRunOver, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            AimedAttackMelee = CreateSkill(Resources.Skills_SkillAimedAttackMelee, SkillCategory.Melee, 2, SkillUsableType.Maxed, "")
                .AddSkillDependencies(new string[] { LightBlow.Name }, () => true);
            WeaponsAndArmor = CreateSkill(Resources.Skills_SkillWeaponsAndArmor, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Kick = CreateSkill(Resources.Skills_SkillKick, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Taunt = CreateSkill(Resources.Skills_SkillTaunt, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            ArmorBreaker = CreateSkill(Resources.Skills_SkillArmorBreaker, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Assassinate = CreateSkill(Resources.Skills_SkillAssassinate, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            JumpAttack = CreateSkill(Resources.Skills_SkillJumpAttack, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");

            // Checkpoint 1
            OneHandedCombat = CreateSkill(Resources.Skills_SkillOneHandedFighting, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            SecondHand = CreateSkill(Resources.Skills_SkillSecondHand, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Shield = CreateSkill(Resources.Skills_SkillShield, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            DefensiveFighting = CreateSkill(Resources.Skills_SkillDefensiveFighting, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Nimble = CreateSkill(Resources.Skills_SkillNimble, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            AggressiveCombat = CreateSkill(Resources.Skills_SkillAgressiveFighting, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Fencing = CreateSkill(Resources.Skills_SkillFencing, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            FullDamage = CreateSkill(Resources.Skills_SkillFullDamage, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            ShieldBash = CreateSkill(Resources.Skills_SkillShieldBash, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Parade = CreateSkill(Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            ThereAndAway = CreateSkill(Resources.Skills_SkillThereAndAway, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            RecklessAttack = CreateSkill(Resources.Skills_SkillRecklessAttack, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            DuplexFerrum = CreateSkill(Resources.Skills_SkillDuplexFerrum, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            SomethingWithShield = CreateSkill(Resources.Skills_SkillSomethingWithShield, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            QuickParade = CreateSkill(Resources.Skills_SkillQuickParade, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            SkillfulRetreat = CreateSkill(Resources.Skills_SkillSkillfulRetreat, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Feint = CreateSkill(Resources.Skills_SkillFeint, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            RoundHouseAttack = CreateSkill(Resources.Skills_SkillRoundHouseAttack, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            HeavyParade = CreateSkill(Resources.Skills_SkillHeavyParade, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            PerfectBlock = CreateSkill(Resources.Skills_SkillPerfectBlock, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            DevastatingAttack = CreateSkill(Resources.Skills_SkillDevastatingAttack, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");

            // Checkpoint 2
            Cavallery = CreateSkill(Resources.Skills_SkillCavallery, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            DefensiveStance = CreateSkill(Resources.Skills_SkillDefensiveStance, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            ArmorUp = CreateSkill(Resources.Skills_SkillArmorUp, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            AccurateMelee = CreateSkill(Resources.Skills_SkillAccurateMelee, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            RecognizeStyle = CreateSkill(Resources.Skills_SkillRecognizeStyle, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            CripplingBlow = CreateSkill(Resources.Skills_SkillCripplingBlow, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            HijackerMelee = CreateSkill(Resources.Skills_SkillHijackerMelee, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Armor = CreateSkill(Resources.Skills_SkillArmor, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Combo = CreateSkill(Resources.Skills_SkillCombo, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            PerfectBlow = CreateSkill(Resources.Skills_SkillPerfectBlow, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            EveryBlowAHit = CreateSkill(Resources.Skills_SkillEveryBlowAHit, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            TakeAHit = CreateSkill(Resources.Skills_SkillTakeAHit, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");

            // Checkpoint 3
            AttackOfOpportunity = CreateSkill(Resources.Skills_SkillAttackOfOpportunity, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            DisarmMelee = CreateSkill(Resources.Skills_SkillDisarmMelee, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            KillingSpree = CreateSkill(Resources.Skills_SkillKillingSpree, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            HeavyFighting = CreateSkill(Resources.Skills_SkillHeavyFighting, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            Riposte = CreateSkill(Resources.Skills_SkillRiposte, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            LoneWarrior = CreateSkill(Resources.Skills_SkillLoneWarrior, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            ChainAttack = CreateSkill(Resources.Skills_SkillChainAttack, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            ShieldBreaker = CreateSkill(Resources.Skills_SkillShieldBreaker, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");
            BladeFan = CreateSkill(Resources.Skills_SkillBladeFan, SkillCategory.Melee, 2, SkillUsableType.Maxed, "");

            #endregion Melee

            #region Ranged
            // Checkpoint 0
            LightShot = CreateSkill(Resources.Skills_SkillLightShot, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            Quickdraw = CreateSkill(Resources.Skills_SkillQuickdraw, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            SkilledWithThrowingWeapons = CreateSkill(Resources.Skills_SkillSkilledWithThrowingWeapons, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            BetterThanThrowing = CreateSkill(Resources.Skills_SkillBetterThanThrowing, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            CalmAiming = CreateSkill(Resources.Skills_SkillCalmAiming, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            DisarmRanged = CreateSkill(Resources.Skills_SkillDisarmRanged, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            DualThrow = CreateSkill(Resources.Skills_SkillDualThrow, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            SlingshotMarksman = CreateSkill(Resources.Skills_SkillSlingshotMarksman, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");

            // Checkpoint 1
            ShootFromTheSaddle = CreateSkill(Resources.Skills_SkillShootFromTheSaddle, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            BuildArrows = CreateSkill(Resources.Skills_SkillBuildArrows, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            PreciseThrow = CreateSkill(Resources.Skills_SkillPreciseThrow, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            AimedAttackRanged = CreateSkill(Resources.Skills_SkillAimedAttackRanged, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            AccurateRanged = CreateSkill(Resources.Skills_SkillAccurateRanged, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            HijackerRanged = CreateSkill(Resources.Skills_SkillHijackerRanged, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            BowMaking = CreateSkill(Resources.Skills_SkillBowMaking, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            StrongThrow = CreateSkill(Resources.Skills_SkillStrongThrow, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            Headshot = CreateSkill(Resources.Skills_SkillHeadshot, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            BackLine = CreateSkill(Resources.Skills_SkillBackLine, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            RoutinedWithThrowingWeapons = CreateSkill(Resources.Skills_SkillRoutinedWithThrowingWeapons, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            ProfessionalSlingshotMarksman = CreateSkill(Resources.Skills_SkillProfessionalSlingshotMarksman, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            PerfectShot = CreateSkill(Resources.Skills_SkillPerfectShot, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");

            // Checkpoint 2
            SupriseAttack = CreateSkill(Resources.Skills_SkillSurpriseAttack, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            NailDown = CreateSkill(Resources.Skills_SkillNailDown, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            CurvedShot = CreateSkill(Resources.Skills_SkillCurvedShot, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            QuickAim = CreateSkill(Resources.Skills_SkillQuickAim, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            StrongArrows = CreateSkill(Resources.Skills_SkillStrongArrows, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            PiercingArrow = CreateSkill(Resources.Skills_SkillPiercingArrow, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            LuckyShot = CreateSkill(Resources.Skills_SkillLuckyShot, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            DoubleShot = CreateSkill(Resources.Skills_SkillDoubleShot, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            MasterfulArcher = CreateSkill(Resources.Skills_SkillMasterfulArcher, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            Magazine = CreateSkill(Resources.Skills_SkillMagazine, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");

            // Checkpoint 3
            Oneshot = CreateSkill(Resources.Skills_SkillOneshot, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            LastShot = CreateSkill(Resources.Skills_SkillLastShot, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            HuntersMark = CreateSkill(Resources.Skills_SkillHuntersMark, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            Readiness = CreateSkill(Resources.Skills_SkillReadiness, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            MasterOfThrowingWeapons = CreateSkill(Resources.Skills_SkillMasterOfThrowingWeapons, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            Trueshot = CreateSkill(Resources.Skills_SkillTrueshot, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");
            Return = CreateSkill(Resources.Skills_SkillReturn, SkillCategory.Ranged, 2, SkillUsableType.Maxed, "");

            #endregion Ranged
            #endregion Skill Definitions
        }

        private Skill CreateSkill(string name, SkillCategory skillCategory, int maxSkillPoints, SkillUsableType skillActivationType, string description = "") => AddSkill(new Skill(name, skillCategory, maxSkillPoints, skillActivationType, description));

        private Skill AddSkill(Skill skill)
        {
            SkillsList.Add(skill);
            return skill;
        }
    }
}
