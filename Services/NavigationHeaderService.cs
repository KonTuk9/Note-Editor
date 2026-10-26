using Wpf.Ui.Controls;

namespace Noteeditor.Services
{
    public class NavigationHeaderService
    {
        private NavigationView _navigationView;

        public void SetNavigationView(NavigationView navigationView)
        {
            _navigationView = navigationView;
        }

        public void SetHeader(object header)
        {
            _navigationView.Header = header;
        }
    }
}
