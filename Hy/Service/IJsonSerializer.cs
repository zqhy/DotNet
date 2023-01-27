using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Hy.Service;

public interface IJsonSerializer
{
    TValue? Deserialize<TValue>(string json);
    object? Deserialize(string json, Type returnType);
        
    string Serialize<TValue>(TValue value);
    string Serialize(object? value, Type inputType);
}

public class JsonSerializerImpl : IJsonSerializer
{
    private readonly JsonSerializerOptions? _jsonSerializerOptions;

    public JsonSerializerImpl(IOptions<JsonSerializerOptions>? jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions?.Value;
    }
    
    public TValue? Deserialize<TValue>(string json) =>
        JsonSerializer.Deserialize<TValue>(json, _jsonSerializerOptions);

    public object? Deserialize(string json, Type returnType) =>
        JsonSerializer.Deserialize(json, returnType, _jsonSerializerOptions);

    public string Serialize<TValue>(TValue value) =>
        JsonSerializer.Serialize(value, _jsonSerializerOptions);

    public string Serialize(object? value, Type inputType) =>
        JsonSerializer.Serialize(value, inputType, _jsonSerializerOptions);
}