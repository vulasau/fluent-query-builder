using System.Collections.Generic;
using System.Linq;
using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Query
{
    public class QueryBuilderHelper
    {
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

                var key = fluentPropertyAttribute.Name ?? prop.Name;
                propertyNames.Add(key);
            }

            return propertyNames;
        }
    }
}
