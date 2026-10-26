using System.ComponentModel;
using System.Reflection;

namespace Noteeditor.Extensions
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            return value.GetType().GetField(value.ToString()).GetCustomAttribute<DescriptionAttribute>().Description;
        }

        public static IEnumerable<KeyValuePair<object, object>> GetValues(Type type)
        {
            return Enum.GetValues(type).Cast<Enum>().Select(f => new KeyValuePair<object, object>(f, f.GetDescription())).ToList();
        }
    }
}
