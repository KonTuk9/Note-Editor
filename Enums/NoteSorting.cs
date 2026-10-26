using System.ComponentModel;

namespace Noteeditor.Enums
{
    public enum NoteSorting
    {
        [Description("Sort by date")]
        ByDate = 0,
        [Description("Sort by name")]
        ByName = 1,
        [Description("Sort by pinned")]
        ByPinned = 2,
        [Description("Sort with no group")]
        ByNoGroup = 3,
    }
}
