using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Query
{
    [TestClass]
    public class QueryProviderLimitsTests : QueryProviderTests
    {
        [TestMethod]
        public void ShouldBuildLimitationQuery()
        {
            var query = _queryProvider.Take(25).ToArray();
            var expectedString = "SELECT boolean, conditioned, conditioned_reverse, date, double, integer, object, readonly FROM model \r\nLIMIT 25 \r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildConditionalLimitationQuery()
        {
            var query = _queryProvider.Where(x => x.BooleanProperty == false).Take(25).ToArray();
            var expectedString = "SELECT boolean, conditioned, conditioned_reverse, date, double, integer, object, readonly FROM model \r\nWHERE (boolean = False) \r\nLIMIT 25 \r\n";
            Assert.AreEqual(expectedString, query);
        }
    }
}
