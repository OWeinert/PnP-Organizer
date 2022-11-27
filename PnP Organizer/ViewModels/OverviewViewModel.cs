using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.Character.StatModifiers;
using PnP_Organizer.IO;
using PnP_Organizer.Logging;
using PnP_Organizer.Models;
using PnP_Organizer.Views.Pages;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.ViewModels
{
    public partial class OverviewViewModel : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private string _characterName = string.Empty;

        [ObservableProperty]
        private string _characterAgeStr = string.Empty;

        [ObservableProperty]
        private string _characterHeightStr = string.Empty;

        [ObservableProperty]
        private BitmapImage? _characterImage;
        private string _characterImageFileExt = string.Empty;

        [ObservableProperty]
        private int _firePearls = 0;
        [ObservableProperty]
        private int _earthPearls = 0;
        [ObservableProperty]
        private int _metalPearls = 0;
        [ObservableProperty]
        private int _airPearls = 0;
        [ObservableProperty]
        private int _woodPearls = 0;
        [ObservableProperty]
        private int _waterPearls = 0;

        [ObservableProperty]
        private int _strength = 0;
        [ObservableProperty]
        private int _constitution = 0;
        [ObservableProperty]
        private int _dexterity = 0;
        [ObservableProperty]
        private int _wisdom = 0;
        [ObservableProperty]
        private int _intelligence = 0;
        [ObservableProperty]
        private int _charisma = 0;

        [ObservableProperty]
        private int _strengthBonus = 0;
        [ObservableProperty]
        private int _constitutionBonus = 0;
        [ObservableProperty]
        private int _dexterityBonus = 0;
        [ObservableProperty]
        private int _wisdomBonus = 0;
        [ObservableProperty]
        private int _intelligenceBonus = 0;
        [ObservableProperty]
        private int _charismaBonus = 0;

        [ObservableProperty]
        private int _currentHealth = 0;
        [ObservableProperty]
        private int _maxHealth = 0;
        [ObservableProperty]
        private int _maxHealthBonus = 0;
        [ObservableProperty]
        private int _maxHealthModifierBonus = 0;
        [ObservableProperty]
        private int _totalMaxHealth = 0;
        

        [ObservableProperty]
        private int _currentEnergy = 0;
        [ObservableProperty]
        private int _maxEnergy = 0;
        [ObservableProperty]
        private int _maxEnergyBonus = 0;
        [ObservableProperty]
        private int _maxEnergyModifierBonus = 0;
        [ObservableProperty]
        private int _totalMaxEnergy = 0;

        [ObservableProperty]
        private int _currentStamina = 0;
        [ObservableProperty]
        private int _maxStamina = 0;
        [ObservableProperty]
        private int _maxStaminaBonus = 0;
        [ObservableProperty]
        private int _maxStaminaModifierBonus = 0;
        [ObservableProperty]
        private int _totalMaxStamina = 0;

        [ObservableProperty]
        private int _initiative = 0;
        [ObservableProperty]
        private int _initiativeBonus = 0;
        [ObservableProperty]
        private int _initiativeModifierBonus = 0;
        [ObservableProperty]
        private int _totalInitiative = 0;

        private readonly IPageService _pageService;

        private bool _isLoading = false;

        public OverviewViewModel(IPageService pageService)
        {
            _pageService = pageService;
            PropertyChanged += OnOverviewPropertyChanged;
        }

        public void OnNavigatedTo()
        {
            ApplySkillBoni();
        }

        public void OnNavigatedFrom() { }

        private void OnOverviewPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyInfo overviewProperty = GetType().GetProperty(e.PropertyName!)!;
            if (e.PropertyName is nameof(Strength) or nameof(Constitution) or nameof(Dexterity) 
                or nameof(Wisdom) or nameof(Intelligence) or nameof(Charisma)
                || e.PropertyName!.Contains("Pearls"))
            {
                UpdateAttributeBonus(overviewProperty);
                CalculateCharacterStats();
            }
            if(e.PropertyName is nameof(CharacterImage) && CharacterImage?.UriSource != null)
                _characterImageFileExt = Path.GetExtension(CharacterImage.UriSource.AbsolutePath);
            if (!_isLoading)
            {
                SaveCharacterPearls();
                SaveCharacterAttributes();
            }
            if ((e.PropertyName.Contains("Max") || e.PropertyName.Contains("Modifier") || e.PropertyName.Contains(nameof(Initiative)))
                && !e.PropertyName.Contains("Total"))
            {
                TotalMaxHealth = MaxHealth + MaxHealthBonus + MaxHealthModifierBonus;
                TotalMaxEnergy = MaxEnergy + MaxEnergyBonus + MaxEnergyModifierBonus;
                TotalMaxStamina = MaxStamina + MaxStaminaBonus + MaxStaminaModifierBonus;
                TotalInitiative = Initiative + InitiativeBonus + InitiativeModifierBonus;
            }

            FileIO.IsCharacterSaved = false;
        }

        #region Stats Save / Load
        /// <summary>
        /// 
        /// </summary>
        public void SaveCharacterStats()
        {
            Logger.Log("Saving Character Stats...");

            FileIO.LoadedCharacter.Name = CharacterName;
            FileIO.LoadedCharacter.Age = GetCharacterAgeFromString(CharacterAgeStr);
            FileIO.LoadedCharacter.Height = GetCharacterHeightFromString(CharacterHeightStr);

            FileIO.LoadedCharacter.CurrentHealth = CurrentHealth;
            FileIO.LoadedCharacter.CurrentEnergy = CurrentEnergy;
            FileIO.LoadedCharacter.CurrentStamina = CurrentStamina;

            FileIO.LoadedCharacter.MaxHealthBonus = MaxHealthBonus;
            FileIO.LoadedCharacter.MaxEnergyBonus = MaxEnergyBonus;
            FileIO.LoadedCharacter.MaxStaminaBonus = MaxStaminaBonus;
            FileIO.LoadedCharacter.InitiativeBonus = InitiativeBonus;

            SaveCharacterAttributes();
            SaveCharacterPearls();
            SaveCharacterImage();

            Logger.Log("Character Stats saved successfully!");
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadCharacterStats()
        {
            _isLoading = true;
            Logger.Log("Loading Character Stats...");

            CharacterName = FileIO.LoadedCharacter.Name;
            CharacterAgeStr = $"{FileIO.LoadedCharacter.Age}";
            CharacterHeightStr = $"{FileIO.LoadedCharacter.Height}";           
            CharacterImage = Utils.BitmapImageFromBytes(FileIO.LoadedCharacter.CharacterImage);
            _characterImageFileExt = FileIO.LoadedCharacter.CharacterImageFileExt;

            MaxHealthBonus = FileIO.LoadedCharacter.MaxHealthBonus;
            MaxEnergyBonus = FileIO.LoadedCharacter.MaxEnergyBonus;
            MaxStaminaBonus = FileIO.LoadedCharacter.MaxStaminaBonus;
            InitiativeBonus = FileIO.LoadedCharacter.InitiativeBonus;

            LoadCharacterAttributes();
            LoadCharacterPearls();

            CurrentHealth = FileIO.LoadedCharacter.CurrentHealth;
            CurrentEnergy = FileIO.LoadedCharacter.CurrentEnergy;
            CurrentStamina = FileIO.LoadedCharacter.CurrentStamina;

            _isLoading = false;

            Logger.Log("Character Stats loaded successfully!");
        }

        private void SaveCharacterAttributes()
        {
            FileIO.LoadedCharacter.Attributes = new CharacterAttributes
            {
                Strength = Strength,
                Constitution = Constitution,
                Dexterity = Dexterity,
                Wisdom = Wisdom,
                Intelligence = Intelligence,
                Charisma = Charisma
            };
        }

        private void SaveCharacterPearls()
        {
            FileIO.LoadedCharacter.Pearls = new CharacterPearls
            {
                Earth = EarthPearls,
                Fire = FirePearls,
                Air = AirPearls,
                Metal = MetalPearls,
                Wood = WoodPearls,
                Water = WaterPearls
            };
        }
        private void SaveCharacterImage()
        {
            FileIO.LoadedCharacter.CharacterImage = Utils.BitmapImageToBytes(CharacterImage, _characterImageFileExt);
            FileIO.LoadedCharacter.CharacterImageFileExt = _characterImageFileExt;
        }

        private void LoadCharacterAttributes()
        {
            CharacterAttributes attributes = FileIO.LoadedCharacter.Attributes;
            Strength = attributes.Strength;
            Constitution = attributes.Constitution;
            Dexterity = attributes.Dexterity;
            Wisdom = attributes.Wisdom;
            Intelligence = attributes.Intelligence;
            Charisma = attributes.Charisma;
        }

        private void LoadCharacterPearls()
        {
            CharacterPearls pearls = FileIO.LoadedCharacter.Pearls;
            EarthPearls = pearls.Earth;
            FirePearls = pearls.Fire;
            AirPearls = pearls.Air;
            MetalPearls = pearls.Metal;
            WoodPearls = pearls.Wood;
            WaterPearls = pearls.Water;
        }
        #endregion Stats Save / Load

        private void UpdateAttributeBonus(PropertyInfo attributeProperty)
        {
            PropertyInfo? attributeBonusProperty = GetType().GetProperty($"{attributeProperty.Name}Bonus");
            attributeBonusProperty?.SetValue(this, Utils.GetAttributeBonus((int)attributeProperty.GetValue(this)!));
        }

        private void CalculateCharacterStats()
        {
            MaxHealth = Strength + Constitution;
            MaxEnergy = (int)Math.Round((Constitution + Charisma + Intelligence) / 3.0);
            MaxStamina = 15 + ConstitutionBonus + DexterityBonus;
            Initiative = Constitution + Dexterity + AirPearls;
        }

        private static int GetCharacterAgeFromString(string ageString)
        {
            if (!int.TryParse(ageString, out int characterAge))
                return -1;
            return characterAge;
        }

        private static float GetCharacterHeightFromString(string heightString)
        {
            if (!float.TryParse(heightString, NumberStyles.Any, CultureInfo.InvariantCulture, out float characterHeight))
                return -1.0f;
            characterHeight = MathF.Round(characterHeight, 2);
            return characterHeight;
        }

        private void ApplySkillBoni()
        {
            var skillsViewModel = _pageService.GetPage<SkillsPage>()!.ViewModel;
            var validSkillModels = skillsViewModel.SkillModels!
                .Where(skillModel => skillModel.Skill!.StatModifiers != null
                && skillModel.Skill.StatModifiers.Any(statModifier => statModifier is OverviewStatModifier))
                .Where(skillModel =>
                {
                    if (skillModel is RepeatableSkillModel rSM && (rSM.Repetition > 0 || rSM.SkillPoints > 0))
                        return true;
                    return skillModel.IsActive;
                }); // Filter out non-valid SkillModels

            // Reset Modifier Boni
            MaxHealthModifierBonus = 0;
            MaxEnergyModifierBonus = 0;
            MaxStaminaModifierBonus = 0;
            InitiativeModifierBonus = 0;

            foreach(var skillModel in validSkillModels)
            {
                var overviewStatModifiers = skillModel!.Skill!.StatModifiers!
                    .Where(statModifier => statModifier is OverviewStatModifier)
                    .Cast<OverviewStatModifier>();
                foreach (OverviewStatModifier overviewStatModifier in overviewStatModifiers)
                {
                    int multiplier = 1;
                    if (skillModel is RepeatableSkillModel rSM)
                        multiplier = rSM.Repetition;
                    overviewStatModifier.StatPropertyInfo.SetValue(this, (int)overviewStatModifier.StatPropertyInfo.GetValue(this)! + overviewStatModifier.Bonus * multiplier);
                }
            }
        }
    }
}
