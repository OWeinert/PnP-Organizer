using Microsoft.Win32;
using Octokit;
using PnP_Organizer.Core;
using PnP_Organizer.Core.IO;
using PnP_Organizer.IO;
using PnP_Organizer.Logging;
using PnP_Organizer.ViewModels;
using PnP_Organizer.Views.Pages;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace PnP_Organizer.Views
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : INavigationWindow
    {
        public static event FinishedLoadingEventHandler? FinishedLoading;
        public delegate void FinishedLoadingEventHandler(object sender, EventArgs e);

        public ContainerViewModel ViewModel
        {
            get;
        }

        private readonly ISnackbarService _snackbarService;
        private readonly IDialogControl _dialogControl;

        public Container(ContainerViewModel viewModel, IPageService pageService, INavigationService navigationService, ISnackbarService snackbarService, IDialogService dialogService)
        {
            ViewModel = viewModel;
            DataContext = this;

            Loaded += Container_Loaded;

            InitializeComponent();
            SetPageService(pageService);

            dialogService.SetDialogControl(RootDialog);
            _dialogControl = dialogService.GetDialogControl();

            _snackbarService = snackbarService;
            _snackbarService.SetSnackbarControl(RootSnackbar);

            navigationService.SetNavigationControl(RootNavigation);

            if (Properties.Settings.Default.PromptLoadLastCharacter)
                ShowOpenLastCharacterDialog();
            else
                CreateAndLoadCharacter(showSnackbar: false);
        }

        private async void Container_Loaded(object sender, RoutedEventArgs e)
        {
            await CheckVersion();
        }

        #region INavigationWindow methods

        public Frame GetFrame()
            => RootFrame;

        public INavigation GetNavigation()
            => RootNavigation;

        public bool Navigate(Type pageType)
            => RootNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService)
            => RootNavigation.PageService = pageService;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();

        #endregion INavigationWindow methods

        #region INavigationWindow Overrides

        protected override async void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            if(!FileIO.IsCharacterSaved)
                await ShowUnsavedCharacterDialog();

            System.Windows.Application.Current.Shutdown();
        }
        #endregion

        #region MenuItem Commands
        private void SaveCharacterCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) => SaveCharacterCommandAction((string)e.Parameter);

        private void LoadCharacterCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) => ShowOpenCharacterDialog();

        private void NewCharacterCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) => ShowCreateNewCharacterDialog();
        #endregion MenuItem Commands

        #region MenuItem Events
        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e) => Navigate(typeof(SettingsPage));
        #endregion

        #region UpdateCheck
        private async Task CheckVersion()
        {
            var github = new GitHubClient(new ProductHeaderValue("PnP-Organizer"));
            var latestRelease = (await github.Repository.Release.GetAll(563509297))[0];
            var latestVersion = new Version(latestRelease.TagName);
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

            Logger.Log($"Checking Version: {currentVersion} (Current) || {latestVersion} (Latest)");

            if(latestVersion > currentVersion)
            {
                Logger.Log($"{latestVersion} > {currentVersion} => update available!");
                await ShowUpdateDialog(latestRelease);
            }
            else
            {
                Logger.Log($"{latestVersion} = {currentVersion} => up-to-date!");
                await Task.CompletedTask;
            }
        }

        private async Task ShowUpdateDialog(Release latestRelease)
        {
            var dialog = (Dialog)_dialogControl;
            dialog.Tag = "update";
            dialog.ButtonLeftName = Properties.Resources.Dialog_ButtonUpdate;
            dialog.ButtonRightName = Properties.Resources.Dialog_ButtonCancel;
            dialog.ButtonLeftAppearance = Wpf.Ui.Common.ControlAppearance.Info;
            dialog.ButtonLeftClick += (sender, e) =>
            {
                if (((string)dialog.Tag) == "update")
                {
                    var ps = new ProcessStartInfo($"https://github.com/Piggo41/PnP-Organizer/releases/tag/{latestRelease.TagName}")
                    {
                        UseShellExecute = true,
                        Verb = "open"
                    };
                    Process.Start(ps);
                    dialog.Hide();
                }
            };

            await dialog.ShowAndWaitAsync(Properties.Resources.Dialog_UpdateTitle, Properties.Resources.Dialog_UpdateMessage);
        }

        #endregion UpdateCheck

        // TODO CharacterData Save/Load Dialogs maybe move to ViewModel
        #region CharacterData Save/Load Dialogs
        private void SaveCharacterCommandAction(string saveCommandParameter)
        {
            MvvmDataIO.LoadCharacterFromViewModels(RootNavigation.PageService!);
            if (saveCommandParameter == CommandParameters.SaveAs || Properties.Settings.Default.LastLoadedCharacter == "")
                ShowSaveCharacterDialog();
            else
            {
                FileIO.SaveLastLoadedCharacter();
                ShowSaveCharSnackbar();
            }
        }

        private void ShowSaveCharacterDialog()
        {
            SaveFileDialog saveCharacterDialog = new()
            {
                Title = Properties.Resources.Dialog_SaveCharacterTitle,
                Filter = FileIO.CharacterFileExtensionFilter,
                InitialDirectory = FileIO.CharacterDirectoryPath
            };
            if (saveCharacterDialog.ShowDialog() == true)
            {
                using var fs = (FileStream)saveCharacterDialog.OpenFile();
                FileIO.SaveCharacter(fs);
                ShowSaveCharSnackbar();
            }
        }

        private async Task ShowUnsavedCharacterDialog()
        {
            var dialog = (Dialog)_dialogControl;
            dialog.Tag = "unsavedChar";
            dialog.ButtonLeftName = Properties.Resources.Dialog_ButtonSaveChanges;
            dialog.ButtonRightName = Properties.Resources.Dialog_ButtonDiscardChanges;
            dialog.ButtonLeftAppearance = Wpf.Ui.Common.ControlAppearance.Info;
            dialog.ButtonLeftClick += (sender, e) =>
            {
                if(((string)dialog.Tag) == "unsavedChar")
                {
                    ShowSaveCharacterDialog();
                    dialog.Hide();
                }
            };

            await dialog.ShowAndWaitAsync(Properties.Resources.Dialog_UnsavedCharacterCloseTitle, Properties.Resources.Dialog_UnsavedCharacterCloseMessage);
        }

        private async void ShowOpenCharacterDialog()
        {
            if (!FileIO.IsCharacterSaved)
            {
                var dialog = (Dialog)_dialogControl;
                dialog.Tag = "openChar_save";
                dialog.ButtonLeftName = Properties.Resources.Dialog_ButtonSaveCharacter;
                dialog.ButtonRightName = Properties.Resources.Dialog_ButtonContinueWithoutSaving;
                dialog.ButtonLeftAppearance = Wpf.Ui.Common.ControlAppearance.Caution;
                dialog.ButtonLeftClick += (sender, e) =>
                {
                    if(((string)dialog.Tag) == "openChar_save")
                    {
                        ShowSaveCharacterDialog();
                        dialog.Hide();
                    }
                };

                await dialog.ShowAndWaitAsync(Properties.Resources.Dialog_OpenCharacterTitle, Properties.Resources.Dialog_UnsavedCharacterOpenMessage, true);
            }

            OpenFileDialog loadCharacterDialog = new()
            {
                Title = Properties.Resources.Dialog_OpenCharacterTitle,
                Filter = FileIO.CharacterFileExtensionFilter,
                InitialDirectory = FileIO.CharacterDirectoryPath
            };
            if (loadCharacterDialog.ShowDialog() == true)
            {
                using var fs = (FileStream)loadCharacterDialog.OpenFile();
                FileIO.LoadCharacter(fs);
                PostLoadCharacter();
            }
        }

        private async void ShowOpenLastCharacterDialog()
        {
            if (Properties.Settings.Default.LastLoadedCharacter != string.Empty)
            {
                var dialog = (Dialog)_dialogControl;
                dialog.Tag = "openLastChar";
                dialog.ButtonLeftName = Properties.Resources.Dialog_ButtonYes;
                dialog.ButtonRightName = Properties.Resources.Dialog_ButtonNo;
                dialog.ButtonLeftClick += (sender, e) =>
                {
                    if(((string)dialog.Tag) == "openLastChar")
                    {
                        FileIO.LoadLastCharacter();
                        PostLoadCharacter();
                        dialog.Hide();
                    }
                };

                await dialog.ShowAndWaitAsync(Properties.Resources.Dialog_OpenLastCharacterTitle, Properties.Resources.Dialog_OpenLastCharacterMessage, true);
            }
            else
                CreateAndLoadCharacter();
        }

        private async void ShowCreateNewCharacterDialog()
        {
            if (!FileIO.IsCharacterSaved)
            {
                var dialog = (Dialog)_dialogControl;
                dialog.Tag = "newChar";
                dialog.ButtonLeftName = Properties.Resources.Dialog_ButtonYes;
                dialog.ButtonRightName = Properties.Resources.Dialog_ButtonNo;
                dialog.ButtonLeftAppearance = Wpf.Ui.Common.ControlAppearance.Caution;
                dialog.ButtonLeftClick += (sender, e) =>
                {
                    if(((string)dialog.Tag) == "newChar")
                    {
                        CreateAndLoadCharacter();
                        FinishedLoading?.Invoke(this, new EventArgs());
                        dialog.Hide();
                    }
                };

                await dialog.ShowAndWaitAsync(Properties.Resources.Dialog_NewCharacterTitle, Properties.Resources.Dialog_UnsavedCharacterNewMessage, true);
            }
            else
                CreateAndLoadCharacter();
        }

        private void Dialog_ButtonRightClick(object sender, RoutedEventArgs e) => ((Dialog)sender).Hide();
        #endregion CharacterData Save/Load Dialogs

        // TODO CharacterData Save/Load move to ViewModel
        #region CharacterData Save/Load
        private void CreateAndLoadCharacter(bool showSnackbar = true)
        {
            FileIO.CreateNewCharacter();
            MvvmDataIO.LoadCharacterToViewModels(RootNavigation.PageService!);
            if(showSnackbar) 
                ShowNewCharSnackbar();
        }

        private void PostLoadCharacter()
        {
            MvvmDataIO.LoadCharacterToViewModels(RootNavigation.PageService!);
            FinishedLoading?.Invoke(this, new EventArgs());
            Navigate(typeof(OverviewPage));
            ShowLoadCharSnackbar();
        }
        #endregion CharacterData Save/Load

        #region Snackbars
        private async void ShowSaveCharSnackbar() => await _snackbarService.ShowAsync($"{Properties.Resources.Snackbar_CharacterSaved}:", $"\"{Properties.Settings.Default.LastLoadedCharacter}\"", Wpf.Ui.Common.SymbolRegular.Save28);

        private async void ShowLoadCharSnackbar() => await _snackbarService.ShowAsync($"{Properties.Resources.Snackbar_CharacterLoaded}:", $"\"{Properties.Settings.Default.LastLoadedCharacter}\"", Wpf.Ui.Common.SymbolRegular.FolderOpen24);

        private async void ShowNewCharSnackbar() => await _snackbarService.ShowAsync(Properties.Resources.Snackbar_CharacterNew, string.Empty, Wpf.Ui.Common.SymbolRegular.DocumentAdd48);
        #endregion Snackbars

        #region Other EventHandlers

        private void CloseMenuItem_Click(object sender, RoutedEventArgs e) => Close();
        #endregion Other EventHandlers
    }
}