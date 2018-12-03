using System;
using System.Collections.Generic;
using System.Linq;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Configuration;
using FluentQueryBuilder.Extensions;

namespace FluentQueryBuilder.Query
{
    public static class QueryBuilderHelper
    {
        public static IConditionResolver ConditionResolver { get; private set; }

        static QueryBuilderHelper()
        {
            ConditionResolver = ObjectMapperConfiguration.ConditionResolver;
        }

        public static string GetFluentEntityName<T>() where T : class, new()
        {
            return GetFluentEntityName(typeof(T));
        }

        public static string GetFluentEntityName(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null");

            if (!type.IsClassWithDefaultConstructor())
                throw new ArgumentException("Parameter 'type' should reflect a class type with default parameterless constructor.", "type");

            var fluentEntityAttribute = type.GetCustomAttributes(typeof(FluentEntityAttribute), false).SingleOrDefault() as FluentEntityAttribute;
            if (fluentEntityAttribute == null)
                return null;

            var name = fluentEntityAttribute.Name ?? type.Name;

            return name;
        }

        public static IEnumerable<string> GetFluentPropertyNames<T>() where T : class, new()
        {
            return GetFluentPropertyNames(typeof(T));
        }

        public static IEnumerable<string> GetFluentPropertyNames(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null");

            if (!type.IsClassWithDefaultConstructor())
                throw new ArgumentException("Parameter 'type' should reflect a class type with default parameterless constructor.", "type");

            var fluentEntityAttribute = type.GetCustomAttributes(typeof(FluentEntityAttribute), false).SingleOrDefault() as FluentEntityAttribute;
            if (fluentEntityAttribute == null)
                return null;

            var props = type.GetProperties().OrderBy(x => x.Name).ToArray();
            var propertyNames = new List<string>();

            foreach (var prop in props)
            {
                var fluentPropertyAttribute = prop.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;

                if (fluentPropertyAttribute == null)
                    continue;

                if (!ResolveCondition(fluentPropertyAttribute.Condition, fluentPropertyAttribute.ReverseCondition))
                    continue;

                var key = fluentPropertyAttribute.Name ?? prop.Name;
                propertyNames.Add(key);
            }

            return propertyNames;
        }

        private static bool ResolveCondition(string conditionName, bool reverse = false)
        {
            if (string.IsNullOrWhiteSpace(conditionName))
                return true;

            return ConditionResolver.IsValid(conditionName, reverse);
        }
    }
}
