using Microsoft.Extensions.DependencyInjection;
using Noteeditor.Controls.Dialogs;
using System.Windows.Controls;

namespace Noteeditor.Services
{
    public class DialogService
    {
        private ContentPresenter _dialogHost;
        private UiDialog _lastDialog;

        public void SetDialogHost(ContentPresenter dialogHost)
        {
            _dialogHost = dialogHost;
        }

        public void Show<T>() where T : UiDialog
        {
            ArgumentNullException.ThrowIfNull(_dialogHost);
            if (typeof(T) == typeof(RenameNoteDialog))
            {
                throw new InvalidOperationException($"{nameof(RenameNoteDialog)} is not acceptable. Use {nameof(ShowNoteRename)} method instead");
            }

            if (_lastDialog?.GetType() == typeof(T) && _dialogHost.Content != null)
            {
                _lastDialog.Show();
                return;
            }

            UiDialog dialog = App.ServiceProvider.GetRequiredService<T>();
            SetDialog(dialog);

            dialog.Show();
        }

        public void ShowNoteRename(Note note)
        {
            ArgumentNullException.ThrowIfNull(_dialogHost);

            UiDialog dialog = App.ServiceProvider.GetRequiredService<RenameNoteDialog>().WithNote(note);
            SetDialog(dialog);

            dialog.Show();
        }

        private void SetDialog(UiDialog dialog)
        {
            _dialogHost.Content = dialog;
            _lastDialog = dialog;
        }
    }
}
