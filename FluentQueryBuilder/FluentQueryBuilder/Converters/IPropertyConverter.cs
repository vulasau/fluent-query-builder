namespace FluentQueryBuilder.Converters
{
    public interface IPropertyConverter
    {
        /// <summary>
        /// Converts string value to a typed value based on converter type.
        /// </summary>
        /// <param name="source">Source value</param>
        /// <param name="parameters">Converter parameters</param>
        /// <returns>Converted value of type based on converter type.</returns>
        object Convert(string source, params object[] parameters);

        /// <summary>
        /// Converts typed value to it's string representation based on converter type.
        /// </summary>
        /// <param name="source">Source value</param>
        /// <param name="parameters">Converter parameters</param>
        /// <returns>Converted string representation value.</returns>
        string ConvertBack(object source, params object[] parameters);
    }
}
