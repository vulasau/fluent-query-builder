using System;
using System.Linq.Expressions;

namespace FluentQueryBuilder.Query
{
    public interface IQueryProvider<T>
    {
        IQueryProvider<T> Where(Expression<Func<T, bool>> predicate);

        string Build();
    }
}
