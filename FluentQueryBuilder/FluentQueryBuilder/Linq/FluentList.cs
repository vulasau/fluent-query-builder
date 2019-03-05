using System.Collections.Generic;
using FluentQueryBuilder.Extensions;
using FluentQueryBuilder.Query;

namespace FluentQueryBuilder.Linq
{
    public class FluentList<T> : FluentQueryable<T>, IFluentList<T> where T : class, new()
    {
        protected FluentList(IQueryExecutor queryExecutor) : base(queryExecutor)
        {
        }

        protected FluentList(IQueryExecutor queryExecutor, IQueryProviderFactory queryProviderFactory) : base(queryExecutor, queryProviderFactory)
        {
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

        public virtual T ExecuteForSingle(string query)
        {
            var item = _queryExecutor.ExecuteForSingle(query);
            return item != null ? item.MapFromFluentObject<T>() : null;
        }

        public virtual IEnumerable<T> ExecuteForMultiple(string query)
        {
            var items = _queryExecutor.ExecuteForMultiple(query);

            var entities = new List<T>();
            foreach (var item in items)
            {
                var entity = item.MapFromFluentObject<T>();
                entities.Add(entity);
            }

            return entities;
        }
    }
}
