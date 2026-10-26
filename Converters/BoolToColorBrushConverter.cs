using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Noteeditor.Converters
{
    public class BoolToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolean)
            {
                return new SolidColorBrush(boolean ? Colors.White : Colors.Black);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
