using Wpf.Ui;

namespace Noteeditor.Windows.Note_Editor_Window
{
    /// <summary>
    /// Interaction logic for NoteEditorWindow.xaml
    /// </summary>
    public partial class NoteEditorWindow : UiWindow
    {
        public NoteEditorWindow(NoteEditorViewModel viewModel, SnackbarService snackbarService, SettingsService settingsService) : base(viewModel, settingsService)
        {
            Closing += viewModel.ConfirmChangesOnClosing;

            InitializeComponent();
            snackbarService.SetSnackbarPresenter(snackbarPresenter);

            textBox.Focus();
        }

        public NoteEditorWindow WithNote(Note note)
        {
            ViewModel.Cast<NoteEditorViewModel>().Note = note;
            return this;
        }
    }
}
