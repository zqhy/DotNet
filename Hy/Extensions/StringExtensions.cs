using System.Diagnostics.CodeAnalysis;

namespace Hy.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? source)
    {
        return string.IsNullOrEmpty(source);
    }
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? source)
    {
        return string.IsNullOrWhiteSpace(source);
    }

    public static bool IsNotNullOrEmpty(this string? source) => !source.IsNullOrEmpty();
        
    /// <summary>   
    /// 得到字符串的长度，一个汉字算2个字符   
    /// </summary>   
    /// <param name="source">字符串</param>   
    /// <returns>返回字符串长度</returns>   
    public static int GetLength(this string? source)
    {
        if (source.IsNullOrEmpty()) { return 0; }
        var l = source.Length;
        var realLen = l;
        #region 计算长度
        var currentLen = 0;//当前长度
        while (currentLen < l)
        {
            //每遇到一个中文，则将实际长度加一。
            if (source[currentLen] > 128) { realLen++; }
            currentLen++;
        }
        #endregion
        return realLen;
    }
        
    public static IEnumerable<string> ToEnumerable(this string source, int stepSize)
    {
        if (stepSize == 1)
        {
            return source.ToArray().Select(p => p.ToString());
        }

        var stepCount = Math.Ceiling(Convert.ToDouble(source.Length) / stepSize);
        return Enumerable.Range(0, (int)stepCount).Select(p => source.Substring(p * stepSize, 
            p * stepSize + stepSize > source.Length ? source.Length - p * stepSize : stepSize));
    }

    public static void EachStep(this string source, int stepSize, Func<string, bool> block)
    {
        var stepCount = Math.Ceiling(Convert.ToDouble(source.Length) / stepSize);
        foreach (var step in Enumerable.Range(0, (int)stepCount))
        {
            if (!block(source.Substring(step * stepSize,step * stepSize + stepSize > source.Length ? source.Length - step * stepSize : stepSize)))
            {
                return;
            }
        }
    }
}