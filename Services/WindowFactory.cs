using Microsoft.Extensions.DependencyInjection;

namespace Noteeditor.Services
{
    public class WindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowFactory(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public T CreateWindow<T>() where T : Window
        {
            IServiceScope scope = _serviceProvider.CreateScope();

            T window = scope.ServiceProvider.GetRequiredService<T>();
            window.Closed += (_, _) => scope.Dispose();


            return window;
        }
    }
}
