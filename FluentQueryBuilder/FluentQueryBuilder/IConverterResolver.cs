using System;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder
{
    public interface IConverterResolver
    {
        /// <summary>
        /// Resolves IPropertyConverter implementation based on provided data type.
        /// </summary>
        /// <param name="type">Date type</param>
        /// <returns>Object of IPropertyConverter implementation type resolved based on data type provided.</returns>
        IPropertyConverter Resolve(Type type);
    }
}
