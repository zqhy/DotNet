using System.Linq.Expressions;

namespace Hy.Specification;

/// <inheritdoc />
/// <summary>
/// NotEspecification convert a original
/// specification with NOT logic operator
/// </summary>
/// <typeparam name="TEntity">Type of element for this specificaiton</typeparam>
public sealed class NotSpecification<TEntity> : Specification<TEntity> where TEntity : class
{
    #region Members

    readonly Expression<Func<TEntity, bool>> _originalCriteria;

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor for NotSpecificaiton
    /// </summary>
    /// <param name="originalSpecification">Original specification</param>
    public NotSpecification(ISpecification<TEntity> originalSpecification)
    {

        if (originalSpecification == null)
            throw new ArgumentNullException(nameof(originalSpecification));

        _originalCriteria = originalSpecification.SatisfiedBy();
    }

    /// <summary>
    /// Constructor for NotSpecification
    /// </summary>
    /// <param name="originalSpecification">Original specificaiton</param>
    public NotSpecification(Expression<Func<TEntity,bool>> originalSpecification)
    {
        _originalCriteria = originalSpecification ?? throw new ArgumentNullException(nameof(originalSpecification));
    }

    #endregion

    #region Override Specification methods

    /// <inheritdoc />
    /// <summary>
    /// <see cref="T:Hy.Specification.ISpecification`1" />
    /// </summary>
    /// <returns><see cref="T:Hy.Specification.ISpecification`1" /></returns>
    public override Expression<Func<TEntity, bool>> SatisfiedBy()
    {
            
        return Expression.Lambda<Func<TEntity,bool>>(Expression.Not(_originalCriteria.Body), _originalCriteria.Parameters.Single());
    }

    #endregion
}