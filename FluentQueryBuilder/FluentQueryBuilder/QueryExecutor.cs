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
    }
}
