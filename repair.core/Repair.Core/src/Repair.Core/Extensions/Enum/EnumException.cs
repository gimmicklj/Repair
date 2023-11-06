using System.ComponentModel;
using System.Reflection;

namespace Repair.Core.Extensions.Enum;

public static class EnumException
{
    public static string GetDescription(object value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
        if (attribute != null)
        {
            return attribute.Description;
        }

        return value.ToString();
    }
}
