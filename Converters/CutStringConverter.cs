using System.Globalization;
using System.Windows.Data;

namespace Noteeditor.Converters
{
    public class CutStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int cap = parameter is int intparam ? intparam : 50;

            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return "Empty...";
            }

            if (value is string str)
            {
                ReadOnlySpan<char> span = str.AsSpan();
                int index = span.IndexOfAny(['\n', '\r']);

                if (index > -1)
                {
                    str = span.Slice(0, index).ToString();
                }

                if (str.Length < cap)
                {
                    return str;
                }
                else
                {
                    return str.Substring(0, cap) + "...";
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
