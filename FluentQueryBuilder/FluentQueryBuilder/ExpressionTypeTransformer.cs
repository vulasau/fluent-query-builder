using System;
using System.Linq.Expressions;

namespace FluentQueryBuilder
{
    public class ExpressionTypeTransformer: IExpressionTypeTransformer
    {
        public virtual string Transform(ExpressionType nodeType)
        {
            // Unary operators
            if (nodeType == ExpressionType.Add)
                return "+";
            if (nodeType == ExpressionType.Decrement)
                return "-";
            if (nodeType == ExpressionType.Multiply)
                return "*";
            if (nodeType == ExpressionType.Divide)
                return "/";

            // Conditional operators
            if (nodeType == ExpressionType.AndAlso)
                return "AND";
            if (nodeType == ExpressionType.OrElse)
                return "OR";

            // Comparison operators
            if (nodeType == ExpressionType.Equal)
                return "=";
            if (nodeType == ExpressionType.NotEqual)
                return "!=";
            if (nodeType == ExpressionType.GreaterThan)
                return ">";
            if (nodeType == ExpressionType.GreaterThanOrEqual)
                return ">=";
            if (nodeType == ExpressionType.LessThan)
                return "<";
            if (nodeType == ExpressionType.LessThanOrEqual)
                return "<=";

            throw new NotSupportedException(string.Format("Expression of type '{0}' is not supported.", nodeType.ToString()));
        }
    }
}
