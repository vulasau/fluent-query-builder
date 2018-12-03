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
            return MapToFluentObject(source, typeof(T));
        }

        public static FluentObject MapToFluentObject(this object source, Type type)
        {
            if (source == null)
                return null;

            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null.");

            if (!type.IsClassWithDefaultConstructor())
                throw new ArgumentException("Parameter 'type' should reflect a class type with parameterless constructor.", "type");

            if (source.GetType() != type)
                throw new ArgumentException("Parameter 'type' should reflect source element type 'T'.", "type");

            var fluentEntityAttribute = type.GetCustomAttributes(typeof(FluentEntityAttribute), false).SingleOrDefault() as FluentEntityAttribute;
            if (fluentEntityAttribute == null)
                return null;

            var name = fluentEntityAttribute.Name ?? type.Name;
            var fluentObject = new FluentObject(name);

            var props = type.GetProperties();

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
            return (T)source.MapFromFluentObject(typeof(T));
        }

        public static object MapFromFluentObject(this FluentObject source, Type type)
        {
            if (source == null)
                return null;

            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null.");

            if (!type.IsClassWithDefaultConstructor())
                throw new ArgumentException("Parameter 'type' should reflect a class type with parameterless constructor.", "type");

            var fluentEntityAttribute = type.GetCustomAttributes(typeof(FluentEntityAttribute), false).SingleOrDefault() as FluentEntityAttribute;
            if (fluentEntityAttribute == null)
                return null;

            var props = type.GetProperties();
            var constructor = type.DefaultConstructor();
            var entity = constructor.Invoke(null);

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
                    var value = converter != null ? converter.Convert(valueString) : prop.PropertyType.DefaultValue();
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
