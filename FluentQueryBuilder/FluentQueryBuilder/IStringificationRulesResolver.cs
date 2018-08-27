using System;

namespace FluentQueryBuilder
{
    public interface IStringificationRulesResolver
    {
        bool RequiresStringification(Type type);
    }
}
