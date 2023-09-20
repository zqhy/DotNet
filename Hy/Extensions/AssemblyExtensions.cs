using System.Reflection;

namespace Hy.Extensions;

// https://blog.csdn.net/mzl87/article/details/104849630
public static class AssemblyExtensions
{
    public static Version? GetAssemblyVersion(this Assembly? assembly) => assembly?.GetName().Version;
    
    public static string? GetAssemblyFileVersion(this Assembly? assembly) =>
        assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
    
    public static string? GetAssemblyName(this Assembly? assembly) => assembly?.GetName().Name;
    
    public static string? GetAssemblyTitle(this Assembly? assembly) => assembly?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
}