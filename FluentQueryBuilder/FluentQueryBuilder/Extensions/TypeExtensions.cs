using System;
using System.Reflection;

namespace FluentQueryBuilder.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool IsClassWithDefaultConstructor(this Type type)
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

        internal static ConstructorInfo DefaultConstructor(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null");

            return type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[0], null);
        }

        internal static object DefaultValue(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type", "Parameter 'type' should not be null");

            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
