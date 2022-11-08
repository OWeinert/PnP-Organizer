using CommunityToolkit.Mvvm.Input;
using PnP_Organizer.ViewModels;
using System.Collections.ObjectModel;

namespace PnP_Organizer.Models
{
    public partial class AddProfessionModel : IModelCollectionItem
    {
        private AttributeTestsViewModel ViewModel
        {
            get;
        }

        public ObservableCollection<IModelCollectionItem> ParentCollection
        {
            get;
        }

        public AddProfessionModel(ObservableCollection<IModelCollectionItem> parentCollection, AttributeTestsViewModel viewModel)
        {
            ParentCollection = parentCollection;
            ViewModel = viewModel;
        }

        [RelayCommand]
        private void AddProfession() => ParentCollection.Insert(ParentCollection.Count - 1, new ProfessionModel(ParentCollection, ViewModel));
    }
}
