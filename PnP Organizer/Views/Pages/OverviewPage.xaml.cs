using Microsoft.Win32;
using PnP_Organizer.IO;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;

namespace PnP_Organizer.Views.Pages
{
    /// <summary>
    /// Interaction logic for OverviewPage.xaml
    /// </summary>
    public partial class OverviewPage : INavigableView<ViewModels.OverviewViewModel>
    {
        public ViewModels.OverviewViewModel ViewModel
        {
            get;
        }

        public OverviewPage(ViewModels.OverviewViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        // TODO CharacterImageBtn_Click: move logic to ViewModel
        /// <summary>
        /// Opens an OpenFileDialog which lets the User select a CharacterImage,
        /// which then will be loaded and set as the CharacterImage in the ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterImageBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openImageDialog = new()
            {
                Title = Properties.Resources.Dialog_OpenImage,
                Filter = FileIO.ImageFileExtensionFilter,
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            if (openImageDialog.ShowDialog() == true)
            {
                using var fs = (FileStream)openImageDialog.OpenFile();
                BitmapImage image = new(new Uri(fs.Name, UriKind.Absolute));
                ViewModel.CharacterImage = image;
            }
        }

        /// <summary>
        /// Filters the input of TextBoxes to only allow fixed-point numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var input = ((TextBox)sender).Text + e.Text;
            e.Handled = !Regex.IsMatch(input, @"^\d*(\.|\,)?\d*$");
        }

        //TODO NumBox_MouseWheel move to static function 1/2
        private void AttributeNumBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var numBox = (NumberBox)sender;

            if(numBox.Value > numBox.Max || numBox.Value < numBox.Min || e.Delta == 0)
                return;

            numBox.Value = e.Delta > 0 ? numBox.Value + numBox.Step : numBox.Value - numBox.Step;
        }
    }
}