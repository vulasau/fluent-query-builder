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

        /// <summary>
        /// Retreives converter for given property using ObjectMapperConfiguration (ConverterFactory and ConverterResolver).
        /// </summary>
        /// <param name="prop">Source property</param>
        /// <param name="parameters">Converter parameters</param>
        /// <returns>
        /// Converter resolved by ObjectMapperConfiguration.ConverterFactory if set via ConverterAttribute.
        /// Converter resolved by ObjectMapperConfiguration.ConverterResolver for given property type if not set via ConverterAttribute. 
        /// </returns>
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

        /// <summary>
        /// Retreives converter for given converter type and result type using ObjectMapperConfiguration (ConverterFactory and ConverterResolver).
        /// </summary>
        /// <param name="converterType">Converter type</param>
        /// <param name="returnType">Result type</param>
        /// <returns>
        /// Converter resolved by ObjectMapperConfiguration.ConverterFactory if converterType parameter is set.
        /// Converter resolved by ObjectMapperConfiguration.ConverterResolver for given returnType if converterType parameter is not set; 
        /// </returns>
        public static IPropertyConverter GetConverter(Type converterType, Type returnType)
        {
            if (converterType != null)
                return _converterFactory.CreateConverter(converterType);

            return _converterResolver.Resolve(returnType);
        }
    }
}
