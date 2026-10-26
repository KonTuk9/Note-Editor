namespace Noteeditor.Pages.About_Page
{
    /// <summary>
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    public partial class AboutPage : UiPage
    {
        private NavigationHeaderService _navigationHeaderService;

        public AboutPage(AboutViewModel viewModel, NavigationHeaderService navigationHeaderService) : base(viewModel)
        {
            _navigationHeaderService = navigationHeaderService;

            InitializeComponent();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            _navigationHeaderService.SetHeader(Resources["Header"]);
        }
    }
}
