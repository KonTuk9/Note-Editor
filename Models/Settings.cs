using DynamicData;
using System.Text.Json.Serialization;
using Wpf.Ui.Appearance;

namespace Noteeditor.Models
{
    public class Settings
    {
        public ApplicationTheme Theme { get; set; } = ApplicationTheme.Light;
        public string CurrentIcon { get; set; } = string.Empty;

        [JsonIgnore]
        public SourceList<string> Icons { get; set; } = new SourceList<string>();
    }
}
