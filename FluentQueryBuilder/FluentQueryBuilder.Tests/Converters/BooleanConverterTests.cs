using FluentQueryBuilder.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Converters
{
    [TestClass]
    public class BooleanConverterTests
    {
        private IPropertyConverter _propertyConverter;

        [TestInitialize]
        public void InitializeTest()
        {
            _propertyConverter = new BooleanConverter();
        }

        [TestMethod]
        public void ConvertsFromBooleanString()
        {
            var asTrue = (bool)_propertyConverter.Convert("True");
            var asFalse = (bool) _propertyConverter.Convert("False");

            Assert.IsTrue(asTrue);
            Assert.IsFalse(asFalse);
        }

        [TestMethod]
        public void ConvertsToBooleanString()
        {
            var forTrue = _propertyConverter.ConvertBack(true);
            var forFalse = _propertyConverter.ConvertBack(false);

            Assert.AreEqual(forTrue, "True");
            Assert.AreEqual(forFalse, "False");
        }
    }
}
