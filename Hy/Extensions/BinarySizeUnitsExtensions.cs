using Hy.Types;

namespace Hy.Extensions;

public static class BinarySizeUnitsExtensions
{
    public static double Of(this BinarySizeUnits units, long value) => 
        value / Math.Pow(1024, (long)units);

    public static string Text(this BinarySizeUnits units, long value) =>
        $"{units.Of(value):0.00} {units}";
}