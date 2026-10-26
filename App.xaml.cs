using Microsoft.Extensions.DependencyInjection;
using Noteeditor.Controls.Dialogs;
using Noteeditor.Pages.About_Page;
using Noteeditor.Pages.Home_Page;
using Noteeditor.Pages.Settings_Page;
using Noteeditor.Windows.Confirm_Changes_Window;
using Noteeditor.Windows.Main_Window;
using Noteeditor.Windows.Note_Editor_Window;
using Wpf.Ui;

namespace Noteeditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        private void Configure()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<Bootstrapper>()
                //UI : Windows
                .AddTransient<MainWindow>()
                .AddTransient<MainViewModel>()

                .AddTransient<NoteEditorWindow>()
                .AddTransient<NoteEditorViewModel>()

                .AddTransient<ConfirmChangesWindow>()
                .AddTransient<ConfirmChangesViewModel>()
                //UI : Pages
                .AddTransient<HomePage>()
                .AddTransient<HomeViewModel>()

                .AddTransient<SettingsPage>()
                .AddTransient<SettingsViewModel>()

                .AddTransient<AboutPage>()
                .AddTransient<AboutViewModel>()
                //Services
                .AddSingleton<WindowFactory>()
                .AddSingleton<WindowPool>()

                .AddSingleton<NoteService>()
                .AddSingleton<SettingsService>()
                .AddSingleton<NavigationHeaderService>()

                .AddScoped<SnackbarService>()
                .AddScoped<DialogService>()
                .AddScoped<NavigationHeaderService>()
                //Dialogs
                .AddTransient<CreateNoteDialog>()
                .AddTransient<RenameNoteDialog>();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Configure();

            Bootstrapper bootstrapper = ServiceProvider.GetRequiredService<Bootstrapper>();
            Dispatcher.InvokeAsync(bootstrapper.Bootstrap);
        }
    }
}
