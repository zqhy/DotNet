using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Hy.Extensions;

public static class UrlExtensions
{
    /// <summary>
    /// 分析url链接，返回参数集合
    /// </summary>
    /// <param name="url">url链接</param>
    /// <param name="baseUrl"></param>
    /// <returns></returns>
    public static System.Collections.Specialized.NameValueCollection? ParseUrl(this string url, out string? baseUrl)
    {
        baseUrl = null;
        
        var nvc = new System.Collections.Specialized.NameValueCollection();
        try
        {
            var questionMarkIndex = url.IndexOf('?');

            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return null;
            }
            baseUrl = url[..questionMarkIndex];
            var ps = url[(questionMarkIndex + 1)..];
            
            // 开始分析参数对   
            var re = new System.Text.RegularExpressions.Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", System.Text.RegularExpressions.RegexOptions.Compiled);
            var mc = re.Matches(ps);
            foreach (System.Text.RegularExpressions.Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
        }
        catch
        {
            // ignored
        }
        return nvc;
    }

    private static readonly JsonSerializerOptions CreateGetMethodUrlJsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // 忽略null值的属性
        PropertyNameCaseInsensitive = true, //忽略大小写
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        AllowTrailingCommas = true,
    };
    public static string ToQueryString(this object parameter, JsonSerializerOptions? options = null)
    {
        var json = JsonSerializer.Serialize(parameter, options ?? CreateGetMethodUrlJsonOptions);
        return string.Join("&", JsonSerializer.Deserialize<IDictionary<string, object>>(json, options ?? CreateGetMethodUrlJsonOptions)!.
            Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value.ToString()!)}"));
    }
    public static string ToQueryString(this IEnumerable<object> parameters, JsonSerializerOptions? options = null)
    {
        return string.Join("&", parameters.Select(p => p.ToQueryString(options)));
    }
        
    public static string? CreateGetMethodUrl(this string? requestUri, IEnumerable<object>? parameters, JsonSerializerOptions? options = null)
    {
        return parameters == null ? requestUri : $"{requestUri ?? ""}?{parameters.ToQueryString(options)}";
    }

    public static string? CreateGetMethodUrl(this string? requestUri, object? parameter, JsonSerializerOptions? options = null)
    {
        return parameter == null ? requestUri :  $"{requestUri ?? ""}?{parameter.ToQueryString(options)}";
    }
}