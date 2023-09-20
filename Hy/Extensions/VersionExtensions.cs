namespace Hy.Extensions;

public static class VersionExtensions
{
    // https://blog.darkthread.net/blog/about-asm-version/
    public static string? GetLiteVersion(this Version? version) => 
        version == null ? null : string.Join(".", new [] { version.Major, version.Minor, version.Build, version.Revision }.Where(p => p != 0));
}