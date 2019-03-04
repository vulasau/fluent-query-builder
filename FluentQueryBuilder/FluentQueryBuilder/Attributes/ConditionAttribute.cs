using System;

namespace FluentQueryBuilder.Attributes
{
    /// <summary>
    /// Attribute allows FluentProperty to be mapped or not depending on condition, Resolved by IConditionResolver mechanism.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConditionAttribute : Attribute
    {
        /// <summary>
        /// Name of condition in mapping and serializaton/deserialization process.
        /// If property is set, IConditionResolver will validate it.
        /// If condition is valid, property will be included to mapping and serialization/deserialization process, otherwise not.
        /// Should not be empty string.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value indicates if the condition logic should be inversed in mapping process.
        /// If property is set to 'true' and condition is resolved as 'true', property will not be mapped.
        /// Same logic vise versa.
        /// Parameter is 'false' by default.
        /// </summary>
        public bool Reverse { get; private set; }

        public ConditionAttribute(string name, bool reverse = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "Parameter 'name' can not be null or empty string.");

            Name = name;
            Reverse = reverse;
        }
    }
}