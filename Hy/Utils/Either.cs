namespace Hy.Utils;

// thank you to https://stackoverflow.com/a/3199453/1586229
public abstract class Either<TL, TR>
{
    public abstract T Match<T>(Func<TL, T> f, Func<TR, T> g);
    public abstract void Match(Action<TL> f, Action<TR> g);

    public abstract Either<T, TR> Map<T>(Func<TL, T> f);

    // private ctor ensures no external classes can inherit
    // (not sure why that's important, but it's in the SO answer)
    private Either() { }
    public sealed class Left : Either<TL, TR>
    {
        private readonly TL _item;
        public Left(TL item) { _item = item; }
        public override T Match<T>(Func<TL, T> f, Func<TR, T> g)
        {
            return f(_item);
        }
        public override void Match(Action<TL> f, Action<TR> g)
        {
            f(_item);
        }
        public override Either<T, TR> Map<T>(Func<TL, T> f)
        {
            return f(_item);
        }
    }
    public static implicit operator Either<TL, TR>(TL ell)
    {
        return new Left(ell);
    }

    public sealed class Right : Either<TL, TR>
    {
        private readonly TR _item;
        public Right(TR item) { _item = item; }
        public override T Match<T>(Func<TL, T> f, Func<TR, T> g)
        {
            return g(_item);
        }
        public override void Match(Action<TL> f, Action<TR> g)
        {
            g(_item);
        }
        public override Either<T, TR> Map<T>(Func<TL, T> f)
        {
            return _item;
        }


    }
    public static implicit operator Either<TL, TR>(TR arr)
    {
        return new Right(arr);
    }
}