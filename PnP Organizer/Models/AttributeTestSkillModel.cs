using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Core.Character.StatModifiers;
using System.Collections.ObjectModel;
using System.Linq;

namespace PnP_Organizer.Models
{
    [INotifyPropertyChanged]
    public partial class AttributeTestSkillModel : IModelCollectionItem
    {
        public ObservableCollection<IModelCollectionItem> ParentCollection { get; }
        public Skill Skill { get; }
        public AttributeTestStatModifier[] StatModifiers { get; }

        [ObservableProperty]
        private bool _isActive = false;

        public AttributeTestSkillModel(Skill skill, ObservableCollection<IModelCollectionItem> parentCollection) 
        {
            Skill = skill;
            StatModifiers = skill.StatModifiers!.Where(statModifier => statModifier is AttributeTestStatModifier).Cast<AttributeTestStatModifier>().ToArray();
            ParentCollection = parentCollection;
        }

        public void ToggleActive() => IsActive = !IsActive;
    }
}
