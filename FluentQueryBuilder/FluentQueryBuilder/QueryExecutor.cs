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

        public virtual T ExecuteForScalar<T>(string query)
        {
            return default(T);
        }


        public virtual IEnumerable<FluentObject> ExecuteForMultiple(string query, string objectName)
        {
            return Enumerable.Empty<FluentObject>();
        }

        public virtual FluentObject ExecuteForSingle(string query, string objectName)
        {
            return null;
        }

        public virtual T ExecuteForScalar<T>(string query, string objectName)
        {
            return default(T);
        }
    }
}
