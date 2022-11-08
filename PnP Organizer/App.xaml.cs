using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PnP_Organizer.Core;
using PnP_Organizer.Core.Character;
using PnP_Organizer.Logging;
using PnP_Organizer.Models;
using PnP_Organizer.Services;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace PnP_Organizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
            .ConfigureServices((context, services) =>
            {
                // App Host
                services.AddHostedService<ApplicationHostService>();

                // Services
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<IThemeService, ThemeService>();
                services.AddSingleton<ITaskBarService, TaskBarService>(); 
                services.AddSingleton<IDialogService, DialogService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<INavigationService, NavigationService>();

                // Main window container with navigation
                services.AddScoped<INavigationWindow, Views.Container>();
                services.AddScoped<ViewModels.ContainerViewModel>();

                // Views and ViewModels
                services.AddScoped<Views.Pages.DashboardPage>();
                services.AddScoped<ViewModels.DashboardViewModel>();

                services.AddScoped<Views.Pages.OverviewPage>();
                services.AddScoped<ViewModels.OverviewViewModel>();
                services.AddScoped<Views.Pages.InventoryPage>();
                services.AddScoped<ViewModels.InventoryViewModel>();
                services.AddScoped<Views.Pages.SkillsPage>();
                services.AddScoped<ViewModels.SkillsViewModel>();
                services.AddScoped<Views.Pages.AttributeTestsPage>();
                services.AddScoped<ViewModels.AttributeTestsViewModel>();
                services.AddScoped<Views.Pages.CalculatorPage>();
                services.AddScoped<ViewModels.CalculatorViewModel>();
                services.AddScoped<Views.Pages.NotesPage>();
                services.AddScoped<ViewModels.NotesViewModel>();

                services.AddScoped<Views.Pages.SettingsPage>();
                services.AddScoped<ViewModels.SettingsViewModel>();

                // Configuration
                services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
            }).Build();

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T? GetService<T>()
            where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }
    }
}