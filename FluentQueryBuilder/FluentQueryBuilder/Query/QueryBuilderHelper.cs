using System.Collections.Generic;
using System.Linq;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Configuration;

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
            var fluentEntityAttribute = typeof (T).GetCustomAttributes(typeof (FluentEntityAttribute), false).SingleOrDefault() as FluentEntityAttribute;
            if (fluentEntityAttribute == null)
                return null;

            var name = fluentEntityAttribute.Name ?? typeof (T).Name;

            return name;
        }

        public static IEnumerable<string> GetFluentPropertyNames<T>() where T : class, new()
        {
            var fluentEntityAttribute = typeof (T).GetCustomAttributes(typeof (FluentEntityAttribute), false).SingleOrDefault() as FluentEntityAttribute;
            if (fluentEntityAttribute == null)
                return null;

            var props = typeof (T).GetProperties().OrderBy(x => x.Name).ToArray();
            var propertyNames = new List<string>();

            foreach (var prop in props)
            {
                var fluentPropertyAttribute = prop.GetCustomAttributes(typeof (FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
                
                if (fluentPropertyAttribute == null)
                    continue;

                if(!ResolveCondition(fluentPropertyAttribute.Condition))
                    continue;

                var key = fluentPropertyAttribute.Name ?? prop.Name;
                propertyNames.Add(key);
            }

            return propertyNames;
        }

        private static bool ResolveCondition(string conditionName)
        {
            if (string.IsNullOrWhiteSpace(conditionName))
                return true;

            return ConditionResolver.IsValid(conditionName);
        }
    }
}
