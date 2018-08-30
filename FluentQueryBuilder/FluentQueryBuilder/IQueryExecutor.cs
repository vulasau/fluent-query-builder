using System.Collections.Generic;

namespace FluentQueryBuilder
{
    public interface IQueryExecutor
    {
        IEnumerable<FluentObject> ExecuteForMultiple(string query);
        FluentObject ExecuteForSingle(string query);
        T ExecuteForScalar<T>(string query);

        IEnumerable<FluentObject> ExecuteForMultiple(string query, string objectName);
        FluentObject ExecuteForSingle(string query, string objectName);
        T ExecuteForScalar<T>(string query, string objectName);
    }
}