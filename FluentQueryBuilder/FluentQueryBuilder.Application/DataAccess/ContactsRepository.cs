using FluentQueryBuilder.Linq;
using FluentQueryBuilder.Application.Models;

namespace FluentQueryBuilder.Application.DataAccess
{
    public class ContactsRepository: FluentQueriable<Contact>
    {
        public ContactsRepository(IQueryExecutor queryExecutor) : base(queryExecutor)
        {

        }
    }
}