using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Query
{
    [TestClass]
    public class QueryProviderMathTests: QueryProviderTests
    {
        [TestMethod]
        public void ShouldBuildCountQuery()
        {
            var query = _queryProvider.Count();
            var expectedString = "SELECT count() FROM model \r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildConditionalCountQuery()
        {
            var query = _queryProvider.Where(x => x.BooleanProperty == false).Count();
            var expectedString = "SELECT count() FROM model \r\nWHERE (boolean = False) \r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildConditionalLimitedCountQuery()
        {
            var query = _queryProvider.Take(25).Where(x => x.BooleanProperty == false).Count();
            var expectedString = "SELECT count() FROM model \r\nWHERE (boolean = False) \r\nLIMIT 25 \r\n";
            Assert.AreEqual(expectedString, query);
        }
    }
}