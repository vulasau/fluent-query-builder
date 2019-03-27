using FluentQueryBuilder.Query;
using FluentQueryBuilder.Tests.Models;
using FluentQueryBuilder.Tests.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Query
{
    [TestClass]
    public class QueryProviderSingleSelectorsTests: QueryProviderTests
    {
        [TestMethod]
        public void ShouldBuildFirstOrDefaultQuery()
        {
            var query = _queryProvider.FirstOrDefault();
            var expectedString = "SELECT boolean, conditioned, date, double, integer, object, readonly FROM model \r\nLIMIT 1\r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildFirstOrDefaultConditionalQuery()
        {
            var query = _queryProvider.FirstOrDefault(x => x.DoubleProperty != 55.5);
            var expectedString = "SELECT boolean, conditioned, date, double, integer, object, readonly FROM model \r\nWHERE (double != 55.5) \r\nLIMIT 1\r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildFirstOrDefaultMultiConditionalQuery()
        {
            var query = _queryProvider.Where(x => x.BooleanProperty == false).FirstOrDefault(x => x.DoubleProperty != 55.5);
            var expectedString = "SELECT boolean, conditioned, date, double, integer, object, readonly FROM model \r\nWHERE (boolean = False)  AND (double != 55.5) \r\nLIMIT 1\r\n";
            Assert.AreEqual(expectedString, query);
        }

        [TestMethod]
        public void ShouldBuildFirstOrDefaultEnumComparisonPredicateQuery()
        {
            var queryProvider = new QueryProvider<ConvertableModel>();

            var query = queryProvider.FirstOrDefault(x => x.ConvertableProperty != EnumValue.Unknown);
            var expectedString = "SELECT ConvertableProperty_c FROM ConvertableModel \r\nWHERE (ConvertableProperty_c != NULL) \r\nLIMIT 1\r\n";
            Assert.AreEqual(expectedString, query);
        }
    }
}
