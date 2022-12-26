using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PnP_Organizer.Core.Character;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PnP_Organizer.Models
{
    [INotifyPropertyChanged]
    public partial class CalculatorSkillModel
    {
        public Skill Skill { get; }

        [ObservableProperty]
        private bool _isActive = false;

        private readonly ICollection<CalculatorSkillModel> _parentCollection;

        public CalculatorSkillModel(Skill skill, ICollection<CalculatorSkillModel> parentCollection)
        {
            Skill = skill;
            _parentCollection = parentCollection;
            PropertyChanged += CalculatorSkillModel_PropertyChanged; ;
        }

        private void CalculatorSkillModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IsActive) && IsActive)
            {
                Debug.WriteLine(IsActive);
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
