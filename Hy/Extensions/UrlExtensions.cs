using System.Collections.Specialized;
using System.Reflection;
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
    
    public static string ToQueryString(this IEnumerable<object> parameters, string? dateTimeFormat)
    {
        return string.Join("&", parameters.SelectMany(p => p.BuildQueryString(dateTimeFormat)));
    }
        
    public static string? CreateGetMethodUrl(this string? requestUri, params object[] parameters)
    {
        return parameters.Any() 
            ? $"{requestUri ?? ""}?{parameters.ToQueryString(null)}" 
            : requestUri;
    }
        
    public static string? CreateGetMethodUrl(this string? requestUri, string? dateTimeFormat,  params object[] parameters)
    {
        return parameters.Any() 
            ? $"{requestUri ?? ""}?{parameters.ToQueryString(dateTimeFormat)}" 
            : requestUri;
    }
    
    private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
    private static List<string> BuildQueryString(this object obj, string? dateTimeFormat)
    {
        var queryList = new List<string>();
        foreach (var p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (p.GetValue(obj, Array.Empty<object>()) != null)
            {
                var value = p.GetValue(obj, Array.Empty<object>());
                if (value == null)
                {
                    continue;
                }
                
                var name = p.Name.ToLower();
                switch (p.PropertyType.IsArray)
                {
                    case true when value.GetType() == typeof(DateTime[]):
                        queryList.AddRange(from item in (DateTime[])value select $"{name}={item.ToString(dateTimeFormat ?? DateTimeFormat)}");
                        break;
                    case true:
                        queryList.AddRange(from object? item in (Array)value! select $"{name}={item}");
                        break;
                    default:
                    {
                        if (p.PropertyType.IsEnum())
                        {
                            queryList.Add($"{name}={(int)value}");
                        }
                        else if (p.PropertyType == typeof(string))
                            queryList.Add($"{name}={value}");

                        else if (p.PropertyType == typeof(DateTime) && !value!.Equals(Activator.CreateInstance(p.PropertyType))) // is not default 
                            queryList.Add($"{name}={((DateTime)value).ToString(dateTimeFormat ?? DateTimeFormat)}");

                        else if (p.PropertyType.IsValueType && !value!.Equals(Activator.CreateInstance(p.PropertyType))) // is not default 
                            queryList.Add($"{name}={value}");


                        else if (p.PropertyType.IsClass)
                            BuildQueryString(value, dateTimeFormat);
                        break;
                    }
                }
            }
        }

        return queryList;
    }
}