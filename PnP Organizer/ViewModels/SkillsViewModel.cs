using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using PnP_Organizer.Core.Character;
using PnP_Organizer.IO;
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
using System.Windows.Threading;

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

        private DispatcherTimer? _searchBoxTimer;

        private readonly ILogger<SkillsViewModel> _logger;

        public SkillsViewModel(ILogger<SkillsViewModel> logger)
        {
            _logger = logger;
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

            SkillModelsView!.Filter += (item) =>
            {
                return SkillModelsView_Filter(item).Result;
            };

            _searchBoxTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 100)
            };
            _searchBoxTimer.Tick += SearchBoxTimer_Tick;

            _isInitialized = true;
        }

        private void InitializeSkillModels()
        {
            SkillModels = new ObservableCollection<SkillModel>();
            SkillModels.CollectionChanged += SkillModels_CollectionChanged;
            foreach (var skill in Skills.Instance.Registry)
            {
                SkillModel skillModel;
                if (skill.Value.IsRepeatable)
                    skillModel = new RepeatableSkillModel(skill.Value);
                else
                    skillModel = new SkillModel(skill.Value);

                SkillModels.Add(skillModel);
            }
            SkillModelsView = CollectionViewSource.GetDefaultView(SkillModels);

            _logger.LogInformation("{models} SkillModels for {skills} Skills initialized", SkillModels.Count, Skills.Instance.Registry.Count);
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
                    var dependendSkillModels = SkillModels!.Where(sM => sM.Skill!.DependendSkills.Contains(senderSkillModel.Skill!.Identifier));
                    foreach (var skillModel in dependendSkillModels)
                    {
                        await CheckSkillModelSkillability(skillModel);
                    }

                    var forcedDependendSkillModel = SkillModels!.FirstOrDefault(sM => sM.Skill!.ForcedDependendSkill.Equals(senderSkillModel.Skill!.Identifier));
                    if (forcedDependendSkillModel != default)
                        await CheckSkillModelSkillability(forcedDependendSkillModel);

                    CalculateUsedSkillPoints();
                });
            }
        }

        // TODO simplify
        private async Task CheckSkillModelSkillability(SkillModel skillModel)
        {
            if(!skillModel.Skill!.DependendSkills.Any() && skillModel.Skill!.ForcedDependendSkill == null) // if this is true, there's no need to check further
            {
                skillModel.IsSkillable = true;
                return;
            }
            var dependedActiveSkills = SkillModels! // Get a list of active skills which the given skill depends on
                .Where(dependendSkill => dependendSkill.IsActive && skillModel.Skill!.DependendSkills.Contains(dependendSkill.Skill!.Identifier));

            if (skillModel.Skill!.ForcedDependendSkill != null)
            {
                var forcedDependendSkill = SkillModels!.First(dependendSkill => dependendSkill.Skill!.Identifier.Equals(skillModel.Skill!.ForcedDependendSkill));  // get the skill which the given skill is forced to depend on
                skillModel.IsSkillable = dependedActiveSkills.Any() && forcedDependendSkill!.IsActive;
            }
            else
                skillModel.IsSkillable = dependedActiveSkills.Any();
            
            await Task.CompletedTask;
        }

        private void SelectedFiltersChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName is nameof(SelectedTreeFilterIndex) or nameof(SelectedSkillableFilterIndex))
            {
                SelectedTreeFilter = TreeFilters![SelectedTreeFilterIndex];
                SelectedSkillableFilter = SkillableFilters![SelectedSkillableFilterIndex];
                Application.Current.Dispatcher.Invoke(() => SkillModelsView?.Refresh());
            }
            if(e.PropertyName == nameof(SearchBoxText))
            {
                _searchBoxTimer?.Start();
            }
        }

        private async Task<bool> SkillModelsView_Filter(object item)
        {
            var skill = (SkillModel)item;

            // Filter by SearchBox text
            if((skill.Name.Contains(SearchBoxText, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(SearchBoxText)))
            {
                await Task.Delay(0);
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
                // additionally Filter by SkillCategory
                return skill.SkillCategory == SelectedTreeFilter.SkillCategory && skillability;
            }
            return false;
        }

        private void SearchBoxTimer_Tick(object? sender, EventArgs e)
        {
            _searchBoxTimer?.Dispatcher.Invoke(() => SkillModelsView?.Refresh());
            _searchBoxTimer?.Stop();
        }

        public void SaveCharacterSkills()
        {
            if (_isInitialized)
            {
                _logger.LogInformation("Saving Skills...");

                List<SkillSaveData> skills = new();
                foreach(var skillModel in SkillModels!)
                {
                    if (skillModel.SkillPoints > 0 ||
                        (skillModel is RepeatableSkillModel model && model.Repetition > 0))
                    {
                        SkillSaveData skillSaveData = new()
                        {
                            Identifier = skillModel.Skill!.Identifier,
                            SkillPoints = skillModel.SkillPoints
                        };
                        if (skillModel is RepeatableSkillModel repeatableSkillModel)
                            skillSaveData.Repetition = repeatableSkillModel.Repetition;

                        skills.Add(skillSaveData);
                    }
                }
                FileIO.LoadedCharacter.Skills = skills;

                _logger.LogInformation("Skills saved successfully!");
            }  
        }

        public void LoadCharacterSkills()
        {
            if (_isInitialized)
            {
                _logger.LogInformation("Loading Skills...");
                foreach (var skillModel in SkillModels!)
                {
                    skillModel.SkillPoints = 0;
                    if (skillModel.Skill!.IsRepeatable)
                        ((RepeatableSkillModel)skillModel).Repetition = 0;
                }

                foreach (var skillSaveData in FileIO.LoadedCharacter.Skills!)
                {
                    var skill = Skills.Instance.Registry[skillSaveData.Identifier];
                    skill.SkillPoints = skillSaveData.SkillPoints;

                    var skillModel = SkillModels!.First(sM => sM.Skill!.Identifier.Equals(skillSaveData.Identifier));

                    skillModel.SkillPoints = skillSaveData.SkillPoints;
                    if (skill.IsRepeatable)
                        ((RepeatableSkillModel)skillModel).Repetition = skillSaveData.Repetition ?? 0;
                }
                foreach(var skillModel in SkillModels!)
                {
                    _ = CheckSkillModelSkillability(skillModel);
                }

                SkillModelsView = CollectionViewSource.GetDefaultView(SkillModels);

                CalculateUsedSkillPoints();

                _logger.LogInformation("Skills loaded successfully!");
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
