using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Debug;
using PnP_Organizer.IO;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace PnP_Organizer.ViewModels
{
    public partial class OverviewViewModel : ObservableObject
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
        private int _currentEnergy = 0;
        [ObservableProperty]
        private int _maxEnergy = 0;
        [ObservableProperty]
        private int _currentStamina = 0;
        [ObservableProperty]
        private int _maxStamina = 0;
        [ObservableProperty]
        private int _initiative = 0;

        private bool _isLoading = false;

        public OverviewViewModel()
        {
            PropertyChanged += OnOverviewPropertyChanged;
        }

        private void OnOverviewPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyInfo overviewProperty = GetType().GetProperty(e.PropertyName!)!;
            if (e.PropertyName is nameof(Strength) or nameof(Constitution) or nameof(Dexterity) or nameof(Wisdom) or nameof(Intelligence) or nameof(Charisma))
            {
                UpdateAttributeBonus(overviewProperty);
                UpdateCharacterStats();
            }
            if(e.PropertyName is nameof(CharacterImage) && CharacterImage?.UriSource != null)
                _characterImageFileExt = Path.GetExtension(CharacterImage.UriSource.AbsolutePath);

            if (!_isLoading)
            {
                SaveCharacterPearls();
                SaveCharacterAttributes();
            }

            if (e.PropertyName == nameof(MaxHealth) && CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;

            if (e.PropertyName == nameof(MaxEnergy) && CurrentEnergy > MaxEnergy)
                CurrentEnergy = MaxEnergy;

            if (e.PropertyName == nameof(MaxStamina) && CurrentStamina > MaxStamina)
                CurrentStamina = MaxStamina;

            FileIO.IsCharacterSaved = false;
        }

        private void UpdateAttributeBonus(PropertyInfo attributeProperty)
        {
            PropertyInfo? attributeBonusProperty = GetType().GetProperty($"{attributeProperty.Name}Bonus");
            if (attributeBonusProperty != null)
                attributeBonusProperty.SetValue(this, Utils.GetAttributeBonus((int)attributeProperty.GetValue(this)!));
        }

        private void UpdateCharacterStats()
        {
            MaxHealth = Strength + Constitution;
            MaxEnergy = (int)Math.Round((Constitution + Charisma + Intelligence) / 3.0);
            Initiative = Constitution + Dexterity;
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

            CurrentHealth = FileIO.LoadedCharacter.CurrentHealth;
            CurrentEnergy = FileIO.LoadedCharacter.CurrentEnergy;
            CurrentStamina = FileIO.LoadedCharacter.CurrentStamina;

            LoadCharacterAttributes();
            LoadCharacterPearls();

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
    }
}
