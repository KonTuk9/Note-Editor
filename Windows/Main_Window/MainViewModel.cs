using CommunityToolkit.Mvvm.ComponentModel;
using Noteeditor.Pages.About_Page;
using Noteeditor.Pages.Home_Page;
using Noteeditor.Pages.Settings_Page;
using System.Windows.Media.Imaging;
using Wpf.Ui.Controls;

namespace Noteeditor.Windows.Main_Window
{
    public partial class MainViewModel : ViewModel
    {
        public object[] Menu { get; } =
        [
            new NavigationViewItem("Home", SymbolRegular.Home16, typeof(HomePage)),
            new NavigationViewItem("Settings", SymbolRegular.Settings16, typeof(SettingsPage)),
        ];

        public object[] FooterMenu { get; } =
        [
            new NavigationViewItem("About", SymbolRegular.QuestionCircle16, typeof(AboutPage)),
        ];

        [ObservableProperty]
        private IconElement _currentIcon;

        public MainViewModel(SettingsService settingsService)
        {
            settingsService.Bind(out var settings, s =>
            {
                CurrentIcon = new ImageIcon()
                {
                    Source = new BitmapImage(new Uri(s.CurrentIcon))
                };
            });
        }
    }
}
