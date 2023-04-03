using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Core.Character;
using System.Collections.Generic;

namespace PnP_Organizer.Models
{
    [INotifyPropertyChanged]
    public partial class CalculatorSkillModel
    {
        public Skill Skill { get; }

        public string Tooltip { get; }

        [ObservableProperty]
        private bool _isActive = false;

        [ObservableProperty]
        private bool _isActivatable = true;

        private readonly ICollection<CalculatorSkillModel> _parentCollection;

        public CalculatorSkillModel(Skill skill, ICollection<CalculatorSkillModel> parentCollection)
        {
            Skill = skill;
            _parentCollection = parentCollection;
            Tooltip = Skill.CreateTooltip(Skill);
            PropertyChanged += CalculatorSkillModel_PropertyChanged;
        }

        private void CalculatorSkillModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IsActive) && IsActive)
            {
                if (!Skill.UsableWithOtherSkills)
                {
                    foreach (var model in _parentCollection)
                    {
                        if (model != this)
                        {
                            model.IsActive = false;
                        }
                    }
                }
            }
        }
    }
}
