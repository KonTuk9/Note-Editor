using CommunityToolkit.Mvvm.ComponentModel;

namespace Noteeditor.Controls.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateNoteDialog.xaml
    /// </summary>
    [ObservableObject]
    public partial class CreateNoteDialog : UiDialog
    {
        private readonly NoteService _noteService;
        private readonly WindowPool _windowPool;

        public CreateNoteDialog(NoteService noteService, WindowPool windowPool)
        {
            _windowPool = windowPool;
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
                Note note = new Note()
                {
                    Name = NoteName
                };

                await _noteService.CreateAsync(note);

                _windowPool.GetNoteEditorWindow(note).Show();
                NoteName = string.Empty;
            });
        }
    }
}
