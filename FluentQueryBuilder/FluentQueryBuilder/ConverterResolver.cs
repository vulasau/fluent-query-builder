using System;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder
{
    public class ConverterResolver : IConverterResolver
    {
        public virtual IPropertyConverter Resolve(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type", "parameter 'type' should be set");

            if (type == typeof(int))
                return new IntegerConverter();
            else if (type == typeof(double))
                return new DoubleConverter();
            else if (type == typeof(bool))
                return new BooleanConverter();
            else if (type == typeof(DateTime))
                return new DateTimeConverter();
            else
                return new ObjectConverter();
        }
    }
}
