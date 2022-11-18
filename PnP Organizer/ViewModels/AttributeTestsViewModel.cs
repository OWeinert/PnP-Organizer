using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core;
using PnP_Organizer.Core.Calculators;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.Character.StatModifiers;
using PnP_Organizer.IO;
using PnP_Organizer.Models;
using PnP_Organizer.Properties;
using PnP_Organizer.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.ViewModels
{
    public partial class AttributeTestsViewModel : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private ICollectionView? _attributeTestModelsView;

        [ObservableProperty]
        private ObservableCollection<IModelCollectionItem>? _professionsModels;

        [ObservableProperty]
        private ComboBoxItem? _selectedAttributeFilter;

        [ObservableProperty]
        private ObservableCollection<AttributeTestModel> _attributeTestModels = new()
        {
             new(Resources.AttributeTests_Acrobatic, AttributeType.Dexterity),
             new(Resources.AttributeTests_Athletic, AttributeType.Strength),
             new(Resources.AttributeTests_Insight, AttributeType.Wisdom),
             new(Resources.AttributeTests_Intimidate, AttributeType.Charisma),
             new(Resources.AttributeTests_SleightOfHand, AttributeType.Dexterity),
             new(Resources.AttributeTests_History, AttributeType.Intelligence),
             new(Resources.AttributeTests_Physique, AttributeType.Constitution),
             new(Resources.AttributeTests_FirstAid, AttributeType.Wisdom),
             new(Resources.AttributeTests_Nature, AttributeType.Intelligence),
             new(Resources.AttributeTests_Performance, AttributeType.Charisma),
             new(Resources.AttributeTests_SneakHide, AttributeType.Dexterity),
             new(Resources.AttributeTests_Bluff, AttributeType.Charisma),
             new(Resources.AttributeTests_HandleAnimals, AttributeType.Wisdom),
             new(Resources.AttributeTests_Inspect, AttributeType.Intelligence),
             new(Resources.AttributeTests_Persuade, AttributeType.Charisma),
             new(Resources.AttributeTests_Perceive, AttributeType.Wisdom)
        };

        private IPageService _pageService;

        public AttributeTestsViewModel(IPageService pageService)
        {
            _pageService = pageService;

            PropertyChanged += AttributeTestsViewModel_PropertyChanged;

            UpdateAttributeTestBoni();

            ProfessionsModels = new ObservableCollection<IModelCollectionItem>();
            ProfessionsModels.Add(new AddProfessionModel(ProfessionsModels, this));

            AttributeTestModelsView = CollectionViewSource.GetDefaultView(AttributeTestModels);

            AttributeTestModelsView.Filter += AttributeTestModelsView_Filter;

            Views.Container.FinishedLoading += (sender, e) => 
            {
                UpdateAttributeTestBoni();
                ApplySkillBoni();
            };
        }

        public void OnNavigatedTo()
        {
            foreach(var model in AttributeTestModels)
            {
                model.ExternalBoni = new ObservableCollection<int>();
                model.ExternalDiceBoni = new ObservableCollection<Dice>();
            }
            
            ApplySkillBoni();
            UpdateAttributeTestBoni();
        }

        public void OnNavigatedFrom() { }

        private void AttributeTestsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedAttributeFilter))
                AttributeTestModelsView?.Refresh();
        }

        private bool AttributeTestModelsView_Filter(object obj)
        {
            var attributeTest = (AttributeTestModel)obj;

            AttributeType? attributeType = null;
            if (SelectedAttributeFilter?.Tag != null && Enum.TryParse(typeof(AttributeType), (string)SelectedAttributeFilter.Tag, out object? parsedAttributeType))
                attributeType = (AttributeType?)parsedAttributeType;

            return (attributeType != null && attributeTest.AttributeType == attributeType) || attributeType == null;
        }

        private void UpdateAttributeTestBoni()
        {
            foreach(var attributeTest in AttributeTestModels)
            {
                CharacterAttributes attributes = FileIO.LoadedCharacter.Attributes;
                int attributeValue = attributeTest.AttributeType switch
                {
                    AttributeType.Strength => attributes.Strength,
                    AttributeType.Constitution => attributes.Constitution,
                    AttributeType.Dexterity => attributes.Dexterity,
                    AttributeType.Wisdom => attributes.Wisdom,
                    AttributeType.Intelligence => attributes.Intelligence,
                    AttributeType.Charisma => attributes.Charisma,
                    _ => -1
                };

                attributeTest.BaseBonus = Utils.GetAttributeBonus(attributeValue);

                if (attributeTest.Name == Resources.AttributeTests_Persuade)
                    attributeTest.PearlBonus = (int)Math.Ceiling(FileIO.LoadedCharacter.Pearls.Fire * 0.5);
                else if (attributeTest.Name == Resources.AttributeTests_Athletic)
                    attributeTest.PearlBonus = FileIO.LoadedCharacter.Pearls.Earth;

                attributeTest.UpdateBonusSum();
                attributeTest.UpdateTotalBonus();
            }
        }

        private void ApplySkillBoni()
        {
            var skillsViewModel = _pageService.GetPage<SkillsPage>()!.ViewModel;
            var attributeTestModifiers = skillsViewModel.SkillModels!
                .Where(skillModel => skillModel.Skill.StatModifiers != null && skillModel.Skill.StatModifiers.Any() && skillModel.IsActive) // Filter out skills inactive skills and those without stat modifiers
                .SelectMany(skillModel => skillModel.Skill.StatModifiers!, (skillModel, statModifier) =>    // Select all stat modifiers which are AttributeTestStatModifiers
                {
                    if (statModifier is AttributeTestStatModifier)
                        return statModifier;
                    return null;
                })
                .Where(statModifier => statModifier != null) // Filter out null values
                .Cast<AttributeTestStatModifier>();


            foreach (var modifier in attributeTestModifiers)
            {
                if (modifier != null)
                {
                    var attributeTestModel = AttributeTestModels.Where(aTM => aTM.Name == modifier.AttributeTestName)
                        .Cast<AttributeTestModel>().First();

                    if (attributeTestModel != null)
                    {
                        if (modifier.Bonus != 0)
                            attributeTestModel.ExternalBoni.Add(modifier.Bonus);
                        if (modifier.Dice.MaxValue > 1)
                            attributeTestModel.ExternalDiceBoni.Add(modifier.Dice);
                    }
                }
            }
        }
    }
}
