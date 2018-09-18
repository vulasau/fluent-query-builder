using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using FluentQueryBuilder.Extensions;
using FluentQueryBuilder.Tests.Models;

namespace FluentQueryBuilder.Tests.Extensions
{
    [TestClass]
    public class ExpressionExtensionsTests
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

            expressionString = ParseExpression<NamedFluentModel>(x => x.ObjectProperty == null);
            expectedString = string.Format("{0} = NULL", NamedFluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.ObjectProperty != null);
            expectedString = string.Format("{0} != NULL", NamedFluentModel.OBJECT_PROPERTY_NAME);
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

            expressionString = ParseExpression<FluentModel>(x => x.ObjectProperty == null);
            expectedString = string.Format("{0} = NULL", FluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.ObjectProperty != null);
            expectedString = string.Format("{0} != NULL", FluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);
        }

        [TestMethod]
        public void ParseCompiledConstantComparisonExpression()
        {
            var trueValue = true;
            var doubleValue = 50.55;
            var integerValue = 70;
            object nullable = null;

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

            expressionString = ParseExpression<NamedFluentModel>(x => x.ObjectProperty == nullable);
            expectedString = string.Format("{0} = NULL", NamedFluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.ObjectProperty != nullable);
            expectedString = string.Format("{0} != NULL", NamedFluentModel.OBJECT_PROPERTY_NAME);
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

            expressionString = ParseExpression<FluentModel>(x => x.ObjectProperty == nullable);
            expectedString = string.Format("{0} = NULL", FluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.ObjectProperty != nullable);
            expectedString = string.Format("{0} != NULL", FluentModel.OBJECT_PROPERTY_NAME);
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


            // Null comparison
            model.ObjectProperty = null;

            expressionString = ParseExpression<NamedFluentModel>(x => x.ObjectProperty == model.ObjectProperty);
            expectedString = string.Format("{0} = NULL", NamedFluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.ObjectProperty != model.ObjectProperty);
            expectedString = string.Format("{0} != NULL", FluentModel.OBJECT_PROPERTY_NAME);
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


            // Null comparison
            Func<object> method = () => { return null; };

            expressionString = ParseExpression<NamedFluentModel>(x => x.ObjectProperty == method.Invoke());
            expectedString = string.Format("{0} = NULL", NamedFluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.ObjectProperty != method.Invoke());
            expectedString = string.Format("{0} != NULL", FluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);
        }


        [TestMethod]
        public void ParseConditionalExpression()
        {
            var booleanValue = !false;
            var integerValue = 70;
            var doubleValue = 50.55;
            object nullable = null;

            // Named attributes
            var expressionString = ParseExpression<NamedFluentModel>(x => x.BooleanProperty == booleanValue && x.IntegerProperty > integerValue || x.ObjectProperty == nullable);
            var expectedString = string.Format("(({0} = True) AND ({1} > {2})) OR ({3} = NULL)", NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.INTEGER_PROPERTY_NAME, integerValue, NamedFluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.BooleanProperty == booleanValue || (x.IntegerProperty > integerValue && x.DoubleProperty < doubleValue));
            expectedString = string.Format("({0} = True) OR (({1} > {2}) AND ({3} < {4}))", NamedFluentModel.BOOLEAN_PROPERTY_NAME, NamedFluentModel.INTEGER_PROPERTY_NAME, integerValue, NamedFluentModel.DOUBLE_PROPERTY_NAME, doubleValue);
            Assert.AreEqual(expressionString, expectedString);


            // Unnamed attributes
            expressionString = ParseExpression<FluentModel>(x => x.BooleanProperty == booleanValue && x.IntegerProperty > integerValue || x.ObjectProperty != nullable);
            expectedString = string.Format("(({0} = True) AND ({1} > {2})) OR ({3} != NULL)", FluentModel.BOOLEAN_PROPERTY_NAME, FluentModel.INTEGER_PROPERTY_NAME, integerValue, FluentModel.OBJECT_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<FluentModel>(x => x.BooleanProperty == booleanValue || (x.IntegerProperty > integerValue && x.DoubleProperty < doubleValue));
            expectedString = string.Format("({0} = True) OR (({1} > {2}) AND ({3} < {4}))", FluentModel.BOOLEAN_PROPERTY_NAME, FluentModel.INTEGER_PROPERTY_NAME, integerValue, FluentModel.DOUBLE_PROPERTY_NAME, doubleValue);
            Assert.AreEqual(expressionString, expectedString);
        }


        [TestMethod]
        public void ParseMathComparisonExpressions()
        {
            var actions = new Actions();
            actions.IntegerProperty = 10;

            var expressionString = ParseExpression<NamedFluentModel>(x => x.IntegerProperty == 10 + actions.IntegerProperty);
            var expectedString = string.Format("{0} = (10 + 10)", NamedFluentModel.INTEGER_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);

            expressionString = ParseExpression<NamedFluentModel>(x => x.IntegerProperty >= 10 + actions.IntegerProperty && x.DoubleProperty <= actions.GetValue(3));
            expectedString = string.Format("({0} >= (10 + 10)) AND ({1} <= 30)", NamedFluentModel.INTEGER_PROPERTY_NAME, NamedFluentModel.DOUBLE_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);
        }


        [TestMethod]
        public void ParseMemberAccessSelector()
        {
            var expressionString = ParseExpression<NamedFluentModel, int>(x => x.IntegerProperty);
            var expectedString = string.Format(NamedFluentModel.INTEGER_PROPERTY_NAME);
            Assert.AreEqual(expressionString, expectedString);
        }


        private string ParseExpression<T>(Expression<Func<T, bool>> predicate)
        {
            return predicate.Parse();
        }

        private string ParseExpression<T, TOut>(Expression<Func<T, TOut>> selector)
        {
            return selector.ParseMemberExpression();
        }
    }

    public class Actions
    {
        public int IntegerProperty { get; set; }

        public int GetValue(int multiplier)
        {
            return IntegerProperty*multiplier;
        }
    }
}
