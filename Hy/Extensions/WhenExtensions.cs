namespace Hy.Extensions;

public static class WhenExtensions
{
    public static void When<T>(this T source, Func<T, bool> predicate, Action<T>? onTrue, Action<T>? onElse = null)
    {
        if (predicate.Invoke(source))
        {
            onTrue?.Invoke(source);
        }
        else
        {
            onElse?.Invoke(source);
        }
    }
        
    public static void WhenTrue(this bool source, Action action)
    {
        if (source)
        {
            action.Invoke();
        }
    }

    public static void WhenFalse(this bool source, Action action)
    {
        if (!source)
        {
            action.Invoke();
        }
    }
}