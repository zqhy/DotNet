namespace Hy.Specification;

/// <inheritdoc />
/// <summary>
/// Base class for composite specifications
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class CompositeSpecification<TEntity> : Specification<TEntity> where TEntity : class
{
    #region Properties

    /// <summary>
    /// Left side specification for this composite element
    /// </summary>
    public abstract ISpecification<TEntity> LeftSideSpecification { get; }

    /// <summary>
    /// Right side specification for this composite element
    /// </summary>
    public abstract ISpecification<TEntity> RightSideSpecification { get; }

    #endregion
}