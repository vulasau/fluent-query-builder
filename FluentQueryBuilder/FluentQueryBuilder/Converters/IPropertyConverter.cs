namespace FluentQueryBuilder.Converters
{
    public interface IPropertyConverter
    {
        /// <summary>
        /// Converts string value to a typed value based on converter type.
        /// </summary>
        /// <param name="source">Source value</param>
        /// <returns>Converted value of type based on converter type.</returns>
        object Convert(string source);

        /// <summary>
        /// Converts typed value to it's string representation based on converter type.
        /// </summary>
        /// <param name="source">Source value</param>
        /// <returns>Converted string representation value.</returns>
        string ConvertBack(object source);
    }
}
