using System.Collections.Generic;
using System.Linq;

namespace FluentQueryBuilder
{
    public abstract class QueryExecutor : IQueryExecutor
    {
        public virtual IEnumerable<FluentObject> ExecuteForMultiple(string query)
        {
            return Enumerable.Empty<FluentObject>();
        }

        public virtual FluentObject ExecuteForSingle(string query)
        {
            return null;
        }

        public T ExecuteForScalar<T>(string query)
        {
            return default(T);
        }


        public IEnumerable<FluentObject> ExecuteForMultiple(string query, string objectName)
        {
            return Enumerable.Empty<FluentObject>();
        }

        public FluentObject ExecuteForSingle(string query, string objectName)
        {
            return null;
        }

        public T ExecuteForScalar<T>(string query, string objectName)
        {
            return default(T);
        }
    }
}
