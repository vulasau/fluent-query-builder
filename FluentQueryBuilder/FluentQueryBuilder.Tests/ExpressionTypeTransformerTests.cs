using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests
{
    [TestClass]
    public class ExpressionTypeTransformerTests
    {
        private IExpressionTypeTransformer _expressionTypeTransformer;

        [TestInitialize]
        public void InitializeTest()
        {
            _expressionTypeTransformer = new ExpressionTypeTransformer();
        }

        [TestMethod]
        public void Transform_TransformsKnownExpressionsToString()
        {
            var forAdd = _expressionTypeTransformer.Transform(ExpressionType.Add);
            var forDecrement = _expressionTypeTransformer.Transform(ExpressionType.Decrement);
            var forMultiply = _expressionTypeTransformer.Transform(ExpressionType.Multiply);
            var forDivide = _expressionTypeTransformer.Transform(ExpressionType.Divide);

            var forAndAlso = _expressionTypeTransformer.Transform(ExpressionType.AndAlso);
            var forOrElse = _expressionTypeTransformer.Transform(ExpressionType.OrElse);

            var forEqual = _expressionTypeTransformer.Transform(ExpressionType.Equal);
            var forNotEqual = _expressionTypeTransformer.Transform(ExpressionType.NotEqual);
            var forGreaterThan = _expressionTypeTransformer.Transform(ExpressionType.GreaterThan);
            var forGreaterThanOrEqual = _expressionTypeTransformer.Transform(ExpressionType.GreaterThanOrEqual);
            var forLessThan = _expressionTypeTransformer.Transform(ExpressionType.LessThan);
            var forLessThanOrEqual = _expressionTypeTransformer.Transform(ExpressionType.LessThanOrEqual);

            Assert.AreEqual(forAdd, "+");
            Assert.AreEqual(forDecrement, "-");
            Assert.AreEqual(forMultiply, "*");
            Assert.AreEqual(forDivide, "/");

            Assert.AreEqual(forAndAlso, "AND");
            Assert.AreEqual(forOrElse, "OR");

            Assert.AreEqual(forEqual, "=");
            Assert.AreEqual(forNotEqual, "!=");
            Assert.AreEqual(forGreaterThan, ">");
            Assert.AreEqual(forGreaterThanOrEqual, ">=");
            Assert.AreEqual(forLessThan, "<");
            Assert.AreEqual(forLessThanOrEqual, "<=");
        }

        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void Transform_ThrowsException_WhenExpressionTypeIsNotSupported()
        {
            var forNothing = _expressionTypeTransformer.Transform(ExpressionType.AddAssign);
        }
    }
}
