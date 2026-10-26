using CommunityToolkit.Mvvm.ComponentModel;

namespace Noteeditor.Controls.Dialogs
{
    /// <summary>
    /// Interaction logic for RenameNoteDialog.xaml
    /// </summary>
    [ObservableObject]
    public partial class RenameNoteDialog : UiDialog
    {
        private readonly NoteService _noteService;
        private Note _note;

        public RenameNoteDialog(NoteService noteService)
        {
            _noteService = noteService;

            InitializeComponent();
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanSubmit))]
        private string _noteName;

        public override bool CanSubmit => !string.IsNullOrWhiteSpace(NoteName);

        protected override void Submit()
        {
            base.Submit();

            Dispatcher.InvokeAsync(async () =>
            {
                await _noteService.EditAsync(_note, NoteName);
            });
        }

        public RenameNoteDialog WithNote(Note note)
        {
            _note = note;
            NoteName = _note.Name;
            return this;
        }
    }
}
