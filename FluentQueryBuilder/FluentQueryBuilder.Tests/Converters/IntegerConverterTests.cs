using FluentQueryBuilder.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Converters
{
    [TestClass]
    public class IntegerConverterTests
    {
        private IPropertyConverter _propertyConverter;

        [TestInitialize]
        public void InitializeTest()
        {
            _propertyConverter = new IntegerConverter();
        }

        [TestMethod]
        public void ConvertsFromIntegerString()
        {
            var forInteger = (int)_propertyConverter.Convert("115");
            Assert.AreEqual(forInteger, 115);
        }

        [TestMethod]
        public void ConvertsToIntegerString()
        {
            var forInteger = _propertyConverter.ConvertBack(115);
            Assert.AreEqual(forInteger, "115");
        }
    }
}
