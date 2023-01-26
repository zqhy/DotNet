using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
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
    
    public static string ToQueryString(this IEnumerable<object> parameters, JsonSerializerOptions? jsonSerializerOptions)
    {
        return string.Join("&", parameters.SelectMany(p => p.BuildQueryString(jsonSerializerOptions)));
    }
    
    [return: NotNullIfNotNull(nameof(requestUri))]
    public static string? CreateGetMethodUrl(this string? requestUri, params object?[] parameters)
    {
        var parameterNotNulls = parameters.Where(p => p != null).Cast<object>().ToArray();
        return parameterNotNulls.Any()
            ? $"{requestUri ?? ""}?{parameterNotNulls.ToQueryString(null)}" 
            : requestUri;
    }
        
    [return: NotNullIfNotNull(nameof(requestUri))]
    public static string? CreateGetMethodUrl(this string? requestUri, JsonSerializerOptions? jsonSerializerOptions,  params object?[] parameters)
    {
        var parameterNotNulls = parameters.Where(p => p != null).Cast<object>().ToArray();
        return parameterNotNulls.Any() 
            ? $"{requestUri ?? ""}?{parameterNotNulls.ToQueryString(jsonSerializerOptions)}" 
            : requestUri;
    }
    
    private static IEnumerable<string> BuildQueryString(this object obj, JsonSerializerOptions? jsonSerializerOptions)
    {
        var queryList = new List<string>();
        
        var json = JsonSerializer.Serialize(obj, jsonSerializerOptions ?? JsonConfig.Get());
        var queryData = JsonSerializer.Deserialize<IDictionary<string, object?>>(json, jsonSerializerOptions ?? JsonConfig.Get());
        if (queryData == null) return queryList;
        
        foreach (var (key, o) in queryData.Where(p => p.Value != null).Cast<KeyValuePair<string, object>>())
        {
            var value = (JsonElement)o;
            switch (value.ValueKind)
            {
                case JsonValueKind.Undefined:
                case JsonValueKind.Null:
                    continue;
                case JsonValueKind.Object:
                    queryList.AddRange(BuildQueryString(value, jsonSerializerOptions));
                    break;
                case JsonValueKind.Array:
                    queryList.AddRange(from object? item in value.EnumerateArray() select $"{key}={item}");
                    break;
                case JsonValueKind.String:
                case JsonValueKind.Number:
                case JsonValueKind.True:
                case JsonValueKind.False:
                    queryList.Add($"{key}={value}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        return queryList;
    }
}