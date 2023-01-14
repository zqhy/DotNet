using System.Collections.Specialized;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Hy.Extensions;

public static class UrlExtensions
{
    /// <summary>
    /// 分析url链接，返回参数集合
    /// </summary>
    /// <param name="url">url链接</param>
    /// <param name="baseUrl"></param>
    /// <returns></returns>
    public static NameValueCollection? ParseUrl(this string url, out string? baseUrl)
    {
        baseUrl = null;
        
        var nvc = new NameValueCollection();
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
            var re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            var mc = re.Matches(ps);
            foreach (Match m in mc)
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
    
    public static string ToQueryString(this object parameter, JsonSerializerOptions? options = null)
    {
        var json = JsonSerializer.Serialize(parameter, options);
        return string.Join("&", JsonSerializer.Deserialize<IDictionary<string, object>>(json, options)!.
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