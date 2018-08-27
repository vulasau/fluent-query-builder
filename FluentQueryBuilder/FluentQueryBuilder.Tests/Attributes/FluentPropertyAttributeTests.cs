using System.Linq;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentQueryBuilder.Tests.Attributes
{
    [TestClass]
    public class FluentPropertyAttributeTests
    {
        [TestMethod]
        public void FluentPropertyAttribute()
        {
            var model = new NamedFluentModel();
            var type = model.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var fluentPropertyAttribute = property.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
                
                Assert.IsNotNull(fluentPropertyAttribute);
                Assert.IsInstanceOfType(fluentPropertyAttribute, typeof (FluentPropertyAttribute));

                if (property.Name.Equals("BooleanProperty"))
                    Assert.AreEqual(fluentPropertyAttribute.Name, NamedFluentModel.BOOLEAN_PROPERTY_NAME);
                else if (property.Name.Equals("DateProperty"))
                    Assert.AreEqual(fluentPropertyAttribute.Name, NamedFluentModel.DATE_PROPERTY_NAME);
                else if (property.Name.Equals("DoubleProperty"))
                    Assert.AreEqual(fluentPropertyAttribute.Name, NamedFluentModel.DOUBLE_PROPERTY_NAME);
                else if (property.Name.Equals("IntegerProperty"))
                    Assert.AreEqual(fluentPropertyAttribute.Name, NamedFluentModel.INTEGER_PROPERTY_NAME);
                else if (property.Name.Equals("ObjectProperty"))
                    Assert.AreEqual(fluentPropertyAttribute.Name, NamedFluentModel.OBJECT_PROPERTY_NAME);
            }
        }
    }
}
