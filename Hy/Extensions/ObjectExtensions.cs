namespace Hy.Extensions;

public static class ObjectExtensions
{
    public static string? ToSafeString(this object? obj, string? defaultValue = null)
    {
        try
        {
            return obj?.ToString();
        }
        catch
        {
            return defaultValue;
        }
    }

    public static short? ToSafeShort(this object? obj)
    {
        if (obj == null)
        {
            return null;
        }
        try
        {
            return Convert.ToInt16(obj);
        }
        catch
        {
            return new short?();
        }
    }

    public static short ToSafeShort(this object? obj, short defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        try
        {
            return Convert.ToInt16(obj);
        }
        catch
        {
            return defaultValue;
        }
    }

    public static int? ToSafeInt(this object? obj)
    {
        if (obj == null)
        {
            return null;
        }
        try
        {
            return Convert.ToInt32(obj);
        }
        catch
        {
            return new int?();
        }
    }

    public static int ToSafeInt(this object? obj, int defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        try
        {
            return Convert.ToInt32(obj);
        }
        catch
        {
            return defaultValue;
        }
    }

    public static bool? ToSafeBool(this object? obj)
    {
        if (obj == null)
        {
            return null;
        }
        try
        {
            return Convert.ToBoolean(obj);
        }
        catch
        {
            return new bool?();
        }
    }

    public static bool ToSafeBool(this object? obj, bool defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        try
        {
            return Convert.ToBoolean(obj);
        }
        catch
        {
            return defaultValue;
        }
    }

    public static double? ToSafeDouble(this object? obj)
    {
        if (obj == null)
        {
            return null;
        }
        try
        {
            return Convert.ToDouble(obj);
        }
        catch
        {
            return new double?();
        }
    }

    public static double ToSafeDouble(this object? obj, double defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        try
        {
            return Convert.ToDouble(obj);
        }
        catch
        {
            return defaultValue;
        }
    }

    public static float? ToSafeFloat(this object? obj)
    {
        if (obj == null)
        {
            return null;
        }
        try
        {
            return Convert.ToSingle(obj);
        }
        catch
        {
            return new float?();
        }
    }

    public static float ToSafeFloat(this object? obj, float defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        try
        {
            return Convert.ToSingle(obj);
        }
        catch
        {
            return defaultValue;
        }
    }

    public static decimal? ToSafeDecimal(this object? obj)
    {
        if (obj == null)
        {
            return null;
        }
        try
        {
            return Convert.ToDecimal(obj);
        }
        catch
        {
            return new decimal?();
        }
    }

    public static decimal ToSafeDecimal(this object? obj, decimal defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        try
        {
            return Convert.ToDecimal(obj);
        }
        catch
        {
            return defaultValue;
        }
    }
}