using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests
{
    [TestClass]
    public class StringificationRulesResolverTests
    {
        private IStringificationRulesResolver _stringificationRulesResolver;

        [TestInitialize]
        public void InitializeTest()
        {
            _stringificationRulesResolver = new StringificationRulesResolver();
        }

        [TestMethod]
        public void RequiresStringification_ReturnsTrue()
        {
            var forString = _stringificationRulesResolver.RequiresStringification(typeof (string));
            var forDateTime = _stringificationRulesResolver.RequiresStringification(typeof (DateTime));
            var forNullableDateTime = _stringificationRulesResolver.RequiresStringification(typeof (DateTime?));

            Assert.IsTrue(forString);
            Assert.IsTrue(forDateTime);
            Assert.IsTrue(forNullableDateTime);
        }

        [TestMethod]
        public void RequiresStringification_ReturnsFalse()
        {
            var forDouble = _stringificationRulesResolver.RequiresStringification(typeof (double));
            var forInt = _stringificationRulesResolver.RequiresStringification(typeof(int));
            var forFloat = _stringificationRulesResolver.RequiresStringification(typeof(float));
            var forBoolean = _stringificationRulesResolver.RequiresStringification(typeof (bool));

            Assert.IsFalse(forDouble);
            Assert.IsFalse(forInt);
            Assert.IsFalse(forFloat);
            Assert.IsFalse(forBoolean);
        }
    }
}
