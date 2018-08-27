using System;

namespace FluentQueryBuilder
{
    public class StringificationRulesResolver : IStringificationRulesResolver
    {
        public bool RequiresStringification(Type type)
        {
            return type == typeof(string)
               || type == typeof(DateTime)
               || type == typeof(DateTime?);
        }
    }
}
