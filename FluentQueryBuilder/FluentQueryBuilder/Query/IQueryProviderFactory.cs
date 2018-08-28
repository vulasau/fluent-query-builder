namespace FluentQueryBuilder.Query
{
    public interface IQueryProviderFactory
    {
        IQueryProvider<T> Create<T>() where T : class, new();
    }
}
