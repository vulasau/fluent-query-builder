using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentQueryBuilder.Query
{
    public interface IQueryProvider<T>
    {
        string Add(T entity);
        string Update(T entity);
        string Delete(T entity);
        string AddRange(IEnumerable<T> entities);
        string UpdateRange(IEnumerable<T> entities);
        string DeleteRange(IEnumerable<T> entities);

        string FirstOrDefault();
        string FirstOrDefault(Expression<Func<T, bool>> predicate);
        IQueryProvider<T> Take(int number);
        IQueryProvider<T> Where(Expression<Func<T, bool>> predicate);
        IQueryProvider<T> Select<TOut>(Expression<Func<T, TOut>> selctor);
        IQueryProvider<T> Select<TOut>() where TOut : class, new();
        IQueryProvider<T> OrderBy<TOut>(Expression<Func<T, TOut>> selector);
        IQueryProvider<T> OrderByDescending<TOut>(Expression<Func<T, TOut>> selector);
        IQueryProvider<T> OrderBy<TOut>(Expression<Func<T, TOut>> selector, bool ascending);
        string Count();
        string Count(Expression<Func<T, bool>> predicate);
        string ToArray();
    }
}
