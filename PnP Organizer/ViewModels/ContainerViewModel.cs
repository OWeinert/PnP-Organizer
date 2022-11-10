using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.ViewModels
{
    public partial class ContainerViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = String.Empty;

        public ContainerViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            ApplicationTitle = "PnP Organizer";
            _isInitialized = true;
        }
    }
}
