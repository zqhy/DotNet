using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Hy.Extensions;

namespace Hy.Helper;

public static class AttributeHelper
{
    #region Type Attribute
        
    public static IEnumerable<TAttribute> GetAttributes<TAttribute, TSource>(bool isInherit) =>
        typeof(TSource).GetAttributes<TAttribute>(isInherit);
        
    public static TAttribute? GetAttribute<TAttribute, TSource>(bool isInherit) 
        where TAttribute : Attribute =>
        typeof(TSource).GetAttribute<TAttribute>(isInherit);

    public static bool IsHasAttribute<TAttribute, TSource>(bool isInherit)
        where TSource : class 
        where TAttribute : Attribute => 
        GetAttribute<TAttribute, TSource>(isInherit) != null;

    public static bool IsHasRequiredAttribute<TSource>(bool isInherit = false) 
        where TSource : class => 
        IsHasAttribute<RequiredAttribute, TSource>(isInherit);
        
    public static string? GetDescription<TSource>(bool isInherit = false)
    {
        return GetAttribute<DescriptionAttribute, TSource>(isInherit)?.Description;
    }

    public static string? GetCategory<TSource>(bool isInherit = false)
    {
        return GetAttribute<CategoryAttribute, TSource>(isInherit)?.Category;
    }

    public static DisplayAttribute? GetDisplay<TSource>(bool isInherit = false)
    {
        return GetAttribute<DisplayAttribute, TSource>(isInherit);
    }

    public static string? GetDisplayShortName<TSource>(bool isInherit = false)
    {
        return GetDisplay<TSource>(isInherit)?.ShortName;
    }

    public static string? GetDisplayName<TSource>(bool isInherit = false)
    {
        return GetDisplay<TSource>(isInherit)?.Name;
    }

    public static string? GetDisplayPrompt<TSource>(bool isInherit = false)
    {
        return GetDisplay<TSource>(isInherit)?.Prompt;
    }
        
    #endregion
        
    #region Property keySelector Attribute
        
    public static IEnumerable<TAttribute>? GetAttributes<TAttribute, TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit) =>
        typeof(TSource).GetAttributes<TAttribute>(THelper.GetMemberName(keySelector), isInherit);
        
    public static TAttribute? GetAttribute<TAttribute, TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit) 
        where TAttribute : Attribute =>
        typeof(TSource).GetAttribute<TAttribute>(THelper.GetMemberName(keySelector), isInherit);

    public static bool IsHasAttribute<TAttribute, TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit)
        where TSource : class 
        where TAttribute : Attribute => 
        GetAttribute<TAttribute, TSource>(keySelector, isInherit) != null;

    public static bool IsHasRequiredAttribute<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false) 
        where TSource : class => 
        IsHasAttribute<RequiredAttribute, TSource>(keySelector, isInherit);
        
    public static string? GetDescription<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetAttribute<DescriptionAttribute, TSource>(keySelector, isInherit)?.Description;
    }

    public static string? GetCategory<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetAttribute<CategoryAttribute, TSource>(keySelector, isInherit)?.Category;
    }

    public static DisplayAttribute? GetDisplay<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetAttribute<DisplayAttribute, TSource>(keySelector, isInherit);
    }

    public static string? GetDisplayShortName<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetDisplay(keySelector, isInherit)?.ShortName;
    }

    public static string? GetDisplayName<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetDisplay(keySelector, isInherit)?.Name;
    }

    public static string? GetDisplayPrompt<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetDisplay(keySelector, isInherit)?.Prompt;
    }
        
    #endregion
        
    #region Field keySelector Attribute
        
    public static IEnumerable<TAttribute>? GetFieldAttributes<TAttribute, TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit) =>
        typeof(TSource).GetFieldAttributes<TAttribute>(THelper.GetMemberName(keySelector), isInherit);
        
    public static TAttribute? GetFieldAttribute<TAttribute, TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit) 
        where TAttribute : Attribute =>
        typeof(TSource).GetFieldAttribute<TAttribute>(THelper.GetMemberName(keySelector), isInherit);

    public static bool IsHasFieldAttribute<TAttribute, TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit)
        where TSource : class 
        where TAttribute : Attribute => 
        GetFieldAttribute<TAttribute, TSource>(keySelector, isInherit) != null;

    public static bool IsHasRequiredFieldAttribute<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false) 
        where TSource : class => 
        IsHasFieldAttribute<RequiredAttribute, TSource>(keySelector, isInherit);
        
    public static string? GetFieldDescription<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetFieldAttribute<DescriptionAttribute, TSource>(keySelector, isInherit)?.Description;
    }

    public static string? GetFieldCategory<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetFieldAttribute<CategoryAttribute, TSource>(keySelector, isInherit)?.Category;
    }

    public static DisplayAttribute? GetFieldDisplay<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetFieldAttribute<DisplayAttribute, TSource>(keySelector, isInherit);
    }

    public static string? GetFieldDisplayShortName<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetFieldDisplay(keySelector, isInherit)?.ShortName;
    }

    public static string? GetFieldDisplayName<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetFieldDisplay(keySelector, isInherit)?.Name;
    }

    public static string? GetFieldDisplayPrompt<TSource>(Expression<Func<TSource, dynamic?>> keySelector, bool isInherit = false)
    {
        return GetFieldDisplay(keySelector, isInherit)?.Prompt;
    }
        
    #endregion
}