using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Noteeditor.Enums;
using System.Windows.Media.Imaging;
using Wpf.Ui.Controls;

namespace Noteeditor.Windows.Confirm_Changes_Window
{
    public partial class ConfirmChangesViewModel : ViewModel
    {
        [ObservableProperty]
        private ImageIcon _currentIcon;

        public ConfirmChangesViewModel(SettingsService settingsService)
        {
            settingsService.Bind(out var settings, s =>
            {
                CurrentIcon = new ImageIcon()
                {
                    Source = new BitmapImage(new Uri(s.CurrentIcon))
                };
            });
        }

        [RelayCommand]
        private void Confirm(ConfirmChangesWindow window)
        {
            window.Result = DialogResult.Confirm;
            window.Close();
        }

        [RelayCommand]
        private void Cancel(ConfirmChangesWindow window)
        {
            window.Result = DialogResult.Cancel;
            window.Close();
        }

        [RelayCommand]
        private void Close(ConfirmChangesWindow window)
        {
            window.Result = DialogResult.Close;
            window.Close();
        }
    }
}
