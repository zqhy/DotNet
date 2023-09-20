namespace Hy.Extensions;

public static class VersionExtensions
{
    // https://blog.darkthread.net/blog/about-asm-version/
    public static string? GetLiteVersion(this Version? version) =>
        version == null 
            ? null 
            : $"{version.Major}.{version.Minor}.{version.Build}{(version.Revision != 0 ? $".{version.Revision}" : string.Empty)}";
}