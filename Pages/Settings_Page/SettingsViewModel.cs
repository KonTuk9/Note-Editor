using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Linq;
using Wpf.Ui;
using Wpf.Ui.Appearance;

namespace Noteeditor.Pages.Settings_Page
{
    public partial class SettingsViewModel : ViewModel
    {
        private readonly SettingsService _settingsService;
        private readonly SnackbarService _snackbarService;
        private readonly Settings _settings;

        public ReadOnlyObservableCollection<string> Icons => _icons;
        private ReadOnlyObservableCollection<string> _icons;

        public SettingsViewModel(SettingsService settingsService, SnackbarService snackbarService)
        {
            _snackbarService = snackbarService;
            _settingsService = settingsService;

            _settingsService.Bind(out _settings, OnSettingsChanged, out _icons);
        }

        private void OnSettingsChanged(Settings settings)
        {
            DarkThemeEnabled = _settings.Theme == ApplicationTheme.Dark;
            SetProperty(ref _iconPath, settings.CurrentIcon, nameof(IconPath));
            UpdateValidation();
            if (settings.Icons.Items.Contains(IconPath))
            {
                CurrentIcon = settings.CurrentIcon;
            }
        }

        [ObservableProperty]
        private string _currentIcon;

        private string _iconPath = string.Empty;
        public string IconPath
        {
            get { return _iconPath; }
            set { SetProperty(ref _iconPath, value); SetIconPath(); UpdateValidation(); }
        }

        private void UpdateValidation()
        {
            OnPropertyChanged(nameof(IsIconPathValid));
            OnPropertyChanged(nameof(CanAddPath));
        }

        public bool IsIconPathValid => File.Exists(IconPath) && IconPath.EndsWith(".ico");
        public bool CanAddPath => IsIconPathValid && !_settings.Icons.Items.Contains(IconPath);

        private void SetIconPath()
        {
            if (IsIconPathValid)
            {
                App.Current.Dispatcher.InvokeAsync(() => _settingsService.SetIcon(IconPath));
            }
        }

        [RelayCommand]
        private async Task SetIcon(string path)
        {
            await _settingsService.SetIcon(path);
        }

        [RelayCommand]
        private async Task BrowseIcon()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                Multiselect = false,
                Filter = ".ico files | *.ico"
            };

            if (dialog.ShowDialog() == true)
            {
                IconPath = dialog.FileName;
            }
        }

        [RelayCommand]
        private async Task AddIcon()
        {
            _settingsService.AddIcon(IconPath);
        }

        [ObservableProperty]
        private bool _darkThemeEnabled;

        [RelayCommand]
        private async Task SetTheme()
        {
            await _settingsService.SetTheme(GetThemeFromBool(DarkThemeEnabled));
        }

        private ApplicationTheme GetThemeFromBool(bool value)
        {
            return value ? ApplicationTheme.Dark : ApplicationTheme.Light;
        }
    }
}
