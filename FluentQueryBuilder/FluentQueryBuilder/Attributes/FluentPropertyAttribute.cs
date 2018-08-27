using System;
using System.Linq;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FluentPropertyAttribute : Attribute
    {
        /// <summary>
        /// Name of the property for mapping and serializaton/deserialization process. 
        /// If not set, property name itself will be used.
        /// Should not be empty string.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Name of condition in mapping and serializaton/deserialization process.
        /// If property is set, IConditionResolver will validate it.
        /// If condition is valid, property will be included to mapping and serialization/deserialization process, otherwise not.
        /// Should not be empty string.
        /// </summary>
        public string Condition { get; private set; }

        /// <summary>
        /// Type of converter which is used for mapping process.
        /// If set, conerter of this type will be used for property value mapping during serialization/deserialization process.
        /// If not set, one of default converters will be used, based on the property type.
        /// Converter should implement IPropertyConverter interface.
        /// </summary>
        public Type Converter { get; private set; }

        public FluentPropertyAttribute(string name = null, string condition = null, Type converter = null)
        {
            if (name != null && name == string.Empty)
                throw new ArgumentException("Parameter 'name' can not be empty string.", "name");

            if (condition != null && condition == string.Empty)
                throw new ArgumentException("Parameter 'condition' can not be empty string.", "condition");

            if (converter != null && !converter.GetInterfaces().Contains(typeof(IPropertyConverter)))
                throw new ArgumentException("Converter should should implement 'IPropertyConverter' interface", "converter");

            Name = name;
            Condition = condition;
            Converter = converter;
        }
    }
}
