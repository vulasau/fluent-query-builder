using System;
using System.Collections.Generic;
using System.Linq;
using FluentQueryBuilder.Extensions;
using FluentQueryBuilder.Query;

namespace FluentQueryBuilder.Linq
{
    public abstract class FluentQueriable<T>: IFluentQueriable<T> where T : class, new()
    {
        protected readonly IQueryProviderFactory _queryProviderFactory;

        protected IQueryExecutor _queryExecutor;
        protected IQueryProvider<T> _queryProvider;

        protected FluentQueriable(IQueryExecutor queryExecutor, IQueryProviderFactory queryProviderFactory)
        {
            _queryProviderFactory = queryProviderFactory;

            _queryExecutor = queryExecutor;
            _queryProvider = _queryProviderFactory.Create<T>();
        }

        protected FluentQueriable(IQueryExecutor queryExecutor)
        {
            _queryProviderFactory = new QueryProviderFactory();

            _queryExecutor = queryExecutor;
            _queryProvider = _queryProviderFactory.Create<T>();
        } 

        public virtual T FirstOrDefault()
        {
            var query = _queryProvider.FirstOrDefault();
            var items = _queryExecutor.Execute(query);

            if (items == null || !items.Any())
                return null;

            Reset();
            return items.FirstOrDefault().MapFromFluentObject<T>();
        }

        public virtual T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query = _queryProvider.FirstOrDefault(predicate);
            var items = _queryExecutor.Execute(query);

            if (items == null || !items.Any())
                return null;

            Reset();
            return items.FirstOrDefault().MapFromFluentObject<T>();
        }

        public virtual IFluentQueriable<T> Take(int number)
        {
            _queryProvider = _queryProvider.Take(number);
            return this;
        }

        public virtual IFluentQueriable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            _queryProvider = _queryProvider.Where(predicate);
            return this;
        }

        public virtual IFluentQueriable<T> Select<TOut>(System.Linq.Expressions.Expression<Func<T, TOut>> selctor)
        {
            _queryProvider = _queryProvider.Select(selctor);
            return this;
        }

        public virtual IFluentQueriable<T> Select<TOut>() where TOut : class, new()
        {
            _queryProvider = _queryProvider.Select<TOut>();
            return this;
        }

        public virtual IFluentQueriable<T> OrderBy<TOut>(System.Linq.Expressions.Expression<Func<T, TOut>> selector)
        {
            _queryProvider = _queryProvider.OrderBy(selector);
            return this;
        }

        public virtual IFluentQueriable<T> OrderByDescending<TOut>(System.Linq.Expressions.Expression<Func<T, TOut>> selector)
        {
            _queryProvider = _queryProvider.OrderByDescending(selector);
            return this;
        }

        public virtual IFluentQueriable<T> OrderBy<TOut>(System.Linq.Expressions.Expression<Func<T, TOut>> selector, bool ascending)
        {
            _queryProvider = _queryProvider.OrderBy(selector, ascending);
            return this;
        }

        public virtual int Count()
        {
            var query = _queryProvider.Count();
            var result = _queryExecutor.ExecuteCountQuery(query);

            Reset();
            return result;
        }

        public virtual int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query = _queryProvider.Count(predicate);
            var result = _queryExecutor.ExecuteCountQuery(query);

            Reset();
            return result;
        }

        public virtual T[] ToArray()
        {
            var query = _queryProvider.ToArray();
            var items = _queryExecutor.Execute(query).ToArray();

            var entities = new List<T>();
            foreach (var item in items)
            {
                var entity = item.MapFromFluentObject<T>();
                entities.Add(entity);
            }

            Reset();
            return entities.ToArray();
        }

        private void Reset()
        {
            _queryProvider = _queryProviderFactory.Create<T>();
        }
    }
}
