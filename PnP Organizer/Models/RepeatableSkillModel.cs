using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PnP_Organizer.Core.Character;

namespace PnP_Organizer.Models
{
    /// <summary>
    /// Data for Skills
    /// </summary>
    public partial class RepeatableSkillModel : SkillModel
    {
        [ObservableProperty]
        private int _repetition = 0;

        [ObservableProperty]
        private int _totalSkillPoints;

        public RepeatableSkillModel(Skill skill) : base(skill) 
        {
            PropertyChanged += RepeatableSkillModel_PropertyChanged;
        }

        private void RepeatableSkillModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName is nameof(SkillPoints) or nameof(Repetition))
            {
                TotalSkillPoints = Repetition * MaxSkillPoints + SkillPoints;
            }
        }

        [RelayCommand]
        private void IncreaseSkillPointsRepeatable()
        {
            SkillPoints++;
            if(SkillPoints >= MaxSkillPoints)
            {
                Repetition++;
                SkillPoints = 0;

            }
        }

        [RelayCommand]
        private void DecreaseSkillPointsRepeatable()
        {
            SkillPoints--;
            if(Repetition > 0 && SkillPoints <= 0)
            {
                SkillPoints = MaxSkillPoints - 1;
                Repetition--;
            }
            else if(SkillPoints < 0)
                SkillPoints = 0;
        }
    }
}
