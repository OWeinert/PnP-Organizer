using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PnP_Organizer.ViewModels;
using System.Collections.ObjectModel;

namespace PnP_Organizer.Models
{
    [INotifyPropertyChanged]
    public partial class ProfessionModel : IModelCollectionItem
    {
        public AttributeTestsViewModel ViewModel
        {
            get;
        }

        public ObservableCollection<IModelCollectionItem> ParentCollection
        {
            get;
        }


        [ObservableProperty]
        private bool _isAttributeTestSelected;
        [ObservableProperty]
        private AttributeTestModel? _selectedAttributeTest;
        [ObservableProperty]
        private int _bonus = 0;

        public ProfessionModel(ObservableCollection<IModelCollectionItem> parentCollection, AttributeTestsViewModel viewModel)
        {
            ParentCollection = parentCollection;
            ViewModel = viewModel;

            PropertyChanged += ProfessionModel_PropertyChanged;
        }

        private void ProfessionModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedAttributeTest))
                IsAttributeTestSelected = SelectedAttributeTest != null;
        }

        [RelayCommand]
        private void Delete()
        {
            ParentCollection.Remove(this);
        }
    }
}
