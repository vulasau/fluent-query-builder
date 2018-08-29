using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests
{
    [TestClass]
    public class ConditionResolverTests
    {
        private IConditionResolver _conditionResolver;

        [TestInitialize]
        public void InitializeTest()
        {
            _conditionResolver = new ConditionResolverBase();
        }

        [TestMethod]
        public void IsValid_ReturnsTrue()
        {
            var isValid = _conditionResolver.IsValid("SomeCondition");
            Assert.IsTrue(isValid);
        }
    }
}
