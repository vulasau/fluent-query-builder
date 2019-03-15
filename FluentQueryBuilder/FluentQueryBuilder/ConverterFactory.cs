using System;
using System.Linq;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder
{
    public class ConverterFactory : IConverterFactory
    {
        public virtual IPropertyConverter CreateConverter(Type type)
        {
            ValidateType(type);

            return (IPropertyConverter)Activator.CreateInstance(type);
        }

        protected void ValidateType(Type type)
        {
            if (!type.GetInterfaces().Contains(typeof(IPropertyConverter)))
                throw new ArgumentOutOfRangeException("type", "Parameter 'type' should represent a type implementing IPropertyConverter interface");
        }
    }
}
