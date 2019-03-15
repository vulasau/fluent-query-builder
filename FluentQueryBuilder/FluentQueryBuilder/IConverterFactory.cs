using System;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder
{
    public interface IConverterFactory
    {
        IPropertyConverter CreateConverter(Type type);
    }
}
