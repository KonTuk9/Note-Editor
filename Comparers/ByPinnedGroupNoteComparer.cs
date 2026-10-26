using Noteeditor.Extensions;

namespace Noteeditor.Comparers
{
    public class ByPinnedGroupNoteComparer : IComparer<NotesGroup>
    {
        public int Compare(NotesGroup x, NotesGroup y)
        {
            int ix = x.Key == PinnedExpressions.Pinned ? 0 : 1;
            int iy = y.Key == PinnedExpressions.Pinned ? 0 : 1;

            return ix.CompareTo(iy);
        }
    }
}
