namespace Hy.Extensions;

public static class ObjectExtensions
{
    public static string? ToSafeString(this object? obj, string? defaultStr = null)
    {
        try
        {
            return obj?.ToString();
        }
        catch
        {
            return defaultStr;
        }
    }

    public static int? ToSafeInt(this object obj)
    {
        try
        {
            return Convert.ToInt32(obj);
        }
        catch
        {
            return new int?();
        }
    }

    public static int ToSafeInt(this object obj, int defaultStr)
    {
        try
        {
            return Convert.ToInt32(obj);
        }
        catch
        {
            return defaultStr;
        }
    }

    public static bool ToSafeBool(this object obj, bool defaultBool = false)
    {
        try
        {
            return Convert.ToBoolean(obj);
        }
        catch
        {
            return defaultBool;
        }
    }

    public static double? ToSafeDouble(this object obj)
    {
        try
        {
            return Convert.ToDouble(obj);
        }
        catch
        {
            return new double?();
        }
    }

    public static double ToSafeDouble(this object obj, double defaultDouble)
    {
        try
        {
            return Convert.ToDouble(obj);
        }
        catch
        {
            return defaultDouble;
        }
    }

    public static float? ToSafeFloat(this object obj)
    {
        try
        {
            return Convert.ToSingle(obj);
        }
        catch
        {
            return new float?();
        }
    }

    public static float ToSafeFloat(this object obj, float defaultFloat)
    {
        try
        {
            return Convert.ToSingle(obj);
        }
        catch
        {
            return defaultFloat;
        }
    }
}