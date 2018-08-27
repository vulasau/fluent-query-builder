using System.Linq.Expressions;

namespace FluentQueryBuilder
{
    public interface IExpressionTypeTransformer
    {
        /// <summary>
        /// Transforms expression operation type to it's string representation.
        /// </summary>
        /// <param name="expressionType">Expression operation</param>
        /// <returns>String representation of expression operation. For example: '>' for 'ExpressionType.GreaterThan' value.</returns>
        string Transform(ExpressionType expressionType);
    }
}
