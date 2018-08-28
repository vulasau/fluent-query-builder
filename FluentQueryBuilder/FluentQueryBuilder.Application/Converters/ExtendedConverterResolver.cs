using System;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder.Application.Converters
{
    public class ExtendedConverterResolver: ConverterResolver
    {
        public override IPropertyConverter Resolve(Type type)
        {
            if (type == typeof (DateTime))
                return new DateConverter();

            return base.Resolve(type);
        }
    }
}
