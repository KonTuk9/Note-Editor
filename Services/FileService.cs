using Newtonsoft.Json;
using System.IO;

namespace Noteeditor.Services
{
    public class FileService<TObject>
    {
        private readonly string _directory;
        private readonly Func<TObject> _defaultValueFactory;

        public string FilePath { get; }

        public FileService(Func<TObject> defaultValueFactory, string fileName, string directory = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
            ArgumentNullException.ThrowIfNull(defaultValueFactory);

            _directory = directory;
            _defaultValueFactory = defaultValueFactory;

            if (!string.IsNullOrWhiteSpace(directory))
            {
                FilePath = Path.GetFullPath(Path.Combine(directory, fileName));
            }
            else
            {
                FilePath = Path.GetFullPath(fileName);
            }

            EnsureCreated();
        }

        private bool EnsureCreated()
        {
            if (!string.IsNullOrWhiteSpace(_directory) && !Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(_defaultValueFactory.Invoke()));

                return false;
            }

            return true;
        }

        public TObject Read()
        {
            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<TObject>(json);
        }

        public async Task<TObject> ReadAsync()
        {
            string json = await File.ReadAllTextAsync(FilePath);
            return JsonConvert.DeserializeObject<TObject>(json);
        }

        public async Task WriteAsync(TObject value)
        {
            string json = JsonConvert.SerializeObject(value);
            await File.WriteAllTextAsync(FilePath, json);
        }
    }
}
