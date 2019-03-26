namespace FluentQueryBuilder
{
    public interface INullComparisonResolver
    {
        /// <summary>
        /// Defalut 'null' value string interpretations.
        /// </summary>
        string NullValue { get; }

        /// <summary>
        /// Creates null comparison expression string from given expression string.
        /// </summary>
        /// <param name="expression">Source expression string</param>
        /// <param name="isNegative">Indicator, whether comparison is negative or not</param>
        /// <returns>Generated null comparison expression string.</returns>
        string GenerateExpression(string expression, bool isNegative = false);
    }
}