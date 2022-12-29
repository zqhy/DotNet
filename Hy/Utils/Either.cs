namespace Hy.Utils;

// thank you to https://stackoverflow.com/a/3199453/1586229
public abstract class Either<L, R>
{
    public abstract T Match<T>(Func<L, T> f, Func<R, T> g);
    public abstract void Match(Action<L> f, Action<R> g);

    public abstract Either<T, R> Map<T>(Func<L, T> f);

    // private ctor ensures no external classes can inherit
    // (not sure why that's important, but it's in the SO answer)
    private Either() { }
    public sealed class Left : Either<L, R>
    {
        private readonly L _item;
        public Left(L item) { _item = item; }
        public override T Match<T>(Func<L, T> f, Func<R, T> g)
        {
            return f(_item);
        }
        public override void Match(Action<L> f, Action<R> g)
        {
            f(_item);
        }
        public override Either<T, R> Map<T>(Func<L, T> f)
        {
            return f(_item);
        }
    }
    public static implicit operator Either<L, R>(L ell)
    {
        return new Left(ell);
    }

    public sealed class Right : Either<L, R>
    {
        private readonly R _item;
        public Right(R item) { _item = item; }
        public override T Match<T>(Func<L, T> f, Func<R, T> g)
        {
            return g(_item);
        }
        public override void Match(Action<L> f, Action<R> g)
        {
            g(_item);
        }
        public override Either<T, R> Map<T>(Func<L, T> f)
        {
            return _item;
        }


    }
    public static implicit operator Either<L, R>(R arr)
    {
        return new Right(arr);
    }
}