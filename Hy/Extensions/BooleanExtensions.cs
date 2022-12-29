namespace Hy.Extensions;

public static class BooleanExtensions
{
    public static bool? ToNullable(this bool source) => source ? true : null;
    public static bool? ToFalseNull(this bool? source) => source == true ? true : null;
}