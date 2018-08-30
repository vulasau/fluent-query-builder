namespace FluentQueryBuilder.Query
{
    public class QueryProviderFactory : IQueryProviderFactory
    {
        public virtual IQueryProvider<T> Create<T>() where T : class, new()
        {
            return new QueryProvider<T>();
        }
    }
}
