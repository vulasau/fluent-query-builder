using System;
using System.Collections.Generic;
using System.Linq;
using FluentQueryBuilder.Extensions;
using FluentQueryBuilder.Query;

namespace FluentQueryBuilder.Linq
{
    public class FluentQueryable<T>: IFluentQueryable<T> where T : class, new()
    {
        protected readonly IQueryProviderFactory _queryProviderFactory;

        protected IQueryExecutor _queryExecutor;
        protected IQueryProvider<T> _queryProvider;

        protected FluentQueryable(IQueryExecutor queryExecutor, IQueryProviderFactory queryProviderFactory)
        {
            _queryProviderFactory = queryProviderFactory;

            _queryExecutor = queryExecutor;
            _queryProvider = _queryProviderFactory.Create<T>();
        }

        protected FluentQueryable(IQueryExecutor queryExecutor)
        {
            _queryProviderFactory = new QueryProviderFactory();

            _queryExecutor = queryExecutor;
            _queryProvider = _queryProviderFactory.Create<T>();
        }

        protected FluentQueryable(IQueryExecutor queryExecutor, IQueryProvider<T> queryProvider, IQueryProviderFactory queryProviderFactory)
        {
            _queryProviderFactory = queryProviderFactory;

            _queryExecutor = queryExecutor;
            _queryProvider = queryProvider;
        }

        public virtual T FirstOrDefault()
        {
            var query = _queryProvider.FirstOrDefault();
            var item = _queryExecutor.ExecuteForSingle(query);

            Reset();
            return item != null ? item.MapFromFluentObject<T>() : null;
        }

        public virtual T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query = _queryProvider.FirstOrDefault(predicate);
            var item = _queryExecutor.ExecuteForSingle(query);

            Reset();
            return item != null ? item.MapFromFluentObject<T>() : null;
        }

        public virtual IFluentQueryable<T> Take(int number)
        {
            _queryProvider = _queryProvider.Take(number);
            return this;
        }

        public virtual IFluentQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            _queryProvider = _queryProvider.Where(predicate);
            return this;
        }

        public virtual IFluentQueryable<T> Select<TOut>(System.Linq.Expressions.Expression<Func<T, TOut>> selctor)
        {
            _queryProvider = _queryProvider.Select(selctor);
            return this;
        }

        public virtual IFluentQueryable<TOut> Select<TOut>() where TOut : class, new()
        {
            Reset();

            var queryExecutor = _queryExecutor;
            var queryProvider = _queryProvider.Select<TOut>();
            var queryProviderFactory = _queryProviderFactory;

            return new FluentQueryable<TOut>(queryExecutor, queryProvider, queryProviderFactory);
        }

        public virtual IFluentQueryable<T> OrderBy<TOut>(System.Linq.Expressions.Expression<Func<T, TOut>> selector)
        {
            _queryProvider = _queryProvider.OrderBy(selector);
            return this;
        }

        public virtual IFluentQueryable<T> OrderByDescending<TOut>(System.Linq.Expressions.Expression<Func<T, TOut>> selector)
        {
            _queryProvider = _queryProvider.OrderByDescending(selector);
            return this;
        }

        public virtual IFluentQueryable<T> OrderBy<TOut>(System.Linq.Expressions.Expression<Func<T, TOut>> selector, bool ascending)
        {
            _queryProvider = _queryProvider.OrderBy(selector, ascending);
            return this;
        }

        public virtual int Count()
        {
            var query = _queryProvider.Count();
            var result = _queryExecutor.ExecuteForScalar<int>(query);

            Reset();
            return result;
        }

        public virtual int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query = _queryProvider.Count(predicate);
            var result = _queryExecutor.ExecuteForScalar<int>(query);

            Reset();
            return result;
        }

        public virtual T[] ToArray()
        {
            var query = _queryProvider.ToArray();
            var items = _queryExecutor.ExecuteForMultiple(query).ToArray();

            var entities = new List<T>();
            foreach (var item in items)
            {
                var entity = item.MapFromFluentObject<T>();
                entities.Add(entity);
            }

            Reset();
            return entities.ToArray();
        }


        protected virtual void Reset()
        {
            _queryProvider = _queryProviderFactory.Create<T>();
        }
    }
}
