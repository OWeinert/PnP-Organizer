using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core;
using PnP_Organizer.Core.Character;
using PnP_Organizer.IO;
using PnP_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PnP_Organizer.ViewModels
{
    public partial class AttributeTestsViewModel : ObservableObject
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
            new AttributeTestModel(Properties.Resources.AttributeTests_Acrobatic, AttributeType.Dexterity),
            new AttributeTestModel(Properties.Resources.AttributeTests_Athletic, AttributeType.Strength, new int[] { FileIO.LoadedCharacter.Pearls.Earth }),
            new AttributeTestModel(Properties.Resources.AttributeTests_Insight, AttributeType.Wisdom),
            new AttributeTestModel(Properties.Resources.AttributeTests_Intimidate, AttributeType.Charisma),
            new AttributeTestModel(Properties.Resources.AttributeTests_SleightOfHand, AttributeType.Dexterity),
            new AttributeTestModel(Properties.Resources.AttributeTests_History, AttributeType.Intelligence),
            new AttributeTestModel(Properties.Resources.AttributeTests_Physique, AttributeType.Constitution),
            new AttributeTestModel(Properties.Resources.AttributeTests_FirstAid, AttributeType.Wisdom),
            new AttributeTestModel(Properties.Resources.AttributeTests_Nature, AttributeType.Intelligence),
            new AttributeTestModel(Properties.Resources.AttributeTests_Performance, AttributeType.Charisma),
            new AttributeTestModel(Properties.Resources.AttributeTests_SneakHide, AttributeType.Dexterity),
            new AttributeTestModel(Properties.Resources.AttributeTests_Bluff,AttributeType.Charisma),
            new AttributeTestModel(Properties.Resources.AttributeTests_HandleAnimals, AttributeType.Wisdom),
            new AttributeTestModel(Properties.Resources.AttributeTests_Inspect, AttributeType.Intelligence),
            new AttributeTestModel(Properties.Resources.AttributeTests_Persuade, AttributeType.Charisma, new int[] { (int)(Math.Floor(FileIO.LoadedCharacter.Pearls.Fire * 0.5)) }),
            new AttributeTestModel(Properties.Resources.AttributeTests_Perceive, AttributeType.Wisdom),
        };

        public AttributeTestsViewModel()
        {
            PropertyChanged += AttributeTestsViewModel_PropertyChanged;

            UpdateAttributeTestBoni();

            ProfessionsModels = new ObservableCollection<IModelCollectionItem>();
            ProfessionsModels.Add(new AddProfessionModel(ProfessionsModels, this));

            AttributeTestModelsView = CollectionViewSource.GetDefaultView(AttributeTestModels);

            AttributeTestModelsView.Filter += AttributeTestModelsView_Filter;

            Views.Container.FinishedLoading += (sender, e) => UpdateAttributeTestBoni();
            FileIO.LoadedCharacter.PropertyChanged += (sender, e) => UpdateAttributeTestBoni();
        }

        private void AttributeTestsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedAttributeFilter))
                AttributeTestModelsView?.Refresh();
        }

        private bool AttributeTestModelsView_Filter(object obj)
        {
            AttributeTestModel attributeTest = (AttributeTestModel)obj;

            AttributeType? attributeType = null;
            if (SelectedAttributeFilter?.Tag != null && Enum.TryParse(typeof(AttributeType), (string)SelectedAttributeFilter.Tag, out object? parsedAttributeType))
                attributeType = (AttributeType?)parsedAttributeType;

            return (attributeType != null && attributeTest.AttributeType == attributeType) || attributeType == null;
        }

        private void UpdateAttributeTestBoni()
        {
            foreach(AttributeTestModel attributeTest in AttributeTestModels)
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
                attributeTest.UpdateBonusSum();
            }
        }
    }
}
