using Noteeditor.Enums;

namespace Noteeditor.Windows.Confirm_Changes_Window
{
    /// <summary>
    /// Interaction logic for ConfirmChangesWindow.xaml
    /// </summary>
    public partial class ConfirmChangesWindow : UiWindow
    {
        public DialogResult Result { get; set; } = Enums.DialogResult.Close;

        public ConfirmChangesWindow(ConfirmChangesViewModel viewModel, SettingsService settingsService) : base(viewModel, settingsService)
        {
            InitializeComponent();
        }

        public ConfirmChangesWindow WithTitle(string title)
        {
            titleBar.Title = title;
            return this;
        }
    }
}
