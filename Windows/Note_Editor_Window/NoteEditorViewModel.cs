using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Noteeditor.Enums;
using Noteeditor.Windows.Confirm_Changes_Window;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Noteeditor.Windows.Note_Editor_Window
{
    public partial class NoteEditorViewModel : ViewModel
    {
        public Note Note
        {
            get { return field; }
            set { SetProperty(ref field, value); Text = field.Content; }
        }

        private readonly NoteService _noteService;
        private readonly WindowPool _windowPool;
        private readonly SnackbarService _snackbarService;

        [ObservableProperty]
        private IconElement _currentIcon;

        public NoteEditorViewModel(NoteService noteService, WindowPool windowPool, SnackbarService snackbarService, SettingsService settingsService)
        {
            _windowPool = windowPool;
            _noteService = noteService;
            _snackbarService = snackbarService;

            settingsService.Bind(out var settings, s =>
            {
                CurrentIcon = new ImageIcon()
                {
                    Source = new BitmapImage(new Uri(s.CurrentIcon))
                };
            });
        }

        [ObservableProperty]
        private string _text;

        [RelayCommand]
        private void OpenStartWindow()
        {
            _windowPool.GetMainWindow().ShowActivate();
        }

        [RelayCommand]
        private void Save()
        {
            _snackbarService.Show("Saved!", "All changes are applied to note successfuly.", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16, fontSize: 24), TimeSpan.FromSeconds(2));

            SaveChanges();
        }

        private void SaveChanges()
        {
            App.Current.Dispatcher.InvokeAsync(async () =>
            {
                await _noteService.EditAsync(Note, content: Text);
            });
        }


        [RelayCommand]
        private void Exit(Window window)
        {
            window.Close();
        }

        public void ConfirmChangesOnClosing(object sender, CancelEventArgs e)
        {
            if (Note.Content == Text)
            {
                return;
            }

            ConfirmChangesWindow window = _windowPool.GetConfirmChangesWindow().WithTitle(Note.Name);
            window.ShowDialog();

            if (window.Result == DialogResult.Confirm)
            {
                SaveChanges();
            }
            else if (window.Result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
