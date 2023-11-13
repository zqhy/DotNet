using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Hy.Helper;
using Hy.Utils;

namespace Hy.Extensions;

public static class AttributeExtensions
{
    #region Type Base Attribute

    public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type type, bool isInherit) =>
        type.GetCustomAttributes(typeof(TAttribute), isInherit).Cast<TAttribute>();
        
    public static TAttribute? GetAttribute<TAttribute>(this Type type, bool isInherit)
        where TAttribute : Attribute =>
        type.GetAttributes<TAttribute>(isInherit).FirstOrDefault();

    public static bool IsHasAttribute<TAttribute>(this Type type, bool isInherit)
        where TAttribute : Attribute => 
        type.GetAttribute<TAttribute>(isInherit) != null;

    public static bool IsHasRequiredAttribute(this Type type, bool isInherit = false) => 
        type.IsHasAttribute<RequiredAttribute>(isInherit);
        
    public static string? GetDescription(this Type type, bool isInherit = false)
    {
        return type.GetAttribute<DescriptionAttribute>(isInherit)?.Description;
    }

    public static string? GetCategory(this Type type, bool isInherit = false)
    {
        return type.GetAttribute<CategoryAttribute>(isInherit)?.Category;
    }

    public static DisplayAttribute? GetDisplay(this Type type, bool isInherit = false)
    {
        return type.GetAttribute<DisplayAttribute>(isInherit);
    }

    public static string? GetDisplayShortName(this Type type, bool isInherit = false)
    {
        return type.GetDisplay(isInherit)?.ShortName;
    }

    public static string? GetDisplayName(this Type type, bool isInherit = false)
    {
        return type.GetDisplay(isInherit)?.Name;
    }

    public static string? GetDisplayPrompt(this Type type, bool isInherit = false)
    {
        return type.GetDisplay(isInherit)?.Prompt;
    }

    public static string? GetDisplayGroupName(this Type type, bool isInherit = false)
    {
        return type.GetDisplay(isInherit)?.GroupName;
    }

    public static int? GetDisplayOrder(this Type type, bool isInherit = false)
    {
        return type.GetDisplay(isInherit)?.Order;
    }

    #endregion

    #region Property Base Attribute

    public static IEnumerable<TAttribute>? GetAttributes<TAttribute>(this Type type, string propertyName, bool isInherit) =>
        type.GetProperties()
            .FirstOrDefault(p => p.Name == propertyName)?
            .GetCustomAttributes(typeof(TAttribute), isInherit)
            .Cast<TAttribute>();

    public static TAttribute? GetAttribute<TAttribute>(this Type type, string propertyName, bool isInherit)
        where TAttribute : Attribute =>
        type.GetAttributes<TAttribute>(propertyName, isInherit)?.FirstOrDefault();

    public static bool IsHasAttribute<TAttribute>(this Type type, string propertyName, bool isInherit)
        where TAttribute : Attribute => 
        type.GetAttribute<TAttribute>(propertyName, isInherit) != null;

    public static bool IsHasRequiredAttribute(this Type type, string propertyName, bool isInherit = false) => 
        type.IsHasAttribute<RequiredAttribute>(propertyName, isInherit);
        
    public static string? GetDescription(this Type type, string propertyName, bool isInherit = false)
    {
        return type.GetAttribute<DescriptionAttribute>(propertyName, isInherit)?.Description;
    }

    public static string? GetCategory(this Type type, string propertyName, bool isInherit = false)
    {
        return type.GetAttribute<CategoryAttribute>(propertyName, isInherit)?.Category;
    }

    public static DisplayAttribute? GetDisplay(this Type type, string propertyName, bool isInherit = false)
    {
        return type.GetAttribute<DisplayAttribute>(propertyName, isInherit);
    }

    public static string? GetDisplayShortName(this Type type, string propertyName, bool isInherit = false)
    {
        return type.GetDisplay(propertyName, isInherit)?.ShortName;
    }

    public static string? GetDisplayName(this Type type, string propertyName, bool isInherit = false)
    {
        return type.GetDisplay(propertyName, isInherit)?.Name;
    }

    public static string? GetDisplayPrompt(this Type type, string propertyName, bool isInherit = false)
    {
        return type.GetDisplay(propertyName, isInherit)?.Prompt;
    }

    public static string? GetDisplayGroupName(this Type type, string propertyName, bool isInherit = false)
    {
        return type.GetDisplay(propertyName, isInherit)?.GroupName;
    }

    public static int? GetDisplayOrder(this Type type, string propertyName, bool isInherit = false)
    {
        return type.GetDisplay(propertyName, isInherit)?.Order;
    }

    #endregion

    #region Field Base Attribute

    public static IEnumerable<TAttribute>? GetFieldAttributes<TAttribute>(this Type type, string fieldName, bool isInherit) =>
        type.GetField(fieldName)?
            .GetCustomAttributes(typeof(TAttribute), isInherit)
            .Cast<TAttribute>();

    public static TAttribute? GetFieldAttribute<TAttribute>(this Type type, string fieldName, bool isInherit)
        where TAttribute : Attribute =>
        type.GetFieldAttributes<TAttribute>(fieldName, isInherit)?.FirstOrDefault();

    public static bool IsHasFieldAttribute<TAttribute>(this Type type, string fieldName, bool isInherit)
        where TAttribute : Attribute => 
        type.GetFieldAttribute<TAttribute>(fieldName, isInherit) != null;

    public static bool IsHasRequiredFieldAttribute(this Type type, string fieldName, bool isInherit = false) => 
        type.IsHasFieldAttribute<RequiredAttribute>(fieldName, isInherit);
        
    public static string? GetFieldDescription(this Type type, string fieldName, bool isInherit = false)
    {
        return type.GetFieldAttribute<DescriptionAttribute>(fieldName, isInherit)?.Description;
    }

    public static string? GetFieldCategory(this Type type, string fieldName, bool isInherit = false)
    {
        return type.GetFieldAttribute<CategoryAttribute>(fieldName, isInherit)?.Category;
    }

    public static DisplayAttribute? GetFieldDisplay(this Type type, string fieldName, bool isInherit = false)
    {
        return type.GetFieldAttribute<DisplayAttribute>(fieldName, isInherit);
    }

    public static string? GetFieldDisplayShortName(this Type type, string fieldName, bool isInherit = false)
    {
        return type.GetFieldDisplay(fieldName, isInherit)?.ShortName;
    }

    public static string? GetFieldDisplayName(this Type type, string fieldName, bool isInherit = false)
    {
        return type.GetFieldDisplay(fieldName, isInherit)?.Name;
    }

    public static string? GetFieldDisplayPrompt(this Type type, string fieldName, bool isInherit = false)
    {
        return type.GetFieldDisplay(fieldName, isInherit)?.Prompt;
    }

    #endregion
        
    #region Property accessor

    public static IEnumerable<TAttribute>? GetAttributes<TAttribute>(this Expression<Func<dynamic?>> accessor, bool isInherit)
        where TAttribute : Attribute
    {
        var fieldIdentifier = FieldIdentifier.Create(accessor);
        return fieldIdentifier.Model.GetType().GetAttributes<TAttribute>(fieldIdentifier.FieldName, isInherit);
    }
        
    public static TAttribute? GetAttribute<TAttribute>(this Expression<Func<dynamic?>> accessor, bool isInherit)
        where TAttribute : Attribute =>
        accessor.GetAttributes<TAttribute>(isInherit)?.FirstOrDefault();

    public static bool IsHasAttribute<TAttribute>(this Expression<Func<dynamic?>> accessor, bool isInherit)
        where TAttribute : Attribute => 
        accessor.GetAttribute<TAttribute>(isInherit) != null;

    public static bool IsHasRequiredAttribute(this Expression<Func<dynamic?>> accessor, bool isInherit = false) => 
        accessor.IsHasAttribute<RequiredAttribute>(isInherit);

    public static string? GetDescription(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetAttribute<DescriptionAttribute>(isInherit)?.Description;

    public static string? GetCategory(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetAttribute<CategoryAttribute>(isInherit)?.Category;

    public static DisplayAttribute? GetDisplay(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetAttribute<DisplayAttribute>(isInherit);

    public static string? GetDisplayShortName(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetDisplay(isInherit)?.ShortName;

    public static string? GetDisplayName(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetDisplay(isInherit)?.Name;

    public static string? GetDisplayPrompt(this Expression<Func<dynamic?>> accessor, bool isInherit = false) => 
        accessor.GetDisplay(isInherit)?.Prompt;

    public static string? GetDisplayGroupName(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetDisplay(isInherit)?.GroupName;

    public static int? GetDisplayOrder(this Expression<Func<dynamic?>> accessor, bool isInherit = false) => 
        accessor.GetDisplay(isInherit)?.Order;
        
        
    public static IEnumerable<TAttribute>? GetAttributes<TAttribute, TField>(this Expression<Func<TField>> accessor, bool isInherit)
        where TAttribute : Attribute
    {
        var fieldIdentifier = FieldIdentifier.Create(accessor);
        return fieldIdentifier.Model.GetType().GetAttributes<TAttribute>(fieldIdentifier.FieldName, isInherit);
    }
        
    public static TAttribute? GetAttribute<TAttribute, TField>(this Expression<Func<TField>> accessor, bool isInherit)
        where TAttribute : Attribute =>
        accessor.GetAttributes<TAttribute, TField>(isInherit)?.FirstOrDefault();

    public static bool IsHasAttribute<TAttribute, TField>(this Expression<Func<TField>> accessor, bool isInherit)
        where TAttribute : Attribute => 
        accessor.GetAttribute<TAttribute, TField>(isInherit) != null;

    public static bool IsHasRequiredAttribute<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) => 
        accessor.IsHasAttribute<RequiredAttribute, TField>(isInherit);

    public static string? GetDescription<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetAttribute<DescriptionAttribute, TField>(isInherit)?.Description;

    public static string? GetCategory<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetAttribute<CategoryAttribute, TField>(isInherit)?.Category;

    public static DisplayAttribute? GetDisplay<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetAttribute<DisplayAttribute, TField>(isInherit);

    public static string? GetDisplayShortName<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetDisplay(isInherit)?.ShortName;

    public static string? GetDisplayName<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetDisplay(isInherit)?.Name;

    public static string? GetDisplayPrompt<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) => 
        accessor.GetDisplay(isInherit)?.Prompt;

    public static string? GetDisplayGroupName<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetDisplay(isInherit)?.GroupName;

    public static int? GetDisplayOrder<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) => 
        accessor.GetDisplay(isInherit)?.Order;
        
    #endregion
        
    #region Field accessor
        
    public static IEnumerable<TAttribute>? GetFieldAttributes<TAttribute>(this Expression<Func<dynamic?>> accessor, bool isInherit)
        where TAttribute : Attribute
    {
        var fieldIdentifier = FieldIdentifier.Create(accessor);
        return fieldIdentifier.Model.GetType().GetFieldAttributes<TAttribute>(fieldIdentifier.FieldName, isInherit);
    }
        
    public static TAttribute? GetFieldAttribute<TAttribute>(this Expression<Func<dynamic?>> accessor, bool isInherit)
        where TAttribute : Attribute =>
        accessor.GetFieldAttributes<TAttribute>(isInherit)?.FirstOrDefault();
 
    public static bool IsHasFieldAttribute<TAttribute>(this Expression<Func<dynamic?>> accessor, bool isInherit)
        where TAttribute : Attribute => 
        accessor.GetFieldAttribute<TAttribute>(isInherit) != null;

    public static bool IsHasRequiredFieldAttribute(this Expression<Func<dynamic?>> accessor, bool isInherit = false) => 
        accessor.IsHasFieldAttribute<RequiredAttribute>(isInherit);
        
    public static string? GetFieldDescription(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetFieldAttribute<DescriptionAttribute>(isInherit)?.Description;

    public static string? GetFieldCategory(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetFieldAttribute<CategoryAttribute>(isInherit)?.Category;

    public static DisplayAttribute? GetFieldDisplay(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetFieldAttribute<DisplayAttribute>(isInherit);

    public static string? GetFieldDisplayShortName(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetFieldDisplay(isInherit)?.ShortName;

    public static string? GetFieldDisplayName(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetFieldDisplay(isInherit)?.Name;

    public static string? GetFieldDisplayPrompt(this Expression<Func<dynamic?>> accessor, bool isInherit = false) =>
        accessor.GetFieldDisplay(isInherit)?.Prompt;
        
        
    public static IEnumerable<TAttribute>? GetFieldAttributes<TAttribute, TField>(this Expression<Func<TField>> accessor, bool isInherit)
        where TAttribute : Attribute
    {
        var fieldIdentifier = FieldIdentifier.Create(accessor);
        return fieldIdentifier.Model.GetType().GetFieldAttributes<TAttribute>(fieldIdentifier.FieldName, isInherit);
    }
        
    public static TAttribute? GetFieldAttribute<TAttribute, TField>(this Expression<Func<TField>> accessor, bool isInherit)
        where TAttribute : Attribute =>
        accessor.GetFieldAttributes<TAttribute, TField>(isInherit)?.FirstOrDefault();
 
    public static bool IsHasFieldAttribute<TAttribute, TField>(this Expression<Func<TField>> accessor, bool isInherit)
        where TAttribute : Attribute => 
        accessor.GetFieldAttribute<TAttribute, TField>(isInherit) != null;

    public static bool IsHasRequiredFieldAttribute<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) => 
        accessor.IsHasFieldAttribute<RequiredAttribute, TField>(isInherit);
        
    public static string? GetFieldDescription<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetFieldAttribute<DescriptionAttribute, TField>(isInherit)?.Description;

    public static string? GetFieldCategory<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetFieldAttribute<CategoryAttribute, TField>(isInherit)?.Category;

    public static DisplayAttribute? GetFieldDisplay<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetFieldAttribute<DisplayAttribute, TField>(isInherit);

    public static string? GetFieldDisplayShortName<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetFieldDisplay(isInherit)?.ShortName;

    public static string? GetFieldDisplayName<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetFieldDisplay(isInherit)?.Name;

    public static string? GetFieldDisplayPrompt<TField>(this Expression<Func<TField>> accessor, bool isInherit = false) =>
        accessor.GetFieldDisplay(isInherit)?.Prompt;
        
    #endregion
        
    #region Property keySelector
        
    public static IEnumerable<TAttribute>? GetAttributes<TAttribute, TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit)
        where TSource : class 
        where TAttribute : Attribute
    {
        var name = TypeHelper.GetMemberName(keySelector);
        return source.GetType().GetAttributes<TAttribute>(name, isInherit);
    }
        
    public static TAttribute? GetAttribute<TAttribute, TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit) 
        where TSource : class 
        where TAttribute : Attribute =>
        source.GetAttributes<TAttribute, TSource>(keySelector, isInherit)?.FirstOrDefault();

    public static bool IsHasAttribute<TAttribute, TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit)
        where TSource : class 
        where TAttribute : Attribute => 
        source.GetAttribute<TAttribute, TSource>(keySelector, isInherit) != null;

    public static bool IsHasRequiredAttribute<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false) 
        where TSource : class => 
        source.IsHasAttribute<RequiredAttribute, TSource>(keySelector, isInherit);

    public static string? GetDescription<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetAttribute<DescriptionAttribute, TSource>(keySelector, isInherit)?.Description;
    }

    public static string? GetCategory<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetAttribute<CategoryAttribute, TSource>(keySelector, isInherit)?.Category;
    }

    public static DisplayAttribute? GetDisplay<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetAttribute<DisplayAttribute, TSource>(keySelector, isInherit);
    }

    public static string? GetDisplayShortName<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetDisplay(keySelector, isInherit)?.ShortName;
    }

    public static string? GetDisplayName<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetDisplay(keySelector, isInherit)?.Name;
    }

    public static string? GetDisplayPrompt<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetDisplay(keySelector, isInherit)?.Prompt;
    }

    public static string? GetDisplayGroupName<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetDisplay(keySelector, isInherit)?.GroupName;
    }

    public static int? GetDisplayOrder<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetDisplay(keySelector, isInherit)?.Order;
    }
        
    #endregion
        
    #region Field keySelector
        
    public static IEnumerable<TAttribute>? GetFieldAttributes<TAttribute, TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit) 
        where TSource : class 
        where TAttribute : Attribute
    {
        string name = TypeHelper.GetMemberName(keySelector);
        return source.GetType().GetFieldAttributes<TAttribute>(name, isInherit);
    }
        
    public static TAttribute? GetFieldAttribute<TAttribute, TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit) 
        where TSource : class 
        where TAttribute : Attribute =>
        source.GetFieldAttributes<TAttribute, TSource>(keySelector, isInherit)?.FirstOrDefault();
 
    public static bool IsHasFieldAttribute<TAttribute, TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit)
        where TSource : class 
        where TAttribute : Attribute => 
        source.GetFieldAttribute<TAttribute, TSource>(keySelector, isInherit) != null;

    public static bool IsHasRequiredFieldAttribute<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false) 
        where TSource : class => 
        source.IsHasFieldAttribute<RequiredAttribute, TSource>(keySelector, isInherit);
        
    public static string? GetFieldDescription<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetFieldAttribute<DescriptionAttribute, TSource>(keySelector, isInherit)?.Description;
    }

    public static string? GetFieldCategory<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetFieldAttribute<CategoryAttribute, TSource>(keySelector, isInherit)?.Category;
    }

    public static DisplayAttribute? GetFieldDisplay<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetFieldAttribute<DisplayAttribute, TSource>(keySelector, isInherit);
    }

    public static string? GetFieldDisplayShortName<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetFieldDisplay(keySelector, isInherit)?.ShortName;
    }

    public static string? GetFieldDisplayName<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetFieldDisplay(keySelector, isInherit)?.Name;
    }

    public static string? GetFieldDisplayPrompt<TSource>(this TSource source, Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
        where TSource : class
    {
        return source.GetFieldDisplay(keySelector, isInherit)?.Prompt;
    }
        
    #endregion
}