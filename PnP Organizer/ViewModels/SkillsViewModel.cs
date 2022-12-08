using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Character;
using PnP_Organizer.IO;
using PnP_Organizer.Logging;
using PnP_Organizer.Models;
using PnP_Organizer.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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
            PropertyChanged += SelectedFiltersChanged;
            
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
            foreach (var skill in Skills.Instance.SkillsList)
            {
                SkillModel skillModel;
                if (skill.IsRepeatable)
                    skillModel = new RepeatableSkillModel(skill);
                else
                    skillModel = new SkillModel(skill);

                SkillModels.Add(skillModel);
            }
            SkillModelsView = CollectionViewSource.GetDefaultView(SkillModels);

            Logger.Log($"{SkillModels.Count} SkillModels for {Skills.Instance.SkillsList.Count} Skills initialized");
        }

        private async void SkillModels_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(SkillModel skillModel in e.NewItems!)
                    {
                        await CheckSkillModelSkillability(skillModel);
                        skillModel.PropertyChanged += new PropertyChangedEventHandler(SkillModel_PropertyChanged);
                    }
                    break;
            }
        }

        private async void SkillModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var senderSkillModel = (SkillModel?)sender;
            if (senderSkillModel != null && e.PropertyName is nameof(senderSkillModel.SkillPoints))
            {
                await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    var dependendSkillModels = SkillModels!.Where(sM => sM.Skill!.DependendSkillNames.Contains(senderSkillModel.Skill!.Name));
                    foreach (var skillModel in dependendSkillModels)
                    {
                        await CheckSkillModelSkillability(skillModel);
                    }

                    var forcedDependendSkillModel = SkillModels!.FirstOrDefault(sM => sM.Skill!.ForcedDependendSkillName == senderSkillModel.Skill!.Name);
                    if (forcedDependendSkillModel != default)
                        await CheckSkillModelSkillability(forcedDependendSkillModel);

                    CalculateUsedSkillPoints();
                });
            }
        }

        // TODO simplify
        private async Task CheckSkillModelSkillability(SkillModel skillModel)
        {
            if(!skillModel.Skill!.DependendSkillNames.Any() && string.IsNullOrWhiteSpace(skillModel.Skill!.ForcedDependendSkillName)) // if this is true, there's no need to check further
            {
                skillModel.IsSkillable = true;
                return;
            }
            var dependedActiveSkills = SkillModels! // Get a list of active skills which the given skill depends on
                .Where(dependendSkill => dependendSkill.IsActive && skillModel.Skill!.DependendSkillNames.Contains(dependendSkill.Name));

            if (!string.IsNullOrWhiteSpace(skillModel.Skill!.ForcedDependendSkillName))
            {
                var forcedDependendSkill = SkillModels!.First(dependendSkill => dependendSkill.Skill!.Name == skillModel.Skill!.ForcedDependendSkillName);  // get the skill which the given skill is forced to depend on
                skillModel.IsSkillable = dependedActiveSkills.Any() && forcedDependendSkill!.IsActive;
            }
            else
                skillModel.IsSkillable = dependedActiveSkills.Any();
            
            await Task.CompletedTask;
        }

        private void SelectedFiltersChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName is nameof(SelectedTreeFilterIndex) or nameof(SelectedSkillableFilterIndex) or nameof(SearchBoxText))
            {
                SelectedTreeFilter = TreeFilters![SelectedTreeFilterIndex];
                SelectedSkillableFilter = SkillableFilters![SelectedSkillableFilterIndex];
                SkillModelsView?.Refresh();
            }
        }

        private bool SkillModelsView_Filter(object item)
        {
            var skill = (SkillModel)item;

            // Filter by SearchBox text
            if((skill.Name.Contains(SearchBoxText) || string.IsNullOrEmpty(SearchBoxText)))
            {
                // Filter by Skillability
                var skillability = SelectedSkillableFilter.SkillableType switch
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

        public void SaveCharacterSkills()
        {
            if (_isInitialized)
            {
                Logger.Log("Saving Skills...");

                List<SkillSaveData> skills = new();
                for(var i = 0; i < SkillModels!.Count; i++)
                {
                    if (SkillModels[i].SkillPoints > 0 ||
                        (SkillModels[i] is RepeatableSkillModel model && model.Repetition > 0))
                    {
                        SkillSaveData skillSaveData = new()
                        {
                            Index = i,
                            SkillPoints = SkillModels[i]!.SkillPoints
                        };
                        if (SkillModels[i] is RepeatableSkillModel repeatableSkillModel)
                            skillSaveData.Repetition = repeatableSkillModel.Repetition;

                        skills.Add(skillSaveData);
                    }
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
                foreach (var skill in Skills.Instance.SkillsList)
                {
                    SkillModel skillModel;
                    if (skill.IsRepeatable)
                        skillModel = new RepeatableSkillModel(skill);
                    else
                        skillModel = new SkillModel(skill);

                    SkillModels!.Add(skillModel);
                }

                foreach (var skillSaveData in FileIO.LoadedCharacter.Skills!)
                {
                    var skill = Skills.Instance.SkillsList[skillSaveData.Index];
                    skill.SkillPoints = skillSaveData.SkillPoints;

                    var skillModel = SkillModels![skillSaveData.Index];

                    skillModel.SkillPoints = skillSaveData.SkillPoints;
                    if (skill.IsRepeatable)
                        ((RepeatableSkillModel)skillModel).Repetition = skillSaveData.Repetition ?? 0;

                    //SkillModels![skillSaveData.Index] = skillModel;
                }
                foreach(var skillModel in SkillModels!)
                {
                    _ = CheckSkillModelSkillability(skillModel);
                }

                SkillModelsView = CollectionViewSource.GetDefaultView(SkillModels);

                CalculateUsedSkillPoints();

                Logger.Log("Skills loaded successfully!");
            }
        }

        private void CalculateUsedSkillPoints()
        {
            UsedSkillPoints = SkillModels!.Sum(sM =>
            {
                if (sM is RepeatableSkillModel rSM)
                {
                    return rSM.TotalSkillPoints;
                }
                return sM.SkillPoints;
            });
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
