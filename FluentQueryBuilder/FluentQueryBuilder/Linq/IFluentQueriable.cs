using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentQueryBuilder.Linq
{
    public interface IFluentQueriable<T>
    {
        T Add(T entity);
        T Update(T entity);
        int Delete(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        int DeleteRange(IEnumerable<T> entities);

        T FirstOrDefault();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        IFluentQueriable<T> Take(int number);
        IFluentQueriable<T> Where(Expression<Func<T, bool>> predicate);
        IFluentQueriable<T> Select<TOut>(Expression<Func<T, TOut>> selctor);
        IFluentQueriable<T> Select<TOut>() where TOut : class, new();
        IFluentQueriable<T> OrderBy<TOut>(Expression<Func<T, TOut>> selector);
        IFluentQueriable<T> OrderByDescending<TOut>(Expression<Func<T, TOut>> selector);
        IFluentQueriable<T> OrderBy<TOut>(Expression<Func<T, TOut>> selector, bool ascending);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        T[] ToArray();
    }
}
