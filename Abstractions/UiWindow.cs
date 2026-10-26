using System.Windows.Media.Imaging;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Noteeditor.Abstractions
{
    public class UiWindow : FluentWindow
    {
        public ViewModel ViewModel { get; protected set; }

        public UiWindow(ViewModel viewModel, SettingsService settingsService)
        {
            DataContext = viewModel;
            ViewModel = viewModel;

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;

            settingsService.Bind(f => Icon = new BitmapImage(new Uri(f.CurrentIcon)));
            ApplicationThemeManager.Changed += (t, c) => WindowBackgroundManager.UpdateBackground(this, t, WindowBackdropType);
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e) { }
        protected virtual void OnLoaded(object sender, RoutedEventArgs e) { }

        public void ShowActivate()
        {
            if (!IsActive)
            {
                Activate();
            }

            Show();
        }
    }
}
