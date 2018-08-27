using System;
using System.Collections.Generic;
using System.Text;
using FluentQueryBuilder.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests
{
    [TestClass]
    public class FluentObjectTests
    {
        [TestMethod]
        public void FluentObject_ConstructsWithoutParameters()
        {
            var fluentObject = new FluentObject();

            fluentObject.Name = NamedFluentModel.MODEL_NAME;
            fluentObject.Add(NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.BOOLEAN_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.DATE_PROPERTY_NAME, NamedFluentModel.DATE_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.DOUBLE_PROPERTY_NAME, NamedFluentModel.DOUBLE_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.INTEGER_PROPERTY_NAME, NamedFluentModel.INTEGER_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.OBJECT_PROPERTY_NAME, NamedFluentModel.OBJECT_VALUE.ToString());

            Assertions(fluentObject);
        }

        [TestMethod]
        public void FluentObject_ConstructsWithNameParameter()
        {
            var fluentObject = new FluentObject(NamedFluentModel.MODEL_NAME);

            fluentObject.Add(NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.BOOLEAN_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.DATE_PROPERTY_NAME, NamedFluentModel.DATE_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.DOUBLE_PROPERTY_NAME, NamedFluentModel.DOUBLE_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.INTEGER_PROPERTY_NAME, NamedFluentModel.INTEGER_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.OBJECT_PROPERTY_NAME, NamedFluentModel.OBJECT_VALUE.ToString());

            Assertions(fluentObject); 
        }

        [TestMethod]
        public void FluentObject_ConstructsWithNameAndValuesParameters()
        {
            var name = NamedFluentModel.MODEL_NAME;

            var values = new Dictionary<string, string>();
            values.Add(NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.BOOLEAN_VALUE.ToString());
            values.Add(NamedFluentModel.DATE_PROPERTY_NAME, NamedFluentModel.DATE_VALUE.ToString());
            values.Add(NamedFluentModel.DOUBLE_PROPERTY_NAME, NamedFluentModel.DOUBLE_VALUE.ToString());
            values.Add(NamedFluentModel.INTEGER_PROPERTY_NAME, NamedFluentModel.INTEGER_VALUE.ToString());
            values.Add(NamedFluentModel.OBJECT_PROPERTY_NAME, NamedFluentModel.OBJECT_VALUE.ToString());

            var fluentObject = new FluentObject(name, values);

            Assertions(fluentObject);
        }

        public void ToString_ShouldReturnStringRepresentation()
        {
            var format = "{0}:{1}{2}";
            var builder = new StringBuilder();
            builder.AppendLine(NamedFluentModel.MODEL_NAME);
            builder.AppendFormat(format, NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.BOOLEAN_VALUE, Environment.NewLine);
            builder.AppendFormat(format, NamedFluentModel.DATE_PROPERTY_NAME, NamedFluentModel.DATE_VALUE, Environment.NewLine);
            builder.AppendFormat(format, NamedFluentModel.DOUBLE_PROPERTY_NAME, NamedFluentModel.DOUBLE_VALUE, Environment.NewLine);
            builder.AppendFormat(format, NamedFluentModel.INTEGER_PROPERTY_NAME, NamedFluentModel.INTEGER_VALUE, Environment.NewLine);
            builder.AppendFormat(format, NamedFluentModel.OBJECT_PROPERTY_NAME, NamedFluentModel.OBJECT_VALUE, Environment.NewLine);

            var fluentObject = new FluentObject();
            fluentObject.Name = NamedFluentModel.MODEL_NAME;
            fluentObject.Add(NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.BOOLEAN_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.DATE_PROPERTY_NAME, NamedFluentModel.DATE_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.DOUBLE_PROPERTY_NAME, NamedFluentModel.DOUBLE_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.INTEGER_PROPERTY_NAME, NamedFluentModel.INTEGER_VALUE.ToString());
            fluentObject.Add(NamedFluentModel.OBJECT_PROPERTY_NAME, NamedFluentModel.OBJECT_VALUE.ToString());

            var fluentObjectString = fluentObject.ToString();
            var expectedObjectString = builder.ToString();

            Assert.AreEqual(fluentObjectString, expectedObjectString);
        }

        private void Assertions(FluentObject fluentObject)
        {
            Assert.IsTrue(fluentObject.Name.Equals(NamedFluentModel.MODEL_NAME));
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

            var booleanString = "False";
            var dateString = (new DateTime(2018, 10, 10)).ToString();
            var doubleString = 100.6.ToString();
            var integerString = 100.ToString();
            var objectString = new Object().ToString();

            fluentObject[NamedFluentModel.BOOLEAN_PROPERTY_NAME] = booleanString;
            fluentObject[NamedFluentModel.DATE_PROPERTY_NAME] = dateString;
            fluentObject[NamedFluentModel.DOUBLE_PROPERTY_NAME] = doubleString;
            fluentObject[NamedFluentModel.INTEGER_PROPERTY_NAME] = integerString;
            fluentObject[NamedFluentModel.OBJECT_PROPERTY_NAME] = objectString;

            Assert.AreEqual(fluentObject[NamedFluentModel.BOOLEAN_PROPERTY_NAME], booleanString);
            Assert.AreEqual(fluentObject[NamedFluentModel.DATE_PROPERTY_NAME], dateString);
            Assert.AreEqual(fluentObject[NamedFluentModel.DOUBLE_PROPERTY_NAME], doubleString);
            Assert.AreEqual(fluentObject[NamedFluentModel.INTEGER_PROPERTY_NAME], integerString);
            Assert.AreEqual(fluentObject[NamedFluentModel.OBJECT_PROPERTY_NAME], objectString);
        }
    }
}
