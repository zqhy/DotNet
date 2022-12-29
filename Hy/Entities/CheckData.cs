using System.Diagnostics.CodeAnalysis;

namespace Hy.Entities;

public record CheckData<T>(bool IsTrue, string? Message, T? Data)
{
    public bool CheckIsTrue([NotNullWhen(true)] out T? data)
    {
        data = Data;
        return IsTrue;
    }
    
    public bool CheckIsFalse([NotNullWhen(false)] out T? data)
    {
        data = Data;
        return IsTrue;
    }

    public static CheckData<T> True(T data, string? message = null) => new(true, message, data);
    
    public static CheckData<T> False(string? message = null) => new(false, message, default);
}