namespace Hy.Helper;

public static class HostingEnvironmentHelper
{
    public static string? Environment { get; private set; }
    
    internal static void SetEnvironment(string environment)
    {
        Environment = environment;
    }
    
    public static bool? IsDevelopment() => Environment == null
        ? null
        : string.Equals(Environment, "Development", StringComparison.OrdinalIgnoreCase);

    public static bool? IsStaging() => Environment == null
        ? null
        : string.Equals(Environment, "Staging", StringComparison.OrdinalIgnoreCase);

    public static bool? IsProduction() => Environment == null
        ? null
        : string.Equals(Environment, "Production", StringComparison.OrdinalIgnoreCase);
}