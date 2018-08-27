using System;
using System.Text;
using FluentQueryBuilder.Extensions;

namespace FluentQueryBuilder.Query
{
    public class QueryProvider<T>: IQueryProvider<T> where T : class, new()
    {
        private string _condition;

        public IQueryProvider<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            _condition = predicate.Parse();
            return this;
        }

        public string Build()
        {
            var query = new StringBuilder();

            var entityName = QueryBuilderHelper.GetFluentEntityName<T>();
            var propertyNames = QueryBuilderHelper.GetFluentPropertyNames<T>();
            var selector = string.Format("SELECT {0} FROM {1} ", string.Join(", ", propertyNames), entityName);
            query.AppendLine(selector);


            if (!string.IsNullOrWhiteSpace(_condition))
            {
                var filter = string.Format("WHERE {0} ", _condition);
                query.AppendLine(filter);
            }
                

            return query.ToString();
        }
    }
}
