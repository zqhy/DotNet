using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hy.Extensions;

public static class EnumExtensions
{
    public static T? GetAttribute<T>(this Enum enumSubItem, bool isInherit) where T : Attribute
    {
        return enumSubItem
            .GetType()
            .GetField(enumSubItem.ToString())?
            .GetCustomAttributes(typeof(T), isInherit)
            .Cast<T>()
            .FirstOrDefault();
    }

    public static string? GetDescription(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetAttribute<DescriptionAttribute>(isInherit)?.Description;
    }

    public static string? GetCategory(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetAttribute<CategoryAttribute>(isInherit)?.Category;
    }

    public static DisplayAttribute? GetDisplay(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetAttribute<DisplayAttribute>(isInherit);
    }

    public static string? GetDisplayShortName(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetDisplay(isInherit)?.ShortName;
    }

    public static string? GetDisplayName(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetDisplay(isInherit)?.Name;
    }

    public static string? GetDisplayPrompt(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetDisplay(isInherit)?.Prompt;
    }

    public static string? GetDisplayGroupName(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetDisplay(isInherit)?.GroupName;
    }

    public static int? GetDisplayOrder(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetDisplay(isInherit)?.Order;
    }
        
    public static T? GetEnumAttribute<T>(this Enum enumSubItem, bool isInherit) where T : Attribute
    {
        return enumSubItem
            .GetType()
            .GetCustomAttributes(typeof(T), isInherit)
            .Cast<T>()
            .FirstOrDefault();
    }

    public static string? GetEnumDescription(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetEnumAttribute<DescriptionAttribute>(isInherit)?.Description;
    }

    public static string? GetEnumCategory(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetEnumAttribute<CategoryAttribute>(isInherit)?.Category;
    }

    public static string? GetEnumDescriptionAndCategory(this Enum enumSubItem, bool isInherit = false)
    {
        var description = enumSubItem.GetEnumDescription(isInherit);
        var category = enumSubItem.GetEnumCategory(isInherit);
        if (description == null && category == null)
        {
            return null;
        }
            
        var stringBuilder = new StringBuilder();
        if (description != null)
        {
            stringBuilder.Append(description);
        }

        if (category != null)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(" : ");
            }
            stringBuilder.Append(category);
        }
        return stringBuilder.ToString();
    }

    public static DisplayAttribute? GetEnumDisplay(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetEnumAttribute<DisplayAttribute>(isInherit);
    }

    public static string? GetEnumDisplayShortName(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetEnumDisplay(isInherit)?.ShortName;
    }

    public static string? GetEnumDisplayName(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetEnumDisplay(isInherit)?.Name;
    }

    public static string? GetEnumDisplayPrompt(this Enum enumSubItem, bool isInherit = false)
    {
        return enumSubItem.GetEnumDisplay(isInherit)?.Prompt;
    }


    public static string ToRoleString<T>(this IEnumerable<T> enums) where T : Enum =>
        string.Join(",", enums.Select(p => p.ToString()));
}