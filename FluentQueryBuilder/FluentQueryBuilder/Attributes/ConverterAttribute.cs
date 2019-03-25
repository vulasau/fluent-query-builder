using System;
using System.Linq;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder.Attributes
{
    public class ConverterAttribute : Attribute
    {
        /// <summary>
        /// Type of converter which is used for mapping process.
        /// If set, conerter of this type will be used for property value mapping during serialization/deserialization process.
        /// If not set, one of default converters will be used, based on the property type.
        /// Converter should implement IPropertyConverter interface.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Type of value returnable by converter during deserialization process.
        /// Parameter is 'System.String' by default.
        public Type ValueType { get; private set; }

        /// <summary>
        /// Array of parameters, which will be passed to converter for property value mapping during serialization/deserialization process.
        /// Parameter is 'object[0]' by default.
        /// </summary>
        public object[] Parameters { get; private set; }

        public ConverterAttribute(Type type, Type valueType = null, params object[] parameters)
        {
            if (type != null && !type.GetInterfaces().Contains(typeof(IPropertyConverter)))
                throw new ArgumentException("Converter should should implement 'IPropertyConverter' interface", "converter");

            Type = type;
            ValueType = valueType ?? typeof(string);
            Parameters = parameters ?? new object[0];
        }
    }
}