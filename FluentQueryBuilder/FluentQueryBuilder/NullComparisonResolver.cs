namespace FluentQueryBuilder
{
    public class NullComparisonResolver : NullComparisonResolverBase
    {
        public override string NullValue
        {
            get { return "NULL"; }
        }

        public override string GenerateExpression(string expression, bool isNegative = false)
        {
            var comparer = isNegative ? "!=" : "=";
            return string.Format("{0} {1} {2}", expression, comparer, NullValue);
        }
    }
}
