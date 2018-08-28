using FluentQueryBuilder.Query;
using FluentQueryBuilder.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Query
{
    [TestClass]
    public class QueryProviderTests
    {
        protected IQueryProvider<NamedFluentModel> _queryProvider;

        [TestInitialize]
        public void InitializeTest()
        {
            _queryProvider = new QueryProvider<NamedFluentModel>();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            _queryProvider = null;
        }
    }
}
