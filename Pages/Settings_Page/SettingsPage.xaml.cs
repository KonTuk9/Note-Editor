namespace Noteeditor.Pages.Settings_Page
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UiPage
    {
        private NavigationHeaderService _navigationHeaderService;

        public SettingsPage(SettingsViewModel viewModel, NavigationHeaderService navigationHeaderService) : base(viewModel)
        {
            _navigationHeaderService = navigationHeaderService;

            InitializeComponent();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            _navigationHeaderService.SetHeader(Resources["Header"]);
        }

        private async void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            await CommandExecutor.ExecuteAsync(ViewModel.Cast<SettingsViewModel>().SetIconCommand, e.AddedItems[0].ToString());
        }
    }
}
