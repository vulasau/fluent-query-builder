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

        public virtual T Add(T entity)
        {
            var query = _queryProvider.Add(entity);
            var item = _queryExecutor.ExecuteForSingle(query);

            Reset();
            return item != null ? item.MapFromFluentObject<T>() : null;
        }

        public virtual T Update(T entity)
        {
            var query = _queryProvider.Update(entity);
            var item = _queryExecutor.ExecuteForSingle(query);

            Reset();
            return item != null ? item.MapFromFluentObject<T>() : null;
        }

        public virtual int Delete(T entity)
        {
            var query = _queryProvider.Delete(entity);
            var number = _queryExecutor.ExecuteForScalar<int>(query);

            Reset();
            return number;
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            var query = _queryProvider.AddRange(entities);
            var items = _queryExecutor.ExecuteForMultiple(query);

            var mappedEntities = new List<T>();
            foreach (var item in items)
            {
                var mappedEntity = item.MapFromFluentObject<T>();
                mappedEntities.Add(mappedEntity);
            }

            Reset();
            return mappedEntities;
        }

        public virtual IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            var query = _queryProvider.UpdateRange(entities);
            var items = _queryExecutor.ExecuteForMultiple(query);

            var mappedEntities = new List<T>();
            foreach (var item in items)
            {
                var mappedEntity = item.MapFromFluentObject<T>();
                mappedEntities.Add(mappedEntity);
            }

            Reset();
            return mappedEntities;
        }

        public virtual int DeleteRange(IEnumerable<T> entities)
        {
            var query = _queryProvider.DeleteRange(entities);
            var number = _queryExecutor.ExecuteForScalar<int>(query);

            Reset();
            return number;
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
