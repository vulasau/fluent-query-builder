using System;

namespace FluentQueryBuilder.Attributes
{
    /// <summary>
    /// Attribute allows FluentProperty to be mapped or not depending on other boolean property value of same class.
    /// Used only in write/update operations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DependencyAttribute : Attribute
    {
        /// <summary>
        /// Name of dependent property in the same class, which indicates whether field will be mapped or not.
        /// When set, mapper will search for property of given name in the same class and try to resolve it's value. 
        /// If resolved dependent property value is 'true', property will be mapped, otherwise not.
        /// Expects dependent property type to be of type Boolean.
        /// Can not depend on properties marked with FluentProperty attribute.
        /// Does not resolve hierachical dependencies.
        /// Should not be empty string.
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Value indicates if dependency logic should be inversed in mapping process.
        /// If property is set to 'true' and dependent property value is resolved as 'true', property will not be mapped.
        /// Same logic vise versa.
        /// Parameter is 'false' by default.
        /// </summary>
        public bool Reverse { get; private set; }

        public DependencyAttribute(string propertyName, bool reverse = false)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName", "Parameter 'propertyName' can not be null or empty string.");

            PropertyName = propertyName;
            Reverse = reverse;
        }
    }
}
