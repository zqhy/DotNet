using System.Diagnostics;
using System.Reflection;

namespace Hy.Extensions;

// https://blog.csdn.net/mzl87/article/details/104849630
public static class AssemblyExtensions
{
    public static Version? GetAssemblyVersion(this Assembly? assembly) => assembly?.GetName().Version;

    public static string? GetAssemblyFileVersion(this Assembly? assembly) =>
        assembly == null ? null : FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
}