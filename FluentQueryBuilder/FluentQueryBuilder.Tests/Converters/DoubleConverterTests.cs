using FluentQueryBuilder.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Converters
{
    [TestClass]
    public class DoubleConverterTests
    {
        private IPropertyConverter _propertyConverter;

        [TestInitialize]
        public void InitializeTest()
        {
            _propertyConverter = new DoubleConverter();
        }

        [TestMethod]
        public void ConvertsFromDoubleString()
        {
            var forDouble = (double)_propertyConverter.Convert("1.25");
            Assert.AreEqual(1.25, forDouble);
        }

        [TestMethod]
        public void ConvertsToDoubleString()
        {
            var forDouble = _propertyConverter.ConvertBack(1.25);
            Assert.AreEqual(forDouble, "1.25");
        }
    }
}
