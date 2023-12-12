namespace Hy.Helper;

public static class HostingEnvironmentHelper
{
    private static string? _environment;

    internal static void SetEnvironment(string environment)
    {
        _environment = environment;
    }

    public static bool? IsDevelopment() => _environment == null
        ? null
        : string.Equals(_environment, "Development", StringComparison.OrdinalIgnoreCase);

    public static bool? IsStaging() => _environment == null
        ? null
        : string.Equals(_environment, "Staging", StringComparison.OrdinalIgnoreCase);

    public static bool? IsProduction() => _environment == null
        ? null
        : string.Equals(_environment, "Production", StringComparison.OrdinalIgnoreCase);
}