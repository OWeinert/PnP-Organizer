using PnP_Organizer.Core.Character.Inventory;
using PnP_Organizer.Models;
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

        public CalculatorPage(ViewModels.CalculatorViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

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

        private void ItemSelectorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var itemSelector = (ItemSelectorModel)comboBox.DataContext;

            if (itemSelector.Type == typeof(InventoryWeapon))
            {
                if (itemSelector.SelectedItem?.Name == "None")
                    ViewModel.SelectedWeapon = null;
                else
                    ViewModel.SelectedWeapon = (InventoryWeapon?)itemSelector.SelectedItem;
            }
            else if (itemSelector.Type == typeof(InventoryArmor))
            {
                if (itemSelector.SelectedItem?.Name == "None")
                    ViewModel.SelectedWeapon = null;
                else
                    ViewModel.SelectedArmor = (InventoryArmor?)itemSelector.SelectedItem;
            }
            else if (itemSelector.Type == typeof(InventoryShield))
            {
                if (itemSelector.SelectedItem?.Name == "None")
                    ViewModel.SelectedWeapon = null;
                else
                    ViewModel.SelectedShield = (InventoryShield?)itemSelector.SelectedItem;
            }

            ViewModel.PopulateCalculatorSkillModels();
        }

        private void ShieldComboBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(!(bool)e.NewValue)
            {
                var numBox = (NumberBox)sender;
                numBox.Value = 0;
            }
        }
    }
}
