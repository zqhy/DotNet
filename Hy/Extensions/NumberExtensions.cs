namespace Hy.Extensions;

public static class NumberExtensions
{
    public static double Intercept(this double source, int digits)
    {
        var digitsPow = Math.Pow(10, digits);
        return Convert.ToDouble((int) (source * digitsPow)) / digitsPow;
    }
        
    public static DateTime ToLocalDateTime(this long timeStamp) => DateTimeOffset.FromUnixTimeMilliseconds(timeStamp).LocalDateTime;
}