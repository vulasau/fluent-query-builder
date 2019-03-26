using System;
using System.Linq;
using System.Reflection;
using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Extensions
{
    public static class PropertyExtensions
    {
        /// <summary>
        /// Sets the value of given property for given FluentObject.
        /// </summary>
        /// <param name="prop">Source property</param>
        /// <param name="source">Source object</param>
        /// <param name="target">Target FluentObject</param>
        public static void SetValue(this PropertyInfo prop, object source, ref FluentObject target)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");

            if (source == null)
                throw new ArgumentNullException("source");

            var fluentPropertyAttribute = prop.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
            if (fluentPropertyAttribute == null)
                throw new ArgumentException("Property is not FluentProperty", "prop");

            object[] converterParameters;
            var converter = prop.GetConverter(out converterParameters);

            var key = fluentPropertyAttribute.Name ?? prop.Name;
            var value = prop.GetValue(source);
            var valueString = converter != null ? converter.ConvertBack(value, converterParameters) : value.ToString();

            target.Add(key, valueString);
        }

        /// <summary>
        /// Sets the value of given property for given object
        /// </summary>
        /// <param name="prop">Source property</param>
        /// <param name="source">Source FluentObject</param>
        /// <param name="target">Target object</param>
        public static void SetValue(this PropertyInfo prop, FluentObject source, ref object target)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");

            if (source == null)
                throw new ArgumentNullException("source");

            var fluentPropertyAttribute = prop.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
            if (fluentPropertyAttribute == null)
                throw new ArgumentException("Property is not FluentProperty", "prop");

            object[] converterParameters;
            var converter = prop.GetConverter(out converterParameters);

            var key = fluentPropertyAttribute.Name ?? prop.Name;

            if (source.ContainsKey(key))
            {
                var valueString = source[key];
                var value = converter != null ? converter.Convert(valueString, converterParameters) : prop.PropertyType.DefaultValue();
                prop.SetValue(target, value);
            }
        }
    }
}
