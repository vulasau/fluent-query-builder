using System;
using System.Linq;
using System.Reflection;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Configuration;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder.Extensions
{
    public static class ConverterExtensions
    {
        private static readonly IConverterResolver _converterResolver;
        private static readonly IConverterFactory _converterFactory;

        static ConverterExtensions()
        {
            _converterResolver = ObjectMapperConfiguration.ConverterResolver;
            _converterFactory = ObjectMapperConfiguration.ConverterFactory;
        }

        public static IPropertyConverter GetConverter(this PropertyInfo prop, out object[] parameters)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");

            var converterAttribute = prop.GetCustomAttributes(typeof(ConverterAttribute), false).SingleOrDefault() as ConverterAttribute;
            var converterType = converterAttribute == null ? null : converterAttribute.Type;
            var converterParameters = converterAttribute == null ? null : converterAttribute.Parameters;
            var converter = GetConverter(converterType, prop.PropertyType);

            parameters = converterParameters;
            return converter;
        }

        private static IPropertyConverter GetConverter(Type converterType, Type returnType)
        {
            if (converterType != null)
                return _converterFactory.CreateConverter(converterType);

            return _converterResolver.Resolve(returnType);
        }
    }
}
