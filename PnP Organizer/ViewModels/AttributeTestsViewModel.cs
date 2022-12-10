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
        private ObservableCollection<IModelCollectionItem>? _professionModels;

        [ObservableProperty]
        private ComboBoxItem? _selectedAttributeFilter;

        [ObservableProperty]
        private ObservableCollection<AttributeTestModel> _attributeTestModels = new(AttributeTests.Models);

        private readonly IPageService _pageService;

        public AttributeTestsViewModel(IPageService pageService)
        {
            _pageService = pageService;

            PropertyChanged += AttributeTestsViewModel_PropertyChanged;

            UpdateAttributeTestBoni();

            ProfessionModels = new ObservableCollection<IModelCollectionItem>();
            ProfessionModels.Add(new AddProfessionModel(ProfessionModels, this));

            ProfessionModels!.CollectionChanged += ProfessionModels_CollectionChanged;

            ProfessionModels!.CollectionChanged += ProfessionModels_CollectionChanged;

            ProfessionModels!.CollectionChanged += ProfessionModels_CollectionChanged;

            ProfessionModels!.CollectionChanged += ProfessionModels_CollectionChanged;

            AttributeTestModelsView = CollectionViewSource.GetDefaultView(AttributeTestModels);

            AttributeTestModelsView.Filter += AttributeTestModelsView_Filter;

            Views.Container.FinishedLoading += (sender, e) => ApplyAllBoni();
        }

        public void OnNavigatedTo()
        {
            foreach(var model in AttributeTestModels)
            {
                model.ExternalBoni = new ObservableCollection<int>();
                model.ExternalDiceBoni = new ObservableCollection<Dice>();
            }
            ApplyAllBoni();
        }

        public void OnNavigatedFrom() { }

        public void SaveProfessions()
        {
            FileIO.LoadedCharacter.Professions = new List<ProfessionSaveData>();
            foreach(var modelCollectionItem in ProfessionModels!)
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
            ProfessionModels!.Clear();
            ProfessionModels.Add(new AddProfessionModel(ProfessionModels, this));

            foreach (var professionSaveData in FileIO.LoadedCharacter.Professions)
            {
                var professionModel = new ProfessionModel(ProfessionModels!, this)
                {
                    SelectedAttributeTest = AttributeTestModels[professionSaveData.AttributeTestIndex],
                    Bonus = professionSaveData.Bonus
                };
                ProfessionModels!.Insert(ProfessionModels.Count - 1, professionModel);
            }
            ApplyProfessionBoni();

            ApplySkillBoni();
            AddToggleableSkills();

            UpdateAttributeTestBoni();
        }

        private void ApplyAllBoni()
        {
            ApplyProfessionBoni();

            ApplySkillBoni();
            AddToggleableSkills();

            UpdateAttributeTestBoni();
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
            if (SelectedAttributeFilter?.Tag != null && Enum.TryParse(typeof(AttributeType), (string)SelectedAttributeFilter.Tag, out var parsedAttributeType))
                attributeType = (AttributeType?)parsedAttributeType;

            return (attributeType != null && attributeTest.AttributeType == attributeType) || attributeType == null;
        }

        private void ProfessionModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                if (e.NewItems![0] is ProfessionModel professionModel)
                {
                    professionModel.PropertyChanged += ProfessionModel_PropertyChanged;
                }
                else if (e.NewItems![0] is AttributeTestSkillModel aTSkillModel)
                {
                    aTSkillModel.PropertyChanged += AttributeTestSkillModel_PropertyChanged;
                }
            }
            ApplyProfessionBoni();
        }

        private void AttributeTestSkillModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName is nameof(AttributeTestSkillModel.IsActive))
            {
                var aTSkillModel = (AttributeTestSkillModel)sender!;

                foreach (var statModifier in aTSkillModel.StatModifiers)
                {
                    var attributeTest = AttributeTestModels.Where(model => model.Name == statModifier.AttributeTestName).First();

                    if (aTSkillModel.IsActive)
                    {
                        if (statModifier.Bonus != 0)
                            attributeTest.ExternalBoni.Add(statModifier.Bonus);
                        if (statModifier.Dice.MaxValue > 1)
                            attributeTest.ExternalDiceBoni.Add(statModifier.Dice);
                    }
                    else
                    {
                        if (statModifier.Bonus != 0)
                            attributeTest.ExternalBoni.Remove(statModifier.Bonus);
                        if (statModifier.Dice.MaxValue > 1)
                            attributeTest.ExternalDiceBoni.Remove(statModifier.Dice);
                    }
                }
                UpdateAttributeTestVisuals();
            }
        }

        private void ProfessionModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ApplyProfessionBoni();
        }

        private void ApplyProfessionBoni()
        {
            foreach (var attributeTest in AttributeTestModels)
                attributeTest.ProfessionBoni.Clear();

            var professions = ProfessionModels!.Where(modelCollectionItem => modelCollectionItem is ProfessionModel).Cast<ProfessionModel>();

            foreach (var professionModel in professions)
            {
                if(professionModel.IsAttributeTestSelected)
                {
                    var attributeTest = AttributeTestModels.Where(model => model.Name == professionModel.SelectedAttributeTest!.Name).First();
                    attributeTest.ProfessionBoni.Add(professionModel.Bonus);
                }
            }

            UpdateAttributeTestVisuals();

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
                attributeTest.UpdateToolTip();
            }
        }

        private void ApplySkillBoni(string[]? attributeTestFilter = null)
        {
            var skillsViewModel = _pageService.GetPage<SkillsPage>()!.ViewModel;

            var skillModels = skillsViewModel.SkillModels!
                .Where(skillModel => skillModel.Skill.StatModifiers != null && skillModel.Skill.StatModifiers.Any() && skillModel.IsActive); // Filter out skills inactive skills and those without stat modifiers

            var attributeTestModifiers = skillModels
                .SelectMany(skillModel => skillModel.Skill.StatModifiers!, (skillModel, statModifier) =>    // Select all stat modifiers which are AttributeTestStatModifiers
                {
                    if (statModifier is AttributeTestStatModifier attributeTestStatModifier && !attributeTestStatModifier.Toggleable
                    && (attributeTestFilter == null || attributeTestFilter.Contains(attributeTestStatModifier.AttributeTestName)))
                        return attributeTestStatModifier;
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

        private void AddToggleableSkills()
        {
            var skillsViewModel = _pageService.GetPage<SkillsPage>()!.ViewModel;

            var skillModels = skillsViewModel.SkillModels!
                .Where(skillModel => skillModel.Skill!.StatModifiers! != null && skillModel.Skill.StatModifiers.Any() && skillModel.IsActive); // Filter out skills inactive skills and those without stat modifiers

            // Clear Skills from Professions/Skills list
            var attributeTestSkillModels = ProfessionModels!.Where(model => model is AttributeTestSkillModel).ToList();
            var count = attributeTestSkillModels.Count;
            for (var i = 0; i < count; i++)
            {
                ProfessionModels!.Remove(attributeTestSkillModels[i]);
            }

            // Add Toggleable Skills to Professions/Skills list
            var toggleableSkills = skillModels
                .Where(skillModel => skillModel.Skill!.StatModifiers!.Any(statModifier => statModifier is AttributeTestStatModifier aTStatMod && aTStatMod.Toggleable));

            foreach (var skillModel in toggleableSkills)
            {
                var professions = ProfessionModels!.Where(model => model is ProfessionModel).Cast<ProfessionModel>();
                var insertionIndex = ProfessionModels!.Any(model => model is ProfessionModel) ? ProfessionModels!.IndexOf(professions.First()) : 0;

                var attributeTestSkillModel = new AttributeTestSkillModel(skillModel.Skill!, ProfessionModels!);

                ProfessionModels?.Insert(insertionIndex, attributeTestSkillModel);
            }
        }

        private void UpdateAttributeTestVisuals()
        {
            foreach (var attributeTest in AttributeTestModels)
            {
                attributeTest.UpdateBonusSum();
                attributeTest.UpdateTotalBonus();
                attributeTest.UpdateToolTip();
            }
        }
    }
}