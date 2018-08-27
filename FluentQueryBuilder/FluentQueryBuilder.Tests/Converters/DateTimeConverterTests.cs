using System;
using FluentQueryBuilder.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Converters
{
    [TestClass]
    public class DateTimeConverterTests
    {
        private IPropertyConverter _propertyConverter;

        [TestInitialize]
        public void InitializeTest()
        {
            _propertyConverter = new DateTimeConverter();
        }

        [TestMethod]
        public void ConvertsFromDateTimeString()
        {
            var now = DateTime.Now;
            var dateString = now.ToString();

            var forDateTime = (DateTime)_propertyConverter.Convert(dateString);
            Assert.AreEqual(now.Day, forDateTime.Day);
            Assert.AreEqual(now.Month, forDateTime.Month);
            Assert.AreEqual(now.Year, forDateTime.Year);
            Assert.AreEqual(now.Hour, forDateTime.Hour);
            Assert.AreEqual(now.Minute, forDateTime.Minute);
            Assert.AreEqual(now.Second, forDateTime.Second);
        }

        [TestMethod]
        public void ConvertsToDateTimeString()
        {
            var now = DateTime.Now;
            var dateString = now.ToString();

            var forDateTime = _propertyConverter.ConvertBack(now);

            Assert.AreEqual(dateString, forDateTime);
        }
    }
}
