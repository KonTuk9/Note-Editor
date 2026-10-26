using System.Windows.Controls;

namespace Noteeditor.Abstractions
{
    public class UiPage : Page
    {
        public ViewModel ViewModel { get; protected set; }

        public UiPage(ViewModel viewModel)
        {
            DataContext = viewModel;
            ViewModel = viewModel;

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        protected virtual void OnUnloaded(object sender, System.Windows.RoutedEventArgs e) { }
        protected virtual void OnLoaded(object sender, System.Windows.RoutedEventArgs e) { }
    }
}
