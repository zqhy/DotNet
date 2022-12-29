namespace Hy.Service;

public interface IJsonSerializer
{
    TValue? Deserialize<TValue>(string json);
    object? Deserialize(string json, Type returnType);
        
    string Serialize<TValue>(TValue value);
    string Serialize(object? value, Type inputType);
}