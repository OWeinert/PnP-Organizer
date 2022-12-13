using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Character;

namespace PnP_Organizer.Models
{
    [INotifyPropertyChanged]
    public partial class CalculatorSkillModel
    {
        public Skill Skill { get; }

        [ObservableProperty]
        private bool _isActive = false;

        public CalculatorSkillModel(Skill skill)
        {
            Skill = skill;
        }
    }
}
