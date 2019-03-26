using System;
using System.Linq;
using System.Reflection;
using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Retrieves FluentProperty of given name for given type.
        /// </summary>
        /// <param name="type">Source type</param>
        /// <param name="name">Property name</param>
        /// <returns>Property of given source type with given name, which if has FluentPropertyAttribute</returns>
        public static PropertyInfo GetFluentProperty(this Type type, string name)
        {
            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null");

            if (!type.IsClass)
                return null;

            var prop = type.GetProperties().FirstOrDefault(x =>
            {
                var fluentPropertyAttribute = x.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
                if (fluentPropertyAttribute == null)
                    return false;

                var propertyName = fluentPropertyAttribute.Name ?? x.Name;
                return string.Equals(propertyName, name);
            });

            return prop;
        }

        /// <summary>
        /// Checks if type is class and has default constructor.
        /// </summary>
        /// <param name="type">Source type</param>
        /// <returns>'True' if type is class and has default constructor, 'False' in other cases.</returns>
        public static bool IsClassWithDefaultConstructor(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null");

            if (!type.IsClass)
                return false;

            var defaultConstructor = type.DefaultConstructor();
            if (defaultConstructor == null)
                return false;

            return true;
        }

        /// <summary>
        /// Retrieves default constructor for given type. 
        /// </summary>
        /// <param name="type">Source type</param>
        /// <returns>Default constructor for given type if exists.</returns>
        public static ConstructorInfo DefaultConstructor(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null");

            return type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[0], null);
        }

        /// <summary>
        /// Retrieves default value for given type.
        /// </summary>
        /// <param name="type">Source type</param>
        /// <returns>Default value if type is ValueType and null if type is ReferenceType.</returns>
        public static object DefaultValue(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null");

            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
