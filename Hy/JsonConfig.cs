using System.Text.Json;

namespace Hy;

public static class JsonConfig
{
    private static JsonSerializerOptions? _config;

    internal static void Set(JsonSerializerOptions config)
    {
        if (_config != null)
        {
            throw new Exception("The configuration cannot be repeated");
        }

        _config = config;
    }

    public static JsonSerializerOptions? Get() => _config;
}