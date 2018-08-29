using System.Collections.Generic;
using System.Linq;

namespace FluentQueryBuilder.Application.DataAccess
{
    public class DataConnector : IQueryExecutor
    {
        private readonly IEnumerable<FluentObject> _objects;

        public DataConnector()
        {
            var fluentObject = new FluentObject("e_contacts");
            fluentObject.Add("ID", "4457");
            fluentObject.Add("p_name", "Benjamin Colin");
            fluentObject.Add("p_birthDate", "1978-8-12");
            fluentObject.Add("p_weight", "78.4");
            fluentObject.Add("p_doNotTrack", "111555999");

            _objects = new[] {fluentObject};
        }

        public IEnumerable<FluentObject> ExecuteForMultiple(string query)
        {
            return _objects;
        }

        public int ExecuteCountQuery(string query)
        {
            return _objects.Count();
        }
    }
}
