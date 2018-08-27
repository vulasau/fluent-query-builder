using System;
using FluentQueryBuilder.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests
{
    [TestClass]
    public class ConverterResolverTests
    {
        private IConverterResolver _converterResolver;

        [TestInitialize]
        public void InitializeTest()
        {
            _converterResolver = new ConverterResolver();
        }

        [TestMethod]
        public void Resolve_ReturnsSpecificConverter()
        {
            var forBoolean = _converterResolver.Resolve(typeof (bool));
            var forDateTime = _converterResolver.Resolve(typeof (DateTime));
            var forDouble = _converterResolver.Resolve(typeof (double));
            var forInteger = _converterResolver.Resolve(typeof (int));

            Assert.IsInstanceOfType(forBoolean, typeof(BooleanConverter));
            Assert.IsInstanceOfType(forDateTime, typeof(DateTimeConverter));
            Assert.IsInstanceOfType(forDouble, typeof(DoubleConverter));
            Assert.IsInstanceOfType(forInteger, typeof(IntegerConverter));
        }

        [TestMethod]
        public void Resolve_ReturnsObjectConverter()
        {
            var forOther = _converterResolver.Resolve(typeof (decimal));

            Assert.IsInstanceOfType(forOther, typeof (ObjectConverter));
        }

        [TestMethod, ExpectedException(typeof (ArgumentNullException))]
        public void Resolve_ThrowsException_WhenParameterIsNull()
        {
            var forNothing = _converterResolver.Resolve(null);
        }
    }
}
