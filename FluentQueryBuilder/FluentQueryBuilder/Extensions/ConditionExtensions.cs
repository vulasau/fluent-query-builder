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
