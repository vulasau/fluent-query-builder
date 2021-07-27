using System;
using System.Linq.Expressions;
using System.Text;
using FluentQueryBuilder.Extensions;

namespace FluentQueryBuilder.Query
{
    public class QueryProvider<T>: IQueryProvider<T> where T : class, new()
    {
        protected string _selector;
        protected string _condition;
        protected string _ordering;
        protected string _limit;

        public QueryProvider()
        {

        }

        protected QueryProvider(string selector, string condition, string ordering, string limit)
        {
            _selector = selector;
            _condition = condition;
            _ordering = ordering;
            _limit = limit;
        }

        public virtual string Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual string Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual string Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual string AddRange(System.Collections.Generic.IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual string UpdateRange(System.Collections.Generic.IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual string DeleteRange(System.Collections.Generic.IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual string FirstOrDefault()
        {
            var query = new StringBuilder();

            var selector = _selector ?? GetSelector<T>();
            query.AppendLine(selector);

            if (!string.IsNullOrWhiteSpace(_condition))
                query.AppendLine(_condition);

            if (!string.IsNullOrWhiteSpace(_ordering))
                query.AppendLine(_ordering);

            var limit = "LIMIT 1";
            query.AppendLine(limit);

            return query.ToString();
        }

        public virtual string FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            var query = new StringBuilder();

            var selector = _selector ?? GetSelector<T>();
            query.AppendLine(selector);

            var condition = predicate.Parse();

            if (!string.IsNullOrWhiteSpace(_condition))
                _condition = string.Format("{0} AND ({1}) ", _condition, condition);
            else
                _condition = string.Format("WHERE ({0}) ", condition);

            query.AppendLine(_condition);

            if (!string.IsNullOrWhiteSpace(_ordering))
                query.AppendLine(_ordering);

            var limit = "LIMIT 1";
            query.AppendLine(limit);

            return query.ToString();
        }

        public virtual IQueryProvider<T> Take(int number)
        {
            if (!string.IsNullOrWhiteSpace(_limit))
                throw new InvalidOperationException("Only one limit per expression is supported.");

            _limit = string.Format("LIMIT {0} ", number);
            return this;
        }

        public virtual IQueryProvider<T> Where(Expression<Func<T, bool>> predicate)
        {
            var condition = predicate.Parse();

            if (!string.IsNullOrWhiteSpace(_condition))
                _condition = string.Format("{0} AND ({1}) ", _condition, condition);
            else
                _condition = string.Format("WHERE ({0}) ", condition);

            return this;
        }

        public virtual IQueryProvider<T> Select<TOut>(Expression<Func<T, TOut>> selctor)
        {
            if (!string.IsNullOrWhiteSpace(_selector))
                throw new InvalidOperationException("Only one selector per expression is supported.");

            _selector = GetSelector(selctor);
            return this;
        }

        public virtual IQueryProvider<TOut> Select<TOut>() where TOut : class, new() 
        {
            if (!string.IsNullOrWhiteSpace(_selector))
                throw new InvalidOperationException("Only one selector per expression is supported.");

            _selector = GetSelector<TOut>();
            return new QueryProvider<TOut>(_selector, _condition, _ordering, _limit);
        }

        public virtual IQueryProvider<T> OrderBy<TOut>(Expression<Func<T, TOut>> selector)
        {
            return OrderBy(selector, true);
        }

        public virtual IQueryProvider<T> OrderByDescending<TOut>(Expression<Func<T, TOut>> selector)
        {
            return OrderBy(selector, false);
        }

        public virtual IQueryProvider<T> OrderBy<TOut>(Expression<Func<T, TOut>> selector, bool ascending)
        {
            if (!string.IsNullOrWhiteSpace(_ordering))
                throw new InvalidOperationException("Only one ordering per expression is supported.");

            var propertyName = selector.ParseMemberExpression();
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Argument 'selector' should be a member access expression.");

            _ordering = string.Format("ORDER BY {0} {1} NULLS LAST ", propertyName, ascending ? "ASC" : "DESC");
            return this;
        }

        public virtual string Count()
        {
            if (!string.IsNullOrWhiteSpace(_ordering))
                throw new InvalidOperationException("'Count()' and 'ORDER BY' conditions can't be used together");

            var query = new StringBuilder();
            
            var entityName = QueryBuilderHelper.GetFluentEntityName<T>();
            var selector = string.Format("SELECT count() FROM {0} ", entityName);
            query.AppendLine(selector);

            if (!string.IsNullOrWhiteSpace(_condition))
                query.AppendLine(_condition);

            if (!string.IsNullOrWhiteSpace(_limit))
                query.AppendLine(_limit);

            return query.ToString();
        }

        public virtual string Count(Expression<Func<T, bool>> predicate)
        {
            var query = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(_ordering))
                throw new InvalidOperationException("'Count()' and 'ORDER BY' conditions can't be used together");

            var entityName = QueryBuilderHelper.GetFluentEntityName<T>();
            var selector = string.Format("SELECT count() FROM {0} ", entityName);
            query.AppendLine(selector);

            var condition = predicate.Parse();

            if (!string.IsNullOrWhiteSpace(_condition))
                _condition = string.Format("{0} AND ({1}) ", _condition, condition);
            else
                _condition = string.Format("WHERE ({0}) ", condition);

            query.AppendLine(_condition);

            if (!string.IsNullOrWhiteSpace(_limit))
                query.AppendLine(_limit);

            return query.ToString();
        }

        public virtual string ToArray()
        {
            var query = new StringBuilder();

            var selector = _selector ?? GetSelector<T>();
            query.AppendLine(selector);

            if (!string.IsNullOrWhiteSpace(_condition))
                query.AppendLine(_condition);

            if (!string.IsNullOrWhiteSpace(_ordering))
                query.AppendLine(_ordering);

            if (!string.IsNullOrWhiteSpace(_limit))
                query.AppendLine(_limit);

            return query.ToString();
        }

        protected virtual string GetSelector<TOut>() where TOut : class, new()
        {
            var entityName = QueryBuilderHelper.GetFluentEntityName<T>();
            var propertyNames = QueryBuilderHelper.GetFluentPropertyNames<TOut>();
            var selector = string.Format("SELECT {0} FROM {1} ", string.Join(", ", propertyNames), entityName);

            return selector;
        }

        protected virtual string GetSelector<TOut>(Expression<Func<T, TOut>> predicate)
        {
            var entityName = QueryBuilderHelper.GetFluentEntityName<T>();
            var propertyName = predicate.ParseMemberExpression();
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Argument 'predicate' should be a member access expression.");

            var selector = string.Format("SELECT {0} FROM {1} ", propertyName, entityName);
            return selector;
        }
    }
}
