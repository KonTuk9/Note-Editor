namespace Noteeditor.Services
{
    public static class DataContextExtractor
    {
        public static T GetData<T>(object target)
        {
            if (target is FrameworkElement fe && fe.DataContext is T value)
            {
                return value;
            }

            throw new ArgumentException();
        }
    }
}
