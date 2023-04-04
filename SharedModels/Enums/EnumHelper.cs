using System.ComponentModel;
using System.Reflection;
namespace SharedModels.Enums;

public static class EnumHelper
{
    public static string GetDescription(this Enum value)
    {
        return value.GetType()
                   .GetMember(value.ToString())
                   .FirstOrDefault()
                   ?.GetCustomAttribute<DescriptionAttribute>()
                   ?.Description
               ?? value.ToString();
    }

    public static TEnum ParseEnumValue<TEnum>(string value)
    {
        foreach (var field in typeof(TEnum).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is not DescriptionAttribute
                descriptionAttribute) continue;
            if (descriptionAttribute.Description == value)
            {
                return (TEnum)field.GetValue(null);
            }
        }
        return (TEnum)Enum.Parse(typeof(TEnum), value, true);
    }
}