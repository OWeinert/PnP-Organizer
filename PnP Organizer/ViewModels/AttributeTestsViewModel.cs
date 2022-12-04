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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<AttributeTestModel> _attributeTestModels = new(AttributeTests.Models);

        private IPageService _pageService;

        public AttributeTestsViewModel(IPageService pageService)
        {
            _pageService = pageService;

            PropertyChanged += AttributeTestsViewModel_PropertyChanged;

            UpdateAttributeTestBoni();

            ProfessionsModels = new ObservableCollection<IModelCollectionItem>();
            ProfessionsModels.Add(new AddProfessionModel(ProfessionsModels, this));

            ProfessionsModels!.CollectionChanged += ProfessionsModels_CollectionChanged;

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

            ApplyProfessionBoni();

            ApplySkillBoni();
            UpdateAttributeTestBoni();
        }

        public void OnNavigatedFrom() { }

        public void SaveProfessions()
        {
            FileIO.LoadedCharacter.Professions = new List<ProfessionSaveData>();
            foreach(var modelCollectionItem in ProfessionsModels!)
            {
                if(modelCollectionItem is ProfessionModel professionModel)
                {
                    var professionSaveData = new ProfessionSaveData()
                    {
                        AttributeTestIndex = AttributeTests.Models.IndexOf(professionModel.SelectedAttributeTest!),
                        Bonus = professionModel.Bonus
                    };
                    FileIO.LoadedCharacter.Professions.Add(professionSaveData);
                }
            }
        }

        public void LoadProfessions()
        {
            ProfessionsModels!.Clear();
            ProfessionsModels.Add(new AddProfessionModel(ProfessionsModels, this));

            foreach (var professionSaveData in FileIO.LoadedCharacter.Professions)
            {
                var professionModel = new ProfessionModel(ProfessionsModels!, this)
                {
                    SelectedAttributeTest = AttributeTestModels[professionSaveData.AttributeTestIndex],
                    Bonus = professionSaveData.Bonus
                };
                ProfessionsModels!.Insert(ProfessionsModels.Count - 1, professionModel);
            }
            ApplyProfessionBoni();
        }

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

        private void ProfessionsModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                if (e.NewItems![0] is ProfessionModel professionModel)
                {
                    professionModel.PropertyChanged += ProfessionModel_PropertyChanged;
                }
            }
            ApplyProfessionBoni();
        }

        private void ProfessionModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ApplyProfessionBoni();
        }

        private void ApplyProfessionBoni()
        {
            foreach (var attributeTest in AttributeTestModels)
            {
                attributeTest.ProfessionBoni = new ObservableCollection<int>();
            }

            var professions = ProfessionsModels!.Where(modelCollectionItem => modelCollectionItem is ProfessionModel)
                .Cast<ProfessionModel>();

            foreach (var professionModel in professions)
            {
                if(professionModel.IsAttributeTestSelected)
                {
                    var attributeTest = AttributeTestModels.Where(model => model.Name == professionModel.SelectedAttributeTest!.Name).First();
                    attributeTest.ProfessionBoni.Add(professionModel.Bonus);
                }
            }
            
            foreach(var attributeTest in AttributeTestModels)
            {
                attributeTest.UpdateBonusSum();
                attributeTest.UpdateTotalBonus();
                attributeTest.UpdateToolTip();
            }

            FileIO.IsCharacterSaved = false;
        }

        private void UpdateAttributeTestBoni()
        {
            foreach(var attributeTest in AttributeTestModels)
            {
                var attributes = FileIO.LoadedCharacter.Attributes;
                var attributeValue = attributeTest.AttributeType switch
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
