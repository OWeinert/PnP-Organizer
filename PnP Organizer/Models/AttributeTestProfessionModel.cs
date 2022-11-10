using CommunityToolkit.Mvvm.ComponentModel;

namespace PnP_Organizer.Models
{
    [INotifyPropertyChanged]
    public partial class AttributeTestProfessionModel
    {
        [ObservableProperty]
        private AttributeTestModel? _attributeTestModel;
        [ObservableProperty]
        private int _bonus;

        public AttributeTestProfessionModel(AttributeTestModel attributeTestModel, int bonus)
        {
            AttributeTestModel = attributeTestModel;
            Bonus = bonus;
        }
    }
}
