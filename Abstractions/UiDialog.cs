using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace Noteeditor.Abstractions
{
    public partial class UiDialog : UserControl
    {
        public void Show()
        {
            Visibility = Visibility.Visible;
        }

        public virtual bool CanSubmit => true;

        [RelayCommand]
        protected virtual void Submit()
        {
            Visibility = Visibility.Collapsed;
        }

        [RelayCommand]
        protected virtual void Close()
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
