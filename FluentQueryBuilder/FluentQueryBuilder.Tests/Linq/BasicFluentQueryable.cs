using FluentQueryBuilder.Linq;
using FluentQueryBuilder.Query;

namespace FluentQueryBuilder.Tests.Linq
{
    public class BasicFluentQueryable<T> : FluentList<T> where T : class, new()
    {
        public BasicFluentQueryable(IQueryExecutor queryExecutor, IQueryProviderFactory queryProviderFactory) : base(queryExecutor, queryProviderFactory)
        {

        }
    }
}
