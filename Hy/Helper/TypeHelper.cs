using System.Linq.Expressions;

namespace Hy.Helper;

public static class TypeHelper
{
    public static string GetMemberName<T>(Expression<Func<T, dynamic?>> property)
    {
        LambdaExpression lambda = property;
        MemberExpression memberExpression;
        
        if (lambda.Body is UnaryExpression unaryExpression)
        {
            memberExpression = (MemberExpression)unaryExpression.Operand;
        }
        else
        {
            memberExpression = (MemberExpression)lambda.Body;
        }
        
        return memberExpression.Member.Name;
    }
}