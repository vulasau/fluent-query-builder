using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Query
{
    [TestClass]
    public class QueryProviderOrderingTests: QueryProviderTests
    {
        [TestMethod]
        public void ShouldBuildAscendingOrderingQuery()
        {
            var query = _queryProvider.OrderBy(x => x.IntegerProperty).ToArray();
            var expectedString = "SELECT boolean, conditioned, date, double, integer, object, readonly FROM model \r\nORDER BY integer ASC NULLS LAST \r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildDescendingOrderingQuery()
        {
            var query = _queryProvider.OrderByDescending(x => x.IntegerProperty).ToArray();
            var expectedString = "SELECT boolean, conditioned, date, double, integer, object, readonly FROM model \r\nORDER BY integer DESC NULLS LAST \r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildConditionalAscendingOrderingQuery()
        {
            var query = _queryProvider.Where(x => x.BooleanProperty == true).OrderBy(x => x.IntegerProperty).ToArray();
            var expectedString = "SELECT boolean, conditioned, date, double, integer, object, readonly FROM model \r\nWHERE (boolean = True) \r\nORDER BY integer ASC NULLS LAST \r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildDescendingConditionalOrderingQuery()
        {
            var query = _queryProvider.Where(x => x.BooleanProperty == true).OrderByDescending(x => x.IntegerProperty).ToArray();
            var expectedString = "SELECT boolean, conditioned, date, double, integer, object, readonly FROM model \r\nWHERE (boolean = True) \r\nORDER BY integer DESC NULLS LAST \r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildAscendingOrderingConditionalSelectionQuery()
        {
            var query = _queryProvider.Where(x => x.BooleanProperty == true).OrderBy(x => x.IntegerProperty).Select(x => x.DoubleProperty).ToArray();
            var expectedString = "SELECT double FROM model \r\nWHERE (boolean = True) \r\nORDER BY integer ASC NULLS LAST \r\n";
            Assert.AreEqual(expectedString, query);
        }
    }
}
