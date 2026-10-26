using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Noteeditor.Services
{
    public static class CommandExecutor
    {
        public static void Execute(ICommand command, object parameter = null)
        {
            command.Execute(parameter);
        }

        public static void Execute<T>(IRelayCommand<T> command, T parameter)
        {
            command.Execute(parameter);
        }

        public static async Task ExecuteAsync<T>(IAsyncRelayCommand<T> command, T parameter = default)
        {
            await command.ExecuteAsync(parameter);
        }
    }
}
