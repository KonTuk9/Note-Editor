using Noteeditor.Windows.Confirm_Changes_Window;
using Noteeditor.Windows.Main_Window;
using Noteeditor.Windows.Note_Editor_Window;

namespace Noteeditor.Services
{
    public class WindowPool
    {
        private readonly WindowFactory _windowFactory;

        private MainWindow _mainWindow;

        public WindowPool(WindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public MainWindow GetMainWindow()
        {
            if (_mainWindow == null)
            {
                _mainWindow = _windowFactory.CreateWindow<MainWindow>();
                _mainWindow.Closed += (_, _) => _mainWindow = null;
            }

            return _mainWindow;
        }

        private Dictionary<Note, NoteEditorWindow> _noteWindows = new Dictionary<Note, NoteEditorWindow>();

        public NoteEditorWindow GetNoteEditorWindow(Note note)
        {
            ArgumentNullException.ThrowIfNull(note);

            if (_noteWindows.TryGetValue(note, out NoteEditorWindow window))
            {
                return window;
            }

            window = _windowFactory.CreateWindow<NoteEditorWindow>().WithNote(note);
            _noteWindows.Add(note, window);
            window.Closed += (_, _) => _noteWindows.Remove(note);

            return window;
        }

        public ConfirmChangesWindow GetConfirmChangesWindow()
        {
            ConfirmChangesWindow window = _windowFactory.CreateWindow<ConfirmChangesWindow>();

            return window;
        }
    }
}
