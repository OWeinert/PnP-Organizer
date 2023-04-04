using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using PnP_Organizer.Core;
using PnP_Organizer.IO;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.Views.Pages
{
    /// <summary>
    /// Interaction logic for NotesPage.xaml
    /// </summary>
    [INotifyPropertyChanged]
    public partial class NotesPage : INavigableView<ViewModels.NotesViewModel>
    {
        public ViewModels.NotesViewModel ViewModel
        {
            get;
        }

        [ObservableProperty]
        private SolidColorBrush _colorPickerBG = (SolidColorBrush)Brushes.White;

        private readonly ISnackbarControl _snackbarControl;

        public NotesPage(ViewModels.NotesViewModel viewModel, ISnackbarService snackbarService)
        {
            ViewModel = viewModel;

            InitializeComponent();

            _snackbarControl = snackbarService.GetSnackbarControl();

            FontSizeComboBox.SelectedIndex = 8;
        }

        private void FontSizeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RootTextBox?.Selection.ApplyPropertyValue(Inline.FontSizeProperty, (double)((int)FontSizeComboBox.SelectedItem));
        }

        public void SaveNotesDocumentToCharacter()
        {
            using var ms = new MemoryStream();
            TextRange textRange = new(RootTextBox.Document.ContentStart, RootTextBox.Document.ContentEnd);
            textRange.Save(ms, DataFormats.Xaml);
            FileIO.LoadedCharacter.Notes = ms.ToArray();
        }

        public void LoadNotesDocumentFromCharacter()
        {
            using var ms = new MemoryStream(FileIO.LoadedCharacter.Notes);
            TextRange textRange = new(RootTextBox.Document.ContentStart, RootTextBox.Document.ContentEnd);
            if(ms.Length == 0) // If the MemoryStream is empty i.e. there are no notes, then clear the TextEditor
                textRange.Text = string.Empty;
            else
                textRange.Load(ms, DataFormats.Xaml);
        }

        private async void RootTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            await Task.CompletedTask;
            FileIO.IsCharacterSaved = false;
        }

        private void ColorPicker_SelectedBrushChanged(object sender, Controls.Events.SelectedBrushChangedEventArgs e)
        {
            ColorPickerBG = e.SelectedBrush;
            TextRange textRange = new(RootTextBox.Selection.Start, RootTextBox.Selection.End);
            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, ColorPickerBG);
        }

        private async void ExportButton_Click(object sender, RoutedEventArgs e) => await ExportDocument();

        private async Task ExportDocument()
        {
            var fileName = Utils.ExportDocument(RootTextBox);
            await _snackbarControl.ShowAsync("Notes saved to:", $"\"{fileName}\"", Wpf.Ui.Common.SymbolRegular.Save28);
        }
    }
}
