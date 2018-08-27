using FluentQueryBuilder.Query;
using FluentQueryBuilder.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Query
{
    [TestClass]
    public class QueryProviderTests
    {
        private IQueryProvider<NamedFluentModel> _queryProvider;

        [TestInitialize]
        public void InitializeTest()
        {
            _queryProvider = new QueryProvider<NamedFluentModel>();
        }
        
        [TestMethod]
        public void ShouldBuildWhereQuery()
        {
            var query = _queryProvider.Where(x => x.BooleanProperty == false || (x.IntegerProperty < 10 && x.DoubleProperty > 20.5)).Build();
        }
    }
}
