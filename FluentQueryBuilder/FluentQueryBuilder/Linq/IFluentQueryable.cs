using System;
using System.Linq.Expressions;

namespace FluentQueryBuilder.Linq
{
    public interface IFluentQueryable<T>
    {
        T FirstOrDefault();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        IFluentQueryable<T> Take(int number);
        IFluentQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IFluentQueryable<T> Select<TOut>(Expression<Func<T, TOut>> selctor);
        IFluentQueryable<TOut> Select<TOut>() where TOut : class, new();
        IFluentQueryable<T> OrderBy<TOut>(Expression<Func<T, TOut>> selector);
        IFluentQueryable<T> OrderByDescending<TOut>(Expression<Func<T, TOut>> selector);
        IFluentQueryable<T> OrderBy<TOut>(Expression<Func<T, TOut>> selector, bool ascending);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        T[] ToArray();
    }
}
