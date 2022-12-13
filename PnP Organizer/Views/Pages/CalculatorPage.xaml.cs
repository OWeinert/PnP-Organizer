using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.Views.Pages
{
    /// <summary>
    /// Interaction logic for CalculatorPage.xaml
    /// </summary>
    public partial class CalculatorPage : INavigableView<ViewModels.CalculatorViewModel>
    {
        public ViewModels.CalculatorViewModel ViewModel
        {
            get;
        }

        private readonly ISnackbarService _snackbarService;

        public CalculatorPage(ViewModels.CalculatorViewModel viewModel, ISnackbarService snackbarService)
        {
            ViewModel = viewModel;
            InitializeComponent();
            _snackbarService = snackbarService;
            
        }

        // TODO CalculatorModifierCard_Click: move logic to Model
        private void CalculatorModifierCard_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var modifierCard = (CardControl)sender;
            var cardGrid = (Grid)modifierCard.Content;
            var toggleSwitch = (ToggleSwitch)cardGrid.Children[0];
            toggleSwitch.IsChecked = !toggleSwitch.IsChecked;
        }

        //TODO NumBox_MouseWheel move to static function 2/2
        private void NumBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var numBox = (NumberBox)sender;

            if (numBox.Value > numBox.Max || numBox.Value < numBox.Min || e.Delta == 0)
                return;

            numBox.Value = e.Delta > 0 ? numBox.Value + numBox.Step : numBox.Value - numBox.Step;
        }

        // TODO CalculateButton_Click: move logic to ViewModel
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            //ViewModel.CalculateValues();
        }
    }
}
