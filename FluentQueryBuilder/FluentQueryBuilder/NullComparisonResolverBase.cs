namespace FluentQueryBuilder
{
    public abstract class NullComparisonResolverBase
    {
        public abstract string NullValue { get; }

        public abstract string GenerateExpression(string expression, bool isNegative = false);
    }
}
