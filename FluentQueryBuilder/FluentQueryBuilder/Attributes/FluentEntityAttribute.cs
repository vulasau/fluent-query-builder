using System;

namespace FluentQueryBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FluentEntityAttribute : Attribute
    {
        /// <summary>
        /// Name of the entity for maping and serialization/deserialization process.
        /// If not set, class name itself will be used.
        /// Should not be empty string.
        /// </summary>
        public string Name { get; private set; }

        public FluentEntityAttribute(string name = null)
        {
            if (name != null && name == string.Empty)
                throw new ArgumentException("Parameter 'name' can not be empty string.", "name");

            Name = name;
        }
    }
}
