using System;
using System.Linq;
using System.Reflection;
using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Extensions
{
    public static class MappingExtensons
    {
        public static FluentObject MapToFluentObject<T>(this T source) where T : class, new()
        {
            return MapToFluentObject(source, typeof(T));
        }

        public static T MapFromFluentObject<T>(this FluentObject source) where T : class, new()
        {
            return (T)source.MapFromFluentObject(typeof(T));
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
                prop.MapPropertyToFluentObject(source, ref fluentObject);
            }

            return fluentObject.Count > 0 ? fluentObject : null;
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
                prop.MapPropertyFromFluentObject(source, ref entity);
            }

            return entity;
        }



        public static void MapPropertyToFluentObject(this PropertyInfo prop, object source, ref FluentObject target)
        {
            if (prop == null)
                return;

            if (source == null)
                throw new ArgumentNullException("source", "Parameter 'source' should not be null.");

            var fluentPropertyAttribute = prop.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
            if (fluentPropertyAttribute == null)
                return;

            if (fluentPropertyAttribute.IsReadony)
                return;

            var condition = prop.ValidateCondition();
            if (!condition)
                return;

            var dependency = prop.ValidateDependencyCondition(source);
            if (!dependency)
                return;

            prop.SetValue(source, ref target);
        }

        public static void MapPropertyFromFluentObject(this PropertyInfo prop, FluentObject source, ref object target)
        {
            if (prop == null)
                return;

            if (source == null)
                throw new ArgumentNullException("source", "Parameter 'source' should not be null.");

            var fluentPropertyAttribute = prop.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
            if (fluentPropertyAttribute == null)
                return;

            var condition = prop.ValidateCondition();
            if (!condition)
                return;

            prop.SetValue(source, ref target);
        }
    }
}
