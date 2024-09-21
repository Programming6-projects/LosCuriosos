namespace DistributionCenter.Domain.Entities.Enums;

using System.ComponentModel;
using System.Reflection;

public static class Extensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());

        if (field == null)
        {
            return string.Empty;
        }

        if (field.GetCustomAttribute(typeof(DescriptionAttribute)) is not DescriptionAttribute attribute)
        {
            return value.ToString();
        }

        return attribute.Description;
    }
}
