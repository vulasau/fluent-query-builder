using System.Linq;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Attributes
{
    [TestClass]
    public class FluentEntityAttributeTests
    {
        [TestMethod]
        public void FluentEntityAttribute()
        {
            var model = new NamedFluentModel();
            var type = model.GetType();
            var fluentEntityAttribute = type.GetCustomAttributes(typeof(FluentEntityAttribute), false).SingleOrDefault() as FluentEntityAttribute;

            Assert.IsNotNull(fluentEntityAttribute);
            Assert.IsInstanceOfType(fluentEntityAttribute, typeof (FluentEntityAttribute));
            Assert.AreEqual(fluentEntityAttribute.Name, NamedFluentModel.MODEL_NAME);
        }
    }
}
