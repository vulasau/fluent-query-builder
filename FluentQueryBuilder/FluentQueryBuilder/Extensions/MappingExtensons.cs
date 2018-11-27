using System;
using System.Linq;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Configuration;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder.Extensions
{
    public static class MappingExtensons
    {
        private static readonly IConditionResolver _conditionResolver;
        private static readonly IConverterResolver _converterResolver;

        static MappingExtensons()
        {
            _conditionResolver = ObjectMapperConfiguration.ConditionResolver;
            _converterResolver = ObjectMapperConfiguration.ConverterResolver;
        }

        public static FluentObject MapToFluentObject<T>(this T source) where T : class, new()
        {
            if (source == null) 
                return null;

            var fluentEntityAttribute = source.GetType().GetCustomAttributes(typeof(FluentEntityAttribute), false).SingleOrDefault() as FluentEntityAttribute;
            if (fluentEntityAttribute == null)
                return null;

            var name = fluentEntityAttribute.Name ?? source.GetType().Name;
            var fluentObject = new FluentObject(name);

            var props = source.GetType().GetProperties();
            
            foreach (var prop in props)
            {
                var fluentPropertyAttribute = prop.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
                if (fluentPropertyAttribute == null)
                    continue;

                if (fluentPropertyAttribute.IsReadony)
                    continue;

                var condition = ValidateCondition(fluentPropertyAttribute.Condition, fluentPropertyAttribute.ReverseCondition);
                if (!condition)
                    continue;

                var key = fluentPropertyAttribute.Name ?? prop.Name;
                var value = prop.GetValue(source);
                var converter = GetConverter(fluentPropertyAttribute.Converter, prop.PropertyType);
                var valueString = converter != null ? converter.ConvertBack(value) : value.ToString();

                fluentObject.Add(key, valueString);
            }

            return fluentObject.Count > 0 ? fluentObject : null;
        }

        public static T MapFromFluentObject<T>(this FluentObject source) where T : class, new()
        {
            if (source == null)
                return null;

            var fluentEntityAttribute = (typeof(T)).GetCustomAttributes(typeof(FluentEntityAttribute), false).SingleOrDefault() as FluentEntityAttribute;
            if (fluentEntityAttribute == null)
                return null;

            var props = typeof (T).GetProperties();
            var entity = new T();

            foreach (var prop in props)
            {
                var fluentPropertyAttribute = prop.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
                if (fluentPropertyAttribute == null)
                    continue;

                var condition = ValidateCondition(fluentPropertyAttribute.Condition, fluentPropertyAttribute.ReverseCondition);
                if (!condition)
                    continue;

                var key = fluentPropertyAttribute.Name ?? prop.Name;
                if (source.ContainsKey(key))
                {
                    var valueString = source[key];
                    var converter = GetConverter(fluentPropertyAttribute.Converter, prop.PropertyType);
                    var value = converter != null ? converter.Convert(valueString) : default(T);
                    prop.SetValue(entity, value);
                }
            }

            return entity;
        }

        private static IPropertyConverter GetConverter(Type converterType, Type returnType)
        {
            if(converterType != null)
                return (IPropertyConverter)Activator.CreateInstance(converterType);

            return _converterResolver.Resolve(returnType);
        }

        private static bool ValidateCondition(string conditionName, bool reverse = false)
        {
            if (string.IsNullOrWhiteSpace(conditionName))
                return true;

            if (_conditionResolver == null)
                return true;

            return _conditionResolver.IsValid(conditionName, reverse);
        }
    }
}
