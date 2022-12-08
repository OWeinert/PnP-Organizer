using System.Diagnostics;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;

namespace PnP_Organizer.Views.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class AttributeTestsPage : INavigableView<ViewModels.AttributeTestsViewModel>
    {
        public ViewModels.AttributeTestsViewModel ViewModel
        {
            get;
        }

        public AttributeTestsPage(ViewModels.AttributeTestsViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        private void NumberBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var numBox = (NumberBox)sender;

            if (numBox.Value > numBox.Max || numBox.Value < numBox.Min || e.Delta == 0)
                return;

            numBox.Value = e.Delta > 0 ? numBox.Value + numBox.Step : numBox.Value - numBox.Step;
            numBox.Text = numBox.Value.ToString();
        }
    }
}