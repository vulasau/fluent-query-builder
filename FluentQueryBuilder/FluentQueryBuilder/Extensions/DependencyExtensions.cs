using System;
using System.Linq;
using System.Reflection;
using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Extensions
{
    public static class DependencyExtensions
    {
        /// <summary>
        /// Validates if dependent property condition is valid for given property.
        /// </summary>
        /// <param name="prop">Source property</param>
        /// <param name="source">Source object</param>
        /// <returns>
        /// 'True' if DependencyAttribute is not set,
        /// 'True' if DependencyAttribute is set and dependent property values is 'True',
        /// 'False' in other cases.
        /// </returns>
        public static bool ValidateDependencyCondition(this PropertyInfo prop, object source)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");

            if (source == null)
                throw new ArgumentNullException("source");

            var dependencyAttribute = prop.GetCustomAttributes(typeof(DependencyAttribute), false).SingleOrDefault() as DependencyAttribute;
            if (dependencyAttribute == null)
                return true;

            var properties = source.GetType().GetProperties();

            return ValidateDependency(dependencyAttribute.PropertyName, dependencyAttribute.Reverse, source, properties);
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
