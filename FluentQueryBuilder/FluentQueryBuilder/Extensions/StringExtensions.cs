using System;

namespace FluentQueryBuilder.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Wraps given string with quotes.
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>New string reflecting source string wrapped with quotes.</returns>
        public static string WrapWithQuotes(this string source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return string.Format("'{0}'", source);
        }

        /// <summary>
        /// Wraps given string with brackets.
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>New string reflecting source string wrapped with brackets.</returns>
        public static string WrapWithBrackets(this string source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return string.Format("({0})", source);
        }
    }
}
