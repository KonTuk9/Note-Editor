using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;
using Noteeditor.Comparers;
using Noteeditor.Controls.Dialogs;
using Noteeditor.Enums;
using Noteeditor.Extensions;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Noteeditor.Pages.Home_Page
{
    public partial class HomeViewModel : ViewModel
    {
        private readonly NoteService _noteService;
        private readonly SnackbarService _snackbarService;
        private readonly DialogService _dialogService;
        private readonly WindowPool _windowPool;

        private ReadOnlyObservableCollection<NotesGroup> _notes;
        public ReadOnlyObservableCollection<NotesGroup> Notes => _notes;

        public HomeViewModel(NoteService noteService, SnackbarService snackbarService, DialogService dialogService, WindowPool windowPool)
        {
            _windowPool = windowPool;
            _noteService = noteService;
            _dialogService = dialogService;
            _snackbarService = snackbarService;

            var grouping = this.WhenValueChanged(f => f.SortingMode).Select(f => f);

            _noteService.Notes.Connect().Filter(GetFilter()).Group(GetGrouping()).Transform(f => new NotesGroup(f, GetNotesSorting())).SortAndBind(out _notes, GetGroupSorting()).DisposeMany().Subscribe();
        }

        [ObservableProperty]
        private string _searchEntry = string.Empty;

        private IObservable<Func<Note, bool>> GetFilter()
        {
            Func<string, Func<Note, bool>> func = f => x => x.Name.Contains(f, StringComparison.OrdinalIgnoreCase);
            return this.WhenValueChanged(f => f.SearchEntry).Select(func);
        }

        [ObservableProperty]
        private NoteSorting _sortingMode;

        private readonly SortExpressionComparer<Note> _nameSort = SortExpressionComparer<Note>.Descending(f => f.Pinned).ThenByAscending(f => f.Name);
        private readonly SortExpressionComparer<Note> _defaultSort = SortExpressionComparer<Note>.Descending(f => f.Pinned).ThenByAscending(f => f.CreationTime);

        private IObservable<Func<Note, string>> GetGrouping()
        {
            return this.WhenValueChanged(f => f.SortingMode).Select(f =>
            {
                return (Func<Note, string>)(f switch
                {
                    NoteSorting.ByName => note => char.ToUpper(note.Name.AsSpan()[0]).ToString(),
                    NoteSorting.ByDate => note => note.CreationTime.ToStringRelative(),
                    NoteSorting.ByPinned => note => note.Pinned ? PinnedExpressions.Pinned : PinnedExpressions.Other,
                    _ => _ => string.Empty,
                });
            });
        }

        private IObservable<IComparer<Note>> GetNotesSorting()
        {
            return this.WhenValueChanged(f => f.SortingMode).Select(f =>
            {
                if (f == NoteSorting.ByName)
                {
                    return _nameSort;
                }

                return _defaultSort;
            });
        }

        private readonly SortExpressionComparer<NotesGroup> _groupNameSort = SortExpressionComparer<NotesGroup>.Ascending(f => f.Key);
        private readonly IComparer<NotesGroup> _groupPinnedSort = new ByPinnedGroupNoteComparer();
        private readonly IComparer<NotesGroup> _groupDateSort = new ByDateGroupNoteComparer();

        public IObservable<IComparer<NotesGroup>> GetGroupSorting()
        {
            return this.WhenValueChanged(f => f.SortingMode).Select(f =>
            {
                if (SortingMode == NoteSorting.ByPinned)
                {
                    return _groupPinnedSort;
                }
                else if (SortingMode == NoteSorting.ByDate)
                {
                    return _groupDateSort;
                }
                return _groupNameSort;
            });
        }

        [RelayCommand]
        private async Task DeleteNote(Note note)
        {
            await _noteService.DeleteAsync(note);
            _snackbarService.Show("Deleted note", $"Note {note.Name} has been deleted.", ControlAppearance.Info, new SymbolIcon(SymbolRegular.Info16, fontSize: 24), TimeSpan.FromSeconds(2));
        }

        [RelayCommand]
        private async Task PinNote(Note note)
        {
            await _noteService.EditAsync(note, pinned: note.Pinned);
        }

        [RelayCommand]
        private void OpenNote(Note note)
        {
            App.Current.Dispatcher.InvokeAsync(_windowPool.GetNoteEditorWindow(note).ShowActivate);
        }

        [RelayCommand]
        private void CreateNote()
        {
            _dialogService.Show<CreateNoteDialog>();
        }

        [RelayCommand]
        private void RenameNote(Note note)
        {
            _dialogService.ShowNoteRename(note);
        }
    }
}
