using System.Linq;
using FluentQueryBuilder.Linq;
using FluentQueryBuilder.Query;
using FluentQueryBuilder.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace FluentQueryBuilder.Tests.Linq
{
    [TestClass]
    public class FluentQueryableTests
    {
        protected IQueryExecutor _queryExecutor;
        protected IQueryProviderFactory _queryProviderFactory;
        protected IQueryProvider<NamedFluentModelBase> _queryProvider;
        protected IFluentQueryable<NamedFluentModelBase> _fluentQueriable;

        protected FluentObject _fluentObject;
            
        [TestInitialize]
        public void InitializeTest()
        {
            _queryExecutor = MockRepository.GenerateMock<IQueryExecutor>();
            _queryProviderFactory = new QueryProviderFactory();
            _queryProvider = new QueryProvider<NamedFluentModelBase>();
            _fluentQueriable = new BasicFluentQueryable<NamedFluentModelBase>(_queryExecutor, _queryProviderFactory);

            _fluentObject = new FluentObject(NamedFluentModelBase.MODEL_NAME);
            _fluentObject.Add(NamedFluentModelBase.BOOLEAN_PROPERTY_NAME, NamedFluentModelBase.BOOLEAN_VALUE.ToString());
            _fluentObject.Add(NamedFluentModelBase.DATE_PROPERTY_NAME, NamedFluentModelBase.DATE_VALUE.ToString());
        }

        [TestCleanup]
        public void CleanupTest()
        {
            _queryExecutor = null;
            _queryProviderFactory = null;
            _queryProvider = null;
            _fluentQueriable = null;
            _fluentObject = null;
        }

        [TestMethod]
        public void FirstOrDefault()
        {
            var query = "SELECT boolean, date FROM model \r\nLIMIT 1\r\n";
            _queryExecutor.Expect(x => x.ExecuteForSingle(query)).Return(_fluentObject);

            var item = _fluentQueriable.FirstOrDefault();

            Assert.IsNotNull(item);
            Assert.AreEqual(item.BooleanProperty, NamedFluentModelBase.BOOLEAN_VALUE);
            Assert.AreEqual(item.DateProperty, NamedFluentModelBase.DATE_VALUE);
            _queryExecutor.VerifyAllExpectations();
        }

        [TestMethod]
        public void FirstOrDefaultWithConditions()
        {
            var query = "SELECT boolean, date FROM model \r\nWHERE (boolean = False) \r\nLIMIT 1\r\n";
            _queryExecutor.Expect(x => x.ExecuteForSingle(query)).Return(_fluentObject);

            var item = _fluentQueriable.FirstOrDefault(x => x.BooleanProperty == false);

            Assert.IsNotNull(item);
            Assert.AreEqual(item.BooleanProperty, NamedFluentModelBase.BOOLEAN_VALUE);
            Assert.AreEqual(item.DateProperty, NamedFluentModelBase.DATE_VALUE);
            _queryExecutor.VerifyAllExpectations();
        }

        [TestMethod]
        public void Where()
        {
            var query = "SELECT boolean, date FROM model \r\nWHERE (boolean = True) \r\n";
            _queryExecutor.Expect(x => x.ExecuteForMultiple(query)).Return(new[] { _fluentObject });

            var items = _fluentQueriable.Where(x => x.BooleanProperty == true).ToArray();

            Assert.IsNotNull(items);
            Assert.AreEqual(1, items.Count());
            Assert.AreEqual(items[0].BooleanProperty, NamedFluentModelBase.BOOLEAN_VALUE);
            Assert.AreEqual(items[0].DateProperty, NamedFluentModelBase.DATE_VALUE);
            _queryExecutor.VerifyAllExpectations();
        }
    }
}
