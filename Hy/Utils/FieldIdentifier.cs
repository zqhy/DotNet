using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Hy.Utils;

/// <summary>
/// Uniquely identifies a single field that can be edited. This may correspond to a property on a
/// model object, or can be any other named value.
/// </summary>
/// <footer><a href="https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.Forms.FieldIdentifier?view=netcore-5.0">`FieldIdentifier` on docs.microsoft.com</a></footer>
public readonly struct FieldIdentifier : IEquatable<FieldIdentifier>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="T:Microsoft.AspNetCore.Components.Forms.FieldIdentifier" /> structure.
  /// </summary>
  /// <param name="accessor">An expression that identifies an object member.</param>
  /// <typeparam name="TField">The field <see cref="T:System.Type" />.</typeparam>
  /// <footer><a href="https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.Forms.FieldIdentifier.Create?view=netcore-5.0">`FieldIdentifier.Create` on docs.microsoft.com</a></footer>
  public static FieldIdentifier Create<TField>(Expression<Func<TField>> accessor)
  {
    if (accessor == null) throw new ArgumentNullException(nameof (accessor));
    ParseAccessor(accessor, out var model, out var fieldName);
    return new FieldIdentifier(model, fieldName);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="T:Microsoft.AspNetCore.Components.Forms.FieldIdentifier" /> structure.
  /// </summary>
  /// <param name="model">The object that owns the field.</param>
  /// <param name="fieldName">The name of the editable field.</param>
  /// <footer><a href="https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.Forms.FieldIdentifier?view=netcore-5.0">`FieldIdentifier` on docs.microsoft.com</a></footer>
  public FieldIdentifier(object model, string fieldName)
  {
    if (model == null)
      throw new ArgumentNullException(nameof (model));
    this.Model = !model.GetType().IsValueType ? model : throw new ArgumentException("The model must be a reference-typed object.", nameof (model));
    this.FieldName = fieldName ?? throw new ArgumentNullException(nameof (fieldName));
  }

  /// <summary>Gets the object that owns the editable field.</summary>
  /// <footer><a href="https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.Forms.FieldIdentifier.Model?view=netcore-5.0">`FieldIdentifier.Model` on docs.microsoft.com</a></footer>
  public object Model { get; }

  /// <summary>Gets the name of the editable field.</summary>
  /// <footer><a href="https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.Forms.FieldIdentifier.FieldName?view=netcore-5.0">`FieldIdentifier.FieldName` on docs.microsoft.com</a></footer>
  public string FieldName { get; }

  /// <inheritdoc />
  /// <footer><a href="https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.Forms.FieldIdentifier.GetHashCode?view=netcore-5.0">`FieldIdentifier.GetHashCode` on docs.microsoft.com</a></footer>
  public override int GetHashCode() => (RuntimeHelpers.GetHashCode(this.Model), StringComparer.Ordinal.GetHashCode(this.FieldName)).GetHashCode();

  /// <inheritdoc />
  /// <footer><a href="https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.Forms.FieldIdentifier.Equals?view=netcore-5.0">`FieldIdentifier.Equals` on docs.microsoft.com</a></footer>
  public override bool Equals(object? obj) => obj is FieldIdentifier otherIdentifier && this.Equals(otherIdentifier);

  /// <inheritdoc />
  /// <footer><a href="https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.Forms.FieldIdentifier.Equals?view=netcore-5.0">`FieldIdentifier.Equals` on docs.microsoft.com</a></footer>
  public bool Equals(FieldIdentifier otherIdentifier) => otherIdentifier.Model == this.Model && string.Equals(otherIdentifier.FieldName, this.FieldName, StringComparison.Ordinal);

  private static void ParseAccessor<T>(
    Expression<Func<T>> accessor,
    out object model,
    out string fieldName)
  {
    var expression1 = accessor.Body;
    if (expression1 is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Convert && unaryExpression.Type == typeof (object))
      expression1 = unaryExpression.Operand;
    if (expression1 is not MemberExpression memberExpression)
      throw new ArgumentException("The provided expression contains a " + expression1.GetType().Name + " which is not supported. FieldIdentifier only supports simple member accessors (fields, properties) of an object.");
    fieldName = memberExpression.Member.Name;
    if (memberExpression.Expression is ConstantExpression expression2)
    {
      model = expression2.Value ?? throw new ArgumentException("The provided expression must evaluate to a non-null value.");
    }
    else
    {
      if (memberExpression.Expression == null)
        throw new ArgumentException("The provided expression contains a " + expression1.GetType().Name + " which is not supported. FieldIdentifier only supports simple member accessors (fields, properties) of an object.");
      model = ((Func<object>) Expression.Lambda(memberExpression.Expression).Compile())() ?? throw new ArgumentException("The provided expression must evaluate to a non-null value.");
    }
  }
}