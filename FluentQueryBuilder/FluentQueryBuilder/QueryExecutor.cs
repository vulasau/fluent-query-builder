using System.Collections.Generic;
using System.Linq;

namespace FluentQueryBuilder
{
    public class QueryExecutor : IQueryExecutor
    {
        public IEnumerable<FluentObject> Execute(string query)
        {
            return Enumerable.Empty<FluentObject>();
        }

        public int ExecuteCountQuery(string query)
        {
            return 0;
        }
    }
}
