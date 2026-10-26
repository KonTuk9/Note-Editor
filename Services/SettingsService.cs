using DynamicData;
using System.Collections.ObjectModel;
using System.IO;
using Wpf.Ui.Appearance;

namespace Noteeditor.Services
{
    public class SettingsService : IInitializer
    {
        private event Action<Settings> SettingsChanged;

        private readonly FileService<Settings> _fileService;
        private Settings _settings;

        private const string c_directory = "Resources\\Icons";

        public SettingsService()
        {
            _fileService = new FileService<Settings>(() => new Settings(), "Settings.dat", "Settings");
            _settings = _fileService.Read();
        }

        public async Task Initialize()
        {
            await SetTheme(_settings.Theme);

            string[] files = Directory.GetFiles(c_directory, "*.ico");
            _settings.Icons.Edit(f =>
            {
                foreach (string file in files)
                {
                    _settings.Icons.Add(Path.GetFullPath(file));
                }
            });

            if (!File.Exists(_settings.CurrentIcon))
            {
                _settings.CurrentIcon = _settings.Icons.Items[0];
            }

            EnsureDirectoryCreated();
        }

        public void Bind(out Settings settings, Action<Settings> onChanged)
        {
            settings = _settings;
            Bind(onChanged);
        }

        public void Bind(out Settings settings, Action<Settings> onChanged, out ReadOnlyObservableCollection<string> collection)
        {
            Bind(out settings, onChanged);
            settings.Icons.Connect().Bind(out collection).Subscribe();
        }

        public void Bind(Action<Settings> onChanged)
        {
            ArgumentNullException.ThrowIfNull(onChanged);
            SettingsChanged += onChanged;
            OnSettingsChanged();
        }

        private void OnSettingsChanged()
        {
            SettingsChanged?.Invoke(_settings);
        }

        public async Task SetTheme(ApplicationTheme theme)
        {
            _settings.Theme = theme;
            ApplicationThemeManager.Apply(theme);
            await SaveChangesAsync();
        }

        public async Task SetIcon(string path)
        {
            _settings.CurrentIcon = path;
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            await _fileService.WriteAsync(_settings);
            OnSettingsChanged();
        }

        public void AddIcon(string path)
        {
            string newPath = Path.GetFullPath(Path.Combine(c_directory, Path.GetFileName(path)));
            File.Copy(path, newPath);

            _settings.Icons.Add(newPath);

            if (_settings.CurrentIcon == path)
            {
                _settings.CurrentIcon = newPath;
            }

            OnSettingsChanged();
        }

        private bool EnsureDirectoryCreated()
        {
            bool result = Directory.Exists(c_directory);

            if (!result)
            {
                Directory.CreateDirectory(c_directory);
            }

            return result;
        }
    }
}
