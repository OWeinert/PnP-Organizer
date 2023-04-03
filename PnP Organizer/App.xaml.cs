using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PnP_Organizer.IO;
using PnP_Organizer.Models;
using PnP_Organizer.Services;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace PnP_Organizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static IHost? _host;

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T? GetService<T>()
            where T : class
        {
            return _host?.Services.GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                    .WriteTo.Console()
                    .WriteTo.Debug()
                    .WriteTo.File($"{FileIO.LogsDirectoryPath}\\log_.txt", rollingInterval: RollingInterval.Minute, fileSizeLimitBytes: 52428800)
                    .CreateLogger();

            _host = Host
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
                .ConfigureLogging((c, loggerBuilder) =>
                {
                    loggerBuilder.AddSerilog(dispose: true);
                })
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
                })
                .UseSerilog(dispose: true)
                .Build();

            await _host.StartAsync();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host!.StopAsync();
            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogException(e.Exception);
        }

        private static void LogException(Exception exception)
        {
            Log.Fatal(exception, "{exType}: {message}", exception.GetType(), exception.Message);
        }
    }
}