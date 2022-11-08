using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Input;
using Wpf.Ui.Common.Interfaces;
using System.Collections.ObjectModel;
using System.CodeDom.Compiler;
using System.ComponentModel;
using PnP_Organizer.IO;
using System.Linq;
using System.Threading;
using System.Globalization;
using PnP_Organizer.Core.IO;
using System.Windows.Markup;
using System.Windows;
using System.Xaml;

namespace PnP_Organizer.ViewModels
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _appVersion = string.Empty;

        [ObservableProperty]
        private int _maxLogFiles = 10;
        [ObservableProperty]
        private bool _logCalculationsEnabled = true;

        [ObservableProperty]
        private ObservableCollection<Language> _languages = new();
        [ObservableProperty]
        private Language _selectedLanguage;

        [ObservableProperty]
        private bool _openLastLoadedCharacterDialogEnabled = false;

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom() { }

        private void InitializeViewModel()
        {
            PropertyChanged += SettingsPropertyChanged;

            Languages = Language.Languages;

            AppVersion = $"PnP_Organizer - {GetAssemblyVersion()}";

            MaxLogFiles = Properties.Settings.Default.MaxLogFiles;
            LogCalculationsEnabled = Properties.Settings.Default.LogCalculations;
            SelectedLanguage = Languages.Where(lang => lang.Key == Properties.Settings.Default.Localization).First();
            OpenLastLoadedCharacterDialogEnabled = Properties.Settings.Default.PromptLoadLastCharacter;

            _isInitialized = true;
        }

        private void SettingsPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MaxLogFiles):
                    Properties.Settings.Default.MaxLogFiles = MaxLogFiles;
                    break;
                case nameof(LogCalculationsEnabled):
                    Properties.Settings.Default.LogCalculations = LogCalculationsEnabled;
                    break;
                case nameof(SelectedLanguage):
                    System.Diagnostics.Debug.WriteLine(SelectedLanguage.Name);
                    Properties.Settings.Default.Localization = SelectedLanguage.Key;
                    ChangeCultureInfo(new CultureInfo(SelectedLanguage.Key));
                    break;
                default:
                    break;
            }
        }

        private static void ChangeCultureInfo(CultureInfo newCultureInfo)
        {
            CultureInfo.DefaultThreadCurrentCulture = newCultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = newCultureInfo;
        }

        private static string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
        }
    }

    
}
