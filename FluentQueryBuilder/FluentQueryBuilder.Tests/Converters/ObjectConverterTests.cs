using FluentQueryBuilder.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Converters
{
    [TestClass]
    public class ObjectConverterTests
    {
        private IPropertyConverter _propertyConverter;

        [TestInitialize]
        public void InitializeTest()
        {
            _propertyConverter = new ObjectConverter();
        }

        [TestMethod]
        public void ConvertsFromObjectString()
        {
            var obj = "object";
            var forObject = _propertyConverter.Convert(obj);

            Assert.AreEqual(forObject, obj);
        }

        [TestMethod]
        public void ConvertsToObjectString()
        {
            var obj = "object";
            var forObject = _propertyConverter.ConvertBack(obj);

            Assert.AreEqual(forObject, (string) obj);
        }
    }
}
