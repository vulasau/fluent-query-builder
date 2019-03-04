using FluentQueryBuilder.Configuration;
using FluentQueryBuilder.Extensions;
using FluentQueryBuilder.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Extensions
{
    [TestClass]
    public class MappingExtensonsTests
    {
        [TestInitialize]
        public void InitializeTest()
        {
            var conditionResolver = new ConditionResolverBase();
            ObjectMapperConfiguration.Use(conditionResolver);
        }

        [TestMethod]
        public void ShouldMapToFluentObject_WithAttributeNames()
        {
            var fluentModel = new NamedFluentModel(true);
            
            var fluentObject = fluentModel.MapToFluentObject();

            Assert.IsNotNull(fluentObject);
            Assert.AreEqual(fluentObject.Name, NamedFluentModel.MODEL_NAME);
            
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.BOOLEAN_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.DATE_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.DOUBLE_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.INTEGER_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.OBJECT_PROPERTY_NAME));
            Assert.IsFalse(fluentObject.ContainsKey(NamedFluentModel.READONLY_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NamedFluentModel.CONDITIONED_PROPERTY_NAME));
            Assert.IsFalse(fluentObject.ContainsKey(NamedFluentModel.CONDITIONED_REVERSE_PROPERTY_NAME));

            Assert.AreEqual(fluentObject[NamedFluentModel.BOOLEAN_PROPERTY_NAME], NamedFluentModel.BOOLEAN_VALUE.ToString());
            Assert.AreEqual(fluentObject[NamedFluentModel.DATE_PROPERTY_NAME], NamedFluentModel.DATE_VALUE.ToString());
            Assert.AreEqual(fluentObject[NamedFluentModel.DOUBLE_PROPERTY_NAME], NamedFluentModel.DOUBLE_VALUE.ToString());
            Assert.AreEqual(fluentObject[NamedFluentModel.INTEGER_PROPERTY_NAME], NamedFluentModel.INTEGER_VALUE.ToString());
            Assert.AreEqual(fluentObject[NamedFluentModel.OBJECT_PROPERTY_NAME], NamedFluentModel.OBJECT_VALUE.ToString());
            Assert.AreEqual(fluentObject[NamedFluentModel.CONDITIONED_PROPERTY_NAME], NamedFluentModel.CONDITIONED_VALUE);
        }

        [TestMethod]
        public void ShouldMapToFluentObject_WithoutAttributeNames()
        {
            var fluentModel = new FluentModel(true);
            
            var fluentObject = fluentModel.MapToFluentObject();

            Assert.IsNotNull(fluentObject);
            Assert.AreEqual(fluentObject.Name, FluentModel.MODEL_NAME);

            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.BOOLEAN_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.DATE_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.DOUBLE_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.INTEGER_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.OBJECT_PROPERTY_NAME));
            Assert.IsFalse(fluentObject.ContainsKey(FluentModel.READONLY_PROPERTY_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(FluentModel.CONDITIONED_PROPERTY_NAME));
            Assert.IsFalse(fluentObject.ContainsKey(FluentModel.CONDITIONED_REVERSE_PROPERTY_NAME));

            Assert.AreEqual(fluentObject[FluentModel.BOOLEAN_PROPERTY_NAME], FluentModel.BOOLEAN_VALUE.ToString());
            Assert.AreEqual(fluentObject[FluentModel.DATE_PROPERTY_NAME], FluentModel.DATE_VALUE.ToString());
            Assert.AreEqual(fluentObject[FluentModel.DOUBLE_PROPERTY_NAME], FluentModel.DOUBLE_VALUE.ToString());
            Assert.AreEqual(fluentObject[FluentModel.INTEGER_PROPERTY_NAME], FluentModel.INTEGER_VALUE.ToString());
            Assert.AreEqual(fluentObject[FluentModel.OBJECT_PROPERTY_NAME], FluentModel.OBJECT_VALUE.ToString());
            Assert.AreEqual(fluentObject[FluentModel.CONDITIONED_PROPERTY_NAME], FluentModel.CONDITIONED_VALUE);
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
            fluentObject.Add(NamedFluentModel.READONLY_PROPERTY_NAME, NamedFluentModel.READONLY_VALUE);
            fluentObject.Add(NamedFluentModel.CONDITIONED_PROPERTY_NAME, NamedFluentModel.CONDITIONED_VALUE);
            fluentObject.Add(NamedFluentModel.CONDITIONED_REVERSE_PROPERTY_NAME, NamedFluentModel.CONDITIONED_REVERSE_VALUE);

            var fluentModel = fluentObject.MapFromFluentObject<NamedFluentModel>();

            Assert.IsNotNull(fluentModel);

            Assert.AreEqual(fluentModel.BooleanProperty, NamedFluentModel.BOOLEAN_VALUE);
            Assert.AreEqual(fluentModel.DateProperty, NamedFluentModel.DATE_VALUE);
            Assert.AreEqual(fluentModel.DoubleProperty, NamedFluentModel.DOUBLE_VALUE);
            Assert.AreEqual(fluentModel.IntegerProperty, NamedFluentModel.INTEGER_VALUE);
            Assert.AreEqual(fluentModel.ObjectProperty, NamedFluentModel.OBJECT_VALUE);
            Assert.AreEqual(fluentModel.ReadonlyProperty, NamedFluentModel.READONLY_VALUE);
            Assert.AreEqual(fluentModel.ConditionedProperty, NamedFluentModel.CONDITIONED_VALUE);
            Assert.AreNotEqual(fluentModel.ConditionedReverseProperty, NamedFluentModel.CONDITIONED_REVERSE_VALUE);
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
            fluentObject.Add(FluentModel.READONLY_PROPERTY_NAME, FluentModel.READONLY_VALUE);
            fluentObject.Add(FluentModel.CONDITIONED_PROPERTY_NAME, FluentModel.CONDITIONED_VALUE);
            fluentObject.Add(FluentModel.CONDITIONED_REVERSE_PROPERTY_NAME, FluentModel.CONDITIONED_REVERSE_VALUE);

            var fluentModel = fluentObject.MapFromFluentObject<FluentModel>();

            Assert.IsNotNull(fluentModel);

            Assert.AreEqual(fluentModel.BooleanProperty, FluentModel.BOOLEAN_VALUE);
            Assert.AreEqual(fluentModel.DateProperty, FluentModel.DATE_VALUE);
            Assert.AreEqual(fluentModel.DoubleProperty, FluentModel.DOUBLE_VALUE);
            Assert.AreEqual(fluentModel.IntegerProperty, FluentModel.INTEGER_VALUE);
            Assert.AreEqual(fluentModel.ObjectProperty, FluentModel.OBJECT_VALUE);
            Assert.AreEqual(fluentModel.ReadonlyProperty, FluentModel.READONLY_VALUE);
            Assert.AreEqual(fluentModel.ConditionedProperty, FluentModel.CONDITIONED_VALUE);
            Assert.AreNotEqual(fluentModel.ConditionedReverseProperty, FluentModel.CONDITIONED_REVERSE_VALUE);
        }

        [TestMethod]
        public void ShouldMapFromNestedModelToFluentObject()
        {
            var nestedModel = new NestedModel(false);

            var fluentObject = nestedModel.MapToFluentObject();

            Assert.IsNotNull(fluentObject);
            Assert.AreEqual(fluentObject.Name, NestedModelFields.MODEL_NAME);

            Assert.IsTrue(fluentObject.ContainsKey(NestedModelFields.FIELD_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NestedModelFields.CONDITIONAL_FIELD_NAME));
            Assert.IsFalse(fluentObject.ContainsKey(NestedModelFields.CONDITIONAL_REVERSED_FIELD_NAME));
            Assert.IsFalse(fluentObject.ContainsKey(NestedModelFields.READONLY_FIELD_NAME));
            Assert.IsTrue(fluentObject.ContainsKey(NestedModelFields.DEPENDENT_FIELD_NAME));
            Assert.IsFalse(fluentObject.ContainsKey(NestedModelFields.DEPENDENT_REVERSED_FIELD_NAME));

            Assert.AreEqual(fluentObject[NestedModelFields.FIELD_NAME], NestedModelFields.FIELD_VALUE);
            Assert.AreEqual(fluentObject[NestedModelFields.CONDITIONAL_FIELD_NAME], NestedModelFields.CONDITIONAL_FIELD_VALUE.ToString());
            Assert.AreEqual(fluentObject[NestedModelFields.DEPENDENT_FIELD_NAME], NestedModelFields.DEPENDENT_FIELD_VALUE.ToString());
        }

        [TestMethod]
        public void ShouldMapFromFluentObjectToNestedModel()
        {
            var fluentObject = new FluentObject(NestedModelFields.MODEL_NAME);
            fluentObject.Add(NestedModelFields.FIELD_NAME, NestedModelFields.FIELD_VALUE.ToString());
            fluentObject.Add(NestedModelFields.CONDITIONAL_FIELD_NAME, NestedModelFields.CONDITIONAL_FIELD_VALUE.ToString());
            fluentObject.Add(NestedModelFields.CONDITIONAL_REVERSED_FIELD_NAME, NestedModelFields.CONDITIONAL_REVERSED_FIELD_VALUE.ToString());
            fluentObject.Add(NestedModelFields.READONLY_FIELD_NAME, NestedModelFields.READONLY_FIELD_VALUE.ToString());
            fluentObject.Add(NestedModelFields.DEPENDENT_FIELD_NAME, NestedModelFields.DEPENDENT_FIELD_VALUE.ToString());
            fluentObject.Add(NestedModelFields.DEPENDENT_REVERSED_FIELD_NAME, NestedModelFields.DEPENDENT_REVERSED_FIELD_VALUE.ToString());

            var nestedModel = fluentObject.MapFromFluentObject<NestedModel>();

            Assert.IsNotNull(nestedModel);
            Assert.AreEqual(nestedModel.StringProperty, NestedModelFields.FIELD_VALUE);
            Assert.AreEqual(nestedModel.ConditionalProperty, NestedModelFields.CONDITIONAL_FIELD_VALUE);
            Assert.AreEqual(nestedModel.ConditionalReversedProperty, 0);
            Assert.AreEqual(nestedModel.ReadonlyProperty, NestedModelFields.READONLY_FIELD_VALUE);
            Assert.AreEqual(nestedModel.DependentProperty, NestedModelFields.DEPENDENT_FIELD_VALUE);
            Assert.AreEqual(nestedModel.DependentReversedProperty, NestedModelFields.DEPENDENT_REVERSED_FIELD_VALUE);
        }
    }
}
