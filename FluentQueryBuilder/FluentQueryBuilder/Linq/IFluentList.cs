using System.Collections.Generic;

namespace FluentQueryBuilder.Linq
{
    public interface IFluentList<T>: IFluentQueryable<T> where T : class, new()
    {
        T Add(T entity);
        T Update(T entity);
        int Delete(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        int DeleteRange(IEnumerable<T> entities);
    }
}
