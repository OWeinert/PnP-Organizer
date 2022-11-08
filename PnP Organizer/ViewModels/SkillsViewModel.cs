using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Debug;
using PnP_Organizer.IO;
using PnP_Organizer.Models;
using PnP_Organizer.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PnP_Organizer.ViewModels
{
    public partial class SkillsViewModel : ObservableObject
    {
        public List<SkillTreeFilter>? TreeFilters { get; private set; }
        public List<SkillSkillableFilter>? SkillableFilters { get; private set; }

        [ObservableProperty]
        private ICollectionView? _skillModelsView;
        [ObservableProperty]
        private SkillTreeFilter _selectedTreeFilter;
        [ObservableProperty]
        private int _selectedTreeFilterIndex = 0;
        [ObservableProperty]
        private SkillSkillableFilter _selectedSkillableFilter;
        [ObservableProperty]
        private int _selectedSkillableFilterIndex = 0;
        [ObservableProperty]
        private ObservableCollection<SkillModel>? _skillModels;
        [ObservableProperty]
        private string _searchBoxText = string.Empty;
        [ObservableProperty]
        private int _usedSkillPoints = 0;

        private bool _isInitialized = false;

        public SkillsViewModel()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            PropertyChanged += SelectedTreeFilterIndexChanged;
            
            TreeFilters = new List<SkillTreeFilter>()
            {
                new SkillTreeFilter(Resources.Skills_All, null, (SolidColorBrush)Application.Current.Resources["TextFillColorPrimaryBrush"]),
                new SkillTreeFilter(Resources.Skills_Character, SkillCategory.Character, (SolidColorBrush)Application.Current.Resources["PaletteGreyBrush"]),
                new SkillTreeFilter(Resources.Skills_Melee, SkillCategory.Melee, (SolidColorBrush)Application.Current.Resources["PaletteBlueBrush"]),
                new SkillTreeFilter(Resources.Skills_Ranged, SkillCategory.Ranged, (SolidColorBrush)Application.Current.Resources["PaletteGreenBrush"])
            };
            SelectedTreeFilter = TreeFilters[0];

            SkillableFilters = new List<SkillSkillableFilter>()
            {
                new SkillSkillableFilter(Resources.Skills_All),
                new SkillSkillableFilter(Resources.Skills_Skilled, SkillableType.Skilled),
                new SkillSkillableFilter(Resources.Skills_Skillable, SkillableType.Skillable),
                new SkillSkillableFilter(Resources.Skills_Locked, SkillableType.NotSkillable)
            };
            SelectedSkillableFilter = SkillableFilters[0];
            
            InitializeSkillModels();

            SkillModelsView!.Filter += SkillModelsView_Filter;

            _isInitialized = true;
        }

        private void InitializeSkillModels()
        {
            SkillModels = new ObservableCollection<SkillModel>();
            SkillModels.CollectionChanged += SkillModels_CollectionChanged;
            foreach (Skill skill in Skills.Instance.SkillsList)
            {
                SkillModels.Add(new SkillModel(skill));
            }
            SkillModelsView = CollectionViewSource.GetDefaultView(SkillModels);

            //CreateSkillModelHierarchy();

            Logger.Log($"{SkillModels.Count} SkillModels for {Skills.Instance.SkillsList.Count} Skills initialized");
        }

        private void SkillModels_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(SkillModel skillModel in e.NewItems!)
                    {
                        CheckSkillModelSkillability(skillModel);
                        skillModel.PropertyChanged += new PropertyChangedEventHandler(SkillModel_PropertyChanged);
                    }
                    break;
            }
        }

        private void SkillModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            SkillModel? senderSkillModel = (SkillModel?)sender;
            if (senderSkillModel != null && e.PropertyName is nameof(senderSkillModel.SkillPoints))
            {
                foreach(SkillModel skillModel in SkillModels!)
                {
                    CheckSkillModelSkillability(skillModel);
                    UsedSkillPoints = SkillModels.Sum(skillModel => skillModel.SkillPoints);
                }
            }
            SkillModelsView?.Refresh();
        }

        private void CheckSkillModelSkillability(SkillModel skillModel)
        {
            List<string> dependedActiveSkills = SkillModels!
                .Where(dependendSkill => dependendSkill.IsActive && skillModel.Skill.DependendSkillNames.Contains(dependendSkill.Name))
                .ToList().ConvertAll(skillModel => skillModel.Name);
            skillModel.IsSkillable = (dependedActiveSkills.Any() || !skillModel.Skill.DependendSkillNames.Any()) && skillModel.Skill.IsSkillable();
        }

        private void SelectedTreeFilterIndexChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SelectedTreeFilterIndex))
                SelectedTreeFilter = TreeFilters![SelectedTreeFilterIndex];
            if (e.PropertyName == nameof(SelectedSkillableFilterIndex))
                SelectedSkillableFilter = SkillableFilters![SelectedSkillableFilterIndex];

            SkillModelsView?.Refresh();
        }

        private bool SkillModelsView_Filter(object item)
        {
            SkillModel skill = (SkillModel)item;

            // Filter by SearchBox text
            if((skill.Name.Contains(SearchBoxText) || string.IsNullOrEmpty(SearchBoxText)))
            {
                // Filter by Skillability
                bool skillability = SelectedSkillableFilter.SkillableType switch
                {
                    SkillableType.Skilled => skill.IsActive,
                    SkillableType.Skillable => !skill.IsActive && skill.IsSkillable,
                    SkillableType.NotSkillable => !skill.IsActive && !skill.IsSkillable,
                    _ => true
                };

                if (SelectedTreeFilterIndex == 0)
                    return skillability;
                // additionaly Filter by SkillCategory
                return skill.SkillCategory == SelectedTreeFilter.SkillCategory && skillability;
            }
            return false;
        }

        /*
        private void CreateSkillModelHierarchy()
        {
            foreach(SkillModel skillModel in SkillModels!)
            {
                SkillModel?[] children = SkillModels.SelectMany(skillModel => skillModel.Skill.DependendSkillNames, (skillModel, dependendSkillName) =>
                {
                    if (skillModel.Name == dependendSkillName)
                        return skillModel;
                    return null;
                }).ToArray();
                skillModel.Children?.AddRange(children.Where(skillModel => skillModel != null)!);
            }
        }
        */

        public void SaveCharacterSkills()
        {
            if (_isInitialized)
            {
                Logger.Log("Saving Skills...");

                List<SkillSaveData> skills = new();
                for(int i = 0; i < SkillModels!.Count; i++)
                {
                    SkillSaveData skillSaveData = new()
                    {
                        Index = i,
                        SkillPoints = SkillModels[i]!.SkillPoints
                    };
                    skills.Add(skillSaveData);
                }
                FileIO.LoadedCharacter.Skills = skills;

                Logger.Log("Skills saved successfully!");
            }  
        }

        public void LoadCharacterSkills()
        {
            if (_isInitialized)
            {
                Logger.Log("Loading Skills...");

                if (FileIO.LoadedCharacter.Skills.Count == 0)
                    FileIO.LoadedCharacter.InitSkillSaveData();

                SkillModels?.Clear();

                foreach (SkillSaveData skillSaveData in FileIO.LoadedCharacter.Skills!)
                {
                    Skill skill = Skills.Instance.SkillsList[skillSaveData.Index];
                    skill.SkillPoints = skillSaveData.SkillPoints;
                    SkillModels?.Add(new SkillModel(skill));
                }
                SkillModelsView = CollectionViewSource.GetDefaultView(SkillModels);

                //CreateSkillModelHierarchy();

                Logger.Log("Skills loaded successfully!");
            }
        }
    }

    public struct SkillTreeFilter
    {
        public string Name { get; set; }
        public SkillCategory? SkillCategory { get; set; }
        public SolidColorBrush Color { get; set; }

        public SkillTreeFilter(string name, SkillCategory? skillCategory, SolidColorBrush color)
        {
            Name = name;
            SkillCategory = skillCategory;
            Color = color;
        }
    }

    public struct SkillSkillableFilter
    {
        public string Name { get; set; }
        public SkillableType? SkillableType { get; set; }

        public SkillSkillableFilter(string name, SkillableType? skillableType = null)
        {
            Name = name;
            SkillableType = skillableType;
        }

    }

    public enum SkillableType
    {
        Skilled,
        Skillable,
        NotSkillable
    }
}
