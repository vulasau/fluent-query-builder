using FluentQueryBuilder.Extensions;
using FluentQueryBuilder.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Extensions
{
    [TestClass]
    public class MappingExtensonsTests
    {
        [TestMethod]
        public void ShouldMapToFluentObject_WithAttributeNames()
        {
            var fluentModel = new NamedFluentModel();
            
            var fluentObject = fluentModel.MapToFluentObject();

            Assert.IsNotNull(fluentObject);
            Assert.AreEqual(fluentObject.Name, NamedFluentModel.MODEL_NAME);
            
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.BOOLEAN_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.DATE_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.DOUBLE_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.INTEGER_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.OBJECT_PROPERTY_NAME));

            Assert.AreEqual(fluentObject[NamedFluentModel.BOOLEAN_PROPERTY_NAME], NamedFluentModel.BOOLEAN_VALUE.ToString());
            Assert.AreEqual(fluentObject[NamedFluentModel.DATE_PROPERTY_NAME], NamedFluentModel.DATE_VALUE.ToString());
            Assert.AreEqual(fluentObject[NamedFluentModel.DOUBLE_PROPERTY_NAME], NamedFluentModel.DOUBLE_VALUE.ToString());
            Assert.AreEqual(fluentObject[NamedFluentModel.INTEGER_PROPERTY_NAME], NamedFluentModel.INTEGER_VALUE.ToString());
            Assert.AreEqual(fluentObject[NamedFluentModel.OBJECT_PROPERTY_NAME], NamedFluentModel.OBJECT_VALUE.ToString());
        }

        [TestMethod]
        public void ShouldMapToFluentObject_WithoutAttributeNames()
        {
            var fluentModel = new FluentModel();
            
            var fluentObject = fluentModel.MapToFluentObject();

            Assert.IsNotNull(fluentObject);
            Assert.AreEqual(fluentObject.Name, FluentModel.MODEL_NAME);

            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.BOOLEAN_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.DATE_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.DOUBLE_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.INTEGER_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.OBJECT_PROPERTY_NAME));

            Assert.AreEqual(fluentObject[FluentModel.BOOLEAN_PROPERTY_NAME], FluentModel.BOOLEAN_VALUE.ToString());
            Assert.AreEqual(fluentObject[FluentModel.DATE_PROPERTY_NAME], FluentModel.DATE_VALUE.ToString());
            Assert.AreEqual(fluentObject[FluentModel.DOUBLE_PROPERTY_NAME], FluentModel.DOUBLE_VALUE.ToString());
            Assert.AreEqual(fluentObject[FluentModel.INTEGER_PROPERTY_NAME], FluentModel.INTEGER_VALUE.ToString());
            Assert.AreEqual(fluentObject[FluentModel.OBJECT_PROPERTY_NAME], FluentModel.OBJECT_VALUE.ToString());
        }

        [TestMethod]
        public void ShouldMapFromFluentObject_WithAttributeNames()
        {
            var fluentObject = new FluentObject(NamedFluentModel.MODEL_NAME);
            fluentObject.Add(NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.BOOLEAN_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.DATE_PROPERTY_NAME, NamedFluentModel.DATE_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.DOUBLE_PROPERTY_NAME, NamedFluentModel.DOUBLE_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.INTEGER_PROPERTY_NAME, NamedFluentModel.INTEGER_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.OBJECT_PROPERTY_NAME, NamedFluentModel.OBJECT_VALUE.ToString());

            var fluentModel = fluentObject.MapFromFluentObject<NamedFluentModel>();

            Assert.IsNotNull(fluentModel);

            Assert.AreEqual(fluentModel.BooleanProperty, NamedFluentModel.BOOLEAN_VALUE);
            Assert.AreEqual(fluentModel.DateProperty, NamedFluentModel.DATE_VALUE);
            Assert.AreEqual(fluentModel.DoubleProperty, NamedFluentModel.DOUBLE_VALUE);
            Assert.AreEqual(fluentModel.IntegerProperty, NamedFluentModel.INTEGER_VALUE);
            Assert.AreEqual(fluentModel.ObjectProperty, NamedFluentModel.OBJECT_VALUE);
        }

        [TestMethod]
        public void ShouldMapFromFluentObject_WithoutAttributeNames()
        {
            var fluentObject = new FluentObject(FluentModel.MODEL_NAME);
            fluentObject.Add(FluentModel.BOOLEAN_PROPERTY_NAME, FluentModel.BOOLEAN_VALUE.ToString());
            fluentObject.Add(FluentModel.DATE_PROPERTY_NAME, FluentModel.DATE_VALUE.ToString());
            fluentObject.Add(FluentModel.DOUBLE_PROPERTY_NAME, FluentModel.DOUBLE_VALUE.ToString());
            fluentObject.Add(FluentModel.INTEGER_PROPERTY_NAME, FluentModel.INTEGER_VALUE.ToString());
            fluentObject.Add(FluentModel.OBJECT_PROPERTY_NAME, FluentModel.OBJECT_VALUE.ToString());

            var fluentModel = fluentObject.MapFromFluentObject<FluentModel>();

            Assert.IsNotNull(fluentModel);

            Assert.AreEqual(fluentModel.BooleanProperty, FluentModel.BOOLEAN_VALUE);
            Assert.AreEqual(fluentModel.DateProperty, FluentModel.DATE_VALUE);
            Assert.AreEqual(fluentModel.DoubleProperty, FluentModel.DOUBLE_VALUE);
            Assert.AreEqual(fluentModel.IntegerProperty, FluentModel.INTEGER_VALUE);
            Assert.AreEqual(fluentModel.ObjectProperty, FluentModel.OBJECT_VALUE);
        }
    }
}
