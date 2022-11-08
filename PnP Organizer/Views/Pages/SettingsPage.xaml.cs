using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;

namespace PnP_Organizer.Views.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : INavigableView<ViewModels.SettingsViewModel>
    {
        public ViewModels.SettingsViewModel ViewModel
        {
            get;
        }

        public SettingsPage(ViewModels.SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        private void NumberBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            NumberBox numBox = (NumberBox)sender;
            double value = numBox.Value;

            if (numBox.Value > numBox.Max || numBox.Value < numBox.Min || e.Delta == 0)
                return;

            value = e.Delta > 0 ? value + numBox.Step : value - numBox.Step;

            // Setting the NumberBox Value does not update the number in the UI, updating the Text updates both UI number and the Value
            numBox.Text = $"{value}";
            e.Handled = true;
        }
    }
}