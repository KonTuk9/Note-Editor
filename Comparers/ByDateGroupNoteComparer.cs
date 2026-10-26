namespace Noteeditor.Comparers
{
    public class ByDateGroupNoteComparer : IComparer<NotesGroup>
    {
        public int Compare(NotesGroup x, NotesGroup y)
        {
            return y.Notes[0].CreationTime.CompareTo(x.Notes[0].CreationTime);
        }
    }
}
