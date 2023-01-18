namespace Hy.Extensions;

public static class TypeExtensions
{
    public static bool IsEnum(this Type type) => 
        type.IsEnum || (type.GenericTypeArguments.Length == 1 && type.GenericTypeArguments.First().IsEnum);
}