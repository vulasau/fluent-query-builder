using System.Collections.Generic;

namespace FluentQueryBuilder
{
    public interface IQueryExecutor
    {
        IEnumerable<FluentObject> Execute(string query);
        int ExecuteCountQuery(string query);
    }
}