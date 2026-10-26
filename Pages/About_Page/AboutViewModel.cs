using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Reflection;

namespace Noteeditor.Pages.About_Page
{
    public partial class AboutViewModel : ViewModel
    {
        public string Version => Assembly.GetAssembly(typeof(App)).ImageRuntimeVersion;

        [RelayCommand]
        private void OpenLink(string link)
        {
            Process.Start(new ProcessStartInfo(link)
            {
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}
