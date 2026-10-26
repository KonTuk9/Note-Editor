using System.Windows.Input;

namespace Noteeditor.Pages.Home_Page
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UiPage
    {
        private readonly NavigationHeaderService _navigationHeaderService;

        public HomePage(HomeViewModel viewModel, NavigationHeaderService navigationHeaderService) : base(viewModel)
        {
            _navigationHeaderService = navigationHeaderService;

            InitializeComponent();

            (Resources["Header"] as FrameworkElement).DataContext = viewModel;
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            _navigationHeaderService.SetHeader(Resources["Header"]);
        }

        private void OnItemMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.ChangedButton == MouseButton.Left && sender is FrameworkElement fe)
            {
                CommandExecutor.Execute(ViewModel.Cast<HomeViewModel>().OpenNoteCommand, fe.DataContext);
            }
        }
    }
}
