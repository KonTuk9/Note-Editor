using Noteeditor.Pages.Home_Page;
using Wpf.Ui;

namespace Noteeditor.Windows.Main_Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UiWindow
    {
        public MainWindow(IServiceProvider serviceProvider, NavigationHeaderService navigationHeaderService, MainViewModel viewModel, SnackbarService snackbarService, DialogService dialogService, SettingsService settingsService) : base(viewModel, settingsService)
        {
            InitializeComponent();
            navigationHeaderService.SetNavigationView(navigationView);
            navigationView.SetServiceProvider(serviceProvider);
            snackbarService.SetSnackbarPresenter(snackbarPresenter);
            dialogService.SetDialogHost(dialogPresenter);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            navigationView.Navigate(typeof(HomePage));
        }
    }
}
