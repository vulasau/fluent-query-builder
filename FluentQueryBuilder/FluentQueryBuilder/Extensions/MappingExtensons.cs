using System;
using System.Linq;
using System.Reflection;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Configuration;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder.Extensions
{
    public static class MappingExtensons
    {
        private static readonly IConditionResolver _conditionResolver;
        private static readonly IConverterResolver _converterResolver;
        private static readonly IConverterFactory _converterFactory;

        static MappingExtensons()
        {
            _conditionResolver = ObjectMapperConfiguration.ConditionResolver;
            _converterResolver = ObjectMapperConfiguration.ConverterResolver;
            _converterFactory = ObjectMapperConfiguration.ConverterFactory;
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

                var conditionAttribute = prop.GetCustomAttributes(typeof(ConditionAttribute), false).SingleOrDefault() as ConditionAttribute;
                var condition = conditionAttribute == null ? true : ValidateCondition(conditionAttribute.Name, conditionAttribute.Reverse);
                if (!condition)
                    continue;

                var dependencyAttribute = prop.GetCustomAttributes(typeof(DependencyAttribute), false).SingleOrDefault() as DependencyAttribute;
                var dependency = dependencyAttribute == null ? true : ValidateDependency(dependencyAttribute.PropertyName, dependencyAttribute.Reverse, source, props);
                if (!dependency)
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

                var conditionAttribute = prop.GetCustomAttributes(typeof(ConditionAttribute), false).SingleOrDefault() as ConditionAttribute;
                var condition = conditionAttribute == null ? true : ValidateCondition(conditionAttribute.Name, conditionAttribute.Reverse);
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
            if (converterType != null)
                return _converterFactory.CreateConverter(converterType);

            return _converterResolver.Resolve(returnType);
        }

        private static bool ValidateCondition(string conditionName, bool reverse)
        {
            if (string.IsNullOrWhiteSpace(conditionName))
                return true;

            if (_conditionResolver == null)
                return true;

            return _conditionResolver.IsValid(conditionName, reverse);
        }

        private static bool ValidateDependency(string propertyName, bool reverse, object source, PropertyInfo[] properties)
        {
            var property = properties.FirstOrDefault(prop => string.Equals(prop.Name, propertyName));

            if (property == null)
                throw new ArgumentException(string.Format("Dependent property '{0}' was not found among class public properties.", propertyName), "dependentPropertyName");

            if (property.PropertyType != typeof(bool))
                throw new ArgumentException(string.Format("Dependent property '{0}' should be of type 'Boolean'", propertyName), "dependentPropertyName");

            var fluentPropertyAttribute = property.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
            if (fluentPropertyAttribute != null)
                throw new InvalidOperationException("Dependent property can not point to a FluentProperty.");

            var propertyValue = (bool)property.GetValue(source);

            return reverse ? !propertyValue : propertyValue;
        }
    }
}
