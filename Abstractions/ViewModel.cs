using CommunityToolkit.Mvvm.ComponentModel;

namespace Noteeditor.Abstractions
{
    public class ViewModel : ObservableObject
    {
        public T Cast<T>() where T : ViewModel
        {
            return (T)this;
        }
    }
}
