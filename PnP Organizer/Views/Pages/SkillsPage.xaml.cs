using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Models;
using System.Windows;
using Wpf.Ui.Common.Interfaces;
using WpfUiButton = Wpf.Ui.Controls.Button;

namespace PnP_Organizer.Views.Pages
{
    /// <summary>
    /// Interaction logic for SkillsPage.xaml
    /// </summary>
    [INotifyPropertyChanged]
    public partial class SkillsPage : INavigableView<ViewModels.SkillsViewModel>
    {
        public ViewModels.SkillsViewModel ViewModel
        {
            get;
        }

        public SkillsPage(ViewModels.SkillsViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }


        // TODO FilterButton_Click move logic to ViewModel
        private async void FilterTreeButton_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                int selectedFilterIndex = ViewModel.SelectedTreeFilterIndex; // Don't modify Property directly to avoid invokating PropertyChanged event
                selectedFilterIndex++;  // PropertyChanged event invokation would result in an IndexOutOfBoundsException here
                if (selectedFilterIndex >= ViewModel.TreeFilters!.Count)
                    selectedFilterIndex = 0;
                ViewModel.SelectedTreeFilterIndex = selectedFilterIndex;
            });
        }

        private async void FilterSkillableButton_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                int selectedFilterIndex = ViewModel.SelectedSkillableFilterIndex; // Don't modify Property directly to avoid invokating PropertyChanged event
                selectedFilterIndex++;  // PropertyChanged event invokation would result in an IndexOutOfBoundsException here
                if (selectedFilterIndex >= ViewModel.SkillableFilters!.Count)
                    selectedFilterIndex = 0;
                ViewModel.SelectedSkillableFilterIndex = selectedFilterIndex;
            });
        }
    }
}