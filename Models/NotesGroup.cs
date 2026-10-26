using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using System.Collections.ObjectModel;

namespace Noteeditor.Models
{
    public class NotesGroup : ObservableObject, IDisposable
    {
        public string Key { get; }

        private readonly ReadOnlyObservableCollection<Note> _notes;
        public ReadOnlyObservableCollection<Note> Notes => _notes;

        private readonly IDisposable _binding;

        public NotesGroup(IGroup<Note, int, string> group, IObservable<IComparer<Note>> sort)
        {
            Key = group.Key;

            _binding = group.Cache.Connect().SortAndBind(out _notes, sort).Subscribe();
        }

        public void Dispose()
        {
            _binding.Dispose();
        }
    }
}
