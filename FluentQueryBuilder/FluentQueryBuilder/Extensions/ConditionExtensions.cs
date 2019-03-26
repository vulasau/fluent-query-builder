using System;
using System.Linq;
using System.Reflection;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Configuration;

namespace FluentQueryBuilder.Extensions
{
    public static class ConditionExtensions
    {
        private static readonly IConditionResolver _conditionResolver;

        static ConditionExtensions()
        {
            _conditionResolver = ObjectMapperConfiguration.ConditionResolver;
        }

        /// <summary>
        /// Validates condition for given property using ObjectMapperConfiguration.ConditionResolver
        /// </summary>
        /// <param name="prop">Source property</param>
        /// <returns>
        /// 'Ture' if condition is not set,
        /// 'True' if condition is set and resolved as 'True',
        /// 'False' in other cases,
        /// </returns>
        public static bool ValidateCondition(this PropertyInfo prop)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");

            var conditionAttribute = prop.GetCustomAttributes(typeof(ConditionAttribute), false).SingleOrDefault() as ConditionAttribute;
            if (conditionAttribute == null)
                return true;

            return ValidateCondition(conditionAttribute.Name, conditionAttribute.Reverse);
        }

        private static bool ValidateCondition(string conditionName, bool reverse)
        {
            if (string.IsNullOrWhiteSpace(conditionName))
                return true;

            if (_conditionResolver == null)
                return true;

            return _conditionResolver.IsValid(conditionName, reverse);
        }
    }
}
