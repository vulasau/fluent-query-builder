namespace FluentQueryBuilder.Query
{
    public class QueryProviderFactory : IQueryProviderFactory
    {
        public IQueryProvider<T> Create<T>() where T : class, new()
        {
            return new QueryProvider<T>();
        }
    }
}
