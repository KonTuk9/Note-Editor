using CommunityToolkit.Mvvm.ComponentModel;

namespace Noteeditor.Models
{
    public class Note : ObservableObject
    {
        public int Id
        {
            get { return field; }
            set { field = value; }
        }

        public string Name
        {
            get { return field; }
            set { SetProperty(ref field, value); }
        }

        public string Content
        {
            get { return field; }
            set { SetProperty(ref field, value); }
        }

        public bool Pinned
        {
            get { return field; }
            set { SetProperty(ref field, value); }
        }

        public DateTime CreationTime
        {
            get { return field; }
            set { SetProperty(ref field, value); }
        } = DateTime.Now;

        public DateTime LastEditTime
        {
            get { return field; }
            set { SetProperty(ref field, value); }
        } = DateTime.Now;
    }
}
