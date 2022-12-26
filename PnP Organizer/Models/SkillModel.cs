using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PnP_Organizer.Core.Character;
using PnP_Organizer.IO;
using PnP_Organizer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PnP_Organizer.Models
{
    /// <summary>
    /// Data for Skills
    /// </summary>
    public partial class SkillModel : ObservableObject
    {
        private Skill? _skill;
        public Skill? Skill
        { 
            get => _skill;
            protected set => _skill = value;
        }

        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;
        [ObservableProperty]
        private string _tooltip = string.Empty;
        [ObservableProperty]
        private SkillCategory _skillCategory = SkillCategory.Character;
        [ObservableProperty]
        private string _localizedSkillCategory = string.Empty;
        [ObservableProperty]
        private string _skillCategoryTooltip = string.Empty;
        [ObservableProperty]
        private int _skillPoints = 0;
        [ObservableProperty]
        private int _maxSkillPoints = 0;
        [ObservableProperty]
        private bool _isActive = false;
        [ObservableProperty]
        private bool _isSkillable = false;
        [ObservableProperty]
        private Visibility _activeOverlayVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _skillableOverlayVisibility = Visibility.Hidden;

        public SkillModel(Skill skill)
        {
            Skill = skill;
            Name = skill.Name;
            Description = skill.Description;
            SkillCategory = skill.SkillCategory;
            SkillCategoryTooltip = $"Category: {SkillCategory}";
            SkillPoints = skill.SkillPoints;
            MaxSkillPoints = skill.MaxSkillPoints;
            LocalizedSkillCategory = SkillCategory switch
            {
                SkillCategory.Melee => Properties.Resources.Skills_Melee,
                SkillCategory.Ranged => Properties.Resources.Skills_Ranged,
                _ => Properties.Resources.Skills_Character,
            };

            Tooltip = Skill.CreateTooltip(Skill);

            UpdateVisuals();
            PropertyChanged += OnSkillPropertyChanged;
        }

        public void RaisePropertyChanged(string propertyName) => OnPropertyChanged(propertyName);

        protected virtual void UpdateIsActive() => IsActive = Skill!.IsActive();

        private void OnSkillPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName is nameof(IsSkillable))
            {
                if (!IsSkillable)
                    SkillPoints = 0;
            }
                
            if (e.PropertyName is not nameof(IsActive) or nameof(ActiveOverlayVisibility))
            {
                Skill!.Name = Name;
                Skill.Description = Description;
                Skill.SkillCategory = SkillCategory;
                Skill.SkillPoints = SkillPoints;

                UpdateVisuals();
                FileIO.IsCharacterSaved = false;
            }
        }

        [RelayCommand]
        private void IncreaseSkillPoints() {
            if(SkillPoints < MaxSkillPoints)
                SkillPoints++;
        }

        [RelayCommand]
        private void DecreaseSkillPoints()
        {
            if (SkillPoints > 0)
                SkillPoints--;
        }

        private void UpdateVisuals()
        {
            UpdateIsActive();
            UpdateOverlay();
        }

        private void UpdateOverlay()
        {
            ActiveOverlayVisibility = IsActive ? Visibility.Hidden : Visibility.Visible;
            SkillableOverlayVisibility = IsSkillable ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
