using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.Views.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : INavigableView<ViewModels.DashboardViewModel>
    {
        public ViewModels.DashboardViewModel ViewModel
        {
            get;
        }

        private INavigationService _navigationService;

        public DashboardPage(ViewModels.DashboardViewModel viewModel, INavigationService navigationService)
        {
            ViewModel = viewModel;

            InitializeComponent();

            _navigationService = navigationService;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e) => _navigationService.Navigate("overview");
    }
}