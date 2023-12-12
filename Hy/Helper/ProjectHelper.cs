namespace Hy.Helper;

public static class ProjectHelper
{
    private static string? _instanceId;

    internal static void SetInstanceId(string instanceId)
    {
        _instanceId = instanceId;
    }

    public static string? GetInstanceId() => _instanceId;
}