namespace Hy.Extensions;

public static class StandardExtensions
{
    // Kotlin: fun <T> T.also(block: (T) -> Unit): T
    public static T Also<T>(this T self, Action<T> block)
    {
        block(self);
        return self;
    }
        
    // Kotlin: fun <T, R> T.let(block: (T) -> R): R
    public static TR Let<T, TR>(this T self, Func<T, TR> block) 
    {
        return block(self);
    }
}