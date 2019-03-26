namespace FluentQueryBuilder
{
    public class NullComparisonResolver : INullComparisonResolver
    {
        public virtual string NullValue
        {
            get { return "NULL"; }
        }

        public virtual string GenerateExpression(string expression, bool isNegative = false)
        {
            var comparer = isNegative ? "!=" : "=";
            return string.Format("{0} {1} {2}", expression, comparer, NullValue);
        }
    }
}
