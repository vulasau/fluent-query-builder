using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using FluentQueryBuilder.Extensions;
using FluentQueryBuilder.Tests.Models;

namespace FluentQueryBuilder.Tests.Extensions
{
    [TestClass]
    public class FluentExpressionExtensionsTests
    {
        [TestMethod]
        public void ParseConstantComparisonExpression()
        {
            // Named attributes
            var expressionString = ParseExpression<NamedFluentModel>(x => x.BooleanProperty == true);
            var expectedString = string.Format("{0} = True", NamedFluentModel.BOOLEAN_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.DoubleProperty > 50.55);
            expectedString = string.Format("{0} > {1}", NamedFluentModel.DOUBLE_PROPERTY_NAME, 50.55);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.IntegerProperty < 70);
            expectedString = string.Format("{0} < {1}", NamedFluentModel.INTEGER_PROPERTY_NAME, 70);
            Assert.AreEqual(expressionString, expectedString);


            // Unnamed attributes
            expressionString = ParseExpression<FluentModel>(x => x.BooleanProperty == true);
            expectedString = string.Format("{0} = True", FluentModel.BOOLEAN_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.DoubleProperty > 50.55);
            expectedString = string.Format("{0} > {1}", FluentModel.DOUBLE_PROPERTY_NAME, 50.55);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.IntegerProperty < 70);
            expectedString = string.Format("{0} < {1}", FluentModel.INTEGER_PROPERTY_NAME, 70);
            Assert.AreEqual(expressionString, expectedString);
        }

        [TestMethod]
        public void ParseCompiledConstantComparisonExpression()
        {
            var trueValue = true;
            var doubleValue = 50.55;
            var integerValue = 70;

            // Named attributes
            var expressionString = ParseExpression<NamedFluentModel>(x => x.BooleanProperty == trueValue);
            var expectedString = string.Format("{0} = True", NamedFluentModel.BOOLEAN_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.DoubleProperty > doubleValue);
            expectedString = string.Format("{0} > {1}", NamedFluentModel.DOUBLE_PROPERTY_NAME, doubleValue);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.IntegerProperty < integerValue);
            expectedString = string.Format("{0} < {1}", NamedFluentModel.INTEGER_PROPERTY_NAME, integerValue);
            Assert.AreEqual(expressionString, expectedString);


            // Unnamed attributes
            expressionString = ParseExpression<FluentModel>(x => x.BooleanProperty == trueValue);
            expectedString = string.Format("{0} = True", FluentModel.BOOLEAN_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.DoubleProperty > doubleValue);
            expectedString = string.Format("{0} > {1}", FluentModel.DOUBLE_PROPERTY_NAME, doubleValue);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.IntegerProperty < integerValue);
            expectedString = string.Format("{0} < {1}", FluentModel.INTEGER_PROPERTY_NAME, integerValue);
            Assert.AreEqual(expressionString, expectedString);
        }

        [TestMethod]
        public void ParseMemberAccessComparisonExpression()
        {
            var model = new FluentModel();

            // Named atributes
            var expressionString = ParseExpression<NamedFluentModel>(x => x.BooleanProperty == model.BooleanProperty);
            var expectedString = string.Format("{0} = True", NamedFluentModel.BOOLEAN_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.DoubleProperty > model.DoubleProperty);
            expectedString = string.Format("{0} > {1}", NamedFluentModel.DOUBLE_PROPERTY_NAME, model.DoubleProperty);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.IntegerProperty < model.IntegerProperty);
            expectedString = string.Format("{0} < {1}", NamedFluentModel.INTEGER_PROPERTY_NAME, model.IntegerProperty);
            Assert.AreEqual(expressionString, expectedString);


            // Unnamed attributes
            expressionString = ParseExpression<FluentModel>(x => x.BooleanProperty == model.BooleanProperty);
            expectedString = string.Format("{0} = True", FluentModel.BOOLEAN_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.DoubleProperty > model.DoubleProperty);
            expectedString = string.Format("{0} > {1}", FluentModel.DOUBLE_PROPERTY_NAME, model.DoubleProperty);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.IntegerProperty < model.IntegerProperty);
            expectedString = string.Format("{0} < {1}", FluentModel.INTEGER_PROPERTY_NAME, model.IntegerProperty);
            Assert.AreEqual(expressionString, expectedString);
        }

        [TestMethod]
        public void ParseMethodCallComparisonExpression()
        {
            // Named attributes
            var expressionString = ParseExpression<NamedFluentModel>(x => x.BooleanProperty == bool.Parse("True"));
            var expectedString = string.Format("{0} = True", NamedFluentModel.BOOLEAN_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.DoubleProperty > double.Parse("50.55"));
            expectedString = string.Format("{0} > {1}", NamedFluentModel.DOUBLE_PROPERTY_NAME, 50.55);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.IntegerProperty < int.Parse("70"));
            expectedString = string.Format("{0} < {1}", NamedFluentModel.INTEGER_PROPERTY_NAME, 70);
            Assert.AreEqual(expressionString, expectedString);


            // Unnamed attributes
            expressionString = ParseExpression<FluentModel>(x => x.BooleanProperty == bool.Parse("True"));
            expectedString = string.Format("{0} = True", FluentModel.BOOLEAN_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.DoubleProperty > double.Parse("50.55"));
            expectedString = string.Format("{0} > {1}", FluentModel.DOUBLE_PROPERTY_NAME, 50.55);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.IntegerProperty < int.Parse("70"));
            expectedString = string.Format("{0} < {1}", FluentModel.INTEGER_PROPERTY_NAME, 70);
            Assert.AreEqual(expressionString, expectedString);
        }


        [TestMethod]
        public void ParseConditionalExpression()
        {
            var booleanValue = !false;
            var integerValue = 70;
            var doubleValue = 50.55;

            // Named attributes
            var expressionString = ParseExpression<NamedFluentModel>(x => x.BooleanProperty == booleanValue && x.IntegerProperty> integerValue);
            var expectedString = string.Format("({0} = True) AND ({1} > {2})", NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.INTEGER_PROPERTY_NAME, integerValue);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.BooleanProperty == booleanValue || (x.IntegerProperty > integerValue && x.DoubleProperty < doubleValue));
            expectedString = string.Format("({0} = True) OR (({1} > {2}) AND ({3} < {4}))", NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.INTEGER_PROPERTY_NAME, integerValue, NamedFluentModel.DOUBLE_PROPERTY_NAME, doubleValue);
            Assert.AreEqual(expressionString, expectedString);


            // Unnamed attributes
            expressionString = ParseExpression<FluentModel>(x => x.BooleanProperty == booleanValue && x.IntegerProperty > integerValue);
            expectedString = string.Format("({0} = True) AND ({1} > {2})", FluentModel.BOOLEAN_PROPERTY_NAME, FluentModel.INTEGER_PROPERTY_NAME, integerValue);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.BooleanProperty == booleanValue || (x.IntegerProperty > integerValue && x.DoubleProperty < doubleValue));
            expectedString = string.Format("({0} = True) OR (({1} > {2}) AND ({3} < {4}))", FluentModel.BOOLEAN_PROPERTY_NAME, FluentModel.INTEGER_PROPERTY_NAME, integerValue, FluentModel.DOUBLE_PROPERTY_NAME, doubleValue);
            Assert.AreEqual(expressionString, expectedString);
        }


        private string ParseExpression<T>(Expression<Func<T, bool>> predicate)
        {
            return predicate.Parse();
        }
    }
}
